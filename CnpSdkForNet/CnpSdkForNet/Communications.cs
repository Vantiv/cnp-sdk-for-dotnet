using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using Renci.SshNet.Common;

namespace Cnp.Sdk
{
    public sealed class Communications
    {
        private static readonly object SynLock = new object();

        public event EventHandler HttpAction;

        private readonly HttpClient client;

        private readonly Dictionary<string, string> _config;

        public Communications(Dictionary<string, string> config = null)
        {
            _config = config ?? new ConfigManager().getConfig();
            
            // The handler specifies several fields we need that cannot be directly set on the HttpClient
            var handler = new HttpClientHandler {SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13};

            // Set the maximum connections for the client, if specified
            if (IsValidConfigValueSet("maxConnections"))
            {
                int.TryParse(_config["maxConnections"], out var maxConnections);
                if (maxConnections > 0)
                {
                    handler.MaxConnectionsPerServer = maxConnections;
                }
            }

            // Configure the client to use the proxy, if specified
            if (IsProxyOn())
            {
                handler.Proxy = new WebProxy(_config["proxyHost"], int.Parse(_config["proxyPort"]))
                {
                    BypassProxyOnLocal = true
                };
                handler.UseProxy = true;
            }

            // Now that the handler is set up, configure any remaining fields on the HttpClient
            client = new HttpClient(handler) {BaseAddress = new Uri(_config["url"])};
            // TODO client.DefaultRequestHeaders.? (note: there is an Add method on the obj for custom)
            // also, any other handler things we need to set?
           /*
            for reference, the old code:
            public HttpWebRequest CreateWebRequest(string xmlRequest)
            {
                request.ServicePoint.MaxIdleTime = 8000;
                request.ServicePoint.Expect100Continue = false;
            }
            */
            
            // Set the timeout for the client, if specified
            if (_config.ContainsKey("timeout"))
            {
                // Read timeout from config and default to 60000 (1 minute) if it cannot be parsed
                var timeoutInMillis = int.TryParse(_config["timeout"], out var temp) ? temp : 60000;
                client.Timeout = TimeSpan.FromMilliseconds(timeoutInMillis);
            }
        }

        private void OnHttpAction(RequestType requestType, string xmlPayload)
        {
            if (HttpAction == null) return;
 
            NeuterXml(ref xmlPayload);
            NeuterUserCredentials(ref xmlPayload);

            HttpAction(this, new HttpActionEventArgs(requestType, xmlPayload));
        }

        public static bool ValidateServerCertificate(
             object sender,
             X509Certificate certificate,
             X509Chain chain,
             SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            Console.WriteLine("Certificate error: {0}", sslPolicyErrors);

            // Do not allow this client to communicate with unauthenticated servers.
            return false;
        }

        // Neuters the XML if needed
        public void NeuterXml(ref string inputXml)
        {
            var neuterAccountNumbers = 
                _config.ContainsKey("neuterAccountNums") && "true".Equals(_config["neuterAccountNums"]);
            if (!neuterAccountNumbers) return;
            
            const string pattern1 = "(?i)<number>.*?</number>";
            const string pattern2 = "(?i)<accNum>.*?</accNum>";
            const string pattern3 = "(?i)<track>.*?</track>";
            const string pattern4 = "(?i)<accountNumber>.*?</accountNumber>";

            var rgx1 = new Regex(pattern1);
            var rgx2 = new Regex(pattern2);
            var rgx3 = new Regex(pattern3);
            var rgx4 = new Regex(pattern4);
            inputXml = rgx1.Replace(inputXml, "<number>xxxxxxxxxxxxxxxx</number>");
            inputXml = rgx2.Replace(inputXml, "<accNum>xxxxxxxxxx</accNum>");
            inputXml = rgx3.Replace(inputXml, "<track>xxxxxxxxxxxxxxxxxxxxxxxxxxxxxx</track>");
            inputXml = rgx4.Replace(inputXml, "<accountNumber>xxxxxxxxxxxxxxxx</accountNumber>");
        }

        // Neuters the user credentials if needed
        public void NeuterUserCredentials(ref string inputXml)
        {
            var neuterUserCredentials =
                _config.ContainsKey("neuterUserCredentials") && "true".Equals(_config["neuterUserCredentials"]);
            if (!neuterUserCredentials) return;

            const string pattern1 = "(?i)<user>.*?</user>";
            const string pattern2 = "(?i)<password>.*></password>";

            var rgx1 = new Regex(pattern1);
            var rgx2 = new Regex(pattern2);
            inputXml = rgx1.Replace(inputXml, "<user>xxxxxx</user>");
            inputXml = rgx2.Replace(inputXml, "<password>xxxxxxxx</password>");
        }

        public void Log(string logMessage, string logFile)
        {
            lock (SynLock)
            {
                NeuterXml(ref logMessage);
                NeuterUserCredentials(ref logMessage);
 
                using (var logWriter = new StreamWriter(logFile, true))
                {
                    var time = DateTime.Now;
                    logWriter.WriteLine(time.ToString(CultureInfo.InvariantCulture));
                    logWriter.WriteLine(logMessage + "\r\n");
                }
            }
        }

        // Post the specified XML asynchronously
        public async Task<string> HttpPostAsync(string xmlRequest, CancellationToken cancellationToken)
        {
            // First, read values from the config that we need that relate to logging
            _config.TryGetValue("logFile", out var logFile);
            var printXml = _config.ContainsKey("printxml") && "true".Equals(_config["printxml"]);

            // Log any data to the appropriate places, only if we need to
            if (printXml)
            {
                Console.WriteLine(xmlRequest);
                Console.WriteLine(logFile);
            }
            if (logFile != null)
            {
                Log(xmlRequest, logFile);
            }
            
            // Now that we have gotten the values for logging from the config, we need to actually send the request
            try
            {
                OnHttpAction(RequestType.Request, xmlRequest);
                var xmlContent = new StringContent(xmlRequest, Encoding.UTF8, "application/xml");
                var response = await client.PostAsync(_config["url"], xmlContent, cancellationToken);
                var xmlResponse = await response.Content.ReadAsStringAsync();
                OnHttpAction(RequestType.Response, xmlResponse);

                if (printXml)
                {
                    Console.WriteLine(xmlResponse);
                }
                if (logFile != null)
                {
                    Log(xmlResponse, logFile);
                }

                return xmlResponse;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string HttpPost(string xmlRequest)
        {
            return HttpPostAsync(xmlRequest, null).Result;
            // TODO do we need a .Wait() on the task before a .Result?
            // TODO do we need a Task.Run()? What about .ConfigureAwait(false)?
            // https://devblogs.microsoft.com/dotnet/configureawait-faq/
            // https://docs.microsoft.com/en-us/archive/blogs/jpsanders/asp-net-do-not-use-task-result-in-main-context
        }

        public bool IsProxyOn()
        {
            return IsValidConfigValueSet("proxyHost") && IsValidConfigValueSet("proxyPort");
        }

        public bool IsValidConfigValueSet(string propertyName)
        {
            return _config.ContainsKey(propertyName) && !string.IsNullOrEmpty(_config[propertyName]);
        }

        public void FtpDropOff(string fileDirectory, string fileName)
        {
            SftpClient sftpClient;

            var url = _config["sftpUrl"];
            var username = _config["sftpUsername"];
            var password = _config["sftpPassword"];
            var filePath = Path.Combine(fileDirectory, fileName);

            var printxml = _config["printxml"] == "true";
            if (printxml)
            {
                Console.WriteLine("Sftp Url: " + url);
                Console.WriteLine("Username: " + username);
                // Console.WriteLine("Password: " + password);
            }

            sftpClient = new SftpClient(url, username, password);

            try
            {
                sftpClient.Connect();
            }
            catch (SshConnectionException e)
            {
                throw new CnpOnlineException("Error occured while establishing an SFTP connection", e);
            }
            catch (SshAuthenticationException e)
            {
                throw new CnpOnlineException("Error occured while attempting to establish an SFTP connection", e);
            }

            try {
                if (printxml) {
                    Console.WriteLine("Dropping off local file " + filePath + " to inbound/" + fileName + ".prg");
                }

                FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
                sftpClient.UploadFile(fileStream, "inbound/" + fileName + ".prg");
                fileStream.Close();
                if (printxml) {
                    Console.WriteLine("File copied - renaming from inbound/" + fileName + ".prg to inbound/" +
                                      fileName + ".asc");
                }

                sftpClient.RenameFile("inbound/" + fileName + ".prg", "inbound/" + fileName + ".asc");
            }
            catch (SshConnectionException e) {
                throw new CnpOnlineException("Error occured while attempting to upload and save the file to SFTP", e);
            }
            catch (SshException e) {
                throw new CnpOnlineException("Error occured while attempting to upload and save the file to SFTP", e);
            }
            finally {
                sftpClient.Disconnect();
            }
        }

        public void FtpPoll(string fileName, int timeout, Dictionary<string, string> config)
        {
            fileName = fileName + ".asc";
            var printxml = config["printxml"] == "true";
            if (printxml)
            {
                Console.WriteLine("Polling for outbound result file.  Timeout set to " + timeout + "ms. File to wait for is " + fileName);
            }

            SftpClient sftpClient;

            var url = config["sftpUrl"];
            var username = config["sftpUsername"];
            var password = config["sftpPassword"];

            sftpClient = new SftpClient(url, username, password);

            try
            {

                sftpClient.Connect();

            }
            catch (SshConnectionException e)
            {
                throw new CnpOnlineException("Error occured while establishing an SFTP connection", e);
            }
            catch (SshAuthenticationException e)
            {
                throw new CnpOnlineException("Error occured while attempting to establish an SFTP connection", e);
            }

            SftpFileAttributes sftpAttrs = null;
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            do
            {
                if (printxml)
                {
                    Console.WriteLine("Elapsed time is " + stopWatch.Elapsed.TotalMilliseconds);
                }
                try
                {
                    sftpAttrs = sftpClient.Get("outbound/" + fileName).Attributes;
                    if (printxml)
                    {
                        Console.WriteLine("Attrs of file are: " + getSftpFileAttributes(sftpAttrs));
                    }
                }
                catch (SshConnectionException e)
                {
                    if (printxml)
                    {
                        Console.WriteLine(e.Message);
                    }
                    System.Threading.Thread.Sleep(30000);
                }
                catch (SftpPathNotFoundException e)
                {
                    if (printxml)
                    {
                        Console.WriteLine(e.Message);
                    }
                    System.Threading.Thread.Sleep(30000);
                }
            } while (sftpAttrs == null && stopWatch.Elapsed.TotalMilliseconds <= timeout);

            // Close the connections.
            sftpClient.Disconnect();
        }

        public void FtpPickUp(string destinationFilePath, string fileName)
        {
            SftpClient sftpClient;

            var printxml = _config["printxml"] == "true";
            var url = _config["sftpUrl"];
            var username = _config["sftpUsername"];
            var password = _config["sftpPassword"];

            sftpClient = new SftpClient(url, username, password);

            try
            {
                sftpClient.Connect();
            }
            catch (SshConnectionException e)
            {
                throw new CnpOnlineException("Error occured while attempting to establish an SFTP connection", e);
            }

            try {
                if (printxml) {
                    Console.WriteLine("Picking up remote file outbound/" + fileName + ".asc");
                    Console.WriteLine("Putting it at " + destinationFilePath);
                }

                FileStream downloadStream = new FileStream(destinationFilePath, FileMode.Create, FileAccess.ReadWrite);
                sftpClient.DownloadFile("outbound/" + fileName + ".asc", downloadStream);
                downloadStream.Close();
                if (printxml) {
                    Console.WriteLine("Removing remote file output/" + fileName + ".asc");
                }

                sftpClient.Delete("outbound/" + fileName + ".asc");
            }
            catch (SshConnectionException e) {
                throw new CnpOnlineException("Error occured while attempting to retrieve and save the file from SFTP",
                    e);
            }
            catch (SftpPathNotFoundException e) {
                throw new CnpOnlineException("Error occured while attempting to locate desired SFTP file path", e);
            }
            finally {
                sftpClient.Disconnect();
            }
        }

        public enum RequestType
        {
            Request, Response
        }

        public class HttpActionEventArgs : EventArgs
        {
            public RequestType RequestType { get; set; }
            public string XmlPayload;

            public HttpActionEventArgs(RequestType requestType, string xmlPayload)
            {
                RequestType = requestType;
                XmlPayload = xmlPayload;
            }
        }

        private String getSftpFileAttributes(SftpFileAttributes sftpAttrs)
        {
            String permissions = sftpAttrs.GetBytes().ToString();
            return "Permissions: " + permissions
                                   + " | UserID: " + sftpAttrs.UserId
                                   + " | GroupID: " + sftpAttrs.GroupId
                                   + " | Size: " + sftpAttrs.Size
                                   + " | LastEdited: " + sftpAttrs.LastWriteTime.ToString();
        }

        public struct SshConnectionInfo
        {
            public string Host;
            public string User;
            public string Pass;
            public string IdentityFile;
        }
    }
}
