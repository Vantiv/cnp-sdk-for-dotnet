﻿using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Net;


namespace Cnp.Sdk.Test.Unit
{
    [TestFixture]
    class TestAuthorization
    {

        private CnpOnline cnp;

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            cnp = new CnpOnline();
        }

        [Test]
        public void TestFraudFilterOverride()
        {
            var auth = new authorization();
            auth.orderId = "12344";
            auth.amount = 2;
            auth.orderSource = orderSourceType.ecommerce;
            auth.reportGroup = "Planets";
            auth.fraudFilterOverride = true;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<fraudFilterOverride>true</fraudFilterOverride>.*", RegexOptions.Singleline) ))
                .Returns(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent("<cnpOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><authorizationResponse><cnpTxnId>123</cnpTxnId></authorizationResponse></cnpOnlineResponse>")});

            var mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var authorizationResponse = cnp.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.cnpTxnId);
        }

        [Test]
        public void TestContactShouldSendEmailForEmail_NotZip()
        {
            var auth = new authorization();
            auth.orderId = "12344";
            auth.amount = 2;
            auth.orderSource = orderSourceType.ecommerce;
            auth.reportGroup = "Planets";
            var billToAddress = new contact();
            billToAddress.email = "gdake@cnp.com";
            billToAddress.zip = "12345";
            auth.billToAddress = billToAddress;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<zip>12345</zip>.*<email>gdake@cnp.com</email>.*", RegexOptions.Singleline) ))
                .Returns(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><authorizationResponse><cnpTxnId>123</cnpTxnId></authorizationResponse></cnpOnlineResponse>")});

            var mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var authorizationResponse = cnp.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.cnpTxnId);
        }

        [Test]
        public void Test3dsAttemptedShouldNotSayItem()
        {
            var auth = new authorization();
            auth.orderId = "12344";
            auth.amount = 2;
            auth.orderSource = orderSourceType.item3dsAttempted;
            auth.reportGroup = "Planets";
            var billToAddress = new contact();
            billToAddress.email = "gdake@cnp.com";
            billToAddress.zip = "12345";
            auth.billToAddress = billToAddress;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>.*<orderSource>3dsAttempted</orderSource>.*", RegexOptions.Singleline) ))
                .Returns(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><authorizationResponse><cnpTxnId>123</cnpTxnId></authorizationResponse></cnpOnlineResponse>")});

            var mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var authorizationResponse = cnp.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.cnpTxnId);
        }

        [Test]
        public void Test3dsAuthenticatedShouldNotSayItem()
        {
            var auth = new authorization();
            auth.orderId = "12344";
            auth.amount = 2;
            auth.orderSource = orderSourceType.item3dsAuthenticated;
            auth.reportGroup = "Planets";
            var billToAddress = new contact();
            billToAddress.email = "gdake@cnp.com";
            billToAddress.zip = "12345";
            auth.billToAddress = billToAddress;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>.*<orderSource>3dsAuthenticated</orderSource>.*", RegexOptions.Singleline) ))
                .Returns(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><authorizationResponse><cnpTxnId>123</cnpTxnId></authorizationResponse></cnpOnlineResponse>")});

            var mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var authorizationResponse = cnp.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.cnpTxnId);
        }

        [Test]
        public void TestSecondaryAmount()
        {
            var auth = new authorization();
            auth.orderId = "12344";
            auth.amount = 2;
            auth.secondaryAmount = 1;
            auth.orderSource = orderSourceType.ecommerce;
            auth.reportGroup = "Planets";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<secondaryAmount>1</secondaryAmount>\r\n<orderSource>ecommerce</orderSource>.*", RegexOptions.Singleline) ))
                .Returns(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><authorizationResponse><cnpTxnId>123</cnpTxnId></authorizationResponse></cnpOnlineResponse>")});

            var mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var authorizationResponse = cnp.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.cnpTxnId);
        }

        [Test]
        public void TestSurchargeAmount()
        {
            var auth = new authorization();
            auth.orderId = "12344";
            auth.amount = 2;
            auth.surchargeAmount = 1;
            auth.orderSource = orderSourceType.ecommerce;
            auth.reportGroup = "Planets";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<surchargeAmount>1</surchargeAmount>\r\n<orderSource>ecommerce</orderSource>.*", RegexOptions.Singleline)))
                .Returns(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><authorizationResponse><cnpTxnId>123</cnpTxnId></authorizationResponse></cnpOnlineResponse>")});

            var mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var authorizationResponse = cnp.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.cnpTxnId);
        }

        [Test]
        public void TestSurchargeAmount_Optional()
        {
            var auth = new authorization();
            auth.orderId = "12344";
            auth.amount = 2;
            auth.orderSource = orderSourceType.ecommerce;
            auth.reportGroup = "Planets";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<orderSource>ecommerce</orderSource>.*", RegexOptions.Singleline)))
                .Returns(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><authorizationResponse><cnpTxnId>123</cnpTxnId></authorizationResponse></cnpOnlineResponse>")});

            var mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var authorizationResponse = cnp.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.cnpTxnId);
        }

        [Test]
        public void TestMethodOfPaymentAllowsGiftCard()
        {
            var auth = new authorization();
            auth.orderId = "12344";
            auth.amount = 2;
            auth.orderSource = orderSourceType.ecommerce;
            auth.reportGroup = "Planets";
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.GC;
            card.number = "414100000000000000";
            card.expDate = "1210";
            auth.card = card;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<card>\r\n<type>GC</type>\r\n<number>414100000000000000</number>\r\n<expDate>1210</expDate>\r\n</card>.*", RegexOptions.Singleline) ))
                .Returns(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent("<cnpOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><authorizationResponse><cnpTxnId>123</cnpTxnId></authorizationResponse></cnpOnlineResponse>")});

            var mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var authorizationResponse = cnp.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.cnpTxnId);
        }

        [Test]
        public void TestMethodOfPaymentApplepayAndWallet()
        {
            var auth = new authorization();
            auth.orderId = "12344";
            auth.amount = 2;
            auth.orderSource = orderSourceType.applepay;
            auth.reportGroup = "Planets";
            var applepay = new applepayType();
            var applepayHeaderType = new applepayHeaderType();
            applepayHeaderType.applicationData = "454657413164";
            applepayHeaderType.ephemeralPublicKey = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855";
            applepayHeaderType.publicKeyHash = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855";
            applepayHeaderType.transactionId = "1234";
            applepay.header = applepayHeaderType;
            applepay.data = "user";
            applepay.signature = "sign";
            applepay.version = "1";
            auth.applepay = applepay;

            var wallet = new wallet();
            wallet.walletSourceTypeId = "123";
            auth.wallet = wallet;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<cnpOnlineRequest.*?<authorization.*?<orderSource>applepay</orderSource>.*?<applepay>.*?<data>user</data>.*?</applepay>.*?<wallet>.*?<walletSourceTypeId>123</walletSourceTypeId>.*?</wallet>.*?</authorization>.*?", RegexOptions.Singleline) ))
                .Returns(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent("<cnpOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><authorizationResponse><cnpTxnId>123</cnpTxnId></authorizationResponse></cnpOnlineResponse>")});

            var mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var authorizationResponse = cnp.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.cnpTxnId);
        }

        [Test]
        public void TestRecurringRequest()
        {
            var auth = new authorization();
            auth.card = new cardType();
            auth.card.type = methodOfPaymentTypeEnum.VI;
            auth.card.number = "4100000000000001";
            auth.card.expDate = "1213";
            auth.orderId = "12344";
            auth.amount = 2;
            auth.orderSource = orderSourceType.ecommerce;
            auth.fraudFilterOverride = true;
            auth.recurringRequest = new recurringRequest();
            auth.recurringRequest.subscription = new subscription();
            auth.recurringRequest.subscription.planCode = "abc123";
            auth.recurringRequest.subscription.numberOfPayments = 12;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<fraudFilterOverride>true</fraudFilterOverride>\r\n<recurringRequest>\r\n<subscription>\r\n<planCode>abc123</planCode>\r\n<numberOfPayments>12</numberOfPayments>\r\n</subscription>\r\n</recurringRequest>\r\n</authorization>.*", RegexOptions.Singleline) ))
                .Returns(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent("<cnpOnlineResponse version='8.18' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><authorizationResponse><cnpTxnId>123</cnpTxnId></authorizationResponse></cnpOnlineResponse>")});

            var mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var authorizationResponse = cnp.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.cnpTxnId);
        }

        [Test]
        public void TestDebtRepayment()
        {
            var auth = new authorization();
            auth.card = new cardType();
            auth.card.type = methodOfPaymentTypeEnum.VI;
            auth.card.number = "4100000000000001";
            auth.card.expDate = "1213";
            auth.orderId = "12344";
            auth.amount = 2;
            auth.orderSource = orderSourceType.ecommerce;
            auth.fraudFilterOverride = true;
            auth.debtRepayment = true;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<fraudFilterOverride>true</fraudFilterOverride>\r\n<debtRepayment>true</debtRepayment>\r\n</authorization>.*", RegexOptions.Singleline) ))
                .Returns(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><authorizationResponse><cnpTxnId>123</cnpTxnId></authorizationResponse></cnpOnlineResponse>")});

            var mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var authorizationResponse = cnp.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.cnpTxnId);
        }

        [Test]
        public void TestRecurringResponse_Full()
        {
            var xmlResponse = "<cnpOnlineResponse version='8.18' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><authorizationResponse><cnpTxnId>123</cnpTxnId><recurringResponse><subscriptionId>12</subscriptionId><responseCode>345</responseCode><responseMessage>Foo</responseMessage><recurringTxnId>678</recurringTxnId></recurringResponse></authorizationResponse></cnpOnlineResponse>";
            var cnpOnlineResponse = CnpOnline.DeserializeObject(xmlResponse);
            var authorizationResponse = (authorizationResponse)cnpOnlineResponse.authorizationResponse;

            Assert.AreEqual(123, authorizationResponse.cnpTxnId);
            Assert.AreEqual(12, authorizationResponse.recurringResponse.subscriptionId);
            Assert.AreEqual("345", authorizationResponse.recurringResponse.responseCode);
            Assert.AreEqual("Foo", authorizationResponse.recurringResponse.responseMessage);
            Assert.AreEqual(678, authorizationResponse.recurringResponse.recurringTxnId);
        }

        [Test]
        public void TestRecurringResponse_NoRecurringTxnId()
        {
            var xmlResponse = "<cnpOnlineResponse version='8.18' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><authorizationResponse><cnpTxnId>123</cnpTxnId><recurringResponse><subscriptionId>12</subscriptionId><responseCode>345</responseCode><responseMessage>Foo</responseMessage></recurringResponse></authorizationResponse></cnpOnlineResponse>";
            var cnpOnlineResponse = CnpOnline.DeserializeObject(xmlResponse);
            var authorizationResponse = (authorizationResponse)cnpOnlineResponse.authorizationResponse;

            Assert.AreEqual(123, authorizationResponse.cnpTxnId);
            Assert.AreEqual(12, authorizationResponse.recurringResponse.subscriptionId);
            Assert.AreEqual("345", authorizationResponse.recurringResponse.responseCode);
            Assert.AreEqual("Foo", authorizationResponse.recurringResponse.responseMessage);
            Assert.AreEqual(0, authorizationResponse.recurringResponse.recurringTxnId);
        }

        [Test]
        public void TestSimpleAuthWithFraudCheck()
        {
            var auth = new authorization();
            auth.card = new cardType();
            auth.card.type = methodOfPaymentTypeEnum.VI;
            auth.card.number = "4100000000000001";
            auth.card.expDate = "1213";
            auth.orderId = "12344";
            auth.amount = 2;
            auth.orderSource = orderSourceType.ecommerce;
            fraudCheckType checkType = new fraudCheckType();
            checkType.authenticationProtocolVersionType = "PAP";
            auth.cardholderAuthentication = checkType;
            auth.cardholderAuthentication.customerIpAddress = "192.168.1.1";

            var expectedResult = @"
<authorization id="""" reportGroup="""">
<orderId>12344</orderId>
<amount>2</amount>
<orderSource>ecommerce</orderSource>
<card>
<type>VI</type>
<number>4100000000000001</number>
<expDate>1213</expDate>
</card>
<cardholderAuthentication>
<customerIpAddress>192.168.1.1</customerIpAddress>
<authenticationProtocolVersionType>PAP</authenticationProtocolVersionType>
</cardholderAuthentication>
</authorization>";

            Assert.AreEqual(Regex.Replace(expectedResult, @"\s+", string.Empty), Regex.Replace(auth.Serialize(), @"\s+", string.Empty));

            var mock = new Mock<Communications>();
            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<authorization id=\".*>.*<customerIpAddress>192.168.1.1</customerIpAddress>.*</authorization>.*", RegexOptions.Singleline) ))
                .Returns(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><authorizationResponse><cnpTxnId>123</cnpTxnId></authorizationResponse></cnpOnlineResponse>")});

            var mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.Authorize(auth);

            var authorizationResponse = cnp.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.cnpTxnId);
        }

        [Test]
        public void TestSimpleAuthWithBillMeLaterRequest()
        {
            var auth = new authorization();
            auth.card = new cardType();
            auth.card.type = methodOfPaymentTypeEnum.VI;
            auth.card.number = "4100000000000001";
            auth.card.expDate = "1213";
            auth.orderId = "12344";
            auth.amount = 2;
            auth.orderSource = orderSourceType.ecommerce;
            auth.billMeLaterRequest = new billMeLaterRequest();
            auth.billMeLaterRequest.virtualAuthenticationKeyData = "Data";
            auth.billMeLaterRequest.virtualAuthenticationKeyPresenceIndicator = "Presence";

            var expectedResult = @"
<authorization id="""" reportGroup="""">
<orderId>12344</orderId>
<amount>2</amount>
<orderSource>ecommerce</orderSource>
<card>
<type>VI</type>
<number>4100000000000001</number>
<expDate>1213</expDate>
</card>
<billMeLaterRequest>
<virtualAuthenticationKeyPresenceIndicator>Presence</virtualAuthenticationKeyPresenceIndicator>
<virtualAuthenticationKeyData>Data</virtualAuthenticationKeyData>
</billMeLaterRequest>
</authorization>";

            Assert.AreEqual(Regex.Replace(expectedResult, @"\s+", string.Empty), Regex.Replace(auth.Serialize(), @"\s+", string.Empty));

            var mock = new Mock<Communications>();
            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<authorization id=\".*>.*<billMeLaterRequest>\r\n<virtualAuthenticationKeyPresenceIndicator>Presence</virtualAuthenticationKeyPresenceIndicator>\r\n<virtualAuthenticationKeyData>Data</virtualAuthenticationKeyData>\r\n</billMeLaterRequest>.*</authorization>.*", RegexOptions.Singleline) ))
                .Returns(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><authorizationResponse><cnpTxnId>123</cnpTxnId></authorizationResponse></cnpOnlineResponse>")});

            var mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.Authorize(auth);

            var authorizationResponse = cnp.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.cnpTxnId);
        }

        [Test]
        public void TestAuthWithAdvancedFraud()
        {
            var auth = new authorization();
            auth.orderId = "123";
            auth.amount = 10;
            auth.advancedFraudChecks = new advancedFraudChecksType();
            auth.advancedFraudChecks.threatMetrixSessionId = "800";
            auth.advancedFraudChecks.customAttribute1 = "testAttribute1";
            auth.advancedFraudChecks.customAttribute2 = "testAttribute2";
            auth.advancedFraudChecks.customAttribute3 = "testAttribute3";
            auth.advancedFraudChecks.customAttribute4 = "testAttribute4";
            auth.advancedFraudChecks.customAttribute5 = "testAttribute5";


            var expectedResult = @"
<authorization id="""" reportGroup="""">
<orderId>123</orderId>
<amount>10</amount>
<advancedFraudChecks>
<threatMetrixSessionId>800</threatMetrixSessionId>
<customAttribute1>testAttribute1</customAttribute1>
<customAttribute2>testAttribute2</customAttribute2>
<customAttribute3>testAttribute3</customAttribute3>
<customAttribute4>testAttribute4</customAttribute4>
<customAttribute5>testAttribute5</customAttribute5>
</advancedFraudChecks>
</authorization>";
            var test = auth.Serialize();
            Assert.AreEqual(Regex.Replace(expectedResult, @"\s+", string.Empty), Regex.Replace(auth.Serialize(), @"\s+", string.Empty));

            var mock = new Mock<Communications>();
            mock.Setup(Communications => Communications.HttpPost(It.IsAny<string>() ))
                .Returns(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent("<cnpOnlineResponse version='8.23' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><authorizationResponse><orderId>123</orderId><fraudResult><advancedFraudResults><deviceReviewStatus>\"ReviewStatus\"</deviceReviewStatus><deviceReputationScore>800</deviceReputationScore></advancedFraudResults></fraudResult></authorizationResponse></cnpOnlineResponse>")});

            var mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var authorizationResponse = cnp.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual("123", authorizationResponse.orderId);
        }

        [Test]
        public void TestAdvancedFraudResponse()
        {
            var xmlResponse = @"<cnpOnlineResponse version='8.23' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'>
<authorizationResponse>
<cnpTxnId>123</cnpTxnId>
<fraudResult>
<advancedFraudResults>
<deviceReviewStatus>ReviewStatus</deviceReviewStatus>
<deviceReputationScore>800</deviceReputationScore>
<triggeredRule>rule triggered</triggeredRule>
<triggeredRule>rule triggered 2</triggeredRule>
</advancedFraudResults>
</fraudResult>
</authorizationResponse>
</cnpOnlineResponse>";

            var cnpOnlineResponse = CnpOnline.DeserializeObject(xmlResponse);
            var authorizationResponse = (authorizationResponse)cnpOnlineResponse.authorizationResponse;


            Assert.AreEqual(123, authorizationResponse.cnpTxnId);
            Assert.NotNull(authorizationResponse.fraudResult);
            Assert.NotNull(authorizationResponse.fraudResult.advancedFraudResults);
            Assert.NotNull(authorizationResponse.fraudResult.advancedFraudResults.deviceReviewStatus);
            Assert.AreEqual("ReviewStatus", authorizationResponse.fraudResult.advancedFraudResults.deviceReviewStatus);
            Assert.NotNull(authorizationResponse.fraudResult.advancedFraudResults.deviceReputationScore);
            Assert.AreEqual(800, authorizationResponse.fraudResult.advancedFraudResults.deviceReputationScore);
            Assert.AreEqual("rule triggered", authorizationResponse.fraudResult.advancedFraudResults.triggeredRule[0]);
            Assert.AreEqual("rule triggered 2", authorizationResponse.fraudResult.advancedFraudResults.triggeredRule[1]);
        }

        [Test]
        public void TestAuthWithPosCatLevelEnum()
        {
            var auth = new authorization();
            auth.pos = new pos();
            auth.orderId = "ABC123";
            auth.amount = 98700;
            auth.pos.catLevel = posCatLevelEnum.selfservice;

            var expectedResult = @"
<authorization id="""" reportGroup="""">
<orderId>ABC123</orderId>
<amount>98700</amount>
<pos>
<catLevel>self service</catLevel>
</pos>
</authorization>";

            Assert.AreEqual(Regex.Replace(expectedResult, @"\s+", string.Empty), Regex.Replace(auth.Serialize(), @"\s+", string.Empty));

            var mock = new Mock<Communications>();
            mock.Setup(Communications => Communications.HttpPost(It.IsAny<string>() ))
                .Returns(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent("<cnpOnlineResponse version='8.23' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><authorizationResponse><cnpTxnId>123</cnpTxnId></authorizationResponse></cnpOnlineResponse>")});

            var mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var authorizationResponse = cnp.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.cnpTxnId);
        }

        [Test]
        public void TestRecycleEngineActive()
        {
            var xmlResponse = @"<cnpOnlineResponse version='8.23' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'>
<authorizationResponse>
<cnpTxnId>123</cnpTxnId>
<fraudResult>
<advancedFraudResults>
<deviceReviewStatus>ReviewStatus</deviceReviewStatus>
<deviceReputationScore>800</deviceReputationScore>
<triggeredRule>rule triggered</triggeredRule>
</advancedFraudResults>
</fraudResult>
<recyclingResponse>
<recycleEngineActive>1</recycleEngineActive>
</recyclingResponse>
</authorizationResponse>
</cnpOnlineResponse>";

            var cnpOnlineResponse = CnpOnline.DeserializeObject(xmlResponse);
            var authorizationResponse = (authorizationResponse)cnpOnlineResponse.authorizationResponse;


            Assert.AreEqual(123, authorizationResponse.cnpTxnId);
            Assert.NotNull(authorizationResponse.fraudResult);
            Assert.NotNull(authorizationResponse.fraudResult.advancedFraudResults);
            Assert.NotNull(authorizationResponse.fraudResult.advancedFraudResults.deviceReviewStatus);
            Assert.AreEqual("ReviewStatus", authorizationResponse.fraudResult.advancedFraudResults.deviceReviewStatus);
            Assert.NotNull(authorizationResponse.fraudResult.advancedFraudResults.deviceReputationScore);
            Assert.AreEqual(800, authorizationResponse.fraudResult.advancedFraudResults.deviceReputationScore);
            Assert.AreEqual("rule triggered", authorizationResponse.fraudResult.advancedFraudResults.triggeredRule[0]);
            Assert.AreEqual(true, authorizationResponse.recyclingResponse.recycleEngineActive);
        }

        [Test]
        public void TestOriginalTransaction()
        {
            var auth = new authorization();
            auth.originalNetworkTransactionId = "123456789";
            auth.originalTransactionAmount = 12;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<originalNetworkTransactionId>123456789</originalNetworkTransactionId>.*?<originalTransactionAmount>12</originalTransactionAmount>.*?", RegexOptions.Singleline) ))
                .Returns(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent("<cnpOnlineResponse version='8.18' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><authorizationResponse><cnpTxnId>123</cnpTxnId></authorizationResponse></cnpOnlineResponse>")});

            var mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var authorizationResponse = cnp.Authorize(auth);
            Assert.AreEqual(123, authorizationResponse.cnpTxnId);
        }

        [Test]
        public void TestOriginalTransactionWithPin()
        {
            var auth = new authorization();
            auth.originalNetworkTransactionId = "123456789";
            auth.originalTransactionAmount = 12;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.MC;
            card.number = "414100000000000000";
            card.expDate = "1210";
            card.pin = "1234";
            auth.card = card;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<card>\r\n<type>MC</type>\r\n<number>414100000000000000</number>\r\n<expDate>1210</expDate>\r\n<pin>1234</pin>\r\n</card>.*", RegexOptions.Singleline) ))
               .Returns(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent("<cnpOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><authorizationResponse><cnpTxnId>123</cnpTxnId></authorizationResponse></cnpOnlineResponse>")});

            var mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var authorizationResponse = cnp.Authorize(auth);
            Assert.AreEqual(123, authorizationResponse.cnpTxnId);
        }

        [Test]
        public void TestAuthWithMCC()
        {
            var auth = new authorization();
            auth.orderId = "12344";
            auth.amount = 2;
            auth.merchantCategoryCode = "0111";
            auth.orderSource = orderSourceType.ecommerce;
            auth.reportGroup = "Planets";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<orderSource>ecommerce</orderSource>\r\n<merchantCategoryCode>0111</merchantCategoryCode>.*", RegexOptions.Singleline)))
                .Returns(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><authorizationResponse><cnpTxnId>123</cnpTxnId></authorizationResponse></cnpOnlineResponse>")});

            var mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var authorizationResponse = cnp.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.cnpTxnId);
        }
        
        [Test]
        public void TestAuthorizationWithLocation()
        {
            var auth = new authorization();
            auth.orderId = "12344";
            auth.amount = 2;
            auth.secondaryAmount = 1;
            auth.orderSource = orderSourceType.ecommerce;
            auth.reportGroup = "Planets";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<secondaryAmount>1</secondaryAmount>\r\n<orderSource>ecommerce</orderSource>.*", RegexOptions.Singleline)))
                .Returns(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><authorizationResponse><cnpTxnId>123</cnpTxnId><location>sandbox</location></authorizationResponse></cnpOnlineResponse>")});

            var mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var authorizationResponse = cnp.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.cnpTxnId);
            Assert.AreEqual("sandbox", authorizationResponse.location);
        }

        [Test]
        public void TestSimpleAuthWithRetailerAddressAndAdditionalCOFdata() ///new testcase 12.24
        {
            var auth = new authorization();
            auth.orderId = "12344";
            auth.amount = 2;
            auth.orderSource = orderSourceType.ecommerce;
            auth.reportGroup = "Planets";
            auth.id = "thisisid";
            auth.businessIndicator = businessIndicatorEnum.fundTransfer;
            auth.crypto = false;
            auth.orderChannel = orderChannelEnum.PHONE;
            auth.fraudCheckStatus = "Not Approved";

            var retailerAddress = new contact();
            retailerAddress.name = "Mikasa Ackerman";
            retailerAddress.addressLine1 = "1st Main Street";
            retailerAddress.city = "Burlington";
            retailerAddress.state = "MA";
            retailerAddress.country = countryTypeEnum.USA;
            retailerAddress.email = "mikasa@cnp.com";
            retailerAddress.zip = "01867-4456";
            retailerAddress.sellerId = "s1234";
            retailerAddress.url = "www.google.com";
            auth.retailerAddress = retailerAddress;

            var additionalCOFData = new additionalCOFData();
            additionalCOFData.totalPaymentCount = "35";
            additionalCOFData.paymentType = paymentTypeEnum.Fixed_Amount;
            additionalCOFData.uniqueId = "12345wereew233";
            additionalCOFData.frequencyOfMIT = frequencyOfMITEnum.BiWeekly;
            additionalCOFData.validationReference = "re3298rhriw4wrw";
            additionalCOFData.sequenceIndicator = 2;

            auth.additionalCOFData = additionalCOFData;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<zip>01867-4456</zip>.*<email>mikasa@cnp.com</email>.*<sellerId>s1234</sellerId>.*<url>www.google.com</url>.*<frequencyOfMIT>BiWeekly</frequencyOfMIT>.*<orderChannel>PHONE</orderChannel>\r\n<fraudCheckStatus>Not Approved</fraudCheckStatus>\r\n<crypto>false</crypto>.*", RegexOptions.Singleline)))
                .Returns(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><authorizationResponse><cnpTxnId>123</cnpTxnId></authorizationResponse></cnpOnlineResponse>")});

            var mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var authorizationResponse = cnp.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.cnpTxnId);
        }

        [Test]
        public void TestAuthWithFsErrorCode_OverridePolicy_ProdEnrolled_MercAcctStatus_FraudSwitchInd_Deci_Purpose() ///new testcase 12.25
        {
            var auth = new authorization();
            auth.orderId = "12344";
            auth.amount = 2;
            auth.orderSource = orderSourceType.ecommerce;
            auth.reportGroup = "Planets";
            auth.id = "thisisid";
            auth.businessIndicator = businessIndicatorEnum.fundTransfer;
            auth.crypto = false;
            auth.orderChannel = orderChannelEnum.PHONE;
            auth.fraudCheckStatus = "Not Approved";

            var retailerAddress = new contact();
            retailerAddress.name = "Mikasa Ackerman";
            retailerAddress.addressLine1 = "1st Main Street";
            retailerAddress.city = "Burlington";
            retailerAddress.state = "MA";
            retailerAddress.country = countryTypeEnum.USA;
            retailerAddress.email = "mikasa@cnp.com";
            retailerAddress.zip = "01867-4456";
            retailerAddress.sellerId = "s1234";
            retailerAddress.url = "www.google.com";
            auth.retailerAddress = retailerAddress;

            var additionalCOFData = new additionalCOFData();
            additionalCOFData.totalPaymentCount = "35";
            additionalCOFData.paymentType = paymentTypeEnum.Fixed_Amount;
            additionalCOFData.uniqueId = "12345wereew233";
            additionalCOFData.frequencyOfMIT = frequencyOfMITEnum.BiWeekly;
            additionalCOFData.validationReference = "re3298rhriw4wrw";
            additionalCOFData.sequenceIndicator = 2;

            auth.additionalCOFData = additionalCOFData;
            auth.overridePolicy = "FIS Policy";
            auth.fsErrorCode = "Not Applicable";
            auth.merchantAccountStatus = "Active";
            auth.productEnrolled = productEnrolledEnum.GUARPAY1;
            auth.decisionPurpose = decisionPurposeEnum.INFORMATION_ONLY;
            auth.fraudSwitchIndicator = fraudSwitchIndicatorEnum.PRE;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<zip>01867-4456</zip>.*<email>mikasa@cnp.com</email>.*<sellerId>s1234</sellerId>.*<url>www.google.com</url>.*<frequencyOfMIT>BiWeekly</frequencyOfMIT>.*<orderChannel>PHONE</orderChannel>\r\n<fraudCheckStatus>Not Approved</fraudCheckStatus>\r\n<crypto>false</crypto>.*", RegexOptions.Singleline)))
                .Returns(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><authorizationResponse><cnpTxnId>123</cnpTxnId></authorizationResponse></cnpOnlineResponse>")});

            var mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var authorizationResponse = cnp.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.cnpTxnId);
        }

        [Test]
        public void  AuthWithLodgingInfoPropertyAddressChanges() ///new testcase 12.25
        {
            var auth = new authorization();
            auth.orderId = "12344";
            auth.amount = 2;
            auth.orderSource = orderSourceType.ecommerce;
            auth.reportGroup = "Planets";
            auth.id = "thisisid";
            auth.businessIndicator = businessIndicatorEnum.fundTransfer;
            auth.crypto = false;
            auth.orderChannel = orderChannelEnum.PHONE;
            auth.fraudCheckStatus = "Not Approved";

            var retailerAddress = new contact();
            retailerAddress.name = "Mikasa Ackerman";
            retailerAddress.addressLine1 = "1st Main Street";
            retailerAddress.city = "Burlington";
            retailerAddress.state = "MA";
            retailerAddress.country = countryTypeEnum.USA;
            retailerAddress.email = "mikasa@cnp.com";
            retailerAddress.zip = "01867-4456";
            retailerAddress.sellerId = "s1234";
            retailerAddress.url = "www.google.com";
            auth.retailerAddress = retailerAddress;

            var additionalCOFData = new additionalCOFData();
            additionalCOFData.totalPaymentCount = "35";
            additionalCOFData.paymentType = paymentTypeEnum.Fixed_Amount;
            additionalCOFData.uniqueId = "12345wereew233";
            additionalCOFData.frequencyOfMIT = frequencyOfMITEnum.BiWeekly;
            additionalCOFData.validationReference = "re3298rhriw4wrw";
            additionalCOFData.sequenceIndicator = 2;

            auth.additionalCOFData = additionalCOFData;

            var lodgingInfo = new lodgingInfo();
            lodgingInfo.bookingID = "13";
            lodgingInfo.passengerName = "Test";
            var propertyAddress = new propertyAddress();
            propertyAddress.name = "si";
            propertyAddress.city = "MA";
            propertyAddress.region = "MH";
            propertyAddress.country = countryTypeEnum.USA;
            lodgingInfo.propertyAddress = propertyAddress;
            lodgingInfo.travelPackageIndicator = travelPackageIndicatorEnum.Both;
            lodgingInfo.smokingPreference = "";
            lodgingInfo.numberOfRooms = 1;
            lodgingInfo.tollFreePhoneNumber = "1334444";
            auth.lodgingInfo = lodgingInfo;
            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<zip>01867-4456</zip>.*<email>mikasa@cnp.com</email>.*<sellerId>s1234</sellerId>.*<url>www.google.com</url>.*<frequencyOfMIT>BiWeekly</frequencyOfMIT>.*<orderChannel>PHONE</orderChannel>\r\n<fraudCheckStatus>Not Approved</fraudCheckStatus>\r\n<crypto>false</crypto>.*", RegexOptions.Singleline)))
                .Returns(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><authorizationResponse><cnpTxnId>123</cnpTxnId></authorizationResponse></cnpOnlineResponse>")});

            var mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var authorizationResponse = cnp.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.cnpTxnId);
        }

        [Test]
        public void AuthWithPassengerTransportDataChanges() //new testcase for 12.26
        {
            var auth = new authorization();
            auth.orderId = "12344";
            auth.amount = 2;
            auth.orderSource = orderSourceType.ecommerce;
            auth.reportGroup = "Planets";
            auth.id = "thisisid";
            auth.businessIndicator = businessIndicatorEnum.fundTransfer;
            auth.crypto = false;
            auth.orderChannel = orderChannelEnum.PHONE;
            auth.fraudCheckStatus = "Not Approved";

            var passengerTransportData = new passengerTransportData();
            passengerTransportData.passengerName = "Pia Jaiswal";
            passengerTransportData.ticketNumber = "TR0001";
            passengerTransportData.issuingCarrier = "IC";
            passengerTransportData.carrierName = "Indigo";
            passengerTransportData.restrictedTicketIndicator = "TI2022";
            passengerTransportData.numberOfAdults = 1;
            passengerTransportData.numberOfChildren = 1;
            passengerTransportData.customerCode = "C2011583";
            passengerTransportData.arrivalDate = new System.DateTime(2022, 12, 31);
            passengerTransportData.issueDate = new System.DateTime(2022, 12, 25);
            passengerTransportData.travelAgencyCode = "TAC12345";
            passengerTransportData.travelAgencyName = "Yatra";
            passengerTransportData.computerizedReservationSystem = computerizedReservationSystemEnum.STRT;
            passengerTransportData.creditReasonIndicator = creditReasonIndicatorEnum.A;
            passengerTransportData.ticketChangeIndicator = ticketChangeIndicatorEnum.C;
            passengerTransportData.ticketIssuerAddress = "Hinjewadi";
            passengerTransportData.exchangeTicketNumber = "ETN12345";
            passengerTransportData.exchangeAmount = 12300;
            passengerTransportData.exchangeFeeAmount = 11000;

            var tripLegData = new tripLegData();
            tripLegData.tripLegNumber = 12;
            tripLegData.departureCode = "DC";
            tripLegData.carrierCode = "CC";
            tripLegData.serviceClass = serviceClassEnum.First;
            tripLegData.stopOverCode = "N";
            tripLegData.destinationCode = "DC111";
            tripLegData.fareBasisCode = "FBC12345";
            tripLegData.departureDate = new System.DateTime(2023, 1, 31);
            tripLegData.originCity = "Pune";
            tripLegData.travelNumber = "TN111";
            tripLegData.departureTime = "13:05";
            tripLegData.arrivalTime = "16:10";
            tripLegData.remarks = "NA";
            passengerTransportData.tripLegData = tripLegData;
            auth.passengerTransportData = passengerTransportData;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<ticketNumber>TR0001</ticketNumber>.*<issuingCarrier>IC</issuingCarrier>.*<carrierName>Indigo</carrierName>.*<restrictedTicketIndicator>TI2022</restrictedTicketIndicator>.*<numberOfAdults>1</numberOfAdults>.*<numberOfChildren>1</numberOfChildren>\r\n<customerCode>C2011583</customerCode>.*", RegexOptions.Singleline)))
                .Returns(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><authorizationResponse><cnpTxnId>123</cnpTxnId></authorizationResponse></cnpOnlineResponse>")});

            var mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var authorizationResponse = cnp.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.cnpTxnId);
        }
    }
}
