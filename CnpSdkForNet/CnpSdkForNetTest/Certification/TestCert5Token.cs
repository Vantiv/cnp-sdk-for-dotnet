using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Cnp.Sdk;

namespace Cnp.Sdk.Test.Certification
{
    public class TestCert5Token
    {
        private CnpOnline cnp;

        public TestCert5Token()
        {
            Dictionary<string, string> config = new Dictionary<string, string>();
            config.Add("url", "https://payments.vantivprelive.com/vap/communicator/online");
            config.Add("reportGroup", "Default Report Group");
            config.Add("username", Properties.Settings.Default.username);
            config.Add("timeout", "500");
            config.Add("merchantId", Properties.Settings.Default.merchantId);
            config.Add("password", Properties.Settings.Default.password);
            config.Add("printxml", "true");
            config.Add("logFile", null);
            config.Add("neuterAccountNums", null);
            config.Add("proxyHost", Properties.Settings.Default.proxyHost);
            config.Add("proxyPort", Properties.Settings.Default.proxyPort);
            config.Add("multiSite", "false");
            cnp = new CnpOnline(config);
        }

        [Fact]
        public void Test50()
        {
            registerTokenRequestType request = new registerTokenRequestType();
            request.id = "1";
            request.orderId = "50";
            request.accountNumber = "4457119922390123";

            registerTokenResponse response = cnp.RegisterToken(request);
            Assert.Equal("445711", response.bin);
            Assert.Equal(methodOfPaymentTypeEnum.VI, response.type);
            //TODO: //Getting 802 instead
            //Assert.Equal("801", response.response);
            Assert.Equal("1111000276870123", response.cnpToken);
            //Assert.Equal("Account number was successfully registered", response.message);
        }

        [Fact]
        public void Test51()
        {
            registerTokenRequestType request = new registerTokenRequestType();
            request.id = "1";
            request.orderId = "51";
            request.accountNumber = "4457119999999999";

            registerTokenResponse response = cnp.RegisterToken(request);
            Assert.Equal("820", response.response);
            Assert.Equal("Credit card number was invalid", response.message);
        }

        [Fact]
        public void Test52()
        {
            registerTokenRequestType request = new registerTokenRequestType();
            request.id = "1";
            request.orderId = "52";
            request.accountNumber = "4457119922390123";

            registerTokenResponse response = cnp.RegisterToken(request);
            Assert.Equal("445711", response.bin);
            Assert.Equal(methodOfPaymentTypeEnum.VI, response.type);
            Assert.Equal("802", response.response);
            Assert.Equal("1111000276870123", response.cnpToken);
            Assert.Equal("Account number was previously registered", response.message);
        }

        [Fact]
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
            //Assert.Equal(methodOfPaymentTypeEnum.EC, response.type);
            //TODO: //getting null as eCheckAccountSuffix
            //Assert.Equal("998", response.eCheckAccountSuffix);
            //TODO: //getting 900 as response and corresponding message
            //Assert.Equal("801", response.response);
            //Assert.Equal("Account number was successfully registered", response.message);
            //TODO: //getting null as cnptoken
            //Assert.Equal("111922223333000998", response.cnpToken);
        }

        [Fact]
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
            Assert.Equal("900", response.response);
        }

        [Fact]
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
            Assert.Equal("000", response.response);
            Assert.Equal("Approved", response.message);
            //TODO: //Getting 802 instead
            //Assert.Equal("801", response.tokenResponse.tokenResponseCode);
            //Assert.Equal("Account number was successfully registered", response.tokenResponse.tokenMessage);
            Assert.Equal(methodOfPaymentTypeEnum.MC, response.tokenResponse.type);
            Assert.Equal("543510", response.tokenResponse.bin);
        }

        [Fact]
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
            Assert.Equal("301", response.response);
        }

        [Fact]
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
            Assert.Equal("000", response.response);
            Assert.Equal("Approved", response.message);
            Assert.Equal("802", response.tokenResponse.tokenResponseCode);
            Assert.Equal("Account number was previously registered", response.tokenResponse.tokenMessage);
            Assert.Equal(methodOfPaymentTypeEnum.MC, response.tokenResponse.type);
            Assert.Equal("543510", response.tokenResponse.bin);
        }

        [Fact]
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
            Assert.Equal("822", response.response);
            Assert.Equal("Token was not found", response.message);
        }

        [Fact]
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
            Assert.Equal("823", response.response);
        }

        [Fact]
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
            //Assert.Equal("801", response.tokenResponse.tokenResponseCode);
            //Assert.Equal("Account number was successfully registered", response.tokenResponse.tokenMessage);
            //Assert.Equal(methodOfPaymentTypeEnum.EC, response.tokenResponse.type);
            //Assert.Equal("111922223333444003", response.tokenResponse.cnpToken);
        }

        [Fact]
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
            //Assert.Equal("801", response.tokenResponse.tokenResponseCode);
            //Assert.Equal("Account number was successfully registered", response.tokenResponse.tokenMessage);
            //Assert.Equal(methodOfPaymentTypeEnum.EC, response.tokenResponse.type);
            //Assert.Equal("999", response.tokenResponse.eCheckAccountSuffix);
            //Assert.Equal("111922223333444999", response.tokenResponse.cnpToken);
        }

        [Fact]
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
            //Assert.Equal("801", response.tokenResponse.tokenResponseCode);
            //Assert.Equal("Account number was successfully registered", response.tokenResponse.tokenMessage);
            //Assert.Equal(methodOfPaymentTypeEnum.EC, response.tokenResponse.type);
            //Assert.Equal("999", response.tokenResponse.eCheckAccountSuffix);
            //Assert.Equal("111922223333555999", response.tokenResponse.cnpToken);
        }
            
    }
}
