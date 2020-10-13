using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Moq;
using System.Text.RegularExpressions;


namespace Cnp.Sdk.Test.Unit
{
    [TestFixture]
    class TestBatchRequest
    {
        private batchRequest batchRequest;
        private const string timeFormat = "MM-dd-yyyy_HH-mm-ss-ffff_";
        private const string timeRegex = "[0-1][0-9]-[0-3][0-9]-[0-9]{4}_[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{4}_";
        private const string batchNameRegex = timeRegex + "[A-Z]{8}";
        private const string mockFileName = "TheRainbow.xml";
        private const string mockFilePath = "C:\\Somewhere\\\\Over\\\\" + mockFileName;

        private Mock<cnpFile> mockCnpFile;
        private Mock<cnpTime> mockCnpTime;

        [OneTimeSetUp]
        public void setUp()
        {
            mockCnpFile = new Mock<cnpFile>();
            mockCnpTime = new Mock<cnpTime>();

            mockCnpFile.Setup(cnpFile => cnpFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), mockCnpTime.Object)).Returns(mockFilePath);
            mockCnpFile.Setup(cnpFile => cnpFile.AppendLineToFile(mockFilePath, It.IsAny<string>())).Returns(mockFilePath);
        }

        [SetUp]
        public void BeforeTestSetup()
        {
            batchRequest = new batchRequest();
            batchRequest.setCnpFile(mockCnpFile.Object);
            batchRequest.setCnpTime(mockCnpTime.Object);
        }

        [Test]
        public void TestBatchRequestContainsMerchantSdkAttribute()
        {
            var mockConfig = new Dictionary<string, string>();

            mockConfig["merchantId"] = "01234";
            mockConfig["requestDirectory"] = "C:\\MockRequests";
            mockConfig["responseDirectory"] = "C:\\MockResponses";

            batchRequest = new batchRequest(mockConfig);

            var actual = batchRequest.generateXmlHeader();
            var expected = @"
<batchRequest id=""""
merchantSdk=""DotNet;" + CnpVersion.CurrentCNPSDKVersion + @"""
merchantId=""01234"">
";
            
            Assert.AreEqual(Regex.Replace(expected, @"\s+", string.Empty), Regex.Replace(actual, @"\s+", string.Empty));
        }

        [Test]
        public void TestInitialization()
        {
            var mockConfig = new Dictionary<string, string>();

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
            mockConfig["requestDirectory"] = Path.Combine(Path.GetTempPath(),"MockRequests");
            mockConfig["responseDirectory"] = Path.Combine(Path.GetTempPath(),"MockResponses");

            batchRequest = new batchRequest(mockConfig);
            Assert.AreEqual(Path.Combine(Path.GetTempPath(),"MockRequests","Requests") + Path.DirectorySeparatorChar, batchRequest.getRequestDirectory());
            Assert.AreEqual(Path.Combine(Path.GetTempPath(),"MockResponses","Responses") + Path.DirectorySeparatorChar, batchRequest.getResponseDirectory());

            Assert.NotNull(batchRequest.getCnpTime());
            Assert.NotNull(batchRequest.getCnpFile());
        }

        [Test]
        public void TestAddAuthorization()
        {
            var authorization = new authorization();
            authorization.reportGroup = "Planets";
            authorization.orderId = "12344";
            authorization.amount = 106;
            authorization.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000002";
            card.expDate = "1210";
            authorization.card = card;

            batchRequest.addAuthorization(authorization);

            Assert.AreEqual(1, batchRequest.getNumAuthorization());
            Assert.AreEqual(authorization.amount, batchRequest.getSumOfAuthorization());

            mockCnpFile.Verify(cnpFile => cnpFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), mockCnpTime.Object));
            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, authorization.Serialize()));
        }

        [Test]
        public void TestAddAccountUpdate()
        {
            var accountUpdate = new accountUpdate();
            accountUpdate.reportGroup = "Planets";
            accountUpdate.orderId = "12344";
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000002";
            card.expDate = "1210";
            accountUpdate.card = card;

            batchRequest.addAccountUpdate(accountUpdate);

            Assert.AreEqual(1, batchRequest.getNumAccountUpdates());

            mockCnpFile.Verify(cnpFile => cnpFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), mockCnpTime.Object));
            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, accountUpdate.Serialize()));
        }

        [Test]
        public void TestAuthReversal()
        {
            var authreversal = new authReversal();
            authreversal.cnpTxnId = 12345678000;
            authreversal.amount = 106;
            authreversal.payPalNotes = "Notes";

            batchRequest.addAuthReversal(authreversal);

            Assert.AreEqual(1, batchRequest.getNumAuthReversal());
            Assert.AreEqual(authreversal.amount, batchRequest.getSumOfAuthReversal());

            mockCnpFile.Verify(cnpFile => cnpFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), mockCnpTime.Object));
            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, authreversal.Serialize()));
        }
        
        [Test]
        public void TestTransactionReversal()
        {
            var transactionReversal = new transactionReversal();
            transactionReversal.cnpTxnId = 12345678000;
            transactionReversal.amount = 106;

            batchRequest.addTransactionReversal(transactionReversal);

            Assert.AreEqual(1, batchRequest.getNumTransactionReversal());
            Assert.AreEqual(transactionReversal.amount, batchRequest.getSumOfTransactionReversal());

            mockCnpFile.Verify(cnpFile => cnpFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), mockCnpTime.Object));
            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, transactionReversal.Serialize()));
        }

        [Test]
        public void TestCapture()
        {
            var capture = new capture();
            capture.cnpTxnId = 12345678000;
            capture.amount = 106;

            batchRequest.addCapture(capture);

            Assert.AreEqual(1, batchRequest.getNumCapture());
            Assert.AreEqual(capture.amount, batchRequest.getSumOfCapture());

            mockCnpFile.Verify(cnpFile => cnpFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), mockCnpTime.Object));
            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, capture.Serialize()));
        }

        [Test]
        public void TestCaptureGivenAuth()
        {
            var capturegivenauth = new captureGivenAuth();
            capturegivenauth.orderId = "12344";
            capturegivenauth.amount = 106;
            var authinfo = new authInformation();
            authinfo.authDate = new DateTime(2002, 10, 9);
            authinfo.authCode = "543216";
            authinfo.authAmount = 12345;
            capturegivenauth.authInformation = authinfo;
            capturegivenauth.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000001";
            card.expDate = "1210";
            capturegivenauth.card = card;

            batchRequest.addCaptureGivenAuth(capturegivenauth);

            Assert.AreEqual(1, batchRequest.getNumCaptureGivenAuth());
            Assert.AreEqual(capturegivenauth.amount, batchRequest.getSumOfCaptureGivenAuth());

            mockCnpFile.Verify(cnpFile => cnpFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), mockCnpTime.Object));
            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, capturegivenauth.Serialize()));
        }

        [Test]
        public void TestCredit()
        {
            var credit = new credit();
            credit.orderId = "12344";
            credit.amount = 106;
            credit.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000001";
            card.expDate = "1210";
            credit.card = card;

            batchRequest.addCredit(credit);

            Assert.AreEqual(1, batchRequest.getNumCredit());
            Assert.AreEqual(credit.amount, batchRequest.getSumOfCredit());

            mockCnpFile.Verify(cnpFile => cnpFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), mockCnpTime.Object));
            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, credit.Serialize()));
        }

        [Test]
        public void TestEcheckCredit()
        {
            var echeckcredit = new echeckCredit();
            echeckcredit.amount = 12;
            echeckcredit.cnpTxnId = 123456789101112;

            batchRequest.addEcheckCredit(echeckcredit);

            Assert.AreEqual(1, batchRequest.getNumEcheckCredit());
            Assert.AreEqual(echeckcredit.amount, batchRequest.getSumOfEcheckCredit());

            mockCnpFile.Verify(cnpFile => cnpFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), mockCnpTime.Object));
            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, echeckcredit.Serialize()));
        }

        [Test]
        public void TestEcheckRedeposit()
        {
            var echeckredeposit = new echeckRedeposit();
            echeckredeposit.cnpTxnId = 123456;

            batchRequest.addEcheckRedeposit(echeckredeposit);

            Assert.AreEqual(1, batchRequest.getNumEcheckRedeposit());

            mockCnpFile.Verify(cnpFile => cnpFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), mockCnpTime.Object));
            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, echeckredeposit.Serialize()));
        }

        [Test]
        public void TestEcheckSale()
        {
            var echecksale = new echeckSale();
            echecksale.orderId = "12345";
            echecksale.amount = 123456;
            echecksale.orderSource = orderSourceType.ecommerce;
            var echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "12345657890";
            echeck.routingNum = "123456789";
            echeck.checkNum = "123455";
            echecksale.echeck = echeck;
            var contact = new contact();
            contact.name = "Bob";
            contact.city = "lowell";
            contact.state = "MA";
            contact.email = "cnp.com";
            echecksale.billToAddress = contact;

            batchRequest.addEcheckSale(echecksale);

            Assert.AreEqual(1, batchRequest.getNumEcheckSale());
            Assert.AreEqual(echecksale.amount, batchRequest.getSumOfEcheckSale());

            mockCnpFile.Verify(cnpFile => cnpFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), mockCnpTime.Object));
            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, echecksale.Serialize()));
        }

        [Test]
        public void TestEcheckVerification()
        {
            var echeckverification = new echeckVerification();
            echeckverification.orderId = "12345";
            echeckverification.amount = 123456;
            echeckverification.orderSource = orderSourceType.ecommerce;
            var echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "12345657890";
            echeck.routingNum = "123456789";
            echeck.checkNum = "123455";
            echeckverification.echeck = echeck;
            var contact = new contact();
            contact.name = "Bob";
            contact.city = "lowell";
            contact.state = "MA";
            contact.email = "cnp.com";
            echeckverification.billToAddress = contact;

            batchRequest.addEcheckVerification(echeckverification);

            Assert.AreEqual(1, batchRequest.getNumEcheckVerification());
            Assert.AreEqual(echeckverification.amount, batchRequest.getSumOfEcheckVerification());

            mockCnpFile.Verify(cnpFile => cnpFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), mockCnpTime.Object));
            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, echeckverification.Serialize()));
        }

        [Test]
        public void TestForceCapture()
        {
            var forcecapture = new forceCapture();
            forcecapture.orderId = "12344";
            forcecapture.amount = 106;
            forcecapture.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000001";
            card.expDate = "1210";
            forcecapture.card = card;

            batchRequest.addForceCapture(forcecapture);

            Assert.AreEqual(1, batchRequest.getNumForceCapture());
            Assert.AreEqual(forcecapture.amount, batchRequest.getSumOfForceCapture());

            mockCnpFile.Verify(cnpFile => cnpFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), mockCnpTime.Object));
            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, forcecapture.Serialize()));
        }

        [Test]
        public void TestSale()
        {
            var sale = new sale();
            sale.orderId = "12344";
            sale.amount = 106;
            sale.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000002";
            card.expDate = "1210";
            sale.card = card;

            batchRequest.addSale(sale);

            Assert.AreEqual(1, batchRequest.getNumSale());
            Assert.AreEqual(sale.amount, batchRequest.getSumOfSale());

            mockCnpFile.Verify(cnpFile => cnpFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), mockCnpTime.Object));
            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, sale.Serialize()));
        }

        [Test]
        public void TestToken()
        {
            var token = new registerTokenRequestType();
            token.orderId = "12344";
            token.accountNumber = "1233456789103801";

            batchRequest.addRegisterTokenRequest(token);

            Assert.AreEqual(1, batchRequest.getNumRegisterTokenRequest());

            mockCnpFile.Verify(cnpFile => cnpFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), mockCnpTime.Object));
            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, token.Serialize()));
        }

        [Test]
        public void TestUpdateCardValidationNumOnToken()
        {
            var updateCardValidationNumOnToken = new updateCardValidationNumOnToken();
            updateCardValidationNumOnToken.orderId = "12344";
            updateCardValidationNumOnToken.cnpToken = "123";

            batchRequest.addUpdateCardValidationNumOnToken(updateCardValidationNumOnToken);

            Assert.AreEqual(1, batchRequest.getNumUpdateCardValidationNumOnToken());

            mockCnpFile.Verify(cnpFile => cnpFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), mockCnpTime.Object));
            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, updateCardValidationNumOnToken.Serialize()));
        }

        [Test]
        public void TestUpdateSubscription()
        {
            var update = new updateSubscription();
            update.billingDate = new DateTime(2002, 10, 9);
            var billToAddress = new contact();
            billToAddress.name = "Greg Dake";
            billToAddress.city = "Lowell";
            billToAddress.state = "MA";
            billToAddress.email = "sdksupport@cnp.com";
            update.billToAddress = billToAddress;
            var card = new cardType();
            card.number = "4100000000000001";
            card.expDate = "1215";
            card.type = methodOfPaymentTypeEnum.VI;
            update.card = card;
            update.planCode = "abcdefg";
            update.subscriptionId = 12345;

            batchRequest.addUpdateSubscription(update);

            Assert.AreEqual(1, batchRequest.getNumUpdateSubscriptions());

            mockCnpFile.Verify(cnpFile => cnpFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), mockCnpTime.Object));
            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, update.Serialize()));
        }

        [Test]
        public void TestCreatePlan()
        {
            var createPlan = new createPlan();

            batchRequest.addCreatePlan(createPlan);

            Assert.AreEqual(1, batchRequest.getNumCreatePlans());

            mockCnpFile.Verify(cnpFile => cnpFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), mockCnpTime.Object));
            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, createPlan.Serialize()));
        }

        [Test]
        public void TestUpdatePlan()
        {
            var updatePlan = new updatePlan();

            batchRequest.addUpdatePlan(updatePlan);

            Assert.AreEqual(1, batchRequest.getNumUpdatePlans());

            mockCnpFile.Verify(cnpFile => cnpFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), mockCnpTime.Object));
            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, updatePlan.Serialize()));
        }

        [Test]
        public void TestActivate()
        {
            var activate = new activate();
            activate.amount = 500;
            activate.orderSource = orderSourceType.ecommerce;
            activate.card = new giftCardCardType();

            batchRequest.addActivate(activate);

            Assert.AreEqual(1, batchRequest.getNumActivates());
            Assert.AreEqual(500, batchRequest.getActivateAmount());

            mockCnpFile.Verify(cnpFile => cnpFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), mockCnpTime.Object));
            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, activate.Serialize()));
        }

        [Test]
        public void TestDeactivate()
        {
            var deactivate = new deactivate();
            deactivate.orderSource = orderSourceType.ecommerce;
            deactivate.card = new giftCardCardType();

            batchRequest.addDeactivate(deactivate);

            Assert.AreEqual(1, batchRequest.getNumDeactivates());

            mockCnpFile.Verify(cnpFile => cnpFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), mockCnpTime.Object));
            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, deactivate.Serialize()));
        }

        [Test]
        public void TestLoad()
        {
            var load = new load();
            load.amount = 600;
            load.orderSource = orderSourceType.ecommerce;
            load.card = new giftCardCardType();

            batchRequest.addLoad(load);

            Assert.AreEqual(1, batchRequest.getNumLoads());
            Assert.AreEqual(600, batchRequest.getLoadAmount());

            mockCnpFile.Verify(cnpFile => cnpFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), mockCnpTime.Object));
            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, load.Serialize()));
        }

        [Test]
        public void TestUnload()
        {
            var unload = new unload();
            unload.amount = 700;
            unload.orderSource = orderSourceType.ecommerce;
            unload.card = new giftCardCardType();

            batchRequest.addUnload(unload);

            Assert.AreEqual(1, batchRequest.getNumUnloads());
            Assert.AreEqual(700, batchRequest.getUnloadAmount());

            mockCnpFile.Verify(cnpFile => cnpFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), mockCnpTime.Object));
            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, unload.Serialize()));
        }

        [Test]
        public void TestBalanceInquiry()
        {
            var balanceInquiry = new balanceInquiry();
            balanceInquiry.orderSource = orderSourceType.ecommerce;
            balanceInquiry.card = new giftCardCardType();

            batchRequest.addBalanceInquiry(balanceInquiry);

            Assert.AreEqual(1, batchRequest.getNumBalanceInquiries());

            mockCnpFile.Verify(cnpFile => cnpFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), mockCnpTime.Object));
            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, balanceInquiry.Serialize()));
        }

        [Test]
        public void TestCancelSubscription()
        {
            var cancel = new cancelSubscription();
            cancel.subscriptionId = 12345;

            batchRequest.addCancelSubscription(cancel);

            Assert.AreEqual(1, batchRequest.getNumCancelSubscriptions());

            mockCnpFile.Verify(cnpFile => cnpFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), mockCnpTime.Object));
            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, cancel.Serialize()));
        }

        [Test]
        public void TestAddEcheckPreNoteSale()
        {
            var echeckPreNoteSale = new echeckPreNoteSale();
            echeckPreNoteSale.orderId = "12345";
            echeckPreNoteSale.orderSource = orderSourceType.ecommerce;
            var echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "12345657890";
            echeck.routingNum = "123456789";
            echeck.checkNum = "123455";
            echeckPreNoteSale.echeck = echeck;
            var contact = new contact();
            contact.name = "Bob";
            contact.city = "lowell";
            contact.state = "MA";
            contact.email = "cnp.com";
            echeckPreNoteSale.billToAddress = contact;

            batchRequest.addEcheckPreNoteSale(echeckPreNoteSale);

            Assert.AreEqual(1, batchRequest.getNumEcheckPreNoteSale());

            mockCnpFile.Verify(cnpFile => cnpFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), mockCnpTime.Object));
            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, echeckPreNoteSale.Serialize()));
        }

        [Test]
        public void TestAddEcheckPreNoteCredit()
        {
            var echeckPreNoteCredit = new echeckPreNoteCredit();
            echeckPreNoteCredit.orderId = "12345";
            echeckPreNoteCredit.orderSource = orderSourceType.ecommerce;
            var echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "12345657890";
            echeck.routingNum = "123456789";
            echeck.checkNum = "123455";
            echeckPreNoteCredit.echeck = echeck;
            var contact = new contact();
            contact.name = "Bob";
            contact.city = "lowell";
            contact.state = "MA";
            contact.email = "cnp.com";
            echeckPreNoteCredit.billToAddress = contact;

            batchRequest.addEcheckPreNoteCredit(echeckPreNoteCredit);

            Assert.AreEqual(1, batchRequest.getNumEcheckPreNoteCredit());

            mockCnpFile.Verify(cnpFile => cnpFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), mockCnpTime.Object));
            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, echeckPreNoteCredit.Serialize()));
        }

        [Test]
        public void TestAddSubmerchantCredit()
        {
            var submerchantCredit = new submerchantCredit();
            submerchantCredit.fundingSubmerchantId = "123456";
            submerchantCredit.submerchantName = "merchant";
            submerchantCredit.fundsTransferId = "123467";
            submerchantCredit.amount = 106L;
            var echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "12345657890";
            echeck.routingNum = "123456789";
            echeck.checkNum = "123455";
            submerchantCredit.accountInfo = echeck;
            submerchantCredit.customIdentifier = "abc123";

            batchRequest.addSubmerchantCredit(submerchantCredit);

            Assert.AreEqual(1, batchRequest.getNumSubmerchantCredit());
            Assert.AreEqual(106L, batchRequest.getSubmerchantCreditAmount());
            Assert.AreEqual("\r\n<submerchantCredit reportGroup=\"Default Report Group\">\r\n<fundingSubmerchantId>123456</fundingSubmerchantId>\r\n<submerchantName>merchant</submerchantName>\r\n<fundsTransferId>123467</fundsTransferId>\r\n<amount>106</amount>\r\n<accountInfo>\r\n<accType>Checking</accType>\r\n<accNum>12345657890</accNum>\r\n<routingNum>123456789</routingNum>\r\n<checkNum>123455</checkNum></accountInfo>\r\n<customIdentifier>abc123</customIdentifier>\r\n</submerchantCredit>",
                submerchantCredit.Serialize());


            mockCnpFile.Verify(cnpFile => cnpFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), mockCnpTime.Object));
            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, submerchantCredit.Serialize()));
        }

        [Test]
        public void TestAddPayFacCredit()
        {
            var payFacCredit = new payFacCredit();
            payFacCredit.fundingSubmerchantId = "123456";
            payFacCredit.fundsTransferId = "123467";
            payFacCredit.amount = 107L;

            batchRequest.addPayFacCredit(payFacCredit);

            Assert.AreEqual(1, batchRequest.getNumPayFacCredit());
            Assert.AreEqual(107L, batchRequest.getPayFacCreditAmount());
            Assert.AreEqual("\r\n<payFacCredit reportGroup=\"Default Report Group\">\r\n<fundingSubmerchantId>123456</fundingSubmerchantId>\r\n<fundsTransferId>123467</fundsTransferId>\r\n<amount>107</amount>\r\n</payFacCredit>",
                payFacCredit.Serialize());

            mockCnpFile.Verify(cnpFile => cnpFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), mockCnpTime.Object));
            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, payFacCredit.Serialize()));
        }

        [Test]
        public void TestAddReserveCredit()
        {
            var reserveCredit = new reserveCredit();
            reserveCredit.fundingSubmerchantId = "123456";
            reserveCredit.fundsTransferId = "123467";
            reserveCredit.amount = 107L;

            batchRequest.addReserveCredit(reserveCredit);

            Assert.AreEqual(1, batchRequest.getNumReserveCredit());
            Assert.AreEqual(107L, batchRequest.getReserveCreditAmount());
            Assert.AreEqual("\r\n<reserveCredit reportGroup=\"Default Report Group\">\r\n<fundingSubmerchantId>123456</fundingSubmerchantId>\r\n<fundsTransferId>123467</fundsTransferId>\r\n<amount>107</amount>\r\n</reserveCredit>",
                reserveCredit.Serialize());

            mockCnpFile.Verify(cnpFile => cnpFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), mockCnpTime.Object));
            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, reserveCredit.Serialize()));
        }

        [Test]
        public void TestAddVendorCredit()
        {
            var vendorCredit = new vendorCredit();
            vendorCredit.fundingSubmerchantId = "123456";
            vendorCredit.vendorName = "merchant";
            vendorCredit.fundsTransferId = "123467";
            vendorCredit.amount = 106L;
            var echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "12345657890";
            echeck.routingNum = "123456789";
            echeck.checkNum = "123455";
            vendorCredit.accountInfo = echeck;

            batchRequest.addVendorCredit(vendorCredit);

            Assert.AreEqual(1, batchRequest.getNumVendorCredit());
            Assert.AreEqual(106L, batchRequest.getVendorCreditAmount());
            Assert.AreEqual("\r\n<vendorCredit reportGroup=\"Default Report Group\">\r\n<fundingSubmerchantId>123456</fundingSubmerchantId>\r\n<vendorName>merchant</vendorName>\r\n<fundsTransferId>123467</fundsTransferId>\r\n<amount>106</amount>\r\n<accountInfo>\r\n<accType>Checking</accType>\r\n<accNum>12345657890</accNum>\r\n<routingNum>123456789</routingNum>\r\n<checkNum>123455</checkNum></accountInfo>\r\n</vendorCredit>",
               vendorCredit.Serialize());

            mockCnpFile.Verify(cnpFile => cnpFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), mockCnpTime.Object));
            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, vendorCredit.Serialize()));
        }
        
        [Test]
        public void TestAddPhysicalCheckCredit()
        {
            var physicalCheckCredit = new physicalCheckCredit();
            physicalCheckCredit.fundingSubmerchantId = "123456";
            physicalCheckCredit.fundsTransferId = "123467";
            physicalCheckCredit.amount = 107L;

            batchRequest.addPhysicalCheckCredit(physicalCheckCredit);

            Assert.AreEqual(1, batchRequest.getNumPhysicalCheckCredit());
            Assert.AreEqual(107L, batchRequest.getPhysicalCheckCreditAmount());
            Assert.AreEqual("\r\n<physicalCheckCredit reportGroup=\"Default Report Group\">\r\n<fundingSubmerchantId>123456</fundingSubmerchantId>\r\n<fundsTransferId>123467</fundsTransferId>\r\n<amount>107</amount>\r\n</physicalCheckCredit>",
                physicalCheckCredit.Serialize());

            mockCnpFile.Verify(cnpFile => cnpFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), mockCnpTime.Object));
            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, physicalCheckCredit.Serialize()));
        }

        [Test]
        public void TestAddSubmerchantDebit()
        {
            var submerchantDebit = new submerchantDebit();
            submerchantDebit.fundingSubmerchantId = "123456";
            submerchantDebit.submerchantName = "merchant";
            submerchantDebit.fundsTransferId = "123467";
            submerchantDebit.amount = 106L;
            var echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "12345657890";
            echeck.routingNum = "123456789";
            echeck.checkNum = "123455";
            submerchantDebit.accountInfo = echeck;
            submerchantDebit.customIdentifier = "abc123";

            batchRequest.addSubmerchantDebit(submerchantDebit);

            Assert.AreEqual(1, batchRequest.getNumSubmerchantDebit());
            Assert.AreEqual(106L, batchRequest.getSubmerchantDebitAmount());
            Assert.AreEqual("\r\n<submerchantDebit reportGroup=\"Default Report Group\">\r\n<fundingSubmerchantId>123456</fundingSubmerchantId>\r\n<submerchantName>merchant</submerchantName>\r\n<fundsTransferId>123467</fundsTransferId>\r\n<amount>106</amount>\r\n<accountInfo>\r\n<accType>Checking</accType>\r\n<accNum>12345657890</accNum>\r\n<routingNum>123456789</routingNum>\r\n<checkNum>123455</checkNum></accountInfo>\r\n<customIdentifier>abc123</customIdentifier>\r\n</submerchantDebit>",
                submerchantDebit.Serialize());

            mockCnpFile.Verify(cnpFile => cnpFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), mockCnpTime.Object));
            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, submerchantDebit.Serialize()));
        }

        [Test]
        public void TestAddPayFacDebit()
        {
            var payFacDebit = new payFacDebit();
            payFacDebit.fundingSubmerchantId = "123456";
            payFacDebit.fundsTransferId = "123467";
            payFacDebit.amount = 107L;

            batchRequest.addPayFacDebit(payFacDebit);

            Assert.AreEqual(1, batchRequest.getNumPayFacDebit());
            Assert.AreEqual(107L, batchRequest.getPayFacDebitAmount());
            Assert.AreEqual("\r\n<payFacDebit reportGroup=\"Default Report Group\">\r\n<fundingSubmerchantId>123456</fundingSubmerchantId>\r\n<fundsTransferId>123467</fundsTransferId>\r\n<amount>107</amount>\r\n</payFacDebit>",
                payFacDebit.Serialize());

            mockCnpFile.Verify(cnpFile => cnpFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), mockCnpTime.Object));
            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, payFacDebit.Serialize()));
        }

        [Test]
        public void TestAddReserveDebit()
        {
            var reserveDebit = new reserveDebit();
            reserveDebit.fundingSubmerchantId = "123456";
            reserveDebit.fundsTransferId = "123467";
            reserveDebit.amount = 107L;

            batchRequest.addReserveDebit(reserveDebit);

            Assert.AreEqual(1, batchRequest.getNumReserveDebit());
            Assert.AreEqual(107L, batchRequest.getReserveDebitAmount());
            Assert.AreEqual("\r\n<reserveDebit reportGroup=\"Default Report Group\">\r\n<fundingSubmerchantId>123456</fundingSubmerchantId>\r\n<fundsTransferId>123467</fundsTransferId>\r\n<amount>107</amount>\r\n</reserveDebit>",
                reserveDebit.Serialize());

            mockCnpFile.Verify(cnpFile => cnpFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), mockCnpTime.Object));
            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, reserveDebit.Serialize()));
        }

        [Test]
        public void TestAddVendorDebit()
        {
            var vendorDebit = new vendorDebit();
            vendorDebit.fundingSubmerchantId = "123456";
            vendorDebit.vendorName = "merchant";
            vendorDebit.fundsTransferId = "123467";
            vendorDebit.amount = 106L;
            var echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "12345657890";
            echeck.routingNum = "123456789";
            echeck.checkNum = "123455";
            vendorDebit.accountInfo = echeck;

            batchRequest.addVendorDebit(vendorDebit);

            Assert.AreEqual(1, batchRequest.getNumVendorDebit());
            Assert.AreEqual(106L, batchRequest.getVendorDebitAmount());
            Assert.AreEqual("\r\n<vendorDebit reportGroup=\"Default Report Group\">\r\n<fundingSubmerchantId>123456</fundingSubmerchantId>\r\n<vendorName>merchant</vendorName>\r\n<fundsTransferId>123467</fundsTransferId>\r\n<amount>106</amount>\r\n<accountInfo>\r\n<accType>Checking</accType>\r\n<accNum>12345657890</accNum>\r\n<routingNum>123456789</routingNum>\r\n<checkNum>123455</checkNum></accountInfo>\r\n</vendorDebit>",
               vendorDebit.Serialize());

            mockCnpFile.Verify(cnpFile => cnpFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), mockCnpTime.Object));
            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, vendorDebit.Serialize()));
        }

        [Test]
        public void TestAddPhysicalCheckDebit()
        {
            var physicalCheckDebit = new physicalCheckDebit();
            physicalCheckDebit.fundingSubmerchantId = "123456";
            physicalCheckDebit.fundsTransferId = "123467";
            physicalCheckDebit.amount = 107L;

            batchRequest.addPhysicalCheckDebit(physicalCheckDebit);

            Assert.AreEqual(1, batchRequest.getNumPhysicalCheckDebit());
            Assert.AreEqual(107L, batchRequest.getPhysicalCheckDebitAmount());
            Assert.AreEqual("\r\n<physicalCheckDebit reportGroup=\"Default Report Group\">\r\n<fundingSubmerchantId>123456</fundingSubmerchantId>\r\n<fundsTransferId>123467</fundsTransferId>\r\n<amount>107</amount>\r\n</physicalCheckDebit>",
                physicalCheckDebit.Serialize());

            mockCnpFile.Verify(cnpFile => cnpFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), mockCnpTime.Object));
            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, physicalCheckDebit.Serialize()));
        }

        [Test]
        public void TestAddCustomerCredit() {
            var customerCredit = new customerCredit();
            customerCredit.fundingCustomerId = "123456";
            customerCredit.customerName = "John Doe";
            customerCredit.fundsTransferId = "123467";
            customerCredit.amount = 107L;
            customerCredit.customIdentifier = "12345678";
            
            var echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "12345657890";
            echeck.routingNum = "123456789";
            echeck.checkNum = "123455";
            customerCredit.accountInfo = echeck;
            
            batchRequest.addCustomerCredit(customerCredit);
            
            Assert.AreEqual(1,batchRequest.getNumCustomerCredit());
            Assert.AreEqual(107L,batchRequest.getCustomerCreditAmount());
            Assert.AreEqual("\r\n<customerCredit reportGroup=\"Default Report Group\">\r\n<fundingCustomerId>123456</fundingCustomerId>\r\n<customerName>John Doe</customerName>\r\n<fundsTransferId>123467</fundsTransferId>\r\n<amount>107</amount>\r\n<accountInfo>\r\n<accType>Checking</accType>\r\n<accNum>12345657890</accNum>\r\n<routingNum>123456789</routingNum>\r\n<checkNum>123455</checkNum></accountInfo>\r\n<customIdentifier>12345678</customIdentifier>\r\n</customerCredit>",
                customerCredit.Serialize());
            
            mockCnpFile.Verify(cnpFile => cnpFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), mockCnpTime.Object));
            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, customerCredit.Serialize()));
        }
        
        [Test]
        public void TestAddPayoutOrgDebit()
        {
            var payoutOrgDebit = new payoutOrgDebit();
            payoutOrgDebit.fundingCustomerId = "123456";
            payoutOrgDebit.fundsTransferId = "123467";
            payoutOrgDebit.amount = 107L;
            
            batchRequest.addPayoutOrgDebit(payoutOrgDebit);
            
            Assert.AreEqual(1,batchRequest.getNumPayoutOrgDebit());
            Assert.AreEqual(107L,batchRequest.getPayoutOrgDebitAmount());
            Assert.AreEqual(
                "\r\n<payoutOrgDebit reportGroup=\"Default Report Group\">\r\n<fundingCustomerId>123456</fundingCustomerId>\r\n<fundsTransferId>123467</fundsTransferId>\r\n<amount>107</amount>\r\n</payoutOrgDebit>",
                payoutOrgDebit.Serialize());

            mockCnpFile.Verify(cnpFile => cnpFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), mockCnpTime.Object)); 
            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, payoutOrgDebit.Serialize()));
        }

        [Test]
        public void TestAddPayoutOrgCredit()
        {
            var payoutOrgCredit = new payoutOrgCredit();
            payoutOrgCredit.fundingCustomerId = "123456";
            payoutOrgCredit.fundsTransferId = "123467";
            payoutOrgCredit.amount = 107L;
            
            batchRequest.addPayoutOrgCredit(payoutOrgCredit);
            
            Assert.AreEqual(1,batchRequest.getNumPayoutOrgCredit());
            Assert.AreEqual(107L,batchRequest.getPayoutOrgCreditAmount());
            Assert.AreEqual(
                "\r\n<payoutOrgCredit reportGroup=\"Default Report Group\">\r\n<fundingCustomerId>123456</fundingCustomerId>\r\n<fundsTransferId>123467</fundsTransferId>\r\n<amount>107</amount>\r\n</payoutOrgCredit>",
                payoutOrgCredit.Serialize());

            mockCnpFile.Verify(cnpFile => cnpFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), mockCnpTime.Object)); 
            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, payoutOrgCredit.Serialize()));
        }
        
        [Test]
        public void TestAddCustomerDebit()
        {
            var customerDebit = new customerDebit();
            customerDebit.fundingCustomerId = "123456";
            customerDebit.customerName = "John Doe";
            customerDebit.fundsTransferId = "123467";
            customerDebit.amount = 107L;
            customerDebit.customIdentifier = "12345678";
            
            var echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "12345657890";
            echeck.routingNum = "123456789";
            echeck.checkNum = "123455";
            customerDebit.accountInfo = echeck;
            
            batchRequest.addCustomerDebit(customerDebit);
            
            Assert.AreEqual(1,batchRequest.getNumCustomerDebit());
            Assert.AreEqual(107L,batchRequest.getCustomerDebitAmount());
            Assert.AreEqual("\r\n<customerDebit reportGroup=\"Default Report Group\">\r\n<fundingCustomerId>123456</fundingCustomerId>\r\n<customerName>John Doe</customerName>\r\n<fundsTransferId>123467</fundsTransferId>\r\n<amount>107</amount>\r\n<accountInfo>\r\n<accType>Checking</accType>\r\n<accNum>12345657890</accNum>\r\n<routingNum>123456789</routingNum>\r\n<checkNum>123455</checkNum></accountInfo>\r\n<customIdentifier>12345678</customIdentifier>\r\n</customerDebit>",
                customerDebit.Serialize());
            
            mockCnpFile.Verify(cnpFile => cnpFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), mockCnpTime.Object));
            mockCnpFile.Verify(cnpFile => cnpFile.AppendLineToFile(mockFilePath, customerDebit.Serialize()));
        }
    }
}
