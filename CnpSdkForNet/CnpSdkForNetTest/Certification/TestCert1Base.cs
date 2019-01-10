using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Cnp.Sdk;

namespace Cnp.Sdk.Test.Certification
{
    public class TestCert1Base
    {
        private CnpOnline cnp;

        public TestCert1Base()
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
            config.Add("multiSite", "true");
            cnp = new CnpOnline(config);
        }


        [Fact]
        public void Test1Auth()
        {
           
            authorization authorization = new authorization();
            authorization.id = "1";
            authorization.orderId = "1";
            authorization.amount = 10010;
            authorization.orderSource = orderSourceType.ecommerce;
            contact contact = new contact();
            contact.name = "John & Mary Smith";
            contact.addressLine1 = "1 Main St.";
            contact.city = "Burlington";
            contact.state = "MA";
            contact.zip = "01803-3747";
            contact.country = countryTypeEnum.US;
            authorization.billToAddress = contact;
            cardType card = new cardType();            
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4457010000000009";
            card.expDate = "0121";
            card.cardValidationNum = "349";
            authorization.card = card;

            authorizationResponse response = cnp.Authorize(authorization);
            Assert.Equal("000", response.response);
            Assert.Equal("Approved", response.message);
            Assert.Equal("11111 ", response.authCode);
            Assert.Equal("01", response.fraudResult.avsResult);
            Assert.Equal("M", response.fraudResult.cardValidationResult);

            capture capture = new capture();
            capture.id = response.id;
            capture.cnpTxnId = response.cnpTxnId;
            captureResponse captureResponse = cnp.Capture(capture);
            Assert.Equal("000", captureResponse.response);
            Assert.Equal("Approved", captureResponse.message);

            credit credit = new credit();
            credit.id = captureResponse.id;
            credit.cnpTxnId = captureResponse.cnpTxnId;
            creditResponse creditResponse = cnp.Credit(credit);
            Assert.Equal("000", creditResponse.response);
            Assert.Equal("Approved", creditResponse.message);

            voidTxn newvoid = new voidTxn();
            newvoid.id = creditResponse.id;
            newvoid.cnpTxnId = creditResponse.cnpTxnId;
            voidResponse voidResponse = cnp.DoVoid(newvoid);
            Assert.Equal("000", voidResponse.response);
            Assert.Equal("Approved", voidResponse.message);
        }

        [Fact]
        public void Test1AVS()
        {
            authorization authorization = new authorization();
            authorization.id = "1";
            authorization.orderId = "1";
            authorization.amount = 0;
            authorization.orderSource = orderSourceType.ecommerce;
            contact contact = new contact();
            contact.name = "John Smith";
            contact.addressLine1 = "1 Main St.";
            contact.city = "Burlington";
            contact.state = "MA";
            contact.zip = "01803-3747";
            contact.country = countryTypeEnum.US;
            authorization.billToAddress = contact;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4457010000000009";
            card.expDate = "0112";
            card.cardValidationNum = "349";
            authorization.card = card;

            authorizationResponse response = cnp.Authorize(authorization);
            Assert.Equal("000", response.response);
            Assert.Equal("Approved", response.message);
            Assert.Equal("11111 ", response.authCode);
            Assert.Equal("01", response.fraudResult.avsResult);
            Assert.Equal("M", response.fraudResult.cardValidationResult);
        }

        [Fact]
        public void Test1Sale()
        {
            sale sale = new sale();
            sale.id = "1";
            sale.orderId = "1";
            sale.amount = 10010;
            sale.orderSource = orderSourceType.ecommerce;
            contact contact = new contact();
            contact.name = "John Smith";
            contact.addressLine1 = "1 Main St.";
            contact.city = "Burlington";
            contact.state = "MA";
            contact.zip = "01803-3747";
            contact.country = countryTypeEnum.US;
            sale.billToAddress = contact;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4457010000000009";
            card.expDate = "0112";
            card.cardValidationNum = "349";
            sale.card = card;

            saleResponse response = cnp.Sale(sale);
            Assert.Equal("000", response.response);
            Assert.Equal("Approved", response.message);
            Assert.Equal("11111 ", response.authCode);
            Assert.Equal("01", response.fraudResult.avsResult);
            Assert.Equal("M", response.fraudResult.cardValidationResult);

            credit credit = new credit();
            credit.id = response.id;
            credit.cnpTxnId = response.cnpTxnId;
            creditResponse creditResponse = cnp.Credit(credit);
            Assert.Equal("000", creditResponse.response);
            Assert.Equal("Approved", creditResponse.message);


            voidTxn newvoid = new voidTxn();
            newvoid.id = creditResponse.id;
            newvoid.cnpTxnId = creditResponse.cnpTxnId;
            voidResponse voidResponse = cnp.DoVoid(newvoid);
            Assert.Equal("000",voidResponse.response);
            Assert.Equal("Approved", voidResponse.message);
        }

        [Fact]
        public void Test2Auth()
        {
            authorization authorization = new authorization();
            authorization.id = "1";
            authorization.orderId = "2";
            authorization.amount = 20020;
            authorization.orderSource = orderSourceType.ecommerce;
            contact contact = new contact();
            contact.name = "Mike J. Hammer";
            contact.addressLine1 = "2 Main St.";
            contact.addressLine2 = "Apt. 222";
            contact.city = "Riverside";
            contact.state = "RI";
            contact.zip = "02915";
            contact.country = countryTypeEnum.US;
            authorization.billToAddress = contact;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.MC;
            card.number = "5112010000000003";
            card.expDate = "0212";
            card.cardValidationNum = "261";
            authorization.card = card;
            fraudCheckType authenticationvalue = new fraudCheckType();
            authenticationvalue.authenticationValue = "BwABBJQ1AgAAAAAgJDUCAAAAAAA=";
            authorization.cardholderAuthentication = authenticationvalue;

            authorizationResponse response = cnp.Authorize(authorization);
            Assert.Equal("000", response.response);
            Assert.Equal("Approved", response.message);
            Assert.Equal("22222", response.authCode.Trim());
            Assert.Equal("10", response.fraudResult.avsResult);
            Assert.Equal("M", response.fraudResult.cardValidationResult);

            capture capture = new capture();
            capture.id = response.id;
            capture.cnpTxnId = response.cnpTxnId;
            captureResponse captureresponse = cnp.Capture(capture);
            Assert.Equal("000", captureresponse.response);
            Assert.Equal("Approved", captureresponse.message);

            credit credit = new credit();
            credit.id = captureresponse.id;
            credit.cnpTxnId = captureresponse.cnpTxnId;
            creditResponse creditResponse = cnp.Credit(credit);
            Assert.Equal("000", creditResponse.response);
            Assert.Equal("Approved", creditResponse.message);

            voidTxn newvoid = new voidTxn();
            newvoid.id = creditResponse.id;
            newvoid.cnpTxnId = creditResponse.cnpTxnId;
            voidResponse voidResponse = cnp.DoVoid(newvoid);
            Assert.Equal("000",voidResponse.response);
            Assert.Equal("Approved", voidResponse.message);
        }

        [Fact]
        public void Test2AVS()
        {
            authorization authorization = new authorization();
            authorization.id = "1";
            authorization.orderId = "2";
            authorization.amount = 0;
            authorization.orderSource = orderSourceType.ecommerce;
            contact contact = new contact();
            contact.name = "Mike J. Hammer";
            contact.addressLine1 = "2 Main St.";
            contact.addressLine2 = "Apt. 222";
            contact.city = "Riverside";
            contact.state = "RI";
            contact.zip = "02915";
            contact.country = countryTypeEnum.US;
            authorization.billToAddress = contact;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.MC;
            card.number = "5112010000000003";
            card.expDate = "0212";
            card.cardValidationNum = "261";
            authorization.card = card;
            fraudCheckType authenticationvalue = new fraudCheckType();
            authenticationvalue.authenticationValue = "BwABBJQ1AgAAAAAgJDUCAAAAAAA=";
            authorization.cardholderAuthentication = authenticationvalue;

            authorizationResponse response = cnp.Authorize(authorization);
            Assert.Equal("000", response.response);
            Assert.Equal("Approved", response.message);
            Assert.Equal("22222", response.authCode.Trim());
            Assert.Equal("10", response.fraudResult.avsResult);
            Assert.Equal("M", response.fraudResult.cardValidationResult);

        }

        [Fact]
        public void Test2Sale()
        {
            sale sale = new sale();
            sale.id = "1";
            sale.orderId = "2";
            sale.amount = 20020;
            sale.orderSource = orderSourceType.ecommerce;
            contact contact = new contact();
            contact.name = "Mike J. Hammer";
            contact.addressLine1 = "2 Main St.";
            contact.addressLine2 = "Apt. 222";
            contact.city = "Riverside";
            contact.state = "RI";
            contact.zip = "02915";
            contact.country = countryTypeEnum.US;
            sale.billToAddress = contact;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.MC;
            card.number = "5112010000000003";
            card.expDate = "0212";
            card.cardValidationNum = "261";
            sale.card = card;
            fraudCheckType authenticationvalue = new fraudCheckType();
            authenticationvalue.authenticationValue = "BwABBJQ1AgAAAAAgJDUCAAAAAAA=";
            sale.cardholderAuthentication = authenticationvalue;

            saleResponse response = cnp.Sale(sale);
            Assert.Equal("000", response.response);
            Assert.Equal("Approved", response.message);
            Assert.Equal("22222", response.authCode.Trim());
            Assert.Equal("10", response.fraudResult.avsResult);
            Assert.Equal("M", response.fraudResult.cardValidationResult);

            credit credit = new credit();
            credit.id = response.id;
            credit.cnpTxnId = response.cnpTxnId;
            creditResponse creditResponse = cnp.Credit(credit);
            Assert.Equal("000", creditResponse.response);
            Assert.Equal("Approved", creditResponse.message);

            voidTxn newvoid = new voidTxn();
            newvoid.id = creditResponse.id;
            newvoid.cnpTxnId = creditResponse.cnpTxnId;
            voidResponse voidResponse = cnp.DoVoid(newvoid);
            Assert.Equal("000", voidResponse.response);
            Assert.Equal("Approved", voidResponse.message);
        }

        [Fact]
        public void Test3Auth()
        {
            authorization authorization = new authorization();
            authorization.id = "1";
            authorization.orderId = "3";
            authorization.amount = 30030;
            authorization.orderSource = orderSourceType.ecommerce;
            contact contact = new contact();
            contact.name = "Eileen Jones";
            contact.addressLine1 = "3 Main St.";
            contact.city = "Bloomfield";
            contact.state = "CT";
            contact.zip = "06002";
            contact.country = countryTypeEnum.US;
            authorization.billToAddress = contact;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.DI;
            card.number = "6011010000000003";
            card.expDate = "0312";
            card.cardValidationNum = "758";
            authorization.card = card;

            authorizationResponse response = cnp.Authorize(authorization);
            Assert.Equal("000", response.response);
            Assert.Equal("Approved", response.message);
            Assert.Equal("33333", response.authCode.Trim());
            Assert.Equal("10", response.fraudResult.avsResult);
            Assert.Equal("M", response.fraudResult.cardValidationResult);

            capture capture = new capture();
            capture.id = response.id;
            capture.cnpTxnId = response.cnpTxnId;
            captureResponse captureResponse = cnp.Capture(capture);
            Assert.Equal("000", captureResponse.response);
            Assert.Equal("Approved", captureResponse.message);

            credit credit = new credit();
            credit.id = captureResponse.id;
            credit.cnpTxnId = captureResponse.cnpTxnId;
            creditResponse creditResponse = cnp.Credit(credit);
            Assert.Equal("000", creditResponse.response);
            Assert.Equal("Approved", creditResponse.message);

            voidTxn newvoid = new voidTxn();
            newvoid.id = creditResponse.id;
            newvoid.cnpTxnId = creditResponse.cnpTxnId;
            voidResponse voidResponse = cnp.DoVoid(newvoid);
            Assert.Equal("000", voidResponse.response);
            Assert.Equal("Approved", voidResponse.message);
        }

        [Fact]
        public void Test3AVS()
        {
            authorization authorization = new authorization();
            authorization.id = "1";
            authorization.orderId = "3";
            authorization.amount = 0;
            authorization.orderSource = orderSourceType.ecommerce;
            contact contact = new contact();
            contact.name = "Eileen Jones";
            contact.addressLine1 = "3 Main St.";
            contact.city = "Bloomfield";
            contact.state = "CT";
            contact.zip = "06002";
            contact.country = countryTypeEnum.US;
            authorization.billToAddress = contact;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.DI;
            card.number = "6011010000000003";
            card.expDate = "0312";
            card.cardValidationNum = "758";
            authorization.card = card;

            authorizationResponse response = cnp.Authorize(authorization);
            Assert.Equal("000", response.response);
            Assert.Equal("Approved", response.message);
            Assert.Equal("33333", response.authCode.Trim());
            Assert.Equal("10", response.fraudResult.avsResult);
            Assert.Equal("M", response.fraudResult.cardValidationResult);

        }

        [Fact]
        public void Test3Sale()
        {
            sale sale = new sale();
            sale.id = "1";
            sale.orderId = "3";
            sale.amount = 30030;
            sale.orderSource = orderSourceType.ecommerce;
            contact contact = new contact();
            contact.name = "Eileen Jones";
            contact.addressLine1 = "3 Main St.";
            contact.city = "Bloomfield";
            contact.state = "CT";
            contact.zip = "06002";
            contact.country = countryTypeEnum.US;
            sale.billToAddress = contact;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.DI;
            card.number = "6011010000000003";
            card.expDate = "0312";
            card.cardValidationNum = "758";
            sale.card = card;

            saleResponse response = cnp.Sale(sale);
            Assert.Equal("000", response.response);
            Assert.Equal("Approved", response.message);
            Assert.Equal("33333", response.authCode.Trim());
            Assert.Equal("10", response.fraudResult.avsResult);
            Assert.Equal("M", response.fraudResult.cardValidationResult);

            credit credit = new credit();
            credit.id = response.id;
            credit.cnpTxnId = response.cnpTxnId;
            creditResponse creditResponse = cnp.Credit(credit);
            Assert.Equal("000", creditResponse.response);
            Assert.Equal("Approved", creditResponse.message);

            voidTxn newvoid = new voidTxn();
            newvoid.id = creditResponse.id;
            newvoid.cnpTxnId = creditResponse.cnpTxnId;
            voidResponse voidResponse = cnp.DoVoid(newvoid);
            Assert.Equal("000", voidResponse.response);
            Assert.Equal("Approved", voidResponse.message);
        }

        [Fact]
        public void Test4Auth()
        {
            authorization authorization = new authorization();
            authorization.id = "1";
            authorization.orderId = "4";
            authorization.amount = 10100;
            authorization.orderSource = orderSourceType.ecommerce;
            contact contact = new contact();
            contact.name = "Bob Black";
            contact.addressLine1 = "4 Main St.";
            contact.city = "Laurel";
            contact.state = "MD";
            contact.zip = "20708";
            contact.country = countryTypeEnum.US;
            authorization.billToAddress = contact;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.AX;
            card.number = "375001000000005";
            card.expDate = "0421";
            //card.cardValidationNum = "758";
            authorization.card = card;

            authorizationResponse response = cnp.Authorize(authorization);
            Assert.Equal("000", response.response);
            Assert.Equal("Approved", response.message);
            Assert.Equal("44444", response.authCode.Trim());
            Assert.Equal("13", response.fraudResult.avsResult);

            capture capture = new capture();
            capture.id = response.id;
            capture.cnpTxnId = response.cnpTxnId;
            captureResponse captureresponse = cnp.Capture(capture);
            Assert.Equal("000", captureresponse.response);
            Assert.Equal("Approved", captureresponse.message);

            credit credit = new credit();
            credit.id = captureresponse.id;
            credit.cnpTxnId = captureresponse.cnpTxnId;
            creditResponse creditResponse = cnp.Credit(credit);
            Assert.Equal("000", creditResponse.response);
            Assert.Equal("Approved", creditResponse.message);

            voidTxn newvoid = new voidTxn();
            newvoid.id = creditResponse.id;
            newvoid.cnpTxnId = creditResponse.cnpTxnId;
            voidResponse voidResponse = cnp.DoVoid(newvoid);
            Assert.Equal("000", voidResponse.response);
            Assert.Equal("Approved", voidResponse.message);
        }

        [Fact]
        public void Test4AVS()
        {
            authorization authorization = new authorization();
            authorization.id = "1";
            authorization.orderId = "4";
            authorization.amount = 0;
            authorization.orderSource = orderSourceType.ecommerce;
            contact contact = new contact();
            contact.name = "Bob Black";
            contact.addressLine1 = "4 Main St.";
            contact.city = "Laurel";
            contact.state = "MD";
            contact.zip = "20708";
            contact.country = countryTypeEnum.US;
            authorization.billToAddress = contact;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.AX;
            card.number = "375001000000005";
            card.expDate = "0412";
            authorization.card = card;

            authorizationResponse response = cnp.Authorize(authorization);
            Assert.Equal("000", response.response);
            Assert.Equal("Approved", response.message);
            Assert.Equal("44444", response.authCode.Trim());
            Assert.Equal("13", response.fraudResult.avsResult);
        }

        [Fact]
        public void Test4Sale()
        {
            sale sale = new sale();
            sale.id = "1";
            sale.orderId = "4";
            sale.amount = 40040;
            sale.orderSource = orderSourceType.ecommerce;
            contact contact = new contact();
            contact.name = "Bob Black";
            contact.addressLine1 = "4 Main St.";
            contact.city = "Laurel";
            contact.state = "MD";
            contact.zip = "20708";
            contact.country = countryTypeEnum.US;
            sale.billToAddress = contact;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.AX;
            card.number = "375001000000005";
            card.expDate = "0412";
            card.cardValidationNum = "758";
            sale.card = card;

            saleResponse response = cnp.Sale(sale);
            Assert.Equal("000", response.response);
            Assert.Equal("Approved", response.message);
            Assert.Equal("44444", response.authCode.Trim());
            Assert.Equal("13", response.fraudResult.avsResult);

            credit credit = new credit();
            credit.id = response.id;
            credit.cnpTxnId = response.cnpTxnId;
            creditResponse creditResponse = cnp.Credit(credit);
            Assert.Equal("000", creditResponse.response);
            Assert.Equal("Approved", creditResponse.message);

            voidTxn newvoid = new voidTxn();
            newvoid.id = creditResponse.id;
            newvoid.cnpTxnId = creditResponse.cnpTxnId;
            voidResponse voidResponse = cnp.DoVoid(newvoid);
            Assert.Equal("000", voidResponse.response);
            Assert.Equal("Approved", voidResponse.message);
        }

        [Fact]
        public void Test5Auth()
        {
            authorization authorization = new authorization();
            authorization.id = "1";
            authorization.orderId = "5";
            authorization.amount = 50050;
            authorization.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4457010200000007";
            card.expDate = "0512";
            card.cardValidationNum = "463";
            authorization.card = card;
            fraudCheckType authenticationvalue = new fraudCheckType();
            authenticationvalue.authenticationValue = "BwABBJQ1AgAAAAAgJDUCAAAAAAA=";
            authorization.cardholderAuthentication = authenticationvalue;

            authorizationResponse response = cnp.Authorize(authorization);
            Assert.Equal("000", response.response);
            Assert.Equal("Approved", response.message);
            Assert.Equal("55555 ", response.authCode);
            Assert.Equal("32", response.fraudResult.avsResult);
            Assert.Equal("M", response.fraudResult.cardValidationResult);

            capture capture = new capture();
            capture.id = response.id;
            capture.cnpTxnId = response.cnpTxnId;
            captureResponse captureresponse = cnp.Capture(capture);
            Assert.Equal("000", captureresponse.response);
            Assert.Equal("Approved", captureresponse.message);

            credit credit = new credit();
            credit.id = captureresponse.id;
            credit.cnpTxnId = captureresponse.cnpTxnId;
            creditResponse creditResponse = cnp.Credit(credit);
            Assert.Equal("000", creditResponse.response);
            Assert.Equal("Approved", creditResponse.message);

            voidTxn newvoid = new voidTxn();
            newvoid.id = creditResponse.id;
            newvoid.cnpTxnId = creditResponse.cnpTxnId;
            voidResponse voidResponse = cnp.DoVoid(newvoid);
            Assert.Equal("000", voidResponse.response);
            Assert.Equal("Approved", voidResponse.message);
        }

        [Fact]
        public void Test5AVS()
        {
            authorization authorization = new authorization();
            authorization.id = "1";
            authorization.orderId = "5";
            authorization.amount = 0;
            authorization.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4457010200000007";
            card.expDate = "0512";
            card.cardValidationNum = "463";
            authorization.card = card;
            fraudCheckType authenticationvalue = new fraudCheckType();
            authenticationvalue.authenticationValue = "BwABBJQ1AgAAAAAgJDUCAAAAAAA=";
            authorization.cardholderAuthentication = authenticationvalue;

            authorizationResponse response = cnp.Authorize(authorization);
            Assert.Equal("000", response.response);
            Assert.Equal("Approved", response.message);
            Assert.Equal("55555 ", response.authCode);
            Assert.Equal("32", response.fraudResult.avsResult);
            Assert.Equal("M", response.fraudResult.cardValidationResult);
        }

        [Fact]
        public void Test5Sale()
        {
            sale sale = new sale();
            sale.id = "1";
            sale.orderId = "5";
            sale.amount = 50050;
            sale.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4457010200000007";
            card.expDate = "0512";
            card.cardValidationNum = "463";
            sale.card = card;
            fraudCheckType authenticationvalue = new fraudCheckType();
            authenticationvalue.authenticationValue = "BwABBJQ1AgAAAAAgJDUCAAAAAAA=";
            sale.cardholderAuthentication = authenticationvalue;

            saleResponse response = cnp.Sale(sale);
            Assert.Equal("000", response.response);
            Assert.Equal("Approved", response.message);
            Assert.Equal("55555 ", response.authCode);
            Assert.Equal("32", response.fraudResult.avsResult);
            Assert.Equal("M", response.fraudResult.cardValidationResult);

            credit credit = new credit();
            credit.id = response.id;
            credit.cnpTxnId = response.cnpTxnId;
            creditResponse creditResponse = cnp.Credit(credit);
            Assert.Equal("000", creditResponse.response);
            Assert.Equal("Approved", creditResponse.message);

            voidTxn newvoid = new voidTxn();
            newvoid.id = creditResponse.id;
            newvoid.cnpTxnId = creditResponse.cnpTxnId;
            voidResponse voidResponse = cnp.DoVoid(newvoid);
            Assert.Equal("000", voidResponse.response);
            Assert.Equal("Approved", voidResponse.message);
        }

        [Fact]
        public void Test6Auth()
        {
            authorization authorization = new authorization();
            authorization.id = "1";
            authorization.orderId = "6";
            authorization.amount = 60060;
            authorization.orderSource = orderSourceType.ecommerce;
            contact contact = new contact();
            contact.name = "Joe Green";
            contact.addressLine1 = "6 Main St.";
            contact.city = "Derry";
            contact.state = "NH";
            contact.zip = "03038";
            contact.country = countryTypeEnum.US;
            authorization.billToAddress = contact;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4457010100000008";
            card.expDate = "0612";
            card.cardValidationNum = "992";
            authorization.card = card;

            authorizationResponse response = cnp.Authorize(authorization);
            Assert.Equal("110", response.response);
            Assert.Equal("Insufficient Funds", response.message);
            Assert.Equal("34", response.fraudResult.avsResult);
            Assert.Equal("P", response.fraudResult.cardValidationResult);
        }

        [Fact]
        public void Test6Sale()
        {
            sale sale = new sale();
            sale.id = "1";
            sale.orderId = "6";
            sale.amount = 60060;
            sale.orderSource = orderSourceType.ecommerce;
            contact contact = new contact();
            contact.name = "Joe Green";
            contact.addressLine1 = "6 Main St.";
            contact.city = "Derry";
            contact.state = "NH";
            contact.zip = "03038";
            contact.country = countryTypeEnum.US;
            sale.billToAddress = contact;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4457010100000008";
            card.expDate = "0612";
            card.cardValidationNum = "992";
            sale.card = card;

            saleResponse response = cnp.Sale(sale);
            Assert.Equal("110", response.response);
            Assert.Equal("Insufficient Funds", response.message);
            Assert.Equal("34", response.fraudResult.avsResult);
            Assert.Equal("P", response.fraudResult.cardValidationResult);

            voidTxn newvoid = new voidTxn();
            newvoid.id = response.id;
            newvoid.cnpTxnId = response.cnpTxnId;
            voidResponse voidResponse = cnp.DoVoid(newvoid);
            Assert.Equal("000", voidResponse.response);
            Assert.Equal("Approved", voidResponse.message);
        }

        [Fact]
        public void Test7Auth()
        {
            authorization authorization = new authorization();
            authorization.id = "1";
            authorization.orderId = "7";
            authorization.amount = 70070;
            authorization.orderSource = orderSourceType.ecommerce;
            contact contact = new contact();
            contact.name = "Jane Murray";
            contact.addressLine1 = "7 Main St.";
            contact.city = "Amesbury";
            contact.state = "MA";
            contact.zip = "01913";
            contact.country = countryTypeEnum.US;
            authorization.billToAddress = contact;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.MC;
            card.number = "5112010100000002";
            card.expDate = "0712";
            card.cardValidationNum = "251";
            authorization.card = card;

            authorizationResponse response = cnp.Authorize(authorization);
            Assert.Equal("301", response.response);
            Assert.Equal("Invalid Account Number", response.message);
            Assert.Equal("34", response.fraudResult.avsResult);
            Assert.Equal("N", response.fraudResult.cardValidationResult);
        }

        [Fact]
        public void Test7AVS()
        {
            authorization authorization = new authorization();
            authorization.id = "1";
            authorization.orderId = "7";
            authorization.amount = 0;
            authorization.orderSource = orderSourceType.ecommerce;
            contact contact = new contact();
            contact.name = "Jane Murray";
            contact.addressLine1 = "7 Main St.";
            contact.city = "Amesbury";
            contact.state = "MA";
            contact.zip = "01913";
            contact.country = countryTypeEnum.US;
            authorization.billToAddress = contact;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.MC;
            card.number = "5112010100000002";
            card.expDate = "0712";
            card.cardValidationNum = "251";
            authorization.card = card;

            authorizationResponse response = cnp.Authorize(authorization);
            Assert.Equal("301", response.response);
            Assert.Equal("Invalid Account Number", response.message);
            Assert.Equal("34", response.fraudResult.avsResult);
            Assert.Equal("N", response.fraudResult.cardValidationResult);
        }

        [Fact]
        public void Test7Sale()
        {
            sale sale = new sale();
            sale.id = "1";
            sale.orderId = "7";
            sale.amount = 70070;
            sale.orderSource = orderSourceType.ecommerce;
            contact contact = new contact();
            contact.name = "Jane Murray";
            contact.addressLine1 = "7 Main St.";
            contact.city = "Amesbury";
            contact.state = "MA";
            contact.zip = "01913";
            contact.country = countryTypeEnum.US;
            sale.billToAddress = contact;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.MC;
            card.number = "5112010100000002";
            card.expDate = "0712";
            card.cardValidationNum = "251";
            sale.card = card;

            saleResponse response = cnp.Sale(sale);
            Assert.Equal("301", response.response);
            Assert.Equal("Invalid Account Number", response.message);
            Assert.Equal("34", response.fraudResult.avsResult);
            Assert.Equal("N", response.fraudResult.cardValidationResult);
        }

        [Fact]
        public void Test8Auth()
        {
            authorization authorization = new authorization();
            authorization.id = "1";
            authorization.orderId = "8";
            authorization.amount = 80080;
            authorization.orderSource = orderSourceType.ecommerce;
            contact contact = new contact();
            contact.name = "Mark Johnson";
            contact.addressLine1 = "8 Main St.";
            contact.city = "Manchester";
            contact.state = "NH";
            contact.zip = "03101";
            contact.country = countryTypeEnum.US;
            authorization.billToAddress = contact;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.DI;
            card.number = "6011010100000002";
            card.expDate = "0812";
            card.cardValidationNum = "184";
            authorization.card = card;

            authorizationResponse response = cnp.Authorize(authorization);
            Assert.Equal("123", response.response);
            Assert.Equal("Call Discover", response.message);
            Assert.Equal("34", response.fraudResult.avsResult);
            Assert.Equal("P", response.fraudResult.cardValidationResult);
        }

        [Fact]
        public void Test8AVS()
        {
            authorization authorization = new authorization();
            authorization.id = "1";
            authorization.orderId = "8";
            authorization.amount = 0;
            authorization.orderSource = orderSourceType.ecommerce;
            contact contact = new contact();
            contact.name = "Mark Johnson";
            contact.addressLine1 = "8 Main St.";
            contact.city = "Manchester";
            contact.state = "NH";
            contact.zip = "03101";
            contact.country = countryTypeEnum.US;
            authorization.billToAddress = contact;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.DI;
            card.number = "6011010100000002";
            card.expDate = "0812";
            card.cardValidationNum = "184";
            authorization.card = card;

            authorizationResponse response = cnp.Authorize(authorization);
            Assert.Equal("123", response.response);
            Assert.Equal("Call Discover", response.message);
            Assert.Equal("34", response.fraudResult.avsResult);
            Assert.Equal("P", response.fraudResult.cardValidationResult);
        }

        [Fact]
        public void Test8Sale()
        {
            sale sale = new sale();
            sale.id = "1";
            sale.orderId = "8";
            sale.amount = 80080;
            sale.orderSource = orderSourceType.ecommerce;
            contact contact = new contact();
            contact.name = "Mark Johnson";
            contact.addressLine1 = "8 Main St.";
            contact.city = "Manchester";
            contact.state = "NH";
            contact.zip = "03101";
            contact.country = countryTypeEnum.US;
            sale.billToAddress = contact;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.DI;
            card.number = "6011010100000002";
            card.expDate = "0812";
            card.cardValidationNum = "184";
            sale.card = card;

            saleResponse response = cnp.Sale(sale);
            Assert.Equal("123", response.response);
            Assert.Equal("Call Discover", response.message);
            Assert.Equal("34", response.fraudResult.avsResult);
            Assert.Equal("P", response.fraudResult.cardValidationResult);
        }

        [Fact]
        public void Test9Auth()
        {
            authorization authorization = new authorization();
            authorization.id = "1";
            authorization.orderId = "9";
            authorization.amount = 90090;
            authorization.orderSource = orderSourceType.ecommerce;
            contact contact = new contact();
            contact.name = "James Miller";
            contact.addressLine1 = "9 Main St.";
            contact.city = "Boston";
            contact.state = "MA";
            contact.zip = "02134";
            contact.country = countryTypeEnum.US;
            authorization.billToAddress = contact;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.AX;
            card.number = "375001010000003";
            card.expDate = "0912";
            card.cardValidationNum = "0421";
            authorization.card = card;

            authorizationResponse response = cnp.Authorize(authorization);
            Assert.Equal("303", response.response);
            Assert.Equal("Pick Up Card", response.message);
            Assert.Equal("34", response.fraudResult.avsResult);
        }

        [Fact]
        public void Test9AVS()
        {
            authorization authorization = new authorization();
            authorization.id = "1";
            authorization.orderId = "9";
            authorization.amount = 0;
            authorization.orderSource = orderSourceType.ecommerce;
            contact contact = new contact();
            contact.name = "James Miller";
            contact.addressLine1 = "9 Main St.";
            contact.city = "Boston";
            contact.state = "MA";
            contact.zip = "02134";
            contact.country = countryTypeEnum.US;
            authorization.billToAddress = contact;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.AX;
            card.number = "375001010000003";
            card.expDate = "0912";
            card.cardValidationNum = "0421";
            authorization.card = card;

            authorizationResponse response = cnp.Authorize(authorization);
            Assert.Equal("303", response.response);
            Assert.Equal("Pick Up Card", response.message);
            Assert.Equal("34", response.fraudResult.avsResult);
        }

        [Fact]
        public void Test9Sale()
        {
            sale sale = new sale();
            sale.id = "1";
            sale.orderId = "9";
            sale.amount = 90090;
            sale.orderSource = orderSourceType.ecommerce;
            contact contact = new contact();
            contact.name = "James Miller";
            contact.addressLine1 = "9 Main St.";
            contact.city = "Boston";
            contact.state = "MA";
            contact.zip = "02134";
            contact.country = countryTypeEnum.US;
            sale.billToAddress = contact;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.AX;
            card.number = "375001010000003";
            card.expDate = "0912";
            card.cardValidationNum = "0421";
            sale.card = card;

            saleResponse response = cnp.Sale(sale);
            Assert.Equal("303", response.response);
            Assert.Equal("Pick Up Card", response.message);
            Assert.Equal("34", response.fraudResult.avsResult);
        }

        [Fact]
        public void Test10()
        {
            authorization authorization = new authorization();
            authorization.id = "1";
            authorization.orderId = "10";
            authorization.amount = 40000;
            authorization.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4457010140000141";
            card.expDate = "0912";
            authorization.card = card;
            authorization.allowPartialAuth = true;

            authorizationResponse response = cnp.Authorize(authorization);
            Assert.Equal("010", response.response);
            Assert.Equal("Partially Approved", response.message);
            Assert.Equal("32000", response.approvedAmount);
        }

        [Fact]
        public void Test11()
        {
            authorization authorization = new authorization();
            authorization.id = "1";
            authorization.orderId = "11";
            authorization.amount = 60000;
            authorization.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.MC;
            card.number = "5112010140000004";
            card.expDate = "1111";
            authorization.card = card;
            authorization.allowPartialAuth = true;

            authorizationResponse response = cnp.Authorize(authorization);
            Assert.Equal("010", response.response);
            Assert.Equal("Partially Approved", response.message);
            Assert.Equal("48000", response.approvedAmount);
        }

        [Fact]
        public void Test12()
        {
            authorization authorization = new authorization();
            authorization.id = "1";
            authorization.orderId = "12";
            authorization.amount = 50000;
            authorization.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.AX;
            card.number = "375001014000009";
            card.expDate = "0412";
            authorization.card = card;
            authorization.allowPartialAuth = true;

            authorizationResponse response = cnp.Authorize(authorization);
            Assert.Equal("010", response.response);
            Assert.Equal("Partially Approved", response.message);
            Assert.Equal("40000", response.approvedAmount);
        }

        [Fact]
        public void Test13()
        {
            authorization authorization = new authorization();
            authorization.id = "1";
            authorization.orderId = "13";
            authorization.amount = 15000;
            authorization.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.DI;
            card.number = "6011010140000004";
            card.expDate = "0812";
            authorization.card = card;
            authorization.allowPartialAuth = true;

            authorizationResponse response = cnp.Authorize(authorization);
            Assert.Equal("010", response.response);
            Assert.Equal("Partially Approved", response.message);
            Assert.Equal("12000", response.approvedAmount);

        }
            
    }
}
