using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Cnp.Sdk;
using System.IO;

namespace Cnp.Sdk.Test.Functional
{
    public class TestBatchStream
    {
        // TODO: Rewrite tests to utilize Xunit rather than NUnit

        //private cnpRequest cnp;
        //private Dictionary<String, String> invalidConfig;
        //private Dictionary<String, String> invalidSftpConfig;

        //[TestFixtureSetUp]
        //public void setUp()
        //{
        //    invalidConfig = new Dictionary<String, String>();
        //    invalidConfig["url"] = Properties.Settings.Default.url;
        //    invalidConfig["reportGroup"] = Properties.Settings.Default.reportGroup;
        //    invalidConfig["username"] = "badUsername";
        //    invalidConfig["printxml"] = Properties.Settings.Default.printxml;
        //    invalidConfig["timeout"] = Properties.Settings.Default.timeout;
        //    invalidConfig["proxyHost"] = Properties.Settings.Default.proxyHost;
        //    invalidConfig["merchantId"] = Properties.Settings.Default.merchantId;
        //    invalidConfig["password"] = "badPassword";
        //    invalidConfig["proxyPort"] = Properties.Settings.Default.proxyPort;
        //    invalidConfig["sftpUrl"] = Properties.Settings.Default.sftpUrl;
        //    invalidConfig["sftpUsername"] = Properties.Settings.Default.sftpUrl;
        //    invalidConfig["sftpPassword"] = Properties.Settings.Default.sftpPassword;
        //    invalidConfig["knownHostsFile"] = Properties.Settings.Default.knownHostsFile;
            

        //    invalidSftpConfig = new Dictionary<String, String>();
        //    invalidSftpConfig["url"] = Properties.Settings.Default.url;
        //    invalidSftpConfig["reportGroup"] = Properties.Settings.Default.reportGroup;
        //    invalidSftpConfig["username"] = Properties.Settings.Default.username;
        //    invalidSftpConfig["printxml"] = Properties.Settings.Default.printxml;
        //    invalidSftpConfig["timeout"] = Properties.Settings.Default.timeout;
        //    invalidSftpConfig["proxyHost"] = Properties.Settings.Default.proxyHost;
        //    invalidSftpConfig["merchantId"] = Properties.Settings.Default.merchantId;
        //    invalidSftpConfig["password"] = Properties.Settings.Default.password;
        //    invalidSftpConfig["proxyPort"] = Properties.Settings.Default.proxyPort;
        //    invalidSftpConfig["sftpUrl"] = Properties.Settings.Default.sftpUrl;
        //    invalidSftpConfig["sftpUsername"] = "badSftpUsername";
        //    invalidSftpConfig["sftpPassword"] = "badSftpPassword";
        //    invalidSftpConfig["knownHostsFile"] = Properties.Settings.Default.knownHostsFile;
            
        //}

        //[SetUp]
        //public void setUpBeforeTest()
        //{
        //    cnp = new cnpRequest();
        //}

        //[Fact]
        //public void SimpleBatch()
        //{
        //    batchRequest cnpBatchRequest = new batchRequest();

        //    authorization authorization = new authorization();
        //    authorization.reportGroup = "Planets";
        //    authorization.orderId = "12344";
        //    authorization.amount = 106;
        //    authorization.orderSource = orderSourceType.ecommerce;
        //    cardType card = new cardType();
        //    card.type = methodOfPaymentTypeEnum.VI;
        //    card.number = "4100000000000001";
        //    card.expDate = "1210";
        //    authorization.card = card;
        //    authorization.id = "id";

        //    cnpBatchRequest.addAuthorization(authorization);

        //    authorization authorization2 = new authorization();
        //    authorization2.reportGroup = "Planets";
        //    authorization2.orderId = "12345";
        //    authorization2.amount = 106;
        //    authorization2.orderSource = orderSourceType.ecommerce;
        //    cardType card2 = new cardType();
        //    card2.type = methodOfPaymentTypeEnum.VI;
        //    card2.number = "4242424242424242";
        //    card2.expDate = "1210";
        //    authorization2.card = card2;
        //    authorization2.id = "id";

        //    cnpBatchRequest.addAuthorization(authorization2);

        //    authReversal reversal = new authReversal();
        //    reversal.cnpTxnId = 12345678000L;
        //    reversal.amount = 106;
        //    reversal.payPalNotes = "Notes";
        //    reversal.id = "id";

        //    cnpBatchRequest.addAuthReversal(reversal);

        //    authReversal reversal2 = new authReversal();
        //    reversal2.cnpTxnId = 12345678900L;
        //    reversal2.amount = 106;
        //    reversal2.payPalNotes = "Notes";
        //    reversal2.id = "id";

        //    cnpBatchRequest.addAuthReversal(reversal2);

        //    capture capture = new capture();
        //    capture.cnpTxnId = 123456000;
        //    capture.amount = 106;
        //    capture.payPalNotes = "Notes";
        //    capture.id = "id";

        //    cnpBatchRequest.addCapture(capture);

        //    capture capture2 = new capture();
        //    capture2.cnpTxnId = 123456700;
        //    capture2.amount = 106;
        //    capture2.payPalNotes = "Notes";
        //    capture2.id = "id";

        //    cnpBatchRequest.addCapture(capture2);

        //    captureGivenAuth capturegivenauth = new captureGivenAuth();
        //    capturegivenauth.amount = 106;
        //    capturegivenauth.orderId = "12344";
        //    authInformation authInfo = new authInformation();
        //    DateTime authDate = new DateTime(2002, 10, 9);
        //    authInfo.authDate = authDate;
        //    authInfo.authCode = "543216";
        //    authInfo.authAmount = 12345;
        //    capturegivenauth.authInformation = authInfo;
        //    capturegivenauth.orderSource = orderSourceType.ecommerce;
        //    capturegivenauth.card = card;
        //    capturegivenauth.id = "id";

        //    cnpBatchRequest.addCaptureGivenAuth(capturegivenauth);

        //    captureGivenAuth capturegivenauth2 = new captureGivenAuth();
        //    capturegivenauth2.amount = 106;
        //    capturegivenauth2.orderId = "12344";
        //    authInformation authInfo2 = new authInformation();
        //    authDate = new DateTime(2003, 10, 9);
        //    authInfo2.authDate = authDate;
        //    authInfo2.authCode = "543216";
        //    authInfo2.authAmount = 12345;
        //    capturegivenauth2.authInformation = authInfo;
        //    capturegivenauth2.orderSource = orderSourceType.ecommerce;
        //    capturegivenauth2.card = card2;
        //    capturegivenauth2.id = "id";

        //    cnpBatchRequest.addCaptureGivenAuth(capturegivenauth2);

        //    credit creditObj = new credit();
        //    creditObj.amount = 106;
        //    creditObj.orderId = "2111";
        //    creditObj.orderSource = orderSourceType.ecommerce;
        //    creditObj.card = card;
        //    creditObj.id = "id";

        //    cnpBatchRequest.addCredit(creditObj);

        //    credit creditObj2 = new credit();
        //    creditObj2.amount = 106;
        //    creditObj2.orderId = "2111";
        //    creditObj2.orderSource = orderSourceType.ecommerce;
        //    creditObj2.card = card2;
        //    creditObj2.id = "id";

        //    cnpBatchRequest.addCredit(creditObj2);

        //    echeckCredit echeckcredit = new echeckCredit();
        //    echeckcredit.amount = 12L;
        //    echeckcredit.orderId = "12345";
        //    echeckcredit.orderSource = orderSourceType.ecommerce;
        //    echeckType echeck = new echeckType();
        //    echeck.accType = echeckAccountTypeEnum.Checking;
        //    echeck.accNum = "1099999903";
        //    echeck.routingNum = "011201995";
        //    echeck.checkNum = "123455";
        //    echeckcredit.echeck = echeck;
        //    contact billToAddress = new contact();
        //    billToAddress.name = "Bob";
        //    billToAddress.city = "Lowell";
        //    billToAddress.state = "MA";
        //    billToAddress.email = "cnp.com";
        //    echeckcredit.billToAddress = billToAddress;
        //    echeckcredit.id = "id";

        //    cnpBatchRequest.addEcheckCredit(echeckcredit);

        //    echeckCredit echeckcredit2 = new echeckCredit();
        //    echeckcredit2.amount = 12L;
        //    echeckcredit2.orderId = "12346";
        //    echeckcredit2.orderSource = orderSourceType.ecommerce;
        //    echeckType echeck2 = new echeckType();
        //    echeck2.accType = echeckAccountTypeEnum.Checking;
        //    echeck2.accNum = "1099999903";
        //    echeck2.routingNum = "011201995";
        //    echeck2.checkNum = "123456";
        //    echeckcredit2.echeck = echeck2;
        //    contact billToAddress2 = new contact();
        //    billToAddress2.name = "Mike";
        //    billToAddress2.city = "Lowell";
        //    billToAddress2.state = "MA";
        //    billToAddress2.email = "cnp.com";
        //    echeckcredit2.billToAddress = billToAddress2;
        //    echeckcredit2.id = "id";

        //    cnpBatchRequest.addEcheckCredit(echeckcredit2);

        //    echeckRedeposit echeckredeposit = new echeckRedeposit();
        //    echeckredeposit.cnpTxnId = 123456;
        //    echeckredeposit.echeck = echeck;
        //    echeckredeposit.id = "id";

        //    cnpBatchRequest.addEcheckRedeposit(echeckredeposit);

        //    echeckRedeposit echeckredeposit2 = new echeckRedeposit();
        //    echeckredeposit2.cnpTxnId = 123457;
        //    echeckredeposit2.echeck = echeck2;
        //    echeckredeposit2.id = "id";

        //    cnpBatchRequest.addEcheckRedeposit(echeckredeposit2);

        //    echeckSale echeckSaleObj = new echeckSale();
        //    echeckSaleObj.amount = 123456;
        //    echeckSaleObj.orderId = "12345";
        //    echeckSaleObj.orderSource = orderSourceType.ecommerce;
        //    echeckSaleObj.echeck = echeck;
        //    echeckSaleObj.billToAddress = billToAddress;
        //    echeckSaleObj.id = "id";

        //    cnpBatchRequest.addEcheckSale(echeckSaleObj);

        //    echeckSale echeckSaleObj2 = new echeckSale();
        //    echeckSaleObj2.amount = 123456;
        //    echeckSaleObj2.orderId = "12346";
        //    echeckSaleObj2.orderSource = orderSourceType.ecommerce;
        //    echeckSaleObj2.echeck = echeck2;
        //    echeckSaleObj2.billToAddress = billToAddress2;
        //    echeckSaleObj2.id = "id";

        //    cnpBatchRequest.addEcheckSale(echeckSaleObj2);

        //    echeckPreNoteSale echeckPreNoteSaleObj1 = new echeckPreNoteSale();
        //    echeckPreNoteSaleObj1.orderId = "12345";
        //    echeckPreNoteSaleObj1.orderSource = orderSourceType.ecommerce;
        //    echeckPreNoteSaleObj1.echeck = echeck;
        //    echeckPreNoteSaleObj1.billToAddress = billToAddress;
        //    echeckPreNoteSaleObj1.id = "id";

        //    cnpBatchRequest.addEcheckPreNoteSale(echeckPreNoteSaleObj1);

        //    echeckPreNoteSale echeckPreNoteSaleObj2 = new echeckPreNoteSale();
        //    echeckPreNoteSaleObj2.orderId = "12345";
        //    echeckPreNoteSaleObj2.orderSource = orderSourceType.ecommerce;
        //    echeckPreNoteSaleObj2.echeck = echeck2;
        //    echeckPreNoteSaleObj2.billToAddress = billToAddress2;
        //    echeckPreNoteSaleObj2.id = "id";

        //    cnpBatchRequest.addEcheckPreNoteSale(echeckPreNoteSaleObj2);

        //    echeckPreNoteCredit echeckPreNoteCreditObj1 = new echeckPreNoteCredit();
        //    echeckPreNoteCreditObj1.orderId = "12345";
        //    echeckPreNoteCreditObj1.orderSource = orderSourceType.ecommerce;
        //    echeckPreNoteCreditObj1.echeck = echeck;
        //    echeckPreNoteCreditObj1.billToAddress = billToAddress;
        //    echeckPreNoteCreditObj1.id = "id";

        //    cnpBatchRequest.addEcheckPreNoteCredit(echeckPreNoteCreditObj1);

        //    echeckPreNoteCredit echeckPreNoteCreditObj2 = new echeckPreNoteCredit();
        //    echeckPreNoteCreditObj2.orderId = "12345";
        //    echeckPreNoteCreditObj2.orderSource = orderSourceType.ecommerce;
        //    echeckPreNoteCreditObj2.echeck = echeck2;
        //    echeckPreNoteCreditObj2.billToAddress = billToAddress2;
        //    echeckPreNoteCreditObj2.id = "id";

        //    cnpBatchRequest.addEcheckPreNoteCredit(echeckPreNoteCreditObj2);

        //    echeckVerification echeckVerificationObject = new echeckVerification();
        //    echeckVerificationObject.amount = 123456;
        //    echeckVerificationObject.orderId = "12345";
        //    echeckVerificationObject.orderSource = orderSourceType.ecommerce;
        //    echeckVerificationObject.echeck = echeck;
        //    echeckVerificationObject.billToAddress = billToAddress;
        //    echeckVerificationObject.id = "id";

        //    cnpBatchRequest.addEcheckVerification(echeckVerificationObject);

        //    echeckVerification echeckVerificationObject2 = new echeckVerification();
        //    echeckVerificationObject2.amount = 123456;
        //    echeckVerificationObject2.orderId = "12346";
        //    echeckVerificationObject2.orderSource = orderSourceType.ecommerce;
        //    echeckVerificationObject2.echeck = echeck2;
        //    echeckVerificationObject2.billToAddress = billToAddress2;
        //    echeckVerificationObject2.id = "id";

        //    cnpBatchRequest.addEcheckVerification(echeckVerificationObject2);

        //    forceCapture forcecapture = new forceCapture();
        //    forcecapture.amount = 106;
        //    forcecapture.orderId = "12344";
        //    forcecapture.orderSource = orderSourceType.ecommerce;
        //    forcecapture.card = card;
        //    forcecapture.id = "id";

        //    cnpBatchRequest.addForceCapture(forcecapture);

        //    forceCapture forcecapture2 = new forceCapture();
        //    forcecapture2.amount = 106;
        //    forcecapture2.orderId = "12345";
        //    forcecapture2.orderSource = orderSourceType.ecommerce;
        //    forcecapture2.card = card2;
        //    forcecapture2.id = "id";

        //    cnpBatchRequest.addForceCapture(forcecapture2);

        //    sale saleObj = new sale();
        //    saleObj.amount = 106;
        //    saleObj.cnpTxnId = 123456;
        //    saleObj.orderId = "12344";
        //    saleObj.orderSource = orderSourceType.ecommerce;
        //    saleObj.card = card;
        //    saleObj.id = "id";

        //    cnpBatchRequest.addSale(saleObj);

        //    sale saleObj2 = new sale();
        //    saleObj2.amount = 106;
        //    saleObj2.cnpTxnId = 123456;
        //    saleObj2.orderId = "12345";
        //    saleObj2.orderSource = orderSourceType.ecommerce;
        //    saleObj2.card = card2;
        //    saleObj2.id = "id";

        //    cnpBatchRequest.addSale(saleObj2);

        //    registerTokenRequestType registerTokenRequest = new registerTokenRequestType();
        //    registerTokenRequest.orderId = "12344";
        //    registerTokenRequest.accountNumber = "1233456789103801";
        //    registerTokenRequest.reportGroup = "Planets";
        //    registerTokenRequest.id = "id";

        //    cnpBatchRequest.addRegisterTokenRequest(registerTokenRequest);

        //    registerTokenRequestType registerTokenRequest2 = new registerTokenRequestType();
        //    registerTokenRequest2.orderId = "12345";
        //    registerTokenRequest2.accountNumber = "1233456789103801";
        //    registerTokenRequest2.reportGroup = "Planets";
        //    registerTokenRequest2.id = "id";

        //    cnpBatchRequest.addRegisterTokenRequest(registerTokenRequest2);

        //    updateCardValidationNumOnToken updateCardValidationNumOnToken = new updateCardValidationNumOnToken();
        //    updateCardValidationNumOnToken.orderId = "12344";
        //    updateCardValidationNumOnToken.cardValidationNum = "123";
        //    updateCardValidationNumOnToken.cnpToken = "4100000000000001";
        //    updateCardValidationNumOnToken.id = "id";

        //    cnpBatchRequest.addUpdateCardValidationNumOnToken(updateCardValidationNumOnToken);

        //    updateCardValidationNumOnToken updateCardValidationNumOnToken2 = new updateCardValidationNumOnToken();
        //    updateCardValidationNumOnToken2.orderId = "12345";
        //    updateCardValidationNumOnToken2.cardValidationNum = "123";
        //    updateCardValidationNumOnToken2.cnpToken = "4242424242424242";
        //    updateCardValidationNumOnToken2.id = "id";

        //    cnpBatchRequest.addUpdateCardValidationNumOnToken(updateCardValidationNumOnToken2);
        //    cnp.addBatch(cnpBatchRequest);

        //    cnpResponse cnpResponse = cnp.sendToCnpWithStream();

        //    Assert.NotNull(cnpResponse);
        //    Assert.AreEqual("0", cnpResponse.response);
        //    Assert.AreEqual("Valid Format", cnpResponse.message);

        //    batchResponse cnpBatchResponse = cnpResponse.nextBatchResponse();
        //    while (cnpBatchResponse != null)
        //    {
        //        authorizationResponse authorizationResponse = cnpBatchResponse.nextAuthorizationResponse();
        //        while (authorizationResponse != null)
        //        {
        //            Assert.AreEqual("000", authorizationResponse.response);

        //            authorizationResponse = cnpBatchResponse.nextAuthorizationResponse();
        //        }

        //        authReversalResponse authReversalResponse = cnpBatchResponse.nextAuthReversalResponse();
        //        while (authReversalResponse != null)
        //        {
        //            Assert.AreEqual("000", authReversalResponse.response);

        //            authReversalResponse = cnpBatchResponse.nextAuthReversalResponse();
        //        }

        //        captureResponse captureResponse = cnpBatchResponse.nextCaptureResponse();
        //        while (captureResponse != null)
        //        {
        //            Assert.AreEqual("000", captureResponse.response);

        //            captureResponse = cnpBatchResponse.nextCaptureResponse();
        //        }

        //        captureGivenAuthResponse captureGivenAuthResponse = cnpBatchResponse.nextCaptureGivenAuthResponse();
        //        while (captureGivenAuthResponse != null)
        //        {
        //            Assert.AreEqual("000", captureGivenAuthResponse.response);

        //            captureGivenAuthResponse = cnpBatchResponse.nextCaptureGivenAuthResponse();
        //        }

        //        creditResponse creditResponse = cnpBatchResponse.nextCreditResponse();
        //        while (creditResponse != null)
        //        {
        //            Assert.AreEqual("000", creditResponse.response);

        //            creditResponse = cnpBatchResponse.nextCreditResponse();
        //        }

        //        echeckCreditResponse echeckCreditResponse = cnpBatchResponse.nextEcheckCreditResponse();
        //        while (echeckCreditResponse != null)
        //        {
        //            Assert.AreEqual("000", echeckCreditResponse.response);

        //            echeckCreditResponse = cnpBatchResponse.nextEcheckCreditResponse();
        //        }

        //        echeckRedepositResponse echeckRedepositResponse = cnpBatchResponse.nextEcheckRedepositResponse();
        //        while (echeckRedepositResponse != null)
        //        {
        //            Assert.AreEqual("000", echeckRedepositResponse.response);

        //            echeckRedepositResponse = cnpBatchResponse.nextEcheckRedepositResponse();
        //        }

        //        echeckSalesResponse echeckSalesResponse = cnpBatchResponse.nextEcheckSalesResponse();
        //        while (echeckSalesResponse != null)
        //        {
        //            Assert.AreEqual("000", echeckSalesResponse.response);

        //            echeckSalesResponse = cnpBatchResponse.nextEcheckSalesResponse();
        //        }

        //        echeckPreNoteSaleResponse echeckPreNoteSaleResponse = cnpBatchResponse.nextEcheckPreNoteSaleResponse();
        //        while (echeckPreNoteSaleResponse != null)
        //        {
        //            Assert.AreEqual("000", echeckPreNoteSaleResponse.response);

        //            echeckPreNoteSaleResponse = cnpBatchResponse.nextEcheckPreNoteSaleResponse();
        //        }

        //        echeckPreNoteCreditResponse echeckPreNoteCreditResponse = cnpBatchResponse.nextEcheckPreNoteCreditResponse();
        //        while (echeckPreNoteCreditResponse != null)
        //        {
        //            Assert.AreEqual("000", echeckPreNoteCreditResponse.response);

        //            echeckPreNoteCreditResponse = cnpBatchResponse.nextEcheckPreNoteCreditResponse();
        //        }

        //        echeckVerificationResponse echeckVerificationResponse = cnpBatchResponse.nextEcheckVerificationResponse();
        //        while (echeckVerificationResponse != null)
        //        {
        //            Assert.AreEqual("957", echeckVerificationResponse.response);

        //            echeckVerificationResponse = cnpBatchResponse.nextEcheckVerificationResponse();
        //        }

        //        forceCaptureResponse forceCaptureResponse = cnpBatchResponse.nextForceCaptureResponse();
        //        while (forceCaptureResponse != null)
        //        {
        //            Assert.AreEqual("000", forceCaptureResponse.response);

        //            forceCaptureResponse = cnpBatchResponse.nextForceCaptureResponse();
        //        }

        //        registerTokenResponse registerTokenResponse = cnpBatchResponse.nextRegisterTokenResponse();
        //        while (registerTokenResponse != null)
        //        {
        //            Assert.AreEqual("820", registerTokenResponse.response);

        //            registerTokenResponse = cnpBatchResponse.nextRegisterTokenResponse();
        //        }

        //        saleResponse saleResponse = cnpBatchResponse.nextSaleResponse();
        //        while (saleResponse != null)
        //        {
        //            Assert.AreEqual("000", saleResponse.response);

        //            saleResponse = cnpBatchResponse.nextSaleResponse();
        //        }

        //        updateCardValidationNumOnTokenResponse updateCardValidationNumOnTokenResponse = cnpBatchResponse.nextUpdateCardValidationNumOnTokenResponse();
        //        while (updateCardValidationNumOnTokenResponse != null)
        //        {
        //            Assert.AreEqual("823", updateCardValidationNumOnTokenResponse.response);

        //            updateCardValidationNumOnTokenResponse = cnpBatchResponse.nextUpdateCardValidationNumOnTokenResponse();
        //        }

        //        cnpBatchResponse = cnpResponse.nextBatchResponse();
        //    }
        //}

        //[Fact]
        //public void accountUpdateBatch()
        //{
        //    batchRequest cnpBatchRequest = new batchRequest();

        //    accountUpdate accountUpdate1 = new accountUpdate();
        //    accountUpdate1.orderId = "1111";
        //    cardType card = new cardType();
        //    card.type = methodOfPaymentTypeEnum.VI;
        //    card.number = "414100000000000000";
        //    card.expDate = "1210";
        //    accountUpdate1.card = card;
        //    accountUpdate1.id = "id";

        //    cnpBatchRequest.addAccountUpdate(accountUpdate1);

        //    accountUpdate accountUpdate2 = new accountUpdate();
        //    accountUpdate2.orderId = "1112";
        //    accountUpdate2.card = card;
        //    accountUpdate2.id = "id";

        //    cnpBatchRequest.addAccountUpdate(accountUpdate2);

        //    cnp.addBatch(cnpBatchRequest);
        //    cnpResponse cnpResponse = cnp.sendToCnpWithStream();

        //    Assert.NotNull(cnpResponse);
        //    Assert.AreEqual("0", cnpResponse.response);
        //    Assert.AreEqual("Valid Format", cnpResponse.message);

        //    batchResponse cnpBatchResponse = cnpResponse.nextBatchResponse();
        //    while (cnpBatchResponse != null)
        //    {
        //        accountUpdateResponse accountUpdateResponse = cnpBatchResponse.nextAccountUpdateResponse();
        //        Assert.NotNull(accountUpdateResponse);
        //        while (accountUpdateResponse != null)
        //        {
        //            Assert.AreEqual("301", accountUpdateResponse.response);

        //            accountUpdateResponse = cnpBatchResponse.nextAccountUpdateResponse();
        //        }
        //        cnpBatchResponse = cnpResponse.nextBatchResponse();
        //    }
        //}

        //[Fact]
        //public void RFRBatch()
        //{
        //    batchRequest cnpBatchRequest = new batchRequest();
        //    cnpBatchRequest.id = "1234567A";

        //    accountUpdate accountUpdate1 = new accountUpdate();
        //    accountUpdate1.orderId = "1111";
        //    cardType card = new cardType();
        //    card.type = methodOfPaymentTypeEnum.VI;
        //    card.number = "4242424242424242";
        //    card.expDate = "1210";
        //    accountUpdate1.card = card;
        //    accountUpdate1.id = "id";

        //    cnpBatchRequest.addAccountUpdate(accountUpdate1);

        //    accountUpdate accountUpdate2 = new accountUpdate();
        //    accountUpdate2.orderId = "1112";
        //    accountUpdate2.card = card;
        //    accountUpdate2.id = "id";

        //    cnpBatchRequest.addAccountUpdate(accountUpdate2);

        //    cnp.addBatch(cnpBatchRequest);
        //    cnpResponse cnpResponse = cnp.sendToCnpWithStream();

        //    Assert.NotNull(cnpResponse);

        //    batchResponse cnpBatchResponse = cnpResponse.nextBatchResponse();
        //    Assert.NotNull(cnpBatchResponse);
        //    while (cnpBatchResponse != null)
        //    {
        //        accountUpdateResponse accountUpdateResponse = cnpBatchResponse.nextAccountUpdateResponse();
        //        Assert.NotNull(accountUpdateResponse);
        //        while (accountUpdateResponse != null)
        //        {
        //            Assert.AreEqual("000", accountUpdateResponse.response);

        //            accountUpdateResponse = cnpBatchResponse.nextAccountUpdateResponse();
        //        }
        //        cnpBatchResponse = cnpResponse.nextBatchResponse();
        //    }

        //    cnpRequest cnpRfr = new cnpRequest();
        //    RFRRequest rfrRequest = new RFRRequest();
        //    accountUpdateFileRequestData accountUpdateFileRequestData = new accountUpdateFileRequestData();
        //    accountUpdateFileRequestData.merchantId = Properties.Settings.Default.merchantId;
        //    accountUpdateFileRequestData.postDay = DateTime.Now;
        //    rfrRequest.accountUpdateFileRequestData = accountUpdateFileRequestData;

        //    cnpRfr.addRFRRequest(rfrRequest);            

        //    try
        //    {
        //        cnpResponse cnpRfrResponse = cnpRfr.sendToCnpWithStream();
        //        Assert.NotNull(cnpRfrResponse);

        //        RFRResponse rfrResponse = cnpRfrResponse.nextRFRResponse();
        //        Assert.NotNull(rfrResponse);
        //        while (rfrResponse != null)
        //        {
        //            Assert.AreEqual("1", rfrResponse.response);
        //            Assert.AreEqual("The account update file is not ready yet.  Please try again later.", rfrResponse.message);
        //            rfrResponse = cnpResponse.nextRFRResponse();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}

        //[Fact]
        //public void nullBatchData()
        //{
        //    batchRequest cnpBatchRequest = new batchRequest();

        //    authorization authorization = new authorization();
        //    authorization.reportGroup = "Planets";
        //    authorization.orderId = "12344";
        //    authorization.amount = 106;
        //    authorization.orderSource = orderSourceType.ecommerce;
        //    cardType card = new cardType();
        //    card.type = methodOfPaymentTypeEnum.VI;
        //    card.number = "414100000000000000";
        //    card.expDate = "1210";
        //    authorization.card = card; //This needs to compile      
        //    authorization.id = "id";

        //    cnpBatchRequest.addAuthorization(authorization);
        //    try
        //    {
        //        cnpBatchRequest.addAuthorization(null);
        //    }
        //    catch (System.NullReferenceException e)
        //    {
        //        Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
        //    }

        //    authReversal reversal = new authReversal();
        //    reversal.cnpTxnId = 12345678000L;
        //    reversal.amount = 106;
        //    reversal.payPalNotes = "Notes";
        //    reversal.id = "id";

        //    cnpBatchRequest.addAuthReversal(reversal);
        //    try
        //    {
        //        cnpBatchRequest.addAuthReversal(null);
        //    }
        //    catch (System.NullReferenceException e)
        //    {
        //        Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
        //    }

        //    capture capture = new capture();
        //    capture.cnpTxnId = 123456000;
        //    capture.amount = 106;
        //    capture.payPalNotes = "Notes";
        //    capture.id = "id";

        //    cnpBatchRequest.addCapture(capture);
        //    try
        //    {
        //        cnpBatchRequest.addCapture(null);
        //    }
        //    catch (System.NullReferenceException e)
        //    {
        //        Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
        //    }

        //    captureGivenAuth capturegivenauth = new captureGivenAuth();
        //    capturegivenauth.amount = 106;
        //    capturegivenauth.orderId = "12344";
        //    authInformation authInfo = new authInformation();
        //    DateTime authDate = new DateTime(2002, 10, 9);
        //    authInfo.authDate = authDate;
        //    authInfo.authCode = "543216";
        //    authInfo.authAmount = 12345;
        //    capturegivenauth.authInformation = authInfo;
        //    capturegivenauth.orderSource = orderSourceType.ecommerce;
        //    capturegivenauth.card = card;
        //    capturegivenauth.id = "id";

        //    cnpBatchRequest.addCaptureGivenAuth(capturegivenauth);
        //    try
        //    {
        //        cnpBatchRequest.addCaptureGivenAuth(null);
        //    }
        //    catch (System.NullReferenceException e)
        //    {
        //        Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
        //    }

        //    credit creditObj = new credit();
        //    creditObj.amount = 106;
        //    creditObj.orderId = "2111";
        //    creditObj.orderSource = orderSourceType.ecommerce;
        //    creditObj.card = card;
        //    creditObj.id = "id";

        //    cnpBatchRequest.addCredit(creditObj);
        //    try
        //    {
        //        cnpBatchRequest.addCredit(null);
        //    }
        //    catch (System.NullReferenceException e)
        //    {
        //        Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
        //    }

        //    echeckCredit echeckcredit = new echeckCredit();
        //    echeckcredit.amount = 12L;
        //    echeckcredit.orderId = "12345";
        //    echeckcredit.orderSource = orderSourceType.ecommerce;
        //    echeckType echeck = new echeckType();
        //    echeck.accType = echeckAccountTypeEnum.Checking;
        //    echeck.accNum = "12345657890";
        //    echeck.routingNum = "011201995";
        //    echeck.checkNum = "123455";
        //    echeckcredit.echeck = echeck;
        //    contact billToAddress = new contact();
        //    billToAddress.name = "Bob";
        //    billToAddress.city = "Lowell";
        //    billToAddress.state = "MA";
        //    billToAddress.email = "cnp.com";
        //    echeckcredit.billToAddress = billToAddress;
        //    echeckcredit.id = "id";

        //    cnpBatchRequest.addEcheckCredit(echeckcredit);
        //    try
        //    {
        //        cnpBatchRequest.addEcheckCredit(null);
        //    }
        //    catch (System.NullReferenceException e)
        //    {
        //        Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
        //    }

        //    echeckRedeposit echeckredeposit = new echeckRedeposit();
        //    echeckredeposit.cnpTxnId = 123456;
        //    echeckredeposit.echeck = echeck;
        //    echeckredeposit.id = "id";

        //    cnpBatchRequest.addEcheckRedeposit(echeckredeposit);
        //    try
        //    {
        //        cnpBatchRequest.addEcheckRedeposit(null);
        //    }
        //    catch (System.NullReferenceException e)
        //    {
        //        Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
        //    }

        //    echeckSale echeckSaleObj = new echeckSale();
        //    echeckSaleObj.amount = 123456;
        //    echeckSaleObj.orderId = "12345";
        //    echeckSaleObj.orderSource = orderSourceType.ecommerce;
        //    echeckSaleObj.echeck = echeck;
        //    echeckSaleObj.billToAddress = billToAddress;
        //    echeckSaleObj.id = "id";

        //    cnpBatchRequest.addEcheckSale(echeckSaleObj);
        //    try
        //    {
        //        cnpBatchRequest.addEcheckSale(null);
        //    }
        //    catch (System.NullReferenceException e)
        //    {
        //        Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
        //    }

        //    echeckVerification echeckVerificationObject = new echeckVerification();
        //    echeckVerificationObject.amount = 123456;
        //    echeckVerificationObject.orderId = "12345";
        //    echeckVerificationObject.orderSource = orderSourceType.ecommerce;
        //    echeckVerificationObject.echeck = echeck;
        //    echeckVerificationObject.billToAddress = billToAddress;
        //    echeckVerificationObject.id = "id";

        //    cnpBatchRequest.addEcheckVerification(echeckVerificationObject);
        //    try
        //    {
        //        cnpBatchRequest.addEcheckVerification(null);
        //    }
        //    catch (System.NullReferenceException e)
        //    {
        //        Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
        //    }

        //    forceCapture forcecapture = new forceCapture();
        //    forcecapture.amount = 106;
        //    forcecapture.orderId = "12344";
        //    forcecapture.orderSource = orderSourceType.ecommerce;
        //    forcecapture.card = card;
        //    forcecapture.id = "id";

        //    cnpBatchRequest.addForceCapture(forcecapture);
        //    try
        //    {
        //        cnpBatchRequest.addForceCapture(null);
        //    }
        //    catch (System.NullReferenceException e)
        //    {
        //        Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
        //    }

        //    sale saleObj = new sale();
        //    saleObj.amount = 106;
        //    saleObj.cnpTxnId = 123456;
        //    saleObj.orderId = "12344";
        //    saleObj.orderSource = orderSourceType.ecommerce;
        //    saleObj.card = card;
        //    saleObj.id = "id";

        //    cnpBatchRequest.addSale(saleObj);
        //    try
        //    {
        //        cnpBatchRequest.addSale(null);
        //    }
        //    catch (System.NullReferenceException e)
        //    {
        //        Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
        //    }

        //    registerTokenRequestType registerTokenRequest = new registerTokenRequestType();
        //    registerTokenRequest.orderId = "12344";
        //    registerTokenRequest.accountNumber = "1233456789103801";
        //    registerTokenRequest.reportGroup = "Planets";
        //    registerTokenRequest.id = "id";

        //    cnpBatchRequest.addRegisterTokenRequest(registerTokenRequest);
        //    try
        //    {
        //        cnpBatchRequest.addRegisterTokenRequest(null);
        //    }
        //    catch (System.NullReferenceException e)
        //    {
        //        Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
        //    }

        //    try
        //    {
        //        cnp.addBatch(cnpBatchRequest);
        //    }
        //    catch (System.NullReferenceException e)
        //    {
        //        Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
        //    }
        //}

        //[Fact]
        //public void InvalidCredientialsBatch()
        //{
        //    batchRequest cnpBatchRequest = new batchRequest();

        //    authorization authorization = new authorization();
        //    authorization.reportGroup = "Planets";
        //    authorization.orderId = "12344";
        //    authorization.amount = 106;
        //    authorization.orderSource = orderSourceType.ecommerce;
        //    cardType card = new cardType();
        //    card.type = methodOfPaymentTypeEnum.VI;
        //    card.number = "4100000000000001";
        //    card.expDate = "1210";
        //    authorization.card = card;
        //    authorization.id = "id";

        //    cnpBatchRequest.addAuthorization(authorization);

        //    authorization authorization2 = new authorization();
        //    authorization2.reportGroup = "Planets";
        //    authorization2.orderId = "12345";
        //    authorization2.amount = 106;
        //    authorization2.orderSource = orderSourceType.ecommerce;
        //    cardType card2 = new cardType();
        //    card2.type = methodOfPaymentTypeEnum.VI;
        //    card2.number = "4242424242424242";
        //    card2.expDate = "1210";
        //    authorization2.card = card2;
        //    authorization2.id = "id";

        //    cnpBatchRequest.addAuthorization(authorization2);

        //    authReversal reversal = new authReversal();
        //    reversal.cnpTxnId = 12345678000L;
        //    reversal.amount = 106;
        //    reversal.payPalNotes = "Notes";
        //    reversal.id = "id";

        //    cnpBatchRequest.addAuthReversal(reversal);

        //    authReversal reversal2 = new authReversal();
        //    reversal2.cnpTxnId = 12345678900L;
        //    reversal2.amount = 106;
        //    reversal2.payPalNotes = "Notes";
        //    reversal2.id = "id";

        //    cnpBatchRequest.addAuthReversal(reversal2);

        //    capture capture = new capture();
        //    capture.cnpTxnId = 123456000;
        //    capture.amount = 106;
        //    capture.payPalNotes = "Notes";
        //    capture.id = "id";

        //    cnpBatchRequest.addCapture(capture);

        //    capture capture2 = new capture();
        //    capture2.cnpTxnId = 123456700;
        //    capture2.amount = 106;
        //    capture2.payPalNotes = "Notes";
        //    capture2.id = "id";

        //    cnpBatchRequest.addCapture(capture2);

        //    captureGivenAuth capturegivenauth = new captureGivenAuth();
        //    capturegivenauth.amount = 106;
        //    capturegivenauth.orderId = "12344";
        //    authInformation authInfo = new authInformation();
        //    DateTime authDate = new DateTime(2002, 10, 9);
        //    authInfo.authDate = authDate;
        //    authInfo.authCode = "543216";
        //    authInfo.authAmount = 12345;
        //    capturegivenauth.authInformation = authInfo;
        //    capturegivenauth.orderSource = orderSourceType.ecommerce;
        //    capturegivenauth.card = card;
        //    capturegivenauth.id = "id";

        //    cnpBatchRequest.addCaptureGivenAuth(capturegivenauth);

        //    captureGivenAuth capturegivenauth2 = new captureGivenAuth();
        //    capturegivenauth2.amount = 106;
        //    capturegivenauth2.orderId = "12344";
        //    authInformation authInfo2 = new authInformation();
        //    authDate = new DateTime(2003, 10, 9);
        //    authInfo2.authDate = authDate;
        //    authInfo2.authCode = "543216";
        //    authInfo2.authAmount = 12345;
        //    capturegivenauth2.authInformation = authInfo;
        //    capturegivenauth2.orderSource = orderSourceType.ecommerce;
        //    capturegivenauth2.card = card2;
        //    capturegivenauth2.id = "id";


        //    cnpBatchRequest.addCaptureGivenAuth(capturegivenauth2);

        //    credit creditObj = new credit();
        //    creditObj.amount = 106;
        //    creditObj.orderId = "2111";
        //    creditObj.orderSource = orderSourceType.ecommerce;
        //    creditObj.card = card;
        //    creditObj.id = "id";

        //    cnpBatchRequest.addCredit(creditObj);

        //    credit creditObj2 = new credit();
        //    creditObj2.amount = 106;
        //    creditObj2.orderId = "2111";
        //    creditObj2.orderSource = orderSourceType.ecommerce;
        //    creditObj2.card = card2;
        //    creditObj2.id = "id";

        //    cnpBatchRequest.addCredit(creditObj2);

        //    echeckCredit echeckcredit = new echeckCredit();
        //    echeckcredit.amount = 12L;
        //    echeckcredit.orderId = "12345";
        //    echeckcredit.orderSource = orderSourceType.ecommerce;
        //    echeckType echeck = new echeckType();
        //    echeck.accType = echeckAccountTypeEnum.Checking;
        //    echeck.accNum = "1099999903";
        //    echeck.routingNum = "011201995";
        //    echeck.checkNum = "123455";
        //    echeckcredit.echeck = echeck;
        //    contact billToAddress = new contact();
        //    billToAddress.name = "Bob";
        //    billToAddress.city = "Lowell";
        //    billToAddress.state = "MA";
        //    billToAddress.email = "cnp.com";
        //    echeckcredit.billToAddress = billToAddress;
        //    echeckcredit.id = "id";

        //    cnpBatchRequest.addEcheckCredit(echeckcredit);

        //    echeckCredit echeckcredit2 = new echeckCredit();
        //    echeckcredit2.amount = 12L;
        //    echeckcredit2.orderId = "12346";
        //    echeckcredit2.orderSource = orderSourceType.ecommerce;
        //    echeckType echeck2 = new echeckType();
        //    echeck2.accType = echeckAccountTypeEnum.Checking;
        //    echeck2.accNum = "1099999903";
        //    echeck2.routingNum = "011201995";
        //    echeck2.checkNum = "123456";
        //    echeckcredit2.echeck = echeck2;
        //    contact billToAddress2 = new contact();
        //    billToAddress2.name = "Mike";
        //    billToAddress2.city = "Lowell";
        //    billToAddress2.state = "MA";
        //    billToAddress2.email = "cnp.com";
        //    echeckcredit2.billToAddress = billToAddress2;
        //    echeckcredit2.id = "id";

        //    cnpBatchRequest.addEcheckCredit(echeckcredit2);

        //    echeckRedeposit echeckredeposit = new echeckRedeposit();
        //    echeckredeposit.cnpTxnId = 123456;
        //    echeckredeposit.echeck = echeck;
        //    echeckredeposit.id = "id";

        //    cnpBatchRequest.addEcheckRedeposit(echeckredeposit);

        //    echeckRedeposit echeckredeposit2 = new echeckRedeposit();
        //    echeckredeposit2.cnpTxnId = 123457;
        //    echeckredeposit2.echeck = echeck2;
        //    echeckredeposit2.id = "id";

        //    cnpBatchRequest.addEcheckRedeposit(echeckredeposit2);

        //    echeckSale echeckSaleObj = new echeckSale();
        //    echeckSaleObj.amount = 123456;
        //    echeckSaleObj.orderId = "12345";
        //    echeckSaleObj.orderSource = orderSourceType.ecommerce;
        //    echeckSaleObj.echeck = echeck;
        //    echeckSaleObj.billToAddress = billToAddress;
        //    echeckSaleObj.id = "id";

        //    cnpBatchRequest.addEcheckSale(echeckSaleObj);

        //    echeckSale echeckSaleObj2 = new echeckSale();
        //    echeckSaleObj2.amount = 123456;
        //    echeckSaleObj2.orderId = "12346";
        //    echeckSaleObj2.orderSource = orderSourceType.ecommerce;
        //    echeckSaleObj2.echeck = echeck2;
        //    echeckSaleObj2.billToAddress = billToAddress2;
        //    echeckSaleObj2.id = "id";

        //    cnpBatchRequest.addEcheckSale(echeckSaleObj2);

        //    echeckVerification echeckVerificationObject = new echeckVerification();
        //    echeckVerificationObject.amount = 123456;
        //    echeckVerificationObject.orderId = "12345";
        //    echeckVerificationObject.orderSource = orderSourceType.ecommerce;
        //    echeckVerificationObject.echeck = echeck;
        //    echeckVerificationObject.billToAddress = billToAddress;
        //    echeckVerificationObject.id = "id";

        //    cnpBatchRequest.addEcheckVerification(echeckVerificationObject);

        //    echeckVerification echeckVerificationObject2 = new echeckVerification();
        //    echeckVerificationObject2.amount = 123456;
        //    echeckVerificationObject2.orderId = "12346";
        //    echeckVerificationObject2.orderSource = orderSourceType.ecommerce;
        //    echeckVerificationObject2.echeck = echeck2;
        //    echeckVerificationObject2.billToAddress = billToAddress2;
        //    echeckVerificationObject2.id = "id";

        //    cnpBatchRequest.addEcheckVerification(echeckVerificationObject2);

        //    forceCapture forcecapture = new forceCapture();
        //    forcecapture.amount = 106;
        //    forcecapture.orderId = "12344";
        //    forcecapture.orderSource = orderSourceType.ecommerce;
        //    forcecapture.card = card;
        //    forcecapture.id = "id";

        //    cnpBatchRequest.addForceCapture(forcecapture);

        //    forceCapture forcecapture2 = new forceCapture();
        //    forcecapture2.amount = 106;
        //    forcecapture2.orderId = "12345";
        //    forcecapture2.orderSource = orderSourceType.ecommerce;
        //    forcecapture2.card = card2;
        //    forcecapture2.id = "id";

        //    cnpBatchRequest.addForceCapture(forcecapture2);

        //    sale saleObj = new sale();
        //    saleObj.amount = 106;
        //    saleObj.cnpTxnId = 123456;
        //    saleObj.orderId = "12344";
        //    saleObj.orderSource = orderSourceType.ecommerce;
        //    saleObj.card = card;
        //    saleObj.id = "id";

        //    cnpBatchRequest.addSale(saleObj);

        //    sale saleObj2 = new sale();
        //    saleObj2.amount = 106;
        //    saleObj2.cnpTxnId = 123456;
        //    saleObj2.orderId = "12345";
        //    saleObj2.orderSource = orderSourceType.ecommerce;
        //    saleObj2.card = card2;
        //    saleObj2.id = "id";

        //    cnpBatchRequest.addSale(saleObj2);

        //    registerTokenRequestType registerTokenRequest = new registerTokenRequestType();
        //    registerTokenRequest.orderId = "12344";
        //    registerTokenRequest.accountNumber = "1233456789103801";
        //    registerTokenRequest.reportGroup = "Planets";
        //    registerTokenRequest.id = "id";

        //    cnpBatchRequest.addRegisterTokenRequest(registerTokenRequest);

        //    registerTokenRequestType registerTokenRequest2 = new registerTokenRequestType();
        //    registerTokenRequest2.orderId = "12345";
        //    registerTokenRequest2.accountNumber = "1233456789103801";
        //    registerTokenRequest2.reportGroup = "Planets";
        //    registerTokenRequest.id = "id";

        //    cnpBatchRequest.addRegisterTokenRequest(registerTokenRequest2);

        //    cnp.addBatch(cnpBatchRequest);

        //    try
        //    {
        //        cnpResponse cnpResponse = cnp.sendToCnpWithStream();
        //    }
        //    catch (CnpOnlineException e)
        //    {
        //        Assert.AreEqual("Error establishing a network connection", e.Message);
        //    }
        //}

        //[Fact]
        //public void EcheckPreNoteTestAll()
        //{
        //    batchRequest cnpBatchRequest = new batchRequest();

        //    contact billToAddress = new contact();
        //    billToAddress.name = "Mike";
        //    billToAddress.city = "Lowell";
        //    billToAddress.state = "MA";
        //    billToAddress.email = "cnp.com";

        //    echeckType echeckSuccess = new echeckType();
        //    echeckSuccess.accType = echeckAccountTypeEnum.Corporate;
        //    echeckSuccess.accNum = "1092969901";
        //    echeckSuccess.routingNum = "011075150";
        //    echeckSuccess.checkNum = "123456";
    

        //    echeckType echeckRoutErr = new echeckType();
        //    echeckRoutErr.accType = echeckAccountTypeEnum.Checking;
        //    echeckRoutErr.accNum = "6099999992";
        //    echeckRoutErr.routingNum = "053133052";
        //    echeckRoutErr.checkNum = "123457";
   

        //    echeckType echeckAccErr = new echeckType();
        //    echeckAccErr.accType = echeckAccountTypeEnum.Corporate;
        //    echeckAccErr.accNum = "10@2969901";
        //    echeckAccErr.routingNum = "011100012";
        //    echeckAccErr.checkNum = "123458";
        

        //    echeckPreNoteSale echeckPreNoteSaleSuccess = new echeckPreNoteSale();
        //    echeckPreNoteSaleSuccess.orderId = "000";
        //    echeckPreNoteSaleSuccess.orderSource = orderSourceType.ecommerce;
        //    echeckPreNoteSaleSuccess.echeck = echeckSuccess;
        //    echeckPreNoteSaleSuccess.billToAddress = billToAddress;
        //    echeckPreNoteSaleSuccess.id = "id";
        //    cnpBatchRequest.addEcheckPreNoteSale(echeckPreNoteSaleSuccess);

        //    echeckPreNoteSale echeckPreNoteSaleRoutErr = new echeckPreNoteSale();
        //    echeckPreNoteSaleRoutErr.orderId = "900";
        //    echeckPreNoteSaleRoutErr.orderSource = orderSourceType.ecommerce;
        //    echeckPreNoteSaleRoutErr.echeck = echeckRoutErr;
        //    echeckPreNoteSaleRoutErr.billToAddress = billToAddress;
        //    echeckPreNoteSaleRoutErr.id = "id";
        //    cnpBatchRequest.addEcheckPreNoteSale(echeckPreNoteSaleRoutErr);

        //    echeckPreNoteSale echeckPreNoteSaleAccErr = new echeckPreNoteSale();
        //    echeckPreNoteSaleAccErr.orderId = "301";
        //    echeckPreNoteSaleAccErr.orderSource = orderSourceType.ecommerce;
        //    echeckPreNoteSaleAccErr.echeck = echeckAccErr;
        //    echeckPreNoteSaleAccErr.billToAddress = billToAddress;
        //    echeckPreNoteSaleAccErr.id = "id";
        //    cnpBatchRequest.addEcheckPreNoteSale(echeckPreNoteSaleAccErr);

        //    echeckPreNoteCredit echeckPreNoteCreditSuccess = new echeckPreNoteCredit();
        //    echeckPreNoteCreditSuccess.orderId = "000";
        //    echeckPreNoteCreditSuccess.orderSource = orderSourceType.ecommerce;
        //    echeckPreNoteCreditSuccess.echeck = echeckSuccess;
        //    echeckPreNoteCreditSuccess.billToAddress = billToAddress;
        //    echeckPreNoteCreditSuccess.id = "id";
        //    cnpBatchRequest.addEcheckPreNoteCredit(echeckPreNoteCreditSuccess);

        //    echeckPreNoteCredit echeckPreNoteCreditRoutErr = new echeckPreNoteCredit();
        //    echeckPreNoteCreditRoutErr.orderId = "900";
        //    echeckPreNoteCreditRoutErr.orderSource = orderSourceType.ecommerce;
        //    echeckPreNoteCreditRoutErr.echeck = echeckRoutErr;
        //    echeckPreNoteCreditRoutErr.billToAddress = billToAddress;
        //    echeckPreNoteCreditRoutErr.id = "id";
        //    cnpBatchRequest.addEcheckPreNoteCredit(echeckPreNoteCreditRoutErr);

        //    echeckPreNoteCredit echeckPreNoteCreditAccErr = new echeckPreNoteCredit();
        //    echeckPreNoteCreditAccErr.orderId = "301";
        //    echeckPreNoteCreditAccErr.orderSource = orderSourceType.ecommerce;
        //    echeckPreNoteCreditAccErr.echeck = echeckAccErr;
        //    echeckPreNoteCreditAccErr.billToAddress = billToAddress;
        //    echeckPreNoteCreditAccErr.id = "id";
        //    cnpBatchRequest.addEcheckPreNoteCredit(echeckPreNoteCreditAccErr);

        //    cnp.addBatch(cnpBatchRequest);

        //    cnpResponse cnpResponse = cnp.sendToCnpWithStream();

        //    Assert.NotNull(cnpResponse);
        //    Assert.AreEqual("0", cnpResponse.response);
        //    Assert.AreEqual("Valid Format", cnpResponse.message);

        //    batchResponse cnpBatchResponse = cnpResponse.nextBatchResponse();
        //    while (cnpBatchResponse != null)
        //    {
        //        echeckPreNoteSaleResponse echeckPreNoteSaleResponse = cnpBatchResponse.nextEcheckPreNoteSaleResponse();
        //        while (echeckPreNoteSaleResponse != null)
        //        {

        //            echeckPreNoteSaleResponse = cnpBatchResponse.nextEcheckPreNoteSaleResponse();
        //        }

        //        echeckPreNoteCreditResponse echeckPreNoteCreditResponse = cnpBatchResponse.nextEcheckPreNoteCreditResponse();
        //        while (echeckPreNoteCreditResponse != null)
        //        {

        //            echeckPreNoteCreditResponse = cnpBatchResponse.nextEcheckPreNoteCreditResponse();
        //        }

        //        cnpBatchResponse = cnpResponse.nextBatchResponse();
        //    }
        //}

        //[Fact]
        //public void PFIFInstructionTxnTest()
        //{

        //    Dictionary<string, string> configOverride = new Dictionary<string, string>();
        //    configOverride["url"] = Properties.Settings.Default.url;
        //    configOverride["reportGroup"] = Properties.Settings.Default.reportGroup;
        //    configOverride["username"] = Properties.Settings.Default.username;
        //    configOverride["printxml"] = Properties.Settings.Default.printxml;
        //    configOverride["timeout"] = Properties.Settings.Default.timeout;
        //    configOverride["proxyHost"] = Properties.Settings.Default.proxyHost;
        //    configOverride["merchantId"] = Properties.Settings.Default.merchantId;
        //    configOverride["password"] = Properties.Settings.Default.password;
        //    configOverride["proxyPort"] = Properties.Settings.Default.proxyPort;
        //    configOverride["sftpUrl"] = Properties.Settings.Default.sftpUrl;
        //    configOverride["sftpUsername"] = Properties.Settings.Default.sftpUsername;
        //    configOverride["sftpPassword"] = Properties.Settings.Default.sftpPassword;
        //    configOverride["knownHostsFile"] = Properties.Settings.Default.knownHostsFile;
        //    configOverride["onlineBatchUrl"] = Properties.Settings.Default.onlineBatchUrl;
        //    configOverride["onlineBatchPort"] = Properties.Settings.Default.onlineBatchPort;
        //    configOverride["requestDirectory"] = Properties.Settings.Default.requestDirectory;
        //    configOverride["responseDirectory"] = Properties.Settings.Default.responseDirectory;

        //    cnpRequest cnpOverride = new cnpRequest(configOverride);

        //    batchRequest cnpBatchRequest = new batchRequest(configOverride);

        //    echeckType echeck = new echeckType();
        //    echeck.accType = echeckAccountTypeEnum.Checking;
        //    echeck.accNum = "1092969901";
        //    echeck.routingNum = "011075150";
        //    echeck.checkNum = "123455";


        //    var submerchantCredit = new submerchantCredit
        //    {
        //        fundingSubmerchantId = "123456",
        //        submerchantName = "merchant",
        //        fundsTransferId = "123467",
        //        amount = 106L,
        //        accountInfo = echeck,
        //        id = "id",
        //        customIdentifier = "123",
        //    };
        //    cnpBatchRequest.addSubmerchantCredit(submerchantCredit);

        //    payFacCredit payFacCredit = new payFacCredit();
        //    payFacCredit.fundingSubmerchantId = "123456";
        //    payFacCredit.fundsTransferId = "123467";
        //    payFacCredit.amount = 107L;
        //    payFacCredit.id = "id";
        //    cnpBatchRequest.addPayFacCredit(payFacCredit);

        //    reserveCredit reserveCredit = new reserveCredit();
        //    reserveCredit.fundingSubmerchantId = "123456";
        //    reserveCredit.fundsTransferId = "123467";
        //    reserveCredit.amount = 107L;
        //    reserveCredit.id = "id";
        //    cnpBatchRequest.addReserveCredit(reserveCredit);

        //    vendorCredit vendorCredit = new vendorCredit();
        //    vendorCredit.fundingSubmerchantId = "123456";
        //    vendorCredit.vendorName = "merchant";
        //    vendorCredit.fundsTransferId = "123467";
        //    vendorCredit.amount = 106L;
        //    vendorCredit.accountInfo = echeck;
        //    vendorCredit.id = "id";
        //    cnpBatchRequest.addVendorCredit(vendorCredit);

        //    physicalCheckCredit physicalCheckCredit = new physicalCheckCredit();
        //    physicalCheckCredit.fundingSubmerchantId = "123456";
        //    physicalCheckCredit.fundsTransferId = "123467";
        //    physicalCheckCredit.amount = 107L;
        //    physicalCheckCredit.id = "id";
        //    cnpBatchRequest.addPhysicalCheckCredit(physicalCheckCredit);

        //    submerchantDebit submerchantDebit = new submerchantDebit();
        //    submerchantDebit.fundingSubmerchantId = "123456";
        //    submerchantDebit.submerchantName = "merchant";
        //    submerchantDebit.fundsTransferId = "123467";
        //    submerchantDebit.amount = 106L;
        //    submerchantDebit.accountInfo = echeck;
        //    submerchantDebit.id = "id";
        //    submerchantDebit.customIdentifier = "123";
        //    cnpBatchRequest.addSubmerchantDebit(submerchantDebit);

        //    payFacDebit payFacDebit = new payFacDebit();
        //    payFacDebit.fundingSubmerchantId = "123456";
        //    payFacDebit.fundsTransferId = "123467";
        //    payFacDebit.amount = 107L;
        //    payFacDebit.id = "id";
        //    cnpBatchRequest.addPayFacDebit(payFacDebit);

        //    reserveDebit reserveDebit = new reserveDebit();
        //    reserveDebit.fundingSubmerchantId = "123456";
        //    reserveDebit.fundsTransferId = "123467";
        //    reserveDebit.amount = 107L;
        //    reserveDebit.id = "id";
        //    cnpBatchRequest.addReserveDebit(reserveDebit);

        //    vendorDebit vendorDebit = new vendorDebit();
        //    vendorDebit.fundingSubmerchantId = "123456";
        //    vendorDebit.vendorName = "merchant";
        //    vendorDebit.fundsTransferId = "123467";
        //    vendorDebit.amount = 106L;
        //    vendorDebit.accountInfo = echeck;
        //    vendorDebit.id = "id";
        //    cnpBatchRequest.addVendorDebit(vendorDebit);

        //    physicalCheckDebit physicalCheckDebit = new physicalCheckDebit();
        //    physicalCheckDebit.fundingSubmerchantId = "123456";
        //    physicalCheckDebit.fundsTransferId = "123467";
        //    physicalCheckDebit.amount = 107L;
        //    physicalCheckDebit.id = "id";
        //    cnpBatchRequest.addPhysicalCheckDebit(physicalCheckDebit);

        //    cnpOverride.addBatch(cnpBatchRequest);

        //    cnpResponse cnpResponse = cnpOverride.sendToCnpWithStream();

        //    Assert.NotNull(cnpResponse);
        //    Assert.AreEqual("0", cnpResponse.response);
        //    Assert.AreEqual("Valid Format", cnpResponse.message);

        //    batchResponse cnpBatchResponse = cnpResponse.nextBatchResponse();
        //    while (cnpBatchResponse != null)
        //    {
        //        submerchantCreditResponse submerchantCreditResponse = cnpBatchResponse.nextSubmerchantCreditResponse();
        //        while (submerchantCreditResponse != null)
        //        {
        //            Assert.AreEqual("000", submerchantCreditResponse.response);
        //            submerchantCreditResponse = cnpBatchResponse.nextSubmerchantCreditResponse();
        //        }

        //        payFacCreditResponse payFacCreditResponse = cnpBatchResponse.nextPayFacCreditResponse();
        //        while (payFacCreditResponse != null)
        //        {
        //            Assert.AreEqual("000", payFacCreditResponse.response);
        //            payFacCreditResponse = cnpBatchResponse.nextPayFacCreditResponse();
        //        }

        //        vendorCreditResponse vendorCreditResponse = cnpBatchResponse.nextVendorCreditResponse();
        //        while (vendorCreditResponse != null)
        //        {
        //            Assert.AreEqual("000", vendorCreditResponse.response);
        //            vendorCreditResponse = cnpBatchResponse.nextVendorCreditResponse();
        //        }

        //        reserveCreditResponse reserveCreditResponse = cnpBatchResponse.nextReserveCreditResponse();
        //        while (reserveCreditResponse != null)
        //        {
        //            Assert.AreEqual("000", reserveCreditResponse.response);
        //            reserveCreditResponse = cnpBatchResponse.nextReserveCreditResponse();
        //        }

        //        physicalCheckCreditResponse physicalCheckCreditResponse = cnpBatchResponse.nextPhysicalCheckCreditResponse();
        //        while (physicalCheckCreditResponse != null)
        //        {
        //            Assert.AreEqual("000", physicalCheckCreditResponse.response);
        //            physicalCheckCreditResponse = cnpBatchResponse.nextPhysicalCheckCreditResponse();
        //        }

        //        submerchantDebitResponse submerchantDebitResponse = cnpBatchResponse.nextSubmerchantDebitResponse();
        //        while (submerchantDebitResponse != null)
        //        {
        //            Assert.AreEqual("000", submerchantDebitResponse.response);
        //            submerchantDebitResponse = cnpBatchResponse.nextSubmerchantDebitResponse();
        //        }

        //        payFacDebitResponse payFacDebitResponse = cnpBatchResponse.nextPayFacDebitResponse();
        //        while (payFacDebitResponse != null)
        //        {
        //            Assert.AreEqual("000", payFacDebitResponse.response);
        //            payFacDebitResponse = cnpBatchResponse.nextPayFacDebitResponse();
        //        }

        //        vendorDebitResponse vendorDebitResponse = cnpBatchResponse.nextVendorDebitResponse();
        //        while (vendorDebitResponse != null)
        //        {
        //            Assert.AreEqual("000", vendorDebitResponse.response);
        //            vendorDebitResponse = cnpBatchResponse.nextVendorDebitResponse();
        //        }

        //        reserveDebitResponse reserveDebitResponse = cnpBatchResponse.nextReserveDebitResponse();
        //        while (reserveDebitResponse != null)
        //        {
        //            Assert.AreEqual("000", reserveDebitResponse.response);
        //            reserveDebitResponse = cnpBatchResponse.nextReserveDebitResponse();
        //        }

        //        physicalCheckDebitResponse physicalCheckDebitResponse = cnpBatchResponse.nextPhysicalCheckDebitResponse();
        //        while (physicalCheckDebitResponse != null)
        //        {
        //            Assert.AreEqual("000", physicalCheckDebitResponse.response);
        //            physicalCheckDebitResponse = cnpBatchResponse.nextPhysicalCheckDebitResponse();
        //        }

        //        cnpBatchResponse = cnpResponse.nextBatchResponse();
        //    }
        //}
    }
}
