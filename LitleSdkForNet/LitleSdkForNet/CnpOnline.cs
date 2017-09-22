using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace Cnp.Sdk
{
    public class CnpOnline : ICnpOnline
    {
        private Dictionary<string, string> _config;
        private Communications _communication;

        /**
         * Construct a Cnp online using the configuration specified in CnpSdkForNet.dll.config
         */
        public CnpOnline()
        {
            _config = new Dictionary<string, string>();
            _config["url"] = Properties.Settings.Default.url;
            _config["reportGroup"] = Properties.Settings.Default.reportGroup;
            _config["username"] = Properties.Settings.Default.username;
            _config["printxml"] = Properties.Settings.Default.printxml;
            _config["timeout"] = Properties.Settings.Default.timeout;
            _config["proxyHost"] = Properties.Settings.Default.proxyHost;
            _config["merchantId"] = Properties.Settings.Default.merchantId;
            _config["password"] = Properties.Settings.Default.password;
            _config["proxyPort"] = Properties.Settings.Default.proxyPort;
            _config["logFile"] = Properties.Settings.Default.logFile;
            _config["neuterAccountNums"] = Properties.Settings.Default.neuterAccountNums;
            _communication = new Communications();
            
        }

        /**
         * Construct a CnpOnline specifying the configuration in code.  This should be used by integration that have another way
         * to specify their configuration settings or where different configurations are needed for different instances of CnpOnline.
         * 
         * Properties that *must* be set are:
         * url (eg https://payments.cnp.com/vap/communicator/online)
         * reportGroup (eg "Default Report Group")
         * username
         * merchantId
         * password
         * timeout (in seconds)
         * Optional properties are:
         * proxyHost
         * proxyPort
         * printxml (possible values "true" and "false" - defaults to false)
         */
        public CnpOnline(Dictionary<string, string> config)
        {
            this._config = config;
            _communication = new Communications();
        }

        public event EventHandler HttpAction
        {
            add { _communication.HttpAction += value; }
            remove { _communication.HttpAction -= value; }
        }

        public void setCommunication(Communications communication)
        {
            this._communication = communication;
        }

        public authorizationResponse Authorize(authorization auth)
        {
            var request = CreateCnpOnlineRequest();          
            fillInReportGroup(auth);
            request.authorization = auth;

            var response = sendToCnp(request);
            var authResponse = response.authorizationResponse;
            return authResponse;
        }

        public authReversalResponse AuthReversal(authReversal reversal)
        {
            var request = CreateCnpOnlineRequest();
            fillInReportGroup(reversal);
            request.authReversal = reversal;

            var response = sendToCnp(request);
            var reversalResponse = response.authReversalResponse;
            return reversalResponse;
        }

        public giftCardAuthReversalResponse GiftCardAuthReversal(giftCardAuthReversal giftCard)
        {
            var request = CreateCnpOnlineRequest();
            fillInReportGroup(giftCard);
            request.giftCardAuthReversal = giftCard;

            var response = sendToCnp(request);
            var giftCardAuthReversalResponse = response.giftCardAuthReversalResponse;
            return giftCardAuthReversalResponse;
        }

        public captureResponse Capture(capture capture)
        {
            var request = CreateCnpOnlineRequest();
            fillInReportGroup(capture);
            request.capture = capture;

            var response = sendToCnp(request);
            var captureResponse = response.captureResponse;
            return captureResponse;
        }

        public giftCardCaptureResponse GiftCardCapture(giftCardCapture giftCardCapture)
        {
            var request = CreateCnpOnlineRequest();
            fillInReportGroup(giftCardCapture);
            request.giftCardCapture = giftCardCapture;

            var response = sendToCnp(request);
            var giftCardCaptureResponse = response.giftCardCaptureResponse;
            return giftCardCaptureResponse;
        }

        public captureGivenAuthResponse CaptureGivenAuth(captureGivenAuth captureGivenAuth)
        {
            var request = CreateCnpOnlineRequest();
            fillInReportGroup(captureGivenAuth);
            request.captureGivenAuth = captureGivenAuth;

            var response = sendToCnp(request);
            var captureGivenAuthResponse = response.captureGivenAuthResponse;
            return captureGivenAuthResponse;
        }

        public creditResponse Credit(credit credit)
        {
            var request = CreateCnpOnlineRequest();
            fillInReportGroup(credit);
            request.credit = credit;

            var response = sendToCnp(request);
            var creditResponse = response.creditResponse;
            return creditResponse;
        }

        public giftCardCreditResponse GiftCardCredit(giftCardCredit giftCardCredit)
        {
            var request = CreateCnpOnlineRequest();
            fillInReportGroup(giftCardCredit);
            request.giftCardCredit = giftCardCredit;

            var response = sendToCnp(request);
            var giftCardCreditResponse = response.giftCardCreditResponse;
            return giftCardCreditResponse;
        }

        public echeckCreditResponse EcheckCredit(echeckCredit echeckCredit)
        {
            var request = CreateCnpOnlineRequest();
            fillInReportGroup(echeckCredit);
            request.echeckCredit = echeckCredit;

            var response = sendToCnp(request);
            var echeckCreditResponse = response.echeckCreditResponse;
            return echeckCreditResponse;
        }

        public echeckRedepositResponse EcheckRedeposit(echeckRedeposit echeckRedeposit)
        {
            var request = CreateCnpOnlineRequest();
            fillInReportGroup(echeckRedeposit);
            request.echeckRedeposit = echeckRedeposit;

            var response = sendToCnp(request);
            var echeckRedepositResponse = response.echeckRedepositResponse;
            return echeckRedepositResponse;
        }

        public echeckSalesResponse EcheckSale(echeckSale echeckSale)
        {
            var request = CreateCnpOnlineRequest();
            fillInReportGroup(echeckSale);
            request.echeckSale = echeckSale;

            var response = sendToCnp(request);
            var echeckSalesResponse = response.echeckSalesResponse;
            return echeckSalesResponse;
        }

        public echeckVerificationResponse EcheckVerification(echeckVerification echeckVerification)
        {
            var request = CreateCnpOnlineRequest();
            fillInReportGroup(echeckVerification);
            request.echeckVerification = echeckVerification;

            var response = sendToCnp(request);
            var echeckVerificationResponse = response.echeckVerificationResponse;
            return echeckVerificationResponse;
        }

        public forceCaptureResponse ForceCapture(forceCapture forceCapture)
        {
            var request = CreateCnpOnlineRequest();
            fillInReportGroup(forceCapture);
            request.forceCapture = forceCapture;

            var response = sendToCnp(request);
            var forceCaptureResponse = response.forceCaptureResponse;
            return forceCaptureResponse;
        }

        public saleResponse Sale(sale sale)
        {
            var request = CreateCnpOnlineRequest();
            fillInReportGroup(sale);
            request.sale = sale;

            var response = sendToCnp(request);
            var saleResponse = response.saleResponse;
            return saleResponse;
        }

        public registerTokenResponse RegisterToken(registerTokenRequestType tokenRequest)
        {
            var request = CreateCnpOnlineRequest();
            fillInReportGroup(tokenRequest);
            request.registerTokenRequest = tokenRequest;

            var response = sendToCnp(request);
            var registerTokenResponse = response.registerTokenResponse;
            return registerTokenResponse;
        }

        public cnpOnlineResponseTransactionResponseVoidResponse DoVoid(voidTxn v)
        {
            var request = CreateCnpOnlineRequest();
            fillInReportGroup(v);
            request.voidTxn = v;

            var response = sendToCnp(request);
            var voidResponse = response.voidResponse;
            return voidResponse;
        }

        public cnpOnlineResponseTransactionResponseEcheckVoidResponse EcheckVoid(echeckVoid v)
        {
            var request = CreateCnpOnlineRequest();
            fillInReportGroup(v);
            request.echeckVoid = v;

            var response = sendToCnp(request);
            var voidResponse = response.echeckVoidResponse;
            return voidResponse;
        }

        public updateCardValidationNumOnTokenResponse UpdateCardValidationNumOnToken(updateCardValidationNumOnToken updateCardValidationNumOnToken)
        {
            var request = CreateCnpOnlineRequest();
            fillInReportGroup(updateCardValidationNumOnToken);
            request.updateCardValidationNumOnToken = updateCardValidationNumOnToken;

            var response = sendToCnp(request);
            var updateResponse = response.updateCardValidationNumOnTokenResponse;
            return updateResponse;
        }

        public cancelSubscriptionResponse CancelSubscription(cancelSubscription cancelSubscription)
        {
            var request = CreateCnpOnlineRequest();
            request.cancelSubscription = cancelSubscription;

            var response = sendToCnp(request);
            var cancelResponse = response.cancelSubscriptionResponse;
            return cancelResponse;
        }

        public updateSubscriptionResponse UpdateSubscription(updateSubscription updateSubscription)
        {
            var request = CreateCnpOnlineRequest();
            request.updateSubscription = updateSubscription;

            var response = sendToCnp(request);
            var updateResponse = response.updateSubscriptionResponse;
            return updateResponse;
        }

        public activateResponse Activate(activate activate)
        {
            var request = CreateCnpOnlineRequest();
            request.activate = activate;

            var response = sendToCnp(request);
            var activateResponse = response.activateResponse;
            return activateResponse;
        }

        public deactivateResponse Deactivate(deactivate deactivate)
        {
            var request = CreateCnpOnlineRequest();
            request.deactivate = deactivate;

            var response = sendToCnp(request);
            var deactivateResponse = response.deactivateResponse;
            return deactivateResponse;
        }

        public loadResponse Load(load load)
        {
            var request = CreateCnpOnlineRequest();
            request.load = load;

            var response = sendToCnp(request);
            var loadResponse = response.loadResponse;
            return loadResponse;
        }

        public unloadResponse Unload(unload unload)
        {
            var request = CreateCnpOnlineRequest();
            request.unload = unload;

            var response = sendToCnp(request);
            var unloadResponse = response.unloadResponse;
            return unloadResponse;
        }

        public balanceInquiryResponse BalanceInquiry(balanceInquiry balanceInquiry)
        {
            var request = CreateCnpOnlineRequest();
            request.balanceInquiry = balanceInquiry;

            var response = sendToCnp(request);
            var balanceInquiryResponse = response.balanceInquiryResponse;
            return balanceInquiryResponse;
        }

        public createPlanResponse CreatePlan(createPlan createPlan)
        {
            var request = CreateCnpOnlineRequest();
            request.createPlan = createPlan;

            var response = sendToCnp(request);
            var createPlanResponse = response.createPlanResponse;
            return createPlanResponse;
        }

        public updatePlanResponse UpdatePlan(updatePlan updatePlan)
        {
            var request = CreateCnpOnlineRequest();
            request.updatePlan = updatePlan;

            var response = sendToCnp(request);
            var updatePlanResponse = response.updatePlanResponse;
            return updatePlanResponse;
        }

        public refundReversalResponse RefundReversal(refundReversal refundReversal)
        {
            var request = CreateCnpOnlineRequest();
            request.refundReversal = refundReversal;

            var response = sendToCnp(request);
            var refundReversalResponse = response.refundReversalResponse;
            return refundReversalResponse;
        }

        public depositReversalResponse DepositReversal(depositReversal depositReversal)
        {
            var request = CreateCnpOnlineRequest();
            request.depositReversal = depositReversal;

            var response = sendToCnp(request);
            var depositReversalResponse = response.depositReversalResponse;
            return depositReversalResponse;
        }

        public activateReversalResponse ActivateReversal(activateReversal activateReversal)
        {
            var request = CreateCnpOnlineRequest();
            request.activateReversal = activateReversal;

            var response = sendToCnp(request);
            var activateReversalResponse = response.activateReversalResponse;
            return activateReversalResponse;
        }

        public deactivateReversalResponse DeactivateReversal(deactivateReversal deactivateReversal)
        {
            var request = CreateCnpOnlineRequest();
            request.deactivateReversal = deactivateReversal;

            var response = sendToCnp(request);
            var deactivateReversalResponse = response.deactivateReversalResponse;
            return deactivateReversalResponse;
        }

        public loadReversalResponse LoadReversal(loadReversal loadReversal)
        {
            var request = CreateCnpOnlineRequest();
            request.loadReversal = loadReversal;

            var response = sendToCnp(request);
            var loadReversalResponse = response.loadReversalResponse;
            return loadReversalResponse;
        }

        public unloadReversalResponse UnloadReversal(unloadReversal unloadReversal)
        {
            var request = CreateCnpOnlineRequest();
            request.unloadReversal = unloadReversal;

            var response = sendToCnp(request);
            var unloadReversalResponse = response.unloadReversalResponse;
            return unloadReversalResponse;
        }

        public transactionTypeWithReportGroup queryTransaction(queryTransaction queryTransaction)
        {
            var request = CreateCnpOnlineRequest();
            request.queryTransaction = queryTransaction;

            var cnpresponse = sendToCnp(request);
            transactionTypeWithReportGroup response = null;
            if (cnpresponse.queryTransactionResponse != null)
            {
                response = cnpresponse.queryTransactionResponse;
            }
            else if (cnpresponse.queryTransactionUnavailableResponse != null)
            {
                response = cnpresponse.queryTransactionUnavailableResponse;
            }
            return response;
        }

        public fraudCheckResponse FraudCheck(fraudCheck fraudCheck)
        {
            var request = CreateCnpOnlineRequest();
            fillInReportGroup(fraudCheck);
            request.fraudCheck = fraudCheck;

            var response = sendToCnp(request);
            var fraudCheckResponse = response.fraudCheckResponse;
            return fraudCheckResponse;
        }

        private cnpOnlineRequest CreateCnpOnlineRequest()
        {
            var request = new cnpOnlineRequest();
            request.merchantId = _config["merchantId"];
            request.merchantSdk = "DotNet;12.0.0";
            var authentication = new authentication();
            authentication.password = _config["password"];
            authentication.user = _config["username"];
            request.authentication = authentication;
            return request;
        }

        private cnpOnlineResponse sendToCnp(cnpOnlineRequest request)
        {
            var xmlRequest = request.Serialize();
            var xmlResponse = _communication.HttpPost(xmlRequest,_config);
            try
            {
                var cnpOnlineResponse = DeserializeObject(xmlResponse);
                if ("1".Equals(cnpOnlineResponse.response))
                {
                    throw new CnpOnlineException(cnpOnlineResponse.message);
                }
                return cnpOnlineResponse;
            }
            catch (InvalidOperationException ioe)
            {
                throw new CnpOnlineException("Error validating xml data against the schema", ioe);
            }
        }

        public static string SerializeObject(cnpOnlineRequest req)
        {
            var serializer = new XmlSerializer(typeof(cnpOnlineRequest));
            var ms = new MemoryStream();
            serializer.Serialize(ms, req);
            return Encoding.UTF8.GetString(ms.GetBuffer());//return string is UTF8 encoded.
        }// serialize the xml

        public static cnpOnlineResponse DeserializeObject(string response)
        {
            var serializer = new XmlSerializer(typeof(cnpOnlineResponse));
            var reader = new StringReader(response);
            var i = (cnpOnlineResponse)serializer.Deserialize(reader);
            return i;

        }// deserialize the object

        private void fillInReportGroup(transactionTypeWithReportGroup txn)
        {
            if (txn.reportGroup == null)
            {
                txn.reportGroup = _config["reportGroup"];
            }
        }

        private void fillInReportGroup(transactionTypeWithReportGroupAndPartial txn)
        {
            if (txn.reportGroup == null)
            {
                txn.reportGroup = _config["reportGroup"];
            }
        }
    }

    public interface ICnpOnline
    {
        authorizationResponse Authorize(authorization auth);
        authReversalResponse AuthReversal(authReversal reversal);
        captureResponse Capture(capture capture);
        captureGivenAuthResponse CaptureGivenAuth(captureGivenAuth captureGivenAuth);
        creditResponse Credit(credit credit);
        echeckCreditResponse EcheckCredit(echeckCredit echeckCredit);
        echeckRedepositResponse EcheckRedeposit(echeckRedeposit echeckRedeposit);
        echeckSalesResponse EcheckSale(echeckSale echeckSale);
        echeckVerificationResponse EcheckVerification(echeckVerification echeckVerification);
        forceCaptureResponse ForceCapture(forceCapture forceCapture);
        saleResponse Sale(sale sale);
        registerTokenResponse RegisterToken(registerTokenRequestType tokenRequest);
        cnpOnlineResponseTransactionResponseVoidResponse DoVoid(voidTxn v);
        cnpOnlineResponseTransactionResponseEcheckVoidResponse EcheckVoid(echeckVoid v);
        updateCardValidationNumOnTokenResponse UpdateCardValidationNumOnToken(updateCardValidationNumOnToken update);
    }
}
