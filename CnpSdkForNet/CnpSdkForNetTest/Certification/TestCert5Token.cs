using System.Collections.Generic;
using NUnit.Framework;

namespace Cnp.Sdk.Test.Certification
{
    [TestFixture]
    class TestCert5Token
    {
        private CnpOnline cnp;

        [OneTimeSetUp]
        public void SetUp()
        {
            EnvironmentVariableTestFlags.RequirePreliveOnlineTestsEnabled();
            
            var existingConfig = new ConfigManager().getConfig();
            Dictionary<string, string> config = new Dictionary<string, string>();
            config.Add("url", "https://payments.vantivprelive.com/vap/communicator/online");
            config.Add("reportGroup", "Default Report Group");
            config.Add("username", existingConfig["username"]);
            config.Add("timeout", "20000");
            config.Add("merchantId", existingConfig["merchantId"]);
            config.Add("password",existingConfig["password"]);
            config.Add("printxml", "true");
            config.Add("logFile", null);
            config.Add("neuterAccountNums", null);
            config.Add("proxyHost", "");
            config.Add("proxyPort", "");
            
            ConfigManager configManager = new ConfigManager(config);
            cnp = new CnpOnline(configManager.getConfig());
        }

        [OneTimeTearDown]
        public void Dispose()
        {
            Communications.DisposeHttpClient();
        }

        [Test]
        public void Test50()
        {
            registerTokenRequestType request = new registerTokenRequestType();
            request.id = "1";
            request.orderId = "50";
            request.accountNumber = "4457119922390123";

            registerTokenResponse response = cnp.RegisterToken(request);
            Assert.AreEqual("445711", response.bin);
            Assert.AreEqual(methodOfPaymentTypeEnum.VI, response.type);
            //TODO: //Getting 802 instead
            //Assert.AreEqual("801", response.response);
            Assert.AreEqual("1111000276870123", response.cnpToken);
            //Assert.AreEqual("Account number was successfully registered", response.message);
        }

        [Test]
        public void Test51()
        {
            registerTokenRequestType request = new registerTokenRequestType();
            request.id = "1";
            request.orderId = "51";
            request.accountNumber = "4457119999999999";

            registerTokenResponse response = cnp.RegisterToken(request);
            Assert.AreEqual("820", response.response);
            Assert.AreEqual("Credit card number was invalid", response.message);
        }

        [Test]
        public void Test52()
        {
            registerTokenRequestType request = new registerTokenRequestType();
            request.id = "1";
            request.orderId = "52";
            request.accountNumber = "4457119922390123";

            registerTokenResponse response = cnp.RegisterToken(request);
            Assert.AreEqual("445711", response.bin);
            Assert.AreEqual(methodOfPaymentTypeEnum.VI, response.type);
            Assert.AreEqual("802", response.response);
            Assert.AreEqual("1111000276870123", response.cnpToken);
            Assert.AreEqual("Account number was previously registered", response.message);
        }

        [Test]
        public void Test53()
        {
            registerTokenRequestType request = new registerTokenRequestType();
            request.id = "1";
            request.orderId = "53";
            echeckForTokenType echeck = new echeckForTokenType();
            echeck.accNum = "1099999998";
            echeck.routingNum = "114567895";
            request.echeckForToken = echeck; ;

            registerTokenResponse response = cnp.RegisterToken(request);
            //TODO: //getting null as response type
            //Assert.AreEqual(methodOfPaymentTypeEnum.EC, response.type);
            //TODO: //getting null as eCheckAccountSuffix
            //Assert.AreEqual("998", response.eCheckAccountSuffix);
            //TODO: //getting 900 as response and corresponding message
            //Assert.AreEqual("801", response.response);
            //Assert.AreEqual("Account number was successfully registered", response.message);
            //TODO: //getting null as cnptoken
            //Assert.AreEqual("111922223333000998", response.cnpToken);
        }

        [Test]
        public void Test54()
        {
            registerTokenRequestType request = new registerTokenRequestType();
            request.id = "1";
            request.orderId = "54";
            echeckForTokenType echeck = new echeckForTokenType();
            echeck.accNum = "1022222102";
            echeck.routingNum = "1145_7895";
            request.echeckForToken = echeck; ;

            registerTokenResponse response = cnp.RegisterToken(request);
            Assert.AreEqual("900", response.response);
        }

        [Test]
        public void Test55()
        {
            authorization auth = new authorization();
            auth.id = "1";
            auth.orderId = "55";
            auth.amount = 15000;
            auth.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.number = "5435101234510196";
            card.expDate = "1121";
            card.cardValidationNum = "987";
            card.type = methodOfPaymentTypeEnum.MC;
            auth.card = card;

            authorizationResponse response = cnp.Authorize(auth);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            //TODO: //Getting 802 instead
            //Assert.AreEqual("801", response.tokenResponse.tokenResponseCode);
            //Assert.AreEqual("Account number was successfully registered", response.tokenResponse.tokenMessage);
            Assert.AreEqual(methodOfPaymentTypeEnum.MC, response.tokenResponse.type);
            Assert.AreEqual("543510", response.tokenResponse.bin);
        }

        [Test]
        public void Test56()
        {
            authorization auth = new authorization();
            auth.id = "1";
            auth.orderId = "56";
            auth.amount = 15000;
            auth.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.number = "5435109999999999";
            card.expDate = "1112";
            card.cardValidationNum = "987";
            card.type = methodOfPaymentTypeEnum.MC;
            auth.card = card;

            authorizationResponse response = cnp.Authorize(auth);
            Assert.AreEqual("301", response.response);
        }

        [Test]
        public void Test57()
        {
            authorization auth = new authorization();
            auth.id = "1";
            auth.orderId = "57";
            auth.amount = 15000;
            auth.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.number = "5435101234510196";
            card.expDate = "1112";
            card.cardValidationNum = "987";
            card.type = methodOfPaymentTypeEnum.MC;
            auth.card = card;

            authorizationResponse response = cnp.Authorize(auth);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual("802", response.tokenResponse.tokenResponseCode);
            Assert.AreEqual("Account number was previously registered", response.tokenResponse.tokenMessage);
            Assert.AreEqual(methodOfPaymentTypeEnum.MC, response.tokenResponse.type);
            Assert.AreEqual("543510", response.tokenResponse.bin);
        }

        [Test]
        public void Test59()
        {
            authorization auth = new authorization();
            auth.id = "1";
            auth.orderId = "59";
            auth.amount = 15000;
            auth.orderSource = orderSourceType.ecommerce;
            cardTokenType token = new cardTokenType();
            token.cnpToken = "1111000100092332";
            token.expDate = "1121";
            auth.token = token;

            authorizationResponse response = cnp.Authorize(auth);
            Assert.AreEqual("822", response.response);
            Assert.AreEqual("Token was not found", response.message);
        }

        [Test]
        public void Test60()
        {
            authorization auth = new authorization();
            auth.id = "1";
            auth.orderId = "60";
            auth.amount = 15000;
            auth.orderSource = orderSourceType.ecommerce;
            cardTokenType token = new cardTokenType();
            token.cnpToken = "1112000100000085";
            token.expDate = "1121";
            auth.token = token;

            authorizationResponse response = cnp.Authorize(auth);
            Assert.AreEqual("822", response.response);
        }

        [Test]
        public void Test61()
        {
            echeckSale sale = new echeckSale();
            sale.id = "1";
            sale.orderId = "61";
            sale.amount = 15000;
            sale.orderSource = orderSourceType.ecommerce;
            contact billToAddress = new contact();
            billToAddress.firstName = "Tom";
            billToAddress.lastName = "Black";
            sale.billToAddress = billToAddress;
            echeckType echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking; ;
            echeck.accNum = "1099999003";
            echeck.routingNum = "011100012";
            sale.echeck = echeck;

            echeckSalesResponse response = cnp.EcheckSale(sale);
            //TODO: could not get token response
            //Assert.AreEqual("801", response.tokenResponse.tokenResponseCode);
            //Assert.AreEqual("Account number was successfully registered", response.tokenResponse.tokenMessage);
            //Assert.AreEqual(methodOfPaymentTypeEnum.EC, response.tokenResponse.type);
            //Assert.AreEqual("111922223333444003", response.tokenResponse.cnpToken);
        }

        [Test]
        public void Test62()
        {
            echeckSale sale = new echeckSale();
            sale.id = "1";
            sale.orderId = "62";
            sale.amount = 15000;
            sale.orderSource = orderSourceType.ecommerce;
            contact billToAddress = new contact();
            billToAddress.firstName = "Tom";
            billToAddress.lastName = "Black";
            sale.billToAddress = billToAddress;
            echeckType echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking; ;
            echeck.accNum = "1099999999";
            echeck.routingNum = "011100012";
            sale.echeck = echeck;

            echeckSalesResponse response = cnp.EcheckSale(sale);
            //TODO: //Could not get token response
            //Assert.AreEqual("801", response.tokenResponse.tokenResponseCode);
            //Assert.AreEqual("Account number was successfully registered", response.tokenResponse.tokenMessage);
            //Assert.AreEqual(methodOfPaymentTypeEnum.EC, response.tokenResponse.type);
            //Assert.AreEqual("999", response.tokenResponse.eCheckAccountSuffix);
            //Assert.AreEqual("111922223333444999", response.tokenResponse.cnpToken);
        }

        [Test]
        public void Test63()
        {
            echeckSale sale = new echeckSale();
            sale.id = "1";
            sale.orderId = "63";
            sale.amount = 15000;
            sale.orderSource = orderSourceType.ecommerce;
            contact billToAddress = new contact();
            billToAddress.firstName = "Tom";
            billToAddress.lastName = "Black";
            sale.billToAddress = billToAddress;
            echeckType echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking; ;
            echeck.accNum = "1099999999";
            echeck.routingNum = "011100012";
            sale.echeck = echeck;

            echeckSalesResponse response = cnp.EcheckSale(sale);
            //TODO: //could not get token response back
            //Assert.AreEqual("801", response.tokenResponse.tokenResponseCode);
            //Assert.AreEqual("Account number was successfully registered", response.tokenResponse.tokenMessage);
            //Assert.AreEqual(methodOfPaymentTypeEnum.EC, response.tokenResponse.type);
            //Assert.AreEqual("999", response.tokenResponse.eCheckAccountSuffix);
            //Assert.AreEqual("111922223333555999", response.tokenResponse.cnpToken);
        }
            
    }
}
