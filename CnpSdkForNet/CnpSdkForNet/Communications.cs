using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using Renci.SshNet.Common;

namespace Cnp.Sdk
{
    public class Communications
    {
        private static readonly object SynLock = new object();
        public static string ContentTypeTextXmlUTF8 = "text/xml; charset=UTF-8";

        public event EventHandler HttpAction;


        private void OnHttpAction(RequestType requestType, string xmlPayload, bool neuterAccNums, bool neuterCreds)
        {
            if (HttpAction != null)
            {
                if (neuterAccNums)
                {
                    NeuterXml(ref xmlPayload);
                }

                if (neuterCreds)
                {
                    NeuterUserCredentials(ref xmlPayload);
                }

                HttpAction(this, new HttpActionEventArgs(requestType, xmlPayload));
            }
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

        public void NeuterXml(ref string inputXml)
        {

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

        public void NeuterUserCredentials(ref string inputXml)
        {

            const string pattern1 = "(?i)<user>.*?</user>";
            const string pattern2 = "(?i)<password>.*></password>";

            var rgx1 = new Regex(pattern1);
            var rgx2 = new Regex(pattern2);
            inputXml = rgx1.Replace(inputXml, "<user>xxxxxx</user>");
            inputXml = rgx2.Replace(inputXml, "<password>xxxxxxxx</password>");
        }

        public void Log(string logMessage, string logFile, bool neuterAccNums, bool neuterCreds)
        {
            lock (SynLock)
            {
                if (neuterAccNums)
                {
                    NeuterXml(ref logMessage);
                }
                if (neuterCreds)
                {
                    NeuterUserCredentials(ref logMessage);
                }
                using (var logWriter = new StreamWriter(logFile, true))
                {
                    var time = DateTime.Now;
                    logWriter.WriteLine(time.ToString(CultureInfo.InvariantCulture));
                    logWriter.WriteLine(logMessage + "\r\n");
                }
            }
        }
        
        public HttpWebRequest CreateWebRequest(string xmlRequest,Dictionary<string, string> config)
        { 
            // Get the log file.
            string logFile = null;
            if (IsValidConfigValueSet(config, "logFile"))
            {
                logFile = config["logFile"];
            }
            
            // Get the rest of the configuration values.
            var requestUrl = config["url"];
            var printXml = false;
            var neuterAccountNumbers = false;
            var neuterUserCredentials = false;
            if (config.ContainsKey("neuterAccountNums"))
            {
                neuterAccountNumbers = ("true".Equals(config["neuterAccountNums"]));
            }
            if (config.ContainsKey("neuterUserCredentials"))
            {
                neuterUserCredentials = ("true".Equals(config["neuterUserCredentials"]));
            }
            if (config.ContainsKey("printxml"))
            {
                printXml = ("true".Equals(config["printxml"]));
            }
            
            // Get the request target information.
            ServicePointManager.SecurityProtocol = ServicePointManager.SecurityProtocol | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
            var request = (HttpWebRequest)WebRequest.Create(requestUrl);

            // Output the request and log file.
            if (printXml)
            {
                Console.WriteLine(xmlRequest);
                Console.WriteLine(logFile);
            }

            // Log the request.
            if (logFile != null)
            {
                this.Log(xmlRequest,logFile, neuterAccountNumbers,neuterUserCredentials);
            }

            // Set up the request.
            request.ContentType = ContentTypeTextXmlUTF8;
            request.Method = "POST";
            request.ServicePoint.MaxIdleTime = 8000;
            request.ServicePoint.Expect100Continue = false;
            request.KeepAlive = true;
            if (IsProxyOn(config))
            {
                var proxy = new WebProxy(config["proxyHost"], int.Parse(config["proxyPort"]))
                {
                    BypassProxyOnLocal = true
                };
                request.Proxy = proxy;
            }

            // Set the timeout (only effective for non-async requests.
            if (config.ContainsKey("timeout")) {
                try {
                    request.Timeout = Convert.ToInt32(config["timeout"]);
                }
                catch (FormatException e) {
                    // If timeout setting contains non-numeric
                    // characters, we will fall back to 1 minute
                    // default timeout.
                    request.Timeout = 60000;
                }
            }

            // Invoke the event.
            this.OnHttpAction(Communications.RequestType.Request,xmlRequest,neuterAccountNumbers,neuterUserCredentials);
            
            // Return the request.
            return request;
        }

        public virtual Task<string> HttpPostAsync(string xmlRequest, Dictionary<string, string> config, CancellationToken cancellationToken)
        {
            return HttpPostCoreAsync(xmlRequest, config, cancellationToken);
        }

        private async Task<string> HttpPostCoreAsync(string xmlRequest, Dictionary<string, string> config, CancellationToken cancellationToken)
        {
            string logFile = null;
            var printXml = false;
            var neuterAccountNumbers = false;
            var neuterUserCredentials = false;
            if (IsValidConfigValueSet(config, "logFile"))
            {
                logFile = config["logFile"];
            }
            if (config.ContainsKey("neuterAccountNums"))
            {
                neuterAccountNumbers = ("true".Equals(config["neuterAccountNums"]));
            }
            if (config.ContainsKey("neuterUserCredentials"))
            {
                neuterUserCredentials = ("true".Equals(config["neuterUserCredentials"]));
            }
            if (config.ContainsKey("printxml"))
            {
                printXml = ("true".Equals(config["printxml"]));
            }
            
            // submit http request
            var request = this.CreateWebRequest(xmlRequest, config);
            using (var writer = new StreamWriter(await request.GetRequestStreamAsync().ConfigureAwait(false)))
            {
                writer.Write(xmlRequest);
            }

            // read response
            string xmlResponse = null;
            var response = await request.GetResponseAsync().ConfigureAwait(false);
            try
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    xmlResponse = (await reader.ReadToEndAsync().ConfigureAwait(false)).Trim();
                }
                if (printXml)
                {
                    Console.WriteLine(xmlResponse);
                }

                OnHttpAction(RequestType.Response, xmlResponse, neuterAccountNumbers, neuterUserCredentials);

                //log response
                if (logFile != null)
                {
                    Log(xmlResponse, logFile, neuterAccountNumbers, neuterUserCredentials);
                }
            }catch (WebException we)
            {
                
            }

            return xmlResponse;
        }
       
        public bool IsProxyOn(Dictionary<string, string> config)
        {
            return IsValidConfigValueSet(config, "proxyHost") && IsValidConfigValueSet(config, "proxyPort");
        }

        public bool IsValidConfigValueSet(Dictionary<string, string> config, string propertyName)
        {
            return config != null && config.ContainsKey(propertyName) && !String.IsNullOrEmpty(config[propertyName]);
        }

        public virtual string HttpPost(string xmlRequest, Dictionary<string, string> config)
        {
            string logFile = null;
            var printXml = false;
            var neuterAccountNumbers = false;
            var neuterUserCredentials = false;
            if (IsValidConfigValueSet(config, "logFile"))
            {
                logFile = config["logFile"];
            }
            if (config.ContainsKey("neuterAccountNums"))
            {
                neuterAccountNumbers = ("true".Equals(config["neuterAccountNums"]));
            }
            if (config.ContainsKey("neuterUserCredentials"))
            {
                neuterUserCredentials = ("true".Equals(config["neuterUserCredentials"]));
            }
            if (config.ContainsKey("printxml"))
            {
                printXml = ("true".Equals(config["printxml"]));
            }
            
            // submit http request
            var request = this.CreateWebRequest(xmlRequest, config);

            // submit http request
            using (var writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(xmlRequest);
            }

            // read response
            string xmlResponse = null;
            try
            {
                var resp = request.GetResponse();
                if (resp == null)
                {
                    return null;
                }
                HttpWebResponse httpResp = (HttpWebResponse)resp;

                using (var reader = new StreamReader(resp.GetResponseStream()))
                {
                    xmlResponse = reader.ReadToEnd().Trim();
                }
                if (printXml)
                {
                    Console.WriteLine(xmlResponse);
                }

                OnHttpAction(RequestType.Response, xmlResponse, neuterAccountNumbers, neuterUserCredentials);

                //log response
                if (logFile != null)
                {
                    Log(xmlResponse, logFile, neuterAccountNumbers, neuterUserCredentials);
                }
            } catch (WebException we)
            {
                
            }
            
            return xmlResponse;
        }



        //public virtual string SocketStream(string xmlRequestFilePath, string xmlResponseDestinationDirectory, Dictionary<string, string> config)
        //{
        //    var url = config["onlineBatchUrl"];
        //    var port = int.Parse(config["onlineBatchPort"]);
        //    TcpClient tcpClient;
        //    SslStream sslStream;

        //    try
        //    {
        //        tcpClient = new TcpClient(url, port);
        //        sslStream = new SslStream(tcpClient.GetStream(), false, ValidateServerCertificate, null);
        //    }
        //    catch (SocketException e)
        //    {
        //        throw new CnpOnlineException("Error establishing a network connection", e);
        //    }

        //    try
        //    {
        //        sslStream.AuthenticateAsClient(url);
        //    }
        //    catch (AuthenticationException e)
        //    {
        //        tcpClient.Close();
        //        throw new CnpOnlineException("Error establishing a network connection - SSL Authencation failed", e);
        //    }

        //    if ("true".Equals(config["printxml"]))
        //    {
        //        Console.WriteLine("Using XML File: " + xmlRequestFilePath);
        //    }

        //    using (var readFileStream = new FileStream(xmlRequestFilePath, FileMode.Open))
        //    {
        //        var bytesRead = -1;

        //        do
        //        {
        //            var byteBuffer = new byte[1024 * sizeof(char)];
        //            bytesRead = readFileStream.Read(byteBuffer, 0, byteBuffer.Length);

        //            sslStream.Write(byteBuffer, 0, bytesRead);
        //            sslStream.Flush();
        //        } while (bytesRead != 0);
        //    }

        //    var batchName = Path.GetFileName(xmlRequestFilePath);
        //    var destinationDirectory = Path.GetDirectoryName(xmlResponseDestinationDirectory);
        //    if (!Directory.Exists(destinationDirectory))
        //    {
        //        if (destinationDirectory != null) Directory.CreateDirectory(destinationDirectory);
        //    }

        //    if ("true".Equals(config["printxml"]))
        //    {
        //        Console.WriteLine("Writing to XML File: " + xmlResponseDestinationDirectory + batchName);
        //    }

        //    using (var writeFileStream = new FileStream(xmlResponseDestinationDirectory + batchName, FileMode.Create))
        //    {
        //        int bytesRead;

        //        do
        //        {
        //            var byteBuffer = new byte[1024 * sizeof(char)];
        //            bytesRead = sslStream.Read(byteBuffer, 0, byteBuffer.Length);

        //            writeFileStream.Write(byteBuffer, 0, bytesRead);
        //        } while (bytesRead > 0);
        //    }

        //    tcpClient.Close();
        //    sslStream.Close();

        //    return xmlResponseDestinationDirectory + batchName;
        //}

        public virtual void FtpDropOff(string fileDirectory, string fileName, Dictionary<string, string> config)
        {
            SftpClient sftpClient;

            var url = config["sftpUrl"];
            var username = config["sftpUsername"];
            var password = config["sftpPassword"];
            var filePath = Path.Combine(fileDirectory, fileName);

            var printxml = config["printxml"] == "true";
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

        public virtual void FtpPoll(string fileName, int timeout, Dictionary<string, string> config)
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

        public virtual void FtpPickUp(string destinationFilePath, Dictionary<string, string> config, string fileName)
        {
            SftpClient sftpClient;

            var printxml = config["printxml"] == "true";

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
