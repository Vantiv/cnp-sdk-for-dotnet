using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

namespace Cnp.Sdk
{

    // Represent an online request.
    // Defining all transactions supported for online processing.
    public class CnpOnline : ICnpOnline
    {
        // Configuration object containing credentials and settings.
        private Dictionary<string, string> _config;

        // The object used for communicating with the server
        private Communications _communication;

        // exposed merchantId for organizations with multiple Merchant Ids.
        private string _merchantId;

        /**
         * Construct a Cnp online using the configuration specified in CnpSdkForNet.dll.config
         */
        public CnpOnline() : this(new ConfigManager().getConfig()) { }

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
         * maxConnections (max amount of connections used for HTTP requests)
         *
         * NOTE: The config of the first CnpOnline object created will determine the following
         *   for the entire lifespan of the application:
         * - proxyHost
         * - proxyPort
         * - timeout
         * - maxConnections
         * These values *cannot* be changed for the lifetime of the application
         */
        public CnpOnline(Dictionary<string, string> config)
        {
            _config = config;
            SetCommunication(new Communications(_config));
        }

        public void SetCommunication(Communications communication)
        {
            _communication = communication;
        }

        public string GetMerchantId()
        {
            return _merchantId;
        }

        public void SetMerchantId(string merchantId)
        {
            _merchantId = merchantId;
        }

        public event EventHandler HttpAction
        {
            add { _communication.HttpAction += value; }
            remove { _communication.HttpAction -= value; }
        }

        public Task<authorizationResponse> AuthorizeAsync(authorization auth, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response =>
            {
                var authResponse = response.authorizationResponse;
                return authResponse;
            }, auth, cancellationToken);
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
            else if (transaction is transactionReversal)
            {
                request.transactionReversal = (transactionReversal) transaction;
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
            else if (transaction is payoutOrgCredit)
            {
                request.payoutOrgCredit = (payoutOrgCredit)transaction;
            }
            else if (transaction is payoutOrgDebit)
            {
                request.payoutOrgDebit = (payoutOrgDebit)transaction;
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
            else if (transaction is translateToLowValueTokenRequest)
            {
                request.translateToLowValueTokenRequest = (translateToLowValueTokenRequest) transaction;
            }
            else if (transaction is vendorDebit)
            {
                request.vendorDebit = (vendorDebit)transaction;
            }
            else if (transaction is customerDebit) 
            {
                request.customerDebit = (customerDebit)transaction;
            }
            else if (transaction is customerCredit)
            {
                request.customerCredit = (customerCredit)transaction;
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
            var cnpResponse =  SendRequest(response => response, auth);
            var authResponse = cnpResponse.authorizationResponse;
            return authResponse;
        }

        public authReversalResponse AuthReversal(authReversal reversal)
        {
            
            var cnpResponse =  SendRequest(response => response, reversal);
            var reversalResponse = cnpResponse.authReversalResponse;
            return reversalResponse;
        }

        public Task<authReversalResponse> AuthReversalAsync(authReversal reversal, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response =>
            {
                var authReversalResponse = response.authReversalResponse;
                return authReversalResponse;
            }, reversal, cancellationToken);
        }
        
        public transactionReversalResponse TransactionReversal(transactionReversal reversal)
        {
            
            var cnpResponse =  SendRequest(response => response, reversal);
            var reversalResponse = cnpResponse.transactionReversalResponse;
            return reversalResponse;
        }
        
        public Task<transactionReversalResponse> TransactionReversalAsync(transactionReversal reversal, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response =>
            {
                var transactionReversalResponse = response.transactionReversalResponse;
                return transactionReversalResponse;
            }, reversal, cancellationToken);
        }

        public giftCardAuthReversalResponse GiftCardAuthReversal(giftCardAuthReversal giftCard)
        {
            var cnpResponse =  SendRequest(response => response, giftCard);
            var giftCardReversalResponse = cnpResponse.giftCardAuthReversalResponse;
            return giftCardReversalResponse;
        }

        public Task<giftCardAuthReversalResponse> GiftCardAuthReversalAsync(giftCardAuthReversal giftCard, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response =>
            {
                var giftCardAuthReversalResponse = response.giftCardAuthReversalResponse;
                return giftCardAuthReversalResponse;
            }, giftCard, cancellationToken);
        }

        public Task<captureResponse> CaptureAsync(capture capture, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response =>
            {
                var captureResponse = response.captureResponse;
                return captureResponse;
            }, capture, cancellationToken);
        }

        public captureResponse Capture(capture capture)
        {
            var cnpResponse =  SendRequest(response => response, capture);
            var captureResponse = cnpResponse.captureResponse;
            return captureResponse;
        }

        public giftCardCaptureResponse GiftCardCapture(giftCardCapture giftCardCapture)
        {
            var cnpResponse =  SendRequest(response => response, giftCardCapture);
            var giftCaptureResponse = cnpResponse.giftCardCaptureResponse;
            return giftCaptureResponse;
        }

        public Task<giftCardCaptureResponse> GiftCardCaptureAsync(giftCardCapture giftCardCapture, CancellationToken cancellationToken)
        {
            
            return SendRequestAsync(response =>
            {
                var giftCardCaptureResponse = response.giftCardCaptureResponse;
                return giftCardCaptureResponse;
            }, giftCardCapture, cancellationToken);
        }

        public Task<captureGivenAuthResponse> CaptureGivenAuthAsync(captureGivenAuth captureGivenAuth, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response =>
            {
                var captureGivenAuthResponse = response.captureGivenAuthResponse;
                return response.captureGivenAuthResponse;
            }, captureGivenAuth, cancellationToken);
        }

        public captureGivenAuthResponse CaptureGivenAuth(captureGivenAuth captureGivenAuth)
        {
            var cnpResponse = SendRequest(response => response, captureGivenAuth);
            var captureAuthResponse = cnpResponse.captureGivenAuthResponse;
            return captureAuthResponse;
        }

        public creditResponse Credit(credit credit)
        {
            var cnpResponse = SendRequest(response => response, credit);
            var creditResponse = cnpResponse.creditResponse;
            return creditResponse;
        }

        public Task<creditResponse> CreditAsync(credit credit, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response =>
            {
                var creditResponse= response.creditResponse;
                return creditResponse;
            }, credit, cancellationToken);
        }

        public giftCardCreditResponse GiftCardCredit(giftCardCredit giftCardCredit)
        {
            var cnpResponse = SendRequest(response => response, giftCardCredit);
            var giftCreditResponse = cnpResponse.giftCardCreditResponse;
            return giftCreditResponse;
        }

        public Task<giftCardCreditResponse> GiftCardCreditAsync(giftCardCredit giftCardCredit, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response =>
            {
                var giftCardCreditResponse = response.giftCardCreditResponse;
                return response.giftCardCreditResponse;
            }, giftCardCredit, cancellationToken);
        }

        public Task<echeckCreditResponse> EcheckCreditAsync(echeckCredit echeckCredit, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response =>
            {
                var echeckCreditResponse = response.echeckCreditResponse;
                return response.echeckCreditResponse;
            }, echeckCredit, cancellationToken);
        }

        public echeckCreditResponse EcheckCredit(echeckCredit echeckCredit)
        {
            var cnpResponse = SendRequest(response => response, echeckCredit);
            var eCheckCreditResponse = cnpResponse.echeckCreditResponse;
            return eCheckCreditResponse;
        }

        public Task<echeckRedepositResponse> EcheckRedepositAsync(echeckRedeposit echeckRedeposit, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response =>
            {
                var echeckRedepositResponse = response.echeckRedepositResponse;
                return echeckRedepositResponse;
            }, echeckRedeposit, cancellationToken);
        }

        public echeckRedepositResponse EcheckRedeposit(echeckRedeposit echeckRedeposit)
        {
            var cnpResponse = SendRequest(response => response, echeckRedeposit);
            var eCheckRedepositResponse = cnpResponse.echeckRedepositResponse;
            return eCheckRedepositResponse;
        }

        public Task<echeckSalesResponse> EcheckSaleAsync(echeckSale echeckSale, CancellationToken cancellationToken)
        {
            
            return SendRequestAsync(response =>
            {
                var echeckSalesResponse = response.echeckSalesResponse;
                return echeckSalesResponse;
            }, echeckSale, cancellationToken);
        }

        public echeckSalesResponse EcheckSale(echeckSale echeckSale)
        {
            var cnpResponse = SendRequest(response => response, echeckSale);
            var eCheckSaleResponse = cnpResponse.echeckSalesResponse;
            return eCheckSaleResponse;
        }

        public echeckVerificationResponse EcheckVerification(echeckVerification echeckVerification)
        {
            var cnpResponse = SendRequest(response => response, echeckVerification);
            var eCheckVerificationResponse = cnpResponse.echeckVerificationResponse;
            return eCheckVerificationResponse;
        }

        public Task<echeckVerificationResponse> EcheckVerificationAsync(echeckVerification echeckVerification, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response =>
            {
                var echeckVerificationResponse = response.echeckVerificationResponse;
                return echeckVerificationResponse;
            }, echeckVerification, cancellationToken);
        }

        public forceCaptureResponse ForceCapture(forceCapture forceCapture)
        {
            var cnpResponse = SendRequest(response => response, forceCapture);
            var forceCaptureResponse = cnpResponse.forceCaptureResponse;
            return forceCaptureResponse;
        }

        public Task<forceCaptureResponse> ForceCaptureAsync(forceCapture forceCapture, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response =>
            {
                var forceCaptureResponse = response.forceCaptureResponse;
                return forceCaptureResponse;
            }, forceCapture, cancellationToken);
        }

        public saleResponse Sale(sale sale)
        {
            var cnpResponse = SendRequest(response => response, sale);
            var saleResponse = cnpResponse.saleResponse;
            return saleResponse;
        }

        public Task<saleResponse> SaleAsync(sale sale, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response =>
            {
                var saleResponse = response.saleResponse;
                return saleResponse;
            }, sale, cancellationToken);
        }

        public Task<registerTokenResponse> RegisterTokenAsync(registerTokenRequestType tokenRequest, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response =>
            {
                var registerTokenResponse = response.registerTokenResponse;
                return response.registerTokenResponse;
            }, tokenRequest, cancellationToken);
        }

        public registerTokenResponse RegisterToken(registerTokenRequestType tokenRequest)
        {
            var cnpResponse = SendRequest(response => response, tokenRequest);
            var tokenResponse = cnpResponse.registerTokenResponse;
            return tokenResponse;
        }

        public voidResponse DoVoid(voidTxn v)
        {
            var cnpResponse = SendRequest(response => response, v);
            var voidResponse = cnpResponse.voidResponse;
            return voidResponse;
        }

        public Task<voidResponse> DoVoidAsync(voidTxn v, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response =>
            { 
                var voidResponse = response.voidResponse;
                return voidResponse;
            }, v, cancellationToken);
        }

        public echeckVoidResponse EcheckVoid(echeckVoid v)
        {
            var cnpResponse = SendRequest(response => response, v);
            var eCheckVoidResponse = cnpResponse.echeckVoidResponse;
            return eCheckVoidResponse;
        }

        public Task<echeckVoidResponse> EcheckVoidAsync(echeckVoid v, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response =>
            {
                var echeckVoidResponse = response.echeckVoidResponse;
                return echeckVoidResponse;
            }, v, cancellationToken);
        }

        public updateCardValidationNumOnTokenResponse UpdateCardValidationNumOnToken(updateCardValidationNumOnToken updateCardValidationNumOnToken)
        {
            return SendRequest(response =>
            {
                var updateCardValidationNumOnTokenResponse = response.updateCardValidationNumOnTokenResponse;
                return updateCardValidationNumOnTokenResponse;
            }, updateCardValidationNumOnToken);
        }

        public Task<updateCardValidationNumOnTokenResponse> UpdateCardValidationNumOnTokenAsync(updateCardValidationNumOnToken update, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response =>
            {
                var updateCardValidationNumOnTokenResponse = response.updateCardValidationNumOnTokenResponse;
                return updateCardValidationNumOnTokenResponse;
            }, update, cancellationToken);
        }

        public cancelSubscriptionResponse CancelSubscription(cancelSubscription cancelSubscription)
        {
            var cnpResponse = SendRequest(response => response, cancelSubscription);
            var cancelSubscriptionResponse = cnpResponse.cancelSubscriptionResponse;
            return cancelSubscriptionResponse;
        }

        public updateSubscriptionResponse UpdateSubscription(updateSubscription updateSubscription)
        {
            var cnpResponse = SendRequest(response => response, updateSubscription);
            var updateSubscriptionResponse = cnpResponse.updateSubscriptionResponse;
            return updateSubscriptionResponse;
        }

        public activateResponse Activate(activate activate)
        {
            var cnpResponse = SendRequest(response => response, activate);
            var activatationResponse = cnpResponse.activateResponse;
            return activatationResponse;
        }

        public deactivateResponse Deactivate(deactivate deactivate)
        {
            var cnpResponse = SendRequest(response => response, deactivate);
            var deactivationResponse = cnpResponse.deactivateResponse;
            return deactivationResponse;
        }

        public loadResponse Load(load load)
        {
            var cnpResponse = SendRequest(response => response, load);
            var loadResponse = cnpResponse.loadResponse;
            return loadResponse;
        }

        public unloadResponse Unload(unload unload)
        {
            var cnpResponse = SendRequest(response => response, unload);
            var unloadResponse = cnpResponse.unloadResponse;
            return unloadResponse;
        }

        public balanceInquiryResponse BalanceInquiry(balanceInquiry balanceInquiry)
        {
            var cnpResponse = SendRequest(response => response, balanceInquiry);
            var balanceInquiryResponse = cnpResponse.balanceInquiryResponse;
            return balanceInquiryResponse;
        }

        public Task<balanceInquiryResponse> BalanceInquiryAsync(balanceInquiry balanceInquiry, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response =>
            {
                var balanceInquiryResponse = response.balanceInquiryResponse;
                return balanceInquiryResponse;
            }, balanceInquiry, cancellationToken);
        }

        public createPlanResponse CreatePlan(createPlan createPlan)
        {
            var cnpResponse = SendRequest(response => response, createPlan);
            var createPlanResponse = cnpResponse.createPlanResponse;
            return createPlanResponse;
        }

        public updatePlanResponse UpdatePlan(updatePlan updatePlan)
        {
            var cnpResponse = SendRequest(response => response, updatePlan);
            var updatePlanResponse = cnpResponse.updatePlanResponse;
            return updatePlanResponse;
        }

        public refundReversalResponse RefundReversal(refundReversal refundReversal)
        {
            var cnpResponse = SendRequest(response => response, refundReversal);
            var refundReversalResponse = cnpResponse.refundReversalResponse;
            return refundReversalResponse;
        }

        public depositReversalResponse DepositReversal(depositReversal depositReversal)
        {
            var cnpResponse = SendRequest(response => response, depositReversal);
            var depositReversalResponse = cnpResponse.depositReversalResponse;
            return depositReversalResponse;
        }

        public activateReversalResponse ActivateReversal(activateReversal activateReversal)
        {
            var cnpResponse = SendRequest(response => response, activateReversal);
            var activateReversalResponse = cnpResponse.activateReversalResponse;
            return activateReversalResponse;
        }

        public deactivateReversalResponse DeactivateReversal(deactivateReversal deactivateReversal)
        {
            var cnpResponse = SendRequest(response => response, deactivateReversal);
            var deactivateReversalResponse = cnpResponse.deactivateReversalResponse;
            return deactivateReversalResponse;
        }

        public loadReversalResponse LoadReversal(loadReversal loadReversal)
        {
            var cnpResponse = SendRequest(response => response, loadReversal);
            var loadReversalResponse = cnpResponse.loadReversalResponse;
            return loadReversalResponse;
        }

        public unloadReversalResponse UnloadReversal(unloadReversal unloadReversal)
        {
            var cnpResponse = SendRequest(response => response, unloadReversal);
            var unloadReversalResponse = cnpResponse.unloadReversalResponse;
            return unloadReversalResponse;
        }

        public Task<transactionTypeWithReportGroup> QueryTransactionAsync(queryTransaction queryTransaction, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response =>
            {
                var res = response.queryTransactionResponse ??
                        (transactionTypeWithReportGroup) response.queryTransactionUnavailableResponse;
                return res;
            }, queryTransaction, cancellationToken);
        }

        public transactionTypeWithReportGroup QueryTransaction(queryTransaction queryTransaction)
        {
            var cnpResponse = SendRequest(response => response, queryTransaction);
            var transactionResponse = cnpResponse.queryTransactionResponse ??
                                      (transactionTypeWithReportGroup) cnpResponse.queryTransactionUnavailableResponse;
            return transactionResponse;
        }

        public fraudCheckResponse FraudCheck(fraudCheck fraudCheck)
        {
            var cnpResponse = SendRequest(response => response, fraudCheck);
            var fraudCheckResponse = cnpResponse.fraudCheckResponse;
            return fraudCheckResponse;
        }

        public fastAccessFundingResponse FastAccessFunding(fastAccessFunding fastAccessFunding)
        {
            var cnpResponse = SendRequest(response => response, fastAccessFunding);
            var fastAccessFundingResponse = cnpResponse.fastAccessFundingResponse;
            return fastAccessFundingResponse;
        }
        
        public payFacCreditResponse PayFacCredit(payFacCredit payFacCredit)
        {
            var cnpResponse = SendRequest(response => response, payFacCredit);
            var payFacCreditResponse = cnpResponse.payFacCreditResponse;
            return payFacCreditResponse;
        }

        public Task<payFacCreditResponse> PayFacCreditAsync(payFacCredit payFacCredit, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response =>
            {
                var payFacCreditResponse = response.payFacCreditResponse;
                return payFacCreditResponse;
            }, payFacCredit, cancellationToken);
        }

        public payFacDebitResponse PayFacDebit(payFacDebit payFacDebit)
        {
            var cnpResponse = SendRequest(response => response, payFacDebit);
            var payfacDebitResponse = cnpResponse.payFacDebitResponse;
            return payfacDebitResponse;
        }

        public Task<payFacDebitResponse> PayFacDebitAsync(payFacDebit payFacDebit, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response =>
            {
                var payFacDebitResponse  = response.payFacDebitResponse;
                return payFacDebitResponse;
            }, payFacDebit, cancellationToken);
        }

        public physicalCheckCreditResponse PhysicalCheckCredit(physicalCheckCredit physicalCheckCredit)
        {
            var cnpResponse = SendRequest(response => response, physicalCheckCredit);
            var physicalCheckCreditResponse = cnpResponse.physicalCheckCreditResponse;
            return physicalCheckCreditResponse;
        }

        public Task<physicalCheckCreditResponse> PhysicalCheckCreditAsync(physicalCheckCredit physicalCheckCredit, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response =>
            {
                var physicalCheckCreditResponse = response.physicalCheckCreditResponse;
                return physicalCheckCreditResponse;
            }, physicalCheckCredit, cancellationToken);
        }

        public physicalCheckDebitResponse PhysicalCheckDebit(physicalCheckDebit physicalCheckDebit)
        {
            var cnpResponse = SendRequest(response => response, physicalCheckDebit);
            var physicalCheckDebitResponse = cnpResponse.physicalCheckDebitResponse;
            return physicalCheckDebitResponse;
        }

        public Task<physicalCheckDebitResponse> PhysicalCheckDebitAsync(physicalCheckDebit physicalCheckDebit, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response =>
            {
                var physicalCheckDebitResponse = response.physicalCheckDebitResponse;
                return physicalCheckDebitResponse;
            }, physicalCheckDebit, cancellationToken);
        }

        public payoutOrgCreditResponse PayoutOrgCredit(payoutOrgCredit payoutOrgCredit)
        {
            var cnpResponse = SendRequest(response => response, payoutOrgCredit);
            var payoutOrgCreditResponse = cnpResponse.payoutOrgCreditResponse;
            return payoutOrgCreditResponse;
        }

        public Task<payoutOrgCreditResponse> PayoutOrgCreditAsync(payoutOrgCredit payoutOrgCredit, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response =>
            {
                var payoutOrgCreditResponse = response.payoutOrgCreditResponse;
                return payoutOrgCreditResponse;
            }, payoutOrgCredit, cancellationToken);
        }

        public payoutOrgDebitResponse PayoutOrgDebit(payoutOrgDebit payoutOrgDebit)
        {
            var cnpResponse = SendRequest(response => response, payoutOrgDebit);
            var payoutOrgDebitResponse = cnpResponse.payoutOrgDebitResponse;
            return payoutOrgDebitResponse;
        }

        public Task<payoutOrgDebitResponse> PayoutOrgDebitAsync(payoutOrgDebit payoutOrgDebit, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response =>
            {
                var payoutOrgDebitResponse = response.payoutOrgDebitResponse;
                return payoutOrgDebitResponse;
            }, payoutOrgDebit, cancellationToken);
        }

        public reserveCreditResponse ReserveCredit(reserveCredit reserveCredit)
        {
            var cnpResponse = SendRequest(response => response, reserveCredit);
            var reserveCreditResponse = cnpResponse.reserveCreditResponse;
            return reserveCreditResponse;
        }

        public Task<reserveCreditResponse> ReserveCreditAsync(reserveCredit reserveCredit, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response =>
            {
                var reserveCreditResponse = response.reserveCreditResponse;
                return response.reserveCreditResponse;
            }, reserveCredit, cancellationToken);
        }

        public reserveDebitResponse ReserveDebit(reserveDebit reserveDebit)
        {
            var cnpResponse = SendRequest(response => response, reserveDebit);
            var reserveDebitResponse = cnpResponse.reserveDebitResponse;
            return reserveDebitResponse;
        }

        public Task<reserveDebitResponse> ReserveDebitAsync(reserveDebit reserveDebit, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response =>
            {
                var reserveDebitResponse = response.reserveDebitResponse;
                return reserveDebitResponse;
            }, reserveDebit, cancellationToken);
        }

        public submerchantCreditResponse SubmerchantCredit(submerchantCredit submerchantCredit)
        {
            var cnpResponse = SendRequest(response => response, submerchantCredit);
            var submerchantCreditResponse = cnpResponse.submerchantCreditResponse;
            return submerchantCreditResponse;
        }

        public Task<submerchantCreditResponse> SubmerchantCreditAsync(submerchantCredit submerchantCredit, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response =>
            {
                var submerchantCreditResponse = response.submerchantCreditResponse;
                return submerchantCreditResponse;
            }, submerchantCredit, cancellationToken);
        }

        public submerchantDebitResponse SubmerchantDebit(submerchantDebit submerchantDebit)
        {
            var cnpResponse = SendRequest(response => response, submerchantDebit);
            var submerchantDebitResponse = cnpResponse.submerchantDebitResponse;
            return submerchantDebitResponse;
        }

        public Task<submerchantDebitResponse> SubmerchantDebitAsync(submerchantDebit submerchantDebit, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response =>
            {
                var submerchantDebitResponse = response.submerchantDebitResponse;
                return submerchantDebitResponse;
            }, submerchantDebit, cancellationToken);
        }

        public vendorCreditResponse VendorCredit(vendorCredit vendorCredit)
        {
            var cnpResponse = SendRequest(response => response, vendorCredit);
            var vendorCreditResponse = cnpResponse.vendorCreditResponse;
            return vendorCreditResponse;
        }

        public Task<vendorCreditResponse> VendorCreditAsync(vendorCredit vendorCredit, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response =>
            {
                var vendorCreditResponse = response.vendorCreditResponse;
                return vendorCreditResponse;
            }, vendorCredit, cancellationToken);
        }

        public customerCreditResponse CustomerCredit(customerCredit customerCredit)
        {
            var cnpResponse = SendRequest(response => response, customerCredit);
            var customerCreditResponse = cnpResponse.customerCreditResponse;
            return customerCreditResponse;
        }

        public Task<customerCreditResponse> CustomerCreditAsync(customerCredit customerCredit, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response =>
            {
                var customerCreditResponse = response.customerCreditResponse;
                return customerCreditResponse;
            }, customerCredit, cancellationToken);
        }

        public translateToLowValueTokenResponse TranslateToLowValueTokenRequest(translateToLowValueTokenRequest translateToLowValueTokenRequest)
        {
            var cnpResponse = SendRequest(response => response, translateToLowValueTokenRequest);
            var translateToLowValueTokenResponse = cnpResponse.translateToLowValueTokenResponse;
            return translateToLowValueTokenResponse;
        }

        public Task<translateToLowValueTokenResponse> TranslateToLowValueTokenRequestAsync(translateToLowValueTokenRequest translateToLowValueTokenRequest, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response =>
            {
                var translateToLowValueTokenResponse = response.translateToLowValueTokenResponse;
                return translateToLowValueTokenResponse;
            }, translateToLowValueTokenRequest, cancellationToken);
        }

        public vendorDebitResponse VendorDebit(vendorDebit vendorDebit)
        {
            var cnpResponse = SendRequest(response => response, vendorDebit);
            var vendorDebitResponse = cnpResponse.vendorDebitResponse;
            return vendorDebitResponse;
        }

        public Task<vendorDebitResponse> VendorDebitAsync(vendorDebit vendorDebit, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response =>
            {
                var vendorDebitResponse = response.vendorDebitResponse;
                return vendorDebitResponse;
            }, vendorDebit, cancellationToken);
        }

        public customerDebitResponse CustomerDebit(customerDebit customerDebit)
        {
            var cnpResponse = SendRequest(response => response, customerDebit);
            var customerDebitResponse = cnpResponse.customerDebitResponse;
            return customerDebitResponse;
        }

        public Task<customerDebitResponse> CustomerDebitAsync(customerDebit customerDebit, CancellationToken cancellationToken)
        {
            return SendRequestAsync(response =>
            {
                var customerDebitResponse = response.customerDebitResponse;
                return customerDebitResponse;
            }, customerDebit, cancellationToken);
        }


        private cnpOnlineRequest CreateCnpOnlineRequest()
        {
            var request = new cnpOnlineRequest();
            request.merchantSdk = "DotNet;" + CnpVersion.CurrentCNPSDKVersion;
            request.merchantId = _merchantId ?? _config["merchantId"];
            var authentication = new authentication();
            authentication.password = _config["password"];
            authentication.user = _config["username"];
            request.authentication = authentication;
            return request;
        }

        private cnpOnlineResponse SendToCnp(cnpOnlineRequest request)
        {
            var xmlRequest = request.Serialize();
            var xmlResponse = _communication.HttpPost(xmlRequest);
            if (xmlResponse == null)
            {
                throw new WebException("Could not retrieve response from server for given request");
            }
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
            string xmlResponse = await _communication.HttpPostAsync(xmlRequest, cancellationToken).ConfigureAwait(false);
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
        voidResponse DoVoid(voidTxn v);
        Task<voidResponse> DoVoidAsync(voidTxn v, CancellationToken cancellationToken);
        echeckVoidResponse EcheckVoid(echeckVoid v);
        Task<echeckVoidResponse> EcheckVoidAsync(echeckVoid v, CancellationToken cancellationToken);
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

        payoutOrgCreditResponse PayoutOrgCredit(payoutOrgCredit payoutOrgCredit);
        Task<payoutOrgCreditResponse> PayoutOrgCreditAsync(payoutOrgCredit payoutOrgCredit, CancellationToken cancellationToken);
        payoutOrgDebitResponse PayoutOrgDebit(payoutOrgDebit payoutOrgDebit);
        Task<payoutOrgDebitResponse> PayoutOrgDebitAsync(payoutOrgDebit payoutOrgDebit, CancellationToken cancellationToken);
        

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

        customerCreditResponse CustomerCredit(customerCredit customerCredit);
        Task<customerCreditResponse> CustomerCreditAsync(customerCredit customerCredit, CancellationToken cancellationToken);
        customerDebitResponse CustomerDebit(customerDebit customerDebit);
        Task<customerDebitResponse> CustomerDebitAsync(customerDebit customerDebit, CancellationToken cancellationToken);

        giftCardCreditResponse GiftCardCredit(giftCardCredit giftCardCredit);
        Task<giftCardCreditResponse> GiftCardCreditAsync(giftCardCredit giftCardCredit, CancellationToken cancellationToken);
        transactionTypeWithReportGroup QueryTransaction(queryTransaction queryTransaction);
        Task<transactionTypeWithReportGroup> QueryTransactionAsync(queryTransaction queryTransaction, CancellationToken cancellationToken);

        translateToLowValueTokenResponse TranslateToLowValueTokenRequest(translateToLowValueTokenRequest translateToLowValueTokenRequest);
        Task<translateToLowValueTokenResponse> TranslateToLowValueTokenRequestAsync(translateToLowValueTokenRequest translateToLowValueTokenRequest, CancellationToken cancellationToken);


        event EventHandler HttpAction;
    }
}
