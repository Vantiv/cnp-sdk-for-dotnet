using System;
using NUnit.Framework;
using Moq;
using System.Text.RegularExpressions;
using System.Net;

namespace Cnp.Sdk.Test.Unit
{
    [TestFixture]
    class TestCnpOnline
    {
        
        private CnpOnline cnp;

        [SetUp]
        public void SetUpCnp()
        {
            cnp = new CnpOnline();
        }

        [TearDown]
        public void Dispose()
        {
            Communications.DisposeHttpClient();
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
           
            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<cnpOnlineRequest.*<authorization.*<card>.*<number>4100000000000002</number>.*</card>.*</authorization>.*", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><authorizationResponse><cnpTxnId>123</cnpTxnId></authorizationResponse></cnpOnlineResponse>");
     
            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            authorizationResponse authorize = cnp.Authorize(authorization);
            Assert.AreEqual(123, authorize.cnpTxnId);
        }

        [Test]
        public void TestAuthReversal()
        {
            authReversal authreversal = new authReversal();
            authreversal.cnpTxnId = 12345678000;
            authreversal.amount = 106;
            authreversal.payPalNotes = "Notes";
            

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<authReversal.*?<cnpTxnId>12345678000</cnpTxnId>.*?</authReversal>.*?", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><authReversalResponse><cnpTxnId>123</cnpTxnId></authReversalResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            authReversalResponse authreversalresponse = cnp.AuthReversal(authreversal);
            Assert.AreEqual(123, authreversalresponse.cnpTxnId);
        }

        [Test]
        public void TestCapture()
        {
            capture caputure = new capture();
            caputure.cnpTxnId = 123456000;
            caputure.amount = 106;
            caputure.payPalNotes = "Notes";


            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<capture.*?<cnpTxnId>123456000</cnpTxnId>.*?</capture>.*?", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureResponse><cnpTxnId>123</cnpTxnId></captureResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            captureResponse captureresponse = cnp.Capture(caputure);
            Assert.AreEqual(123, captureresponse.cnpTxnId);
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

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<captureGivenAuth.*?<card>.*?<number>4100000000000001</number>.*?</card>.*?</captureGivenAuth>.*?", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureGivenAuthResponse><cnpTxnId>123</cnpTxnId></captureGivenAuthResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            captureGivenAuthResponse caputregivenauthresponse = cnp.CaptureGivenAuth(capturegivenauth);
            Assert.AreEqual(123, caputregivenauthresponse.cnpTxnId);
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

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<credit.*?<card>.*?<number>4100000000000001</number>.*?</card>.*?</credit>.*?", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><creditResponse><cnpTxnId>123</cnpTxnId></creditResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            creditResponse creditresponse = cnp.Credit(credit);
            Assert.AreEqual(123, creditresponse.cnpTxnId);
        }

        [Test]
        public void TestEcheckCredit()
        {
            echeckCredit echeckcredit = new echeckCredit();
            echeckcredit.amount = 12;
            echeckcredit.cnpTxnId = 123456789101112;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<echeckCredit.*?<cnpTxnId>123456789101112</cnpTxnId>.*?</echeckCredit>.*?", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><echeckCreditResponse><cnpTxnId>123</cnpTxnId></echeckCreditResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            echeckCreditResponse echeckcreditresponse = cnp.EcheckCredit(echeckcredit);
            Assert.AreEqual(123, echeckcreditresponse.cnpTxnId);
        }

        [Test]
        public void TestEcheckRedeposit()
        {
            echeckRedeposit echeckredeposit = new echeckRedeposit();
            echeckredeposit.cnpTxnId = 123456;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<echeckRedeposit.*?<cnpTxnId>123456</cnpTxnId>.*?</echeckRedeposit>.*?", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><echeckRedepositResponse><cnpTxnId>123</cnpTxnId></echeckRedepositResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            echeckRedepositResponse echeckredepositresponse = cnp.EcheckRedeposit(echeckredeposit);
            Assert.AreEqual(123, echeckredepositresponse.cnpTxnId);
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
            

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<echeckSale.*?<echeck>.*?<accNum>12345657890</accNum>.*?</echeck>.*?</echeckSale>.*?", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><echeckSalesResponse><cnpTxnId>123</cnpTxnId></echeckSalesResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            echeckSalesResponse echecksaleresponse = cnp.EcheckSale(echecksale);
            Assert.AreEqual(123, echecksaleresponse.cnpTxnId);
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


            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<echeckVerification.*?<echeck>.*?<accNum>12345657890</accNum>.*?</echeck>.*?</echeckVerification>.*?", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><echeckVerificationResponse><cnpTxnId>123</cnpTxnId></echeckVerificationResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            echeckVerificationResponse echeckverificaitonresponse = cnp.EcheckVerification(echeckverification);
            Assert.AreEqual(123, echeckverificaitonresponse.cnpTxnId);
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

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<forceCapture.*?<card>.*?<number>4100000000000001</number>.*?</card>.*?</forceCapture>.*?", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><forceCaptureResponse><cnpTxnId>123</cnpTxnId></forceCaptureResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            forceCaptureResponse forcecaptureresponse = cnp.ForceCapture(forcecapture);
            Assert.AreEqual(123, forcecaptureresponse.cnpTxnId);
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

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<sale.*?<card>.*?<number>4100000000000002</number>.*?</card>.*?</sale>.*?", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><saleResponse><cnpTxnId>123</cnpTxnId></saleResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            saleResponse saleresponse = cnp.Sale(sale);
            Assert.AreEqual(123, saleresponse.cnpTxnId);
        }

        [Test]
        public void TestSale_BadConnection()
        {
            var sale = new sale
            {
                id = "21321415412",
                orderId = "1556632727643",
                amount = 5000,
                orderSource = orderSourceType.androidpay,
                paypage = new cardPaypageType
                {
                    paypageRegistrationId = "4005795464788715792"
                }
            };

            Communications.DisposeHttpClient();
            var config = new ConfigManager().getConfig();
            config["proxyHost"] = "some-garbage";
            config["proxyPort"] = "123";
            var tempCnp = new CnpOnline(config);

            // Expect a WebException because an invalid proxy configuration is set
            Assert.Throws<WebException>(() => tempCnp.Sale(sale));
        }

        [Test]
        public void TestToken()
        {
            registerTokenRequestType token = new registerTokenRequestType();
            token.orderId = "12344";
            token.accountNumber = "1233456789103801";
            

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<registerTokenRequest.*?<accountNumber>1233456789103801</accountNumber>.*?</registerTokenRequest>.*?", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><registerTokenResponse><cnpTxnId>123</cnpTxnId></registerTokenResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            registerTokenResponse registertokenresponse = cnp.RegisterToken(token);
            Assert.AreEqual(123, registertokenresponse.cnpTxnId);
            Assert.IsNull(registertokenresponse.type);
        }

        [Test]
        public void TestActivate()
        {
            activate activate = new activate();
            activate.orderId = "2";
            activate.orderSource = orderSourceType.ecommerce;
            activate.card = new giftCardCardType();

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<activate.*?<orderId>2</orderId>.*?</activate>.*?", RegexOptions.Singleline)  ))
                .Returns(@"
<cnpOnlineResponse version=""11.0"" xmlns=""http://www.vantivcnp.com/schema""
response=""0"" message=""ValidFormat"">
<activateResponse id=""1"" reportGroup=""Planets"">
<cnpTxnId>82919789861357149</cnpTxnId>
<response>000</response>
<responseTime>2017-01-23T19:31:10</responseTime>
<message>InvalidAccountNumber</message>
<postDate>2017-01-24</postDate>
<fraudResult/>
<virtualGiftCardResponse>
<accountNumber>123456</accountNumber>
<cardValidationNum>123456</cardValidationNum>
<pin>1234</pin>
</virtualGiftCardResponse>
</activateResponse>
</cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            activateResponse activateResponse = cnp.Activate(activate);
            Assert.AreEqual(82919789861357149, activateResponse.cnpTxnId);
            Assert.AreEqual("123456", activateResponse.virtualGiftCardResponse.accountNumber);
            Assert.AreEqual("123456", activateResponse.virtualGiftCardResponse.cardValidationNum);
            Assert.AreEqual("1234", activateResponse.virtualGiftCardResponse.pin);
        }

        [Test]
        public void TestDeactivate()
        {
            deactivate deactivate = new deactivate();
            deactivate.orderId = "2";
            deactivate.orderSource = orderSourceType.ecommerce;
            deactivate.card = new giftCardCardType();

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<deactivate.*?<orderId>2</orderId>.*?</deactivate>.*?", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.21' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><deactivateResponse><cnpTxnId>123</cnpTxnId></deactivateResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            deactivateResponse deactivateResponse = cnp.Deactivate(deactivate);
            Assert.AreEqual(123, deactivateResponse.cnpTxnId);
        }

        [Test]
        public void TestLoad()
        {
            load load = new load();
            load.orderId = "2";
            load.orderSource = orderSourceType.ecommerce;
            load.card = new giftCardCardType();

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<load.*?<orderId>2</orderId>.*?</load>.*?", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.21' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><loadResponse><cnpTxnId>123</cnpTxnId></loadResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            loadResponse loadResponse = cnp.Load(load);
            Assert.AreEqual(123, loadResponse.cnpTxnId);
        }

        [Test]
        public void TestUnload()
        {
            unload unload = new unload();
            unload.orderId = "2";
            unload.orderSource = orderSourceType.ecommerce;
            unload.card = new giftCardCardType();

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<unload.*?<orderId>2</orderId>.*?</unload>.*?", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.21' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><unloadResponse><cnpTxnId>123</cnpTxnId></unloadResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            unloadResponse unloadResponse = cnp.Unload(unload);
            Assert.AreEqual(123, unloadResponse.cnpTxnId);
        }

        [Test]
        public void TestBalanceInquiry()
        {
            balanceInquiry balanceInquiry = new balanceInquiry();
            balanceInquiry.orderId = "2";
            balanceInquiry.orderSource = orderSourceType.ecommerce;
            balanceInquiry.card = new giftCardCardType();

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<balanceInquiry.*?<orderId>2</orderId>.*?</balanceInquiry>.*?", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.21' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><balanceInquiryResponse><cnpTxnId>123</cnpTxnId></balanceInquiryResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            balanceInquiryResponse balanceInquiryResponse = cnp.BalanceInquiry(balanceInquiry);
            Assert.AreEqual(123, balanceInquiryResponse.cnpTxnId);
        }

        [Test]
        public void TestCreatePlan()
        {
            createPlan createPlan = new createPlan();
            createPlan.planCode = "theCode";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<createPlan.*?<planCode>theCode</planCode>.*?</createPlan>.*?", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.21' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><createPlanResponse><planCode>theCode</planCode></createPlanResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            createPlanResponse createPlanResponse = cnp.CreatePlan(createPlan);
            Assert.AreEqual("theCode", createPlanResponse.planCode);
        }

        [Test]
        public void TestUpdatePlan()
        {
            updatePlan updatePlan = new updatePlan();
            updatePlan.planCode = "theCode";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<updatePlan.*?<planCode>theCode</planCode>.*?</updatePlan>.*?", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.21' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><updatePlanResponse><planCode>theCode</planCode></updatePlanResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            updatePlanResponse updatePlanResponse = cnp.UpdatePlan(updatePlan);
            Assert.AreEqual("theCode", updatePlanResponse.planCode);
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

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<authorization.*?<card>.*?<number>4100000000000002</number>.*?</card>.*?</authorization>.*?", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.10' response='1' message='Error validating xml data against the schema' xmlns='http://www.vantivcnp.com/schema'><authorizationResponse><cnpTxnId>123</cnpTxnId></authorizationResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            try
            {
                cnp.Authorize(authorization);
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

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<authorization.*?<card>.*?<number>4100000000000002</number>.*?</card>.*?</authorization>.*?", RegexOptions.Singleline)  ))
                .Returns("no xml");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            try
            {
                cnp.Authorize(authorization);
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

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<authorization.*? reportGroup=\"Default Report Group\">.*?<card>.*?<number>4100000000000002</number>.*?</card>.*?</authorization>.*?", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><authorizationResponse reportGroup='Default Report Group'></authorizationResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            authorizationResponse authorize = cnp.Authorize(authorization);
            Assert.AreEqual("Default Report Group", authorize.reportGroup);
        }

        [Test]
        public void TestSetMerchantSdk()
        {

        }
            
    }
}
