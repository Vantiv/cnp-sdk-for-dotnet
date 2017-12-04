using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Security.Cryptography;

namespace Cnp.Sdk
{
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

            config["url"] = Properties.Settings.Default.url;
            config["reportGroup"] = Properties.Settings.Default.reportGroup;
            config["username"] = Properties.Settings.Default.username;
            config["printxml"] = Properties.Settings.Default.printxml;
            config["timeout"] = Properties.Settings.Default.timeout;
            config["proxyHost"] = Properties.Settings.Default.proxyHost;
            config["merchantId"] = Properties.Settings.Default.merchantId;
            config["password"] = Properties.Settings.Default.password;
            config["proxyPort"] = Properties.Settings.Default.proxyPort;
            config["sftpUrl"] =  "10.2.32.90";//Properties.Settings.Default.sftpUrl;
            config["sftpUsername"] = Properties.Settings.Default.sftpUsername;
            config["sftpPassword"] = Properties.Settings.Default.sftpPassword;
            config["knownHostsFile"] = Properties.Settings.Default.knownHostsFile;
            config["onlineBatchUrl"] = Properties.Settings.Default.onlineBatchUrl;
            config["onlineBatchPort"] = Properties.Settings.Default.onlineBatchPort;
            config["requestDirectory"] = Properties.Settings.Default.requestDirectory;
            config["responseDirectory"] = Properties.Settings.Default.responseDirectory;

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
         * knownHostsFile
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

        private void initializeRequest()
        {
            communication = new Communications();

            authentication = new authentication();
            authentication.user = config["username"];
            authentication.password = config["password"];

            requestDirectory = config["requestDirectory"] + "\\Requests\\";
            responseDirectory = config["responseDirectory"] + "\\Responses\\";

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

        public void addBatch(batchRequest cnpBatchRequest)
        {
            if (numOfRFRRequest != 0)
            {
                throw new CnpOnlineException("Can not add a batch request to a batch with an RFRrequest!");
            }

            fillInReportGroup(cnpBatchRequest);

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
            var requestFilePath = this.Serialize();

            communication.FtpDropOff(requestDirectory, Path.GetFileName(requestFilePath), config);
            return Path.GetFileName(requestFilePath);
        }


        public void blockAndWaitForResponse(string fileName, int timeOut)
        {
            communication.FtpPoll(fileName, timeOut, config);
        }

        public cnpResponse receiveFromCnp(string batchFileName)
        {
            cnpFile.createDirectory(responseDirectory);

            communication.FtpPickUp(responseDirectory + batchFileName, config, batchFileName);

            var cnpResponse = (cnpResponse)cnpXmlSerializer.DeserializeObjectFromFile(responseDirectory + batchFileName);
            return cnpResponse;
        }

        public string SerializeBatchRequestToFile(batchRequest cnpBatchRequest, string filePath)
        {

            filePath = cnpFile.createRandomFile(requestDirectory, Path.GetFileName(filePath), "_temp_cnpRequest.xml", cnpTime);
            var tempFilePath = cnpBatchRequest.Serialize();

            cnpFile.AppendFileToFile(filePath, tempFilePath);

            return filePath;
        }

        public string SerializeRFRRequestToFile(RFRRequest rfrRequest, string filePath)
        {
            filePath = cnpFile.createRandomFile(requestDirectory, Path.GetFileName(filePath), "_temp_cnpRequest.xml", cnpTime);
            var tempFilePath = rfrRequest.Serialize();

            cnpFile.AppendFileToFile(filePath, tempFilePath);

            return filePath;
        }

        public string Serialize()
        {
            var xmlHeader = "<?xml version='1.0' encoding='utf-8'?>\r\n<cnpRequest version=\"12.0\"" +
             " xmlns=\"http://www.vantivcnp.com/schema\" " +
             "numBatchRequests=\"" + numOfCnpBatchRequest + "\">";

            var xmlFooter = "\r\n</cnpRequest>";

            finalFilePath = cnpFile.createRandomFile(requestDirectory, Path.GetFileName(finalFilePath), ".xml", cnpTime);
            var filePath = finalFilePath;

            cnpFile.AppendLineToFile(finalFilePath, xmlHeader);
            cnpFile.AppendLineToFile(finalFilePath, authentication.Serialize());

            if (batchFilePath != null)
            {
                cnpFile.AppendFileToFile(finalFilePath, batchFilePath);
            }
            else
            {
                throw new CnpOnlineException("No batch was added to the CnpBatch!");
            }

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
