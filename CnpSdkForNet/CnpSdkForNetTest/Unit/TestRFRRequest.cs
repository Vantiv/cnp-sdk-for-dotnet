using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Cnp.Sdk;
using Moq;
using System.Text.RegularExpressions;

namespace Cnp.Sdk.Test.Unit
{
    public class TestRFRRequest
    {
        private RFRRequest rfrRequest = new RFRRequest();

        private const string timeFormat = "MM-dd-yyyy_HH-mm-ss-ffff_";
        private const string timeRegex = "[0-1][0-9]-[0-3][0-9]-[0-9]{4}_[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{4}_";
        private const string batchNameRegex = timeRegex + "[A-Z]{8}";
        private const string mockFileName = "TheRainbow.xml";
        private const string mockFilePath = "C:\\Somewhere\\\\Over\\\\" + mockFileName;

        private Mock<cnpFile> mockCnpFile = new Mock<cnpFile>();
        private Mock<cnpTime> mockCnpTime = new Mock<cnpTime>();

        public TestRFRRequest()
        {
            mockCnpFile.Setup(cnpFile => cnpFile.createRandomFile(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(), mockCnpTime.Object)).Returns(mockFilePath);
            mockCnpFile.Setup(cnpFile => cnpFile.AppendLineToFile(mockFilePath, It.IsAny<String>())).Returns(mockFilePath);
        }

        [Fact]
        public void TestInitialization()
        {
            Dictionary<String, String> mockConfig = new Dictionary<string, string>();

            mockConfig["url"] = "https://www.mockurl.com";
            mockConfig["reportGroup"] = "Mock Report Group";
            mockConfig["username"] = "mockUser";
            mockConfig["printxml"] = "false";
            mockConfig["timeout"] = "35";
            mockConfig["proxyHost"] = "www.mockproxy.com";
            mockConfig["merchantId"] = "MOCKID";
            mockConfig["password"] = "mockPassword";
            mockConfig["proxyPort"] = "3000";
            mockConfig["sftpUrl"] = "www.mockftp.com";
            mockConfig["sftpUsername"] = "mockFtpUser";
            mockConfig["sftpPassword"] = "mockFtpPassword";
            mockConfig["knownHostsFile"] = "C:\\MockKnownHostsFile";
            mockConfig["onlineBatchUrl"] = "www.mockbatch.com";
            mockConfig["onlineBatchPort"] = "4000";
            mockConfig["requestDirectory"] = "C:\\MockRequests";
            mockConfig["responseDirectory"] = "C:\\MockResponses";

            rfrRequest = new RFRRequest(mockConfig);

            Assert.Equal("C:\\MockRequests\\Requests\\", rfrRequest.getRequestDirectory());
            Assert.Equal("C:\\MockResponses\\Responses\\", rfrRequest.getResponseDirectory());

            Assert.NotNull(rfrRequest.getCnpTime());
            Assert.NotNull(rfrRequest.getCnpFile());
        }

        [Fact]
        public void TestSerialize()
        {
            cnpFile mockedCnpFile = mockCnpFile.Object;
            cnpTime mockedCnpTime = mockCnpTime.Object;

            rfrRequest.cnpSessionId = 123456789;
            rfrRequest.setCnpFile(mockedCnpFile);
            rfrRequest.setCnpTime(mockedCnpTime);

            Assert.Equal(mockFilePath, rfrRequest.Serialize());

            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, "\r\n<RFRRequest xmlns=\"http://www.vantivcnp.com/schema\">"));
            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, "\r\n<cnpSessionId>123456789</cnpSessionId>"));
            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, "\r\n</RFRRequest>"));
        }

        [Fact]
        public void TestAccountUpdateFileRequestData() 
        {
            Dictionary<String, String> mockConfig = new Dictionary<string, string>();

            mockConfig["url"] = "https://www.mockurl.com";
            mockConfig["reportGroup"] = "Mock Report Group";
            mockConfig["username"] = "mockUser";
            mockConfig["printxml"] = "false";
            mockConfig["timeout"] = "35";
            mockConfig["proxyHost"] = "www.mockproxy.com";
            mockConfig["merchantId"] = "MOCKID";
            mockConfig["password"] = "mockPassword";
            mockConfig["proxyPort"] = "3000";
            mockConfig["sftpUrl"] = "www.mockftp.com";
            mockConfig["sftpUsername"] = "mockFtpUser";
            mockConfig["sftpPassword"] = "mockFtpPassword";
            mockConfig["knownHostsFile"] = "C:\\MockKnownHostsFile";
            mockConfig["onlineBatchUrl"] = "www.mockbatch.com";
            mockConfig["onlineBatchPort"] = "4000";
            mockConfig["requestDirectory"] = "C:\\MockRequests";
            mockConfig["responseDirectory"] = "C:\\MockResponses";

            accountUpdateFileRequestData accountUpdateFileRequest = new accountUpdateFileRequestData(mockConfig);
            accountUpdateFileRequestData accountUpdateFileRequestDefault = new accountUpdateFileRequestData();

            Assert.Equal(accountUpdateFileRequestDefault.merchantId, Properties.Settings.Default.merchantId);
            Assert.Equal(accountUpdateFileRequest.merchantId, mockConfig["merchantId"]);
        }
    }
}
