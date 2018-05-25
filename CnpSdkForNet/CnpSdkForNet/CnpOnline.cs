using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Cnp.Sdk
{
    // Represent an online request.
    // Defining all transactions supported for online processing.
    public class CnpOnline : ICnpOnline
    {
        // Configuration object containing credentials and settings.
        private Dictionary<string, string> _config;
        // 
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

        public void SetCommunication(Communications communication)
        {
            this._communication = communication;
        }

        public Task<authorizationResponse> AuthorizeAsync(authorization auth, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.authorizationResponse, auth, cancellationToken);
        }

        private T SendRequest<T>(Func<cnpOnlineResponse, T> getResponse, transactionRequest transaction)
        {
            var request = CreateRequest(transaction);
            cnpOnlineResponse response = SendToCnp(request);

            return getResponse(response);
        }

        private cnpOnlineRequest CreateRequest(transactionRequest transaction)
        {
            cnpOnlineRequest request = CreateCnpOnlineRequest();

            if (transaction is transactionTypeWithReportGroup)
            {
                FillInReportGroup((transactionTypeWithReportGroup)transaction);
            }
            else if (transaction is transactionTypeWithReportGroupAndPartial)
            {
                FillInReportGroup((transactionTypeWithReportGroupAndPartial)transaction);
            }
            if (transaction is authorization)
            {
                request.authorization = (authorization)transaction;
            }
            else if (transaction is authReversal)
            {
                request.authReversal = (authReversal)transaction;

            }
            else if (transaction is capture)
            {
                request.capture = (capture)transaction;

            }
            else if (transaction is captureGivenAuth)
            {
                request.captureGivenAuth = (captureGivenAuth)transaction;

            }
            else if (transaction is credit)
            {
                request.credit = (credit)transaction;

            }
            else if (transaction is echeckCredit)
            {
                request.echeckCredit = (echeckCredit)transaction;

            }
            else if (transaction is echeckRedeposit)
            {
                request.echeckRedeposit = (echeckRedeposit)transaction;

            }
            else if (transaction is echeckSale)
            {
                request.echeckSale = (echeckSale)transaction;

            }
            else if (transaction is echeckVerification)
            {
                request.echeckVerification = (echeckVerification)transaction;

            }
            else if (transaction is forceCapture)
            {
                request.forceCapture = (forceCapture)transaction;

            }
            else if (transaction is sale)
            {
                request.sale = (sale)transaction;

            }
            else if (transaction is registerTokenRequestType)
            {
                request.registerTokenRequest = (registerTokenRequestType)transaction;

            }
            else if (transaction is voidTxn)
            {
                request.voidTxn = (voidTxn)transaction;

            }
            else if (transaction is echeckVoid)
            {
                request.echeckVoid = (echeckVoid)transaction;

            }
            else if (transaction is updateCardValidationNumOnToken)
            {
                request.updateCardValidationNumOnToken = (updateCardValidationNumOnToken)transaction;

            }
            else if (transaction is cancelSubscription)
            {
                request.cancelSubscription = (cancelSubscription)transaction;

            }
            else if (transaction is updateSubscription)
            {
                request.updateSubscription = (updateSubscription)transaction;

            }
            else if (transaction is activate)
            {
                request.activate = (activate)transaction;

            }
            else if (transaction is deactivate)
            {
                request.deactivate = (deactivate)transaction;

            }
            else if (transaction is load)
            {
                request.load = (load)transaction;

            }
            else if (transaction is unload)
            {
                request.unload = (unload)transaction;

            }
            else if (transaction is balanceInquiry)
            {
                request.balanceInquiry = (balanceInquiry)transaction;

            }
            else if (transaction is createPlan)
            {
                request.createPlan = (createPlan)transaction;

            }
            else if (transaction is updatePlan)
            {
                request.updatePlan = (updatePlan)transaction;

            }
            else if (transaction is refundReversal)
            {
                request.refundReversal = (refundReversal)transaction;

            }
            else if (transaction is depositReversal)
            {
                request.depositReversal = (depositReversal)transaction;

            }
            else if (transaction is activateReversal)
            {
                request.activateReversal = (activateReversal)transaction;

            }
            else if (transaction is deactivateReversal)
            {
                request.deactivateReversal = (deactivateReversal)transaction;

            }
            else if (transaction is loadReversal)
            {
                request.loadReversal = (loadReversal)transaction;

            }
            else if (transaction is unloadReversal)
            {
                request.unloadReversal = (unloadReversal)transaction;

            }
            else if (transaction is fraudCheck)
            {
                request.fraudCheck = (fraudCheck)transaction;

            }
            else if (transaction is giftCardAuthReversal)
            {
                request.giftCardAuthReversal = (giftCardAuthReversal)transaction;
            }
            else if (transaction is giftCardCapture)
            {
                request.giftCardCapture = (giftCardCapture)transaction;
            }
            else if (transaction is giftCardCredit)
            {
                request.giftCardCredit = (giftCardCredit)transaction;
            }
            else if (transaction is queryTransaction)
            {
                request.queryTransaction = (queryTransaction)transaction;
            }
            else if (transaction is fastAccessFunding)
            {
                request.fastAccessFunding = (fastAccessFunding) transaction;
            }
            else if (transaction is payFacCredit)
            {
                request.payFacCredit = (payFacCredit) transaction;
            }
            else if (transaction is payFacDebit)
            {
                request.payFacDebit = (payFacDebit) transaction;
            }
            else if (transaction is physicalCheckCredit)
            {
                request.physicalCheckCredit = (physicalCheckCredit) transaction;
            }
            else if (transaction is physicalCheckDebit)
            {
                request.physicalCheckDebit = (physicalCheckDebit) transaction;
            }
            else if (transaction is reserveCredit)
            {
                request.reserveCredit = (reserveCredit) transaction;
            }
            else if (transaction is reserveDebit)
            {
                request.reserveDebit = (reserveDebit) transaction;
            }
            else if (transaction is submerchantCredit)
            {
                request.submerchantCredit = (submerchantCredit) transaction;
            }
            else if (transaction is submerchantDebit)
            {
                request.submerchantDebit = (submerchantDebit) transaction;
            }
            else if (transaction is vendorCredit)
            {
                request.vendorCredit = (vendorCredit) transaction;
            }
            else if (transaction is vendorDebit)
            {
                request.vendorDebit = (vendorDebit) transaction;
            }
            else
            {
                throw new NotImplementedException("Support for type: " + transaction.GetType().Name +
                                                  " not implemented.");
            }
            return request;
        }

        public authorizationResponse Authorize(authorization auth)
        {
            return SendRequest(response => response.authorizationResponse, auth);
        }

        public authReversalResponse AuthReversal(authReversal reversal)
        {
            return SendRequest(response => response.authReversalResponse, reversal);
        }

        public Task<authReversalResponse> AuthReversalAsync(authReversal reversal, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.authReversalResponse, reversal, cancellationToken);
        }

        public giftCardAuthReversalResponse GiftCardAuthReversal(giftCardAuthReversal giftCard)
        {
            return SendRequest(response => response.giftCardAuthReversalResponse, giftCard);
        }

        public Task<giftCardAuthReversalResponse> GiftCardAuthReversalAsync(giftCardAuthReversal giftCard, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.giftCardAuthReversalResponse, giftCard, cancellationToken);
        }

        public Task<captureResponse> CaptureAsync(capture capture, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.captureResponse, capture, cancellationToken);
        }

        public captureResponse Capture(capture capture)
        {
            return SendRequest(response => response.captureResponse, capture);
        }

        public giftCardCaptureResponse GiftCardCapture(giftCardCapture giftCardCapture)
        {
            return SendRequest(response => response.giftCardCaptureResponse, giftCardCapture);
        }

        public Task<giftCardCaptureResponse> GiftCardCaptureAsync(giftCardCapture giftCardCapture, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.giftCardCaptureResponse, giftCardCapture, cancellationToken);
        }

        public Task<captureGivenAuthResponse> CaptureGivenAuthAsync(captureGivenAuth captureGivenAuth, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.captureGivenAuthResponse, captureGivenAuth, cancellationToken);
        }

        public captureGivenAuthResponse CaptureGivenAuth(captureGivenAuth captureGivenAuth)
        {
            return SendRequest(response => response.captureGivenAuthResponse, captureGivenAuth);
        }

        public creditResponse Credit(credit credit)
        {
            return SendRequest(response => response.creditResponse, credit);
        }

        public Task<creditResponse> CreditAsync(credit credit, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.creditResponse, credit, cancellationToken);
        }

        public Task<vendorDebitResponse> VendorDebitAsync(vendorDebit vendorDebit, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.vendorDebitResponse, vendorDebit, cancellationToken);
        }

        public giftCardCreditResponse GiftCardCredit(giftCardCredit giftCardCredit)
        {
            return SendRequest(response => response.giftCardCreditResponse, giftCardCredit);
        }

        public Task<giftCardCreditResponse> GiftCardCreditAsync(giftCardCredit giftCardCredit, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.giftCardCreditResponse, giftCardCredit, cancellationToken);
        }

        public Task<echeckCreditResponse> EcheckCreditAsync(echeckCredit echeckCredit, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.echeckCreditResponse, echeckCredit, cancellationToken);
        }

        public echeckCreditResponse EcheckCredit(echeckCredit echeckCredit)
        {
            return SendRequest(response => response.echeckCreditResponse, echeckCredit);
        }

        public Task<echeckRedepositResponse> EcheckRedepositAsync(echeckRedeposit echeckRedeposit, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.echeckRedepositResponse, echeckRedeposit, cancellationToken);
        }

        public echeckRedepositResponse EcheckRedeposit(echeckRedeposit echeckRedeposit)
        {
            return SendRequest(response => response.echeckRedepositResponse, echeckRedeposit);
        }

        public Task<echeckSalesResponse> EcheckSaleAsync(echeckSale echeckSale, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.echeckSalesResponse, echeckSale, cancellationToken);
        }

        public echeckSalesResponse EcheckSale(echeckSale echeckSale)
        {
            return SendRequest(response => response.echeckSalesResponse, echeckSale);
        }

        public echeckVerificationResponse EcheckVerification(echeckVerification echeckVerification)
        {
            return SendRequest(response => response.echeckVerificationResponse, echeckVerification);
        }

        public Task<echeckVerificationResponse> EcheckVerificationAsync(echeckVerification echeckVerification, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.echeckVerificationResponse, echeckVerification, cancellationToken);
        }

        public forceCaptureResponse ForceCapture(forceCapture forceCapture)
        {
            return SendRequest(response => response.forceCaptureResponse, forceCapture);
        }

        public Task<forceCaptureResponse> ForceCaptureAsync(forceCapture forceCapture, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.forceCaptureResponse, forceCapture, cancellationToken);
        }

        public saleResponse Sale(sale sale)
        {
            return SendRequest(response => response.saleResponse, sale);
        }

        public Task<saleResponse> SaleAsync(sale sale, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.saleResponse, sale, cancellationToken);
        }

        public Task<registerTokenResponse> RegisterTokenAsync(registerTokenRequestType tokenRequest, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.registerTokenResponse, tokenRequest, cancellationToken);
        }

        public registerTokenResponse RegisterToken(registerTokenRequestType tokenRequest)
        {
            return SendRequest(response => response.registerTokenResponse, tokenRequest);
        }

        public cnpOnlineResponseTransactionResponseVoidResponse DoVoid(voidTxn v)
        {
            return SendRequest(response => response.voidResponse, v);
        }

        public Task<cnpOnlineResponseTransactionResponseVoidResponse> DoVoidAsync(voidTxn v, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.voidResponse, v, cancellationToken);
        }

        public cnpOnlineResponseTransactionResponseEcheckVoidResponse EcheckVoid(echeckVoid v)
        {
            return SendRequest(response => response.echeckVoidResponse, v);
        }

        public Task<cnpOnlineResponseTransactionResponseEcheckVoidResponse> EcheckVoidAsync(echeckVoid v, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.echeckVoidResponse, v, cancellationToken);
        }

        public updateCardValidationNumOnTokenResponse UpdateCardValidationNumOnToken(updateCardValidationNumOnToken updateCardValidationNumOnToken)
        {
            return SendRequest(response => response.updateCardValidationNumOnTokenResponse, updateCardValidationNumOnToken);
        }

        public Task<updateCardValidationNumOnTokenResponse> UpdateCardValidationNumOnTokenAsync(updateCardValidationNumOnToken update, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.updateCardValidationNumOnTokenResponse, update, cancellationToken);
        }

        public cancelSubscriptionResponse CancelSubscription(cancelSubscription cancelSubscription)
        {
            return SendRequest(response => response.cancelSubscriptionResponse, cancelSubscription);
        }

        public updateSubscriptionResponse UpdateSubscription(updateSubscription updateSubscription)
        {
            return SendRequest(response => response.updateSubscriptionResponse, updateSubscription);
        }

        public activateResponse Activate(activate activate)
        {
            return SendRequest(response => response.activateResponse, activate);
        }

        public deactivateResponse Deactivate(deactivate deactivate)
        {
            return SendRequest(response => response.deactivateResponse, deactivate);
        }

        public loadResponse Load(load load)
        {
            return SendRequest(response => response.loadResponse, load);
        }

        public unloadResponse Unload(unload unload)
        {
            return SendRequest(response => response.unloadResponse, unload);
        }

        public balanceInquiryResponse BalanceInquiry(balanceInquiry balanceInquiry)
        {
            return SendRequest(response => response.balanceInquiryResponse, balanceInquiry);
        }

        public Task<balanceInquiryResponse> BalanceInquiryAsync(balanceInquiry balanceInquiry, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.balanceInquiryResponse, balanceInquiry, cancellationToken);
        }

        public createPlanResponse CreatePlan(createPlan createPlan)
        {
            return SendRequest(response => response.createPlanResponse, createPlan);
        }

        public updatePlanResponse UpdatePlan(updatePlan updatePlan)
        {
            return SendRequest(response => response.updatePlanResponse, updatePlan);
        }

        public refundReversalResponse RefundReversal(refundReversal refundReversal)
        {
            return SendRequest(response => response.refundReversalResponse, refundReversal);
        }

        public depositReversalResponse DepositReversal(depositReversal depositReversal)
        {
            return SendRequest(response => response.depositReversalResponse, depositReversal);
        }

        public activateReversalResponse ActivateReversal(activateReversal activateReversal)
        {
            return SendRequest(response => response.activateReversalResponse, activateReversal);
        }

        public deactivateReversalResponse DeactivateReversal(deactivateReversal deactivateReversal)
        {
            return SendRequest(response => response.deactivateReversalResponse, deactivateReversal);
        }

        public loadReversalResponse LoadReversal(loadReversal loadReversal)
        {
            return SendRequest(response => response.loadReversalResponse, loadReversal);
        }

        public unloadReversalResponse UnloadReversal(unloadReversal unloadReversal)
        {
            return SendRequest(response => response.unloadReversalResponse, unloadReversal);
        }

        public Task<transactionTypeWithReportGroup> QueryTransactionAsync(queryTransaction queryTransaction, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => (response.queryTransactionResponse ?? (transactionTypeWithReportGroup)response.queryTransactionUnavailableResponse), queryTransaction, cancellationToken);
        }

        public transactionTypeWithReportGroup QueryTransaction(queryTransaction queryTransaction)
        {
            return SendRequest(response =>(response.queryTransactionResponse ?? (transactionTypeWithReportGroup)response.queryTransactionUnavailableResponse), queryTransaction);
        }

        public fraudCheckResponse FraudCheck(fraudCheck fraudCheck)
        {
            return SendRequest(response => response.fraudCheckResponse, fraudCheck);
        }

        public fastAccessFundingResponse FastAccessFunding(fastAccessFunding fastAccessFunding)
        {
            return SendRequest(response => response.fastAccessFundingResponse, fastAccessFunding);
        }
        
        public payFacCreditResponse PayFacCredit(payFacCredit payFacCredit)
        {
            return SendRequest(response => response.payFacCreditResponse, payFacCredit);
        }

        public Task<payFacCreditResponse> PayFacCreditAsync(payFacCredit payFacCredit, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.payFacCreditResponse, payFacCredit, cancellationToken);
        }

        public payFacDebitResponse PayFacDebit(payFacDebit payFacDebit)
        {
            return SendRequest(response => response.payFacDebitResponse, payFacDebit);
        }

        public Task<payFacDebitResponse> PayFacDebitAsync(payFacDebit payFacDebit, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.payFacDebitResponse, payFacDebit, cancellationToken);
        }

        public physicalCheckCreditResponse PhysicalCheckCredit(physicalCheckCredit physicalCheckCredit)
        {
            return SendRequest(response => response.physicalCheckCreditResponse, physicalCheckCredit);
        }

        public Task<physicalCheckCreditResponse> PhysicalCheckCreditAsync(physicalCheckCredit physicalCheckCredit, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.physicalCheckCreditResponse, physicalCheckCredit, cancellationToken);
        }

        public physicalCheckDebitResponse PhysicalCheckDebit(physicalCheckDebit physicalCheckDebit)
        {
            return SendRequest(response => response.physicalCheckDebitResponse, physicalCheckDebit);
        }

        public Task<physicalCheckDebitResponse> PhysicalCheckDebitAsync(physicalCheckDebit physicalCheckDebit, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.physicalCheckDebitResponse, physicalCheckDebit, cancellationToken);
        }

        public reserveCreditResponse ReserveCredit(reserveCredit reserveCredit)
        {
            return SendRequest(response => response.reserveCreditResponse, reserveCredit);
        }

        public Task<reserveCreditResponse> ReserveCreditAsync(reserveCredit reserveCredit, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.reserveCreditResponse, reserveCredit, cancellationToken);
        }

        public reserveDebitResponse ReserveDebit(reserveDebit reserveDebit)
        {
            return SendRequest(response => response.reserveDebitResponse, reserveDebit);
        }

        public Task<reserveDebitResponse> ReserveDebitAsync(reserveDebit reserveDebit, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.reserveDebitResponse, reserveDebit, cancellationToken);
        }

        public submerchantCreditResponse SubmerchantCredit(submerchantCredit submerchantCredit)
        {
            return SendRequest(response => response.submerchantCreditResponse, submerchantCredit);
        }

        public Task<submerchantCreditResponse> SubmerchantCreditAsync(submerchantCredit submerchantCredit, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.submerchantCreditResponse, submerchantCredit, cancellationToken);
        }

        public submerchantDebitResponse SubmerchantDebit(submerchantDebit submerchantDebit)
        {
            return SendRequest(response => response.submerchantDebitResponse, submerchantDebit);
        }

        public Task<submerchantDebitResponse> SubmerchantDebitAsync(submerchantDebit submerchantDebit, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.submerchantDebitResponse, submerchantDebit, cancellationToken);
        }

        public vendorCreditResponse VendorCredit(vendorCredit vendorCredit)
        {
            return SendRequest(response => response.vendorCreditResponse, vendorCredit);
        }

        public Task<vendorCreditResponse> VendorCreditAsync(vendorCredit vendorCredit, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response => response.vendorCreditResponse, vendorCredit, cancellationToken);
        }

        public vendorDebitResponse VendorDebit(vendorDebit vendorDebit)
        {
            return SendRequest(response => response.vendorDebitResponse, vendorDebit);
        }

        private cnpOnlineRequest CreateCnpOnlineRequest()
        {
            var request = new cnpOnlineRequest();
            request.merchantId = _config["merchantId"];
            request.merchantSdk = "DotNet;12.3.0";
            var authentication = new authentication();
            authentication.password = _config["password"];
            authentication.user = _config["username"];
            request.authentication = authentication;
            return request;
        }

        private cnpOnlineResponse SendToCnp(cnpOnlineRequest request)
        {
            var xmlRequest = request.Serialize();
            var xmlResponse = _communication.HttpPost(xmlRequest, _config);
            try
            {
                var cnpOnlineResponse = DeserializeObject(xmlResponse);
                if (_config.ContainsKey("printxml") && Convert.ToBoolean(_config["printxml"]))
                {
                    
                    Console.WriteLine(cnpOnlineResponse.response);
                    
                }
                if (!"0".Equals(cnpOnlineResponse.response))
                {
                    if ("2".Equals(cnpOnlineResponse.response) || "3".Equals(cnpOnlineResponse.response))
                    {
                        throw new CnpInvalidCredentialException(cnpOnlineResponse.message);
                    }
                    else if ("4".Equals(cnpOnlineResponse.response))
                    {
                        throw new CnpConnectionLimitExceededException(cnpOnlineResponse.message);
                    }
                    else if ("5".Equals(cnpOnlineResponse.response))
                    {
                        throw new CnpObjectionableContentException(cnpOnlineResponse.message);
                    }
                    else
                    {
                        throw new CnpOnlineException(cnpOnlineResponse.message);
                    }
                }
                return cnpOnlineResponse;
            }
            catch (InvalidOperationException ioe)
            {
                throw new CnpOnlineException("Error validating xml data against the schema", ioe);
            }
        }

        private async Task<T> SendRequestAsync<T>(Func<cnpOnlineResponse, T> getResponse, transactionRequest transaction, CancellationToken cancellationToken)
        {
            var request = CreateRequest(transaction);

            cnpOnlineResponse response = await SendToCnpAsync(request, cancellationToken).ConfigureAwait(false);
            return getResponse(response);
        }

        private async Task<cnpOnlineResponse> SendToCnpAsync(cnpOnlineRequest request, CancellationToken cancellationToken)
        {
            string xmlRequest = request.Serialize();
            string xmlResponse = await _communication.HttpPostAsync(xmlRequest, _config, cancellationToken).ConfigureAwait(false);
            return DeserializeResponse(xmlResponse);
        }

        private cnpOnlineResponse DeserializeResponse(string xmlResponse)
        {
            try
            {
                cnpOnlineResponse cnpOnlineResponse = DeserializeObject(xmlResponse);
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

        private void FillInReportGroup(transactionTypeWithReportGroup txn)
        {
            if (txn.reportGroup == null)
            {
                txn.reportGroup = _config["reportGroup"];
            }
        }

        private void FillInReportGroup(transactionTypeWithReportGroupAndPartial txn)
        {
            if (txn.reportGroup == null)
            {
                txn.reportGroup = _config["reportGroup"];
            }
        }
    }

    // CnpOnline interface for synchronous and asynchronous call.
    public interface ICnpOnline
    {
        authorizationResponse Authorize(authorization auth);
        Task<authorizationResponse> AuthorizeAsync(authorization auth, CancellationToken cancellationToken);
        authReversalResponse AuthReversal(authReversal reversal);
        Task<authReversalResponse> AuthReversalAsync(authReversal reversal, CancellationToken cancellationToken);
        captureResponse Capture(capture capture);
        Task<captureResponse> CaptureAsync(capture capture, CancellationToken cancellationToken);
        captureGivenAuthResponse CaptureGivenAuth(captureGivenAuth captureGivenAuth);
        Task<captureGivenAuthResponse> CaptureGivenAuthAsync(captureGivenAuth captureGivenAuth, CancellationToken cancellationToken);
        creditResponse Credit(credit credit);
        Task<creditResponse> CreditAsync(credit credit, CancellationToken cancellationToken);
        echeckCreditResponse EcheckCredit(echeckCredit echeckCredit);
        Task<echeckCreditResponse> EcheckCreditAsync(echeckCredit echeckCredit, CancellationToken cancellationToken);
        echeckRedepositResponse EcheckRedeposit(echeckRedeposit echeckRedeposit);
        Task<echeckRedepositResponse> EcheckRedepositAsync(echeckRedeposit echeckRedeposit, CancellationToken cancellationToken);
        echeckSalesResponse EcheckSale(echeckSale echeckSale);
        Task<echeckSalesResponse> EcheckSaleAsync(echeckSale echeckSale, CancellationToken cancellationToken);
        echeckVerificationResponse EcheckVerification(echeckVerification echeckVerification);
        Task<echeckVerificationResponse> EcheckVerificationAsync(echeckVerification echeckVerification, CancellationToken cancellationToken);
        forceCaptureResponse ForceCapture(forceCapture forceCapture);
        Task<forceCaptureResponse> ForceCaptureAsync(forceCapture forceCapture, CancellationToken cancellationToken);
        saleResponse Sale(sale sale);
        Task<saleResponse> SaleAsync(sale sale, CancellationToken cancellationToken);
        registerTokenResponse RegisterToken(registerTokenRequestType tokenRequest);
        Task<registerTokenResponse> RegisterTokenAsync(registerTokenRequestType tokenRequest, CancellationToken cancellationToken);
        cnpOnlineResponseTransactionResponseVoidResponse DoVoid(voidTxn v);
        Task<cnpOnlineResponseTransactionResponseVoidResponse> DoVoidAsync(voidTxn v, CancellationToken cancellationToken);
        cnpOnlineResponseTransactionResponseEcheckVoidResponse EcheckVoid(echeckVoid v);
        Task<cnpOnlineResponseTransactionResponseEcheckVoidResponse> EcheckVoidAsync(echeckVoid v, CancellationToken cancellationToken);
        updateCardValidationNumOnTokenResponse UpdateCardValidationNumOnToken(updateCardValidationNumOnToken update);
        Task<updateCardValidationNumOnTokenResponse> UpdateCardValidationNumOnTokenAsync(updateCardValidationNumOnToken update, CancellationToken cancellationToken);
        giftCardAuthReversalResponse GiftCardAuthReversal(giftCardAuthReversal giftCard);
        Task<giftCardAuthReversalResponse> GiftCardAuthReversalAsync(giftCardAuthReversal giftCard, CancellationToken cancellationToken);
        giftCardCaptureResponse GiftCardCapture(giftCardCapture giftCardCapture);
        Task<giftCardCaptureResponse> GiftCardCaptureAsync(giftCardCapture giftCardCapture, CancellationToken cancellationToken);
        
        payFacCreditResponse PayFacCredit(payFacCredit payFacCredit);
        Task<payFacCreditResponse> PayFacCreditAsync(payFacCredit payFacCredit, CancellationToken cancellationToken);
        payFacDebitResponse PayFacDebit(payFacDebit payFacDebit);
        Task<payFacDebitResponse> PayFacDebitAsync(payFacDebit payFacDebit, CancellationToken cancellationToken);
        
        physicalCheckCreditResponse PhysicalCheckCredit(physicalCheckCredit physicalCheckCredit);
        Task<physicalCheckCreditResponse> PhysicalCheckCreditAsync(physicalCheckCredit physicalCheckCredit, CancellationToken cancellationToken);
        physicalCheckDebitResponse PhysicalCheckDebit(physicalCheckDebit physicalCheckDebit);
        Task<physicalCheckDebitResponse> PhysicalCheckDebitAsync(physicalCheckDebit physicalCheckDebit, CancellationToken cancellationToken);
        
        reserveCreditResponse ReserveCredit(reserveCredit reserveCredit);
        Task<reserveCreditResponse> ReserveCreditAsync(reserveCredit reserveCredit, CancellationToken cancellationToken);
        reserveDebitResponse ReserveDebit(reserveDebit reserveDebit);
        Task<reserveDebitResponse> ReserveDebitAsync(reserveDebit reserveDebit, CancellationToken cancellationToken);
        
        submerchantCreditResponse SubmerchantCredit(submerchantCredit submerchantCredit);
        Task<submerchantCreditResponse> SubmerchantCreditAsync(submerchantCredit submerchantCredit, CancellationToken cancellationToken);
        submerchantDebitResponse SubmerchantDebit(submerchantDebit submerchantDebit);
        Task<submerchantDebitResponse> SubmerchantDebitAsync(submerchantDebit submerchantDebit, CancellationToken cancellationToken);
        
        vendorCreditResponse VendorCredit(vendorCredit vendorCredit);
        Task<vendorCreditResponse> VendorCreditAsync(vendorCredit vendorCredit, CancellationToken cancellationToken);
        vendorDebitResponse VendorDebit(vendorDebit vendorDebit);
        Task<vendorDebitResponse> VendorDebitAsync(vendorDebit vendorDebit, CancellationToken cancellationToken);
        
        giftCardCreditResponse GiftCardCredit(giftCardCredit giftCardCredit);
        Task<giftCardCreditResponse> GiftCardCreditAsync(giftCardCredit giftCardCredit, CancellationToken cancellationToken);
        transactionTypeWithReportGroup QueryTransaction(queryTransaction queryTransaction);
        Task<transactionTypeWithReportGroup> QueryTransactionAsync(queryTransaction queryTransaction, CancellationToken cancellationToken);
        event EventHandler HttpAction;
    }
}
