using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Cnp.Sdk;
using System.IO;
using System.Collections;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    class TestBatchRequest
    {
        private cnpRequest cnp;

        [SetUp]
        public void setUpBeforeTest()
        {
            CommManager.reset();
            cnp = new cnpRequest();
        }

        [Test]
        public void SimpleBatch()
        {
            var cnpBatchRequest = new batchRequest();

            var authorization = new authorization
            {
                reportGroup = "Planets",
                orderId = "12344",
                amount = 106,
                orderSource = orderSourceType.ecommerce
            };
            var card = new cardType
            {
                type = methodOfPaymentTypeEnum.VI,
                number = "4100000000000001",
                expDate = "1210"
            };
            authorization.card = card;
            authorization.id = "id";

            cnpBatchRequest.addAuthorization(authorization);

            var authorization2 = new authorization();
            authorization2.reportGroup = "Planets";
            authorization2.orderId = "12345";
            authorization2.amount = 106;
            authorization2.orderSource = orderSourceType.ecommerce;
            var card2 = new cardType();
            card2.type = methodOfPaymentTypeEnum.VI;
            card2.number = "4242424242424242";
            card2.expDate = "1210";
            authorization2.card = card2;
            authorization2.id = "id";

            cnpBatchRequest.addAuthorization(authorization2);
            
            var reversal = new authReversal();
            reversal.cnpTxnId = 12345678000L;
            reversal.amount = 106;
            reversal.payPalNotes = "Notes";
            reversal.id = "id";

            cnpBatchRequest.addAuthReversal(reversal);

            var reversal2 = new authReversal();
            reversal2.cnpTxnId = 12345678900L;
            reversal2.amount = 106;
            reversal2.payPalNotes = "Notes"; 
            reversal2.id = "id";

            cnpBatchRequest.addAuthReversal(reversal2);
            
            var giftCardAuthReversal = new giftCardAuthReversal();
            giftCardAuthReversal.id = "id";
            giftCardAuthReversal.cnpTxnId = 12345678000L;
            var giftCardCardTypeAuthReversal = new giftCardCardType();
            giftCardCardTypeAuthReversal.type = methodOfPaymentTypeEnum.GC;
            giftCardCardTypeAuthReversal.number = "4100000000000001";
            giftCardCardTypeAuthReversal.expDate = "1210";
            giftCardAuthReversal.card = giftCardCardTypeAuthReversal;
            giftCardAuthReversal.originalRefCode = "123456";
            giftCardAuthReversal.originalAmount = 1000;
            giftCardAuthReversal.originalTxnTime = DateTime.Now;
            giftCardAuthReversal.originalSystemTraceId = 123;
            giftCardAuthReversal.originalSequenceNumber = "123456";

            cnpBatchRequest.addGiftCardAuthReversal(giftCardAuthReversal);

            var capture = new capture();
            capture.cnpTxnId = 123456000;
            capture.amount = 106;
            capture.payPalNotes = "Notes";
            capture.id = "id";

            cnpBatchRequest.addCapture(capture);

            var capture2 = new capture();
            capture2.cnpTxnId = 123456700;
            capture2.amount = 106;
            capture2.payPalNotes = "Notes";
            capture2.id = "id";

            cnpBatchRequest.addCapture(capture2);
            
            var giftCardCapture = new giftCardCapture();
            giftCardCapture.id = "id";
            giftCardCapture.cnpTxnId = 12345678000L;
            giftCardCapture.captureAmount = 123456;
            var giftCardCardTypeCapture = new giftCardCardType();
            giftCardCardTypeCapture.type = methodOfPaymentTypeEnum.GC;
            giftCardCardTypeCapture.number = "4100000000000001";
            giftCardCardTypeCapture.expDate = "1210";
            giftCardCapture.card = giftCardCardTypeCapture;
            giftCardCapture.originalRefCode = "123456";
            giftCardCapture.originalAmount = 1000;
            giftCardCapture.originalTxnTime = DateTime.Now;

            cnpBatchRequest.addGiftCardCapture(giftCardCapture);

            var capturegivenauth = new captureGivenAuth();
            capturegivenauth.amount = 106;
            capturegivenauth.orderId = "12344";
            var authInfo = new authInformation();
            var authDate = new DateTime(2002, 10, 9);
            authInfo.authDate = authDate;
            authInfo.authCode = "543216";
            authInfo.authAmount = 12345;
            capturegivenauth.authInformation = authInfo;
            capturegivenauth.orderSource = orderSourceType.ecommerce;
            capturegivenauth.card = card;
            capturegivenauth.id = "id";

            cnpBatchRequest.addCaptureGivenAuth(capturegivenauth);

            var capturegivenauth2 = new captureGivenAuth();
            capturegivenauth2.amount = 106;
            capturegivenauth2.orderId = "12344";
            var authInfo2 = new authInformation();
            authDate = new DateTime(2003, 10, 9);
            authInfo2.authDate = authDate;
            authInfo2.authCode = "543216";
            authInfo2.authAmount = 12345;
            capturegivenauth2.authInformation = authInfo;
            capturegivenauth2.orderSource = orderSourceType.ecommerce;
            capturegivenauth2.card = card2;
            capturegivenauth2.id = "id";

            cnpBatchRequest.addCaptureGivenAuth(capturegivenauth2);

            var creditObj = new credit();
            creditObj.amount = 106;
            creditObj.orderId = "2111";
            creditObj.orderSource = orderSourceType.ecommerce;
            creditObj.card = card;
            creditObj.id = "id";

            cnpBatchRequest.addCredit(creditObj);

            var creditObj2 = new credit();
            creditObj2.amount = 106;
            creditObj2.orderId = "2111";
            creditObj2.orderSource = orderSourceType.ecommerce;
            creditObj2.card = card2;
            creditObj2.id = "id";

            cnpBatchRequest.addCredit(creditObj2);
            
            var giftCardCredit = new giftCardCredit();
            giftCardCredit.id = "id";
            giftCardCredit.cnpTxnId = 12345678000L;
            giftCardCredit.creditAmount = 123456;
            var giftCardCardTypeCredit = new giftCardCardType();
            giftCardCardTypeCredit.type = methodOfPaymentTypeEnum.GC;
            giftCardCardTypeCredit.number = "4100000000000001";
            giftCardCardTypeCredit.expDate = "1210";
            giftCardCredit.card = giftCardCardTypeCredit;
            giftCardCredit.orderId = "123456";
            giftCardCredit.orderSource = orderSourceType.ecommerce;

            cnpBatchRequest.addGiftCardCredit(giftCardCredit);

            var echeckcredit = new echeckCredit();
            echeckcredit.amount = 12L;
            echeckcredit.orderId = "12345";
            echeckcredit.orderSource = orderSourceType.ecommerce;
            var echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "1099999903";
            echeck.routingNum = "011201995";
            echeck.checkNum = "123455";
            echeckcredit.echeck = echeck;
            var billToAddress = new contact();
            billToAddress.name = "Bob";
            billToAddress.city = "Lowell";
            billToAddress.state = "MA";
            billToAddress.email = "cnp.com";
            echeckcredit.billToAddress = billToAddress;
            echeckcredit.id = "id";

            cnpBatchRequest.addEcheckCredit(echeckcredit);

            var echeckcredit2 = new echeckCredit();
            echeckcredit2.amount = 12L;
            echeckcredit2.orderId = "12346";
            echeckcredit2.orderSource = orderSourceType.ecommerce;
            var echeck2 = new echeckType();
            echeck2.accType = echeckAccountTypeEnum.Checking;
            echeck2.accNum = "1099999903";
            echeck2.routingNum = "011201995";
            echeck2.checkNum = "123456";
            echeckcredit2.echeck = echeck2;
            var billToAddress2 = new contact();
            billToAddress2.name = "Mike";
            billToAddress2.city = "Lowell";
            billToAddress2.state = "MA";
            billToAddress2.email = "cnp.com";
            echeckcredit2.billToAddress = billToAddress2;
            echeckcredit2.id = "id";

            cnpBatchRequest.addEcheckCredit(echeckcredit2);

            var echeckredeposit = new echeckRedeposit();
            echeckredeposit.cnpTxnId = 123456;
            echeckredeposit.echeck = echeck;
            echeckredeposit.id = "id";

            cnpBatchRequest.addEcheckRedeposit(echeckredeposit);

            var echeckredeposit2 = new echeckRedeposit();
            echeckredeposit2.cnpTxnId = 123457;
            echeckredeposit2.echeck = echeck2;
            echeckredeposit2.id = "id";

            cnpBatchRequest.addEcheckRedeposit(echeckredeposit2);

            var echeckSaleObj = new echeckSale();
            echeckSaleObj.amount = 123456;
            echeckSaleObj.orderId = "12345";
            echeckSaleObj.orderSource = orderSourceType.ecommerce;
            echeckSaleObj.echeck = echeck;
            echeckSaleObj.billToAddress = billToAddress;
            echeckSaleObj.id = "id";

            cnpBatchRequest.addEcheckSale(echeckSaleObj);

            var echeckSaleObj2 = new echeckSale();
            echeckSaleObj2.amount = 123456;
            echeckSaleObj2.orderId = "12346";
            echeckSaleObj2.orderSource = orderSourceType.ecommerce;
            echeckSaleObj2.echeck = echeck2;
            echeckSaleObj2.billToAddress = billToAddress2;
            echeckSaleObj2.id = "id";

            cnpBatchRequest.addEcheckSale(echeckSaleObj2);

            var echeckPreNoteSaleObj1 = new echeckPreNoteSale();
            echeckPreNoteSaleObj1.orderId = "12345";
            echeckPreNoteSaleObj1.orderSource = orderSourceType.ecommerce;
            echeckPreNoteSaleObj1.echeck = echeck;
            echeckPreNoteSaleObj1.billToAddress = billToAddress;
            echeckPreNoteSaleObj1.id = "id";

            cnpBatchRequest.addEcheckPreNoteSale(echeckPreNoteSaleObj1);

            var echeckPreNoteSaleObj2 = new echeckPreNoteSale();
            echeckPreNoteSaleObj2.orderId = "12345";
            echeckPreNoteSaleObj2.orderSource = orderSourceType.ecommerce;
            echeckPreNoteSaleObj2.echeck = echeck2;
            echeckPreNoteSaleObj2.billToAddress = billToAddress2;
            echeckPreNoteSaleObj2.id = "id";

            cnpBatchRequest.addEcheckPreNoteSale(echeckPreNoteSaleObj2);

            var echeckPreNoteCreditObj1 = new echeckPreNoteCredit();
            echeckPreNoteCreditObj1.orderId = "12345";
            echeckPreNoteCreditObj1.orderSource = orderSourceType.ecommerce;
            echeckPreNoteCreditObj1.echeck = echeck;
            echeckPreNoteCreditObj1.billToAddress = billToAddress;
            echeckPreNoteCreditObj1.id = "id";

            cnpBatchRequest.addEcheckPreNoteCredit(echeckPreNoteCreditObj1);

            var echeckPreNoteCreditObj2 = new echeckPreNoteCredit();
            echeckPreNoteCreditObj2.orderId = "12345";
            echeckPreNoteCreditObj2.orderSource = orderSourceType.ecommerce;
            echeckPreNoteCreditObj2.echeck = echeck2;
            echeckPreNoteCreditObj2.billToAddress = billToAddress2;
            echeckPreNoteCreditObj2.id = "id";

            var echeckVerificationObject = new echeckVerification();
            echeckVerificationObject.amount = 123456;
            echeckVerificationObject.orderId = "12345";
            echeckVerificationObject.orderSource = orderSourceType.ecommerce;
            echeckVerificationObject.echeck = echeck;
            echeckVerificationObject.billToAddress = billToAddress;
            echeckVerificationObject.id = "id";

            cnpBatchRequest.addEcheckVerification(echeckVerificationObject);

            var echeckVerificationObject2 = new echeckVerification();
            echeckVerificationObject2.amount = 123456;
            echeckVerificationObject2.orderId = "12346";
            echeckVerificationObject2.orderSource = orderSourceType.ecommerce;
            echeckVerificationObject2.echeck = echeck2;
            echeckVerificationObject2.billToAddress = billToAddress2; 
            echeckVerificationObject2.id = "id";

            cnpBatchRequest.addEcheckVerification(echeckVerificationObject2);

            var forcecapture = new forceCapture();
            forcecapture.amount = 106;
            forcecapture.orderId = "12344";
            forcecapture.orderSource = orderSourceType.ecommerce;
            forcecapture.card = card;
            forcecapture.id = "id";

            cnpBatchRequest.addForceCapture(forcecapture);

            var forcecapture2 = new forceCapture();
            forcecapture2.amount = 106;
            forcecapture2.orderId = "12345";
            forcecapture2.orderSource = orderSourceType.ecommerce;
            forcecapture2.card = card2;
            forcecapture2.id = "id";

            cnpBatchRequest.addForceCapture(forcecapture2);

            var saleObj = new sale();
            saleObj.amount = 106;
            saleObj.cnpTxnId = 123456;
            saleObj.orderId = "12344";
            saleObj.orderSource = orderSourceType.ecommerce;
            saleObj.card = card;
            saleObj.id = "id";

            cnpBatchRequest.addSale(saleObj);

            var saleObj2 = new sale();
            saleObj2.amount = 106;
            saleObj2.cnpTxnId = 123456;
            saleObj2.orderId = "12345";
            saleObj2.orderSource = orderSourceType.ecommerce;
            saleObj2.card = card2;
            saleObj2.id = "id";

            cnpBatchRequest.addSale(saleObj2);

            var registerTokenRequest = new registerTokenRequestType();
            registerTokenRequest.orderId = "12344";
            registerTokenRequest.accountNumber = "1233456789103801";
            registerTokenRequest.reportGroup = "Planets";
            registerTokenRequest.id = "id";

            cnpBatchRequest.addRegisterTokenRequest(registerTokenRequest);

            var registerTokenRequest2 = new registerTokenRequestType();
            registerTokenRequest2.orderId = "12345";
            registerTokenRequest2.accountNumber = "1233456789103801";
            registerTokenRequest2.reportGroup = "Planets";
            registerTokenRequest2.id = "id";

            cnpBatchRequest.addRegisterTokenRequest(registerTokenRequest2);

            var updateCardValidationNumOnToken = new updateCardValidationNumOnToken();
            updateCardValidationNumOnToken.orderId = "12344";
            updateCardValidationNumOnToken.cardValidationNum = "123";
            updateCardValidationNumOnToken.cnpToken = "4100000000000001";
            updateCardValidationNumOnToken.id = "id";

            cnpBatchRequest.addUpdateCardValidationNumOnToken(updateCardValidationNumOnToken);

            var updateCardValidationNumOnToken2 = new updateCardValidationNumOnToken();
            updateCardValidationNumOnToken2.orderId = "12345";
            updateCardValidationNumOnToken2.cardValidationNum = "123";
            updateCardValidationNumOnToken2.cnpToken = "4242424242424242";
            updateCardValidationNumOnToken2.id = "id";

            cnpBatchRequest.addUpdateCardValidationNumOnToken(updateCardValidationNumOnToken2);
            
//            fastAccessFunding fastAccessFunding = new fastAccessFunding();
//            fastAccessFunding.id = "A123456";
//            fastAccessFunding.reportGroup = "FastPayment";
//            fastAccessFunding.fundingSubmerchantId = "SomeSubMerchant";
//            fastAccessFunding.submerchantName = "Some Merchant Inc.";
//            fastAccessFunding.fundsTransferId = "123e4567e89b12d3";
//            fastAccessFunding.amount = 3000;
//            fastAccessFunding.token = new cardTokenType
//            {
//                cnpToken = "1111000101039449",
//                expDate = "1112",
//                cardValidationNum = "987",
//                type = methodOfPaymentTypeEnum.VI,
//            };
//            cnpBatchRequest.addfastAccessFunding(fastAccessFunding);
            
            cnp.addBatch(cnpBatchRequest);

            var batchName = cnp.sendToCnp();

            cnp.blockAndWaitForResponse(batchName, estimatedResponseTime(2 * 2, 10 * 2));

            var cnpResponse = cnp.receiveFromCnp(batchName);

            Assert.NotNull(cnpResponse);
            Assert.AreEqual("0", cnpResponse.response);
            Assert.AreEqual("Valid Format", cnpResponse.message);

            var cnpBatchResponse = cnpResponse.nextBatchResponse();
            while (cnpBatchResponse != null)
            {
                var authorizationResponse = cnpBatchResponse.nextAuthorizationResponse();
                while (authorizationResponse != null)
                {
                    Assert.AreEqual("000", authorizationResponse.response);

                    authorizationResponse = cnpBatchResponse.nextAuthorizationResponse();
                }

                var authReversalResponse = cnpBatchResponse.nextAuthReversalResponse();
                while (authReversalResponse != null)
                {
                    Assert.AreEqual("000", authReversalResponse.response);

                    authReversalResponse = cnpBatchResponse.nextAuthReversalResponse();
                }

                var giftCardAuthReversalResponse = cnpBatchResponse.nextGiftCardAuthReversalResponse();
                while (giftCardAuthReversalResponse != null)
                {
                    Assert.NotNull(giftCardAuthReversalResponse.response);

                    giftCardAuthReversalResponse = cnpBatchResponse.nextGiftCardAuthReversalResponse();
                }

                var captureResponse = cnpBatchResponse.nextCaptureResponse();
                while (captureResponse != null)
                {
                    Assert.AreEqual("000", captureResponse.response);

                    captureResponse = cnpBatchResponse.nextCaptureResponse();
                }

                var giftCardCaptureResponse = cnpBatchResponse.nextGiftCardCaptureResponse();
                while (giftCardCaptureResponse != null)
                {
                    Assert.NotNull(giftCardCaptureResponse.response);

                    giftCardCaptureResponse = cnpBatchResponse.nextGiftCardCaptureResponse();
                }


                var captureGivenAuthResponse = cnpBatchResponse.nextCaptureGivenAuthResponse();
                while (captureGivenAuthResponse != null)
                {
                    Assert.AreEqual("000", captureGivenAuthResponse.response);

                    captureGivenAuthResponse = cnpBatchResponse.nextCaptureGivenAuthResponse();
                }

                var creditResponse = cnpBatchResponse.nextCreditResponse();
                while (creditResponse != null)
                {
                    Assert.AreEqual("000", creditResponse.response);

                    creditResponse = cnpBatchResponse.nextCreditResponse();
                }

                var giftCardCreditResponse = cnpBatchResponse.nextGiftCardCreditResponse();
                while (giftCardCreditResponse != null)
                {
                    Assert.NotNull(giftCardCreditResponse.response);

                    giftCardCreditResponse = cnpBatchResponse.nextGiftCardCreditResponse();
                }

                var echeckCreditResponse = cnpBatchResponse.nextEcheckCreditResponse();
                while (echeckCreditResponse != null)
                {
                    Assert.AreEqual("000", echeckCreditResponse.response);

                    echeckCreditResponse = cnpBatchResponse.nextEcheckCreditResponse();
                }

                var echeckRedepositResponse = cnpBatchResponse.nextEcheckRedepositResponse();
                while (echeckRedepositResponse != null)
                {
                    Assert.AreEqual("000", echeckRedepositResponse.response);

                    echeckRedepositResponse = cnpBatchResponse.nextEcheckRedepositResponse();
                }

                var echeckSalesResponse = cnpBatchResponse.nextEcheckSalesResponse();
                while (echeckSalesResponse != null)
                {
                    Assert.AreEqual("000", echeckSalesResponse.response);

                    echeckSalesResponse = cnpBatchResponse.nextEcheckSalesResponse();
                }

                var echeckPreNoteSaleResponse = cnpBatchResponse.nextEcheckPreNoteSaleResponse();
                while (echeckPreNoteSaleResponse != null)
                {
                    Assert.AreEqual("000", echeckPreNoteSaleResponse.response);

                    echeckPreNoteSaleResponse = cnpBatchResponse.nextEcheckPreNoteSaleResponse();
                }

                var echeckPreNoteCreditResponse = cnpBatchResponse.nextEcheckPreNoteCreditResponse();
                while (echeckPreNoteCreditResponse != null)
                {
                    Assert.AreEqual("000", echeckPreNoteCreditResponse.response);

                    echeckPreNoteCreditResponse = cnpBatchResponse.nextEcheckPreNoteCreditResponse();
                }

                var echeckVerificationResponse = cnpBatchResponse.nextEcheckVerificationResponse();
                while (echeckVerificationResponse != null)
                {
                    Assert.AreEqual("957", echeckVerificationResponse.response);

                    echeckVerificationResponse = cnpBatchResponse.nextEcheckVerificationResponse();
                }

                var forceCaptureResponse = cnpBatchResponse.nextForceCaptureResponse();
                while (forceCaptureResponse != null)
                {
                    Assert.AreEqual("000", forceCaptureResponse.response);

                    forceCaptureResponse = cnpBatchResponse.nextForceCaptureResponse();
                }

                var registerTokenResponse = cnpBatchResponse.nextRegisterTokenResponse();
                while (registerTokenResponse != null)
                {
                    Assert.AreEqual("820", registerTokenResponse.response);

                    registerTokenResponse = cnpBatchResponse.nextRegisterTokenResponse();
                }

                var saleResponse = cnpBatchResponse.nextSaleResponse();
                while (saleResponse != null)
                {
                    Assert.AreEqual("000", saleResponse.response);

                    saleResponse = cnpBatchResponse.nextSaleResponse();
                }

                var updateCardValidationNumOnTokenResponse = cnpBatchResponse.nextUpdateCardValidationNumOnTokenResponse();
                while (updateCardValidationNumOnTokenResponse != null)
                {
                    Assert.AreEqual("823", updateCardValidationNumOnTokenResponse.response);

                    updateCardValidationNumOnTokenResponse = cnpBatchResponse.nextUpdateCardValidationNumOnTokenResponse();
                }

                cnpBatchResponse = cnpResponse.nextBatchResponse();
            }
        }
        
        [Test]
        [Ignore("ctxAll returns bad response code")]
        public void ctxAll()
        {
            
            CommManager.reset();
            
            Dictionary<string, string>  _config = new Dictionary<string, string>();
            _config["merchantId"] = Environment.GetEnvironmentVariable("payfacMerchantId_v12_7");
            _config["username"] = Environment.GetEnvironmentVariable("payfacUsername_v12_7");
            _config["password"] = Environment.GetEnvironmentVariable("payfacPassword_v12_7");
            _config["sftpUsername"] = Environment.GetEnvironmentVariable("payfacSftpUsername_v12_7");
            _config["sftpPassword"] = Environment.GetEnvironmentVariable("payfacSftpPassword_v12_7");
            _config["url"] = Properties.Settings.Default.url;
            _config["reportGroup"] = Properties.Settings.Default.reportGroup;
            _config["printxml"] = Properties.Settings.Default.printxml;
            _config["timeout"] = Properties.Settings.Default.timeout;
            _config["proxyHost"] = Properties.Settings.Default.proxyHost;
            _config["proxyPort"] = Properties.Settings.Default.proxyPort;
            _config["sftpUrl"] = Properties.Settings.Default.sftpUrl;
            _config["knownHostsFile"] = Properties.Settings.Default.knownHostsFile;
            _config["requestDirectory"] = Properties.Settings.Default.requestDirectory;
            _config["responseDirectory"] = Properties.Settings.Default.responseDirectory;
            _config["useEncryption"] = "false";

            var cnpBatchRequest = new batchRequest(_config);


            var cnpCtx = new cnpRequest(_config);

            string[] ctxPaymentInformation = { "ctx1 for submerchantcredit", "ctx2 for submerchantcredit", "ctx3 for submerchantcredit", "ctx4 for submerchantcredit", "ctx5 for submerchantcredit"};
            string fundsTransferIdString = DateTime.Now.Ticks.ToString();
            var accountInfoCtx = new echeckTypeCtx()
            {
                accType = echeckAccountTypeEnum.Checking,
                accNum = "1092969901",
                routingNum = "011075150",
                checkNum = "123455",
                ctxPaymentInformation = ctxPaymentInformation
            };
            
            var submerchantCreditCtx = new submerchantCreditCtx
            {
                // attributes.
                id = "111",
                reportGroup = "submerchantCredit",
                // required child elements.
                accountInfo = accountInfoCtx,
                amount = 500,
                fundingSubmerchantId = "submerchantCredit",
                fundsTransferId = fundsTransferIdString,
                submerchantName = "Vantiv",
                customIdentifier = "WorldPay"
            };
            cnpBatchRequest.addSubmerchantCreditCtx(submerchantCreditCtx);

            var req = submerchantCreditCtx.Serialize();
            var submerchantDebitCtx = new submerchantDebitCtx
            {
                // attributes.
                id = "11",
                reportGroup = "CTX Report Group",
                // required child elements.
                accountInfo = accountInfoCtx,
                amount = 500,
                fundingSubmerchantId = "value for fundingSubmerchantId",
                fundsTransferId = fundsTransferIdString,
                submerchantName = "Vantiv",
                customIdentifier = "WorldPay"
            };
            cnpBatchRequest.addSubmerchantDebitCtx(submerchantDebitCtx);

            var vendorCreditCtx = new vendorCreditCtx
            {
                // attributes.
                id = "11",
                reportGroup = "CTX Report Group",
                // required child elements.
                accountInfo = accountInfoCtx,
                amount = 500,
                fundingSubmerchantId = "value for fundingSubmerchantId",
                fundsTransferId = fundsTransferIdString,
                vendorName = "Vantiv"
            };
            cnpBatchRequest.addVendorCreditCtx(vendorCreditCtx);

            var vendorDebitCtx = new vendorDebitCtx
            {
                // attributes.
                id = "11",
                reportGroup = "CTX Report Group",
                // required child elements.
                accountInfo = accountInfoCtx,
                amount = 500,
                fundingSubmerchantId = "value for fundingSubmerchantId",
                fundsTransferId = fundsTransferIdString,
                vendorName = "Vantiv"
            };
            cnpBatchRequest.addVendorDebitCtx(vendorDebitCtx);


            cnpCtx.addBatch(cnpBatchRequest);
            var batchName = cnpCtx.sendToCnp();
            cnpCtx.blockAndWaitForResponse(batchName, estimatedResponseTime(0, 1));
            var cnpResponse = cnpCtx.receiveFromCnp(batchName);

            Assert.NotNull(cnpResponse);
            Assert.AreEqual("0", cnpResponse.response);
            Assert.AreEqual("Valid Format", cnpResponse.message);
            
            var cnpBatchResponse = cnpResponse.nextBatchResponse();
            while (cnpBatchResponse != null)
            {
                var submerchantCreditResponse = cnpBatchResponse.nextSubmerchantCreditResponse();
                while (submerchantCreditResponse != null)
                {
                    Assert.AreEqual("000", submerchantCreditResponse.response);

                    submerchantCreditResponse = cnpBatchResponse.nextSubmerchantCreditResponse();
                }

                var submerchantDebitResponse = cnpBatchResponse.nextSubmerchantDebitResponse();
                while (submerchantDebitResponse != null)
                {
                    Assert.AreEqual("000", submerchantDebitResponse.response);

                    submerchantDebitResponse = cnpBatchResponse.nextSubmerchantDebitResponse();
                }

                var vendorCreditResponse = cnpBatchResponse.nextVendorCreditResponse();
                while (vendorCreditResponse != null)
                {
                    Assert.AreEqual("000", vendorCreditResponse.response);

                    vendorCreditResponse = cnpBatchResponse.nextVendorCreditResponse();
                }

                var vendorDebitResponse = cnpBatchResponse.nextVendorDebitResponse();
                while (vendorDebitResponse != null)
                {
                    Assert.AreEqual("000", vendorDebitResponse.response);

                    vendorDebitResponse = cnpBatchResponse.nextVendorDebitResponse();
                }
                cnpBatchResponse = cnpResponse.nextBatchResponse();
            }
            
        }

        
        [Test]
        [Ignore("Fast access funding not setup for test merchant")]
        public void simpleBatchWithJustFastAccessFunding()
        {
            var cnpBatchRequest = new batchRequest();
            var fastAccessFunding = new fastAccessFunding();
            fastAccessFunding.id = "id";
            fastAccessFunding.fundingSubmerchantId = "SomeSubMerchant";
            fastAccessFunding.submerchantName = "Some Merchant Inc.";
            fastAccessFunding.fundsTransferId = "123e4567e89b12d3";
            fastAccessFunding.amount = 4000;
            //fastAccessFunding.disbursementType = disbursementTypeEnum.VAA;
            fastAccessFunding.card = new cardType
            {
                type = methodOfPaymentTypeEnum.VI,
                number = "4100000000000001",
                expDate = "1210"
            };
            
            cnpBatchRequest.addfastAccessFunding(fastAccessFunding);
            
            cnp.addBatch(cnpBatchRequest);

            var batchName = cnp.sendToCnp();

            cnp.blockAndWaitForResponse(batchName, estimatedResponseTime(0, 1));

            var cnpResponse = cnp.receiveFromCnp(batchName);

            Assert.NotNull(cnpResponse);
            Assert.AreEqual("0", cnpResponse.response);
            Assert.AreEqual("Valid Format", cnpResponse.message);
            
        }
        
        
        private int estimatedResponseTime(int numAuthsAndSales, int numRest)
        {
            return (int)(5 * 60 * 1000 + 2.5 * 1000 + numAuthsAndSales * (1 / 5) * 1000 + numRest * (1 / 50) * 1000) * 5;
        }
    }
}