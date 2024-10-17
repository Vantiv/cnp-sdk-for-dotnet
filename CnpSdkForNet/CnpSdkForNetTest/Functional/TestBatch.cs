using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    class TestBatch
    {
        private cnpRequest _cnp;
        private Dictionary<string, string> _invalidConfig;
        private Dictionary<string, string> _invalidSftpConfig;
        private static readonly string tempDirectroyPath = Path.Combine(Path.GetTempPath(),"NET" + CnpVersion.CurrentCNPXMLVersion) + Path.DirectorySeparatorChar;

        [OneTimeSetUp]
        public void SetUp()
        {
            EnvironmentVariableTestFlags.RequirePreliveBatchTestsEnabled();
            
            ConfigManager invalidConfigManager = new ConfigManager();
            _invalidConfig = invalidConfigManager.getConfig();
            _invalidConfig["username"] = "badUsername";
            _invalidConfig["password"] = "badPassword";
            _invalidConfig["requestDirectory"] = tempDirectroyPath + "BatchRequests";
            _invalidConfig["responseDirectory"] = tempDirectroyPath + "BatchResponses";
            _invalidConfig["useEncryption"] = "false";

            _invalidSftpConfig = invalidConfigManager.getConfig();
            _invalidSftpConfig["sftpUsername"] = "badSftpUsername";
            _invalidSftpConfig["sftpPassword"] = "badSftpPassword";
            _invalidSftpConfig["requestDirectory"] = tempDirectroyPath + "BatchRequests";
            _invalidSftpConfig["responseDirectory"] = tempDirectroyPath + "BatchResponses";
            _invalidSftpConfig["useEncryption"] = "false";
        }

        [SetUp]
        public void SetUpBeforeTest()
        {
            Dictionary<String,String> config = new ConfigManager().getConfig();
            config["requestDirectory"] = tempDirectroyPath + "BatchRequests";
            config["responseDirectory"] = tempDirectroyPath + "BatchResponses";
            _cnp = new cnpRequest(config);
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
            authorization.accountFundingTransactionData = new accountFundingTransactionData()
            {
                receiverFirstName = "abcc",
                receiverLastName = "cde",
                receiverCountry = countryTypeEnum.US,
                receiverState = stateTypeEnum.AL,
                receiverAccountNumberType = accountFundingTransactionAccountNumberTypeEnum.cardAccount,
                receiverAccountNumber = "4141000",
                accountFundingTransactionType = accountFundingTransactionTypeEnum.accountToAccount
            };
            authorization.fraudCheckAction = fraudCheckActionEnum.APPROVED_SKIP_FRAUD_CHECK;


            cnpBatchRequest.addAuthorization(authorization);

            var translateToLowValueTokenRequest = new translateToLowValueTokenRequest
            {
                orderId = "2121",
                token = "22",
            };
            translateToLowValueTokenRequest.id = "id";
            translateToLowValueTokenRequest.reportGroup = "Planets";
            cnpBatchRequest.addTranslateToLowValueTokenRequest(translateToLowValueTokenRequest);

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
            authorization2.typeOfDigitalCurrency = "type";
            authorization2.conversionAffiliateId = "conversion";
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
            partialCapture cap = new partialCapture();
            cap.partialCaptureSequenceNumber = 5;
            cap.partialCaptureTotalCount = 5;
            capture.partialCapture=cap;
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
            capturegivenauth.typeOfDigitalCurrency = "type";
            capturegivenauth.conversionAffiliateId = "conversion";
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
            saleObj2.typeOfDigitalCurrency = "Bcoin";
            saleObj2.conversionAffiliateId = "DC12345";
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
            _cnp.addBatch(cnpBatchRequest);

            var batchName = _cnp.sendToCnp();

            _cnp.blockAndWaitForResponse(batchName, estimatedResponseTime(2 * 2, 10 * 2));

            var cnpResponse = _cnp.receiveFromCnp(batchName);

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

                var translateToLowValueTokenResponse = cnpBatchResponse.nextTranslateToLowValueTokenResponse();
                while (translateToLowValueTokenResponse != null)
                {
                    Assert.NotNull(translateToLowValueTokenResponse.message);
                    Assert.NotNull(translateToLowValueTokenResponse.response);

                    translateToLowValueTokenResponse = cnpBatchResponse.nextTranslateToLowValueTokenResponse();
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
        public void AccountUpdateBatch()
        {
            var cnpBatchRequest = new batchRequest();

            var accountUpdate1 = new accountUpdate();
            accountUpdate1.orderId = "1111";
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "414100000000000000";
            card.expDate = "1210";
            accountUpdate1.card = card;
            accountUpdate1.id = "id";

            cnpBatchRequest.addAccountUpdate(accountUpdate1);

            var accountUpdate2 = new accountUpdate();
            accountUpdate2.orderId = "1112";
            accountUpdate2.card = card;
            accountUpdate2.id = "id";

            cnpBatchRequest.addAccountUpdate(accountUpdate2);

            _cnp.addBatch(cnpBatchRequest);
            var batchName = _cnp.sendToCnp();

            _cnp.blockAndWaitForResponse(batchName, estimatedResponseTime(0, 1 * 2));

            var cnpResponse = _cnp.receiveFromCnp(batchName);

            Assert.NotNull(cnpResponse);
            Assert.AreEqual("0", cnpResponse.response);
            Assert.AreEqual("Valid Format", cnpResponse.message);

            var cnpBatchResponse = cnpResponse.nextBatchResponse();
            while (cnpBatchResponse != null)
            {
                var accountUpdateResponse = cnpBatchResponse.nextAccountUpdateResponse();
                while (accountUpdateResponse != null)
                {
                    Assert.AreEqual("301", accountUpdateResponse.response);

                    accountUpdateResponse = cnpBatchResponse.nextAccountUpdateResponse();
                }
                cnpBatchResponse = cnpResponse.nextBatchResponse();
            }
        }

        [Test]
        public void RFRBatch()
        {
            var cnpBatchRequest = new batchRequest();
            cnpBatchRequest.id = "1234567A";

            var accountUpdate1 = new accountUpdate();
            accountUpdate1.orderId = new Random().Next(100000).ToString();
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4242424242424242";
            card.expDate = "1223";
            accountUpdate1.card = card;
            accountUpdate1.id = new Random().Next(100000000).ToString();

            cnpBatchRequest.addAccountUpdate(accountUpdate1);

            var accountUpdate2 = new accountUpdate();
            accountUpdate2.orderId = new Random().Next(1000000).ToString();
            accountUpdate2.card = card;
            accountUpdate2.id = new Random().Next(10000000).ToString(); ;

            cnpBatchRequest.addAccountUpdate(accountUpdate2);
            _cnp.addBatch(cnpBatchRequest);

            var batchName = _cnp.sendToCnp();
            _cnp.blockAndWaitForResponse(batchName, estimatedResponseTime(0, 1 * 2));
            var cnpResponse = _cnp.receiveFromCnp(batchName);

            Assert.NotNull(cnpResponse);

            var cnpBatchResponse = cnpResponse.nextBatchResponse();
            Assert.NotNull(cnpBatchResponse);
            while (cnpBatchResponse != null)
            {
                var accountUpdateResponse = cnpBatchResponse.nextAccountUpdateResponse();
                Assert.NotNull(accountUpdateResponse);
                while (accountUpdateResponse != null)
                {
                    Assert.AreEqual("000", accountUpdateResponse.response);

                    accountUpdateResponse = cnpBatchResponse.nextAccountUpdateResponse();
                }
                cnpBatchResponse = cnpResponse.nextBatchResponse();
            }

            var cnpRfr = new cnpRequest();
            var rfrRequest = new RFRRequest();
            var accountUpdateFileRequestData = new accountUpdateFileRequestData();
            accountUpdateFileRequestData.merchantId = Properties.Settings.Default.merchantId;
            accountUpdateFileRequestData.postDay = DateTime.Now;
            rfrRequest.accountUpdateFileRequestData = accountUpdateFileRequestData;

            cnpRfr.addRFRRequest(rfrRequest);

            var rfrBatchName = cnpRfr.sendToCnp();
            
            try
            {
                _cnp.blockAndWaitForResponse(rfrBatchName, 120000);
                var cnpRfrResponse = _cnp.receiveFromCnp(rfrBatchName);
                Assert.NotNull(cnpRfrResponse);
                var rfrResponse = cnpRfrResponse.nextRFRResponse();
                Assert.NotNull(rfrResponse);
                while (rfrResponse != null)
                {
                    Assert.AreEqual("1", rfrResponse.response);
                    Assert.AreEqual("The account update file is not ready yet.  Please try again later.", rfrResponse.message);
                    rfrResponse = cnpResponse.nextRFRResponse();
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }

        [Test]
        public void NullBatchData()
        {
            var cnpBatchRequest = new batchRequest();

            var authorization = new authorization();
            authorization.reportGroup = "Planets";
            authorization.orderId = "12344";
            authorization.amount = 106;
            authorization.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "414100000000000000";
            card.expDate = "1210";
            authorization.card = card;

            cnpBatchRequest.addAuthorization(authorization);
            try
            {
                cnpBatchRequest.addAuthorization(null);
            }
            catch (System.NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            var reversal = new authReversal();
            reversal.cnpTxnId = 12345678000L;
            reversal.amount = 106;
            reversal.payPalNotes = "Notes";

            cnpBatchRequest.addAuthReversal(reversal);
            try
            {
                cnpBatchRequest.addAuthReversal(null);
            }
            catch (System.NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            var capture = new capture();
            capture.cnpTxnId = 123456000;
            capture.amount = 106;
            capture.payPalNotes = "Notes";

            cnpBatchRequest.addCapture(capture);
            try
            {
                cnpBatchRequest.addCapture(null);
            }
            catch (System.NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

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

            cnpBatchRequest.addCaptureGivenAuth(capturegivenauth);
            try
            {
                cnpBatchRequest.addCaptureGivenAuth(null);
            }
            catch (System.NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            var creditObj = new credit();
            creditObj.amount = 106;
            creditObj.orderId = "2111";
            creditObj.orderSource = orderSourceType.ecommerce;
            creditObj.card = card;

            cnpBatchRequest.addCredit(creditObj);
            try
            {
                cnpBatchRequest.addCredit(null);
            }
            catch (System.NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            var echeckcredit = new echeckCredit();
            echeckcredit.amount = 12L;
            echeckcredit.orderId = "12345";
            echeckcredit.orderSource = orderSourceType.ecommerce;
            var echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "12345657890";
            echeck.routingNum = "011201995";
            echeck.checkNum = "123455";
            echeckcredit.echeck = echeck;
            var billToAddress = new contact();
            billToAddress.name = "Bob";
            billToAddress.city = "Lowell";
            billToAddress.state = "MA";
            billToAddress.email = "cnp.com";
            echeckcredit.billToAddress = billToAddress;

            cnpBatchRequest.addEcheckCredit(echeckcredit);
            try
            {
                cnpBatchRequest.addEcheckCredit(null);
            }
            catch (System.NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            var echeckredeposit = new echeckRedeposit();
            echeckredeposit.cnpTxnId = 123456;
            echeckredeposit.echeck = echeck;

            cnpBatchRequest.addEcheckRedeposit(echeckredeposit);
            try
            {
                cnpBatchRequest.addEcheckRedeposit(null);
            }
            catch (System.NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            var echeckSaleObj = new echeckSale();
            echeckSaleObj.amount = 123456;
            echeckSaleObj.orderId = "12345";
            echeckSaleObj.orderSource = orderSourceType.ecommerce;
            echeckSaleObj.echeck = echeck;
            echeckSaleObj.billToAddress = billToAddress;

            cnpBatchRequest.addEcheckSale(echeckSaleObj);
            try
            {
                cnpBatchRequest.addEcheckSale(null);
            }
            catch (System.NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            var echeckVerificationObject = new echeckVerification();
            echeckVerificationObject.amount = 123456;
            echeckVerificationObject.orderId = "12345";
            echeckVerificationObject.orderSource = orderSourceType.ecommerce;
            echeckVerificationObject.echeck = echeck;
            echeckVerificationObject.billToAddress = billToAddress;

            cnpBatchRequest.addEcheckVerification(echeckVerificationObject);
            try
            {
                cnpBatchRequest.addEcheckVerification(null);
            }
            catch (System.NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            var forcecapture = new forceCapture();
            forcecapture.amount = 106;
            forcecapture.orderId = "12344";
            forcecapture.orderSource = orderSourceType.ecommerce;
            forcecapture.card = card;

            cnpBatchRequest.addForceCapture(forcecapture);
            try
            {
                cnpBatchRequest.addForceCapture(null);
            }
            catch (System.NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            var saleObj = new sale();
            saleObj.amount = 106;
            saleObj.cnpTxnId = 123456;
            saleObj.orderId = "12344";
            saleObj.orderSource = orderSourceType.ecommerce;
            saleObj.card = card;

            cnpBatchRequest.addSale(saleObj);
            try
            {
                cnpBatchRequest.addSale(null);
            }
            catch (System.NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            var registerTokenRequest = new registerTokenRequestType();
            registerTokenRequest.orderId = "12344";
            registerTokenRequest.accountNumber = "1233456789103801";
            registerTokenRequest.reportGroup = "Planets";

            cnpBatchRequest.addRegisterTokenRequest(registerTokenRequest);
            try
            {
                cnpBatchRequest.addRegisterTokenRequest(null);
            }
            catch (System.NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            try
            {
                _cnp.addBatch(cnpBatchRequest);
            }
            catch (System.NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }

        [Test]
        public void InvalidCredientialsBatch()
        {
            var cnpIC = new cnpRequest(_invalidConfig);

            var cnpBatchRequest = new batchRequest();

            var authorization = new authorization();
            authorization.reportGroup = "Planets";
            authorization.orderId = "12344";
            authorization.amount = 106;
            authorization.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000001";
            card.expDate = "1210";
            authorization.card = card; //This needs to compile      

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
            authorization2.card = card2; //This needs to compile

            cnpBatchRequest.addAuthorization(authorization2);

            var reversal = new authReversal
            {
                cnpTxnId = 12345678000L,
                amount = 106,
                payPalNotes = "Notes"
            };

            cnpBatchRequest.addAuthReversal(reversal);

            var reversal2 = new authReversal();
            reversal2.cnpTxnId = 12345678900L;
            reversal2.amount = 106;
            reversal2.payPalNotes = "Notes";

            cnpBatchRequest.addAuthReversal(reversal2);

            var capture = new capture();
            capture.cnpTxnId = 123456000;
            capture.amount = 106;
            capture.payPalNotes = "Notes";

            cnpBatchRequest.addCapture(capture);

            var capture2 = new capture();
            capture2.cnpTxnId = 123456700;
            capture2.amount = 106;
            capture2.payPalNotes = "Notes";

            cnpBatchRequest.addCapture(capture2);

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

            cnpBatchRequest.addCaptureGivenAuth(capturegivenauth2);

            var creditObj = new credit();
            creditObj.amount = 106;
            creditObj.orderId = "2111";
            creditObj.orderSource = orderSourceType.ecommerce;
            creditObj.card = card;

            cnpBatchRequest.addCredit(creditObj);

            var creditObj2 = new credit();
            creditObj2.amount = 106;
            creditObj2.orderId = "2111";
            creditObj2.orderSource = orderSourceType.ecommerce;
            creditObj2.card = card2;

            cnpBatchRequest.addCredit(creditObj2);

            var echeckcredit = new echeckCredit
            {
                amount = 12L,
                orderId = "12345",
                orderSource = orderSourceType.ecommerce
            };
            var echeck = new echeckType
            {
                accType = echeckAccountTypeEnum.Checking,
                accNum = "1099999903",
                routingNum = "011201995",
                checkNum = "123455"
            };
            echeckcredit.echeck = echeck;
            var billToAddress = new contact
            {
                name = "Bob",
                city = "Lowell",
                state = "MA",
                email = "cnp.com"
            };
            echeckcredit.billToAddress = billToAddress;

            cnpBatchRequest.addEcheckCredit(echeckcredit);

            var echeckcredit2 = new echeckCredit
            {
                amount = 12L,
                orderId = "12346",
                orderSource = orderSourceType.ecommerce
            };
            var echeck2 = new echeckType
            {
                accType = echeckAccountTypeEnum.Checking,
                accNum = "1099999903",
                routingNum = "011201995",
                checkNum = "123456"
            };
            echeckcredit2.echeck = echeck2;
            var billToAddress2 = new contact
            {
                name = "Mike",
                city = "Lowell",
                state = "MA",
                email = "cnp.com"
            };
            echeckcredit2.billToAddress = billToAddress2;

            cnpBatchRequest.addEcheckCredit(echeckcredit2);

            var echeckredeposit = new echeckRedeposit
            {
                cnpTxnId = 123456,
                echeck = echeck
            };

            cnpBatchRequest.addEcheckRedeposit(echeckredeposit);

            var echeckredeposit2 = new echeckRedeposit
            {
                cnpTxnId = 123457,
                echeck = echeck2
            };

            cnpBatchRequest.addEcheckRedeposit(echeckredeposit2);

            var echeckSaleObj = new echeckSale
            {
                amount = 123456,
                orderId = "12345",
                orderSource = orderSourceType.ecommerce,
                echeck = echeck,
                billToAddress = billToAddress
            };

            cnpBatchRequest.addEcheckSale(echeckSaleObj);

            var echeckSaleObj2 = new echeckSale
            {
                amount = 123456,
                orderId = "12346",
                orderSource = orderSourceType.ecommerce,
                echeck = echeck2,
                billToAddress = billToAddress2
            };

            cnpBatchRequest.addEcheckSale(echeckSaleObj2);

            var echeckVerificationObject = new echeckVerification
            {
                amount = 123456,
                orderId = "12345",
                orderSource = orderSourceType.ecommerce,
                echeck = echeck,
                billToAddress = billToAddress
            };

            cnpBatchRequest.addEcheckVerification(echeckVerificationObject);

            var echeckVerificationObject2 = new echeckVerification
            {
                amount = 123456,
                orderId = "12346",
                orderSource = orderSourceType.ecommerce,
                echeck = echeck2,
                billToAddress = billToAddress2
            };

            cnpBatchRequest.addEcheckVerification(echeckVerificationObject2);

            var forcecapture = new forceCapture
            {
                amount = 106,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                card = card
            };

            cnpBatchRequest.addForceCapture(forcecapture);

            var forcecapture2 = new forceCapture
            {
                amount = 106,
                orderId = "12345",
                orderSource = orderSourceType.ecommerce,
                card = card2
            };

            cnpBatchRequest.addForceCapture(forcecapture2);

            var saleObj = new sale
            {
                amount = 106,
                cnpTxnId = 123456,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                card = card
            };

            cnpBatchRequest.addSale(saleObj);

            var saleObj2 = new sale
            {
                amount = 106,
                cnpTxnId = 123456,
                orderId = "12345",
                orderSource = orderSourceType.ecommerce,
                card = card2
            };

            cnpBatchRequest.addSale(saleObj2);

            var registerTokenRequest = new registerTokenRequestType
            {
                orderId = "12344",
                accountNumber = "1233456789103801",
                reportGroup = "Planets"
            };

            cnpBatchRequest.addRegisterTokenRequest(registerTokenRequest);

            var registerTokenRequest2 = new registerTokenRequestType
            {
                orderId = "12345",
                accountNumber = "1233456789103801",
                reportGroup = "Planets"
            };

            cnpBatchRequest.addRegisterTokenRequest(registerTokenRequest2);

            cnpIC.addBatch(cnpBatchRequest);

            try
            {
                var batchName = cnpIC.sendToCnp();

                cnpIC.blockAndWaitForResponse(batchName, 60*1000*5);
                var cnpResponse = cnpIC.receiveFromCnp(batchName);
                Assert.Fail("Fail to throw a connection exception");
            }
            catch (CnpOnlineException e)
            {
                Assert.AreEqual("Error occured while attempting to establish an SFTP connection", e.Message);
            }
        }

        [Test]
        public void InvalidSftpCredientialsBatch()
        {
            var cnpIsc = new cnpRequest(_invalidSftpConfig);

            var cnpBatchRequest = new batchRequest();

            var authorization = new authorization();
            authorization.reportGroup = "Planets";
            authorization.orderId = "12344";
            authorization.amount = 106;
            authorization.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000001";
            card.expDate = "1210";
            authorization.card = card; //This needs to compile      

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
            authorization2.card = card2; //This needs to compile

            cnpBatchRequest.addAuthorization(authorization2);

            var reversal = new authReversal();
            reversal.cnpTxnId = 12345678000L;
            reversal.amount = 106;
            reversal.payPalNotes = "Notes";

            cnpBatchRequest.addAuthReversal(reversal);

            var reversal2 = new authReversal();
            reversal2.cnpTxnId = 12345678900L;
            reversal2.amount = 106;
            reversal2.payPalNotes = "Notes";

            cnpBatchRequest.addAuthReversal(reversal2);

            var capture = new capture();
            capture.cnpTxnId = 123456000;
            capture.amount = 106;
            capture.payPalNotes = "Notes";

            cnpBatchRequest.addCapture(capture);

            var capture2 = new capture();
            capture2.cnpTxnId = 123456700;
            capture2.amount = 106;
            capture2.payPalNotes = "Notes";

            cnpBatchRequest.addCapture(capture2);

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

            cnpBatchRequest.addCaptureGivenAuth(capturegivenauth2);

            var creditObj = new credit();
            creditObj.amount = 106;
            creditObj.orderId = "2111";
            creditObj.orderSource = orderSourceType.ecommerce;
            creditObj.card = card;

            cnpBatchRequest.addCredit(creditObj);

            var creditObj2 = new credit();
            creditObj2.amount = 106;
            creditObj2.orderId = "2111";
            creditObj2.orderSource = orderSourceType.ecommerce;
            creditObj2.card = card2;

            cnpBatchRequest.addCredit(creditObj2);

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

            cnpBatchRequest.addEcheckCredit(echeckcredit2);

            var echeckredeposit = new echeckRedeposit();
            echeckredeposit.cnpTxnId = 123456;
            echeckredeposit.echeck = echeck;

            cnpBatchRequest.addEcheckRedeposit(echeckredeposit);

            var echeckredeposit2 = new echeckRedeposit();
            echeckredeposit2.cnpTxnId = 123457;
            echeckredeposit2.echeck = echeck2;

            cnpBatchRequest.addEcheckRedeposit(echeckredeposit2);

            var echeckSaleObj = new echeckSale();
            echeckSaleObj.amount = 123456;
            echeckSaleObj.orderId = "12345";
            echeckSaleObj.orderSource = orderSourceType.ecommerce;
            echeckSaleObj.echeck = echeck;
            echeckSaleObj.billToAddress = billToAddress;

            cnpBatchRequest.addEcheckSale(echeckSaleObj);

            var echeckSaleObj2 = new echeckSale();
            echeckSaleObj2.amount = 123456;
            echeckSaleObj2.orderId = "12346";
            echeckSaleObj2.orderSource = orderSourceType.ecommerce;
            echeckSaleObj2.echeck = echeck2;
            echeckSaleObj2.billToAddress = billToAddress2;

            cnpBatchRequest.addEcheckSale(echeckSaleObj2);

            var echeckVerificationObject = new echeckVerification();
            echeckVerificationObject.amount = 123456;
            echeckVerificationObject.orderId = "12345";
            echeckVerificationObject.orderSource = orderSourceType.ecommerce;
            echeckVerificationObject.echeck = echeck;
            echeckVerificationObject.billToAddress = billToAddress;

            cnpBatchRequest.addEcheckVerification(echeckVerificationObject);

            var echeckVerificationObject2 = new echeckVerification();
            echeckVerificationObject2.amount = 123456;
            echeckVerificationObject2.orderId = "12346";
            echeckVerificationObject2.orderSource = orderSourceType.ecommerce;
            echeckVerificationObject2.echeck = echeck2;
            echeckVerificationObject2.billToAddress = billToAddress2;

            cnpBatchRequest.addEcheckVerification(echeckVerificationObject2);

            var forcecapture = new forceCapture();
            forcecapture.amount = 106;
            forcecapture.orderId = "12344";
            forcecapture.orderSource = orderSourceType.ecommerce;
            forcecapture.card = card;

            cnpBatchRequest.addForceCapture(forcecapture);

            var forcecapture2 = new forceCapture();
            forcecapture2.amount = 106;
            forcecapture2.orderId = "12345";
            forcecapture2.orderSource = orderSourceType.ecommerce;
            forcecapture2.card = card2;

            cnpBatchRequest.addForceCapture(forcecapture2);

            var saleObj = new sale();
            saleObj.amount = 106;
            saleObj.cnpTxnId = 123456;
            saleObj.orderId = "12344";
            saleObj.orderSource = orderSourceType.ecommerce;
            saleObj.card = card;

            cnpBatchRequest.addSale(saleObj);

            var saleObj2 = new sale();
            saleObj2.amount = 106;
            saleObj2.cnpTxnId = 123456;
            saleObj2.orderId = "12345";
            saleObj2.orderSource = orderSourceType.ecommerce;
            saleObj2.card = card2;

            cnpBatchRequest.addSale(saleObj2);

            var registerTokenRequest = new registerTokenRequestType();
            registerTokenRequest.orderId = "12344";
            registerTokenRequest.accountNumber = "1233456789103801";
            registerTokenRequest.reportGroup = "Planets";

            cnpBatchRequest.addRegisterTokenRequest(registerTokenRequest);

            var registerTokenRequest2 = new registerTokenRequestType();
            registerTokenRequest2.orderId = "12345";
            registerTokenRequest2.accountNumber = "1233456789103801";
            registerTokenRequest2.reportGroup = "Planets";

            cnpBatchRequest.addRegisterTokenRequest(registerTokenRequest2);

            cnpIsc.addBatch(cnpBatchRequest);

            try
            {
                var batchName = cnpIsc.sendToCnp();
                Assert.Fail("Fail to throw a connection exception");
            }
            catch (CnpOnlineException e)
            {
                Assert.AreSame("Error occured while attempting to establish an SFTP connection", e.Message);
            }
        }

        [Test]
        public void SimpleBatchWithSpecialCharacters()
        {
            var cnpBatchRequest = new batchRequest();

            var authorization = new authorization();
            authorization.reportGroup = "<ReportGroup>";
            authorization.orderId = "12344&'\'";
            authorization.amount = new Random().Next(10000);
            authorization.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000001";
            card.expDate = "1223";
            authorization.card = card;
            authorization.id = new Random().Next(1000).ToString();

            cnpBatchRequest.addAuthorization(authorization);

            _cnp.addBatch(cnpBatchRequest);

            var batchName = _cnp.sendToCnp();

            _cnp.blockAndWaitForResponse(batchName, estimatedResponseTime(2 * 2, 10 * 2));

            var cnpResponse = _cnp.receiveFromCnp(batchName);

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

                cnpBatchResponse = cnpResponse.nextBatchResponse();
            }
        }
        
        [Test]
        public void TestSimpleBatchWithDepositTransactionReversal()
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

            var reversal = new depositTransactionReversal
            {
                id = "1",
                reportGroup = "Planets",
                cnpTxnId = 12345678000L,
                amount = 106,
            };

            cnpBatchRequest.addDepositTransactionReversal(reversal);

            _cnp.addBatch(cnpBatchRequest);

            var batchName = _cnp.sendToCnp();

            _cnp.blockAndWaitForResponse(batchName, estimatedResponseTime(2 * 2, 10 * 2));

            var cnpResponse = _cnp.receiveFromCnp(batchName);

            Assert.NotNull(cnpResponse);
            Assert.AreEqual("0", cnpResponse.response);
            Assert.AreEqual("Valid Format", cnpResponse.message);

            var cnpBatchResponse = cnpResponse.nextBatchResponse();
            while (cnpBatchResponse != null)
            {
                var reversalResponse = cnpBatchResponse.nextDepositTransactionReversalResponse();
                while (reversalResponse != null)
                {
                    Assert.AreEqual("983", reversalResponse.response);

                    reversalResponse = cnpBatchResponse.nextDepositTransactionReversalResponse();
                }

                cnpBatchResponse = cnpResponse.nextBatchResponse();
            }
        }

        [Test]
        public void SimpleAuthSaleCaptureGivenAuthWithRetailerAddress()///12.24
        {
            var cnpBatchRequest = new batchRequest();
            var authorization = new authorization
            {
                id = new Random().Next(100).ToString(),
                reportGroup = "Planets",
                orderId = "12344",
                amount = new Random().Next(1000),
                orderSource = orderSourceType.ecommerce,
                crypto = false,
                orderChannel = orderChannelEnum.PHONE,
                fraudCheckStatus = "Not Approved",
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1223"

                },
                retailerAddress = new contact
                {
                    name = "Mikasa Ackerman",
                    addressLine1 = "1st Main Street",
                    city = "Burlington",
                    state = "MA",
                    country = countryTypeEnum.USA,
                    email = "mikasa@cnp.com",
                    zip = "01867-4456",
                    sellerId = "s1234",
                    url = "www.google.com"
                },
                additionalCOFData = new additionalCOFData()
                {
                    totalPaymentCount = "35",
                    paymentType = paymentTypeEnum.Fixed_Amount,
                    uniqueId = "12345wereew233",
                    frequencyOfMIT = frequencyOfMITEnum.BiWeekly,
                    validationReference = "re3298rhriw4wrw",
                    sequenceIndicator = 2
                },
                customBilling = new customBilling { phone = "1112223333" }
            };

            var capturegivenauth = new captureGivenAuth
            {
                id = new Random().Next(10000000).ToString(),
                amount = new Random().Next(100000),
                orderId = "12344",
                crypto = false,
                authInformation = new authInformation
                {
                    authDate = new DateTime(2002, 10, 9),
                    authCode = "543216",
                    authAmount = 12345,
                },
                orderSource = orderSourceType.ecommerce,
                card=authorization.card,
                retailerAddress = new contact
                {
                    name = "Mikasa Ackerman",
                    addressLine1 = "1st Main Street",
                    city = "Burlington",
                    state = "MA",
                    country = countryTypeEnum.USA,
                    email = "mikasa@cnp.com",
                    zip = "01867-4456",
                    sellerId = "s1234",
                    url = "www.google.com"
                },
                additionalCOFData = new additionalCOFData()
                {
                    totalPaymentCount = "35",
                    paymentType = paymentTypeEnum.Fixed_Amount,
                    uniqueId = "12345wereew233",
                    frequencyOfMIT = frequencyOfMITEnum.BiWeekly,
                    validationReference = "re3298rhriw4wrw",
                    sequenceIndicator = 2
                },
                processingType = processingType.initialCOF,
                originalNetworkTransactionId = "abc123",
                originalTransactionAmount = 123456789
            };

            var saleObj = new sale
            {
                id = new Random().Next(10000000).ToString(),
                amount = new Random().Next(1000000),
                reportGroup = "Planets",
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                crypto = false,
                orderChannel = orderChannelEnum.PHONE,
                fraudCheckStatus = "Not Approved",
                card = authorization.card,
                retailerAddress = new contact
                {
                    name = "Mikasa Ackerman",
                    addressLine1 = "1st Main Street",
                    city = "Burlington",
                    state = "MA",
                    country = countryTypeEnum.USA,
                    email = "mikasa@cnp.com",
                    zip = "01867-4456",
                    sellerId = "s1234",
                    url = "www.google.com"
                },
                additionalCOFData = new additionalCOFData()
                {
                    totalPaymentCount = "35",
                    paymentType = paymentTypeEnum.Fixed_Amount,
                    uniqueId = "12345wereew233",
                    frequencyOfMIT = frequencyOfMITEnum.BiWeekly,
                    validationReference = "re3298rhriw4wrw",
                    sequenceIndicator = 2
                },
                customBilling = new customBilling { phone = "1112223333" }
            };

            cnpBatchRequest.addAuthorization(authorization);
            cnpBatchRequest.addCaptureGivenAuth(capturegivenauth);
            cnpBatchRequest.addSale(saleObj);

            _cnp.addBatch(cnpBatchRequest);

            var batchName = _cnp.sendToCnp();

            _cnp.blockAndWaitForResponse(batchName, estimatedResponseTime(2 * 2, 10 * 2));

            var cnpResponse = _cnp.receiveFromCnp(batchName);

            Assert.NotNull(cnpResponse);
            Assert.AreEqual("0", cnpResponse.response);
            Assert.AreEqual("Valid Format", cnpResponse.message);

            var cnpBatchResponse = cnpResponse.nextBatchResponse();
            while (cnpBatchResponse != null)
            {
                var authResponse = cnpBatchResponse.nextAuthorizationResponse();
                while (authResponse != null)
                {
                    Assert.AreEqual("000", authResponse.response);

                    authResponse = cnpBatchResponse.nextAuthorizationResponse();
                }

                var captureGivenAuthResponse = cnpBatchResponse.nextCaptureGivenAuthResponse();
                while (captureGivenAuthResponse != null)
                {
                    Assert.AreEqual("000", captureGivenAuthResponse.response);

                    captureGivenAuthResponse = cnpBatchResponse.nextCaptureGivenAuthResponse();
                }

                var saleResponse = cnpBatchResponse.nextSaleResponse();
                while (saleResponse != null)
                {
                    Assert.AreEqual("000", saleResponse.response);

                    saleResponse = cnpBatchResponse.nextSaleResponse();
                }
                cnpBatchResponse = cnpResponse.nextBatchResponse();
            }
            
        }

        [Test]
        public void TestSimpleBatchWithRefundTransactionReversal()
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

            var reversal = new refundTransactionReversal
            {
                id = "1",
                reportGroup = "Planets",
                cnpTxnId = 12345678000L,
                amount = 106,
            };

            cnpBatchRequest.addRefundTransactionReversal(reversal);

            _cnp.addBatch(cnpBatchRequest);

            var batchName = _cnp.sendToCnp();

            _cnp.blockAndWaitForResponse(batchName, estimatedResponseTime(2 * 2, 10 * 2));

            var cnpResponse = _cnp.receiveFromCnp(batchName);

            Assert.NotNull(cnpResponse);
            Assert.AreEqual("0", cnpResponse.response);
            Assert.AreEqual("Valid Format", cnpResponse.message);

            var cnpBatchResponse = cnpResponse.nextBatchResponse();
            while (cnpBatchResponse != null)
            {
                var reversalResponse = cnpBatchResponse.nextRefundTransactionReversalResponse();
                while (reversalResponse != null)
                {
                    Assert.AreEqual("983", reversalResponse.response);

                    reversalResponse = cnpBatchResponse.nextRefundTransactionReversalResponse();
                }

                cnpBatchResponse = cnpResponse.nextBatchResponse();
            }
        }

        [Test]
        public void SimpleAuthSaleCaptureGivenAuthWithOverridePolicy()///12.25
        {
            var cnpBatchRequest = new batchRequest();
            var authorization = new authorization
            {
                id = new Random().Next(100).ToString(),
                reportGroup = "Planets",
                orderId = "12344",
                amount = new Random().Next(1000),
                orderSource = orderSourceType.ecommerce,
                crypto = false,
                orderChannel = orderChannelEnum.PHONE,
                fraudCheckStatus = "Not Approved",
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1223"

                },
                lodgingInfo = new lodgingInfo
                {
                    bookingID = "BID12345",
                    passengerName = "Pia Jaiswal",
                    propertyAddress = new propertyAddress
                    {
                        name = "Godrej",
                        city = "Pune",
                        region = "WES",
                        country = countryTypeEnum.IN
                    },
                    travelPackageIndicator = travelPackageIndicatorEnum.AirlineReservation,
                    smokingPreference = "N",
                    numberOfRooms = 1,
                    tollFreePhoneNumber = "1234567890"
                },
                overridePolicy = "Override Policy",
                fsErrorCode = "FS Error Code",
                merchantAccountStatus = "Merchant Account Status",
                productEnrolled = productEnrolledEnum.GUARPAY2,
                decisionPurpose = decisionPurposeEnum.INFORMATION_ONLY,
                fraudSwitchIndicator = fraudSwitchIndicatorEnum.PRE,
                customBilling = new customBilling { phone = "1112223333" }
            };

            cnpBatchRequest.addAuthorization(authorization);
            
            _cnp.addBatch(cnpBatchRequest);

            var batchName = _cnp.sendToCnp();

            _cnp.blockAndWaitForResponse(batchName, estimatedResponseTime(2 * 2, 10 * 2));

            var cnpResponse = _cnp.receiveFromCnp(batchName);

            Assert.NotNull(cnpResponse);
            Assert.AreEqual("0", cnpResponse.response);
            Assert.AreEqual("Valid Format", cnpResponse.message);

            var cnpBatchResponse = cnpResponse.nextBatchResponse();
            while (cnpBatchResponse != null)
            {
                var authResponse = cnpBatchResponse.nextAuthorizationResponse();
                while (authResponse != null)
                {
                    Assert.AreEqual("000", authResponse.response);

                    authResponse = cnpBatchResponse.nextAuthorizationResponse();
                }
                cnpBatchResponse = cnpResponse.nextBatchResponse();
            }
        }

        [Test]
        public void simpleAuthReversalWithAdditionalCOFdata()
        {
            var cnpBatchRequest = new batchRequest();
            var reversal = new authReversal()
            {
                cnpTxnId = 12345678000L,
                amount = 106,
                payPalNotes = "Notes",
                id = "id",
                additionalCOFData = new additionalCOFData()
                {
                    totalPaymentCount = "35",
                    paymentType = paymentTypeEnum.Fixed_Amount,
                    uniqueId = "12345wereew233",
                    frequencyOfMIT = frequencyOfMITEnum.BiWeekly,
                    validationReference = "re3298rhriw4wrw",
                    sequenceIndicator = 2
                }
            };
            cnpBatchRequest.addAuthReversal(reversal);

            _cnp.addBatch(cnpBatchRequest);

            var batchName = _cnp.sendToCnp();

            _cnp.blockAndWaitForResponse(batchName, estimatedResponseTime(2 * 2, 10 * 2));

            var cnpResponse = _cnp.receiveFromCnp(batchName);

            Assert.NotNull(cnpResponse);
            Assert.AreEqual("0", cnpResponse.response);
            Assert.AreEqual("Valid Format", cnpResponse.message);

            var cnpBatchResponse = cnpResponse.nextBatchResponse();
            while (cnpBatchResponse != null)
            {
                var authReversalResponse = cnpBatchResponse.nextAuthReversalResponse();
                while (authReversalResponse != null)
                {
                    Assert.AreEqual("000", authReversalResponse.response);

                    authReversalResponse = cnpBatchResponse.nextAuthReversalResponse();
                }
                cnpBatchResponse = cnpResponse.nextBatchResponse();
            }
        }

        // 12.28,12.29 and 12.30 start
        [Test]
        public void SimpleAuthSaleWithOrderchannelEnumMIT_SellerInfo_AuthIndicatorEstimatedEnum() //12.28 , 12.29 and 12.30
        {
            var cnpBatchRequest = new batchRequest();
            var authorization = new authorization
            {
                id = new Random().Next(100).ToString(),
                reportGroup = "Planets",
                orderId = "12344",
                amount = new Random().Next(1000),
                orderSource = orderSourceType.ecommerce,
                sellerInfo = new sellerInfo
                {
                    accountNumber = "4485581000000005",
                    aggregateOrderCount = 4,
                    aggregateOrderDollars = 100000,
                    sellerAddress = new sellerAddress
                    {
                        sellerStreetaddress = "15 Main Street",
                        sellerUnit = "100 AB",
                        sellerPostalcode = "12345",
                        sellerCity = "San Jose",
                        sellerProvincecode = "MA",
                        sellerCountrycode = "US"
                    },
                    createdDate = "2015-11-12T20:33:09",
                    domain = "vap",
                    email = "bob@example.com",
                    lastUpdateDate = "2015-11-12T20:33:09",
                    name = "bob",
                    onboardingEmail = "bob@example.com",
                    onboardingIpAddress = "75.100.88.78",
                    parentEntity = "abc",
                    phone = "9785510040",
                    sellerId = "123456789",
                    sellerTags = new sellerTagsType
                    {
                        tag = "2"
                    },
                    username = "bob123"

                },
                crypto = false,
                orderChannel = orderChannelEnum.MIT,
                fraudCheckStatus = "Not Approved",
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1223"

                },

                overridePolicy = "Override Policy",
                fsErrorCode = "FS Error Code",
                merchantAccountStatus = "Merchant Account Status",
                productEnrolled = productEnrolledEnum.GUARPAY2,
                decisionPurpose = decisionPurposeEnum.INFORMATION_ONLY,
                fraudSwitchIndicator = fraudSwitchIndicatorEnum.PRE,
                customBilling = new customBilling { phone = "1112223333" },
                authIndicator = authIndicatorEnum.Estimated
            };

            cnpBatchRequest.addAuthorization(authorization);

            _cnp.addBatch(cnpBatchRequest);

            var batchName = _cnp.sendToCnp();

            _cnp.blockAndWaitForResponse(batchName, estimatedResponseTime(2 * 2, 10 * 2));

            var cnpResponse = _cnp.receiveFromCnp(batchName);

            Assert.NotNull(cnpResponse);
            Assert.AreEqual("0", cnpResponse.response);
            Assert.AreEqual("Valid Format", cnpResponse.message);

            var cnpBatchResponse = cnpResponse.nextBatchResponse();
            while (cnpBatchResponse != null)
            {
                var authResponse = cnpBatchResponse.nextAuthorizationResponse();
                while (authResponse != null)
                {
                    Assert.AreEqual("000", authResponse.response);

                    authResponse = cnpBatchResponse.nextAuthorizationResponse();
                }
                cnpBatchResponse = cnpResponse.nextBatchResponse();
            }
        }
        // 12.28,12.29 and 12.30 end
        [Test]
        public void SimpleCaptureForceCaptureSaleCaptureGivenAuthWithForeignRetailerIndicator()//12.31
        {
            var cnpBatchRequest = new batchRequest();
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000001";
            card.expDate = "1210";

            var authorization = new authorization
            {
                id = new Random().Next(100).ToString(),
                reportGroup = "Planets",
                orderId = "12344",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                crypto = false,
                orderChannel = orderChannelEnum.PHONE,
                fraudCheckStatus = "Not Approved",
                card = card,
                retailerAddress = new contact
                {
                    name = "Mikasa Ackerman",
                    addressLine1 = "1st Main Street",
                    city = "Burlington",
                    state = "MA",
                    country = countryTypeEnum.USA,
                    email = "mikasa@cnp.com",
                    zip = "01867-4456",
                    sellerId = "s1234",
                    url = "www.google.com"
                },
                additionalCOFData = new additionalCOFData()
                {
                    totalPaymentCount = "35",
                    paymentType = paymentTypeEnum.Fixed_Amount,
                    uniqueId = "12345wereew233",
                    frequencyOfMIT = frequencyOfMITEnum.BiWeekly,
                    validationReference = "re3298rhriw4wrw",
                    sequenceIndicator = 2
                },
                customBilling = new customBilling { phone = "1112223333" }
            };

            var capture = new capture();
            capture.id = new Random().Next(100).ToString();
            capture.cnpTxnId = 123456000;
            capture.orderId = "12344";
            capture.amount = 106;
            capture.payPalNotes = "Notes";
            capture.foreignRetailerIndicator = foreignRetailerIndicatorEnum.F;

            var forcecapture = new forceCapture();
            forcecapture.id = new Random().Next(1000).ToString();
            forcecapture.amount = 106;
            forcecapture.orderId = "12344";
            forcecapture.orderSource = orderSourceType.ecommerce;
            forcecapture.card = card;
            forcecapture.foreignRetailerIndicator = foreignRetailerIndicatorEnum.F;

            var capturegivenauth = new captureGivenAuth
            {
                id = new Random().Next(10000000).ToString(),
                amount = new Random().Next(100000),
                orderId = "12344",
                crypto = false,
                authInformation = new authInformation
                {
                    authDate = new DateTime(2002, 10, 9),
                    authCode = "543216",
                    authAmount = 12345,
                },
                orderSource = orderSourceType.ecommerce,
                card = card,
                retailerAddress = new contact
                {
                    name = "Mikasa Ackerman",
                    addressLine1 = "1st Main Street",
                    city = "Burlington",
                    state = "MA",
                    country = countryTypeEnum.USA,
                    email = "mikasa@cnp.com",
                    zip = "01867-4456",
                    sellerId = "s1234",
                    url = "www.google.com"
                },
                additionalCOFData = new additionalCOFData()
                {
                    totalPaymentCount = "35",
                    paymentType = paymentTypeEnum.Fixed_Amount,
                    uniqueId = "12345wereew233",
                    frequencyOfMIT = frequencyOfMITEnum.BiWeekly,
                    validationReference = "re3298rhriw4wrw",
                    sequenceIndicator = 2
                },
                processingType = processingType.initialCOF,
                originalNetworkTransactionId = "abc123",
                originalTransactionAmount = 123456789,
                foreignRetailerIndicator = foreignRetailerIndicatorEnum.F,
                accountFundingTransactionData = new accountFundingTransactionData()
                {
                    receiverFirstName = "abcc",
                    receiverLastName = "cde",
                    receiverCountry = countryTypeEnum.US,
                    receiverState = stateTypeEnum.AL,
                    receiverAccountNumberType = accountFundingTransactionAccountNumberTypeEnum.cardAccount,
                    receiverAccountNumber = "4141000",
                    accountFundingTransactionType = accountFundingTransactionTypeEnum.accountToAccount
                }
            };
            var saleObj = new sale
            {
                id = new Random().Next(10000000).ToString(),
                amount = 1234567,
                reportGroup = "Planets",
                orderId = "1234984",
                orderSource = orderSourceType.ecommerce,
                crypto = false,
                orderChannel = orderChannelEnum.PHONE,
                fraudCheckStatus = "Not Approved",
                card = card,
                retailerAddress = new contact
                {
                    name = "Mikasa Ackerman",
                    addressLine1 = "1st Main Street",
                    city = "Burlington",
                    state = "MA",
                    country = countryTypeEnum.USA,
                    email = "mikasa@cnp.com",
                    zip = "01867-4456",
                    sellerId = "s1234",
                    url = "www.google.com"
                },
                additionalCOFData = new additionalCOFData()
                {
                    totalPaymentCount = "35",
                    paymentType = paymentTypeEnum.Fixed_Amount,
                    uniqueId = "12345wereew233",
                    frequencyOfMIT = frequencyOfMITEnum.BiWeekly,
                    validationReference = "re3298rhriw4wrw",
                    sequenceIndicator = 2
                },
                customBilling = new customBilling { phone = "1112223333" },
                foreignRetailerIndicator = foreignRetailerIndicatorEnum.F
            };

            cnpBatchRequest.addAuthorization(authorization);
            cnpBatchRequest.addCapture(capture);
            cnpBatchRequest.addForceCapture(forcecapture);
            cnpBatchRequest.addCaptureGivenAuth(capturegivenauth);
            cnpBatchRequest.addSale(saleObj);

            _cnp.addBatch(cnpBatchRequest);

            var batchName = _cnp.sendToCnp();

            _cnp.blockAndWaitForResponse(batchName, estimatedResponseTime(2 * 2, 10 * 2));

            var cnpResponse = _cnp.receiveFromCnp(batchName);

            Assert.NotNull(cnpResponse);
            Assert.AreEqual("0", cnpResponse.response);
            Assert.AreEqual("Valid Format", cnpResponse.message);

            var cnpBatchResponse = cnpResponse.nextBatchResponse();
            while (cnpBatchResponse != null)
            {
                var authResponse = cnpBatchResponse.nextAuthorizationResponse();
                while (authResponse != null)
                {
                    Assert.AreEqual("000", authResponse.response);

                    authResponse = cnpBatchResponse.nextAuthorizationResponse();
                }

                var captureResponse = cnpBatchResponse.nextCaptureResponse();
                while (captureResponse != null)
                {
                    Assert.AreEqual("000", captureResponse.response);

                    captureResponse = cnpBatchResponse.nextCaptureResponse();
                }

                var forceCaptureResponse = cnpBatchResponse.nextForceCaptureResponse();
                while (forceCaptureResponse != null)
                {
                    Assert.AreEqual("000", forceCaptureResponse.response);

                    forceCaptureResponse = cnpBatchResponse.nextForceCaptureResponse();
                }

                var captureGivenAuthResponse = cnpBatchResponse.nextCaptureGivenAuthResponse();
                while (captureGivenAuthResponse != null)
                {
                    Assert.AreEqual("000", captureGivenAuthResponse.response);

                    captureGivenAuthResponse = cnpBatchResponse.nextCaptureGivenAuthResponse();
                }

                var saleResponse = cnpBatchResponse.nextSaleResponse();
                while (saleResponse != null)
                {
                    Assert.AreEqual("000", saleResponse.response);

                    saleResponse = cnpBatchResponse.nextSaleResponse();
                }
                cnpBatchResponse = cnpResponse.nextBatchResponse();
            }

        }
        //12.31 end

        [Test]
        public void SimpleAuthCaptureForceCaptureSaleCaptureGivenAuthWithShipmentIDSunscription()//12.33
        {
            var cnpBatchRequest = new batchRequest();
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000001";
            card.expDate = "1210";
            var authorization = new authorization
            {
                id = new Random().Next(100).ToString(),
                reportGroup = "Planets",
                orderId = "12344",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                crypto = false,
                orderChannel = orderChannelEnum.PHONE,
                fraudCheckStatus = "Not Approved",
                card = card,
                enhancedData = new enhancedData
                {
                    customerReference = "000000008110801",
                    salesTax = 23,
                    deliveryType = enhancedDataDeliveryType.DIG,
                    taxExempt = false,
                    detailTaxes = new List<detailTax>(),
                    lineItems = new List<lineItemData>(),
                },
                customBilling = new customBilling { phone = "1112223333" },
                accountFundingTransactionData = new accountFundingTransactionData()
                {
                    receiverFirstName = "abcc",
                    receiverLastName = "cde",
                    receiverCountry = countryTypeEnum.US,
                    receiverState = stateTypeEnum.AL,
                    receiverAccountNumberType = accountFundingTransactionAccountNumberTypeEnum.cardAccount,
                    receiverAccountNumber = "4141000",
                    accountFundingTransactionType = accountFundingTransactionTypeEnum.accountToAccount
                }
            };

            var capture = new capture
            {
                id = "1",
                cnpTxnId = 123456000,
                orderId = "defaultOrderId",
                amount = 106,
                enhancedData = new enhancedData
                {
                    customerReference = "000000008110801",
                    salesTax = 27,
                    deliveryType = enhancedDataDeliveryType.DIG,
                    taxExempt = false,
                    lineItems = new List<lineItemData>(),
                },
                partialCapture = new partialCapture
                {
                    partialCaptureSequenceNumber = 5,
                    partialCaptureTotalCount= 5,
                }

            };

            var capturegivenauth = new captureGivenAuth
            {
                id = "1",
                amount = 1176,
                orderId = "12377",
                crypto = false,
                authInformation = new authInformation
                {
                    authDate = new DateTime(2023, 4, 9),
                    authCode = "543216",
                    authAmount = 6532,
                },
                orderSource = orderSourceType.ecommerce,
                card = card,
                enhancedData = new enhancedData
                {
                    customerReference = "000000008110801",
                    salesTax = 27,
                    deliveryType = enhancedDataDeliveryType.DIG,
                    taxExempt = false,
                    lineItems = new List<lineItemData>(),
                },
                accountFundingTransactionData = new accountFundingTransactionData()
                {
                    receiverFirstName = "abcc",
                    receiverLastName = "cde",
                    receiverCountry = countryTypeEnum.US,
                    receiverState = stateTypeEnum.AL,
                    receiverAccountNumberType = accountFundingTransactionAccountNumberTypeEnum.cardAccount,
                    receiverAccountNumber = "4141000",
                    accountFundingTransactionType = accountFundingTransactionTypeEnum.accountToAccount
                }

            };
            capturegivenauth.foreignRetailerIndicator = foreignRetailerIndicatorEnum.F;

            var forcecapture = new forceCapture
            {
                id = new Random().Next(1000).ToString(),
                amount = 106,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                card = card,
                enhancedData = new enhancedData
                {
                    customerReference = "000000008110801",
                    salesTax = 23,
                    deliveryType = enhancedDataDeliveryType.DIG,
                    taxExempt = false,
                    detailTaxes = new List<detailTax>(),
                    lineItems = new List<lineItemData>(),

                },
                foreignRetailerIndicator = foreignRetailerIndicatorEnum.F,
                accountFundingTransactionData = new accountFundingTransactionData()
                {
                    receiverFirstName = "abcc",
                    receiverLastName = "cde",
                    receiverCountry = countryTypeEnum.US,
                    receiverState = stateTypeEnum.AL,
                    receiverAccountNumberType = accountFundingTransactionAccountNumberTypeEnum.cardAccount,
                    receiverAccountNumber = "4141000",
                    accountFundingTransactionType = accountFundingTransactionTypeEnum.accountToAccount
                },

            };

            var saleObj = new sale
            {
                amount = 106,
                cnpTxnId = 123456,
                id = "1",
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1210"
                },
                cardholderAuthentication = new fraudCheckType
                {
                    customerIpAddress = "127.1.1",
                    authenticationProtocolVersion="9"
                },
                enhancedData = new enhancedData
                {
                    customerReference = "000000008110801",
                    salesTax = 23,
                    deliveryType = enhancedDataDeliveryType.DIG,
                    taxExempt = false,
                    lineItems = new List<lineItemData>(),

                },
                accountFundingTransactionData = new accountFundingTransactionData()
                {
                    receiverFirstName = "abcc",
                    receiverLastName = "cde",
                    receiverCountry = countryTypeEnum.US,
                    receiverState = stateTypeEnum.AL,
                    receiverAccountNumberType = accountFundingTransactionAccountNumberTypeEnum.cardAccount,
                    receiverAccountNumber = "4141000",
                    accountFundingTransactionType = accountFundingTransactionTypeEnum.accountToAccount
                },
                fraudCheckAction = fraudCheckActionEnum.APPROVED_SKIP_FRAUD_CHECK,

            };

            var mysubscription = new subscriptions();
            mysubscription.subscriptionId = "123";
            mysubscription.currentPeriod = 112;
            mysubscription.periodUnit = periodUnit.WEEK;
            mysubscription.numberOfPeriods = 131;
            mysubscription.regularItemPrice = 169;
            mysubscription.nextDeliveryDate = new DateTime(2023, 3, 2);

            var mylineItemData = new lineItemData();
            mylineItemData.itemSequenceNumber = 1;
            mylineItemData.itemDescription = "Ecomm";
            mylineItemData.productCode = "El11";
            mylineItemData.itemCategory = "Ele Appiances";
            mylineItemData.itemSubCategory = "home appliaces";
            mylineItemData.productId = "1111";
            mylineItemData.productName = "dryer";
            mylineItemData.shipmentId = "2593";
            mylineItemData.subscription.Add(mysubscription);

            authorization.enhancedData.lineItems.Add(mylineItemData);
            capture.enhancedData.lineItems.Add(mylineItemData);
            capturegivenauth.enhancedData.lineItems.Add(mylineItemData);
            forcecapture.enhancedData.lineItems.Add(mylineItemData);
            saleObj.enhancedData.lineItems.Add(mylineItemData);

            cnpBatchRequest.addAuthorization(authorization);
            cnpBatchRequest.addCapture(capture);
            cnpBatchRequest.addCaptureGivenAuth(capturegivenauth);
            cnpBatchRequest.addForceCapture(forcecapture);
            cnpBatchRequest.addSale(saleObj);

            _cnp.addBatch(cnpBatchRequest);

            var batchName = _cnp.sendToCnp();

            _cnp.blockAndWaitForResponse(batchName, estimatedResponseTime(2 * 2, 10 * 2));

            var cnpResponse = _cnp.receiveFromCnp(batchName);

            Assert.NotNull(cnpResponse);
            Assert.AreEqual("0", cnpResponse.response);
            Assert.AreEqual("Valid Format", cnpResponse.message);

            var cnpBatchResponse = cnpResponse.nextBatchResponse();
            while (cnpBatchResponse != null)
            {
                var authResponse = cnpBatchResponse.nextAuthorizationResponse();
                while (authResponse != null)
                {
                    Assert.AreEqual("000", authResponse.response);

                    authResponse = cnpBatchResponse.nextAuthorizationResponse();
                }

                var captureResponse = cnpBatchResponse.nextCaptureResponse();
                while (captureResponse != null)
                {
                    Assert.AreEqual("000", captureResponse.response);

                    captureResponse = cnpBatchResponse.nextCaptureResponse();
                }

                var forceCaptureResponse = cnpBatchResponse.nextForceCaptureResponse();
                while (forceCaptureResponse != null)
                {
                    Assert.AreEqual("000", forceCaptureResponse.response);

                    forceCaptureResponse = cnpBatchResponse.nextForceCaptureResponse();
                }

                var captureGivenAuthResponse = cnpBatchResponse.nextCaptureGivenAuthResponse();
                while (captureGivenAuthResponse != null)
                {
                    Assert.AreEqual("000", captureGivenAuthResponse.response);

                    captureGivenAuthResponse = cnpBatchResponse.nextCaptureGivenAuthResponse();
                }

                var saleResponse = cnpBatchResponse.nextSaleResponse();
                while (saleResponse != null)
                {
                    Assert.AreEqual("000", saleResponse.response);

                    saleResponse = cnpBatchResponse.nextSaleResponse();
                }
                cnpBatchResponse = cnpResponse.nextBatchResponse();
            }

        } //12.33 End

        private int estimatedResponseTime(int numAuthsAndSales, int numRest)
        {
            return (int)(5 * 60 * 1000 + 2.5 * 1000 + numAuthsAndSales * (1 / 5) * 1000 + numRest * (1 / 50) * 1000) * 5;
        }
    }
}
