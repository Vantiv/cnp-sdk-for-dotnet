using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Moq;
using System.Text.RegularExpressions;
using System.Xml;


namespace Cnp.Sdk.Test.Unit
{
    [TestFixture]
    class TestBatch
    {
        private cnpRequest cnp;
        private const string timeFormat = "MM-dd-yyyy_HH-mm-ss-ffff_";
        private const string timeRegex = "[0-1][0-9]-[0-3][0-9]-[0-9]{4}_[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{4}_";
        private const string batchNameRegex = timeRegex + "[A-Z]{8}";
        private const string mockFileName = "TheRainbow.xml";
        private static readonly string tempDirectroyPath = (Path.GetTempPath() + "NET" + CnpVersion.CurrentCNPXMLVersion + "/").Replace("\\","/");
        private static readonly string mockFilePath = tempDirectroyPath + mockFileName;

        private Mock<cnpTime> mockCnpTime;
        private Mock<cnpFile> mockCnpFile;
        private Mock<Communications> mockCommunications;
        private Mock<XmlReader> mockXmlReader;

        [OneTimeSetUp]
        public void SetUp()
        {
            mockCnpTime = new Mock<cnpTime>();
            mockCnpTime.Setup(cnpTime => cnpTime.getCurrentTime(It.Is<String>(resultFormat => resultFormat == timeFormat))).Returns("01-01-1960_01-22-30-1234_");

            mockCnpFile = new Mock<cnpFile>();
            mockCnpFile.Setup(cnpFile => cnpFile.createDirectory(It.IsAny<String>()));
            mockCnpFile.Setup(cnpFile => cnpFile.createRandomFile(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(), mockCnpTime.Object)).Returns(mockFilePath);
            mockCnpFile.Setup(cnpFile => cnpFile.AppendFileToFile(mockFilePath, It.IsAny<String>())).Returns(mockFilePath);
            mockCnpFile.Setup(cnpFile => cnpFile.AppendLineToFile(mockFilePath, It.IsAny<String>())).Returns(mockFilePath);

            mockCommunications = new Mock<Communications>();
        }

        [SetUp]
        public void SetUpBeforeEachTest()
        {
            Dictionary<String,String> config = new ConfigManager().getConfig();
            config["requestDirectory"] = tempDirectroyPath + "MockRequests";
            config["responseDirectory"] = tempDirectroyPath + "MockResponses";
            cnp = new cnpRequest(config);

            mockXmlReader = new Mock<XmlReader>();
            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadToFollowing(It.IsAny<String>())).Returns(true).Returns(true).Returns(false);
            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadState).Returns(ReadState.Initial).Returns(ReadState.Interactive).Returns(ReadState.Closed);
        }

        [Test]
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
            mockConfig["onlineBatchUrl"] = "www.mockbatch.com";
            mockConfig["onlineBatchPort"] = "4000";
            mockConfig["requestDirectory"] = tempDirectroyPath + "MockRequests";
            mockConfig["responseDirectory"] = tempDirectroyPath + "MockResponses";

            cnp = new cnpRequest(mockConfig);

            Assert.AreEqual(Path.Combine(tempDirectroyPath,"MockRequests","Requests") + Path.DirectorySeparatorChar, cnp.getRequestDirectory());
            Assert.AreEqual(Path.Combine(tempDirectroyPath,"MockResponses","Responses") + Path.DirectorySeparatorChar, cnp.getResponseDirectory());

            Assert.NotNull(cnp.getCommunication());
            Assert.NotNull(cnp.getCnpTime());
            Assert.NotNull(cnp.getCnpFile());
            Assert.NotNull(cnp.getCnpXmlSerializer());
        }

        [Test]
        public void TestAccountUpdate()
        {
            accountUpdate accountUpdate = new accountUpdate();
            accountUpdate.reportGroup = "Planets";
            accountUpdate.orderId = "12344";
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000002";
            card.expDate = "1210";
            accountUpdate.card = card;

            var mockCnpResponse = new Mock<cnpResponse>();
            var mockCnpXmlSerializer = new Mock<cnpXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<accountUpdateResponse reportGroup=\"Merch01ReportGrp\" xmlns=\"http://www.vantivcnp.com/schema\"><cnpTxnId>123</cnpTxnId><orderId>MERCH01-0002</orderId><response>000</response><responseTime>2010-04-11T15:44:26</responseTime><message>Approved</message></accountUpdateResponse>")
                .Returns("<accountUpdateResponse reportGroup=\"Merch01ReportGrp\" xmlns=\"http://www.vantivcnp.com/schema\"><cnpTxnId>124</cnpTxnId><orderId>MERCH01-0002</orderId><response>000</response><responseTime>2010-04-11T15:44:26</responseTime><message>Approved</message></accountUpdateResponse>");

            batchResponse mockCnpBatchResponse = new batchResponse();
            mockCnpBatchResponse.setAccountUpdateResponseReader(mockXmlReader.Object);

            mockCnpResponse.Setup(cnpResponse => cnpResponse.nextBatchResponse()).Returns(mockCnpBatchResponse);
            cnpResponse mockedCnpResponse = mockCnpResponse.Object;

            mockCnpXmlSerializer.Setup(cnpXmlSerializer => cnpXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedCnpResponse);

            Communications mockedCommunication = mockCommunications.Object;
            cnp.setCommunication(mockedCommunication);

            cnpXmlSerializer mockedCnpXmlSerializer = mockCnpXmlSerializer.Object;
            cnp.setCnpXmlSerializer(mockedCnpXmlSerializer);

            cnpFile mockedCnpFile = mockCnpFile.Object;
            cnp.setCnpFile(mockedCnpFile);

            cnp.setCnpTime(mockCnpTime.Object);

            batchRequest cnpBatchRequest = new batchRequest();
            cnpBatchRequest.setCnpFile(mockedCnpFile);
            cnpBatchRequest.setCnpTime(mockCnpTime.Object);
            cnpBatchRequest.addAccountUpdate(accountUpdate);
            cnpBatchRequest.addAccountUpdate(accountUpdate);
            cnp.addBatch(cnpBatchRequest);

            string batchFileName = cnp.sendToCnp();
            cnpResponse actualCnpResponse = cnp.receiveFromCnp(batchFileName);
            batchResponse actualCnpBatchResponse = actualCnpResponse.nextBatchResponse();
            accountUpdateResponse actualAccountUpdateResponse1 = actualCnpBatchResponse.nextAccountUpdateResponse();
            accountUpdateResponse actualAccountUpdateResponse2 = actualCnpBatchResponse.nextAccountUpdateResponse();
            accountUpdateResponse nullAccountUpdateResponse = actualCnpBatchResponse.nextAccountUpdateResponse();

            Assert.AreEqual(123, actualAccountUpdateResponse1.cnpTxnId);
            Assert.AreEqual("000", actualAccountUpdateResponse1.response);
            Assert.AreEqual(124, actualAccountUpdateResponse2.cnpTxnId);
            Assert.AreEqual("000", actualAccountUpdateResponse2.response);
            Assert.IsNull(nullAccountUpdateResponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName  ));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>()  , mockFileName));
        }


        [Test]
        public void TestAuth()
        {
            authorization authorization = new authorization();
            authorization.reportGroup = "Planets";
            authorization.orderId = "12344";
            authorization.amount = 106;
            authorization.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000002";
            card.expDate = "1210";
            authorization.card = card;

            var mockCnpResponse = new Mock<cnpResponse>();
            var mockCnpXmlSerializer = new Mock<cnpXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<authorizationResponse id=\"\" reportGroup=\"Planets\" xmlns=\"http://www.vantivcnp.com/schema\"><cnpTxnId>123</cnpTxnId><orderId>123</orderId><response>000</response><responseTime>2013-06-19T19:54:42</responseTime><message>Approved</message><authCode>123457</authCode><fraudResult><avsResult>00</avsResult></fraudResult><tokenResponse><cnpToken>1711000103054242</cnpToken><tokenResponseCode>802</tokenResponseCode><tokenMessage>Account number was previously registered</tokenMessage><type>VI</type><bin>424242</bin></tokenResponse></authorizationResponse>")
                .Returns("<authorizationResponse id=\"\" reportGroup=\"Planets\" xmlns=\"http://www.vantivcnp.com/schema\"><cnpTxnId>124</cnpTxnId><orderId>124</orderId><response>000</response><responseTime>2013-06-19T19:54:42</responseTime><message>Approved</message><authCode>123457</authCode><fraudResult><avsResult>00</avsResult></fraudResult><tokenResponse><cnpToken>1711000103054242</cnpToken><tokenResponseCode>802</tokenResponseCode><tokenMessage>Account number was previously registered</tokenMessage><type>VI</type><bin>424242</bin></tokenResponse></authorizationResponse>");

            batchResponse mockCnpBatchResponse = new batchResponse();
            mockCnpBatchResponse.setAuthorizationResponseReader(mockXmlReader.Object);

            mockCnpResponse.Setup(cnpResponse => cnpResponse.nextBatchResponse()).Returns(mockCnpBatchResponse);
            cnpResponse mockedCnpResponse = mockCnpResponse.Object;

            mockCnpXmlSerializer.Setup(cnpXmlSerializer => cnpXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedCnpResponse);

            Communications mockedCommunication = mockCommunications.Object;
            cnp.setCommunication(mockedCommunication);

            cnpXmlSerializer mockedCnpXmlSerializer = mockCnpXmlSerializer.Object;
            cnp.setCnpXmlSerializer(mockedCnpXmlSerializer);

            cnpFile mockedCnpFile = mockCnpFile.Object;
            cnp.setCnpFile(mockedCnpFile);

            cnp.setCnpTime(mockCnpTime.Object);

            batchRequest cnpBatchRequest = new batchRequest();
            cnpBatchRequest.setCnpFile(mockedCnpFile);
            cnpBatchRequest.setCnpTime(mockCnpTime.Object);
            cnpBatchRequest.addAuthorization(authorization);
            cnpBatchRequest.addAuthorization(authorization);
            cnp.addBatch(cnpBatchRequest);

            string batchFileName = cnp.sendToCnp();
            cnpResponse actualCnpResponse = cnp.receiveFromCnp(batchFileName);
            batchResponse actualCnpBatchResponse = actualCnpResponse.nextBatchResponse();

            Assert.AreSame(mockCnpBatchResponse, actualCnpBatchResponse);
            Assert.AreEqual(123, actualCnpBatchResponse.nextAuthorizationResponse().cnpTxnId);
            Assert.AreEqual(124, actualCnpBatchResponse.nextAuthorizationResponse().cnpTxnId);
            Assert.IsNull(actualCnpBatchResponse.nextAuthorizationResponse());

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName  ));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>()  , mockFileName));
        }

        [Test]
        public void TestAuthReversal()
        {
            authReversal authreversal = new authReversal();
            authreversal.cnpTxnId = 12345678000;
            authreversal.amount = 106;
            authreversal.payPalNotes = "Notes";

            var mockCnpResponse = new Mock<cnpResponse>();
            var mockCnpXmlSerializer = new Mock<cnpXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<authReversalResponse id=\"123\" customerId=\"Customer Id\" reportGroup=\"Auth Reversals\" xmlns=\"http://www.vantivcnp.com/schema\"><cnpTxnId>123</cnpTxnId><orderId>abc123</orderId><response>000</response><responseTime>2011-08-30T13:15:43</responseTime><message>Approved</message></authReversalResponse>")
                .Returns("<authReversalResponse id=\"123\" customerId=\"Customer Id\" reportGroup=\"Auth Reversals\" xmlns=\"http://www.vantivcnp.com/schema\"><cnpTxnId>124</cnpTxnId><orderId>abc123</orderId><response>000</response><responseTime>2011-08-30T13:15:43</responseTime><message>Approved</message></authReversalResponse>");

            batchResponse mockCnpBatchResponse = new batchResponse();
            mockCnpBatchResponse.setAuthReversalResponseReader(mockXmlReader.Object);

            mockCnpResponse.Setup(cnpResponse => cnpResponse.nextBatchResponse()).Returns(mockCnpBatchResponse);
            cnpResponse mockedCnpResponse = mockCnpResponse.Object;

            mockCnpXmlSerializer.Setup(cnpXmlSerializer => cnpXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedCnpResponse);

            Communications mockedCommunications = mockCommunications.Object;
            cnp.setCommunication(mockedCommunications);

            cnpXmlSerializer mockedCnpXmlSerializer = mockCnpXmlSerializer.Object;
            cnp.setCnpXmlSerializer(mockedCnpXmlSerializer);

            cnpFile mockedCnpFile = mockCnpFile.Object;
            cnp.setCnpFile(mockedCnpFile);

            cnp.setCnpTime(mockCnpTime.Object);

            batchRequest cnpBatchRequest = new batchRequest();
            cnpBatchRequest.setCnpFile(mockedCnpFile);
            cnpBatchRequest.setCnpTime(mockCnpTime.Object);
            cnpBatchRequest.addAuthReversal(authreversal);
            cnpBatchRequest.addAuthReversal(authreversal);
            cnp.addBatch(cnpBatchRequest);

            string batchFileName = cnp.sendToCnp();

            cnpResponse actualCnpResponse = cnp.receiveFromCnp(batchFileName);
            batchResponse actualCnpBatchResponse = actualCnpResponse.nextBatchResponse();
            authReversalResponse actualAuthReversalResponse1 = actualCnpBatchResponse.nextAuthReversalResponse();
            authReversalResponse actualAuthReversalResponse2 = actualCnpBatchResponse.nextAuthReversalResponse();
            authReversalResponse nullAuthReversalResponse = actualCnpBatchResponse.nextAuthReversalResponse();

            Assert.AreEqual(123, actualAuthReversalResponse1.cnpTxnId);
            Assert.AreEqual(124, actualAuthReversalResponse2.cnpTxnId);
            Assert.IsNull(nullAuthReversalResponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName  ));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>()  , mockFileName));
        }

        [Test]
        public void TestCapture()
        {
            capture capture = new capture();
            capture.cnpTxnId = 12345678000;
            capture.amount = 106;

            var mockCnpResponse = new Mock<cnpResponse>();
            var mockCnpXmlSerializer = new Mock<cnpXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
              .Returns("<captureResponse id=\"123\" reportGroup=\"RG27\" xmlns=\"http://www.vantivcnp.com/schema\"> <cnpTxnId>123</cnpTxnId> <orderId>12z58743y1</orderId> <response>000</response> <responseTime>2011-09-01T10:24:31</responseTime> <message>message</message> </captureResponse>")
              .Returns("<captureResponse id=\"124\" reportGroup=\"RG27\" xmlns=\"http://www.vantivcnp.com/schema\"> <cnpTxnId>124</cnpTxnId> <orderId>12z58743y1</orderId> <response>000</response> <responseTime>2011-09-01T10:24:31</responseTime> <message>message</message> </captureResponse>");

            batchResponse mockedCnpBatchResponse = new batchResponse();
            mockedCnpBatchResponse.setCaptureResponseReader(mockXmlReader.Object);

            mockCnpResponse.Setup(cnpResponse => cnpResponse.nextBatchResponse()).Returns(mockedCnpBatchResponse);
            cnpResponse mockedCnpResponse = mockCnpResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockCnpXmlSerializer.Setup(cnpXmlSerializer => cnpXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedCnpResponse);
            cnpXmlSerializer mockedCnpXmlSerializer = mockCnpXmlSerializer.Object;

            cnpFile mockedCnpFile = mockCnpFile.Object;

            cnp.setCommunication(mockedCommunications);
            cnp.setCnpXmlSerializer(mockedCnpXmlSerializer);
            cnp.setCnpFile(mockedCnpFile);
            cnp.setCnpTime(mockCnpTime.Object);

            batchRequest cnpBatchRequest = new batchRequest();
            cnpBatchRequest.setCnpFile(mockedCnpFile);
            cnpBatchRequest.setCnpTime(mockCnpTime.Object);
            cnpBatchRequest.addCapture(capture);
            cnpBatchRequest.addCapture(capture);
            cnp.addBatch(cnpBatchRequest);

            string batchFileName = cnp.sendToCnp();

            cnpResponse actualCnpResponse = cnp.receiveFromCnp(batchFileName);
            batchResponse actualCnpBatchResponse = actualCnpResponse.nextBatchResponse();
            captureResponse actualCaptureResponse1 = actualCnpBatchResponse.nextCaptureResponse();
            captureResponse actualCaptureResponse2 = actualCnpBatchResponse.nextCaptureResponse();
            captureResponse nullCaptureResponse = actualCnpBatchResponse.nextCaptureResponse();

            Assert.AreEqual(123, actualCaptureResponse1.cnpTxnId);
            Assert.AreEqual(124, actualCaptureResponse2.cnpTxnId);
            Assert.IsNull(nullCaptureResponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName  ));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>()  , mockFileName));
        }

        [Test]
        public void TestCaptureGivenAuth()
        {
            captureGivenAuth capturegivenauth = new captureGivenAuth();
            capturegivenauth.orderId = "12344";
            capturegivenauth.amount = 106;
            authInformation authinfo = new authInformation();
            authinfo.authDate = new DateTime(2002, 10, 9);
            authinfo.authCode = "543216";
            authinfo.authAmount = 12345;
            capturegivenauth.authInformation = authinfo;
            capturegivenauth.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000001";
            card.expDate = "1210";
            capturegivenauth.card = card;

            var mockCnpResponse = new Mock<cnpResponse>();
            var mockCnpXmlSerializer = new Mock<cnpXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<captureGivenAuthResponse xmlns='http://www.vantivcnp.com/schema'><cnpTxnId>123</cnpTxnId></captureGivenAuthResponse>")
                .Returns("<captureGivenAuthResponse xmlns='http://www.vantivcnp.com/schema'><cnpTxnId>124</cnpTxnId></captureGivenAuthResponse>");

            batchResponse mockedCnpBatchResponse = new batchResponse();
            mockedCnpBatchResponse.setCaptureGivenAuthResponseReader(mockXmlReader.Object);

            mockCnpResponse.Setup(cnpResponse => cnpResponse.nextBatchResponse()).Returns(mockedCnpBatchResponse);
            cnpResponse mockedCnpResponse = mockCnpResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockCnpXmlSerializer.Setup(cnpXmlSerializer => cnpXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedCnpResponse);
            cnpXmlSerializer mockedCnpXmlSerializer = mockCnpXmlSerializer.Object;

            cnpFile mockedCnpFile = mockCnpFile.Object;

            cnp.setCommunication(mockedCommunications);
            cnp.setCnpXmlSerializer(mockedCnpXmlSerializer);
            cnp.setCnpFile(mockedCnpFile);
            cnp.setCnpTime(mockCnpTime.Object);

            batchRequest cnpBatchRequest = new batchRequest();
            cnpBatchRequest.setCnpFile(mockedCnpFile);
            cnpBatchRequest.setCnpTime(mockCnpTime.Object);
            cnpBatchRequest.addCaptureGivenAuth(capturegivenauth);
            cnpBatchRequest.addCaptureGivenAuth(capturegivenauth);
            cnp.addBatch(cnpBatchRequest);

            string batchFileName = cnp.sendToCnp();

            cnpResponse actualCnpResponse = cnp.receiveFromCnp(batchFileName);
            batchResponse actualCnpBatchResponse = actualCnpResponse.nextBatchResponse();
            captureGivenAuthResponse actualCaptureGivenAuthReponse1 = actualCnpBatchResponse.nextCaptureGivenAuthResponse();
            captureGivenAuthResponse actualCaptureGivenAuthReponse2 = actualCnpBatchResponse.nextCaptureGivenAuthResponse();
            captureGivenAuthResponse nullCaptureGivenAuthReponse = actualCnpBatchResponse.nextCaptureGivenAuthResponse();

            Assert.AreEqual(123, actualCaptureGivenAuthReponse1.cnpTxnId);
            Assert.AreEqual(124, actualCaptureGivenAuthReponse2.cnpTxnId);
            Assert.IsNull(nullCaptureGivenAuthReponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName  ));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>()  , mockFileName));
        }

        [Test]
        public void TestCredit()
        {
            credit credit = new credit();
            credit.orderId = "12344";
            credit.amount = 106;
            credit.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000001";
            card.expDate = "1210";
            credit.card = card;

            var mockCnpResponse = new Mock<cnpResponse>();
            var mockCnpXmlSerializer = new Mock<cnpXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<creditResponse xmlns='http://www.vantivcnp.com/schema'><cnpTxnId>123</cnpTxnId></creditResponse>")
                .Returns("<creditResponse xmlns='http://www.vantivcnp.com/schema'><cnpTxnId>124</cnpTxnId></creditResponse>");

            batchResponse mockedCnpBatchResponse = new batchResponse();
            mockedCnpBatchResponse.setCreditResponseReader(mockXmlReader.Object);

            mockCnpResponse.Setup(cnpResponse => cnpResponse.nextBatchResponse()).Returns(mockedCnpBatchResponse);
            cnpResponse mockedCnpResponse = mockCnpResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockCnpXmlSerializer.Setup(cnpXmlSerializer => cnpXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedCnpResponse);
            cnpXmlSerializer mockedCnpXmlSerializer = mockCnpXmlSerializer.Object;

            cnpFile mockedCnpFile = mockCnpFile.Object;

            cnp.setCommunication(mockedCommunications);
            cnp.setCnpXmlSerializer(mockedCnpXmlSerializer);
            cnp.setCnpFile(mockedCnpFile);
            cnp.setCnpTime(mockCnpTime.Object);

            batchRequest cnpBatchRequest = new batchRequest();
            cnpBatchRequest.setCnpFile(mockedCnpFile);
            cnpBatchRequest.setCnpTime(mockCnpTime.Object);
            cnpBatchRequest.addCredit(credit);
            cnpBatchRequest.addCredit(credit);
            cnp.addBatch(cnpBatchRequest);

            string batchFileName = cnp.sendToCnp();

            cnpResponse actualCnpResponse = cnp.receiveFromCnp(batchFileName);
            batchResponse actualCnpBatchResponse = actualCnpResponse.nextBatchResponse();
            creditResponse actualCreditReponse1 = actualCnpBatchResponse.nextCreditResponse();
            creditResponse actualCreditReponse2 = actualCnpBatchResponse.nextCreditResponse();
            creditResponse nullCreditReponse1 = actualCnpBatchResponse.nextCreditResponse();

            Assert.AreEqual(123, actualCreditReponse1.cnpTxnId);
            Assert.AreEqual(124, actualCreditReponse2.cnpTxnId);
            Assert.IsNull(nullCreditReponse1);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName  ));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>()  , mockFileName));
        }

        [Test]
        public void TestEcheckCredit()
        {
            echeckCredit echeckcredit = new echeckCredit();
            echeckcredit.amount = 12;
            echeckcredit.cnpTxnId = 123456789101112;

            var mockCnpResponse = new Mock<cnpResponse>();
            var mockCnpXmlSerializer = new Mock<cnpXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<echeckCreditResponse xmlns='http://www.vantivcnp.com/schema'><cnpTxnId>123</cnpTxnId></echeckCreditResponse>")
                .Returns("<echeckCreditResponse xmlns='http://www.vantivcnp.com/schema'><cnpTxnId>124</cnpTxnId></echeckCreditResponse>");

            batchResponse mockedCnpBatchResponse = new batchResponse();
            mockedCnpBatchResponse.setEcheckCreditResponseReader(mockXmlReader.Object);

            mockCnpResponse.Setup(cnpResponse => cnpResponse.nextBatchResponse()).Returns(mockedCnpBatchResponse);
            cnpResponse mockedCnpResponse = mockCnpResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockCnpXmlSerializer.Setup(cnpXmlSerializer => cnpXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedCnpResponse);
            cnpXmlSerializer mockedCnpXmlSerializer = mockCnpXmlSerializer.Object;

            cnpFile mockedCnpFile = mockCnpFile.Object;

            cnp.setCommunication(mockedCommunications);
            cnp.setCnpXmlSerializer(mockedCnpXmlSerializer);
            cnp.setCnpFile(mockedCnpFile);
            cnp.setCnpTime(mockCnpTime.Object);

            batchRequest cnpBatchRequest = new batchRequest();
            cnpBatchRequest.setCnpFile(mockedCnpFile);
            cnpBatchRequest.setCnpTime(mockCnpTime.Object);
            cnpBatchRequest.addEcheckCredit(echeckcredit);
            cnpBatchRequest.addEcheckCredit(echeckcredit);
            cnp.addBatch(cnpBatchRequest);

            string batchFileName = cnp.sendToCnp();

            cnpResponse actualCnpResponse = cnp.receiveFromCnp(batchFileName);
            batchResponse actualCnpBatchResponse = actualCnpResponse.nextBatchResponse();
            echeckCreditResponse actualEcheckCreditResponse1 = actualCnpBatchResponse.nextEcheckCreditResponse();
            echeckCreditResponse actualEcheckCreditResponse2 = actualCnpBatchResponse.nextEcheckCreditResponse();
            echeckCreditResponse nullEcheckCreditResponse = actualCnpBatchResponse.nextEcheckCreditResponse();

            Assert.AreEqual(123, actualEcheckCreditResponse1.cnpTxnId);
            Assert.AreEqual(124, actualEcheckCreditResponse2.cnpTxnId);
            Assert.IsNull(nullEcheckCreditResponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName  ));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>()  , mockFileName));
        }

        [Test]
        public void TestEcheckRedeposit()
        {
            echeckRedeposit echeckredeposit = new echeckRedeposit();
            echeckredeposit.cnpTxnId = 123456;

            var mockCnpResponse = new Mock<cnpResponse>();
            var mockCnpXmlSerializer = new Mock<cnpXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<echeckRedepositResponse xmlns='http://www.vantivcnp.com/schema'><cnpTxnId>123</cnpTxnId></echeckRedepositResponse>")
                .Returns("<echeckRedepositResponse xmlns='http://www.vantivcnp.com/schema'><cnpTxnId>124</cnpTxnId></echeckRedepositResponse>");

            batchResponse mockedCnpBatchResponse = new batchResponse();
            mockedCnpBatchResponse.setEcheckRedepositResponseReader(mockXmlReader.Object);

            mockCnpResponse.Setup(cnpResponse => cnpResponse.nextBatchResponse()).Returns(mockedCnpBatchResponse);
            cnpResponse mockedCnpResponse = mockCnpResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockCnpXmlSerializer.Setup(cnpXmlSerializer => cnpXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedCnpResponse);
            cnpXmlSerializer mockedCnpXmlSerializer = mockCnpXmlSerializer.Object;

            cnpFile mockedCnpFile = mockCnpFile.Object;

            cnp.setCommunication(mockedCommunications);
            cnp.setCnpXmlSerializer(mockedCnpXmlSerializer);
            cnp.setCnpFile(mockedCnpFile);
            cnp.setCnpTime(mockCnpTime.Object);

            batchRequest cnpBatchRequest = new batchRequest();
            cnpBatchRequest.setCnpFile(mockedCnpFile);
            cnpBatchRequest.setCnpTime(mockCnpTime.Object);
            cnpBatchRequest.addEcheckRedeposit(echeckredeposit);
            cnpBatchRequest.addEcheckRedeposit(echeckredeposit);
            cnp.addBatch(cnpBatchRequest);

            string batchFileName = cnp.sendToCnp();

            cnpResponse actualCnpResponse = cnp.receiveFromCnp(batchFileName);
            batchResponse actualCnpBatchResponse = actualCnpResponse.nextBatchResponse();
            echeckRedepositResponse actualEcheckRedepositResponse1 = actualCnpBatchResponse.nextEcheckRedepositResponse();
            echeckRedepositResponse actualEcheckRedepositResponse2 = actualCnpBatchResponse.nextEcheckRedepositResponse();
            echeckRedepositResponse nullEcheckRedepositResponse = actualCnpBatchResponse.nextEcheckRedepositResponse();

            Assert.AreEqual(123, actualEcheckRedepositResponse1.cnpTxnId);
            Assert.AreEqual(124, actualEcheckRedepositResponse2.cnpTxnId);
            Assert.IsNull(nullEcheckRedepositResponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName  ));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>()  , mockFileName));
        }

        [Test]
        public void TestEcheckSale()
        {
            echeckSale echecksale = new echeckSale();
            echecksale.orderId = "12345";
            echecksale.amount = 123456;
            echecksale.orderSource = orderSourceType.ecommerce;
            echeckType echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "12345657890";
            echeck.routingNum = "123456789";
            echeck.checkNum = "123455";
            echecksale.echeck = echeck;
            contact contact = new contact();
            contact.name = "Bob";
            contact.city = "lowell";
            contact.state = "MA";
            contact.email = "cnp.com";
            echecksale.billToAddress = contact;

            var mockCnpResponse = new Mock<cnpResponse>();
            var mockCnpXmlSerializer = new Mock<cnpXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<echeckSalesResponse xmlns='http://www.vantivcnp.com/schema'><cnpTxnId>123</cnpTxnId></echeckSalesResponse>")
                .Returns("<echeckSalesResponse xmlns='http://www.vantivcnp.com/schema'><cnpTxnId>124</cnpTxnId></echeckSalesResponse>");

            batchResponse mockedCnpBatchResponse = new batchResponse();
            mockedCnpBatchResponse.setEcheckSalesResponseReader(mockXmlReader.Object);

            mockCnpResponse.Setup(cnpResponse => cnpResponse.nextBatchResponse()).Returns(mockedCnpBatchResponse);
            cnpResponse mockedCnpResponse = mockCnpResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockCnpXmlSerializer.Setup(cnpXmlSerializer => cnpXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedCnpResponse);
            cnpXmlSerializer mockedCnpXmlSerializer = mockCnpXmlSerializer.Object;

            cnpFile mockedCnpFile = mockCnpFile.Object;

            cnp.setCommunication(mockedCommunications);
            cnp.setCnpXmlSerializer(mockedCnpXmlSerializer);
            cnp.setCnpFile(mockedCnpFile);
            cnp.setCnpTime(mockCnpTime.Object);

            batchRequest cnpBatchRequest = new batchRequest();
            cnpBatchRequest.setCnpFile(mockedCnpFile);
            cnpBatchRequest.setCnpTime(mockCnpTime.Object);
            cnpBatchRequest.addEcheckSale(echecksale);
            cnpBatchRequest.addEcheckSale(echecksale);
            cnp.addBatch(cnpBatchRequest);

            string batchFileName = cnp.sendToCnp();

            cnpResponse actualCnpResponse = cnp.receiveFromCnp(batchFileName);
            batchResponse actualCnpBatchResponse = actualCnpResponse.nextBatchResponse();
            echeckSalesResponse actualEcheckSalesResponse1 = actualCnpBatchResponse.nextEcheckSalesResponse();
            echeckSalesResponse actualEcheckSalesResponse2 = actualCnpBatchResponse.nextEcheckSalesResponse();
            echeckSalesResponse nullEcheckSalesResponse = actualCnpBatchResponse.nextEcheckSalesResponse();

            Assert.AreEqual(123, actualEcheckSalesResponse1.cnpTxnId);
            Assert.AreEqual(124, actualEcheckSalesResponse2.cnpTxnId);
            Assert.IsNull(nullEcheckSalesResponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName  ));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>()  , mockFileName));
        }

        [Test]
        public void TestEcheckVerification()
        {
            echeckVerification echeckverification = new echeckVerification();
            echeckverification.orderId = "12345";
            echeckverification.amount = 123456;
            echeckverification.orderSource = orderSourceType.ecommerce;
            echeckType echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "12345657890";
            echeck.routingNum = "123456789";
            echeck.checkNum = "123455";
            echeckverification.echeck = echeck;
            contact contact = new contact();
            contact.name = "Bob";
            contact.city = "lowell";
            contact.state = "MA";
            contact.email = "cnp.com";
            echeckverification.billToAddress = contact;

            var mockCnpResponse = new Mock<cnpResponse>();
            var mockCnpXmlSerializer = new Mock<cnpXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<echeckVerificationResponse xmlns='http://www.vantivcnp.com/schema'><cnpTxnId>123</cnpTxnId></echeckVerificationResponse>")
                .Returns("<echeckVerificationResponse xmlns='http://www.vantivcnp.com/schema'><cnpTxnId>124</cnpTxnId></echeckVerificationResponse>");

            batchResponse mockedCnpBatchResponse = new batchResponse();
            mockedCnpBatchResponse.setEcheckVerificationResponseReader(mockXmlReader.Object);

            mockCnpResponse.Setup(cnpResponse => cnpResponse.nextBatchResponse()).Returns(mockedCnpBatchResponse);
            cnpResponse mockedCnpResponse = mockCnpResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockCnpXmlSerializer.Setup(cnpXmlSerializer => cnpXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedCnpResponse);
            cnpXmlSerializer mockedCnpXmlSerializer = mockCnpXmlSerializer.Object;

            cnpFile mockedCnpFile = mockCnpFile.Object;

            cnp.setCommunication(mockedCommunications);
            cnp.setCnpXmlSerializer(mockedCnpXmlSerializer);
            cnp.setCnpFile(mockedCnpFile);
            cnp.setCnpTime(mockCnpTime.Object);

            batchRequest cnpBatchRequest = new batchRequest();
            cnpBatchRequest.setCnpFile(mockedCnpFile);
            cnpBatchRequest.setCnpTime(mockCnpTime.Object);
            cnpBatchRequest.addEcheckVerification(echeckverification);
            cnpBatchRequest.addEcheckVerification(echeckverification);
            cnp.addBatch(cnpBatchRequest);

            string batchFileName = cnp.sendToCnp();

            cnpResponse actualCnpResponse = cnp.receiveFromCnp(batchFileName);
            batchResponse actualCnpBatchResponse = actualCnpResponse.nextBatchResponse();
            echeckVerificationResponse actualEcheckVerificationResponse1 = actualCnpBatchResponse.nextEcheckVerificationResponse();
            echeckVerificationResponse actualEcheckVerificationResponse2 = actualCnpBatchResponse.nextEcheckVerificationResponse();
            echeckVerificationResponse nullEcheckVerificationResponse = actualCnpBatchResponse.nextEcheckVerificationResponse();

            Assert.AreEqual(123, actualEcheckVerificationResponse1.cnpTxnId);
            Assert.AreEqual(124, actualEcheckVerificationResponse2.cnpTxnId);
            Assert.IsNull(nullEcheckVerificationResponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName  ));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>()  , mockFileName));
        }

        [Test]
        public void TestForceCapture()
        {
            forceCapture forcecapture = new forceCapture();
            forcecapture.orderId = "12344";
            forcecapture.amount = 106;
            forcecapture.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000001";
            card.expDate = "1210";
            forcecapture.card = card;

            var mockCnpResponse = new Mock<cnpResponse>();
            var mockCnpXmlSerializer = new Mock<cnpXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<forceCaptureResponse xmlns='http://www.vantivcnp.com/schema'><cnpTxnId>123</cnpTxnId></forceCaptureResponse>")
                .Returns("<forceCaptureResponse xmlns='http://www.vantivcnp.com/schema'><cnpTxnId>124</cnpTxnId></forceCaptureResponse>");

            batchResponse mockedCnpBatchResponse = new batchResponse();
            mockedCnpBatchResponse.setForceCaptureResponseReader(mockXmlReader.Object);

            mockCnpResponse.Setup(cnpResponse => cnpResponse.nextBatchResponse()).Returns(mockedCnpBatchResponse);
            cnpResponse mockedCnpResponse = mockCnpResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockCnpXmlSerializer.Setup(cnpXmlSerializer => cnpXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedCnpResponse);
            cnpXmlSerializer mockedCnpXmlSerializer = mockCnpXmlSerializer.Object;

            cnpFile mockedCnpFile = mockCnpFile.Object;

            cnp.setCommunication(mockedCommunications);
            cnp.setCnpXmlSerializer(mockedCnpXmlSerializer);
            cnp.setCnpFile(mockedCnpFile);
            cnp.setCnpTime(mockCnpTime.Object);

            batchRequest cnpBatchRequest = new batchRequest();
            cnpBatchRequest.setCnpFile(mockedCnpFile);
            cnpBatchRequest.setCnpTime(mockCnpTime.Object);
            cnpBatchRequest.addForceCapture(forcecapture);
            cnpBatchRequest.addForceCapture(forcecapture);
            cnp.addBatch(cnpBatchRequest);

            string batchFileName = cnp.sendToCnp();

            cnpResponse actualCnpResponse = cnp.receiveFromCnp(batchFileName);
            batchResponse actualCnpBatchResponse = actualCnpResponse.nextBatchResponse();
            forceCaptureResponse actualForceCaptureResponse1 = actualCnpBatchResponse.nextForceCaptureResponse();
            forceCaptureResponse actualForceCaptureResponse2 = actualCnpBatchResponse.nextForceCaptureResponse();
            forceCaptureResponse nullForceCaptureResponse = actualCnpBatchResponse.nextForceCaptureResponse();

            Assert.AreEqual(123, actualForceCaptureResponse1.cnpTxnId);
            Assert.AreEqual(124, actualForceCaptureResponse2.cnpTxnId);
            Assert.IsNull(nullForceCaptureResponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName  ));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>()  , mockFileName));
        }

        [Test]
        public void TestSale()
        {
            sale sale = new sale();
            sale.orderId = "12344";
            sale.amount = 106;
            sale.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000002";
            card.expDate = "1210";
            sale.card = card;

            var mockCnpResponse = new Mock<cnpResponse>();
            var mockCnpXmlSerializer = new Mock<cnpXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<saleResponse xmlns='http://www.vantivcnp.com/schema'><cnpTxnId>123</cnpTxnId></saleResponse>")
                .Returns("<saleResponse xmlns='http://www.vantivcnp.com/schema'><cnpTxnId>124</cnpTxnId></saleResponse>");

            batchResponse mockedCnpBatchResponse = new batchResponse();
            mockedCnpBatchResponse.setSaleResponseReader(mockXmlReader.Object);

            mockCnpResponse.Setup(cnpResponse => cnpResponse.nextBatchResponse()).Returns(mockedCnpBatchResponse);
            cnpResponse mockedCnpResponse = mockCnpResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockCnpXmlSerializer.Setup(cnpXmlSerializer => cnpXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedCnpResponse);
            cnpXmlSerializer mockedCnpXmlSerializer = mockCnpXmlSerializer.Object;

            cnpFile mockedCnpFile = mockCnpFile.Object;

            cnp.setCommunication(mockedCommunications);
            cnp.setCnpXmlSerializer(mockedCnpXmlSerializer);
            cnp.setCnpFile(mockedCnpFile);
            cnp.setCnpTime(mockCnpTime.Object);

            batchRequest cnpBatchRequest = new batchRequest();
            cnpBatchRequest.setCnpFile(mockedCnpFile);
            cnpBatchRequest.setCnpTime(mockCnpTime.Object);
            cnpBatchRequest.addSale(sale);
            cnpBatchRequest.addSale(sale);
            cnp.addBatch(cnpBatchRequest);

            string batchFileName = cnp.sendToCnp();

            cnpResponse actualCnpResponse = cnp.receiveFromCnp(batchFileName);
            batchResponse actualCnpBatchResponse = actualCnpResponse.nextBatchResponse();
            saleResponse actualSaleResponse1 = actualCnpBatchResponse.nextSaleResponse();
            saleResponse actualSaleResponse2 = actualCnpBatchResponse.nextSaleResponse();
            saleResponse nullSaleResponse = actualCnpBatchResponse.nextSaleResponse();

            Assert.AreEqual(123, actualSaleResponse1.cnpTxnId);
            Assert.AreEqual(124, actualSaleResponse2.cnpTxnId);
            Assert.IsNull(nullSaleResponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName  ));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>()  , mockFileName));
        }

        [Test]
        public void TestToken()
        {
            registerTokenRequestType token = new registerTokenRequestType();
            token.orderId = "12344";
            token.accountNumber = "1233456789103801";

            var mockCnpResponse = new Mock<cnpResponse>();
            var mockCnpXmlSerializer = new Mock<cnpXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<registerTokenResponse xmlns='http://www.vantivcnp.com/schema'><cnpTxnId>123</cnpTxnId></registerTokenResponse>")
                .Returns("<registerTokenResponse xmlns='http://www.vantivcnp.com/schema'><cnpTxnId>124</cnpTxnId></registerTokenResponse>");

            batchResponse mockedCnpBatchResponse = new batchResponse();
            mockedCnpBatchResponse.setRegisterTokenResponseReader(mockXmlReader.Object);

            mockCnpResponse.Setup(cnpResponse => cnpResponse.nextBatchResponse()).Returns(mockedCnpBatchResponse);
            cnpResponse mockedCnpResponse = mockCnpResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockCnpXmlSerializer.Setup(cnpXmlSerializer => cnpXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedCnpResponse);
            cnpXmlSerializer mockedCnpXmlSerializer = mockCnpXmlSerializer.Object;

            cnpFile mockedCnpFile = mockCnpFile.Object;

            cnp.setCommunication(mockedCommunications);
            cnp.setCnpXmlSerializer(mockedCnpXmlSerializer);
            cnp.setCnpFile(mockedCnpFile);
            cnp.setCnpTime(mockCnpTime.Object);

            batchRequest cnpBatchRequest = new batchRequest();
            cnpBatchRequest.setCnpFile(mockedCnpFile);
            cnpBatchRequest.setCnpTime(mockCnpTime.Object);
            cnpBatchRequest.addRegisterTokenRequest(token);
            cnpBatchRequest.addRegisterTokenRequest(token);
            cnp.addBatch(cnpBatchRequest);

            string batchFileName = cnp.sendToCnp();

            cnpResponse actualCnpResponse = cnp.receiveFromCnp(batchFileName);
            batchResponse actualCnpBatchResponse = actualCnpResponse.nextBatchResponse();
            registerTokenResponse actualRegisterTokenResponse1 = actualCnpBatchResponse.nextRegisterTokenResponse();
            registerTokenResponse actualRegisterTokenResponse2 = actualCnpBatchResponse.nextRegisterTokenResponse();
            registerTokenResponse nullRegisterTokenResponse = actualCnpBatchResponse.nextRegisterTokenResponse();

            Assert.AreEqual(123, actualRegisterTokenResponse1.cnpTxnId);
            Assert.AreEqual(124, actualRegisterTokenResponse2.cnpTxnId);
            Assert.IsNull(nullRegisterTokenResponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName  ));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>()  , mockFileName));
        }

        [Test]
        public void TestUpdateCardValidationNumOnToken()
        {
            updateCardValidationNumOnToken updateCardValidationNumOnToken = new updateCardValidationNumOnToken();
            updateCardValidationNumOnToken.orderId = "12344";
            updateCardValidationNumOnToken.cnpToken = "123";

            var mockCnpResponse = new Mock<cnpResponse>();
            var mockCnpXmlSerializer = new Mock<cnpXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<updateCardValidationNumOnTokenResponse xmlns='http://www.vantivcnp.com/schema'><cnpTxnId>123</cnpTxnId></updateCardValidationNumOnTokenResponse>")
                .Returns("<updateCardValidationNumOnTokenResponse xmlns='http://www.vantivcnp.com/schema'><cnpTxnId>124</cnpTxnId></updateCardValidationNumOnTokenResponse>");

            batchResponse mockedCnpBatchResponse = new batchResponse();
            mockedCnpBatchResponse.setUpdateCardValidationNumOnTokenResponseReader(mockXmlReader.Object);

            mockCnpResponse.Setup(cnpResponse => cnpResponse.nextBatchResponse()).Returns(mockedCnpBatchResponse);
            cnpResponse mockedCnpResponse = mockCnpResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockCnpXmlSerializer.Setup(cnpXmlSerializer => cnpXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedCnpResponse);
            cnpXmlSerializer mockedCnpXmlSerializer = mockCnpXmlSerializer.Object;

            cnpFile mockedCnpFile = mockCnpFile.Object;

            cnp.setCommunication(mockedCommunications);
            cnp.setCnpXmlSerializer(mockedCnpXmlSerializer);
            cnp.setCnpFile(mockedCnpFile);
            cnp.setCnpTime(mockCnpTime.Object);

            batchRequest cnpBatchRequest = new batchRequest();
            cnpBatchRequest.setCnpFile(mockedCnpFile);
            cnpBatchRequest.setCnpTime(mockCnpTime.Object);
            cnpBatchRequest.addUpdateCardValidationNumOnToken(updateCardValidationNumOnToken);
            cnpBatchRequest.addUpdateCardValidationNumOnToken(updateCardValidationNumOnToken);
            cnp.addBatch(cnpBatchRequest);

            string batchFileName = cnp.sendToCnp();

            cnpResponse actualCnpResponse = cnp.receiveFromCnp(batchFileName);
            batchResponse actualCnpBatchResponse = actualCnpResponse.nextBatchResponse();
            updateCardValidationNumOnTokenResponse actualUpdateCardValidationNumOnTokenResponse1 = actualCnpBatchResponse.nextUpdateCardValidationNumOnTokenResponse();
            updateCardValidationNumOnTokenResponse actualUpdateCardValidationNumOnTokenResponse2 = actualCnpBatchResponse.nextUpdateCardValidationNumOnTokenResponse();
            updateCardValidationNumOnTokenResponse nullUpdateCardValidationNumOnTokenResponse = actualCnpBatchResponse.nextUpdateCardValidationNumOnTokenResponse();

            Assert.AreEqual(123, actualUpdateCardValidationNumOnTokenResponse1.cnpTxnId);
            Assert.AreEqual(124, actualUpdateCardValidationNumOnTokenResponse2.cnpTxnId);
            Assert.IsNull(nullUpdateCardValidationNumOnTokenResponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName  ));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>()  , mockFileName));
        }

        [Test]
        public void TestCnpOnlineException()
        {
            authorization authorization = new authorization();
            authorization.reportGroup = "Planets";
            authorization.orderId = "12344";
            authorization.amount = 106;
            authorization.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000002";
            card.expDate = "1210";
            authorization.card = card;

            var mockCnpResponse = new Mock<cnpResponse>();
            var mockCnpBatchResponse = new Mock<batchResponse>();
            var mockCnpXmlSerializer = new Mock<cnpXmlSerializer>();

            authorizationResponse mockAuthorizationResponse1 = new authorizationResponse();
            mockAuthorizationResponse1.cnpTxnId = 123;
            authorizationResponse mockAuthorizationResponse2 = new authorizationResponse();
            mockAuthorizationResponse2.cnpTxnId = 124;

            mockCnpBatchResponse.SetupSequence(cnpBatchResponse => cnpBatchResponse.nextAuthorizationResponse())
                .Returns(mockAuthorizationResponse1)
                .Returns(mockAuthorizationResponse2)
                .Returns((authorizationResponse)null);

            cnpResponse mockedCnpResponse = mockCnpResponse.Object;
            mockedCnpResponse.message = "Error validating xml data against the schema";
            mockedCnpResponse.response = "1";

            Communications mockedCommunications = mockCommunications.Object;

            mockCnpXmlSerializer.Setup(cnpXmlSerializer => cnpXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedCnpResponse);
            cnpXmlSerializer mockedCnpXmlSerializer = mockCnpXmlSerializer.Object;

            cnpFile mockedCnpFile = mockCnpFile.Object;

            try
            {
                cnp.setCommunication(mockedCommunications);
                cnp.setCnpXmlSerializer(mockedCnpXmlSerializer);
                cnp.setCnpFile(mockedCnpFile);
                cnp.setCnpTime(mockCnpTime.Object);
                batchRequest cnpBatchRequest = new batchRequest();
                cnpBatchRequest.setCnpFile(mockedCnpFile);
                cnpBatchRequest.setCnpTime(mockCnpTime.Object);

                cnpBatchRequest.addAuthorization(authorization);
                cnpBatchRequest.addAuthorization(authorization);
                cnp.addBatch(cnpBatchRequest);

                string batchFileName = cnp.sendToCnp();
                cnpResponse cnpResponse = cnp.receiveFromCnp(batchFileName);
            }
            catch (CnpOnlineException e)
            {
                Assert.AreEqual("Error validating xml data against the schema", e.Message);
            }
        }

        [Test]
        public void TestInvalidOperationException()
        {
            authorization authorization = new authorization();
            authorization.reportGroup = "Planets";
            authorization.orderId = "12344";
            authorization.amount = 106;
            authorization.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000002";
            card.expDate = "1210";
            authorization.card = card;

            cnpResponse mockCnpResponse = null;

            var mockXml = new Mock<cnpXmlSerializer>();

            Communications mockedCommunications = mockCommunications.Object;

            mockXml.Setup(cnpXmlSerializer => cnpXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockCnpResponse);
            cnpXmlSerializer mockedCnpXmlSerializer = mockXml.Object;

            cnpFile mockedCnpFile = mockCnpFile.Object;

            try
            {
                cnp.setCommunication(mockedCommunications);
                cnp.setCnpXmlSerializer(mockedCnpXmlSerializer);
                cnp.setCnpFile(mockedCnpFile);
                cnp.setCnpTime(mockCnpTime.Object);
                batchRequest cnpBatchRequest = new batchRequest();
                cnpBatchRequest.setCnpFile(mockedCnpFile);
                cnpBatchRequest.setCnpTime(mockCnpTime.Object);

                cnpBatchRequest.addAuthorization(authorization);
                cnpBatchRequest.addAuthorization(authorization);
                cnp.addBatch(cnpBatchRequest);

                string batchFileName = cnp.sendToCnp();
                cnpResponse cnpResponse = cnp.receiveFromCnp(batchFileName);
            }
            catch (CnpOnlineException e)
            {
                Assert.AreEqual("Error validating xml data against the schema", e.Message);
            }
        }

        [Test]
        public void TestDefaultReportGroup()
        {
            authorization authorization = new authorization();
            authorization.orderId = "12344";
            authorization.amount = 106;
            authorization.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000002";
            card.expDate = "1210";
            authorization.card = card;

            var mockCnpResponse = new Mock<cnpResponse>();
            var mockCnpXmlSerializer = new Mock<cnpXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<authorizationResponse reportGroup=\"Default Report Group\" xmlns='http://www.vantivcnp.com/schema'><cnpTxnId>123</cnpTxnId></authorizationResponse>")
                .Returns("<authorizationResponse reportGroup=\"Default Report Group\" xmlns='http://www.vantivcnp.com/schema'><cnpTxnId>124</cnpTxnId></authorizationResponse>");

            batchResponse mockedCnpBatchResponse = new batchResponse();
            mockedCnpBatchResponse.setAuthorizationResponseReader(mockXmlReader.Object);

            mockCnpResponse.Setup(cnpResponse => cnpResponse.nextBatchResponse()).Returns(mockedCnpBatchResponse);
            cnpResponse mockedCnpResponse = mockCnpResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockCnpXmlSerializer.Setup(cnpXmlSerializer => cnpXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedCnpResponse);
            cnpXmlSerializer mockedCnpXmlSerializer = mockCnpXmlSerializer.Object;

            cnpFile mockedCnpFile = mockCnpFile.Object;

            cnp.setCommunication(mockedCommunications);
            cnp.setCnpXmlSerializer(mockedCnpXmlSerializer);
            cnp.setCnpFile(mockedCnpFile);
            cnp.setCnpTime(mockCnpTime.Object);
            batchRequest cnpBatchRequest = new batchRequest();
            cnpBatchRequest.setCnpFile(mockedCnpFile);
            cnpBatchRequest.setCnpTime(mockCnpTime.Object);
            cnpBatchRequest.addAuthorization(authorization);
            cnpBatchRequest.addAuthorization(authorization);
            cnp.addBatch(cnpBatchRequest);

            string batchFileName = cnp.sendToCnp();

            cnpResponse actualCnpResponse = cnp.receiveFromCnp(batchFileName);
            batchResponse actualCnpBatchResponse = actualCnpResponse.nextBatchResponse();
            authorizationResponse actualAuthorizationResponse1 = actualCnpBatchResponse.nextAuthorizationResponse();
            authorizationResponse actualAuthorizationResponse2 = actualCnpBatchResponse.nextAuthorizationResponse();
            authorizationResponse nullAuthorizationResponse = actualCnpBatchResponse.nextAuthorizationResponse();

            Assert.AreEqual(123, actualAuthorizationResponse1.cnpTxnId);
            Assert.AreEqual("Default Report Group", actualAuthorizationResponse1.reportGroup);
            Assert.AreEqual(124, actualAuthorizationResponse2.cnpTxnId);
            Assert.AreEqual("Default Report Group", actualAuthorizationResponse2.reportGroup);
            Assert.IsNull(nullAuthorizationResponse);

            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, It.IsRegex(".*reportGroup=\"Default Report Group\".*", RegexOptions.Singleline)));
            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName  ));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>()  , mockFileName));
        }

        [Test]
        public void TestSerialize()
        {
            authorization authorization = new authorization();
            authorization.orderId = "12344";
            authorization.amount = 106;
            authorization.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000002";
            card.expDate = "1210";
            authorization.card = card;

            cnpFile mockedCnpFile = mockCnpFile.Object;
            cnpTime mockedCnpTime = mockCnpTime.Object;

            cnp.setCnpTime(mockedCnpTime);
            cnp.setCnpFile(mockedCnpFile);

            batchRequest cnpBatchRequest = new batchRequest();
            cnpBatchRequest.setCnpFile(mockedCnpFile);
            cnpBatchRequest.addAuthorization(authorization);
            cnp.addBatch(cnpBatchRequest);

            string resultFile = cnp.Serialize();

            Assert.IsTrue(resultFile.Equals(mockFilePath));

            mockCnpFile.Verify(cnpFile => cnpFile.AppendFileToFile(mockFilePath, It.IsAny<String>()));
        }

        [Test]
        public void TestRFRRequest()
        {
            RFRRequest rfrRequest = new RFRRequest();
            rfrRequest.cnpSessionId = 123456789;

            var mockBatchXmlReader = new Mock<XmlReader>();
            mockBatchXmlReader.Setup(XmlReader => XmlReader.ReadState).Returns(ReadState.Closed);

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadState).Returns(ReadState.Interactive).Returns(ReadState.Closed);
            mockXmlReader.Setup(XmlReader => XmlReader.ReadOuterXml()).Returns("<RFRResponse response=\"1\" message=\"The account update file is not ready yet. Please try again later.\" xmlns='http://www.vantivcnp.com/schema'> </RFRResponse>");

            cnpResponse mockedCnpResponse = new cnpResponse();
            mockedCnpResponse.setRfrResponseReader(mockXmlReader.Object);
            mockedCnpResponse.setBatchResponseReader(mockBatchXmlReader.Object);

            var mockCnpXmlSerializer = new Mock<cnpXmlSerializer>();
            mockCnpXmlSerializer.Setup(cnpXmlSerializer => cnpXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedCnpResponse);
            cnpXmlSerializer mockedCnpXmlSerializer = mockCnpXmlSerializer.Object;

            cnpFile mockedCnpFile = mockCnpFile.Object;
            cnpTime mockedCnpTime = mockCnpTime.Object;
            Communications mockedCommunications = mockCommunications.Object;

            cnp.setCnpFile(mockedCnpFile);
            cnp.setCnpTime(mockedCnpTime);
            cnp.setCommunication(mockedCommunications);
            cnp.setCnpXmlSerializer(mockedCnpXmlSerializer);

            rfrRequest.setCnpFile(mockedCnpFile);
            rfrRequest.setCnpTime(mockedCnpTime);

            cnp.addRFRRequest(rfrRequest);

            string batchFileName = cnp.sendToCnp();

            cnpResponse actualCnpResponse = cnp.receiveFromCnp(batchFileName);
            batchResponse nullCnpBatchResponse = actualCnpResponse.nextBatchResponse();
            RFRResponse actualRFRResponse = actualCnpResponse.nextRFRResponse();
            RFRResponse nullRFRResponse = actualCnpResponse.nextRFRResponse();

            Assert.IsNotNull(actualRFRResponse);
            Assert.AreEqual("1", actualRFRResponse.response);
            Assert.AreEqual("The account update file is not ready yet. Please try again later.", actualRFRResponse.message);
            Assert.IsNull(nullCnpBatchResponse);
            Assert.IsNull(nullRFRResponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName  ));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>()  , mockFileName));
        }

        [Test]
        public void TestCancelSubscription()
        {
            cancelSubscription cancel = new cancelSubscription();
            cancel.subscriptionId = 12345;

            var mockCnpResponse = new Mock<cnpResponse>();
            var mockCnpXmlSerializer = new Mock<cnpXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<cancelSubscriptionResponse xmlns=\"http://www.vantivcnp.com/schema\"><cnpTxnId>54321</cnpTxnId><response>000</response><message>Approved</message><responseTime>2013-09-04T21:55:14</responseTime><subscriptionId>12345</subscriptionId></cancelSubscriptionResponse>")
                .Returns("<cancelSubscriptionResponse xmlns=\"http://www.vantivcnp.com/schema\"><cnpTxnId>12345</cnpTxnId><response>000</response><message>Approved</message><responseTime>2013-09-04T21:55:14</responseTime><subscriptionId>54321</subscriptionId></cancelSubscriptionResponse>");

            batchResponse mockCnpBatchResponse = new batchResponse();
            mockCnpBatchResponse.setCancelSubscriptionResponseReader(mockXmlReader.Object);

            mockCnpResponse.Setup(cnpResponse => cnpResponse.nextBatchResponse()).Returns(mockCnpBatchResponse);
            cnpResponse mockedCnpResponse = mockCnpResponse.Object;

            mockCnpXmlSerializer.Setup(cnpXmlSerializer => cnpXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedCnpResponse);

            Communications mockedCommunication = mockCommunications.Object;
            cnp.setCommunication(mockedCommunication);

            cnpXmlSerializer mockedCnpXmlSerializer = mockCnpXmlSerializer.Object;
            cnp.setCnpXmlSerializer(mockedCnpXmlSerializer);

            cnpFile mockedCnpFile = mockCnpFile.Object;
            cnp.setCnpFile(mockedCnpFile);

            cnp.setCnpTime(mockCnpTime.Object);

            batchRequest cnpBatchRequest = new batchRequest();
            cnpBatchRequest.setCnpFile(mockedCnpFile);
            cnpBatchRequest.setCnpTime(mockCnpTime.Object);
            cnpBatchRequest.addCancelSubscription(cancel);
            cnp.addBatch(cnpBatchRequest);

            string batchFileName = cnp.sendToCnp();
            cnpResponse actualCnpResponse = cnp.receiveFromCnp(batchFileName);
            batchResponse actualCnpBatchResponse = actualCnpResponse.nextBatchResponse();

            Assert.AreSame(mockCnpBatchResponse, actualCnpBatchResponse);
            Assert.AreEqual("12345", actualCnpBatchResponse.nextCancelSubscriptionResponse().subscriptionId);
            Assert.AreEqual("54321", actualCnpBatchResponse.nextCancelSubscriptionResponse().subscriptionId);
            Assert.IsNull(actualCnpBatchResponse.nextCancelSubscriptionResponse());

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName  ));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>()  , mockFileName));
        }

        [Test]
        public void TestUpdateSubscription()
        {
            updateSubscription update = new updateSubscription();
            update.billingDate = new DateTime(2002, 10, 9);
            contact billToAddress = new contact();
            billToAddress.name = "Greg Dake";
            billToAddress.city = "Lowell";
            billToAddress.state = "MA";
            billToAddress.email = "sdksupport@cnp.com";
            update.billToAddress = billToAddress;
            cardType card = new cardType();
            card.number = "4100000000000001";
            card.expDate = "1215";
            card.type = methodOfPaymentTypeEnum.VI;
            update.card = card;
            update.planCode = "abcdefg";
            update.subscriptionId = 12345;

            var mockCnpResponse = new Mock<cnpResponse>();
            var mockCnpXmlSerializer = new Mock<cnpXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<updateSubscriptionResponse xmlns=\"http://www.vantivcnp.com/schema\"><cnpTxnId>54321</cnpTxnId><response>000</response><message>Approved</message><responseTime>2013-09-04T21:55:14</responseTime><subscriptionId>12345</subscriptionId></updateSubscriptionResponse>")
                .Returns("<updateSubscriptionResponse xmlns=\"http://www.vantivcnp.com/schema\"><cnpTxnId>12345</cnpTxnId><response>000</response><message>Approved</message><responseTime>2013-09-04T21:55:14</responseTime><subscriptionId>54321</subscriptionId></updateSubscriptionResponse>");

            batchResponse mockCnpBatchResponse = new batchResponse();
            mockCnpBatchResponse.setUpdateSubscriptionResponseReader(mockXmlReader.Object);

            mockCnpResponse.Setup(cnpResponse => cnpResponse.nextBatchResponse()).Returns(mockCnpBatchResponse);
            cnpResponse mockedCnpResponse = mockCnpResponse.Object;

            mockCnpXmlSerializer.Setup(cnpXmlSerializer => cnpXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedCnpResponse);

            Communications mockedCommunication = mockCommunications.Object;
            cnp.setCommunication(mockedCommunication);

            cnpXmlSerializer mockedCnpXmlSerializer = mockCnpXmlSerializer.Object;
            cnp.setCnpXmlSerializer(mockedCnpXmlSerializer);

            cnpFile mockedCnpFile = mockCnpFile.Object;
            cnp.setCnpFile(mockedCnpFile);

            cnp.setCnpTime(mockCnpTime.Object);

            batchRequest cnpBatchRequest = new batchRequest();
            cnpBatchRequest.setCnpFile(mockedCnpFile);
            cnpBatchRequest.setCnpTime(mockCnpTime.Object);
            cnpBatchRequest.addUpdateSubscription(update);
            cnp.addBatch(cnpBatchRequest);

            string batchFileName = cnp.sendToCnp();
            cnpResponse actualCnpResponse = cnp.receiveFromCnp(batchFileName);
            batchResponse actualCnpBatchResponse = actualCnpResponse.nextBatchResponse();

            Assert.AreSame(mockCnpBatchResponse, actualCnpBatchResponse);
            Assert.AreEqual("12345", actualCnpBatchResponse.nextUpdateSubscriptionResponse().subscriptionId);
            Assert.AreEqual("54321", actualCnpBatchResponse.nextUpdateSubscriptionResponse().subscriptionId);
            Assert.IsNull(actualCnpBatchResponse.nextUpdateSubscriptionResponse());

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName  ));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>()  , mockFileName));
        }

        [Test]
        public void testCreatePlan()
        {
            createPlan createPlan = new createPlan();
            createPlan.planCode = "thePlanCode";
            createPlan.name = "theName";
            createPlan.intervalType = intervalType.ANNUAL;
            createPlan.amount = 100;

            var mockCnpResponse = new Mock<cnpResponse>();
            var mockCnpXmlSerializer = new Mock<cnpXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<createPlanResponse xmlns=\"http://www.vantivcnp.com/schema\"><cnpTxnId>123</cnpTxnId></createPlanResponse>")
                .Returns("<createPlanResponse xmlns=\"http://www.vantivcnp.com/schema\"><cnpTxnId>124</cnpTxnId></createPlanResponse>");

            batchResponse mockCnpBatchResponse = new batchResponse();
            mockCnpBatchResponse.setCreatePlanResponseReader(mockXmlReader.Object);

            mockCnpResponse.Setup(cnpResponse => cnpResponse.nextBatchResponse()).Returns(mockCnpBatchResponse);
            cnpResponse mockedCnpResponse = mockCnpResponse.Object;

            mockCnpXmlSerializer.Setup(cnpXmlSerializer => cnpXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedCnpResponse);

            Communications mockedCommunication = mockCommunications.Object;
            cnp.setCommunication(mockedCommunication);

            cnpXmlSerializer mockedCnpXmlSerializer = mockCnpXmlSerializer.Object;
            cnp.setCnpXmlSerializer(mockedCnpXmlSerializer);

            cnpFile mockedCnpFile = mockCnpFile.Object;
            cnp.setCnpFile(mockedCnpFile);

            cnp.setCnpTime(mockCnpTime.Object);

            batchRequest cnpBatchRequest = new batchRequest();
            cnpBatchRequest.setCnpFile(mockedCnpFile);
            cnpBatchRequest.setCnpTime(mockCnpTime.Object);
            cnpBatchRequest.addCreatePlan(createPlan);
            cnpBatchRequest.addCreatePlan(createPlan);
            cnp.addBatch(cnpBatchRequest);

            string batchFileName = cnp.sendToCnp();
            cnpResponse actualCnpResponse = cnp.receiveFromCnp(batchFileName);
            batchResponse actualCnpBatchResponse = actualCnpResponse.nextBatchResponse();

            Assert.AreSame(mockCnpBatchResponse, actualCnpBatchResponse);
            Assert.AreEqual("123", actualCnpBatchResponse.nextCreatePlanResponse().cnpTxnId);
            Assert.AreEqual("124", actualCnpBatchResponse.nextCreatePlanResponse().cnpTxnId);
            Assert.IsNull(actualCnpBatchResponse.nextCreatePlanResponse());

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName  ));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>()  , mockFileName));
        }

        [Test]
        public void TestUpdatePlan()
        {
            updatePlan updatePlan = new updatePlan();
            updatePlan.planCode = "thePlanCode";
            updatePlan.active = true;

            var mockCnpResponse = new Mock<cnpResponse>();
            var mockCnpXmlSerializer = new Mock<cnpXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<updatePlanResponse xmlns=\"http://www.vantivcnp.com/schema\"><cnpTxnId>123</cnpTxnId></updatePlanResponse>")
                .Returns("<updatePlanResponse xmlns=\"http://www.vantivcnp.com/schema\"><cnpTxnId>124</cnpTxnId></updatePlanResponse>");

            batchResponse mockCnpBatchResponse = new batchResponse();
            mockCnpBatchResponse.setUpdatePlanResponseReader(mockXmlReader.Object);

            mockCnpResponse.Setup(cnpResponse => cnpResponse.nextBatchResponse()).Returns(mockCnpBatchResponse);
            cnpResponse mockedCnpResponse = mockCnpResponse.Object;

            mockCnpXmlSerializer.Setup(cnpXmlSerializer => cnpXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedCnpResponse);

            Communications mockedCommunication = mockCommunications.Object;
            cnp.setCommunication(mockedCommunication);

            cnpXmlSerializer mockedCnpXmlSerializer = mockCnpXmlSerializer.Object;
            cnp.setCnpXmlSerializer(mockedCnpXmlSerializer);

            cnpFile mockedCnpFile = mockCnpFile.Object;
            cnp.setCnpFile(mockedCnpFile);

            cnp.setCnpTime(mockCnpTime.Object);

            batchRequest cnpBatchRequest = new batchRequest();
            cnpBatchRequest.setCnpFile(mockedCnpFile);
            cnpBatchRequest.setCnpTime(mockCnpTime.Object);
            cnpBatchRequest.addUpdatePlan(updatePlan);
            cnpBatchRequest.addUpdatePlan(updatePlan);
            cnp.addBatch(cnpBatchRequest);

            string batchFileName = cnp.sendToCnp();
            cnpResponse actualCnpResponse = cnp.receiveFromCnp(batchFileName);
            batchResponse actualCnpBatchResponse = actualCnpResponse.nextBatchResponse();

            Assert.AreSame(mockCnpBatchResponse, actualCnpBatchResponse);
            Assert.AreEqual("123", actualCnpBatchResponse.nextUpdatePlanResponse().cnpTxnId);
            Assert.AreEqual("124", actualCnpBatchResponse.nextUpdatePlanResponse().cnpTxnId);
            Assert.IsNull(actualCnpBatchResponse.nextUpdatePlanResponse());

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName  ));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>()  , mockFileName));
        }

        [Test]
        public void TestActivate()
        {
            activate activate = new activate();
            activate.orderId = "theOrderId";
            activate.orderSource = orderSourceType.ecommerce;
            activate.card = new giftCardCardType();

            var mockCnpResponse = new Mock<cnpResponse>();
            var mockCnpXmlSerializer = new Mock<cnpXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<activateResponse reportGroup=\"A\" id=\"3\" customerId=\"4\" duplicate=\"true\" xmlns=\"http://www.vantivcnp.com/schema\"><cnpTxnId>123</cnpTxnId><response>000</response><responseTime>2013-09-05T14:23:45</responseTime><postDate>2013-09-05</postDate><message>Approved</message><fraudResult></fraudResult><giftCardResponse></giftCardResponse></activateResponse>")
                .Returns("<activateResponse xmlns=\"http://www.vantivcnp.com/schema\"><cnpTxnId>124</cnpTxnId></activateResponse>");

            batchResponse mockCnpBatchResponse = new batchResponse();
            mockCnpBatchResponse.setActivateResponseReader(mockXmlReader.Object);

            mockCnpResponse.Setup(cnpResponse => cnpResponse.nextBatchResponse()).Returns(mockCnpBatchResponse);
            cnpResponse mockedCnpResponse = mockCnpResponse.Object;

            mockCnpXmlSerializer.Setup(cnpXmlSerializer => cnpXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedCnpResponse);

            Communications mockedCommunication = mockCommunications.Object;
            cnp.setCommunication(mockedCommunication);

            cnpXmlSerializer mockedCnpXmlSerializer = mockCnpXmlSerializer.Object;
            cnp.setCnpXmlSerializer(mockedCnpXmlSerializer);

            cnpFile mockedCnpFile = mockCnpFile.Object;
            cnp.setCnpFile(mockedCnpFile);

            cnp.setCnpTime(mockCnpTime.Object);

            batchRequest cnpBatchRequest = new batchRequest();
            cnpBatchRequest.setCnpFile(mockedCnpFile);
            cnpBatchRequest.setCnpTime(mockCnpTime.Object);
            cnpBatchRequest.addActivate(activate);
            cnpBatchRequest.addActivate(activate);
            cnp.addBatch(cnpBatchRequest);

            string batchFileName = cnp.sendToCnp();
            cnpResponse actualCnpResponse = cnp.receiveFromCnp(batchFileName);
            batchResponse actualCnpBatchResponse = actualCnpResponse.nextBatchResponse();

            Assert.AreSame(mockCnpBatchResponse, actualCnpBatchResponse);
            Assert.AreEqual(123, actualCnpBatchResponse.nextActivateResponse().cnpTxnId);
            Assert.AreEqual(124, actualCnpBatchResponse.nextActivateResponse().cnpTxnId);
            Assert.IsNull(actualCnpBatchResponse.nextActivateResponse());

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName  ));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>()  , mockFileName));
        }

        [Test]
        public void testDeactivate()
        {
            deactivate deactivate = new deactivate();
            deactivate.orderId = "theOrderId";
            deactivate.orderSource = orderSourceType.ecommerce;
            deactivate.card = new giftCardCardType();

            var mockCnpResponse = new Mock<cnpResponse>();
            var mockCnpXmlSerializer = new Mock<cnpXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<deactivateResponse xmlns=\"http://www.vantivcnp.com/schema\"><cnpTxnId>123</cnpTxnId></deactivateResponse>")
                .Returns("<deactivateResponse xmlns=\"http://www.vantivcnp.com/schema\"><cnpTxnId>124</cnpTxnId></deactivateResponse>");

            batchResponse mockCnpBatchResponse = new batchResponse();
            mockCnpBatchResponse.setDeactivateResponseReader(mockXmlReader.Object);

            mockCnpResponse.Setup(cnpResponse => cnpResponse.nextBatchResponse()).Returns(mockCnpBatchResponse);
            cnpResponse mockedCnpResponse = mockCnpResponse.Object;

            mockCnpXmlSerializer.Setup(cnpXmlSerializer => cnpXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedCnpResponse);

            Communications mockedCommunication = mockCommunications.Object;
            cnp.setCommunication(mockedCommunication);

            cnpXmlSerializer mockedCnpXmlSerializer = mockCnpXmlSerializer.Object;
            cnp.setCnpXmlSerializer(mockedCnpXmlSerializer);

            cnpFile mockedCnpFile = mockCnpFile.Object;
            cnp.setCnpFile(mockedCnpFile);

            cnp.setCnpTime(mockCnpTime.Object);

            batchRequest cnpBatchRequest = new batchRequest();
            cnpBatchRequest.setCnpFile(mockedCnpFile);
            cnpBatchRequest.setCnpTime(mockCnpTime.Object);
            cnpBatchRequest.addDeactivate(deactivate);
            cnpBatchRequest.addDeactivate(deactivate);
            cnp.addBatch(cnpBatchRequest);

            string batchFileName = cnp.sendToCnp();
            cnpResponse actualCnpResponse = cnp.receiveFromCnp(batchFileName);
            batchResponse actualCnpBatchResponse = actualCnpResponse.nextBatchResponse();

            Assert.AreSame(mockCnpBatchResponse, actualCnpBatchResponse);
            Assert.AreEqual(123, actualCnpBatchResponse.nextDeactivateResponse().cnpTxnId);
            Assert.AreEqual(124, actualCnpBatchResponse.nextDeactivateResponse().cnpTxnId);
            Assert.IsNull(actualCnpBatchResponse.nextDeactivateResponse());

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName  ));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>()  , mockFileName));
        }

        [Test]
        public void testLoad()
        {
            load load = new load();
            load.orderId = "theOrderId";
            load.orderSource = orderSourceType.ecommerce;
            load.card = new giftCardCardType();

            var mockCnpResponse = new Mock<cnpResponse>();
            var mockCnpXmlSerializer = new Mock<cnpXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<loadResponse xmlns=\"http://www.vantivcnp.com/schema\"><cnpTxnId>123</cnpTxnId></loadResponse>")
                .Returns("<loadResponse xmlns=\"http://www.vantivcnp.com/schema\"><cnpTxnId>124</cnpTxnId></loadResponse>");

            batchResponse mockCnpBatchResponse = new batchResponse();
            mockCnpBatchResponse.setLoadResponseReader(mockXmlReader.Object);

            mockCnpResponse.Setup(cnpResponse => cnpResponse.nextBatchResponse()).Returns(mockCnpBatchResponse);
            cnpResponse mockedCnpResponse = mockCnpResponse.Object;

            mockCnpXmlSerializer.Setup(cnpXmlSerializer => cnpXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedCnpResponse);

            Communications mockedCommunication = mockCommunications.Object;
            cnp.setCommunication(mockedCommunication);

            cnpXmlSerializer mockedCnpXmlSerializer = mockCnpXmlSerializer.Object;
            cnp.setCnpXmlSerializer(mockedCnpXmlSerializer);

            cnpFile mockedCnpFile = mockCnpFile.Object;
            cnp.setCnpFile(mockedCnpFile);

            cnp.setCnpTime(mockCnpTime.Object);

            batchRequest cnpBatchRequest = new batchRequest();
            cnpBatchRequest.setCnpFile(mockedCnpFile);
            cnpBatchRequest.setCnpTime(mockCnpTime.Object);
            cnpBatchRequest.addLoad(load);
            cnpBatchRequest.addLoad(load);
            cnp.addBatch(cnpBatchRequest);

            string batchFileName = cnp.sendToCnp();
            cnpResponse actualCnpResponse = cnp.receiveFromCnp(batchFileName);
            batchResponse actualCnpBatchResponse = actualCnpResponse.nextBatchResponse();

            Assert.AreSame(mockCnpBatchResponse, actualCnpBatchResponse);
            Assert.AreEqual(123, actualCnpBatchResponse.nextLoadResponse().cnpTxnId);
            Assert.AreEqual(124, actualCnpBatchResponse.nextLoadResponse().cnpTxnId);
            Assert.IsNull(actualCnpBatchResponse.nextLoadResponse());

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName  ));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>()  , mockFileName));
        }

        [Test]
        public void testUnload()
        {
            unload unload = new unload();
            unload.orderId = "theOrderId";
            unload.orderSource = orderSourceType.ecommerce;
            unload.card = new giftCardCardType();

            var mockCnpResponse = new Mock<cnpResponse>();
            var mockCnpXmlSerializer = new Mock<cnpXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<unloadResponse xmlns=\"http://www.vantivcnp.com/schema\"><cnpTxnId>123</cnpTxnId></unloadResponse>")
                .Returns("<unloadResponse xmlns=\"http://www.vantivcnp.com/schema\"><cnpTxnId>124</cnpTxnId></unloadResponse>");

            batchResponse mockCnpBatchResponse = new batchResponse();
            mockCnpBatchResponse.setUnloadResponseReader(mockXmlReader.Object);

            mockCnpResponse.Setup(cnpResponse => cnpResponse.nextBatchResponse()).Returns(mockCnpBatchResponse);
            cnpResponse mockedCnpResponse = mockCnpResponse.Object;

            mockCnpXmlSerializer.Setup(cnpXmlSerializer => cnpXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedCnpResponse);

            Communications mockedCommunication = mockCommunications.Object;
            cnp.setCommunication(mockedCommunication);

            cnpXmlSerializer mockedCnpXmlSerializer = mockCnpXmlSerializer.Object;
            cnp.setCnpXmlSerializer(mockedCnpXmlSerializer);

            cnpFile mockedCnpFile = mockCnpFile.Object;
            cnp.setCnpFile(mockedCnpFile);

            cnp.setCnpTime(mockCnpTime.Object);

            batchRequest cnpBatchRequest = new batchRequest();
            cnpBatchRequest.setCnpFile(mockedCnpFile);
            cnpBatchRequest.setCnpTime(mockCnpTime.Object);
            cnpBatchRequest.addUnload(unload);
            cnpBatchRequest.addUnload(unload);
            cnp.addBatch(cnpBatchRequest);

            string batchFileName = cnp.sendToCnp();
            cnpResponse actualCnpResponse = cnp.receiveFromCnp(batchFileName);
            batchResponse actualCnpBatchResponse = actualCnpResponse.nextBatchResponse();

            Assert.AreSame(mockCnpBatchResponse, actualCnpBatchResponse);
            Assert.AreEqual(123, actualCnpBatchResponse.nextUnloadResponse().cnpTxnId);
            Assert.AreEqual(124, actualCnpBatchResponse.nextUnloadResponse().cnpTxnId);
            Assert.IsNull(actualCnpBatchResponse.nextUnloadResponse());

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName  ));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>()  , mockFileName));
        }

        [Test]
        public void TestBalanceInquiry()
        {
            balanceInquiry balanceInquiry = new balanceInquiry();
            balanceInquiry.orderId = "theOrderId";
            balanceInquiry.orderSource = orderSourceType.ecommerce;
            balanceInquiry.card = new giftCardCardType();

            var mockCnpResponse = new Mock<cnpResponse>();
            var mockCnpXmlSerializer = new Mock<cnpXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<balanceInquiryResponse xmlns=\"http://www.vantivcnp.com/schema\"><cnpTxnId>123</cnpTxnId></balanceInquiryResponse>")
                .Returns("<balanceInquiryResponse xmlns=\"http://www.vantivcnp.com/schema\"><cnpTxnId>124</cnpTxnId></balanceInquiryResponse>");

            batchResponse mockCnpBatchResponse = new batchResponse();
            mockCnpBatchResponse.setBalanceInquiryResponseReader(mockXmlReader.Object);

            mockCnpResponse.Setup(cnpResponse => cnpResponse.nextBatchResponse()).Returns(mockCnpBatchResponse);
            cnpResponse mockedCnpResponse = mockCnpResponse.Object;

            mockCnpXmlSerializer.Setup(cnpXmlSerializer => cnpXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedCnpResponse);

            Communications mockedCommunication = mockCommunications.Object;
            cnp.setCommunication(mockedCommunication);

            cnpXmlSerializer mockedCnpXmlSerializer = mockCnpXmlSerializer.Object;
            cnp.setCnpXmlSerializer(mockedCnpXmlSerializer);

            cnpFile mockedCnpFile = mockCnpFile.Object;
            cnp.setCnpFile(mockedCnpFile);

            cnp.setCnpTime(mockCnpTime.Object);

            batchRequest cnpBatchRequest = new batchRequest();
            cnpBatchRequest.setCnpFile(mockedCnpFile);
            cnpBatchRequest.setCnpTime(mockCnpTime.Object);
            cnpBatchRequest.addBalanceInquiry(balanceInquiry);
            cnpBatchRequest.addBalanceInquiry(balanceInquiry);
            cnp.addBatch(cnpBatchRequest);

            string batchFileName = cnp.sendToCnp();
            cnpResponse actualCnpResponse = cnp.receiveFromCnp(batchFileName);
            batchResponse actualCnpBatchResponse = actualCnpResponse.nextBatchResponse();

            Assert.AreSame(mockCnpBatchResponse, actualCnpBatchResponse);
            Assert.AreEqual(123, actualCnpBatchResponse.nextBalanceInquiryResponse().cnpTxnId);
            Assert.AreEqual(124, actualCnpBatchResponse.nextBalanceInquiryResponse().cnpTxnId);
            Assert.IsNull(actualCnpBatchResponse.nextBalanceInquiryResponse());

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName  ));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>()  , mockFileName));
        }

        [Test]
        public void TestEcheckPreNoteSale()
        {
            echeckPreNoteSale echeckPreNoteSale = new echeckPreNoteSale();
            echeckPreNoteSale.orderId = "12345";
            echeckPreNoteSale.orderSource = orderSourceType.ecommerce;
            echeckType echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "12345657890";
            echeck.routingNum = "123456789";
            echeck.checkNum = "123455";
            echeckPreNoteSale.echeck = echeck;
            contact contact = new contact();
            contact.name = "Bob";
            contact.city = "lowell";
            contact.state = "MA";
            contact.email = "cnp.com";
            echeckPreNoteSale.billToAddress = contact;

            var mockCnpResponse = new Mock<cnpResponse>();
            var mockCnpXmlSerializer = new Mock<cnpXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<echeckPreNoteSaleResponse xmlns='http://www.vantivcnp.com/schema'><cnpTxnId>123</cnpTxnId></echeckPreNoteSaleResponse>")
                .Returns("<echeckPreNoteSaleResponse xmlns='http://www.vantivcnp.com/schema'><cnpTxnId>124</cnpTxnId></echeckPreNoteSaleResponse>");

            batchResponse mockedCnpBatchResponse = new batchResponse();
            mockedCnpBatchResponse.setEcheckPreNoteSaleResponseReader(mockXmlReader.Object);

            mockCnpResponse.Setup(cnpResponse => cnpResponse.nextBatchResponse()).Returns(mockedCnpBatchResponse);
            cnpResponse mockedCnpResponse = mockCnpResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockCnpXmlSerializer.Setup(cnpXmlSerializer => cnpXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedCnpResponse);
            cnpXmlSerializer mockedCnpXmlSerializer = mockCnpXmlSerializer.Object;

            cnpFile mockedCnpFile = mockCnpFile.Object;

            cnp.setCommunication(mockedCommunications);
            cnp.setCnpXmlSerializer(mockedCnpXmlSerializer);
            cnp.setCnpFile(mockedCnpFile);
            cnp.setCnpTime(mockCnpTime.Object);

            batchRequest cnpBatchRequest = new batchRequest();
            cnpBatchRequest.setCnpFile(mockedCnpFile);
            cnpBatchRequest.setCnpTime(mockCnpTime.Object);
            cnpBatchRequest.addEcheckPreNoteSale(echeckPreNoteSale);
            cnpBatchRequest.addEcheckPreNoteSale(echeckPreNoteSale);
            cnp.addBatch(cnpBatchRequest);

            string batchFileName = cnp.sendToCnp();

            cnpResponse actualCnpResponse = cnp.receiveFromCnp(batchFileName);
            batchResponse actualCnpBatchResponse = actualCnpResponse.nextBatchResponse();
            echeckPreNoteSaleResponse actualEcheckPreNoteSaleResponse1 = actualCnpBatchResponse.nextEcheckPreNoteSaleResponse();
            echeckPreNoteSaleResponse actualEcheckPreNoteSaleResponse2 = actualCnpBatchResponse.nextEcheckPreNoteSaleResponse();
            echeckPreNoteSaleResponse nullEcheckPreNoteSalesResponse = actualCnpBatchResponse.nextEcheckPreNoteSaleResponse();

            Assert.AreEqual(123, actualEcheckPreNoteSaleResponse1.cnpTxnId);
            Assert.AreEqual(124, actualEcheckPreNoteSaleResponse2.cnpTxnId);
            Assert.IsNull(nullEcheckPreNoteSalesResponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName  ));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>()  , mockFileName));
        }

        [Test]
        public void TestEcheckPreNoteCredit()
        {
            echeckPreNoteCredit echeckPreNoteCredit = new echeckPreNoteCredit();
            echeckPreNoteCredit.orderId = "12345";
            echeckPreNoteCredit.orderSource = orderSourceType.ecommerce;
            echeckType echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.CorpSavings;
            echeck.accNum = "12345657890";
            echeck.routingNum = "123456789";
            echeck.checkNum = "123455";
            echeckPreNoteCredit.echeck = echeck;
            contact contact = new contact();
            contact.name = "Bob";
            contact.city = "lowell";
            contact.state = "MA";
            contact.email = "cnp.com";
            echeckPreNoteCredit.billToAddress = contact;

            var mockCnpResponse = new Mock<cnpResponse>();
            var mockCnpXmlSerializer = new Mock<cnpXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<echeckPreNoteCreditResponse xmlns='http://www.vantivcnp.com/schema'><cnpTxnId>123</cnpTxnId></echeckPreNoteCreditResponse>")
                .Returns("<echeckPreNoteCreditResponse xmlns='http://www.vantivcnp.com/schema'><cnpTxnId>124</cnpTxnId></echeckPreNoteCreditResponse>");

            batchResponse mockedCnpBatchResponse = new batchResponse();
            mockedCnpBatchResponse.setEcheckPreNoteCreditResponseReader(mockXmlReader.Object);

            mockCnpResponse.Setup(cnpResponse => cnpResponse.nextBatchResponse()).Returns(mockedCnpBatchResponse);
            cnpResponse mockedCnpResponse = mockCnpResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockCnpXmlSerializer.Setup(cnpXmlSerializer => cnpXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedCnpResponse);
            cnpXmlSerializer mockedCnpXmlSerializer = mockCnpXmlSerializer.Object;

            cnpFile mockedCnpFile = mockCnpFile.Object;

            cnp.setCommunication(mockedCommunications);
            cnp.setCnpXmlSerializer(mockedCnpXmlSerializer);
            cnp.setCnpFile(mockedCnpFile);
            cnp.setCnpTime(mockCnpTime.Object);

            batchRequest cnpBatchRequest = new batchRequest();
            cnpBatchRequest.setCnpFile(mockedCnpFile);
            cnpBatchRequest.setCnpTime(mockCnpTime.Object);
            cnpBatchRequest.addEcheckPreNoteCredit(echeckPreNoteCredit);
            cnpBatchRequest.addEcheckPreNoteCredit(echeckPreNoteCredit);
            cnp.addBatch(cnpBatchRequest);

            string batchFileName = cnp.sendToCnp();

            cnpResponse actualCnpResponse = cnp.receiveFromCnp(batchFileName);
            batchResponse actualCnpBatchResponse = actualCnpResponse.nextBatchResponse();
            echeckPreNoteCreditResponse actualEcheckPreNoteCreditResponse1 = actualCnpBatchResponse.nextEcheckPreNoteCreditResponse();
            echeckPreNoteCreditResponse actualEcheckPreNoteCreditResponse2 = actualCnpBatchResponse.nextEcheckPreNoteCreditResponse();
            echeckPreNoteCreditResponse nullEcheckPreNoteCreditsResponse = actualCnpBatchResponse.nextEcheckPreNoteCreditResponse();

            Assert.AreEqual(123, actualEcheckPreNoteCreditResponse1.cnpTxnId);
            Assert.AreEqual(124, actualEcheckPreNoteCreditResponse2.cnpTxnId);
            Assert.IsNull(nullEcheckPreNoteCreditsResponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName  ));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>()  , mockFileName));
        }

        [Test]
        public void TestGiftCardAuthReversal()
        {
            var giftCardAuthReversal = new giftCardAuthReversal
            {
                id = "1",
                reportGroup = "Planets",
                card = new giftCardCardType
                {
                    type = methodOfPaymentTypeEnum.GC,
                    number = "4100000000000002",
                    expDate = "1210"
                },
                originalRefCode = "123",
                originalAmount = 12345,
                originalTxnTime = DateTime.Now,
                originalSystemTraceId = 1234,
                originalSequenceNumber = "OneTime"
            };

            var mockCnpResponse = new Mock<cnpResponse>();
            var mockCnpXmlSerializer = new Mock<cnpXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns(@"<giftCardAuthReversalResponse xmlns='http://www.vantivcnp.com/schema'>
<cnpTxnId>522547723741503000</cnpTxnId>
<response>000</response>
<responseTime>2017-01-24T16:11:31</responseTime>
<message>Approved</message>
<postDate>2017-01-24</postDate>
<giftCardResponse>
<txnTime>2017-01-24T16:11:31</txnTime>
<systemTraceId>0</systemTraceId>
<sequenceNumber>12</sequenceNumber>
</giftCardResponse>
</giftCardAuthReversalResponse>")
                .Returns(@"<giftCardAuthReversalResponse xmlns='http://www.vantivcnp.com/schema'>
<cnpTxnId>522547723741503001</cnpTxnId>
<response>000</response>
<responseTime>2017-01-24T16:11:31</responseTime>
<message>Approved</message>
<postDate>2017-01-24</postDate>
<giftCardResponse>
<txnTime>2017-01-24T16:11:31</txnTime>
<systemTraceId>0</systemTraceId>
<sequenceNumber>12</sequenceNumber>
</giftCardResponse>
</giftCardAuthReversalResponse>");

            batchResponse mockCnpBatchResponse = new batchResponse();
            mockCnpBatchResponse.setGiftCardAuthReversalResponseReader(mockXmlReader.Object);

            mockCnpResponse.Setup(cnpResponse => cnpResponse.nextBatchResponse()).Returns(mockCnpBatchResponse);
            cnpResponse mockedCnpResponse = mockCnpResponse.Object;

            mockCnpXmlSerializer.Setup(cnpXmlSerializer => cnpXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedCnpResponse);

            Communications mockedCommunication = mockCommunications.Object;
            cnp.setCommunication(mockedCommunication);

            cnpXmlSerializer mockedCnpXmlSerializer = mockCnpXmlSerializer.Object;
            cnp.setCnpXmlSerializer(mockedCnpXmlSerializer);

            cnpFile mockedCnpFile = mockCnpFile.Object;
            cnp.setCnpFile(mockedCnpFile);

            cnp.setCnpTime(mockCnpTime.Object);

            batchRequest cnpBatchRequest = new batchRequest();
            cnpBatchRequest.setCnpFile(mockedCnpFile);
            cnpBatchRequest.setCnpTime(mockCnpTime.Object);
            cnpBatchRequest.addGiftCardAuthReversal(giftCardAuthReversal);
            cnpBatchRequest.addGiftCardAuthReversal(giftCardAuthReversal);
            cnp.addBatch(cnpBatchRequest);

            string batchFileName = cnp.sendToCnp();
            cnpResponse actualCnpResponse = cnp.receiveFromCnp(batchFileName);
            batchResponse actualCnpBatchResponse = actualCnpResponse.nextBatchResponse();

            Assert.AreSame(mockCnpBatchResponse, actualCnpBatchResponse);
            Assert.AreEqual(522547723741503000, actualCnpBatchResponse.nextGiftCardAuthReversalResponse().cnpTxnId);
            Assert.AreEqual(522547723741503001, actualCnpBatchResponse.nextGiftCardAuthReversalResponse().cnpTxnId);
            Assert.IsNull(actualCnpBatchResponse.nextGiftCardAuthReversalResponse());

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName  ));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>()  , mockFileName));
        }

        [Test]
        public void TestGiftCardCapture()
        {
            var giftCardCapture = new giftCardCapture
            {
                id = "1",
                reportGroup = "Planets",
                captureAmount = 123,
                card = new giftCardCardType
                {
                    type = methodOfPaymentTypeEnum.GC,
                    number = "4100000000000002",
                    expDate = "1210"
                },
                originalRefCode = "123",
                originalAmount = 12345,
                originalTxnTime = DateTime.Now
            };

            var mockCnpResponse = new Mock<cnpResponse>();
            var mockCnpXmlSerializer = new Mock<cnpXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns(@"<giftCardCaptureResponse xmlns='http://www.vantivcnp.com/schema'>
<cnpTxnId>522547723741503000</cnpTxnId>
<response>000</response>
<responseTime>2017-01-24T16:11:31</responseTime>
<message>Approved</message>
<postDate>2017-01-24</postDate>
<giftCardResponse>
<txnTime>2017-01-24T16:11:31</txnTime>
<systemTraceId>0</systemTraceId>
<sequenceNumber>12</sequenceNumber>
</giftCardResponse>
</giftCardCaptureResponse>")
                .Returns(@"<giftCardCaptureResponse xmlns='http://www.vantivcnp.com/schema'>
<cnpTxnId>522547723741503001</cnpTxnId>
<response>000</response>
<responseTime>2017-01-24T16:11:31</responseTime>
<message>Approved</message>
<postDate>2017-01-24</postDate>
<giftCardResponse>
<txnTime>2017-01-24T16:11:31</txnTime>
<systemTraceId>0</systemTraceId>
<sequenceNumber>12</sequenceNumber>
</giftCardResponse>
</giftCardCaptureResponse>");

            batchResponse mockCnpBatchResponse = new batchResponse();
            mockCnpBatchResponse.setGiftCardCaptureResponseReader(mockXmlReader.Object);

            mockCnpResponse.Setup(cnpResponse => cnpResponse.nextBatchResponse()).Returns(mockCnpBatchResponse);
            cnpResponse mockedCnpResponse = mockCnpResponse.Object;

            mockCnpXmlSerializer.Setup(cnpXmlSerializer => cnpXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedCnpResponse);

            Communications mockedCommunication = mockCommunications.Object;
            cnp.setCommunication(mockedCommunication);

            cnpXmlSerializer mockedCnpXmlSerializer = mockCnpXmlSerializer.Object;
            cnp.setCnpXmlSerializer(mockedCnpXmlSerializer);

            cnpFile mockedCnpFile = mockCnpFile.Object;
            cnp.setCnpFile(mockedCnpFile);

            cnp.setCnpTime(mockCnpTime.Object);

            batchRequest cnpBatchRequest = new batchRequest();
            cnpBatchRequest.setCnpFile(mockedCnpFile);
            cnpBatchRequest.setCnpTime(mockCnpTime.Object);
            cnpBatchRequest.addGiftCardCapture(giftCardCapture);
            cnpBatchRequest.addGiftCardCapture(giftCardCapture);
            cnp.addBatch(cnpBatchRequest);

            string batchFileName = cnp.sendToCnp();
            cnpResponse actualCnpResponse = cnp.receiveFromCnp(batchFileName);
            batchResponse actualCnpBatchResponse = actualCnpResponse.nextBatchResponse();

            Assert.AreSame(mockCnpBatchResponse, actualCnpBatchResponse);
            Assert.AreEqual(522547723741503000, actualCnpBatchResponse.nextGiftCardCaptureResponse().cnpTxnId);
            Assert.AreEqual(522547723741503001, actualCnpBatchResponse.nextGiftCardCaptureResponse().cnpTxnId);
            Assert.IsNull(actualCnpBatchResponse.nextGiftCardCaptureResponse());

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName  ));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>()  , mockFileName));
        }

        [Test]
        public void TestGiftCardCreditWithTxnId()
        {
            var giftCardCredit = new giftCardCredit
            {
                id = "1",
                reportGroup = "Planets",
                cnpTxnId = 123456000,
                creditAmount = 123,
                card = new giftCardCardType
                {
                    type = methodOfPaymentTypeEnum.GC,
                    number = "4100000000000002",
                    expDate = "1210"
                }
            };

            var mockCnpResponse = new Mock<cnpResponse>();
            var mockCnpXmlSerializer = new Mock<cnpXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns(@"<giftCardCreditResponse xmlns='http://www.vantivcnp.com/schema'>
<cnpTxnId>522547723741503000</cnpTxnId>
<response>000</response>
<responseTime>2017-01-24T16:11:31</responseTime>
<message>Approved</message>
<postDate>2017-01-24</postDate>
<giftCardResponse>
<txnTime>2017-01-24T16:11:31</txnTime>
<systemTraceId>0</systemTraceId>
<sequenceNumber>12</sequenceNumber>
</giftCardResponse>
</giftCardCreditResponse>")
                .Returns(@"<giftCardCreditResponse xmlns='http://www.vantivcnp.com/schema'>
<cnpTxnId>522547723741503001</cnpTxnId>
<response>000</response>
<responseTime>2017-01-24T16:11:31</responseTime>
<message>Approved</message>
<postDate>2017-01-24</postDate>
<giftCardResponse>
<txnTime>2017-01-24T16:11:31</txnTime>
<systemTraceId>0</systemTraceId>
<sequenceNumber>12</sequenceNumber>
</giftCardResponse>
</giftCardCreditResponse>");

            batchResponse mockCnpBatchResponse = new batchResponse();
            mockCnpBatchResponse.setGiftCardCreditResponseReader(mockXmlReader.Object);

            mockCnpResponse.Setup(cnpResponse => cnpResponse.nextBatchResponse()).Returns(mockCnpBatchResponse);
            cnpResponse mockedCnpResponse = mockCnpResponse.Object;

            mockCnpXmlSerializer.Setup(cnpXmlSerializer => cnpXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedCnpResponse);

            Communications mockedCommunication = mockCommunications.Object;
            cnp.setCommunication(mockedCommunication);

            cnpXmlSerializer mockedCnpXmlSerializer = mockCnpXmlSerializer.Object;
            cnp.setCnpXmlSerializer(mockedCnpXmlSerializer);

            cnpFile mockedCnpFile = mockCnpFile.Object;
            cnp.setCnpFile(mockedCnpFile);

            cnp.setCnpTime(mockCnpTime.Object);

            batchRequest cnpBatchRequest = new batchRequest();
            cnpBatchRequest.setCnpFile(mockedCnpFile);
            cnpBatchRequest.setCnpTime(mockCnpTime.Object);
            cnpBatchRequest.addGiftCardCredit(giftCardCredit);
            cnpBatchRequest.addGiftCardCredit(giftCardCredit);
            cnp.addBatch(cnpBatchRequest);

            string batchFileName = cnp.sendToCnp();
            cnpResponse actualCnpResponse = cnp.receiveFromCnp(batchFileName);
            batchResponse actualCnpBatchResponse = actualCnpResponse.nextBatchResponse();

            Assert.AreSame(mockCnpBatchResponse, actualCnpBatchResponse);
            Assert.AreEqual(522547723741503000, actualCnpBatchResponse.nextGiftCardCreditResponse().cnpTxnId);
            Assert.AreEqual(522547723741503001, actualCnpBatchResponse.nextGiftCardCreditResponse().cnpTxnId);
            Assert.IsNull(actualCnpBatchResponse.nextGiftCardCreditResponse());

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName  ));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>()  , mockFileName));
        }

        [Test]
        public void TestGiftCardCreditWithOrderId()
        {
            var giftCardCredit = new giftCardCredit
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "2111",
                creditAmount = 123,
                orderSource = orderSourceType.ecommerce,
                card = new giftCardCardType
                {
                    type = methodOfPaymentTypeEnum.GC,
                    number = "4100000000000002",
                    expDate = "1210"
                }
            };

            var mockCnpResponse = new Mock<cnpResponse>();
            var mockCnpXmlSerializer = new Mock<cnpXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns(@"<giftCardCreditResponse xmlns='http://www.vantivcnp.com/schema'>
<cnpTxnId>522547723741503000</cnpTxnId>
<orderId>2111</orderId>
<response>000</response>
<responseTime>2017-01-24T16:11:31</responseTime>
<message>Approved</message>
<postDate>2017-01-24</postDate>
<giftCardResponse>
<txnTime>2017-01-24T16:11:31</txnTime>
<systemTraceId>0</systemTraceId>
<sequenceNumber>12</sequenceNumber>
</giftCardResponse>
</giftCardCreditResponse>")
                .Returns(@"<giftCardCreditResponse xmlns='http://www.vantivcnp.com/schema'>
<cnpTxnId>522547723741503001</cnpTxnId>
<orderId>2111</orderId>
<response>000</response>
<responseTime>2017-01-24T16:11:31</responseTime>
<message>Approved</message>
<postDate>2017-01-24</postDate>
<giftCardResponse>
<txnTime>2017-01-24T16:11:31</txnTime>
<systemTraceId>0</systemTraceId>
<sequenceNumber>12</sequenceNumber>
</giftCardResponse>
</giftCardCreditResponse>");

            batchResponse mockCnpBatchResponse = new batchResponse();
            mockCnpBatchResponse.setGiftCardCreditResponseReader(mockXmlReader.Object);

            mockCnpResponse.Setup(cnpResponse => cnpResponse.nextBatchResponse()).Returns(mockCnpBatchResponse);
            cnpResponse mockedCnpResponse = mockCnpResponse.Object;

            mockCnpXmlSerializer.Setup(cnpXmlSerializer => cnpXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedCnpResponse);

            Communications mockedCommunication = mockCommunications.Object;
            cnp.setCommunication(mockedCommunication);

            cnpXmlSerializer mockedCnpXmlSerializer = mockCnpXmlSerializer.Object;
            cnp.setCnpXmlSerializer(mockedCnpXmlSerializer);

            cnpFile mockedCnpFile = mockCnpFile.Object;
            cnp.setCnpFile(mockedCnpFile);

            cnp.setCnpTime(mockCnpTime.Object);

            batchRequest cnpBatchRequest = new batchRequest();
            cnpBatchRequest.setCnpFile(mockedCnpFile);
            cnpBatchRequest.setCnpTime(mockCnpTime.Object);
            cnpBatchRequest.addGiftCardCredit(giftCardCredit);
            cnpBatchRequest.addGiftCardCredit(giftCardCredit);
            cnp.addBatch(cnpBatchRequest);

            string batchFileName = cnp.sendToCnp();
            cnpResponse actualCnpResponse = cnp.receiveFromCnp(batchFileName);
            batchResponse actualCnpBatchResponse = actualCnpResponse.nextBatchResponse();

            Assert.AreSame(mockCnpBatchResponse, actualCnpBatchResponse);
            Assert.AreEqual(522547723741503000, actualCnpBatchResponse.nextGiftCardCreditResponse().cnpTxnId);
            Assert.AreEqual(522547723741503001, actualCnpBatchResponse.nextGiftCardCreditResponse().cnpTxnId);
            Assert.IsNull(actualCnpBatchResponse.nextGiftCardCreditResponse());

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName  ));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>()  , mockFileName));
        }

        [Test]
        public void TestNullNumAccountUpdates() {
            var xmlReader = XmlReader.Create(new StringReader("<batchResponse id=\"1\" cnpBatchId=\"2\" merchantId=\"3\"></batchResponse>"));
            xmlReader.Read();
               
            batchResponse cnpBatchResponse = new batchResponse();
            try {
                cnpBatchResponse.readXml(xmlReader, "nullFile");
            }
            catch (FileNotFoundException e) {
                // Other XMLReaders can't generate since the file doesn't exist. Everything else should be initialized though.
            }

            Assert.AreEqual(cnpBatchResponse.id, "1");
            Assert.AreEqual(cnpBatchResponse.cnpBatchId, 2);
            Assert.AreEqual(cnpBatchResponse.merchantId, "3");
            Assert.AreEqual(cnpBatchResponse.numAccountUpdates, null);
        }
        
        [Test]
        public void TestNumAccountUpdates() {
            var xmlReader = XmlReader.Create(new StringReader("<batchResponse id=\"1\" cnpBatchId=\"2\" merchantId=\"3\" numAccountUpdates=\"4\"></batchResponse>"));
            xmlReader.Read();
               
            batchResponse cnpBatchResponse = new batchResponse();
            try {
                cnpBatchResponse.readXml(xmlReader, "nullFile");
            }
            catch (FileNotFoundException e) {
                // Other XMLReaders can't generate since the file doesn't exist. Everything else should be initialized though.
            }

            Assert.AreEqual(cnpBatchResponse.id, "1");
            Assert.AreEqual(cnpBatchResponse.cnpBatchId, 2);
            Assert.AreEqual(cnpBatchResponse.merchantId, "3");
            Assert.AreEqual(cnpBatchResponse.numAccountUpdates, 4);
        }
        
        [Test]
        public void TestAccountUpdateSourceRealtime() {
            var xmlReader = XmlReader.Create(new StringReader("<saleResponse reportGroup=\"Merch01ReportGrp\" xmlns=\"http://www.vantivcnp.com/schema\"><cnpTxnId>123</cnpTxnId><orderId>MERCH01-0002</orderId><response>000</response><responseTime>2010-04-11T15:44:26</responseTime><message>Approved</message><accountUpdater><accountUpdateSource>R</accountUpdateSource></accountUpdater></saleResponse>"));
            xmlReader.Read();
               
            batchResponse mockCnpBatchResponse = new batchResponse();
            mockCnpBatchResponse.setSaleResponseReader(xmlReader);
            
            var mockCnpResponse = new Mock<cnpResponse>();
            var mockCnpXmlSerializer = new Mock<cnpXmlSerializer>();
            
            mockCnpResponse.Setup(cnpResponse => cnpResponse.nextBatchResponse()).Returns(mockCnpBatchResponse);
            cnpResponse mockedCnpResponse = mockCnpResponse.Object;

            mockCnpXmlSerializer.Setup(cnpXmlSerializer => cnpXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedCnpResponse);

            Communications mockedCommunication = mockCommunications.Object;
            cnp.setCommunication(mockedCommunication);

            cnpXmlSerializer mockedCnpXmlSerializer = mockCnpXmlSerializer.Object;
            cnp.setCnpXmlSerializer(mockedCnpXmlSerializer);

            cnpFile mockedCnpFile = mockCnpFile.Object;
            cnp.setCnpFile(mockedCnpFile);
            cnp.setCnpTime(mockCnpTime.Object);
            
            batchRequest cnpBatchRequest = new batchRequest();
            cnpBatchRequest.setCnpFile(mockedCnpFile);
            cnpBatchRequest.setCnpTime(mockCnpTime.Object);
            cnp.addBatch(cnpBatchRequest);
            
            string batchFileName = cnp.sendToCnp();
            cnpResponse actualCnpResponse = cnp.receiveFromCnp(batchFileName);
            batchResponse actualCnpBatchResponse = actualCnpResponse.nextBatchResponse();
            saleResponse saleResponse = actualCnpBatchResponse.nextSaleResponse();
            
            Assert.AreEqual(123,saleResponse.cnpTxnId);
            Assert.AreEqual("000", saleResponse.response);
            Assert.AreEqual(accountUpdateSourceType.R, saleResponse.accountUpdater.accountUpdateSource);
        }

        [Test]
        public void TestAccountUpdateSourceNotRealtime() {
            var xmlReader = XmlReader.Create(new StringReader(
                "<saleResponse reportGroup=\"Merch01ReportGrp\" xmlns=\"http://www.vantivcnp.com/schema\"><cnpTxnId>123</cnpTxnId><orderId>MERCH01-0002</orderId><response>000</response><responseTime>2010-04-11T15:44:26</responseTime><message>Approved</message><accountUpdater><accountUpdateSource>N</accountUpdateSource></accountUpdater></saleResponse>"));
            xmlReader.Read();

            batchResponse mockCnpBatchResponse = new batchResponse();
            mockCnpBatchResponse.setSaleResponseReader(xmlReader);

            var mockCnpResponse = new Mock<cnpResponse>();
            var mockCnpXmlSerializer = new Mock<cnpXmlSerializer>();

            mockCnpResponse.Setup(cnpResponse => cnpResponse.nextBatchResponse()).Returns(mockCnpBatchResponse);
            cnpResponse mockedCnpResponse = mockCnpResponse.Object;

            mockCnpXmlSerializer
                .Setup(cnpXmlSerializer => cnpXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>()))
                .Returns(mockedCnpResponse);

            Communications mockedCommunication = mockCommunications.Object;
            cnp.setCommunication(mockedCommunication);

            cnpXmlSerializer mockedCnpXmlSerializer = mockCnpXmlSerializer.Object;
            cnp.setCnpXmlSerializer(mockedCnpXmlSerializer);

            cnpFile mockedCnpFile = mockCnpFile.Object;
            cnp.setCnpFile(mockedCnpFile);
            cnp.setCnpTime(mockCnpTime.Object);

            batchRequest cnpBatchRequest = new batchRequest();
            cnpBatchRequest.setCnpFile(mockedCnpFile);
            cnpBatchRequest.setCnpTime(mockCnpTime.Object);
            cnp.addBatch(cnpBatchRequest);

            string batchFileName = cnp.sendToCnp();
            cnpResponse actualCnpResponse = cnp.receiveFromCnp(batchFileName);
            batchResponse actualCnpBatchResponse = actualCnpResponse.nextBatchResponse();
            saleResponse saleResponse = actualCnpBatchResponse.nextSaleResponse();

            Assert.AreEqual(123, saleResponse.cnpTxnId);
            Assert.AreEqual("000", saleResponse.response);
            Assert.AreEqual(accountUpdateSourceType.N, saleResponse.accountUpdater.accountUpdateSource);
        }

        [Test]
        public void TestAccountUpdateSourceNull() {
            var xmlReader = XmlReader.Create(new StringReader("<saleResponse reportGroup=\"Merch01ReportGrp\" xmlns=\"http://www.vantivcnp.com/schema\"><cnpTxnId>123</cnpTxnId><orderId>MERCH01-0002</orderId><response>000</response><responseTime>2010-04-11T15:44:26</responseTime><message>Approved</message><accountUpdater></accountUpdater></saleResponse>"));
            xmlReader.Read();
               
            batchResponse mockCnpBatchResponse = new batchResponse();
            mockCnpBatchResponse.setSaleResponseReader(xmlReader);
            
            var mockCnpResponse = new Mock<cnpResponse>();
            var mockCnpXmlSerializer = new Mock<cnpXmlSerializer>();
            
            mockCnpResponse.Setup(cnpResponse => cnpResponse.nextBatchResponse()).Returns(mockCnpBatchResponse);
            cnpResponse mockedCnpResponse = mockCnpResponse.Object;

            mockCnpXmlSerializer.Setup(cnpXmlSerializer => cnpXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedCnpResponse);

            Communications mockedCommunication = mockCommunications.Object;
            cnp.setCommunication(mockedCommunication);

            cnpXmlSerializer mockedCnpXmlSerializer = mockCnpXmlSerializer.Object;
            cnp.setCnpXmlSerializer(mockedCnpXmlSerializer);

            cnpFile mockedCnpFile = mockCnpFile.Object;
            cnp.setCnpFile(mockedCnpFile);
            cnp.setCnpTime(mockCnpTime.Object);
            
            batchRequest cnpBatchRequest = new batchRequest();
            cnpBatchRequest.setCnpFile(mockedCnpFile);
            cnpBatchRequest.setCnpTime(mockCnpTime.Object);
            cnp.addBatch(cnpBatchRequest);
            
            string batchFileName = cnp.sendToCnp();
            cnpResponse actualCnpResponse = cnp.receiveFromCnp(batchFileName);
            batchResponse actualCnpBatchResponse = actualCnpResponse.nextBatchResponse();
            saleResponse saleResponse = actualCnpBatchResponse.nextSaleResponse();
            
            Assert.AreEqual(123,saleResponse.cnpTxnId);
            Assert.AreEqual("000", saleResponse.response);
            Assert.AreEqual(null, saleResponse.accountUpdater.accountUpdateSource);
        }
    }
}
