using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Cnp.Sdk;
using Moq;
using System.Text.RegularExpressions;


namespace Cnp.Sdk.Test.Unit
{
    
    public class TestCnpOnline
    {
        
        private CnpOnline cnp = new CnpOnline();
        

        [Fact]
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

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<cnpOnlineRequest.*<authorization.*<card>.*<number>4100000000000002</number>.*</card>.*</authorization>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<cnpOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><authorizationResponse><cnpTxnId>123</cnpTxnId></authorizationResponse></cnpOnlineResponse>");
     
            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            authorizationResponse authorize = cnp.Authorize(authorization);
            Assert.Equal(123, authorize.cnpTxnId);
        }

        [Fact]
        public void TestAuthReversal()
        {
            authReversal authreversal = new authReversal();
            authreversal.cnpTxnId = 12345678000;
            authreversal.amount = 106;
            authreversal.payPalNotes = "Notes";
            

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<authReversal.*?<cnpTxnId>12345678000</cnpTxnId>.*?</authReversal>.*?", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<cnpOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><authReversalResponse><cnpTxnId>123</cnpTxnId></authReversalResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            authReversalResponse authreversalresponse = cnp.AuthReversal(authreversal);
            Assert.Equal(123, authreversalresponse.cnpTxnId);
        }

        [Fact]
        public void TestCapture()
        {
            capture caputure = new capture();
            caputure.cnpTxnId = 123456000;
            caputure.amount = 106;
            caputure.payPalNotes = "Notes";


            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<capture.*?<cnpTxnId>123456000</cnpTxnId>.*?</capture>.*?", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<cnpOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureResponse><cnpTxnId>123</cnpTxnId></captureResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            captureResponse captureresponse = cnp.Capture(caputure);
            Assert.Equal(123, captureresponse.cnpTxnId);
        }

        [Fact]
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

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<captureGivenAuth.*?<card>.*?<number>4100000000000001</number>.*?</card>.*?</captureGivenAuth>.*?", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<cnpOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureGivenAuthResponse><cnpTxnId>123</cnpTxnId></captureGivenAuthResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            captureGivenAuthResponse caputregivenauthresponse = cnp.CaptureGivenAuth(capturegivenauth);
            Assert.Equal(123, caputregivenauthresponse.cnpTxnId);
        }

        [Fact]
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

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<credit.*?<card>.*?<number>4100000000000001</number>.*?</card>.*?</credit>.*?", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<cnpOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><creditResponse><cnpTxnId>123</cnpTxnId></creditResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            creditResponse creditresponse = cnp.Credit(credit);
            Assert.Equal(123, creditresponse.cnpTxnId);
        }

        [Fact]
        public void TestEcheckCredit()
        {
            echeckCredit echeckcredit = new echeckCredit();
            echeckcredit.amount = 12;
            echeckcredit.cnpTxnId = 123456789101112;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<echeckCredit.*?<cnpTxnId>123456789101112</cnpTxnId>.*?</echeckCredit>.*?", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<cnpOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><echeckCreditResponse><cnpTxnId>123</cnpTxnId></echeckCreditResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            echeckCreditResponse echeckcreditresponse = cnp.EcheckCredit(echeckcredit);
            Assert.Equal(123, echeckcreditresponse.cnpTxnId);
        }

        [Fact]
        public void TestEcheckRedeposit()
        {
            echeckRedeposit echeckredeposit = new echeckRedeposit();
            echeckredeposit.cnpTxnId = 123456;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<echeckRedeposit.*?<cnpTxnId>123456</cnpTxnId>.*?</echeckRedeposit>.*?", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<cnpOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><echeckRedepositResponse><cnpTxnId>123</cnpTxnId></echeckRedepositResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            echeckRedepositResponse echeckredepositresponse = cnp.EcheckRedeposit(echeckredeposit);
            Assert.Equal(123, echeckredepositresponse.cnpTxnId);
        }

        [Fact]
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

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<echeckSale.*?<echeck>.*?<accNum>12345657890</accNum>.*?</echeck>.*?</echeckSale>.*?", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<cnpOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><echeckSalesResponse><cnpTxnId>123</cnpTxnId></echeckSalesResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            echeckSalesResponse echecksaleresponse = cnp.EcheckSale(echecksale);
            Assert.Equal(123, echecksaleresponse.cnpTxnId);
        }

        [Fact]
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

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<echeckVerification.*?<echeck>.*?<accNum>12345657890</accNum>.*?</echeck>.*?</echeckVerification>.*?", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<cnpOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><echeckVerificationResponse><cnpTxnId>123</cnpTxnId></echeckVerificationResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            echeckVerificationResponse echeckverificaitonresponse = cnp.EcheckVerification(echeckverification);
            Assert.Equal(123, echeckverificaitonresponse.cnpTxnId);
        }

        [Fact]
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

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<forceCapture.*?<card>.*?<number>4100000000000001</number>.*?</card>.*?</forceCapture>.*?", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<cnpOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><forceCaptureResponse><cnpTxnId>123</cnpTxnId></forceCaptureResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            forceCaptureResponse forcecaptureresponse = cnp.ForceCapture(forcecapture);
            Assert.Equal(123, forcecaptureresponse.cnpTxnId);
        }

        [Fact]
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

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<sale.*?<card>.*?<number>4100000000000002</number>.*?</card>.*?</sale>.*?", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<cnpOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><saleResponse><cnpTxnId>123</cnpTxnId></saleResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            saleResponse saleresponse = cnp.Sale(sale);
            Assert.Equal(123, saleresponse.cnpTxnId);
        }

        [Fact]
        public void TestToken()
        {
            registerTokenRequestType token = new registerTokenRequestType();
            token.orderId = "12344";
            token.accountNumber = "1233456789103801";
            

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<registerTokenRequest.*?<accountNumber>1233456789103801</accountNumber>.*?</registerTokenRequest>.*?", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<cnpOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><registerTokenResponse><cnpTxnId>123</cnpTxnId></registerTokenResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            registerTokenResponse registertokenresponse = cnp.RegisterToken(token);
            Assert.Equal(123, registertokenresponse.cnpTxnId);
            Assert.Null(registertokenresponse.type);
        }

        [Fact]
        public void TestActivate()
        {
            activate activate = new activate();
            activate.orderId = "2";
            activate.orderSource = orderSourceType.ecommerce;
            activate.card = new giftCardCardType();

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<activate.*?<orderId>2</orderId>.*?</activate>.*?", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
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
            Assert.Equal(82919789861357149, activateResponse.cnpTxnId);
            Assert.Equal("123456", activateResponse.virtualGiftCardResponse.accountNumber);
            Assert.Equal("123456", activateResponse.virtualGiftCardResponse.cardValidationNum);
            Assert.Equal("1234", activateResponse.virtualGiftCardResponse.pin);
        }

        [Fact]
        public void TestDeactivate()
        {
            deactivate deactivate = new deactivate();
            deactivate.orderId = "2";
            deactivate.orderSource = orderSourceType.ecommerce;
            deactivate.card = new giftCardCardType();

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<deactivate.*?<orderId>2</orderId>.*?</deactivate>.*?", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<cnpOnlineResponse version='8.21' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><deactivateResponse><cnpTxnId>123</cnpTxnId></deactivateResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            deactivateResponse deactivateResponse = cnp.Deactivate(deactivate);
            Assert.Equal(123, deactivateResponse.cnpTxnId);
        }

        [Fact]
        public void TestLoad()
        {
            load load = new load();
            load.orderId = "2";
            load.orderSource = orderSourceType.ecommerce;
            load.card = new giftCardCardType();

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<load.*?<orderId>2</orderId>.*?</load>.*?", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<cnpOnlineResponse version='8.21' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><loadResponse><cnpTxnId>123</cnpTxnId></loadResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            loadResponse loadResponse = cnp.Load(load);
            Assert.Equal(123, loadResponse.cnpTxnId);
        }

        [Fact]
        public void TestUnload()
        {
            unload unload = new unload();
            unload.orderId = "2";
            unload.orderSource = orderSourceType.ecommerce;
            unload.card = new giftCardCardType();

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<unload.*?<orderId>2</orderId>.*?</unload>.*?", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<cnpOnlineResponse version='8.21' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><unloadResponse><cnpTxnId>123</cnpTxnId></unloadResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            unloadResponse unloadResponse = cnp.Unload(unload);
            Assert.Equal(123, unloadResponse.cnpTxnId);
        }

        [Fact]
        public void TestBalanceInquiry()
        {
            balanceInquiry balanceInquiry = new balanceInquiry();
            balanceInquiry.orderId = "2";
            balanceInquiry.orderSource = orderSourceType.ecommerce;
            balanceInquiry.card = new giftCardCardType();

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<balanceInquiry.*?<orderId>2</orderId>.*?</balanceInquiry>.*?", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<cnpOnlineResponse version='8.21' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><balanceInquiryResponse><cnpTxnId>123</cnpTxnId></balanceInquiryResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            balanceInquiryResponse balanceInquiryResponse = cnp.BalanceInquiry(balanceInquiry);
            Assert.Equal(123, balanceInquiryResponse.cnpTxnId);
        }

        [Fact]
        public void TestCreatePlan()
        {
            createPlan createPlan = new createPlan();
            createPlan.planCode = "theCode";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<createPlan.*?<planCode>theCode</planCode>.*?</createPlan>.*?", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<cnpOnlineResponse version='8.21' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><createPlanResponse><planCode>theCode</planCode></createPlanResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            createPlanResponse createPlanResponse = cnp.CreatePlan(createPlan);
            Assert.Equal("theCode", createPlanResponse.planCode);
        }

        [Fact]
        public void TestUpdatePlan()
        {
            updatePlan updatePlan = new updatePlan();
            updatePlan.planCode = "theCode";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<updatePlan.*?<planCode>theCode</planCode>.*?</updatePlan>.*?", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<cnpOnlineResponse version='8.21' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><updatePlanResponse><planCode>theCode</planCode></updatePlanResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            updatePlanResponse updatePlanResponse = cnp.UpdatePlan(updatePlan);
            Assert.Equal("theCode", updatePlanResponse.planCode);
        }

        [Fact]
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

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<authorization.*?<card>.*?<number>4100000000000002</number>.*?</card>.*?</authorization>.*?", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<cnpOnlineResponse version='8.10' response='1' message='Error validating xml data against the schema' xmlns='http://www.vantivcnp.com/schema'><authorizationResponse><cnpTxnId>123</cnpTxnId></authorizationResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            try
            {
                cnp.Authorize(authorization);
            }
            catch (CnpOnlineException e)
            {
                Assert.Equal("Error validating xml data against the schema", e.Message);
            }
        }

        [Fact]
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

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<authorization.*?<card>.*?<number>4100000000000002</number>.*?</card>.*?</authorization>.*?", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("no xml");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            try
            {
                cnp.Authorize(authorization);
            }
            catch (CnpOnlineException e)
            {
                Assert.Equal("Error validating xml data against the schema", e.Message);
            }
        }

        [Fact]
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

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<authorization.*? reportGroup=\"Default Report Group\">.*?<card>.*?<number>4100000000000002</number>.*?</card>.*?</authorization>.*?", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<cnpOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><authorizationResponse reportGroup='Default Report Group'></authorizationResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            authorizationResponse authorize = cnp.Authorize(authorization);
            Assert.Equal("Default Report Group", authorize.reportGroup);
        }

        [Fact]
        public void TestSetMerchantSdk()
        {

        }
            
    }
}
