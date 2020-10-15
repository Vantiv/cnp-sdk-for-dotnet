using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace Cnp.Sdk
{
    // Represent cnpRequest, which contains multiple batches.
    public class cnpRequest
    {
        private authentication authentication;
        private Dictionary<string, string> config;
        private Communications communication;
        private cnpXmlSerializer cnpXmlSerializer;
        private int numOfCnpBatchRequest = 0;
        private int numOfRFRRequest = 0;
        public string finalFilePath = null;
        private string batchFilePath = null;
        private string requestDirectory;
        private string responseDirectory;
        private cnpTime cnpTime;
        private cnpFile cnpFile;

        /**
         * Construct a Cnp online using the configuration specified in CnpSdkForNet.dll.config
         */
        public cnpRequest()
        {
            config = new Dictionary<string, string>();
            ConfigManager configManager = new ConfigManager();
            config = configManager.getConfig();
            // Retrieve all the settings.
            //config["url"] = Properties.Settings.Default.url;
            //config["reportGroup"] = Properties.Settings.Default.reportGroup;
            //config["username"] = Properties.Settings.Default.username;
            //config["printxml"] = Properties.Settings.Default.printxml;
            //config["timeout"] = Properties.Settings.Default.timeout;
            //config["proxyHost"] = Properties.Settings.Default.proxyHost;
            //config["merchantId"] = Properties.Settings.Default.merchantId;
            //config["password"] = Properties.Settings.Default.password;
            //config["proxyPort"] = Properties.Settings.Default.proxyPort;
            //config["sftpUrl"] =  Properties.Settings.Default.sftpUrl;
            //config["sftpUsername"] = Properties.Settings.Default.sftpUsername;
            //config["sftpPassword"] = Properties.Settings.Default.sftpPassword;
            //config["onlineBatchUrl"] = Properties.Settings.Default.onlineBatchUrl;
            //config["onlineBatchPort"] = Properties.Settings.Default.onlineBatchPort;
            //config["requestDirectory"] = Properties.Settings.Default.requestDirectory;
            //config["responseDirectory"] = Properties.Settings.Default.responseDirectory;
            //config["useEncryption"] = Properties.Settings.Default.useEncryption;
            //config["vantivPublicKeyId"] = Properties.Settings.Default.vantivPublicKeyId;
            //config["pgpPassphrase"] = Properties.Settings.Default.pgpPassphrase;

            initializeRequest();
        }

        /**
         * Construct a CnpOnline specifying the configuration in code.  This should be used by integration that have another way
         * to specify their configuration settings or where different configurations are needed for different instances of CnpOnline.
         * 
         * Properties that *must* be set are:
         * url (eg https://payments.cnp.com/vap/communicator/online)
         * reportGroup (eg "Default Report Group")
         * username
         * merchantId
         * password
         * timeout (in seconds)
         * Optional properties are:
         * proxyHost
         * proxyPort
         * printxml (possible values "true" and "false" - defaults to false)
         * sftpUrl
         * sftpUsername
         * sftpPassword
         * onlineBatchUrl
         * onlineBatchPort
         * requestDirectory
         * responseDirectory
         */
        public cnpRequest(Dictionary<string, string> config)
        {
            this.config = config;
            initializeRequest();
        }

        // 
        private void initializeRequest()
        {
            communication = new Communications(config);

            authentication = new authentication();
            authentication.user = config["username"];
            authentication.password = config["password"];

            requestDirectory = Path.Combine(config["requestDirectory"],"Requests") + Path.DirectorySeparatorChar;
            responseDirectory = Path.Combine(config["responseDirectory"],"Responses") + Path.DirectorySeparatorChar;

            cnpXmlSerializer = new cnpXmlSerializer();
            cnpTime = new cnpTime();
            cnpFile = new cnpFile();
        }

        public authentication getAuthenication()
        {
            return this.authentication;
        }

        public string getRequestDirectory()
        {
            return this.requestDirectory;
        }

        public string getResponseDirectory()
        {
            return this.responseDirectory;
        }

        public void setCommunication(Communications communication)
        {
            this.communication = communication;
        }

        public Communications getCommunication()
        {
            return this.communication;
        }

        public void setCnpXmlSerializer(cnpXmlSerializer cnpXmlSerializer)
        {
            this.cnpXmlSerializer = cnpXmlSerializer;
        }

        public cnpXmlSerializer getCnpXmlSerializer()
        {
            return this.cnpXmlSerializer;
        }

        public void setCnpTime(cnpTime cnpTime)
        {
            this.cnpTime = cnpTime;
        }

        public cnpTime getCnpTime()
        {
            return this.cnpTime;
        }

        public void setCnpFile(cnpFile cnpFile)
        {
            this.cnpFile = cnpFile;
        }

        public cnpFile getCnpFile()
        {
            return this.cnpFile;
        }

        // Add a single batch to batch request.
        public void addBatch(batchRequest cnpBatchRequest)
        {
            if (numOfRFRRequest != 0)
            {
                throw new CnpOnlineException("Can not add a batch request to a batch with an RFRrequest!");
            }
            // Fill in report group attribute for cnpRequest xml element.
            fillInReportGroup(cnpBatchRequest);
            // Add batchRequest xml element into cnpRequest xml element.
            batchFilePath = SerializeBatchRequestToFile(cnpBatchRequest, batchFilePath);
            numOfCnpBatchRequest++;
        }

        public void addRFRRequest(RFRRequest rfrRequest)
        {
            if (numOfCnpBatchRequest != 0)
            {
                throw new CnpOnlineException("Can not add an RFRRequest to a batch with requests!");
            }
            else if (numOfRFRRequest >= 1)
            {
                throw new CnpOnlineException("Can not add more than one RFRRequest to a batch!");
            }

            batchFilePath = SerializeRFRRequestToFile(rfrRequest, batchFilePath);
            numOfRFRRequest++;
        }

        //public cnpResponse sendToCnpWithStream()
        //{
        //    var requestFilePath = this.Serialize();
        //    var batchName = Path.GetFileName(requestFilePath);

        //    var responseFilePath = communication.SocketStream(requestFilePath, responseDirectory, config);

        //    var cnpResponse = (cnpResponse)cnpXmlSerializer.DeserializeObjectFromFile(responseFilePath);
        //    return cnpResponse;
        //}

        public string sendToCnp()
        {
            var useEncryption =  config.ContainsKey("useEncryption")? config["useEncryption"] : "false";
            var vantivPublicKeyId = config.ContainsKey("vantivPublicKeyId")? config["vantivPublicKeyId"] : "";
            
            var requestFilePath = this.Serialize();
            var batchRequestDir = requestDirectory;
            var finalRequestFilePath = requestFilePath;
            if ("true".Equals(useEncryption))
            {
                batchRequestDir = Path.Combine(requestDirectory, "encrypted");
                Console.WriteLine(batchRequestDir);
                finalRequestFilePath =
                    Path.Combine(batchRequestDir, Path.GetFileName(requestFilePath) + ".encrypted");
                cnpFile.createDirectory(finalRequestFilePath);
                PgpHelper.EncryptFile(requestFilePath, finalRequestFilePath, vantivPublicKeyId);
            }
            
            communication.FtpDropOff(batchRequestDir, Path.GetFileName(finalRequestFilePath));
            
            return Path.GetFileName(finalRequestFilePath);
        }


        public void blockAndWaitForResponse(string fileName, int timeOut)
        {
            communication.FtpPoll(fileName, timeOut, config);
        }

        public cnpResponse receiveFromCnp(string batchFileName)
        {
            var useEncryption =  config.ContainsKey("useEncryption")? config["useEncryption"] : "false";
            var pgpPassphrase = config.ContainsKey("pgpPassphrase")? config["pgpPassphrase"] : "";
            
            cnpFile.createDirectory(responseDirectory);
            
            var responseFilePath = Path.Combine(responseDirectory, batchFileName);
            var batchResponseDir = responseDirectory;
            var finalResponseFilePath = responseFilePath;

            if ("true".Equals(useEncryption))
            {
                batchResponseDir = Path.Combine(responseDirectory, "encrypted");
                finalResponseFilePath =
                    Path.Combine(batchResponseDir, batchFileName);
                cnpFile.createDirectory(finalResponseFilePath);
            }
            communication.FtpPickUp(finalResponseFilePath, batchFileName);

            if ("true".Equals(useEncryption))
            {
                responseFilePath = responseFilePath.Replace(".encrypted", "");
                PgpHelper.DecryptFile(finalResponseFilePath, responseFilePath, pgpPassphrase);
            }

            var cnpResponse = (cnpResponse)cnpXmlSerializer.DeserializeObjectFromFile(responseFilePath);
                        
            return cnpResponse;
        }

        // Serialize the batch into temp xml file, and return the path to it.
        public string SerializeBatchRequestToFile(batchRequest cnpBatchRequest, string filePath)
        {
            // Create cnpRequest xml file if not exist.
            // Otherwise, the xml file created, thus storing some batch requests.
            filePath = cnpFile.createRandomFile(requestDirectory, Path.GetFileName(filePath), "_temp_cnpRequest.xml", cnpTime);
            // Serializing the batchRequest creates an xml for that batch request and returns the path to it.
            var tempFilePath = cnpBatchRequest.Serialize();
            // Append the batch request xml just created to the accummulating cnpRequest xml file.
            cnpFile.AppendFileToFile(filePath, tempFilePath);
            // Return the path to temp xml file.
            return filePath;
        }

        public string SerializeRFRRequestToFile(RFRRequest rfrRequest, string filePath)
        {
            filePath = cnpFile.createRandomFile(requestDirectory, Path.GetFileName(filePath), "_temp_cnpRequest.xml", cnpTime);
            var tempFilePath = rfrRequest.Serialize();

            cnpFile.AppendFileToFile(filePath, tempFilePath);

            return filePath;
        }

        // Convert all batch objects into xml and place them in cnpRequest, then build the Session file.
        public string Serialize()
        {
            var xmlHeader = "<?xml version='1.0' encoding='utf-8'?>\r\n<cnpRequest version=\"" + CnpVersion.CurrentCNPXMLVersion + "\"" +
             " xmlns=\"http://www.vantivcnp.com/schema\" " +
             "numBatchRequests=\"" + numOfCnpBatchRequest + "\">";

            var xmlFooter = "\r\n</cnpRequest>";

            // Create the Session file.
            finalFilePath = cnpFile.createRandomFile(requestDirectory, Path.GetFileName(finalFilePath), ".xml", cnpTime);
            var filePath = finalFilePath;

            // Add the header into the Session file.
            cnpFile.AppendLineToFile(finalFilePath, xmlHeader);
            // Add authentication.
            cnpFile.AppendLineToFile(finalFilePath, authentication.Serialize());

            // batchFilePath is not null when some batch is added into the batch request.
            if (batchFilePath != null)
            {
                cnpFile.AppendFileToFile(finalFilePath, batchFilePath);
            }
            else
            {
                throw new CnpOnlineException("No batch was added to the CnpBatch!");
            }
            
            // Add the footer into Session file
            cnpFile.AppendLineToFile(finalFilePath, xmlFooter);

            finalFilePath = null;

            return filePath;
        }

        private void fillInReportGroup(batchRequest cnpBatchRequest)
        {
            if (cnpBatchRequest.reportGroup == null)
            {
                cnpBatchRequest.reportGroup = config["reportGroup"];
            }
        }

    }

    public class cnpFile
    {
        // Create a file with name and timestamp if not exists.
        public virtual string createRandomFile(string fileDirectory, string fileName, string fileExtension, cnpTime cnpTime)
        {
            string filePath = null;
            if (string.IsNullOrEmpty(fileName))
            {
                if (!Directory.Exists(fileDirectory))
                {
                    Directory.CreateDirectory(fileDirectory);
                }

                fileName = cnpTime.getCurrentTime("MM-dd-yyyy_HH-mm-ss-ffff_") + RandomGen.NextString(8);
                filePath = fileDirectory + fileName + fileExtension;

                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                }
            }
            else
            {
                filePath = fileDirectory + fileName;
            }

            return filePath;
        }

        public virtual string AppendLineToFile(string filePath, string lineToAppend)
        {
            using (var fs = new FileStream(filePath, FileMode.Append))
            using (var sw = new StreamWriter(fs))
            {
                sw.Write(lineToAppend);
            }

            return filePath;
        }


        public virtual string AppendFileToFile(string filePathToAppendTo, string filePathToAppend)
        {

            using (var fs = new FileStream(filePathToAppendTo, FileMode.Append))
            using (var fsr = new FileStream(filePathToAppend, FileMode.Open))
            {
                var buffer = new byte[16];

                var bytesRead = 0;

                do
                {
                    bytesRead = fsr.Read(buffer, 0, buffer.Length);
                    fs.Write(buffer, 0, bytesRead);
                }
                while (bytesRead > 0);
            }

            File.Delete(filePathToAppend);

            return filePathToAppendTo;
        }

        public virtual void createDirectory(string destinationFilePath)
        {
            var destinationDirectory = Path.GetDirectoryName(destinationFilePath);

            if (!Directory.Exists(destinationDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);
            }
        }

    }

    public static class RandomGen
    {
        private static RNGCryptoServiceProvider _global = new RNGCryptoServiceProvider();
        private static Random _local;
        public static int NextInt()
        {
            var inst = _local;
            if (inst == null)
            {
                var buffer = new byte[8];
                _global.GetBytes(buffer);
                _local = inst = new Random(BitConverter.ToInt32(buffer, 0));
            }

            return _local.Next();
        }

        public static string NextString(int length)
        {
            var result = "";

            for (var i = 0; i < length; i++)
            {
                result += Convert.ToChar(NextInt() % ('Z' - 'A') + 'A');
            }

            return result;
        }
    }

    public class cnpTime
    {
        public virtual string getCurrentTime(string format)
        {
            return DateTime.Now.ToString(format);
        }
    }

}
