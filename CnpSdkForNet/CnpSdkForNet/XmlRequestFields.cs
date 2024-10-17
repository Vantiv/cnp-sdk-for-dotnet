using System;
using System.Collections.Generic;
using System.Text;
using System.Security;
using System.Xml.Serialization;

namespace Cnp.Sdk
{
    // Represents cnpOnlineRequest object.
    // A cnpOnlineRequest can contain only one transaction.
    public partial class cnpOnlineRequest
    {

        public activate activate;
        public activateReversal activateReversal;
        public authentication authentication;
        public authorization authorization;
        public authReversal authReversal;
        public balanceInquiry balanceInquiry;
        public BNPLAuthorizationRequest BNPLAuthorizationRequest;
        public BNPLCancelRequest BNPLCancelRequest;
        public BNPLCaptureRequest BNPLCaptureRequest;
        public BNPLRefundRequest BNPLRefundRequest;
        public BNPLInquiryRequest BNPLInquiryRequest;
        public cancelSubscription cancelSubscription;
        public capture capture;
        public captureGivenAuth captureGivenAuth;
        public createPlan createPlan;
        public credit credit;
        public deactivate deactivate;
        public deactivateReversal deactivateReversal;
        public depositReversal depositReversal;
        public echeckCredit echeckCredit;
        public echeckRedeposit echeckRedeposit;
        public echeckSale echeckSale;
        public echeckVerification echeckVerification;
        public echeckVoid echeckVoid;
        public EncryptionKeyRequest encryptionKeyRequest;
        public fastAccessFunding fastAccessFunding;
        public finicityUrlRequest finicityUrlRequest;
        public finicityAccountRequest finicityAccountRequest;
        public forceCapture forceCapture;
        public fraudCheck fraudCheck;
        public giftCardAuthReversal giftCardAuthReversal;
        public giftCardCapture giftCardCapture;
        public giftCardCredit giftCardCredit;
        public load load;
        public loadReversal loadReversal;
        public payFacCredit payFacCredit;
        public payFacDebit payFacDebit;
        public physicalCheckCredit physicalCheckCredit;
        public physicalCheckDebit physicalCheckDebit;
        public payoutOrgCredit payoutOrgCredit;
        public payoutOrgDebit payoutOrgDebit;
        public reserveCredit reserveCredit;
        public reserveDebit reserveDebit;
        public vendorCredit vendorCredit;
        public vendorDebit vendorDebit;
        public customerCredit customerCredit;
        public customerDebit customerDebit;
        public submerchantCredit submerchantCredit;
        public submerchantDebit submerchantDebit;
        public queryTransaction queryTransaction;
        public refundReversal refundReversal;
        public depositTransactionReversal depositTransactionReversal;
        public refundTransactionReversal refundTransactionReversal;
        public registerTokenRequestType registerTokenRequest;
        public sale sale;
        public string merchantId;
        public string merchantSdk;
        public unload unload;
        public unloadReversal unloadReversal;
        public updateCardValidationNumOnToken updateCardValidationNumOnToken;
        public updatePlan updatePlan;
        public updateSubscription updateSubscription;
        public voidTxn voidTxn;
        public translateToLowValueTokenRequest translateToLowValueTokenRequest;

        // Serialize the cnpOnlineRequest.
        // Convert the cnpOnlineRequest object to xml string.
        public string Serialize()
        {
            // Create header for the cnpOnlineRequest with user credential.
            var xml = "<?xml version='1.0' encoding='utf-8'?>\r\n<cnpOnlineRequest merchantId=\"" + merchantId
                + "\" version=\"" + CnpVersion.CurrentCNPXMLVersion + "\" merchantSdk=\"" + merchantSdk + "\" xmlns=\"http://www.vantivcnp.com/schema\">"
                + authentication.Serialize();

            // Because an online request can contain only one transaction, it assumes that only one instance variable of 
            // this cnpOnlineRequest is not null, and the rest are null.
            if (authorization != null) xml += authorization.Serialize();
            else if (capture != null) xml += capture.Serialize();
            else if (credit != null) xml += credit.Serialize();
            else if (voidTxn != null) xml += voidTxn.Serialize();
            else if (sale != null) xml += sale.Serialize();
            else if (authReversal != null) xml += authReversal.Serialize();
            else if (BNPLAuthorizationRequest != null) xml += BNPLAuthorizationRequest.Serialize();
            else if (echeckCredit != null) xml += echeckCredit.Serialize();
            else if (echeckVerification != null) xml += echeckVerification.Serialize();
            else if (echeckSale != null) xml += echeckSale.Serialize();
            else if (registerTokenRequest != null) xml += registerTokenRequest.Serialize();
            else if (forceCapture != null) xml += forceCapture.Serialize();
            else if (captureGivenAuth != null) xml += captureGivenAuth.Serialize();
            else if (echeckRedeposit != null) xml += echeckRedeposit.Serialize();
            else if (echeckVoid != null) xml += echeckVoid.Serialize();
            else if (updateCardValidationNumOnToken != null) xml += updateCardValidationNumOnToken.Serialize();
            else if (updateSubscription != null) xml += updateSubscription.Serialize();
            else if (cancelSubscription != null) xml += cancelSubscription.Serialize();
            else if (activate != null) xml += activate.Serialize();
            else if (deactivate != null) xml += deactivate.Serialize();
            else if (load != null) xml += load.Serialize();
            else if (unload != null) xml += unload.Serialize();
            else if (balanceInquiry != null) xml += balanceInquiry.Serialize();
            else if (createPlan != null) xml += createPlan.Serialize();
            else if (updatePlan != null) xml += updatePlan.Serialize();
            else if (refundReversal != null) xml += refundReversal.Serialize();
            else if (loadReversal != null) xml += loadReversal.Serialize();
            else if (depositReversal != null) xml += depositReversal.Serialize();
            else if (activateReversal != null) xml += activateReversal.Serialize();
            else if (deactivateReversal != null) xml += deactivateReversal.Serialize();
            else if (unloadReversal != null) xml += unloadReversal.Serialize();
            else if (queryTransaction != null) xml += queryTransaction.Serialize();
            else if (fraudCheck != null) xml += fraudCheck.Serialize();
            else if (giftCardAuthReversal != null) xml += giftCardAuthReversal.Serialize();
            else if (giftCardCapture != null) xml += giftCardCapture.Serialize();
            else if (giftCardCredit != null) xml += giftCardCredit.Serialize();
            else if (fastAccessFunding != null) xml += fastAccessFunding.Serialize();
            else if (payFacCredit != null) xml += payFacCredit.Serialize();
            else if (payFacDebit != null) xml += payFacDebit.Serialize();
            else if (physicalCheckCredit != null) xml += physicalCheckCredit.Serialize();
            else if (physicalCheckDebit != null) xml += physicalCheckDebit.Serialize();
            else if (reserveCredit != null) xml += reserveCredit.Serialize();
            else if (reserveDebit != null) xml += reserveDebit.Serialize();
            else if (submerchantCredit != null) xml += submerchantCredit.Serialize();
            else if (submerchantDebit != null) xml += submerchantDebit.Serialize();
            else if (vendorCredit != null) xml += vendorCredit.Serialize();
            else if (vendorDebit != null) xml += vendorDebit.Serialize();
            else if (customerCredit != null) xml += customerCredit.Serialize();
            else if (customerDebit != null) xml += customerDebit.Serialize();
            else if (payoutOrgCredit != null) xml += payoutOrgCredit.Serialize();
            else if (payoutOrgDebit != null) xml += payoutOrgDebit.Serialize();
            else if (translateToLowValueTokenRequest != null) xml += translateToLowValueTokenRequest.Serialize();
            else if (depositTransactionReversal != null) xml += depositTransactionReversal.Serialize();
            else if (refundTransactionReversal != null) xml += refundTransactionReversal.Serialize();
            else if (finicityUrlRequest != null) xml += finicityUrlRequest.Serialize();
            else if (finicityAccountRequest != null) xml += finicityAccountRequest.Serialize();
            else if (BNPLCaptureRequest != null) xml += BNPLCaptureRequest.Serialize();
            else if (BNPLRefundRequest != null) xml += BNPLRefundRequest.Serialize();
            else if (BNPLCancelRequest != null) xml += BNPLCancelRequest.Serialize();
            else if (BNPLInquiryRequest != null) xml += BNPLInquiryRequest.Serialize();
            else if (encryptionKeyRequest != null) xml += encryptionKeyRequest.Serialize();
            xml += "\r\n</cnpOnlineRequest>";

            return xml;
        }
    }

    // Authentication for requests.
    public partial class authentication
    {
        public string user;
        public string password;
        public string Serialize()
        {
            return "\r\n<authentication>\r\n<user>" + SecurityElement.Escape(user) + "</user>\r\n<password>" + SecurityElement.Escape(password) + "</password>\r\n</authentication>";
        }
    }

    #region Supported Transactions.

    // Activate Transaction.
    public partial class activate : transactionTypeWithReportGroup
    {
        public string orderId;
        public long amount;
        public orderSourceType orderSource;
        public giftCardCardType card;
        public virtualGiftCardType virtualGiftCard;

        public override string Serialize()
        {
            var xml = "\r\n<activate";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            xml += "\r\n<orderId>" + SecurityElement.Escape(orderId) + "</orderId>";
            xml += "\r\n<amount>" + amount + "</amount>";
            xml += "\r\n<orderSource>" + orderSource.Serialize() + "</orderSource>";
            if (card != null) xml += "\r\n<card>" + card.Serialize() + "\r\n</card>";
            else if (virtualGiftCard != null) xml += "\r\n<virtualGiftCard>" + virtualGiftCard.Serialize() + "\r\n</virtualGiftCard>";
            xml += "\r\n</activate>";
            return xml;
        }
    }

    // Activate Reversal Transaction.
    public partial class activateReversal : transactionTypeWithReportGroup
    {
        private long cnpTxnIdField;
        private bool cnpTxnIdSet;
        public long cnpTxnId
        {
            get
            {
                return cnpTxnIdField;
            }
            set
            {
                cnpTxnIdField = value;
                cnpTxnIdSet = true;
            }
        }
        private string virtualGiftCardBinField;
        private bool virtualGiftCardBinSet;
        public string virtualGiftCardBin
        {
            get
            {
                return virtualGiftCardBinField;
            }
            set
            {
                virtualGiftCardBinField = value;
                virtualGiftCardBinSet = true;
            }
        }
        public giftCardCardType card;
        public string originalRefCode;
        public long originalAmount;
        public DateTime originalTxnTime;
        public int originalSystemTraceId;
        public string originalSequenceNumber;

        public override string Serialize()
        {
            var xml = "\r\n<activateReversal";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (cnpTxnIdSet)
            {
                xml += "\r\n<cnpTxnId>" + cnpTxnId + "</cnpTxnId>";
            }
            if (virtualGiftCardBinSet)
            {
                xml += "\r\n<virtualGiftCardBin>" + virtualGiftCardBin + "</virtualGiftCardBin>";
            }
            xml += "\r\n<card>" + card.Serialize() + "\r\n</card>";
            xml += "\r\n<originalRefCode>" + originalRefCode + "</originalRefCode>";
            xml += "\r\n<originalAmount>" + originalAmount + "</originalAmount>";
            xml += "\r\n<originalTxnTime>" + originalTxnTime.ToString("yyyy-MM-ddTHH:mm:ssZ") + "</originalTxnTime>";
            xml += "\r\n<originalSystemTraceId>" + originalSystemTraceId + "</originalSystemTraceId>";
            xml += "\r\n<originalSequenceNumber>" + originalSequenceNumber + "</originalSequenceNumber>";
            xml += "\r\n</activateReversal>";
            return xml;
        }
    }

    // Authorization Transaction.
    public partial class authorization : transactionTypeWithReportGroup
    {

        private long cnpTxnIdField;
        private bool cnpTxnIdSet;
        public long cnpTxnId
        {
            get
            {
                return cnpTxnIdField;
            }
            set
            {
                cnpTxnIdField = value;
                cnpTxnIdSet = true;
            }
        }
        public string orderId;
        public long amount;
        private bool secondaryAmountSet;
        private long secondaryAmountField;
        public long secondaryAmount
        {
            get { return secondaryAmountField; }
            set { secondaryAmountField = value; secondaryAmountSet = true; }
        }
        private bool surchargeAmountSet;
        private long surchargeAmountField;
        public long surchargeAmount
        {
            get { return surchargeAmountField; }
            set { surchargeAmountField = value; surchargeAmountSet = true; }
        }
        public orderSourceType orderSource;
        public customerInfo customerInfo;
        public sellerInfo sellerInfo;//12.29
        public contact billToAddress;
        public contact retailerAddress;///12.24
        public additionalCOFData additionalCOFData;
        public contact shipToAddress;
        public cardType card;
        public mposType mpos;
        public payPal paypal;
        public cardTokenType token;
        public cardPaypageType paypage;
        public applepayType applepay;
        public billMeLaterRequest billMeLaterRequest;
        public fraudCheckType cardholderAuthentication;
        public processingInstructions processingInstructions;
        public pos pos;
        public customBilling customBilling;
        private govtTaxTypeEnum taxTypeField;
        private bool taxTypeSet;
        public govtTaxTypeEnum taxType
        {
            get { return taxTypeField; }
            set { taxTypeField = value; taxTypeSet = true; }
        }
        
        
        private businessIndicatorEnum businessIndicatorField;
        private bool businessIndicatorSet;
        public businessIndicatorEnum businessIndicator
        {
            get { return businessIndicatorField; }
            set { businessIndicatorField = value; businessIndicatorSet = true; }
        }
        
        
        
        private processingType processingTypeField;
        private bool processingTypeSet;
        public processingType processingType
        {
            get { return processingTypeField; }
            set { processingTypeField = value; processingTypeSet = true; }
        }
        public enhancedData enhancedData;
        public amexAggregatorData amexAggregatorData;
        private bool allowPartialAuthField;
        private bool allowPartialAuthSet;
        public bool allowPartialAuth
        {
            get
            {
                return allowPartialAuthField;
            }
            set
            {
                allowPartialAuthField = value;
                allowPartialAuthSet = true;
            }
        }
        public healthcareIIAS healthcareIIAS;
        public lodgingInfo lodgingInfo;
        public filteringType filtering;
        public merchantDataType merchantData;
        public recyclingRequestType recyclingRequest;
        private bool fraudFilterOverrideField;
        private bool fraudFilterOverrideSet;
        public bool fraudFilterOverride
        {
            get
            {
                return fraudFilterOverrideField;
            }
            set
            {
                fraudFilterOverrideField = value;
                fraudFilterOverrideSet = true;
            }
        }
        public recurringRequest recurringRequest;
        private bool debtRepaymentField;
        private bool debtRepaymentSet;
        public bool debtRepayment
        {
            get
            {
                return debtRepaymentField;
            }
            set
            {
                debtRepaymentField = value;
                debtRepaymentSet = true;
            }
        }
        public advancedFraudChecksType advancedFraudChecks;
        public wallet wallet;
        private string originalNetworkTransactionIdField;
        private bool originalNetworkTransactionIdSet;
        public string originalNetworkTransactionId
        {
            get
            {
                return originalNetworkTransactionIdField;
            }
            set
            {
                originalNetworkTransactionIdField = value;
                originalNetworkTransactionIdSet = true;
            }
        }
        private long originalTransactionAmountField;
        private bool originalTransactionAmountSet;
        public long originalTransactionAmount
        {
            get
            {
                return originalTransactionAmountField;
            }
            set
            {
                originalTransactionAmountField = value;
                originalTransactionAmountSet = true;
            }
        }
        ///12.24 
        private orderChannelEnum orderChannelField;
        private bool orderChannelSet;
        public orderChannelEnum orderChannel
        {
            get
            {
                return orderChannelField;
            }
            set
            {
                orderChannelField = value;
                orderChannelSet = true;
            }
        }
        private string fraudCheckStatusField;
        private bool fraudCheckStatusSet;
        public string fraudCheckStatus
        {
            get
            {
                return fraudCheckStatusField;
            }
            set
            {
                fraudCheckStatusField = value;
                fraudCheckStatusSet = true;
            }
        }
        private bool cryptoField;
        private bool cryptoSet;
        public bool crypto
        {
            get
            {
                return cryptoField;
            }
            set
            {
                cryptoField = value;
                cryptoSet = true;
            }
        }
        ///end
        //12.25 and 12.26 start
        private string overridePolicyField;
        private bool overridePolicySet;
        public string overridePolicy
        {
            get 
            { 
                return overridePolicyField; 
            }
            set 
            { 
                overridePolicyField = value; 
                overridePolicySet = true;
            }
        }
        private string fsErrorCodeField;
        private bool fsErrorCodeSet;
        public string fsErrorCode
        {
            get 
            { 
                return fsErrorCodeField; 
            }
            set
            {
                fsErrorCodeField = value;
                fsErrorCodeSet = true;
            }
        }
        private string merchantAccountStatusField;
        private bool merchantAccountStatusSet;
        public string merchantAccountStatus
        {
            get 
            { 
                return merchantAccountStatusField;
            }
            set { merchantAccountStatusField = value;
                merchantAccountStatusSet = true;
                }
        }
        private productEnrolledEnum productEnrolledField;
        private bool productEnrolledSet;
        public productEnrolledEnum productEnrolled
        {
            get 
            { 
                return productEnrolledField; 
            }
            set
            {
                productEnrolledField = value;
                productEnrolledSet = true;
            }
        }
        private decisionPurposeEnum decisionPurposeField;
        private bool decisionPurposeSet;
        public decisionPurposeEnum decisionPurpose
        {
            get 
            { 
                return decisionPurposeField;
            }
            set
            {
                decisionPurposeField = value;
                decisionPurposeSet = true;
            }
        }
        private fraudSwitchIndicatorEnum fraudSwitchIndicatorField;
        private bool fraudSwitchIndicatorSet;
        public fraudSwitchIndicatorEnum fraudSwitchIndicator
        {
            get 
            { 
                return fraudSwitchIndicatorField; 
            }
            set
            {
                fraudSwitchIndicatorField = value;
                fraudSwitchIndicatorSet = true;
            }
        }
        public passengerTransportData passengerTransportData;
        //12.25 and 12.26 end

        //12.30 start
        private authIndicatorEnum authIndicatorField;
        private bool authIndicatorSet;
        public authIndicatorEnum authIndicator
        {
            get
            {
                return authIndicatorField;
            }
            set
            {
                authIndicatorField = value;
                authIndicatorSet = true;
            }
        }

        //12.30 end

        public bool? skipRealtimeAU;

        public string merchantCategoryCode;

        //12.35
        public accountFundingTransactionData accountFundingTransactionData;
        private fraudCheckActionEnum fraudCheckActionFeild;
        private bool fraudCheckActionSet;
        public fraudCheckActionEnum fraudCheckAction
        {
            get
            {
                return fraudCheckActionFeild;
            }
            set
            {
                fraudCheckActionFeild = value;
                fraudCheckActionSet = true;
            }
        }

        private string typeOfDigitalCurrencyField;
        private bool typeOfDigitalCurrencySet;
        public string typeOfDigitalCurrency
        {
            get
            {
                return typeOfDigitalCurrencyField;
            }
            set
            {
                typeOfDigitalCurrencyField = value;
                typeOfDigitalCurrencySet = true;
            }
        }

        private string conversionAffiliateIdField;
        private bool conversionAffiliateIdSet;
        public string conversionAffiliateId
        {
            get
            {
                return conversionAffiliateIdField;
            }
            set
            {
                conversionAffiliateIdField = value;
                conversionAffiliateIdSet = true;
            }
        }

        public override string Serialize()
        {
            var xml = "\r\n<authorization";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (cnpTxnIdSet)
            {
                xml += "\r\n<cnpTxnId>" + cnpTxnIdField + "</cnpTxnId>";
                xml += "\r\n<amount>" + amount + "</amount>";
                xml += "\r\n<authIndicator>" + authIndicatorField + "</authIndicator>";
            }
            else
            {
                xml += "\r\n<orderId>" + SecurityElement.Escape(orderId) + "</orderId>";
                xml += "\r\n<amount>" + amount + "</amount>";
                if (secondaryAmountSet) xml += "\r\n<secondaryAmount>" + secondaryAmountField + "</secondaryAmount>";
                if (surchargeAmountSet) xml += "\r\n<surchargeAmount>" + surchargeAmountField + "</surchargeAmount>";
                if (orderSource != null) xml += "\r\n<orderSource>" + orderSource.Serialize() + "</orderSource>";

                if (customerInfo != null)
                {
                    xml += "\r\n<customerInfo>" + customerInfo.Serialize() + "\r\n</customerInfo>";
                }
                if (sellerInfo != null)
                {
                    xml += "\r\n<sellerInfo>" + sellerInfo.Serialize() + "\r\n</sellerInfo>";
                }
                if (billToAddress != null)
                {
                    xml += "\r\n<billToAddress>" + billToAddress.Serialize() + "\r\n</billToAddress>";
                }
                if (retailerAddress != null)///12.24
                {
                    xml += "\r\n<retailerAddress>" + retailerAddress.Serialize() + "\r\n</retailerAddress>";
                }
                if (shipToAddress != null)
                {
                    xml += "\r\n<shipToAddress>" + shipToAddress.Serialize() + "\r\n</shipToAddress>";
                }
                if (additionalCOFData != null)///12.24
                {
                    xml += "\r\n<additionalCOFData>" + additionalCOFData.Serialize() + "\r\n</additionalCOFData>";
                }
                if (card != null)
                {
                    xml += "\r\n<card>" + card.Serialize() + "\r\n</card>";
                }
                else if (paypal != null)
                {
                    xml += "\r\n<paypal>" + paypal.Serialize() + "\r\n</paypal>";
                }
                else if (mpos != null)
                {
                    xml += "\r\n<mpos>" + mpos.Serialize() + "\r\n</mpos>";
                }
                else if (token != null)
                {
                    xml += "\r\n<token>" + token.Serialize() + "\r\n</token>";
                }
                else if (paypage != null)
                {
                    xml += "\r\n<paypage>" + paypage.Serialize() + "\r\n</paypage>";
                }
                else if (applepay != null)
                {
                    xml += "\r\n<applepay>" + applepay.Serialize() + "\r\n</applepay>";
                }
                if (billMeLaterRequest != null)
                {
                    xml += "\r\n<billMeLaterRequest>" + billMeLaterRequest.Serialize() + "\r\n</billMeLaterRequest>";
                }
                if (cardholderAuthentication != null)
                {
                    xml += "\r\n<cardholderAuthentication>" + cardholderAuthentication.Serialize() + "\r\n</cardholderAuthentication>";
                }
                if (processingInstructions != null)
                {
                    xml += "\r\n<processingInstructions>" + processingInstructions.Serialize() + "\r\n</processingInstructions>";
                }
                if (pos != null)
                {
                    xml += "\r\n<pos>" + pos.Serialize() + "\r\n</pos>";
                }
                if (customBilling != null)
                {
                    xml += "\r\n<customBilling>" + customBilling.Serialize() + "\r\n</customBilling>";
                }

                if (taxTypeSet)
                {
                    xml += "\r\n<taxType>" + taxTypeField + "</taxType>";
                }

                if (enhancedData != null)
                {
                    xml += "\r\n<enhancedData>" + enhancedData.Serialize() + "\r\n</enhancedData>";
                }
                if (amexAggregatorData != null)
                {
                    xml += "\r\n<amexAggregatorData>" + amexAggregatorData.Serialize() + "\r\n</amexAggregatorData>";
                }
                if (allowPartialAuthSet)
                {
                    xml += "\r\n<allowPartialAuth>" + allowPartialAuthField.ToString().ToLower() + "</allowPartialAuth>";
                }
                if (healthcareIIAS != null)
                {
                    xml += "\r\n<healthcareIIAS>" + healthcareIIAS.Serialize() + "\r\n</healthcareIIAS>";
                }
                if (lodgingInfo != null)
                {
                    xml += "\r\n<lodgingInfo>" + lodgingInfo.Serialize() + "\r\n</lodgingInfo>";
                }
                if (filtering != null)
                {
                    xml += "\r\n<filtering>" + filtering.Serialize() + "\r\n</filtering>";
                }
                ///12.24
                if (orderChannelSet)
                {
                    xml += "\r\n<orderChannel>" + orderChannelField + "</orderChannel>";
                }
                if (fraudCheckStatusSet)
                {
                    xml += "\r\n<fraudCheckStatus>" + fraudCheckStatusField + "</fraudCheckStatus>";
                }
                if (cryptoSet)
                {
                    xml += "\r\n<crypto>" + cryptoField.ToString().ToLower() + "</crypto>";
                }
                ///end
                //12.25 and 12.26
                if(overridePolicySet)
                {
                    xml += "\r\n<overridePolicy>" + overridePolicyField + "</overridePolicy>";
                }
                if (fsErrorCodeSet)
                {
                    xml += "\r\n<fsErrorCode>" + fsErrorCodeField + "</fsErrorCode>";
                }
                if (merchantAccountStatusSet)
                {
                    xml += "\r\n<merchantAccountStatus>" + merchantAccountStatusField + "</merchantAccountStatus>";
                }
                if (productEnrolledSet)
                {
                    xml += "\r\n<productEnrolled>" + productEnrolledField + "</productEnrolled>";
                }
                if (decisionPurposeSet)
                {
                    xml += "\r\n<decisionPurpose>" + decisionPurposeField + "</decisionPurpose>";
                }
                if (fraudSwitchIndicatorSet)
                {
                    xml += "\r\n<fraudSwitchIndicator>" + fraudSwitchIndicatorField + "</fraudSwitchIndicator>";
                }
                if (passengerTransportData != null)
                {
                    xml += "\r\n<passengerTransportData>" + passengerTransportData.Serialize() + "\r\n</passengerTransportData>";
                }
                //end

                //12.30 start

                if (authIndicatorSet)
                {
                    xml += "\r\n<authIndicator>" + authIndicatorField + "</authIndicator>";
                }
                //end 

                if (merchantData != null)
                {
                    xml += "\r\n<merchantData>" + merchantData.Serialize() + "\r\n</merchantData>";
                }
                if (recyclingRequest != null)
                {
                    xml += "\r\n<recyclingRequest>" + recyclingRequest.Serialize() + "\r\n</recyclingRequest>";
                }
                if (fraudFilterOverrideSet) xml += "\r\n<fraudFilterOverride>" + fraudFilterOverrideField.ToString().ToLower() + "</fraudFilterOverride>";
                if (recurringRequest != null)
                {
                    xml += "\r\n<recurringRequest>" + recurringRequest.Serialize() + "\r\n</recurringRequest>";
                }
                if (debtRepaymentSet) xml += "\r\n<debtRepayment>" + debtRepayment.ToString().ToLower() + "</debtRepayment>";
                if (advancedFraudChecks != null)
                {
                    xml += "\r\n<advancedFraudChecks>" + advancedFraudChecks.Serialize() + "\r\n</advancedFraudChecks>";
                }
                if (wallet != null)
                {
                    xml += "\r\n<wallet>" + wallet.Serialize() + "\r\n</wallet>";
                }
                if (processingTypeSet && processingType != processingType.undefined)
                {
                    xml += "\r\n<processingType>" + processingTypeField + "</processingType>";
                }
                if (originalNetworkTransactionIdSet)
                {
                    xml += "\r\n<originalNetworkTransactionId>" + originalNetworkTransactionId + "</originalNetworkTransactionId>";
                }
                if (originalTransactionAmountSet)
                {
                    xml += "\r\n<originalTransactionAmount>" + originalTransactionAmount + "</originalTransactionAmount>";
                }
                if (skipRealtimeAU != null) {
                    xml += "\r\n<skipRealtimeAU>" + skipRealtimeAU.ToString().ToLower() + "</skipRealtimeAU>";
                }

                if (merchantCategoryCode != null)
                {
                    xml += "\r\n<merchantCategoryCode>" + merchantCategoryCode + "</merchantCategoryCode>";
                }
                
                if (businessIndicatorSet)
                {
                    xml += "\r\n<businessIndicator>" + businessIndicatorField + "</businessIndicator>";
                }

                if (accountFundingTransactionData != null)
                {
                    xml += "\r\n<accountFundingTransactionData>" + accountFundingTransactionData.Serialize() + "\r\n</accountFundingTransactionData>";
                }
                if (fraudCheckActionSet != null)
                {
                    xml += "\r\n<fraudCheckAction>" + fraudCheckActionFeild + "</fraudCheckAction>";
                }
                if (typeOfDigitalCurrencySet)
                {
                    xml += "\r\n<typeOfDigitalCurrency>" + typeOfDigitalCurrencyField + "</typeOfDigitalCurrency>";
                }
                if (conversionAffiliateIdSet)
                {
                    xml += "\r\n<conversionAffiliateId>" + conversionAffiliateIdField + "</conversionAffiliateId>";
                }
            }

            xml += "\r\n</authorization>";
            return xml;
        }
    }

    // Authorization Reversal Transaction.
    public partial class authReversal : transactionTypeWithReportGroup
    {
        public long cnpTxnId;
        private long amountField;
        private bool amountSet;
        public long amount
        {
            get { return amountField; }
            set { amountField = value; amountSet = true; }
        }
        private bool surchargeAmountSet;
        private long surchargeAmountField;
        public long surchargeAmount
        {
            get { return surchargeAmountField; }
            set { surchargeAmountField = value; surchargeAmountSet = true; }
        }
        public string payPalNotes;
        public string actionReason;
        public additionalCOFData additionalCOFData;//12.26

        public override string Serialize()
        {
            var xml = "\r\n<authReversal";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            xml += "\r\n<cnpTxnId>" + cnpTxnId + "</cnpTxnId>";
            if (amountSet)
            {
                xml += "\r\n<amount>" + amountField + "</amount>";
            }
            if (surchargeAmountSet) xml += "\r\n<surchargeAmount>" + surchargeAmountField + "</surchargeAmount>";
            if (payPalNotes != null)
            {
                xml += "\r\n<payPalNotes>" + SecurityElement.Escape(payPalNotes) + "</payPalNotes>";
            }
            if (actionReason != null)
            {
                xml += "\r\n<actionReason>" + SecurityElement.Escape(actionReason) + "</actionReason>";
            }
            if (additionalCOFData != null)///12.26
            {
                xml += "\r\n<additionalCOFData>" + additionalCOFData.Serialize() + "\r\n</additionalCOFData>";
            }
            xml += "\r\n</authReversal>";
            return xml;
        }

    }

    // Authorization Reversal Transaction.
    public partial class depositTransactionReversal : transactionTypeWithReportGroup
    {
        public long cnpTxnId;
        private long amountField;
        private bool amountSet;
        public long amount
        {
            get { return amountField; }
            set { amountField = value; amountSet = true; }
        }

        private bool pinSet;
        private string pinField;
        public string pin
        {
            get { return pinField; }
            set
            {
                pinField = value; pinSet = true;
            }
        }

        private bool surchargeAmountIsSet;
        private int surchargeAmountField;

        public int surchargeAmount
        {
            get { return surchargeAmountField; }
            set { surchargeAmountField = value; surchargeAmountIsSet = true; }
        }

        public enhancedData enhancedData;

        private bool processingInstructionsIsSet;
        private processingInstructions processingInstructionsField;
        public processingInstructions processingInstructions
        {
            get { return processingInstructionsField; }
            set { processingInstructions = value; processingInstructionsIsSet = true; }
        }

        private bool customBillingIsSet;
        private customBilling customBillingField;

        public customBilling customBilling
        {
            get { return customBillingField; }
            set { customBillingField = value; customBillingIsSet = true; }
        }

        private bool lodgingInfoIsSet;
        private lodgingInfo lodgingInfoField;

        public lodgingInfo lodgingInfo
        {
            get { return lodgingInfoField; }
            set { lodgingInfoField = value; lodgingInfoIsSet = true; }
        }

        public passengerTransportData passengerTransportData;//12.26

        public override string Serialize()
        {
            var xml = "\r\n<depositTransactionReversal";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            xml += "\r\n<cnpTxnId>" + cnpTxnId + "</cnpTxnId>";
            if (amountSet)
            {
                xml += "\r\n<amount>" + amountField + "</amount>";
            }

            if (pinSet)
            {
                xml += "\r\n<pin>" + SecurityElement.Escape(pinField) + "</pin>";
            }

            if (surchargeAmountIsSet)
            {
                xml += "\r\n<surchargeAmount>" + surchargeAmountField + "</surchargeAmount>";
            }

            if (this.customBillingIsSet)
            {
                xml += this.customBillingField.Serialize();
            }

            if (enhancedData != null)
            {
                xml += "\r\n<enhancedData>" + enhancedData.Serialize() + "\r\n</enhancedData>";
            }

            if (this.lodgingInfoIsSet)
            {
                xml += this.lodgingInfoField.Serialize();
            }
            
            if (this.processingInstructionsIsSet)
            {
                xml += this.processingInstructionsField.Serialize();
            }
            if (passengerTransportData != null)//12.26
            {
                xml += "\r\n<passengerTransportData>" + passengerTransportData.Serialize() + "</passengerTransportData>";
            }
            xml += "\r\n</depositTransactionReversal>";
            return xml;
        }

    }

    public partial class refundTransactionReversal : transactionTypeWithReportGroup
    {
        public long cnpTxnId;
        private long amountField;
        private bool amountSet;
        public long amount
        {
            get { return amountField; }
            set { amountField = value; amountSet = true; }
        }

        private bool pinSet;
        private string pinField;
        public string pin
        {
            get { return pinField; }
            set
            {
                pinField = value; pinSet = true;
            }
        }

        private bool surchargeAmountIsSet;
        private int surchargeAmountField;

        public int surchargeAmount
        {
            get { return surchargeAmountField; }
            set { surchargeAmountField = value; surchargeAmountIsSet = true; }
        }

        public enhancedData enhancedData;
        
        private bool processingInstructionsIsSet;
        private processingInstructions processingInstructionsField;
        public processingInstructions processingInstructions
        {
            get { return processingInstructionsField; }
            set { processingInstructions = value; processingInstructionsIsSet = true; }
        }

        private bool customBillingIsSet;
        private customBilling customBillingField;

        public customBilling customBilling
        {
            get { return customBillingField; }
            set { customBillingField = value; customBillingIsSet = true; }
        }

        private bool lodgingInfoIsSet;
        private lodgingInfo lodgingInfoField;

        public lodgingInfo lodgingInfo
        {
            get { return lodgingInfoField; }
            set { lodgingInfoField = value; lodgingInfoIsSet = true; }
        }

        public passengerTransportData passengerTransportData;//12.26

        public override string Serialize()
        {
            var xml = "\r\n<refundTransactionReversal";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            xml += "\r\n<cnpTxnId>" + cnpTxnId + "</cnpTxnId>";
            if (amountSet)
            {
                xml += "\r\n<amount>" + amountField + "</amount>";
            }

            if (pinSet)
            {
                xml += "\r\n<pin>" + SecurityElement.Escape(pinField) + "</pin>";
            }

            if (surchargeAmountIsSet)
            {
                xml += "\r\n<surchargeAmount>" + surchargeAmountField + "</surchargeAmount>";
            }

            if (this.customBillingIsSet)
            {
                xml += this.customBillingField.Serialize();
            }

            if (enhancedData != null)
            {
                xml += "\r\n<enhancedData>" + enhancedData.Serialize() + "\r\n</enhancedData>";
            }

            if (this.lodgingInfoIsSet)
            {
                xml += this.lodgingInfoField.Serialize();
            }

            if (this.processingInstructionsIsSet)
            {
                xml += this.processingInstructionsField.Serialize();
            }
            if (passengerTransportData != null)//12.26
            {
                xml += "\r\n<passengerTransportData>" + passengerTransportData.Serialize() + "</passengerTransportData>";
            }
            xml += "\r\n</refundTransactionReversal>";
            return xml;
        }

    }

    // Balance Inquiry Transaction.
    public partial class balanceInquiry : transactionTypeWithReportGroup
    {
        public string orderId;
        public orderSourceType orderSource;
        public giftCardCardType card;

        public override string Serialize()
        {
            var xml = "\r\n<balanceInquiry";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            xml += "\r\n<orderId>" + SecurityElement.Escape(orderId) + "</orderId>";
            xml += "\r\n<orderSource>" + orderSource.Serialize() + "</orderSource>";
            xml += "\r\n<card>" + card.Serialize() + "\r\n</card>";
            xml += "\r\n</balanceInquiry>";
            return xml;
        }
    }

    // Cancel Subscription Transaction.
    public partial class cancelSubscription : recurringTransactionType
    {
        private long subscriptionIdField;
        private bool subscriptionIdSet;
        public long subscriptionId
        {
            get
            {
                return subscriptionIdField;
            }
            set
            {
                subscriptionIdField = value;
                subscriptionIdSet = true;
            }
        }

        public override string Serialize()
        {
            var xml = "\r\n<cancelSubscription>";
            if (subscriptionIdSet) xml += "\r\n<subscriptionId>" + subscriptionIdField + "</subscriptionId>";
            xml += "\r\n</cancelSubscription>";
            return xml;
        }
    }

    // Capture Transaction.
    public partial class capture : transactionTypeWithReportGroupAndPartial
    {
        public long cnpTxnId;
        public string orderId;
        private long amountField;
        private bool amountSet;
        public long amount
        {
            get { return amountField; }
            set { amountField = value; amountSet = true; }
        }
        private bool surchargeAmountSet;
        private long surchargeAmountField;
        public long surchargeAmount
        {
            get { return surchargeAmountField; }
            set { surchargeAmountField = value; surchargeAmountSet = true; }
        }
        public enhancedData enhancedData;
        public processingInstructions processingInstructions;
        private bool payPalOrderCompleteField;
        private bool payPalOrderCompleteSet;
        public bool payPalOrderComplete
        {
            get { return payPalOrderCompleteField; }
            set { payPalOrderCompleteField = value; payPalOrderCompleteSet = true; }
        }
        public string payPalNotes;
        public customBilling customBilling;
        public lodgingInfo lodgingInfo;
        public string pin;
        public passengerTransportData passengerTransportData;//12.26
        //12.31 start
        private foreignRetailerIndicatorEnum foreignRetailerIndicatorField;
        private bool foreignRetailerIndicatorSet;
        public foreignRetailerIndicatorEnum foreignRetailerIndicator
        {
            get { return foreignRetailerIndicatorField; }
            set { foreignRetailerIndicatorField = value; foreignRetailerIndicatorSet = true; }
        }
        public partialCapture partialCapture; //12.38
        //12.31 end

        public override string Serialize()
        {
            var xml = "\r\n<capture";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\"";
            if (partialSet)
            {
                xml += " partial=\"" + partial.ToString().ToLower() + "\"";
            }
            xml += ">";
            xml += "\r\n<cnpTxnId>" + cnpTxnId + "</cnpTxnId>";
            if (orderId != null) xml += "\r\n<orderId>" + SecurityElement.Escape(orderId) + "</orderId>";
            if (amountSet) xml += "\r\n<amount>" + amountField + "</amount>";
            if (surchargeAmountSet) xml += "\r\n<surchargeAmount>" + surchargeAmountField + "</surchargeAmount>";
            if (enhancedData != null) xml += "\r\n<enhancedData>" + enhancedData.Serialize() + "\r\n</enhancedData>";
            if (processingInstructions != null) xml += "\r\n<processingInstructions>" + processingInstructions.Serialize() + "\r\n</processingInstructions>";
            if (payPalOrderCompleteSet) xml += "\r\n<payPalOrderComplete>" + payPalOrderCompleteField.ToString().ToLower() + "</payPalOrderComplete>";
            if (payPalNotes != null) xml += "\r\n<payPalNotes>" + SecurityElement.Escape(payPalNotes) + "</payPalNotes>";
            if (customBilling != null) xml += "\r\n<customBilling>" + customBilling.Serialize() + "\r\n</customBilling>";
            if (lodgingInfo != null)
            {
                xml += "\r\n<lodgingInfo>" + lodgingInfo.Serialize() + "\r\n</lodgingInfo>";
            }
            if (pin != null) xml += "\r\n<pin>" + pin + "</pin>";
            if(passengerTransportData != null)//12.26
            {
                xml += "\r\n<passengerTransportData>" + passengerTransportData.Serialize() + "\r\n</passengerTransportData>";
            }
            if (foreignRetailerIndicatorSet)//12.31
            {
                xml += "\r\n<foreignRetailerIndicator>" + foreignRetailerIndicatorField + "</foreignRetailerIndicator>";
            }
            if (partialCapture != null)//12.38
            {
                xml += "\r\n<partialCapture>" + partialCapture.Serialize() + "\r\n</partialCapture>";
            }
            xml += "\r\n</capture>";

            return xml;
        }
    }

    // Capture Given Auth Transaction.
    public partial class captureGivenAuth : transactionTypeWithReportGroup
    {
        public string orderId;
        public authInformation authInformation;
        public long amount;
        private bool secondaryAmountSet;
        private long secondaryAmountField;
        public long secondaryAmount
        {
            get { return secondaryAmountField; }
            set { secondaryAmountField = value; secondaryAmountSet = true; }
        }
        private bool surchargeAmountSet;
        private long surchargeAmountField;
        public long surchargeAmount
        {
            get { return surchargeAmountField; }
            set { surchargeAmountField = value; surchargeAmountSet = true; }
        }
        public orderSourceType orderSource;
        public contact billToAddress;
        public contact retailerAddress;//12.24
        public contact shipToAddress;
        public additionalCOFData additionalCOFData;//12.24
        public cardType card;
        public mposType mpos;
        public cardTokenType token;
        public cardPaypageType paypage;
        public customBilling customBilling;
        private govtTaxTypeEnum taxTypeField;
        private bool taxTypeSet;
        public govtTaxTypeEnum taxType
        {
            get { return taxTypeField; }
            set { taxTypeField = value; taxTypeSet = true; }
        }
        
        
        private businessIndicatorEnum businessIndicatorField;
        private bool businessIndicatorSet;
        public businessIndicatorEnum businessIndicator
        {
            get { return businessIndicatorField; }
            set { businessIndicatorField = value; businessIndicatorSet = true; }
        }

        
        
        public billMeLaterRequest billMeLaterRequest;
        public enhancedData enhancedData;
        public lodgingInfo lodgingInfo;
        public processingInstructions processingInstructions;
        public pos pos;
        public amexAggregatorData amexAggregatorData;
        public merchantDataType merchantData;
        private bool debtRepaymentField;
        private bool debtRepaymentSet;
        public bool debtRepayment
        {
            get
            {
                return debtRepaymentField;
            }
            set
            {
                debtRepaymentField = value;
                debtRepaymentSet = true;
            }
        }
        private processingType processingTypeField;
        private bool processingTypeSet;
        public processingType processingType
        {
            get { return processingTypeField; }
            set { processingTypeField = value; processingTypeSet = true; }
        }
        private string originalNetworkTransactionIdField;
        private bool originalNetworkTransactionIdSet;
        public string originalNetworkTransactionId
        {
            get
            {
                return originalNetworkTransactionIdField;
            }
            set
            {
                originalNetworkTransactionIdField = value;
                originalNetworkTransactionIdSet = true;
            }
        }
        private long originalTransactionAmountField;
        private bool originalTransactionAmountSet;
        public long originalTransactionAmount
        {
            get
            {
                return originalTransactionAmountField;
            }
            set
            {
                originalTransactionAmountField = value;
                originalTransactionAmountSet = true;
            }
        }
        ///12.24
        private bool cryptoField;
        private bool cryptoSet;
        public bool crypto
        {
            get
            {
                return cryptoField;
            }
            set
            {
                cryptoField = value;
                cryptoSet = true;
            }
        }
        ///end
        public string merchantCategoryCode;
        public passengerTransportData passengerTransportData; //12.26
        //12.31 start
        private foreignRetailerIndicatorEnum foreignRetailerIndicatorField;
        private bool foreignRetailerIndicatorSet;
        public foreignRetailerIndicatorEnum foreignRetailerIndicator
        {
            get { return foreignRetailerIndicatorField; }
            set { foreignRetailerIndicatorField = value; foreignRetailerIndicatorSet = true; }
        }
        //12.31 end
        public accountFundingTransactionData accountFundingTransactionData;
        private string typeOfDigitalCurrencyField;
        private bool typeOfDigitalCurrencySet;
        public string typeOfDigitalCurrency
        {
            get
            {
                return typeOfDigitalCurrencyField;
            }
            set
            {
                typeOfDigitalCurrencyField = value;
                typeOfDigitalCurrencySet = true;
            }
        }

        private string conversionAffiliateIdField;
        private bool conversionAffiliateIdSet;
        public string conversionAffiliateId
        {
            get
            {
                return conversionAffiliateIdField;
            }
            set
            {
                conversionAffiliateIdField = value;
                conversionAffiliateIdSet = true;
            }
        }

        public override string Serialize()
        {
            var xml = "\r\n<captureGivenAuth";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            xml += "\r\n<orderId>" + SecurityElement.Escape(orderId) + "</orderId>";
            if (authInformation != null) xml += "\r\n<authInformation>" + authInformation.Serialize() + "\r\n</authInformation>";
            xml += "\r\n<amount>" + amount + "</amount>";
            if (secondaryAmountSet) xml += "\r\n<secondaryAmount>" + secondaryAmountField + "</secondaryAmount>";
            if (surchargeAmountSet) xml += "\r\n<surchargeAmount>" + surchargeAmountField + "</surchargeAmount>";
            if (orderSource != null) xml += "\r\n<orderSource>" + orderSource.Serialize() + "</orderSource>";
            if (billToAddress != null)
            {
                xml += "\r\n<billToAddress>" + billToAddress.Serialize() + "\r\n</billToAddress>"; 
            }
            if (retailerAddress != null)///12.24
            {
                xml += "\r\n<retailerAddress>" + retailerAddress.Serialize() + "\r\n</retailerAddress>"; 
            }
            if (shipToAddress != null)
            {
                xml += "\r\n<shipToAddress>" + shipToAddress.Serialize() + "\r\n</shipToAddress>";
            }
            if (additionalCOFData != null)///12.24
            {
                xml += "\r\n<additionalCOFData>" + additionalCOFData.Serialize() + "\r\n</additionalCOFData>";
            }
            if (card != null)
            {
                xml += "\r\n<card>" + card.Serialize() + "\r\n</card>";
            }
            else if (token != null)
            {
                xml += "\r\n<token>" + token.Serialize() + "\r\n</token>";
            }
            else if (mpos != null)
            {
                xml += "\r\n<mpos>" + mpos.Serialize() + "</mpos>";
            }
            else if (paypage != null)
            {
                xml += "\r\n<paypage>" + paypage.Serialize() + "\r\n</paypage>";
            }
            if (customBilling != null)
            {
                xml += "\r\n<customBilling>" + customBilling.Serialize() + "\r\n</customBilling>";
            }
            if (taxTypeSet)
            {
                xml += "\r\n<taxType>" + taxTypeField + "</taxType>";
            }

            if (billMeLaterRequest != null)
            {
                xml += "\r\n<billMeLaterRequest>" + billMeLaterRequest.Serialize() + "\r\n</billMeLaterRequest>";
            }
            if (enhancedData != null)
            {
                xml += "\r\n<enhancedData>" + enhancedData.Serialize() + "\r\n</enhancedData>";
            }
            if (lodgingInfo != null)
            {
                xml += "\r\n<lodgingInfo>" + lodgingInfo.Serialize() + "\r\n</lodgingInfo>";
            }
            if (processingInstructions != null)
            {
                xml += "\r\n<processingInstructions>" + processingInstructions.Serialize() + "\r\n</processingInstructions>";
            }
            if (pos != null)
            {
                xml += "\r\n<pos>" + pos.Serialize() + "\r\n</pos>";
            }
            if (amexAggregatorData != null)
            {
                xml += "\r\n<amexAggregatorData>" + amexAggregatorData.Serialize() + "\r\n</amexAggregatorData>";
            }
            if (merchantData != null)
            {
                xml += "\r\n<merchantData>" + merchantData.Serialize() + "\r\n</merchantData>";
            }
            if (debtRepaymentSet)
            {
                xml += "\r\n<debtRepayment>" + debtRepayment.ToString().ToLower() + "</debtRepayment>";
            }
            if (processingTypeSet && processingType != processingType.undefined)
            {
                xml += "\r\n<processingType>" + processingType + "</processingType>";
            }
            if (originalNetworkTransactionIdSet)
            {
                xml += "\r\n<originalNetworkTransactionId>" + originalNetworkTransactionId + "</originalNetworkTransactionId>";
            }
            if (originalTransactionAmountSet)
            {
                xml += "\r\n<originalTransactionAmount>" + originalTransactionAmount + "</originalTransactionAmount>";
            }
            if (cryptoSet)
            {
                xml += "\r\n<crypto>" + cryptoField.ToString().ToLower() + "</crypto>";
            }
            if (merchantCategoryCode != null)
            {
                xml += "\r\n<merchantCategoryCode>" + merchantCategoryCode + "</merchantCategoryCode>";
            }
            if (businessIndicatorSet)
            {
                xml += "\r\n<businessIndicator>" + businessIndicatorField + "</businessIndicator>";
            }
            if (passengerTransportData != null)//12.26
            {
                xml += "\r\n<passengerTransportData>" + passengerTransportData.Serialize() + "\r\n</passengerTransportData>";
            }
            if (foreignRetailerIndicatorSet)//12.31
            {
                xml += "\r\n<foreignRetailerIndicator>" + foreignRetailerIndicatorField + "</foreignRetailerIndicator>";
            }
            if (accountFundingTransactionData != null)
            {
                xml += "\r\n<accountFundingTransactionData>" + accountFundingTransactionData.Serialize() + "\r\n</accountFundingTransactionData>";
            }
            if (typeOfDigitalCurrencySet)
            {
                xml += "\r\n<typeOfDigitalCurrency>" + typeOfDigitalCurrencyField + "</typeOfDigitalCurrency>";
            }
            if (conversionAffiliateIdSet)
            {
                xml += "\r\n<conversionAffiliateId>" + conversionAffiliateIdField + "</conversionAffiliateId>";
            }
            xml += "\r\n</captureGivenAuth>";
            return xml;
        }
    }

    // Create Plan Transaction.
    public partial class createPlan : recurringTransactionType
    {
        public string planCode;
        public string name;

        private string descriptionField;
        private bool descriptionSet;
        public string description
        {
            get { return descriptionField; }
            set { descriptionField = value; descriptionSet = true; }
        }

        public intervalType intervalType;
        public long amount;

        public int numberOfPaymentsField;
        public bool numberOfPaymentsSet;
        public int numberOfPayments
        {
            get { return numberOfPaymentsField; }
            set { numberOfPaymentsField = value; numberOfPaymentsSet = true; }
        }

        public int trialNumberOfIntervalsField;
        public bool trialNumberOfIntervalsSet;
        public int trialNumberOfIntervals
        {
            get { return trialNumberOfIntervalsField; }
            set { trialNumberOfIntervalsField = value; trialNumberOfIntervalsSet = true; }
        }

        private trialIntervalType trialIntervalTypeField;
        private bool trialIntervalTypeSet;
        public trialIntervalType trialIntervalType
        {
            get { return trialIntervalTypeField; }
            set { trialIntervalTypeField = value; trialIntervalTypeSet = true; }
        }

        private bool activeField;
        private bool activeSet;
        public bool active
        {
            get { return activeField; }
            set { activeField = value; activeSet = true; }
        }

        public override string Serialize()
        {
            var xml = "\r\n<createPlan>";
            xml += "\r\n<planCode>" + SecurityElement.Escape(planCode) + "</planCode>";
            xml += "\r\n<name>" + SecurityElement.Escape(name) + "</name>";
            if (descriptionSet) xml += "\r\n<description>" + SecurityElement.Escape(descriptionField) + "</description>";
            xml += "\r\n<intervalType>" + intervalType + "</intervalType>";
            xml += "\r\n<amount>" + amount + "</amount>";
            if (numberOfPaymentsSet) xml += "\r\n<numberOfPayments>" + numberOfPaymentsField + "</numberOfPayments>";
            if (trialNumberOfIntervalsSet) xml += "\r\n<trialNumberOfIntervals>" + trialNumberOfIntervalsField + "</trialNumberOfIntervals>";
            if (trialIntervalTypeSet) xml += "\r\n<trialIntervalType>" + trialIntervalTypeField + "</trialIntervalType>";
            if (activeSet) xml += "\r\n<active>" + activeField.ToString().ToLower() + "</active>";
            xml += "\r\n</createPlan>";
            return xml;
        }
    }

    // Credit Transaction.
    public partial class credit : transactionTypeWithReportGroup
    {
        private long cnpTxnIdField;
        private bool cnpTxnIdSet;
        public long cnpTxnId
        {
            get { return cnpTxnIdField; }
            set { cnpTxnIdField = value; cnpTxnIdSet = true; }
        }
        private long amountField;
        private bool amountSet;
        public long amount
        {
            get { return amountField; }
            set { amountField = value; amountSet = true; }
        }
        private bool secondaryAmountSet;
        private long secondaryAmountField;
        public long secondaryAmount
        {
            get { return secondaryAmountField; }
            set { secondaryAmountField = value; secondaryAmountSet = true; }
        }
        private bool surchargeAmountSet;
        private long surchargeAmountField;
        public long surchargeAmount
        {
            get { return surchargeAmountField; }
            set { surchargeAmountField = value; surchargeAmountSet = true; }
        }
        public additionalCOFData additionalCOFData;///12.26
        public customBilling customBilling;
        public enhancedData enhancedData;
        public lodgingInfo lodgingInfo;
        public processingInstructions processingInstructions;
        public string orderId;
        public orderSourceType orderSource;
        public contact billToAddress;
        public contact retailerAddress;///12.24
        public cardType card;
        public mposType mpos;
        public cardTokenType token;
        public cardPaypageType paypage;
        public payPal paypal;
        private taxTypeIdentifierEnum taxTypeField;
        private bool taxTypeSet;
        public taxTypeIdentifierEnum taxType
        {
            get { return taxTypeField; }
            set { taxTypeField = value; taxTypeSet = true; }
        }
        
        private businessIndicatorEnum businessIndicatorField;
        private bool businessIndicatorSet;
        public businessIndicatorEnum businessIndicator
        {
            get { return businessIndicatorField; }
            set { businessIndicatorField = value; businessIndicatorSet = true; }
        }

        
        
        public billMeLaterRequest billMeLaterRequest;
        public pos pos;
        private string pinField;
        private bool pinSet;
        public string pin
        {
            get { return pinField; }
            set { pinField = value; pinSet = true; }
        }
        public passengerTransportData passengerTransportData;//12.26
        public amexAggregatorData amexAggregatorData;
        public merchantDataType merchantData;
        public string merchantCategoryCode;
        public string payPalNotes;
        public string actionReason;
        public accountFundingTransactionData accountFundingTransactionData;
        public override string Serialize()
        {
            var xml = "\r\n<credit";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\"";
            xml += ">";

            if (cnpTxnIdSet)
            {
                xml += "\r\n<cnpTxnId>" + cnpTxnIdField + "</cnpTxnId>";
                if (orderId != null) xml += "\r\n<orderId>" + SecurityElement.Escape(orderId) + "</orderId>";
                if (amountSet) xml += "\r\n<amount>" + amountField + "</amount>";
                if (secondaryAmountSet) xml += "\r\n<secondaryAmount>" + secondaryAmountField + "</secondaryAmount>";
                if (surchargeAmountSet) xml += "\r\n<surchargeAmount>" + surchargeAmountField + "</surchargeAmount>";
                if (additionalCOFData != null)///12.26
                {
                    xml += "\r\n<additionalCOFData>" + additionalCOFData.Serialize() + "\r\n</additionalCOFData>";
                }
                if (customBilling != null) xml += "\r\n<customBilling>" + customBilling.Serialize() + "</customBilling>";
                if (enhancedData != null) xml += "\r\n<enhancedData>" + enhancedData.Serialize() + "</enhancedData>";
                if (lodgingInfo != null) xml += "\r\n<lodgingInfo>" + lodgingInfo.Serialize() + "\r\n</lodgingInfo>";
                if (processingInstructions != null) xml += "\r\n<processingInstructions>" + processingInstructions.Serialize() + "</processingInstructions>";
                if (pos != null) xml += "\r\n<pos>" + pos.Serialize() + "</pos>";
                if (pinSet) xml += "\r\n<pin>" + pinField + "</pin>";
                if (passengerTransportData != null)///12.26
                {
                    xml += "\r\n<passengerTransportData>" + passengerTransportData.Serialize() + "\r\n</passengerTransportData>";
                }
            }
            else
            {
                xml += "\r\n<orderId>" + SecurityElement.Escape(orderId) + "</orderId>";
                xml += "\r\n<amount>" + amountField + "</amount>";
                if (secondaryAmountSet) xml += "\r\n<secondaryAmount>" + secondaryAmountField + "</secondaryAmount>";
                if (surchargeAmountSet) xml += "\r\n<surchargeAmount>" + surchargeAmountField + "</surchargeAmount>";
                if (orderSource != null) xml += "\r\n<orderSource>" + orderSource.Serialize() + "</orderSource>";
                if (billToAddress != null) xml += "\r\n<billToAddress>" + billToAddress.Serialize() + "</billToAddress>";
                if (additionalCOFData != null)///12.26
                {
                    xml += "\r\n<additionalCOFData>" + additionalCOFData.Serialize() + "\r\n</additionalCOFData>";
                }
                if (retailerAddress != null) xml += "\r\n<retailerAddress>" + retailerAddress.Serialize() + "</retailerAddress>";///12.24
                if (card != null) xml += "\r\n<card>" + card.Serialize() + "</card>";
                else if (token != null) xml += "\r\n<token>" + token.Serialize() + "</token>";
                else if (mpos != null) xml += "\r\n<mpos>" + mpos.Serialize() + "</mpos>";
                else if (paypage != null) xml += "\r\n<paypage>" + paypage.Serialize() + "</paypage>";
                else if (paypal != null)
                {
                    xml += "\r\n<paypal>";
                    if (paypal.payerId != null) xml += "\r\n<payerId>" + SecurityElement.Escape(paypal.payerId) + "</payerId>";
                    else if (paypal.payerEmail != null) xml += "\r\n<payerEmail>" + SecurityElement.Escape(paypal.payerEmail) + "</payerEmail>";
                    xml += "\r\n</paypal>";
                }
                if (customBilling != null) xml += "\r\n<customBilling>" + customBilling.Serialize() + "</customBilling>";
                if (taxTypeSet) xml += "\r\n<taxType>" + taxTypeField + "</taxType>";
                if (billMeLaterRequest != null) xml += "\r\n<billMeLaterRequest>" + billMeLaterRequest.Serialize() + "</billMeLaterRequest>";
                if (enhancedData != null) xml += "\r\n<enhancedData>" + enhancedData.Serialize() + "</enhancedData>";
                if (lodgingInfo != null) xml += "\r\n<lodgingInfo>" + lodgingInfo.Serialize() + "\r\n</lodgingInfo>";
                if (processingInstructions != null) xml += "\r\n<processingInstructions>" + processingInstructions.Serialize() + "</processingInstructions>";
                if (pos != null) xml += "\r\n<pos>" + pos.Serialize() + "</pos>";
                if (amexAggregatorData != null) xml += "\r\n<amexAggregatorData>" + amexAggregatorData.Serialize() + "</amexAggregatorData>";
                if (merchantData != null) xml += "\r\n<merchantData>" + merchantData.Serialize() + "</merchantData>";
                if (merchantCategoryCode != null)
                {
                    xml += "\r\n<merchantCategoryCode>" + merchantCategoryCode + "</merchantCategoryCode>";
                }
                if (passengerTransportData != null)///12.26
                {
                    xml += "\r\n<passengerTransportData>" + passengerTransportData.Serialize() + "\r\n</passengerTransportData>";
                }
            }
            if (payPalNotes != null) xml += "\r\n<payPalNotes>" + SecurityElement.Escape(payPalNotes) + "</payPalNotes>";
            if (actionReason != null) xml += "\r\n<actionReason>" + SecurityElement.Escape(actionReason) + "</actionReason>";
            if (businessIndicatorSet) xml += "\r\n<businessIndicator>" + businessIndicatorField + "</businessIndicator>";
            if (accountFundingTransactionData != null)
            {
                xml += "\r\n<accountFundingTransactionData>" + accountFundingTransactionData.Serialize() + "\r\n</accountFundingTransactionData>";
            }
            xml += "\r\n</credit>";
            return xml;
        }
    }

    // Deactivate Transaction.
    public partial class deactivate : transactionTypeWithReportGroup
    {
        public string orderId;
        public orderSourceType orderSource;
        public giftCardCardType card;

        public override string Serialize()
        {
            var xml = "\r\n<deactivate";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            xml += "\r\n<orderId>" + SecurityElement.Escape(orderId) + "</orderId>";
            xml += "\r\n<orderSource>" + orderSource.Serialize() + "</orderSource>";
            xml += "\r\n<card>" + card.Serialize() + "\r\n</card>";
            xml += "\r\n</deactivate>";
            return xml;
        }
    }

    // Deactivate Reversal Transaction.
    public partial class deactivateReversal : transactionTypeWithReportGroup
    {
        private long cnpTxnIdField;
        private bool cnpTxnIdSet;
        public long cnpTxnId
        {
            get
            {
                return cnpTxnIdField;
            }
            set
            {
                cnpTxnIdField = value;
                cnpTxnIdSet = true;
            }
        }
        public giftCardCardType card;
        public string originalRefCode;
        public DateTime originalTxnTime;
        public int originalSystemTraceId;
        public string originalSequenceNumber;

        public override string Serialize()
        {
            var xml = "\r\n<deactivateReversal";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (cnpTxnIdSet)
            {
                xml += "\r\n<cnpTxnId>" + cnpTxnId + "</cnpTxnId>";
            }
            xml += "\r\n<card>" + card.Serialize() + "\r\n</card>";
            xml += "\r\n<originalRefCode>" + originalRefCode + "</originalRefCode>";
            xml += "\r\n<originalTxnTime>" + originalTxnTime.ToString("yyyy-MM-ddTHH:mm:ssZ") + "</originalTxnTime>";
            xml += "\r\n<originalSystemTraceId>" + originalSystemTraceId + "</originalSystemTraceId>";
            xml += "\r\n<originalSequenceNumber>" + originalSequenceNumber + "</originalSequenceNumber>";

            xml += "\r\n</deactivateReversal>";
            return xml;
        }
    }

    // Deposit Reservsal Transaction.
    public partial class depositReversal : transactionTypeWithReportGroup
    {
        private long cnpTxnIdField;
        private bool cnpTxnIdSet;
        public long cnpTxnId
        {
            get
            {
                return cnpTxnIdField;
            }
            set
            {
                cnpTxnIdField = value;
                cnpTxnIdSet = true;
            }
        }
        public giftCardCardType card;
        public string originalRefCode;
        public long originalAmount;
        public DateTime originalTxnTime;
        public int originalSystemTraceId;
        public string originalSequenceNumber;

        public override string Serialize()
        {
            var xml = "\r\n<depositReversal";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (cnpTxnIdSet)
            {
                xml += "\r\n<cnpTxnId>" + cnpTxnId + "</cnpTxnId>";
            }
            xml += "\r\n<card>" + card.Serialize() + "\r\n</card>";
            xml += "\r\n<originalRefCode>" + originalRefCode + "</originalRefCode>";
            xml += "\r\n<originalAmount>" + originalAmount + "</originalAmount>";
            xml += "\r\n<originalTxnTime>" + originalTxnTime.ToString("yyyy-MM-ddTHH:mm:ssZ") + "</originalTxnTime>";
            xml += "\r\n<originalSystemTraceId>" + originalSystemTraceId + "</originalSystemTraceId>";
            xml += "\r\n<originalSequenceNumber>" + originalSequenceNumber + "</originalSequenceNumber>";

            xml += "\r\n</depositReversal>";
            return xml;
        }
    }

    // eCheck Credit Transaction.
    public partial class echeckCredit : transactionTypeWithReportGroup
    {
        private long cnpTxnIdField;
        private bool cnpTxnIdSet;
        public long cnpTxnId
        {
            get { return cnpTxnIdField; }
            set { cnpTxnIdField = value; cnpTxnIdSet = true; }
        }
        private long amountField;
        private bool amountSet;
        public long amount
        {
            get { return amountField; }
            set { amountField = value; amountSet = true; }
        }
        private bool secondaryAmountSet;
        private long secondaryAmountField;
        public long secondaryAmount
        {
            get { return secondaryAmountField; }
            set { secondaryAmountField = value; secondaryAmountSet = true; }
        }
        public customBilling customBilling;
        public string customIdentifier;
        public string orderId;
        public orderSourceType orderSource;
        public contact billToAddress;
        public contact retailerAddress;///12.24
        public echeckType echeck;

        [Obsolete()]
        public echeckTokenType token
        {
            get { return echeckToken; }
            set { echeckToken = value; }
        }

        public echeckTokenType echeckToken;

        public merchantDataType merchantData;

        public override string Serialize()
        {
            var xml = "\r\n<echeckCredit";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\"";
            xml += ">";

            if (cnpTxnIdSet)
            {
                xml += "\r\n<cnpTxnId>" + cnpTxnIdField + "</cnpTxnId>";
                if (amountSet) xml += "\r\n<amount>" + amountField + "</amount>";
                if (secondaryAmountSet) xml += "\r\n<secondaryAmount>" + secondaryAmountField + "</secondaryAmount>";
                if (customBilling != null) xml += "\r\n<customBilling>" + customBilling.Serialize() + "</customBilling>";
                if (customIdentifier != null) xml += "\r\n<customIdentifier>" + customIdentifier + "</customIdentifier>";
            }
            else
            {
                xml += "\r\n<orderId>" + SecurityElement.Escape(orderId) + "</orderId>";
                xml += "\r\n<amount>" + amountField + "</amount>";
                if (secondaryAmountSet) xml += "\r\n<secondaryAmount>" + secondaryAmountField + "</secondaryAmount>";
                if (orderSource != null) xml += "\r\n<orderSource>" + orderSource.Serialize() + "</orderSource>";
                if (billToAddress != null) xml += "\r\n<billToAddress>" + billToAddress.Serialize() + "</billToAddress>"; 
                if (retailerAddress != null) xml += "\r\n<retailerAddress>" + retailerAddress.Serialize() + "</retailerAddress>";///12.24
                if (echeck != null) xml += "\r\n<echeck>" + echeck.Serialize() + "</echeck>";
                else if (echeckToken != null) xml += "\r\n<echeckToken>" + echeckToken.Serialize() + "</echeckToken>";
                if (customBilling != null) xml += "\r\n<customBilling>" + customBilling.Serialize() + "</customBilling>";
                if (merchantData != null) xml += "\r\n<merchantData>" + merchantData.Serialize() + "</merchantData>";
                if (customIdentifier != null) xml += "\r\n<customIdentifier>" + customIdentifier + "</customIdentifier>";
            }
            xml += "\r\n</echeckCredit>";
            return xml;
        }
    }

    // eCheck Redeposit Transaction.
    public partial class echeckRedeposit : baseRequestTransactionEcheckRedeposit
    {
        //cnpTxnIdField and set are in super
        public echeckType echeck;
        public echeckTokenType token;
        public merchantDataType merchantData;
        public string customIdentifier;

        public override string Serialize()
        {
            var xml = "\r\n<echeckRedeposit";
            xml += " id=\"" + id + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + customerId + "\"";
            }
            xml += " reportGroup=\"" + reportGroup + "\">";
            if (cnpTxnIdSet) xml += "\r\n<cnpTxnId>" + cnpTxnIdField + "</cnpTxnId>";
            if (echeck != null) xml += "\r\n<echeck>" + echeck.Serialize() + "</echeck>";
            else if (token != null) xml += "\r\n<echeckToken>" + token.Serialize() + "</echeckToken>";
            if (merchantData != null) { xml += "\r\n<merchantData>" + merchantData.Serialize() + "\r\n</merchantData>"; }
            if (customIdentifier != null) xml += "\r\n<customIdentifier>" + customIdentifier + "</customIdentifier>";
            xml += "\r\n</echeckRedeposit>";
            return xml;
        }
    }

    // eCheck Sale Transaction.
    public partial class echeckSale : transactionTypeWithReportGroup
    {
        private long cnpTxnIdField;
        private bool cnpTxnIdSet;
        public long cnpTxnId
        {
            get { return cnpTxnIdField; }
            set { cnpTxnIdField = value; cnpTxnIdSet = true; }
        }
        private long amountField;
        private bool amountSet;
        public long amount
        {
            get { return amountField; }
            set { amountField = value; amountSet = true; }
        }
        private bool secondaryAmountSet;
        private long secondaryAmountField;
        public long secondaryAmount
        {
            get { return secondaryAmountField; }
            set { secondaryAmountField = value; secondaryAmountSet = true; }
        }
        public customBilling customBilling;
        public string customIdentifier;
        public string orderId;
        private bool verifyField;
        private bool verifySet;
        public bool verify
        {
            get { return verifyField; }
            set { verifyField = value; verifySet = true; }
        }
        public orderSourceType orderSource;
        public contact billToAddress;
        public contact retailerAddress;///12.24
        public contact shipToAddress;
        public echeckType echeck;
        public echeckTokenType token;
        public merchantDataType merchantData;

        public override string Serialize()
        {
            var xml = "\r\n<echeckSale";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\"";
            xml += ">";

            if (cnpTxnIdSet)
            {
                xml += "\r\n<cnpTxnId>" + cnpTxnIdField + "</cnpTxnId>";
                if (amountSet) xml += "\r\n<amount>" + amountField + "</amount>";
                // let sandbox do the validation for secondaryAmount
                if (secondaryAmountSet) xml += "\r\n<secondaryAmount>" + secondaryAmountField + "</secondaryAmount>";
                if (customBilling != null) xml += "\r\n<customBilling>" + customBilling.Serialize() + "</customBilling>";
                if (customIdentifier != null) xml += "\r\n<customIdentifier>" + customIdentifier + "</customIdentifier>";
            }
            else
            {
                xml += "\r\n<orderId>" + SecurityElement.Escape(orderId) + "</orderId>";
                if (verifySet) xml += "\r\n<verify>" + (verifyField ? "true" : "false") + "</verify>";
                xml += "\r\n<amount>" + amountField + "</amount>";
                if (secondaryAmountSet) xml += "\r\n<secondaryAmount>" + secondaryAmountField + "</secondaryAmount>";
                if (orderSource != null) xml += "\r\n<orderSource>" + orderSource.Serialize() + "</orderSource>";
                if (billToAddress != null) xml += "\r\n<billToAddress>" + billToAddress.Serialize() + "</billToAddress>"; 
                if (retailerAddress != null) xml += "\r\n<retailerAddress>" + retailerAddress.Serialize() + "</retailerAddress>";///12.24
                if (shipToAddress != null) xml += "\r\n<shipToAddress>" + shipToAddress.Serialize() + "</shipToAddress>";
                if (echeck != null) xml += "\r\n<echeck>" + echeck.Serialize() + "</echeck>";
                else if (token != null) xml += "\r\n<echeckToken>" + token.Serialize() + "</echeckToken>";
                if (customBilling != null) xml += "\r\n<customBilling>" + customBilling.Serialize() + "</customBilling>";
                if (merchantData != null) xml += "\r\n<merchantData>" + merchantData.Serialize() + "</merchantData>";
                if (customIdentifier != null) xml += "\r\n<customIdentifier>" + customIdentifier + "</customIdentifier>";
            }
            xml += "\r\n</echeckSale>";
            return xml;
        }
    }

    // eCheck Verification Transaction.
    public partial class echeckVerification : transactionTypeWithReportGroup
    {
        public string orderId;
        private long amountField;
        private bool amountSet;
        public long amount
        {
            get { return amountField; }
            set { amountField = value; amountSet = true; }
        }
        public orderSourceType orderSource;
        public contact billToAddress;
        public contact retailerAddress;///12.24
        public echeckType echeck;
        public echeckTokenType token;
        public merchantDataType merchantData;

        public override string Serialize()
        {
            var xml = "\r\n<echeckVerification";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\"";
            xml += ">";

            xml += "\r\n<orderId>" + orderId + "</orderId>";
            if (amountSet) xml += "\r\n<amount>" + amountField + "</amount>";
            if (orderSource != null) xml += "\r\n<orderSource>" + orderSource.Serialize() + "</orderSource>";
            if (billToAddress != null) xml += "\r\n<billToAddress>" + billToAddress.Serialize() + "</billToAddress>"; 
            if (retailerAddress != null) xml += "\r\n<retailerAddress>" + retailerAddress.Serialize() + "</retailerAddress>";///12.24
            if (echeck != null) xml += "\r\n<echeck>" + echeck.Serialize() + "</echeck>";
            else if (token != null) xml += "\r\n<echeckToken>" + token.Serialize() + "</echeckToken>";
            if (merchantData != null) xml += "\r\n<merchantData>" + merchantData.Serialize() + "</merchantData>";
            xml += "\r\n</echeckVerification>";
            return xml;
        }
    }

    // eCheck Void Transaction.
    public partial class echeckVoid : transactionTypeWithReportGroup
    {
        public long cnpTxnId;

        public override string Serialize()
        {
            var xml = "\r\n<echeckVoid";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            xml += "\r\n<cnpTxnId>" + cnpTxnId + "</cnpTxnId>";
            xml += "\r\n</echeckVoid>";
            return xml;
        }

        }

        // Force Capture Transaction.
        public partial class forceCapture : transactionTypeWithReportGroup
    {
        public string orderId;
        public long amount;
        private bool secondaryAmountSet;
        private long secondaryAmountField;
        public long secondaryAmount
        {
            get { return secondaryAmountField; }
            set { secondaryAmountField = value; secondaryAmountSet = true; }
        }
        private bool surchargeAmountSet;
        private long surchargeAmountField;
        public long surchargeAmount
        {
            get { return surchargeAmountField; }
            set { surchargeAmountField = value; surchargeAmountSet = true; }
        }
        public orderSourceType orderSource;
        public contact billToAddress;
        public contact retailerAddress;///12.24
        public cardType card;
        public mposType mpos;
        public cardTokenType token;
        public cardPaypageType paypage;
        public customBilling customBilling;
        private govtTaxTypeEnum taxTypeField;
        private bool taxTypeSet;
        public govtTaxTypeEnum taxType
        {
            get { return taxTypeField; }
            set { taxTypeField = value; taxTypeSet = true; }
        }
        
        private businessIndicatorEnum businessIndicatorField;
        private bool businessIndicatorSet;
        public businessIndicatorEnum businessIndicator
        {
            get { return businessIndicatorField; }
            set { businessIndicatorField = value; businessIndicatorSet = true; }
        }
        
        
        
        
        public enhancedData enhancedData;
        public lodgingInfo lodgingInfo;
        public processingInstructions processingInstructions;
        public pos pos;
        public amexAggregatorData amexAggregatorData;
        public merchantDataType merchantData;
        private bool debtRepaymentField;
        private bool debtRepaymentSet;
        public bool debtRepayment
        {
            get
            {
                return debtRepaymentField;
            }
            set
            {
                debtRepaymentField = value;
                debtRepaymentSet = true;
            }
        }
        private processingType processingTypeField;
        private bool processingTypeSet;
        public processingType processingType
        {
            get { return processingTypeField; }
            set { processingTypeField = value; processingTypeSet = true; }
        }

        public string merchantCategoryCode;
        public passengerTransportData passengerTransportData;//12.26

        //12.31 start
        private foreignRetailerIndicatorEnum foreignRetailerIndicatorField;
        private bool foreignRetailerIndicatorSet;
        public foreignRetailerIndicatorEnum foreignRetailerIndicator
        {
            get { return foreignRetailerIndicatorField; }
            set { foreignRetailerIndicatorField = value; foreignRetailerIndicatorSet = true; }
        }
        //12.31 end

        //new 12.35 start 
        public accountFundingTransactionData accountFundingTransactionData;
        //new 12.35 end
        public override string Serialize()
        {
            var xml = "\r\n<forceCapture";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            xml += "\r\n<orderId>" + SecurityElement.Escape(orderId) + "</orderId>";
            xml += "\r\n<amount>" + amount + "</amount>";
            if (secondaryAmountSet) xml += "\r\n<secondaryAmount>" + secondaryAmountField + "</secondaryAmount>";
            if (surchargeAmountSet) xml += "\r\n<surchargeAmount>" + surchargeAmountField + "</surchargeAmount>";
            if (orderSource != null) xml += "\r\n<orderSource>" + orderSource.Serialize() + "</orderSource>";
            if (billToAddress != null)
            {
                xml += "\r\n<billToAddress>" + billToAddress.Serialize() + "\r\n</billToAddress>"; 
            }
            if (retailerAddress != null)
            {
                xml += "\r\n<retailerAddress>" + retailerAddress.Serialize() + "\r\n</retailerAddress>"; ///12.24
            }
            if (card != null)
            {
                xml += "\r\n<card>" + card.Serialize() + "\r\n</card>";
            }
            else if (token != null)
            {
                xml += "\r\n<token>" + token.Serialize() + "\r\n</token>";
            }
            else if (mpos != null)
            {
                xml += "\r\n<mpos>" + mpos.Serialize() + "</mpos>";
            }
            else if (paypage != null)
            {
                xml += "\r\n<paypage>" + paypage.Serialize() + "\r\n</paypage>";
            }
            if (customBilling != null)
            {
                xml += "\r\n<customBilling>" + customBilling.Serialize() + "\r\n</customBilling>";
            }
            if (taxTypeSet)
            {
                xml += "\r\n<taxType>" + taxTypeField + "</taxType>";
            }
            if (enhancedData != null)
            {
                xml += "\r\n<enhancedData>" + enhancedData.Serialize() + "\r\n</enhancedData>";
            }
            if (lodgingInfo != null)
            {
                xml += "\r\n<lodgingInfo>" + lodgingInfo.Serialize() + "\r\n</lodgingInfo>";
            }
            if (processingInstructions != null)
            {
                xml += "\r\n<processingInstructions>" + processingInstructions.Serialize() + "\r\n</processingInstructions>";
            }
            if (pos != null)
            {
                xml += "\r\n<pos>" + pos.Serialize() + "\r\n</pos>";
            }
            if (amexAggregatorData != null)
            {
                xml += "\r\n<amexAggregatorData>" + amexAggregatorData.Serialize() + "\r\n</amexAggregatorData>";
            }
            if (merchantData != null)
            {
                xml += "\r\n<merchantData>" + merchantData.Serialize() + "\r\n</merchantData>";
            }
            if (debtRepaymentSet)
            {
                xml += "\r\n<debtRepayment>" + debtRepayment.ToString().ToLower() + "</debtRepayment>";
            }
            if (processingTypeSet && processingType != processingType.undefined)
            {
                xml += "\r\n<processingType>" + processingType + "</processingType>";
            }

            if (merchantCategoryCode != null)
            {
                xml += "\r\n<merchantCategoryCode>" + merchantCategoryCode + "</merchantCategoryCode>";
            }

            if (businessIndicatorSet)
            {
                xml += "\r\n<businessIndicator>" + businessIndicatorField + "</businessIndicator>";
            }
            if (passengerTransportData != null)//12.26
            {
                xml += "\r\n<passengerTransportData>" + passengerTransportData.Serialize() + "</passengerTransportData>";
            }
            if (foreignRetailerIndicatorSet)//12.31
            {
                xml += "\r\n<foreignRetailerIndicator>" + foreignRetailerIndicatorField + "</foreignRetailerIndicator>";
            }
            if (accountFundingTransactionData != null)
            {
                xml += "\r\n<accountFundingTransactionData>" + accountFundingTransactionData.Serialize() + "\r\n</accountFundingTransactionData>";
            }
            xml += "\r\n</forceCapture>";
            return xml;
        }
    }

    // Fraud Check Transaction. [Online]
    public partial class fraudCheckType
    {
        public string authenticationValue;
        public string authenticationTransactionId;
        public string customerIpAddress;
        public string authenticationProtocolVersion;
        private bool authenticatedByMerchantField;
        private bool authenticatedByMerchantSet;
        private string tokenAuthenticationValue;
        public bool authenticatedByMerchant
        {
            get { return authenticatedByMerchantField; }
            set { authenticatedByMerchantField = value; authenticatedByMerchantSet = true; }
        }

        public string Serialize()
        {
            var xml = "";
            if (authenticationValue != null) xml += "\r\n<authenticationValue>" + SecurityElement.Escape(authenticationValue) + "</authenticationValue>";
            if (authenticationTransactionId != null) xml += "\r\n<authenticationTransactionId>" + SecurityElement.Escape(authenticationTransactionId) + "</authenticationTransactionId>";
            if (customerIpAddress != null) xml += "\r\n<customerIpAddress>" + SecurityElement.Escape(customerIpAddress) + "</customerIpAddress>";
            if (authenticatedByMerchantSet) xml += "\r\n<authenticatedByMerchant>" + authenticatedByMerchantField + "</authenticatedByMerchant>";
            if (authenticationProtocolVersion != null) xml += "\r\n<authenticationProtocolVersion>" + authenticationProtocolVersion + "</authenticationProtocolVersion>";
            if (tokenAuthenticationValue != null) xml += "\r\n<tokenAuthenticationValue>" + tokenAuthenticationValue + "</tokenAuthenticationValue";
            return xml;
        }
    }

    // Gift Card Auth Reversal Transaction.
    public partial class giftCardAuthReversal : transactionTypeWithReportGroup
    {
        private long cnpTxnIdField;
        private bool cnpTxnIdSet;
        public long cnpTxnId
        {
            get
            {
                return cnpTxnIdField;
            }
            set
            {
                cnpTxnIdField = value;
                cnpTxnIdSet = true;
            }
        }
        public giftCardCardType card;
        public string originalRefCode;
        public long originalAmount;
        public DateTime originalTxnTime;
        private int originalSystemTraceIdField;
        private bool originalSystemTraceIdSet;
        public int originalSystemTraceId
        {
            get
            {
                return originalSystemTraceIdField;
            }
            set
            {
                originalSystemTraceIdField = value;
                originalSystemTraceIdSet = true;
            }
        }
        public string originalSequenceNumber;

        public override string Serialize()
        {
            var xml = "\r\n<giftCardAuthReversal ";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (cnpTxnIdSet)
            {
                xml += "\r\n<cnpTxnId>" + cnpTxnIdField + "</cnpTxnId>";
            }
            if (card != null)
            {
                xml += "\r\n<card>" + card.Serialize() + "\r\n</card>";
            }
            if (originalRefCode != null)
            {
                xml += "\r\n<originalRefCode>" + originalRefCode + "</originalRefCode>";
            }
            // Do I need to turn original amount long into a boolean because it cannot be compared to null
            xml += "\r\n<originalAmount>" + originalAmount + "</originalAmount>";

            if (originalTxnTime != null)
            {
                xml += "\r\n<originalTxnTime>" + originalTxnTime.ToString("yyyy-MM-ddTHH:mm:ssZ") + "</originalTxnTime>";
            }
            // I turned this into a boolean so the serialixing methos was in a boolean form
            if (originalSystemTraceIdSet)
            {
                xml += "\r\n<originalSystemTraceId>" + originalSystemTraceIdField + "</originalSystemTraceId>";
            }
            if (originalSequenceNumber != null)
            {
                xml += "\r\n<originalSequenceNumber>" + originalSequenceNumber + "</originalSequenceNumber>";
            }
            xml += "\r\n</giftCardAuthReversal>";
            return xml;
        }
    }

    // Gift Card Capture Transaction.
    public partial class giftCardCapture : transactionTypeWithReportGroupAndPartial
    {
        private long cnpTxnIdField;
        private bool cnpTxnIdSet;
        public long cnpTxnId
        {
            get
            {
                return cnpTxnIdField;
            }
            set
            {
                cnpTxnIdField = value;
                cnpTxnIdSet = true;
            }
        }
        public long captureAmount;
        public giftCardCardType card;
        public string originalRefCode;
        public long originalAmount;
        public DateTime originalTxnTime;

        public override string Serialize()
        {
            var xml = "\r\n<giftCardCapture";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\"";
            if (partialSet)
            {
                xml += " partial=\"" + partial.ToString().ToLower() + "\"";
            }
            xml += ">";
            if (cnpTxnIdSet)
            {
                xml += "\r\n<cnpTxnId>" + cnpTxnIdField + "</cnpTxnId>";
            }

            xml += "\r\n<captureAmount>" + captureAmount + "</captureAmount>";

            if (card != null)
            {
                xml += "\r\n<card>" + card.Serialize() + "\r\n</card>";
            }
            if (originalRefCode != null)
            {
                xml += "\r\n<originalRefCode>" + originalRefCode + "</originalRefCode>";
            }
            // Do I need to turn original amount long into a boolean because it cannot be compared to null
            xml += "\r\n<originalAmount>" + originalAmount + "</originalAmount>";

            if (originalTxnTime != null)
            {
                xml += "\r\n<originalTxnTime>" + originalTxnTime.ToString("yyyy-MM-ddTHH:mm:ssZ") + "</originalTxnTime>";
            }
            xml += "\r\n</giftCardCapture>";
            return xml;
        }
    }

    // Gift Card Credit Transaction.
    public partial class giftCardCredit : transactionTypeWithReportGroup
    {
        private long cnpTxnIdField;
        private bool cnpTxnIdSet;
        public long cnpTxnId
        {
            get { return cnpTxnIdField; }
            set { cnpTxnIdField = value; cnpTxnIdSet = true; }
        }
        public long creditAmount;
        public giftCardCardType card;
        public string orderId;
        public orderSourceType orderSource;

        public override string Serialize()
        {
            var xml = "\r\n<giftCardCredit";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";

            if (cnpTxnIdSet)
            {
                xml += "\r\n<cnpTxnId>" + cnpTxnIdField + "</cnpTxnId>";
                xml += "\r\n<creditAmount>" + creditAmount + "</creditAmount>";
                xml += "\r\n<card>" + card.Serialize() + "\r\n</card>";
            }
            else
            {
                xml += "\r\n<orderId>" + SecurityElement.Escape(orderId) + "</orderId>";
                xml += "\r\n<creditAmount>" + creditAmount + "</creditAmount>";
                xml += "\r\n<orderSource>" + orderSource.Serialize() + "</orderSource>";
                xml += "\r\n<card>" + card.Serialize() + "\r\n</card>";
            }
            xml += "\r\n</giftCardCredit>";
            return xml;
        }
    }

    // Load Transaction.
    public partial class load : transactionTypeWithReportGroup
    {
        public string orderId;
        public long amount;
        public orderSourceType orderSource;
        public giftCardCardType card;

        public override string Serialize()
        {
            var xml = "\r\n<load";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            xml += "\r\n<orderId>" + SecurityElement.Escape(orderId) + "</orderId>";
            xml += "\r\n<amount>" + amount + "</amount>";
            xml += "\r\n<orderSource>" + orderSource.Serialize() + "</orderSource>";
            xml += "\r\n<card>" + card.Serialize() + "\r\n</card>";
            xml += "\r\n</load>";
            return xml;
        }
    }

    // Load Reversal Transaction.
    public partial class loadReversal : transactionTypeWithReportGroup
    {
        private long cnpTxnIdField;
        private bool cnpTxnIdSet;
        public long cnpTxnId
        {
            get
            {
                return cnpTxnIdField;
            }
            set
            {
                cnpTxnIdField = value;
                cnpTxnIdSet = true;
            }
        }
        public giftCardCardType card;
        public string originalRefCode;
        public long originalAmount;
        public DateTime originalTxnTime;
        public int originalSystemTraceId;
        public string originalSequenceNumber;

        public override string Serialize()
        {
            var xml = "\r\n<loadReversal";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (cnpTxnIdSet)
            {
                xml += "\r\n<cnpTxnId>" + cnpTxnId + "</cnpTxnId>";
            }
            xml += "\r\n<card>" + card.Serialize() + "\r\n</card>";
            xml += "\r\n<originalRefCode>" + originalRefCode + "</originalRefCode>";
            xml += "\r\n<originalAmount>" + originalAmount + "</originalAmount>";
            xml += "\r\n<originalTxnTime>" + originalTxnTime.ToString("yyyy-MM-ddTHH:mm:ssZ") + "</originalTxnTime>";
            xml += "\r\n<originalSystemTraceId>" + originalSystemTraceId + "</originalSystemTraceId>";
            xml += "\r\n<originalSequenceNumber>" + originalSequenceNumber + "</originalSequenceNumber>";

            xml += "\r\n</loadReversal>";
            return xml;
        }
    }

    // Register Token Transaction.
    public partial class registerTokenRequestType : transactionTypeWithReportGroup
    {
        public string encryptionKeyId;
        public string orderId;
        public mposType mpos;
        public string accountNumber;
        public string encryptedAccountNumber;
        public echeckForTokenType echeckForToken;
        public string paypageRegistrationId;
        public string cardValidationNum;
        public applepayType applepay;
        public string encryptedCardValidationNum;

        public override string Serialize()
        {
            var xml = "\r\n<registerTokenRequest";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\"";
            xml += ">";
            xml += "\r\n<encryptionKeyId>" + encryptionKeyId + "</encryptionKeyId>";
            xml += "\r\n<orderId>" + orderId + "</orderId>";
            if (mpos != null) xml += "\r\n<mpos>" + mpos.Serialize() + "</mpos>";
            else if (accountNumber != null) xml += "\r\n<accountNumber>" + accountNumber + "</accountNumber>";
            else if (encryptedAccountNumber != null) xml += "\r\n<encryptedAccountNumber>" + encryptedAccountNumber + "</encryptedAccountNumber>";
            else if (echeckForToken != null) xml += "\r\n<echeckForToken>" + echeckForToken.Serialize() + "</echeckForToken>";
            else if (paypageRegistrationId != null) xml += "\r\n<paypageRegistrationId>" + paypageRegistrationId + "</paypageRegistrationId>";
            else if (applepay != null) xml += "\r\n<applepay>" + applepay.Serialize() + "\r\n</applepay>";
            if (cardValidationNum != null) xml += "\r\n<cardValidationNum>" + cardValidationNum + "</cardValidationNum>";
            else if (encryptedCardValidationNum != null) xml += "\r\n<encryptedCardValidationNum>" + encryptedCardValidationNum + "</encryptedCardValidationNum>";
            xml += "\r\n</registerTokenRequest>";
            return xml;
        }
    }

    // Refund Reversal Transaction.
    public partial class refundReversal : transactionTypeWithReportGroup
    {
        private long cnpTxnIdField;
        private bool cnpTxnIdSet;
        public long cnpTxnId
        {
            get
            {
                return cnpTxnIdField;
            }
            set
            {
                cnpTxnIdField = value;
                cnpTxnIdSet = true;
            }
        }
        public giftCardCardType card;
        public string originalRefCode;
        public long originalAmount;
        public DateTime originalTxnTime;
        public int originalSystemTraceId;
        public string originalSequenceNumber;

        public override string Serialize()
        {
            var xml = "\r\n<refundReversal";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (cnpTxnIdSet)
            {
                xml += "\r\n<cnpTxnId>" + cnpTxnId + "</cnpTxnId>";
            }
            xml += "\r\n<card>" + card.Serialize() + "\r\n</card>";
            xml += "\r\n<originalRefCode>" + originalRefCode + "</originalRefCode>";
            xml += "\r\n<originalAmount>" + originalAmount + "</originalAmount>";
            xml += "\r\n<originalTxnTime>" + originalTxnTime.ToString("yyyy-MM-ddTHH:mm:ssZ") + "</originalTxnTime>";
            xml += "\r\n<originalSystemTraceId>" + originalSystemTraceId + "</originalSystemTraceId>";
            xml += "\r\n<originalSequenceNumber>" + originalSequenceNumber + "</originalSequenceNumber>";

            xml += "\r\n</refundReversal>";
            return xml;
        }
    }

    // Sale Transaction.
    public partial class sale : transactionTypeWithReportGroup
    {

        private long cnpTxnIdField;
        private bool cnpTxnIdSet;
        public long cnpTxnId
        {
            get
            {
                return cnpTxnIdField;
            }
            set
            {
                cnpTxnIdField = value;
                cnpTxnIdSet = true;
            }
        }
        public string orderId;
        public long amount;
        private bool secondaryAmountSet;
        private long secondaryAmountField;
        public long secondaryAmount
        {
            get { return secondaryAmountField; }
            set { secondaryAmountField = value; secondaryAmountSet = true; }
        }
        private bool surchargeAmountSet;
        private long surchargeAmountField;
        public long surchargeAmount
        {
            get { return surchargeAmountField; }
            set { surchargeAmountField = value; surchargeAmountSet = true; }
        }
        public orderSourceType orderSource;
        public customerInfo customerInfo;
        public sellerInfo sellerInfo;//12.29
        public contact billToAddress;
        public contact retailerAddress;///12.24
        public contact shipToAddress;
        public additionalCOFData additionalCOFData;///12.24
        public cardType card;
        public mposType mpos;
        public payPal paypal;
        public cardTokenType token;
        public cardPaypageType paypage;
        public applepayType applepay;
        public sepaDirectDebitType sepaDirectDebit;
        public idealType ideal;
        public giropayType giropay;
        public sofortType sofort;
        public billMeLaterRequest billMeLaterRequest;
        public fraudCheckType cardholderAuthentication;
        public customBilling customBilling;
        private govtTaxTypeEnum taxTypeField;
        private bool taxTypeSet;

        public govtTaxTypeEnum taxType
        {
            get { return taxTypeField; }
            set { taxTypeField = value; taxTypeSet = true; }
        }
        
        private businessIndicatorEnum businessIndicatorField;
        private bool businessIndicatorSet;
        public businessIndicatorEnum businessIndicator
        {
            get { return businessIndicatorField; }
            set { businessIndicatorField = value; businessIndicatorSet = true; }
        }
        ///12.24 
        private orderChannelEnum orderChannelField;
        private bool orderChannelSet;
        public orderChannelEnum orderChannel
        {
            get
            {
                return orderChannelField;
            }
            set
            {
                orderChannelField = value;
                orderChannelSet = true;
            }
        }
        private string fraudCheckStatusField;
        private bool fraudCheckStatusSet;
        public string fraudCheckStatus
        {
            get
            {
                return fraudCheckStatusField;
            }
            set
            {
                fraudCheckStatusField = value;
                fraudCheckStatusSet = true;
            }
        }
        private bool cryptoField;
        private bool cryptoSet;
        public bool crypto
        {
            get
            {
                return cryptoField;
            }
            set
            {
                cryptoField = value;
                cryptoSet = true;
            }
        }
        ///end

        //12.25 and 12.26 start
        private string overridePolicyField;
        private bool overridePolicySet;
        public string overridePolicy
        {
            get
            {
                return overridePolicyField;
            }
            set
            {
                overridePolicyField = value;
                overridePolicySet = true;
            }
        }
        private string fsErrorCodeField;
        private bool fsErrorCodeSet;
        public string fsErrorCode
        {
            get
            {
                return fsErrorCodeField;
            }
            set
            {
                fsErrorCodeField = value;
                fsErrorCodeSet = true;
            }
        }
        private string merchantAccountStatusField;
        private bool merchantAccountStatusSet;
        public string merchantAccountStatus
        {
            get
            {
                return merchantAccountStatusField;
            }
            set
            {
                merchantAccountStatusField = value;
                merchantAccountStatusSet = true;
            }
        }
        private productEnrolledEnum productEnrolledField;
        private bool productEnrolledSet;
        public productEnrolledEnum productEnrolled
        {
            get
            {
                return productEnrolledField;
            }
            set
            {
                productEnrolledField = value;
                productEnrolledSet = true;
            }
        }
        private decisionPurposeEnum decisionPurposeField;
        private bool decisionPurposeSet;
        public decisionPurposeEnum decisionPurpose
        {
            get
            {
                return decisionPurposeField;
            }
            set
            {
                decisionPurposeField = value;
                decisionPurposeSet = true;
            }
        }
        private fraudSwitchIndicatorEnum fraudSwitchIndicatorField;
        private bool fraudSwitchIndicatorSet;
        public fraudSwitchIndicatorEnum fraudSwitchIndicator
        {
            get
            {
                return fraudSwitchIndicatorField;
            }
            set
            {
                fraudSwitchIndicatorField = value;
                fraudSwitchIndicatorSet = true;
            }
        }
        public passengerTransportData passengerTransportData;
        //12.25 and 12.26 end
        public enhancedData enhancedData;
        public processingInstructions processingInstructions;
        public pos pos;
        private bool payPalOrderCompleteField;
        private bool payPalOrderCompleteSet;
        public bool payPalOrderComplete
        {
            get { return payPalOrderCompleteField; }
            set { payPalOrderCompleteField = value; payPalOrderCompleteSet = true; }
        }
        public string payPalNotes;
        public amexAggregatorData amexAggregatorData;
        private bool allowPartialAuthField;
        private bool allowPartialAuthSet;
        public bool allowPartialAuth
        {
            get
            {
                return allowPartialAuthField;
            }
            set
            {
                allowPartialAuthField = value;
                allowPartialAuthSet = true;
            }
        }
        public healthcareIIAS healthcareIIAS;
        public lodgingInfo lodgingInfo;
        public filteringType filtering;
        public merchantDataType merchantData;
        public recyclingRequestType recyclingRequest;
        private bool fraudFilterOverrideField;
        private bool fraudFilterOverrideSet;
        public bool fraudFilterOverride
        {
            get
            {
                return fraudFilterOverrideField;
            }
            set
            {
                fraudFilterOverrideField = value;
                fraudFilterOverrideSet = true;
            }
        }
        public recurringRequest recurringRequest;
        public cnpInternalRecurringRequest cnpInternalRecurringRequest;
        private bool debtRepaymentField;
        private bool debtRepaymentSet;
        public bool debtRepayment
        {
            get
            {
                return debtRepaymentField;
            }
            set
            {
                debtRepaymentField = value;
                debtRepaymentSet = true;
            }
        }
        public advancedFraudChecksType advancedFraudChecks;
        public wallet wallet;
        private processingType processingTypeField;
        private bool processingTypeSet;
        public processingType processingType
        {
            get { return processingTypeField; }
            set { processingTypeField = value; processingTypeSet = true; }
        }
        private string originalNetworkTransactionIdField;
        private bool originalNetworkTransactionIdSet;
        public string originalNetworkTransactionId
        {
            get
            {
                return originalNetworkTransactionIdField;
            }
            set
            {
                originalNetworkTransactionIdField = value;
                originalNetworkTransactionIdSet = true;
            }
        }
        private long originalTransactionAmountField;
        private bool originalTransactionAmountSet;
        public long originalTransactionAmount
        {
            get
            {
                return originalTransactionAmountField;
            }
            set
            {
                originalTransactionAmountField = value;
                originalTransactionAmountSet = true;
            }
        }

        public pinlessDebitRequestType pinlessDebitRequest;
        public bool? skipRealtimeAU;
        public string merchantCategoryCode;
        //12.31 start
        private foreignRetailerIndicatorEnum foreignRetailerIndicatorField;
        private bool foreignRetailerIndicatorSet;
        public foreignRetailerIndicatorEnum foreignRetailerIndicator
        {
            get { return foreignRetailerIndicatorField; }
            set { foreignRetailerIndicatorField = value; foreignRetailerIndicatorSet = true; }
        }
        //12.31 end

        //private routingPreferenceEnum routingPreferenceField;
        //private bool routingPreferenceSet;

        //public routingPreferenceEnum routingPreference
        //{
        //    get { return routingPreferenceField; }
        //    set
        //    {
        //        routingPreferenceField = value;
        //        routingPreferenceSet = true;
        //    }
        //}


        //new 12.35
        public accountFundingTransactionData accountFundingTransactionData;

        private fraudCheckActionEnum fraudCheckActionFeild;
        private bool fraudCheckActionSet;
        public fraudCheckActionEnum fraudCheckAction
        {
            get
            {
                return fraudCheckActionFeild;
            }
            set
            {
                fraudCheckActionFeild = value;
                fraudCheckActionSet = true;
            }
        }

        private string typeOfDigitalCurrencyField;
        private bool typeOfDigitalCurrencySet;
        public string typeOfDigitalCurrency
        {
            get
            {
                return typeOfDigitalCurrencyField;
            }
            set
            {
                typeOfDigitalCurrencyField = value;
                typeOfDigitalCurrencySet = true;
            }
        }

        private string conversionAffiliateIdField;
        private bool conversionAffiliateIdSet;
        public string conversionAffiliateId
        {
            get
            {
                return conversionAffiliateIdField;
            }
            set
            {
                conversionAffiliateIdField = value;
                conversionAffiliateIdSet = true;
            }
        }


        public override string Serialize()
        {
            var xml = "\r\n<sale";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (cnpTxnIdSet) xml += "\r\n<cnpTxnId>" + cnpTxnIdField + "</cnpTxnId>";
            xml += "\r\n<orderId>" + orderId + "</orderId>";
            xml += "\r\n<amount>" + amount + "</amount>";
            if (secondaryAmountSet) xml += "\r\n<secondaryAmount>" + secondaryAmountField + "</secondaryAmount>";
            if (surchargeAmountSet) xml += "\r\n<surchargeAmount>" + surchargeAmountField + "</surchargeAmount>";
            if (orderSource != null) xml += "\r\n<orderSource>" + orderSource.Serialize() + "</orderSource>";
            if (customerInfo != null)
            {
                xml += "\r\n<customerInfo>" + customerInfo.Serialize() + "\r\n</customerInfo>";
            }
            if (sellerInfo != null)
            {
                xml += "\r\n<sellerInfo>" + sellerInfo.Serialize() + "\r\n</sellerInfo>";
            }
            if (billToAddress != null)
            {
                xml += "\r\n<billToAddress>" + billToAddress.Serialize() + "\r\n</billToAddress>"; 
            }
            if (retailerAddress != null)///12.24
            {
                xml += "\r\n<retailerAddress>" + retailerAddress.Serialize() + "\r\n</retailerAddress>"; 
            }
            if (shipToAddress != null)
            {
                xml += "\r\n<shipToAddress>" + shipToAddress.Serialize() + "\r\n</shipToAddress>";
            }
            if (additionalCOFData != null)///12.24
            {
                xml += "\r\n<additionalCOFData>" + additionalCOFData.Serialize() + "\r\n</additionalCOFData>";
            }
            if (card != null)
            {
                xml += "\r\n<card>" + card.Serialize() + "\r\n</card>";
            }
            else if (paypal != null)
            {
                xml += "\r\n<paypal>" + paypal.Serialize() + "\r\n</paypal>";
            }
            else if (token != null)
            {
                xml += "\r\n<token>" + token.Serialize() + "\r\n</token>";
            }
            else if (mpos != null)
            {
                xml += "\r\n<mpos>" + mpos.Serialize() + "</mpos>";
            }
            else if (paypage != null)
            {
                xml += "\r\n<paypage>" + paypage.Serialize() + "\r\n</paypage>";
            }
            else if (applepay != null)
            {
                xml += "\r\n<applepay>" + applepay.Serialize() + "\r\n</applepay>";
            }
            else if (sepaDirectDebit != null)
            {
                xml += "\r\n<sepaDirectDebit>" + sepaDirectDebit.Serialize() + "\r\n</sepaDirectDebit>";
            }
            else if (ideal != null)
            {
                xml += "\r\n<ideal>" + ideal.Serialize() + "\r\n</ideal>";
            }
            else if (giropay != null)
            {
                xml += "\r\n<giropay>" + giropay.Serialize() + "\r\n</giropay>";
            }
            else if (sofort != null)
            {
                xml += "\r\n<sofort>" + sofort.Serialize() + "\r\n</sofort>";
            }
            if (billMeLaterRequest != null)
            {
                xml += "\r\n<billMeLaterRequest>" + billMeLaterRequest.Serialize() + "\r\n</billMeLaterRequest>";
            }
            if (cardholderAuthentication != null)
            {
                xml += "\r\n<cardholderAuthentication>" + cardholderAuthentication.Serialize() + "\r\n</cardholderAuthentication>";
            }
            if (customBilling != null)
            {
                xml += "\r\n<customBilling>" + customBilling.Serialize() + "\r\n</customBilling>";
            }
            if (taxTypeSet)
            {
                xml += "\r\n<taxType>" + taxTypeField + "</taxType>";
            }
            ///12.24
            if (orderChannelSet)
            {
                xml += "\r\n<orderChannel>" + orderChannelField + "</orderChannel>";
            }
            if (fraudCheckStatusSet)
            {
                xml += "\r\n<fraudCheckStatus>" + fraudCheckStatusField + "</fraudCheckStatus>";
            }
            if (cryptoSet)
            {
                xml += "\r\n<crypto>" + cryptoField.ToString().ToLower() + "</crypto>";
            }
            ///end
            if (enhancedData != null)
            {
                xml += "\r\n<enhancedData>" + enhancedData.Serialize() + "\r\n</enhancedData>";
            }
            if (processingInstructions != null)
            {
                xml += "\r\n<processingInstructions>" + processingInstructions.Serialize() + "\r\n</processingInstructions>";
            }
            if (pos != null)
            {
                xml += "\r\n<pos>" + pos.Serialize() + "\r\n</pos>";
            }
            if (payPalOrderCompleteSet) xml += "\r\n<payPalOrderCompleteSet>" + payPalOrderCompleteField.ToString().ToLower() + "</payPalOrderCompleteSet>";
            if (payPalNotes != null) xml += "\r\n<payPalNotes>" + SecurityElement.Escape(payPalNotes) + "</payPalNotes>";
            if (amexAggregatorData != null)
            {
                xml += "\r\n<amexAggregatorData>" + amexAggregatorData.Serialize() + "\r\n</amexAggregatorData>";
            }
            if (allowPartialAuthSet)
            {
                xml += "\r\n<allowPartialAuth>" + allowPartialAuthField.ToString().ToLower() + "</allowPartialAuth>";
            }
            if (healthcareIIAS != null)
            {
                xml += "\r\n<healthcareIIAS>" + healthcareIIAS.Serialize() + "\r\n</healthcareIIAS>";
            }
            if (lodgingInfo != null)
            {
                xml += "\r\n<lodgingInfo>" + lodgingInfo.Serialize() + "\r\n</lodgingInfo>";
            }
            if (filtering != null)
            {
                xml += "\r\n<filtering>" + filtering.Serialize() + "\r\n</filtering>";
            }
            if (merchantData != null)
            {
                xml += "\r\n<merchantData>" + merchantData.Serialize() + "\r\n</merchantData>";
            }
            if (recyclingRequest != null)
            {
                xml += "\r\n<recyclingRequest>" + recyclingRequest.Serialize() + "\r\n</recyclingRequest>";
            }
            if (fraudFilterOverrideSet) xml += "\r\n<fraudFilterOverride>" + fraudFilterOverrideField.ToString().ToLower() + "</fraudFilterOverride>";
            if (recurringRequest != null)
            {
                xml += "\r\n<recurringRequest>" + recurringRequest.Serialize() + "\r\n</recurringRequest>";
            }
            if (cnpInternalRecurringRequest != null)
            {
                xml += "\r\n<cnpInternalRecurringRequest>" + cnpInternalRecurringRequest.Serialize() + "\r\n</cnpInternalRecurringRequest>";
            }
            if (debtRepaymentSet) xml += "\r\n<debtRepayment>" + debtRepayment.ToString().ToLower() + "</debtRepayment>";
            if (advancedFraudChecks != null) xml += "\r\n<advancedFraudChecks>" + advancedFraudChecks.Serialize() + "\r\n</advancedFraudChecks>";
            if (wallet != null)
            {
                xml += "\r\n<wallet>" + wallet.Serialize() + "\r\n</wallet>";
            }
            if (processingTypeSet && processingType != processingType.undefined)
            {
                xml += "\r\n<processingType>" + processingType + "</processingType>";
            }
            if (originalNetworkTransactionIdSet)
            {
                xml += "\r\n<originalNetworkTransactionId>" + originalNetworkTransactionId + "</originalNetworkTransactionId>";
            }
            if (originalTransactionAmountSet)
            {
                xml += "\r\n<originalTransactionAmount>" + originalTransactionAmount + "</originalTransactionAmount>";
            }
            if (pinlessDebitRequest != null) {
                xml += "\r\n<pinlessDebitRequest>" + pinlessDebitRequest.Serialize() + "</pinlessDebitRequest>";
            }
            if (skipRealtimeAU != null) {
                xml += "\r\n<skipRealtimeAU>" + skipRealtimeAU.ToString().ToLower() + "</skipRealtimeAU>";
            }
            if (merchantCategoryCode != null)
            {
                xml += "\r\n<merchantCategoryCode>" + merchantCategoryCode + "</merchantCategoryCode>";
            }
            if (businessIndicatorSet)
            {
                xml += "\r\n<businessIndicator>" + businessIndicatorField + "</businessIndicator>";
            }
            //12.25 and 12.26
            if (overridePolicySet)
            {
                xml += "\r\n<overridePolicy>" + overridePolicyField + "</overridePolicy>";
            }
            if (fsErrorCodeSet)
            {
                xml += "\r\n<fsErrorCode>" + fsErrorCodeField + "</fsErrorCode>";
            }
            if (merchantAccountStatusSet)
            {
                xml += "\r\n<merchantAccountStatus>" + merchantAccountStatusField + "</merchantAccountStatus>";
            }
            if (productEnrolledSet)
            {
                xml += "\r\n<productEnrolled>" + productEnrolledField + "</productEnrolled>";
            }
            if (decisionPurposeSet)
            {
                xml += "\r\n<decisionPurpose>" + decisionPurposeField + "</decisionPurpose>";
            }
            if (fraudSwitchIndicatorSet)
            {
                xml += "\r\n<fraudSwitchIndicator>" + fraudSwitchIndicatorField + "</fraudSwitchIndicator>";
            }
            if (passengerTransportData != null)//12.26
            {
                xml += "\r\n<passengerTransportData>" + passengerTransportData.Serialize() + "\r\n</passengerTransportData>";
            }
            if (foreignRetailerIndicatorSet)//12.31
            {
                xml += "\r\n<foreignRetailerIndicator>" + foreignRetailerIndicatorField + "</foreignRetailerIndicator>";
            }
            if (accountFundingTransactionData != null)
            {
                xml += "\r\n<accountFundingTransactionData>" + accountFundingTransactionData.Serialize() + "\r\n</accountFundingTransactionData>";
            }
            if (fraudCheckActionSet != null)
            {
                xml += "\r\n<fraudCheckAction>" + fraudCheckActionFeild + "</fraudCheckAction>";
            }

            if (typeOfDigitalCurrencySet)
            {
                xml += "\r\n<typeOfDigitalCurrency>" + typeOfDigitalCurrencyField + "</typeOfDigitalCurrency>";
            }
            if (conversionAffiliateIdSet)
            {
                xml += "\r\n<conversionAffiliateId>" + conversionAffiliateIdField + "</conversionAffiliateId>";
            }
        //end
        //if (routingPreferenceSet)
        //{
        //    var routingPreferenceName = routingPreferenceField.ToString();
        //    var attributes = 
        //        (XmlEnumAttribute[])typeof(echeckAccountTypeEnum).GetMember(routingPreferenceField.ToString())[0].GetCustomAttributes(typeof(XmlEnumAttribute), false);
        //    if (attributes.Length > 0) routingPreferenceName = attributes[0].Name;
        //    xml += "\r\n<routingPreference>" + routingPreferenceName + "</routingPreference>";
        //}

        xml += "\r\n</sale>";
            return xml;
        }
    }

    // Unload Transaction.
    public partial class unload : transactionTypeWithReportGroup
    {
        public string orderId;
        public long amount;
        public orderSourceType orderSource;
        public giftCardCardType card;

        public override string Serialize()
        {
            var xml = "\r\n<unload";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            xml += "\r\n<orderId>" + SecurityElement.Escape(orderId) + "</orderId>";
            xml += "\r\n<amount>" + amount + "</amount>";
            xml += "\r\n<orderSource>" + orderSource.Serialize() + "</orderSource>";
            xml += "\r\n<card>" + card.Serialize() + "\r\n</card>";
            xml += "\r\n</unload>";
            return xml;
        }
    }

    // Update Card Validation Number Transaction.
    public partial class updateCardValidationNumOnToken : transactionTypeWithReportGroup
    {
        public string orderId;
        public string cnpToken;
        public string cardValidationNum;

        public override string Serialize()
        {
            var xml = "\r\n<updateCardValidationNumOnToken";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\"";
            xml += ">";

            if (orderId != null) xml += "\r\n<orderId>" + SecurityElement.Escape(orderId) + "</orderId>";
            if (cnpToken != null) xml += "\r\n<cnpToken>" + SecurityElement.Escape(cnpToken) + "</cnpToken>";
            if (cardValidationNum != null) xml += "\r\n<cardValidationNum>" + SecurityElement.Escape(cardValidationNum) + "</cardValidationNum>";
            xml += "\r\n</updateCardValidationNumOnToken>";
            return xml;
        }
    }

    // Update Plan Transaction.
    public partial class updatePlan : recurringTransactionType
    {
        public string planCode;

        private bool activeField;
        private bool activeSet;
        public bool active
        {
            get { return activeField; }
            set { activeField = value; activeSet = true; }
        }

        public override string Serialize()
        {
            var xml = "\r\n<updatePlan>";
            xml += "\r\n<planCode>" + SecurityElement.Escape(planCode) + "</planCode>";
            if (activeSet) xml += "\r\n<active>" + activeField.ToString().ToLower() + "</active>";
            xml += "\r\n</updatePlan>";
            return xml;
        }
    }

    // Update Subscription Transaction.
    public partial class updateSubscription : recurringTransactionType
    {
        private long subscriptionIdField;
        private bool subscriptionIdSet;
        public long subscriptionId
        {
            get
            {
                return subscriptionIdField;
            }
            set
            {
                subscriptionIdField = value;
                subscriptionIdSet = true;
            }
        }

        public string planCode;
        public contact billToAddress;
        public contact retailerAddress;///12.24
        public cardType card;
        public cardTokenType token;
        public cardPaypageType paypage;
        private DateTime billingDateField;
        private bool billingDateSet;
        public DateTime billingDate
        {
            get
            {
                return billingDateField;
            }
            set
            {
                billingDateField = value;
                billingDateSet = true;
            }
        }

        public List<createDiscount> createDiscounts;
        public List<updateDiscount> updateDiscounts;
        public List<deleteDiscount> deleteDiscounts;
        public List<createAddOn> createAddOns;
        public List<updateAddOn> updateAddOns;
        public List<deleteAddOn> deleteAddOns;

        public updateSubscription()
        {
            createDiscounts = new List<createDiscount>();
            updateDiscounts = new List<updateDiscount>();
            deleteDiscounts = new List<deleteDiscount>();
            createAddOns = new List<createAddOn>();
            updateAddOns = new List<updateAddOn>();
            deleteAddOns = new List<deleteAddOn>();
        }

        public override string Serialize()
        {
            var xml = "\r\n<updateSubscription>";
            if (subscriptionIdSet) xml += "\r\n<subscriptionId>" + subscriptionIdField + "</subscriptionId>";
            if (planCode != null) xml += "\r\n<planCode>" + SecurityElement.Escape(planCode) + "</planCode>";
            if (billToAddress != null) xml += "\r\n<billToAddress>" + billToAddress.Serialize() + "\r\n</billToAddress>";
            if (retailerAddress != null) xml += "\r\n<retailerAddress>" + retailerAddress.Serialize() + "\r\n</retailerAddress>";///12.24
            if (card != null) xml += "\r\n<card>" + card.Serialize() + "\r\n</card>";
            else if (token != null) xml += "\r\n<token>" + token.Serialize() + "\r\n</token>";
            else if (paypage != null) xml += "\r\n<paypage>" + paypage.Serialize() + "\r\n</paypage>";
            if (billingDateSet) xml += "\r\n<billingDate>" + XmlUtil.toXsdDate(billingDateField) + "</billingDate>";
            foreach (var createDiscount in createDiscounts)
            {
                xml += "\r\n<createDiscount>" + createDiscount.Serialize() + "\r\n</createDiscount>";
            }
            foreach (var updateDiscount in updateDiscounts)
            {
                xml += "\r\n<updateDiscount>" + updateDiscount.Serialize() + "\r\n</updateDiscount>";
            }
            foreach (var deleteDiscount in deleteDiscounts)
            {
                xml += "\r\n<deleteDiscount>" + deleteDiscount.Serialize() + "\r\n</deleteDiscount>";
            }
            foreach (var createAddOn in createAddOns)
            {
                xml += "\r\n<createAddOn>" + createAddOn.Serialize() + "\r\n</createAddOn>";
            }
            foreach (var updateAddOn in updateAddOns)
            {
                xml += "\r\n<updateAddOn>" + updateAddOn.Serialize() + "\r\n</updateAddOn>";
            }
            foreach (var deleteAddOn in deleteAddOns)
            {
                xml += "\r\n<deleteAddOn>" + deleteAddOn.Serialize() + "\r\n</deleteAddOn>";
            }
            xml += "\r\n</updateSubscription>";
            return xml;
        }
    }

    // Unload Reversal Transaction.
    public partial class unloadReversal : transactionTypeWithReportGroup
    {
        private long cnpTxnIdField;
        private bool cnpTxnIdSet;
        public long cnpTxnId
        {
            get
            {
                return cnpTxnIdField;
            }
            set
            {
                cnpTxnIdField = value;
                cnpTxnIdSet = true;
            }
        }
        public giftCardCardType card;
        public string originalRefCode;
        public long originalAmount;
        public DateTime originalTxnTime;
        public int originalSystemTraceId;
        public string originalSequenceNumber;

        public override string Serialize()
        {
            var xml = "\r\n<unloadReversal";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (cnpTxnIdSet)
            {
                xml += "\r\n<cnpTxnId>" + cnpTxnId + "</cnpTxnId>";
            }
            xml += "\r\n<card>" + card.Serialize() + "\r\n</card>";
            xml += "\r\n<originalRefCode>" + originalRefCode + "</originalRefCode>";
            xml += "\r\n<originalAmount>" + originalAmount + "</originalAmount>";
            xml += "\r\n<originalTxnTime>" + originalTxnTime.ToString("yyyy-MM-ddTHH:mm:ssZ") + "</originalTxnTime>";
            xml += "\r\n<originalSystemTraceId>" + originalSystemTraceId + "</originalSystemTraceId>";
            xml += "\r\n<originalSequenceNumber>" + originalSequenceNumber + "</originalSequenceNumber>";

            xml += "\r\n</unloadReversal>";
            return xml;
        }
    }

    // Void Transaction.
    public partial class voidTxn : transactionTypeWithReportGroup
    {
        // The void element is the parent element for all Void transactions.
        // You can use this element only in Online transactions.
        // Note: the element Void named voidTxn as void is a keyword in C#.
        public long cnpTxnId;
        public processingInstructions processingInstructions;

        public override string Serialize()
        {
            var xml = "\r\n<void";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\"";
            xml += ">";
            xml += "\r\n<cnpTxnId>" + cnpTxnId + "</cnpTxnId>";
            if (processingInstructions != null) xml += "\r\n<processingInstructions>" + processingInstructions.Serialize() + "\r\n</processingInstructions>";
            xml += "\r\n</void>";

            return xml;
        }

    }

    // Fast Access Funding Transaction.
    public partial class fastAccessFunding : transactionTypeWithReportGroup
    {
        public string fundingSubmerchantId;
        public string fundingCustomerId;
        public string submerchantName;
        public string customerName;
        public string fundsTransferId;
        public addressType cardholderAddress;
        public int amount;
        private disbursementTypeEnum disbursementTypeField;
        private bool disbursementTypeSet;
        public cardType card;
        public cardTokenType token;
        public cardPaypageType paypage;

        public disbursementTypeEnum disbursementType
        {
            get { return disbursementTypeField; }
            set
            {
                disbursementTypeField = value;
                disbursementTypeSet = true;
            }
        }

        public override string Serialize()
        {
            var xml = "\r\n<fastAccessFunding";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";

            // The first element of a sequence xml element  represent the sequence element
            if (fundingSubmerchantId != null || fundingCustomerId != null)
            {
                if (fundingSubmerchantId != null) 
                    xml += "\r\n<fundingSubmerchantId>" + fundingSubmerchantId + "</fundingSubmerchantId>";
                else if (fundingCustomerId != null)
                    xml += "\r\n<fundingCustomerId>" + fundingCustomerId + "</fundingCustomerId>";
                if (submerchantName != null)
                    xml += "\r\n<submerchantName>" + submerchantName + "</submerchantName>";
                else if (customerName != null)
                    xml += "\r\n<customerName>" + customerName + "</customerName>";
                xml += "\r\n<fundsTransferId>" + fundsTransferId + "</fundsTransferId>";
                xml += "\r\n<amount>" + amount + "</amount>";
                if (disbursementTypeSet)
                    xml += "\r\n<disbursementType>" + disbursementTypeField + "</disbursementType>";
                if (cardholderAddress != null) xml += "\r\n<cardholderAddress>" + cardholderAddress.Serialize() + "</cardholderAddress>";
                if (card != null) xml += "\r\n<card>" + card.Serialize() + "</card>";
                else if (token != null) xml += "\r\n<token>" + token.Serialize() + "</token>";
                else xml += "\r\n<paypage>" + paypage.Serialize() + "</paypage>";
            }
            xml += "\r\n</fastAccessFunding>";
            return xml;
        }
    }

    public enum disbursementTypeEnum
    {
        VAA,
        VBB,
        VBI,
        VBP,
        VCC,
        VCI,
        VCO,
        VCP,
        VFD,
        VGD,
        VGP,
        VLO,
        VMA,
        VMD,
        VMI,
        VMP,
        VOG,
        VPD,
        VPG,
        VPP,
        VPS,
        VTU,
        VWT,
    }

    // Translate To Low Value Token Request Transaction.
    public partial class translateToLowValueTokenRequest : transactionTypeWithReportGroup
    {
        public string orderId { get; set; }

        public string token { get; set; }


        public override string Serialize()
        {
            var xml = "\r\n<translateToLowValueTokenRequest ";

            xml += "id=\"" + SecurityElement.Escape(id) + "\" ";
            if (customerId != null)
                xml += "customerId=\"" + SecurityElement.Escape(customerId) + "\" ";
            xml += "reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (orderId != null)
                xml += "\r\n<orderId>" + SecurityElement.Escape(orderId) + "</orderId>";
            xml += "\r\n<token>" + SecurityElement.Escape(token) + "</token>";
            xml += "\r\n</translateToLowValueTokenRequest>";

            return xml;
        }
    }

    // PayFac Credit Transaction. Implemented in CnpBatchRequest.

    // PayFac Debit Transaction. Implemented in CnpBatchRequest.

    // Physical Check Credit Transaction. Implemented in CnpBatchRequest.

    // Physical Check Debit Transaction. Implemented in CnpBatchRequest.

    // Reserve Credit Transaction. Implemented in CnpBatchRequest.

    // Reserve Debit Transaction. Implemented in CnpBatchRequest.

    // Vendor Credit Transaction. Implemented in CnpBatchRequest.

    // Vendor Debit Transaction. Implemented in CnpBatchRequest.

    // Query Transaction. Implemented in CnpBatchRequest.
    public partial class queryTransaction : transactionTypeWithReportGroup
    {
        public string origId;
        private actionTypeEnum origActionTypeField;
        private bool origActionTypeSet;
        public long origCnpTxnId;
        private yesNoTypeEnum showStatusOnlyField;
        private bool showStatusOnlySet;
        private paymentTypeEnum paymentTypeField; ///12.24
        private bool paymentTypeSet;
        private frequencyOfMITEnum frequencyOfMITField;///12.24
        private bool frequencyOfMITSet;
        private fulfilmentMethodTypeEnum fulfilmentMethodTypeField; ///12.24
        private bool fulfilmentMethodTypeSet;
        private orderChannelEnum orderChannelField; ///12.24
        private bool orderChannelSet;

        public actionTypeEnum origActionType
        {
            get
            {
                return origActionTypeField;
            }
            set
            {
                origActionTypeField = value;
                origActionTypeSet = true;
            }
        }

        public yesNoTypeEnum showStatusOnly
        {
            get
            {
                return showStatusOnlyField;
            }
            set
            {
                showStatusOnlyField = value;
                showStatusOnlySet = true;
            }
        }
        ///12.24
        public paymentTypeEnum paymentType
        {
            get 
            { 
                return paymentTypeField;
            }
            set 
            { 
                paymentTypeField = value; 
                paymentTypeSet = true;
            }
        }

        ///12.24
        public frequencyOfMITEnum frequencyOfMIT
        {
            get
            {
                return frequencyOfMITField;
            }
            set
            {
                frequencyOfMITField = value;
                frequencyOfMITSet = true;
            }
        }

        ///12.24
        public fulfilmentMethodTypeEnum fulfilmentMethod
        {
            get
            {
                return fulfilmentMethodTypeField;
            }
            set
            {
                fulfilmentMethodTypeField = value;
                fulfilmentMethodTypeSet = true;
            }
        }

        ///12.24
        public orderChannelEnum orderChannel
        {
            get
            {
                return orderChannelField;
            }
            set
            {
                orderChannelField = value;
                orderChannelSet = true;
            }
        }

        public override string Serialize()
        {

            var xml = "\r\n<queryTransaction";

            xml += " id=\"" + SecurityElement.Escape(id) + "\"";

            if (customerId != null) xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";

            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";

            if (origId != null) xml += "\r\n<origId>" + SecurityElement.Escape(origId) + "</origId>";

            if (origActionTypeSet) xml += "\r\n<origActionType>" + origActionTypeField + "</origActionType>";

            if (origCnpTxnId != 0) xml += "\r\n<origCnpTxnId>" + origCnpTxnId + "</origCnpTxnId>";

            if (showStatusOnlySet) xml += "\r\n<showStatusOnly>" + showStatusOnlyField + "</showStatusOnly>";

            var accTypeName = paymentTypeField.ToString();
            var attributes =
                (XmlEnumAttribute[])typeof(paymentTypeEnum).GetMember(paymentTypeField.ToString())[0].GetCustomAttributes(typeof(XmlEnumAttribute), false);
            if (attributes.Length > 0) accTypeName = attributes[0].Name;
            

            if (paymentTypeSet) xml += "\r\n<paymentType>" + accTypeName + "</paymentType>";///12.24

            if (frequencyOfMITSet) xml += "\r\n<frequencyOfMIT>" + frequencyOfMITField + "</frequencyOfMIT>";///12.24

            if (fulfilmentMethodTypeSet) xml += "\r\n<fulfilmentMethod>" + fulfilmentMethodTypeField + "</fulfilmentMethod>";///12.24 

            if (orderChannelSet) xml += "\r\n<orderChannel>" + orderChannelField + "</orderChannel>";///12.24

            xml += "\r\n</queryTransaction>";

            return xml;
        }

    }

    // All Enum declarations

    public enum actionTypeEnum
    {
        A,
        D,
        R,
        AR,
        G,
        I,
        J,
        L,
        LR,
        P,
        RR,
        S,
        T,
        UR,
        V,
        W,
        X,
        //12.27
        FIFA,
        FIVC,
        FIVD,
        FISC,
        FISD,
        FIPC,
        FIPD,
        FIRC,
        FIRD
    }

    // To include element showStatusOnly having type Enum
    public enum yesNoTypeEnum
    {

        Y,
        N,
    }

    // [END] SUPPORTED TRANSACTIONS FOR ONLINE PROCESSING.

    /// new 12.24
    public enum frequencyOfMITEnum
    {
        Daily,
        Weekly,
        BiWeekly,
        Monthly,
        Quarterly,
        BiAnnually,
        Annually,
        UNSCHEDULED,

    }

    /// new 12.24 to be checked
    public enum paymentTypeEnum
    {
        [System.Xml.Serialization.XmlEnumAttribute("Fixed Amount")]
        Fixed_Amount,

        [System.Xml.Serialization.XmlEnumAttribute("Variable Amount")]
        Variable_Amount,
    }

    /// new 12.24
    public enum fulfilmentMethodTypeEnum
    {
        DELIVERY,
        COUNTER_PICKUP,
        CURBSIDE_PICKUP,
        LOCKER_PICKUP,
        STANDARD_SHIPPING,
        EXPEDITED_SHIPPING
    }

    /// new 12.24
    public enum orderChannelEnum
    {
        WEB,
        PHONE,
        MOBILE_APP,
        SOCIAL,
        MARKETPLACE,
        IN_STORE_KIOSK,
        MIT,// new 12.28
        // new 12.35
        SCAN_AND_GO,
        SMART_TV
    }

    //new 12.25, 12.26 and 12.27 start
    public enum travelPackageIndicatorEnum
    {
        CarRentalReservation,
        AirlineReservation,
        Both,
        Unknown
    }

    public enum productEnrolledEnum
    {
        GUARPAY1,
        GUARPAY2,
        GUARPAY3
    }

    public enum decisionPurposeEnum
    {
        CONSIDER_DECISION,
        INFORMATION_ONLY
    }

    public enum fraudSwitchIndicatorEnum
    {
        PRE,
        POST
    }

    public enum creditReasonIndicatorEnum
    {
        A,
        B,
        C,
        O,
        P
    }

    public enum ticketChangeIndicatorEnum
    {
        C,
        N
    }

    public enum computerizedReservationSystemEnum
    {
        STRT,
        PARS,
        DATS,
        SABR,
        DALA,
        BLAN,
        DERD,
        TUID
    }

    public enum serviceClassEnum
    {
        First,
        Business,
        Economy
    }

    public enum walletSourceType
    {
        MasterPass,

    }
    //12.25, 12.26 and 12.27 end

    //new 12.28, 12.29 and 12.30 start
    public enum authIndicatorEnum
    {
        Estimated,
        Incremental
    }
    // 12.28, 12.29 and 12.30 end

    //new 12.31 start
    public enum foreignRetailerIndicatorEnum
    {
       F
    }
    // 12.31 end
    #endregion

    //new 12.35
    public enum accountFundingTransactionTypeEnum
    {
        accountToAccount,
        agentCashOut,
        bankInitiated,
        businessDisbursement,
        businessToBusinessTransfer,
        debitPrepaidAccount,
        fundsDisbursement,
        fundTransfer,
        governmentNonProfitDisbursement,
        paymentOfOwnCreditCardBill,
        payrollDisbursement,
        personToPerson,
        personToPersonCardAccount,
        rapidMerchantSettlement,
        topUp,
        walletTransfer
    }

    public enum accountFundingTransactionAccountNumberTypeEnum
    {
        other,
        RTNAndBAN,
        IBAN,
        cardAccount,
        email,
        phoneNumber,
        BANAndBIC,
        walletID,
        socialNetworkID
    }
    public enum fraudCheckActionEnum
    {
        APPROVED_SKIP_FRAUD_CHECK,
        DECLINED_NEED_FRAUD_CHECK,
    } 
    public enum provider
    {
        AFFIRM
    }


    #region Child elements.
    // The customerInfo element is the parent of several child elements use to define customer information.
    public partial class customerInfo
    {

        public string ssn;

        public DateTime dob;

        public DateTime customerRegistrationDate;

        private customerInfoCustomerType customerTypeField;
        private bool customerTypeSet;
        public customerInfoCustomerType customerType
        {
            get { return customerTypeField; }
            set { customerTypeField = value; customerTypeSet = true; }
        }

        private long incomeAmountField;
        private bool incomeAmountSet;
        public long incomeAmount
        {
            get { return incomeAmountField; }
            set { incomeAmountField = value; incomeAmountSet = true; }
        }

        private currencyCodeEnum incomeCurrencyField;
        private bool incomeCurrencySet;
        public currencyCodeEnum incomeCurrency
        {
            get { return incomeCurrencyField; }
            set { incomeCurrencyField = value; incomeCurrencySet = true; }
        }

        private bool customerCheckingAccountField;
        private bool customerCheckingAccountSet;
        public bool customerCheckingAccount
        {
            get { return customerCheckingAccountField; }
            set { customerCheckingAccountField = value; customerCheckingAccountSet = true; }
        }

        private bool customerSavingAccountField;
        private bool customerSavingAccountSet;
        public bool customerSavingAccount
        {
            get { return customerSavingAccountField; }
            set { customerSavingAccountField = value; customerSavingAccountSet = true; }
        }

        public string employerName;
        ///new 12.24 start
        public string accountUsername;
        public string userAccountNumber;
        
        public string membershipId;
        public string membershipName;
        public string membershipPhone;
        public string membershipEmail;    
        public DateTime accountCreatedDate;
        public string userAccountPhone;
        public string userAccountEmail;
        ///end
        public string customerWorkTelephone;

        private customerInfoResidenceStatus residenceStatusField;
        private bool residenceStatusSet;
        public customerInfoResidenceStatus residenceStatus
        {
            get { return residenceStatusField; }
            set { residenceStatusField = value; residenceStatusSet = true; }
        }

        private int yearsAtResidenceField;
        private bool yearsAtResidenceSet;
        public int yearsAtResidence
        {
            get { return yearsAtResidenceField; }
            set { yearsAtResidenceField = value; yearsAtResidenceSet = true; }
        }

        private int yearsAtEmployerField;
        private bool yearsAtEmployerSet;
        public int yearsAtEmployer
        {
            get
            {
                return yearsAtEmployerField;
            }
            set
            {
                yearsAtEmployerField = value;
                yearsAtEmployerSet = true;
            }
        }


        public customerInfo()
        {
            incomeCurrency = currencyCodeEnum.USD;
        }

        public string Serialize()
        {
            var xml = "";
            if (ssn != null)
            {
                xml += "\r\n<ssn>" + SecurityElement.Escape(ssn) + "</ssn>";
            }
            if (dob != null)
            {
                xml += "\r\n<dob>" + XmlUtil.toXsdDate(dob) + "</dob>";
            }
            if (customerRegistrationDate != null)
            {
                xml += "\r\n<customerRegistrationDate>" + XmlUtil.toXsdDate(customerRegistrationDate) + "</customerRegistrationDate>";
            }
            if (customerTypeSet)
            {
                xml += "\r\n<customerType>" + customerTypeField + "</customerType>";
            }
            if (incomeAmountSet)
            {
                xml += "\r\n<incomeAmount>" + incomeAmountField + "</incomeAmount>";
            }
            if (incomeCurrencySet)
            {
                xml += "\r\n<incomeCurrency>" + incomeCurrencyField + "</incomeCurrency>";
            }
            if (customerCheckingAccountSet)
            {
                xml += "\r\n<customerCheckingAccount>" + customerCheckingAccountField.ToString().ToLower() + "</customerCheckingAccount>";
            }
            if (customerSavingAccountSet)
            {
                xml += "\r\n<customerSavingAccount>" + customerSavingAccountField.ToString().ToLower() + "</customerSavingAccount>";
            }
            if (employerName != null)
            {
                xml += "\r\n<employerName>" + SecurityElement.Escape(employerName) + "</employerName>";
            }            
            if (customerWorkTelephone != null)
            {
                xml += "\r\n<customerWorkTelephone>" + SecurityElement.Escape(customerWorkTelephone) + "</customerWorkTelephone>";
            }
            if (residenceStatusSet)
            {
                xml += "\r\n<residenceStatus>" + residenceStatusField + "</residenceStatus>";
            }
            if (yearsAtResidenceSet)
            {
                xml += "\r\n<yearsAtResidence>" + yearsAtResidenceField + "</yearsAtResidence>";
            }
            if (yearsAtEmployerSet)
            {
                xml += "\r\n<yearsAtEmployer>" + yearsAtEmployerField + "</yearsAtEmployer>";
            }
            ///12.24 start
            if (accountUsername != null)
            {
                xml += "\r\n<accountUsername>" + SecurityElement.Escape(accountUsername) + "</accountUsername>";
            }
            if (userAccountNumber != null)
            {
                xml += "\r\n<userAccountNumber>" + SecurityElement.Escape(userAccountNumber) + "</userAccountNumber>";
            }
            if (userAccountEmail != null)
            {
                xml += "\r\n<userAccountEmail>" + SecurityElement.Escape(userAccountEmail) + "</userAccountEmail>";
            }
            if (membershipId != null)
            {
                xml += "\r\n<membershipId>" + SecurityElement.Escape(membershipId) + "</membershipId>";
            }
            if (membershipPhone != null)
            {
                xml += "\r\n<membershipPhone>" + SecurityElement.Escape(membershipPhone) + "</membershipPhone>";
            }
            if (membershipEmail != null)
            {
                xml += "\r\n<membershipEmail>" + SecurityElement.Escape(membershipEmail) + "</membershipEmail>";
            } 
            if (membershipName != null)
            {
                xml += "\r\n<membershipName>" + SecurityElement.Escape(membershipName) + "</membershipName>";
            }
            if (accountCreatedDate != null)
            {
                xml += "\r\n<accountCreatedDate>" + XmlUtil.toXsdDate(accountCreatedDate) + "</accountCreatedDate>";
            } 
            if (userAccountPhone != null)
            {
                xml += "\r\n<userAccountPhone>" + SecurityElement.Escape(userAccountPhone) + "</userAccountPhone>";
            }
            ///12.24 end
            return xml;
        }


    }

    // Not used anywhere.
    public enum customerInfoCustomerType
    {

        /// <remarks/>
        New,
        Existing,
    }

    public enum currencyCodeEnum
    {

        /// <remarks/>
        AUD,
        CAD,
        CHF,
        DKK,
        EUR,
        GBP,
        HKD,
        JPY,
        NOK,
        NZD,
        SEK,
        SGD,
        USD,
    }

    public enum customerInfoResidenceStatus
    {

        /// <remarks/>
        Own,
        Rent,
        Other,
    }

    // The enhancedData element allows you to specify extra information concerning a transaction
    // in order to qualify for certain purchasing interchange rates.
    public partial class enhancedData
    {
        public string customerReference;
        private long salesTaxField;
        private bool salesTaxSet;
        public long salesTax
        {
            get { return salesTaxField; }
            set { salesTaxField = value; salesTaxSet = true; }
        }
        private enhancedDataDeliveryType deliveryTypeField;
        private bool deliveryTypeSet;
        public enhancedDataDeliveryType deliveryType
        {
            get { return deliveryTypeField; }
            set { deliveryTypeField = value; deliveryTypeSet = true; }
        }
        public bool taxExemptField;
        public bool taxExemptSet;
        public bool taxExempt
        {
            get { return taxExemptField; }
            set { taxExemptField = value; taxExemptSet = true; }
        }
        private long discountAmountField;
        private bool discountAmountSet;
        public long discountAmount
        {
            get { return discountAmountField; }
            set { discountAmountField = value; discountAmountSet = true; }
        }
        private long shippingAmountField;
        private bool shippingAmountSet;
        public long shippingAmount
        {
            get { return shippingAmountField; }
            set { shippingAmountField = value; shippingAmountSet = true; }
        }
        private long dutyAmountField;
        private bool dutyAmountSet;
        public long dutyAmount
        {
            get { return dutyAmountField; }
            set { dutyAmountField = value; dutyAmountSet = true; }
        }
        public string shipFromPostalCode;
        public string destinationPostalCode;
        private countryTypeEnum destinationCountryCodeField;
        private bool destinationCountryCodeSet;
        public countryTypeEnum destinationCountry
        {
            get { return destinationCountryCodeField; }
            set { destinationCountryCodeField = value; destinationCountryCodeSet = true; }
        }
        public string invoiceReferenceNumber;
        ///12.24
        public string discountCode;
        private int discountPercentField;
        private bool discountPercentSet;
        public int discountPercent
        {
            get { return discountPercentField; }
            set { discountPercentField = value; discountPercentSet = true; }
        }
        private fulfilmentMethodTypeEnum fulfilmentMethodTypeField;
        private bool fulfilmentMethodTypeSet;
        public fulfilmentMethodTypeEnum fulfilmentMethodType
        {
            get 
            {
                return fulfilmentMethodTypeField; 
            }
            set
            {
                fulfilmentMethodTypeField = value;
                fulfilmentMethodTypeSet = true;
            }
        }
        ///end
        private DateTime orderDateField;
        private bool orderDateSet;
        public DateTime orderDate
        {
            get { return orderDateField; }
            set { orderDateField = value; orderDateSet = true; }
        }
        public List<detailTax> detailTaxes;
        public List<lineItemData> lineItems;

        public enhancedData()
        {
            lineItems = new List<lineItemData>();
            detailTaxes = new List<detailTax>();
        }

        public string Serialize()
        {
            var xml = "";
            if (customerReference != null) xml += "\r\n<customerReference>" + SecurityElement.Escape(customerReference) + "</customerReference>";
            if (salesTaxSet) xml += "\r\n<salesTax>" + salesTaxField + "</salesTax>";
            if (deliveryTypeSet) xml += "\r\n<deliveryType>" + deliveryTypeField + "</deliveryType>";
            if (taxExemptSet) xml += "\r\n<taxExempt>" + taxExemptField.ToString().ToLower() + "</taxExempt>";
            if (discountAmountSet) xml += "\r\n<discountAmount>" + discountAmountField + "</discountAmount>";
            if (shippingAmountSet) xml += "\r\n<shippingAmount>" + shippingAmountField + "</shippingAmount>";
            if (dutyAmountSet) xml += "\r\n<dutyAmount>" + dutyAmountField + "</dutyAmount>";
            if (shipFromPostalCode != null) xml += "\r\n<shipFromPostalCode>" + SecurityElement.Escape(shipFromPostalCode) + "</shipFromPostalCode>";
            if (destinationPostalCode != null) xml += "\r\n<destinationPostalCode>" + SecurityElement.Escape(destinationPostalCode) + "</destinationPostalCode>";
            if (destinationCountryCodeSet) xml += "\r\n<destinationCountryCode>" + destinationCountryCodeField + "</destinationCountryCode>";
            if (invoiceReferenceNumber != null) xml += "\r\n<invoiceReferenceNumber>" + SecurityElement.Escape(invoiceReferenceNumber) + "</invoiceReferenceNumber>";
            if (orderDateSet) xml += "\r\n<orderDate>" + XmlUtil.toXsdDate(orderDateField) + "</orderDate>";
            foreach (var detailTax in detailTaxes)
            {
                xml += "\r\n<detailTax>" + detailTax.Serialize() + "\r\n</detailTax>";
            }
            foreach (var lineItem in lineItems)
            {
                xml += "\r\n<lineItemData>" + lineItem.Serialize() + "\r\n</lineItemData>";
            }
            ///12.24
            if (discountCode != null) xml += "\r\n<discountCode>" + SecurityElement.Escape(discountCode) + "</discountCode>";
            if (discountPercentSet) xml += "\r\n<discountPercent>" + discountPercentField + "</discountPercent>";
            if (fulfilmentMethodTypeSet) xml += "\r\n<fulfilmentMethodType>" + fulfilmentMethodTypeField + "</fulfilmentMethodType>";
            ///end
            return xml;
        }
    }

    // The lineItemData element contains several child elements
    // used to define information concerning individual items in the order.
    public partial class lineItemData
    {
        private int itemSeqenceNumberField;
        private bool itemSequenceNumberSet;
        public int itemSequenceNumber
        {
            get { return itemSeqenceNumberField; }
            set { itemSeqenceNumberField = value; itemSequenceNumberSet = true; }
        }
        public string itemDescription;
        public string productCode;
        public string quantity;
        public string unitOfMeasure;
        private long taxAmountField;
        private bool taxAmountSet;
        public long taxAmount
        {
            get { return taxAmountField; }
            set { taxAmountField = value; taxAmountSet = true; }
        }
        private long lineItemTotalField;
        private bool lineItemTotalSet;
        public long lineItemTotal
        {
            get { return lineItemTotalField; }
            set { lineItemTotalField = value; lineItemTotalSet = true; }
        }
        private long lineItemTotalWithTaxField;
        private bool lineItemTotalWithTaxSet;
        public long lineItemTotalWithTax
        {
            get { return lineItemTotalWithTaxField; }
            set { lineItemTotalWithTaxField = value; lineItemTotalWithTaxSet = true; }
        }
        private long itemDiscountAmountField;
        private bool itemDiscountAmountSet;
        public long itemDiscountAmount
        {
            get { return itemDiscountAmountField; }
            set { itemDiscountAmountField = value; itemDiscountAmountSet = true; }
        }
        public string commodityCode;
        public string unitCost;
        ///new 12.24
        public string itemCategory;
        public string itemSubCategory;
        public string productId;
        public string productName;
        ///end
        public List<detailTax> detailTaxes;
        // 12.33
        public string shipmentId;
        public List<subscriptions> subscription;

        public lineItemData()
        {
            detailTaxes = new List<detailTax>();
            subscription = new List<subscriptions>();
        }
        
        public string Serialize()
        {
            var xml = "";
            if (itemSequenceNumberSet) xml += "\r\n<itemSequenceNumber>" + itemSeqenceNumberField + "</itemSequenceNumber>";
            if (itemDescription != null) xml += "\r\n<itemDescription>" + SecurityElement.Escape(itemDescription) + "</itemDescription>";
            if (productCode != null) xml += "\r\n<productCode>" + SecurityElement.Escape(productCode) + "</productCode>";
            if (quantity != null) xml += "\r\n<quantity>" + SecurityElement.Escape(quantity) + "</quantity>";
            if (unitOfMeasure != null) xml += "\r\n<unitOfMeasure>" + SecurityElement.Escape(unitOfMeasure) + "</unitOfMeasure>";
            if (taxAmountSet) xml += "\r\n<taxAmount>" + taxAmountField + "</taxAmount>";
            if (lineItemTotalSet) xml += "\r\n<lineItemTotal>" + lineItemTotalField + "</lineItemTotal>";
            if (lineItemTotalWithTaxSet) xml += "\r\n<lineItemTotalWithTax>" + lineItemTotalWithTaxField + "</lineItemTotalWithTax>";
            if (itemDiscountAmountSet) xml += "\r\n<itemDiscountAmount>" + itemDiscountAmountField + "</itemDiscountAmount>";
            if (commodityCode != null) xml += "\r\n<commodityCode>" + SecurityElement.Escape(commodityCode) + "</commodityCode>";
            if (unitCost != null) xml += "\r\n<unitCost>" + SecurityElement.Escape(unitCost) + "</unitCost>";
            ///12.24 begin
            if (itemCategory != null) xml += "\r\n<itemCategory>" + SecurityElement.Escape(itemCategory) + "</itemCategory>";
            if (itemSubCategory != null) xml += "\r\n<itemSubCategory>" + SecurityElement.Escape(itemSubCategory) + "</itemSubCategory>";
            if (productId != null) xml += "\r\n<productId>" + SecurityElement.Escape(productId) + "</productId>";
            if (productName != null) xml += "\r\n<productName>" + SecurityElement.Escape(productName) + "</productName>";
            ///end  
            foreach (var detailTax in detailTaxes)
            {
                if (detailTax != null) xml += "\r\n<detailTax>" + detailTax.Serialize() + "</detailTax>";
            }
            if (shipmentId != null) xml += "\r\n<shipmentId>" + SecurityElement.Escape(shipmentId) + "</shipmentId>";
            foreach (var subscription in subscription)
            {
                if (subscription != null) xml += "\r\n<subscription>" + subscription.Serialize() + "</subscription>";
            }
            return xml;
        }

    }

    // The detailTax element is an optional child of both the enhancedData and lineItemData elements,
    // which you use to specify detailed tax information (for example, city or local tax).
    public partial class detailTax
    {
        private bool taxIncludedInTotalField;
        private bool taxIncludedInTotalSet;
        public bool taxIncludedInTotal
        {
            get { return taxIncludedInTotalField; }
            set { taxIncludedInTotalField = value; taxIncludedInTotalSet = true; }
        }
        private long taxAmountField;
        private bool taxAmountSet;
        public long taxAmount
        {
            get { return taxAmountField; }
            set { taxAmountField = value; taxAmountSet = true; }
        }
        public string taxRate;
        private taxTypeIdentifierEnum taxTypeIdentifierField;
        private bool taxTypeIdentifierSet;
        public taxTypeIdentifierEnum taxTypeIdentifier
        {
            get { return taxTypeIdentifierField; }
            set { taxTypeIdentifierField = value; taxTypeIdentifierSet = true; }
        }
        public string cardAcceptorTaxId;

        public string Serialize()
        {
            var xml = "";
            if (taxIncludedInTotalSet) xml += "\r\n<taxIncludedInTotal>" + taxIncludedInTotalField.ToString().ToLower() + "</taxIncludedInTotal>";
            if (taxAmountSet) xml += "\r\n<taxAmount>" + taxAmountField + "</taxAmount>";
            if (taxRate != null) xml += "\r\n<taxRate>" + SecurityElement.Escape(taxRate) + "</taxRate>";
            if (taxTypeIdentifierSet)
            {
                var type = taxTypeIdentifierField.GetType();
                var info = type.GetField(Enum.GetName(typeof(taxTypeIdentifierEnum), taxTypeIdentifierField));
                var att = (XmlEnumAttribute)info.GetCustomAttributes(typeof(XmlEnumAttribute), false)[0];
                //If there is an xmlattribute defined, return the name

                xml += "\r\n<taxTypeIdentifier>" + att.Name + "</taxTypeIdentifier>";
            }
            if (cardAcceptorTaxId != null) xml += "\r\n<cardAcceptorTaxId>" + SecurityElement.Escape(cardAcceptorTaxId) + "</cardAcceptorTaxId>";
            return xml;
        }
    }

    public partial class transactionTypeWithReportGroupAndPartial : transactionType
    {
        public string reportGroup;
        private bool partialField;
        protected bool partialSet;
        public bool partial
        {
            get { return partialField; }
            set { partialField = value; partialSet = true; }
        }
    }

    public partial class transactionTypeWithReportGroupAndRtp : transactionType
    {
        public string reportGroup;
        private bool rtpField;
        protected bool rtpSet;
        public bool rtp
        {
            get { return rtpField; }
            set { rtpField = value; rtpSet = true; }
        }
    }

    public partial class echeckForTokenType
    {
        public string accNum;
        public string routingNum;

        public string Serialize()
        {
            var xml = "";
            if (accNum != null) xml += "\r\n<accNum>" + SecurityElement.Escape(accNum) + "</accNum>";
            if (routingNum != null) xml += "\r\n<routingNum>" + SecurityElement.Escape(routingNum) + "</routingNum>";
            return xml;
        }
    }

    public partial class echeckType
    {
        private echeckAccountTypeEnum accTypeField;
        private bool accTypeSet;
        public echeckAccountTypeEnum accType
        {
            get { return accTypeField; }
            set { accTypeField = value; accTypeSet = true; }
        }

        public string accNum;
        public string routingNum;
        public string checkNum;
        public string ccdPaymentInformation;
        public string[] ctxPaymentInformation;
        public string echeckCustomerId;
        public string accountId;

        public string Serialize()
        {
            var xml = "";
            var accTypeName = accTypeField.ToString();
            var attributes =
                (XmlEnumAttribute[])typeof(echeckAccountTypeEnum).GetMember(accTypeField.ToString())[0].GetCustomAttributes(typeof(XmlEnumAttribute), false);
            if (attributes.Length > 0) accTypeName = attributes[0].Name;
            if (accTypeSet) xml += "\r\n<accType>" + accTypeName + "</accType>";
            if (accNum != null) xml += "\r\n<accNum>" + SecurityElement.Escape(accNum) + "</accNum>";
            if (routingNum != null) xml += "\r\n<routingNum>" + SecurityElement.Escape(routingNum) + "</routingNum>";
            if (checkNum != null) xml += "\r\n<checkNum>" + SecurityElement.Escape(checkNum) + "</checkNum>";
            if (ccdPaymentInformation != null) xml += "\r\n<ccdPaymentInformation>" + SecurityElement.Escape(ccdPaymentInformation) + "</ccdPaymentInformation>";
            if (ctxPaymentInformation != null)
            {
                xml += "\r\n<ctxPaymentInformation>";
                for (int i = 0; i < ctxPaymentInformation.Length; i++) xml += "\r\n<ctxPaymentDetail>" + SecurityElement.Escape(ctxPaymentInformation[i]) + "</ctxPaymentDetail>";
                xml += "\r\n</ctxPaymentInformation>";
            }
            if (echeckCustomerId != null) xml += "\r\n<echeckCustomerId>" + SecurityElement.Escape(echeckCustomerId) + "</echeckCustomerId>";
            if (accountId != null) xml += "\r\n<accountId>" + SecurityElement.Escape(accountId) + "</accountId>";
            return xml;
        }
        
    }

    public partial class echeckTokenType
    {
        public string cnpToken;
        public string routingNum;
        private echeckAccountTypeEnum accTypeField;
        private bool accTypeSet;
        public echeckAccountTypeEnum accType
        {
            get { return accTypeField; }
            set { accTypeField = value; accTypeSet = true; }
        }
        public string checkNum;

        public string Serialize()
        {
            var xml = "";
            var accTypeFieldName = accTypeField.ToString();
            var attributes =
                (XmlEnumAttribute[])typeof(echeckAccountTypeEnum).GetMember(accTypeField.ToString())[0].GetCustomAttributes(typeof(XmlEnumAttribute), false);
            if (attributes.Length > 0) accTypeFieldName = attributes[0].Name;
            if (cnpToken != null) xml += "\r\n<cnpToken>" + SecurityElement.Escape(cnpToken) + "</cnpToken>";
            if (routingNum != null) xml += "\r\n<routingNum>" + SecurityElement.Escape(routingNum) + "</routingNum>";
            if (accTypeSet) xml += "\r\n<accType>" + accTypeFieldName + "</accType>";
            if (checkNum != null) xml += "\r\n<checkNum>" + SecurityElement.Escape(checkNum) + "</checkNum>";
            return xml;
        }

    }

    public partial class pos
    {
        private posCapabilityTypeEnum capabilityField;
        private bool capabilitySet;
        public posCapabilityTypeEnum capability
        {
            get { return capabilityField; }
            set { capabilityField = value; capabilitySet = true; }
        }

        private posEntryModeTypeEnum entryModeField;
        private bool entryModeSet;
        public posEntryModeTypeEnum entryMode
        {
            get { return entryModeField; }
            set { entryModeField = value; entryModeSet = true; }
        }

        private posCardholderIdTypeEnum cardholderIdField;
        private bool cardholderIdSet;
        public posCardholderIdTypeEnum cardholderId
        {
            get { return cardholderIdField; }
            set { cardholderIdField = value; cardholderIdSet = true; }
        }
        public string terminalId;

        private posCatLevelEnum catLevelField;
        private bool catLevelSet;
        public posCatLevelEnum catLevel
        {
            get { return catLevelField; }
            set { catLevelField = value; catLevelSet = true; }
        }

        public string Serialize()
        {
            var xml = "";
            if (capabilitySet) xml += "\r\n<capability>" + capabilityField + "</capability>";
            if (entryModeSet) xml += "\r\n<entryMode>" + entryModeField + "</entryMode>";
            if (cardholderIdSet) xml += "\r\n<cardholderId>" + cardholderIdField + "</cardholderId>";
            if (terminalId != null) xml += "\r\n<terminalId>" + SecurityElement.Escape(terminalId) + "</terminalId>";
            if (catLevelSet) xml += "\r\n<catLevel>" + catLevelField.Serialize() + "</catLevel>";
            return xml;
        }

    }

    public partial class payPal
    {

        public string payerId;
        public string payerEmail;
        public string token;
        public string transactionId;

        public string Serialize()
        {
            var xml = "";
            if (payerId != null) xml += "\r\n<payerId>" + SecurityElement.Escape(payerId) + "</payerId>";
            if (payerEmail != null) xml += "\r\n<payerEmail>" + SecurityElement.Escape(payerEmail) + "</payerEmail>";
            if (token != null) xml += "\r\n<token>" + SecurityElement.Escape(token) + "</token>";
            if (transactionId != null) xml += "\r\n<transactionId>" + SecurityElement.Escape(transactionId) + "</transactionId>";
            return xml;
        }
    }

    public partial class merchantDataType
    {
        public string campaign;
        public string affiliate;
        public string merchantGroupingId;

        public string Serialize()
        {
            var xml = "";
            if (campaign != null) xml += "\r\n<campaign>" + SecurityElement.Escape(campaign) + "</campaign>";
            if (affiliate != null) xml += "\r\n<affiliate>" + SecurityElement.Escape(affiliate) + "</affiliate>";
            if (merchantGroupingId != null) xml += "\r\n<merchantGroupingId>" + SecurityElement.Escape(merchantGroupingId) + "</merchantGroupingId>";
            return xml;
        }
    }

    public partial class cardTokenType
    {
        public string cnpToken;
        public string tokenUrl;
        public string expDate;
        public string cardValidationNum;
	    private string authenticatedShopperID;
        private methodOfPaymentTypeEnum typeField;
        private bool typeSet;
        public methodOfPaymentTypeEnum type
        {
            get { return typeField; }
            set { typeField = value; typeSet = true; }
        }

        private string checkoutIdField;
        private bool checkoutIdSet;

        public string checkoutId
        {
            get { return checkoutIdField; }
            set { checkoutIdField = value;
                checkoutIdSet = true;
            }
        }

 

        public string Serialize()
        {
            var xml = "";
            if (cnpToken != null) xml += "\r\n<cnpToken>" + SecurityElement.Escape(cnpToken) + "</cnpToken>";
            else if (tokenUrl != null) xml += "\r\n<tokenUrl>" + SecurityElement.Escape(tokenUrl) + "</tokenUrl>";
            if (expDate != null) xml += "\r\n<expDate>" + SecurityElement.Escape(expDate) + "</expDate>";
            if (cardValidationNum != null) xml += "\r\n<cardValidationNum>" + SecurityElement.Escape(cardValidationNum) + "</cardValidationNum>";
            if (typeSet) xml += "\r\n<type>" + methodOfPaymentSerializer.Serialize(typeField) + "</type>";
            if (checkoutIdSet) xml += "\r\n<checkoutId>" + checkoutId + "</checkoutId>";
	    if (authenticatedShopperID != null) xml += "\r\n<authenticatednShopperID>" + authenticatedShopperID + "</authenticatedShopperID>";
            return xml;
        }
    }

    public partial class cardPaypageType
    {
        public string paypageRegistrationId;
        public string expDate;
        public string cardValidationNum;
        private methodOfPaymentTypeEnum typeField;
        private bool typeSet;
        public methodOfPaymentTypeEnum type
        {
            get { return typeField; }
            set { typeField = value; typeSet = true; }
        }

        public string Serialize()
        {
            var xml = "\r\n<paypageRegistrationId>" + SecurityElement.Escape(paypageRegistrationId) + "</paypageRegistrationId>";
            if (expDate != null) xml += "\r\n<expDate>" + SecurityElement.Escape(expDate) + "</expDate>";
            if (cardValidationNum != null) xml += "\r\n<cardValidationNum>" + SecurityElement.Escape(cardValidationNum) + "</cardValidationNum>";
            if (typeSet) xml += "\r\n<type>" + methodOfPaymentSerializer.Serialize(typeField) + "</type>";
            return xml;
        }
    }

    ///new 12.24
    public partial class additionalCOFData
    {
        public string totalPaymentCount;
        private paymentTypeEnum paymentTypeField;
        private bool paymentTypeSet;
        public paymentTypeEnum paymentType
        {
            get { return paymentTypeField; }
            set { paymentTypeField = value; paymentTypeSet = true; }
        }
        public string uniqueId;
        private frequencyOfMITEnum frequencyOfMITField;
        private bool frequencyOfMITSet;
        public frequencyOfMITEnum frequencyOfMIT
        {
            get { return frequencyOfMITField; }
            set { frequencyOfMITField = value; frequencyOfMITSet = true; }
        }
        public string validationReference;
        private int sequenceIndicatorField;
        private bool sequenceIndicatorSet;
        public int sequenceIndicator
        {
            get { return sequenceIndicatorField; }
            set { sequenceIndicatorField = value; sequenceIndicatorSet = true; }
        }

        public string Serialize()
        {
            var xml = "";
            if (totalPaymentCount != null) xml += "\r\n<totalPaymentCount>" + SecurityElement.Escape(totalPaymentCount) + "</totalPaymentCount>";
            var accTypeName = paymentTypeField.ToString();
            var attributes =
                (XmlEnumAttribute[])typeof(paymentTypeEnum).GetMember(paymentTypeField.ToString())[0].GetCustomAttributes(typeof(XmlEnumAttribute), false);
            if (attributes.Length > 0) accTypeName = attributes[0].Name;


            if (paymentTypeSet) xml += "\r\n<paymentType>" + accTypeName + "</paymentType>";///12.24

            ///if (paymentTypeSet) xml += "\r\n<paymentType>" + paymentTypeField + "</paymentType>";
            if (uniqueId != null) xml += "\r\n<uniqueId>" + SecurityElement.Escape(uniqueId) + "</uniqueId>";
            if (frequencyOfMITSet) xml += "\r\n<frequencyOfMIT>" + frequencyOfMITField + "</frequencyOfMIT>";
            if (validationReference != null) xml += "\r\n<validationReference>" + SecurityElement.Escape(validationReference) + "</validationReference>";
            if (sequenceIndicatorSet) xml += "\r\n<sequenceIndicator>" + sequenceIndicatorField + "</sequenceIndicator>";
            return xml;
        }
    }

    /// end

    public partial class billMeLaterRequest
    {
        private long bmlMerchantIdField;
        private bool bmlMerchantIdSet;
        public long bmlMerchantId
        {
            get { return bmlMerchantIdField; }
            set { bmlMerchantIdField = value; bmlMerchantIdSet = true; }
        }
        private long bmlProductTypeField;
        private bool bmlProductTypeSet;
        public long bmlProductType
        {
            get { return bmlProductTypeField; }
            set { bmlProductTypeField = value; bmlProductTypeSet = true; }
        }
        private int termsAndConditionsField;
        private bool termsAndConditionsSet;
        public int termsAndConditions
        {
            get { return termsAndConditionsField; }
            set { termsAndConditionsField = value; termsAndConditionsSet = true; }
        }
        public string preapprovalNumber;
        private int merchantPromotionalCodeField;
        private bool merchantPromotionalCodeSet;
        public int merchantPromotionalCode
        {
            get { return merchantPromotionalCodeField; }
            set { merchantPromotionalCodeField = value; merchantPromotionalCodeSet = true; }
        }
        public string virtualAuthenticationKeyPresenceIndicator;
        public string virtualAuthenticationKeyData;
        private int itemCategoryCodeField;
        private bool itemCategoryCodeSet;
        public int itemCategoryCode
        {
            get { return itemCategoryCodeField; }
            set { itemCategoryCodeField = value; itemCategoryCodeSet = true; }
        }

        public string Serialize()
        {
            var xml = "";
            if (bmlMerchantIdSet) xml += "\r\n<bmlMerchantId>" + bmlMerchantIdField + "</bmlMerchantId>";
            if (bmlProductTypeSet) xml += "\r\n<bmlProductType>" + bmlProductTypeField + "</bmlProductType>";
            if (termsAndConditionsSet) xml += "\r\n<termsAndConditions>" + termsAndConditionsField + "</termsAndConditions>";
            if (preapprovalNumber != null) xml += "\r\n<preapprovalNumber>" + SecurityElement.Escape(preapprovalNumber) + "</preapprovalNumber>";
            if (merchantPromotionalCodeSet) xml += "\r\n<merchantPromotionalCode>" + merchantPromotionalCodeField + "</merchantPromotionalCode>";
            if (virtualAuthenticationKeyPresenceIndicator != null) xml += "\r\n<virtualAuthenticationKeyPresenceIndicator>" + SecurityElement.Escape(virtualAuthenticationKeyPresenceIndicator) + "</virtualAuthenticationKeyPresenceIndicator>";
            if (virtualAuthenticationKeyData != null) xml += "\r\n<virtualAuthenticationKeyData>" + SecurityElement.Escape(virtualAuthenticationKeyData) + "</virtualAuthenticationKeyData>";
            if (itemCategoryCodeSet) xml += "\r\n<itemCategoryCode>" + itemCategoryCodeField + "</itemCategoryCode>";
            return xml;
        }

    }

    public partial class customBilling
    {
        public string phone;
        public string city;
        public string url;
        public string descriptor;
        public string Serialize()
        {
            var xml = "";
            if (phone != null) xml += "\r\n<phone>" + SecurityElement.Escape(phone) + "</phone>";
            else if (city != null) xml += "\r\n<city>" + SecurityElement.Escape(city) + "</city>";
            else if (url != null) xml += "\r\n<url>" + SecurityElement.Escape(url) + "</url>";
            if (descriptor != null) xml += "\r\n<descriptor>" + SecurityElement.Escape(descriptor) + "</descriptor>";
            return xml;
        }
    }

    public partial class amexAggregatorData
    {
        public string sellerId;
        public string sellerMerchantCategoryCode;
        public string Serialize()
        {
            var xml = "";
            xml += "\r\n<sellerId>" + SecurityElement.Escape(sellerId) + "</sellerId>";
            xml += "\r\n<sellerMerchantCategoryCode>" + SecurityElement.Escape(sellerMerchantCategoryCode) + "</sellerMerchantCategoryCode>";
            return xml;
        }

    }

    public partial class processingInstructions
    {
        private bool bypassVelocityCheckField;
        private bool bypassVelocityCheckSet;
        public bool bypassVelocityCheck
        {
            get { return bypassVelocityCheckField; }
            set { bypassVelocityCheckField = value; bypassVelocityCheckSet = true; }
        }

        public string Serialize()
        {
            var xml = "";
            if (bypassVelocityCheckSet) xml += "\r\n<bypassVelocityCheck>" + bypassVelocityCheckField.ToString().ToLower() + "</bypassVelocityCheck>";
            return xml;
        }
    }

    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.vantivcnp.com/schema")]
    public enum intervalType
    {
        ANNUAL,
        SEMIANNUAL,
        QUARTERLY,
        MONTHLY,
        WEEKLY
    }

    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://www.vantivcnp.com/schema")]
    public enum trialIntervalType
    {
        MONTH,
        DAY
    }

    public partial class fraudResult
    {
        public string Serialize()
        {
            var xml = "";
            if (avsResult != null) xml += "\r\n<avsResult>" + SecurityElement.Escape(avsResult) + "</avsResult>";
            if (cardValidationResult != null) xml += "\r\n<cardValidationResult>" + SecurityElement.Escape(cardValidationResult) + "</cardValidationResult>";
            if (authenticationResult != null) xml += "\r\n<authenticationResult>" + SecurityElement.Escape(authenticationResult) + "</authenticationResult>";
            if (advancedAVSResult != null) xml += "\r\n<advancedAVSResult>" + SecurityElement.Escape(advancedAVSResult) + "</advancedAVSResult>";
            return xml;
        }
    }


    public partial class authInformation
    {
        public DateTime authDate;
        public string authCode;
        public fraudResult fraudResult;
        private long authAmountField;
        private bool authAmountSet;
        public long authAmount
        {
            get { return authAmountField; }
            set { authAmountField = value; authAmountSet = true; }
        }

        public string Serialize()
        {
            var xml = "";
            if (authDate != null) xml += "\r\n<authDate>" + XmlUtil.toXsdDate(authDate) + "</authDate>";
            if (authCode != null) xml += "\r\n<authCode>" + SecurityElement.Escape(authCode) + "</authCode>";
            if (fraudResult != null) xml += "\r\n<fraudResult>" + fraudResult.Serialize() + "</fraudResult>";
            if (authAmountSet) xml += "\r\n<authAmount>" + authAmountField + "</authAmount>";
            return xml;
        }
    }

    public partial class recyclingRequestType
    {
        private recycleByTypeEnum recycleByField;
        private bool recycleBySet;
        public recycleByTypeEnum recycleBy
        {
            get { return recycleByField; }
            set { recycleByField = value; recycleBySet = true; }
        }
        public string recycleId;

        public string Serialize()
        {
            var xml = "";
            if (recycleBySet) xml += "\r\n<recycleBy>" + recycleByField + "</recycleBy>";
            if (recycleId != null) xml += "\r\n<recycleId>" + SecurityElement.Escape(recycleId) + "</recycleId>";
            return xml;
        }
    }

    public partial class cnpInternalRecurringRequest
    {
        public string subscriptionId;
        public string recurringTxnId;

        private bool finalPaymentField;
        private bool finalPaymentSet;
        public bool finalPayment
        {
            get { return finalPaymentField; }
            set { finalPaymentField = value; finalPaymentSet = true; }
        }

        public string Serialize()
        {
            var xml = "";
            if (subscriptionId != null) xml += "\r\n<subscriptionId>" + SecurityElement.Escape(subscriptionId) + "</subscriptionId>";
            if (recurringTxnId != null) xml += "\r\n<recurringTxnId>" + SecurityElement.Escape(recurringTxnId) + "</recurringTxnId>";
            if (finalPaymentSet) xml += "\r\n<finalPayment>" + finalPaymentField.ToString().ToLower() + "</finalPayment>";
            return xml;
        }
    }

    public partial class createDiscount
    {
        public string discountCode;
        public string name;
        public long amount;
        public DateTime startDate;
        public DateTime endDate;

        public string Serialize()
        {
            var xml = "";
            xml += "\r\n<discountCode>" + SecurityElement.Escape(discountCode) + "</discountCode>";
            xml += "\r\n<name>" + SecurityElement.Escape(name) + "</name>";
            xml += "\r\n<amount>" + amount + "</amount>";
            xml += "\r\n<startDate>" + XmlUtil.toXsdDate(startDate) + "</startDate>";
            xml += "\r\n<endDate>" + XmlUtil.toXsdDate(endDate) + "</endDate>";
            return xml;
        }
    }

    public partial class updateDiscount
    {
        public string discountCode;

        private string nameField;
        private bool nameSet;
        public string name
        {
            get { return nameField; }
            set { nameField = value; nameSet = true; }
        }

        private long amountField;
        private bool amountSet;
        public long amount
        {
            get { return amountField; }
            set { amountField = value; amountSet = true; }
        }

        private DateTime startDateField;
        private bool startDateSet;
        public DateTime startDate
        {
            get { return startDateField; }
            set { startDateField = value; startDateSet = true; }
        }

        private DateTime endDateField;
        private bool endDateSet;
        public DateTime endDate
        {
            get { return endDateField; }
            set { endDateField = value; endDateSet = true; }
        }

        public string Serialize()
        {
            var xml = "";
            xml += "\r\n<discountCode>" + SecurityElement.Escape(discountCode) + "</discountCode>";
            if (nameSet) xml += "\r\n<name>" + SecurityElement.Escape(nameField) + "</name>";
            if (amountSet) xml += "\r\n<amount>" + amountField + "</amount>";
            if (startDateSet) xml += "\r\n<startDate>" + XmlUtil.toXsdDate(startDateField) + "</startDate>";
            if (endDateSet) xml += "\r\n<endDate>" + XmlUtil.toXsdDate(endDateField) + "</endDate>";
            return xml;
        }
    }

    public partial class deleteDiscount
    {
        public string discountCode;

        public string Serialize()
        {
            var xml = "";
            xml += "\r\n<discountCode>" + SecurityElement.Escape(discountCode) + "</discountCode>";
            return xml;
        }
    }

    public partial class createAddOn
    {
        public string addOnCode;
        public string name;
        public long amount;
        public DateTime startDate;
        public DateTime endDate;

        public string Serialize()
        {
            var xml = "";
            xml += "\r\n<addOnCode>" + SecurityElement.Escape(addOnCode) + "</addOnCode>";
            xml += "\r\n<name>" + SecurityElement.Escape(name) + "</name>";
            xml += "\r\n<amount>" + amount + "</amount>";
            xml += "\r\n<startDate>" + XmlUtil.toXsdDate(startDate) + "</startDate>";
            xml += "\r\n<endDate>" + XmlUtil.toXsdDate(endDate) + "</endDate>";
            return xml;
        }
    }

    public partial class updateAddOn
    {
        public string addOnCode;

        private string nameField;
        private bool nameSet;
        public string name
        {
            get { return nameField; }
            set { nameField = value; nameSet = true; }
        }

        private long amountField;
        private bool amountSet;
        public long amount
        {
            get { return amountField; }
            set { amountField = value; amountSet = true; }
        }

        private DateTime startDateField;
        private bool startDateSet;
        public DateTime startDate
        {
            get { return startDateField; }
            set { startDateField = value; startDateSet = true; }
        }

        private DateTime endDateField;
        private bool endDateSet;
        public DateTime endDate
        {
            get { return endDateField; }
            set { endDateField = value; endDateSet = true; }
        }

        public string Serialize()
        {
            var xml = "";
            xml += "\r\n<addOnCode>" + SecurityElement.Escape(addOnCode) + "</addOnCode>";
            if (nameSet) xml += "\r\n<name>" + SecurityElement.Escape(nameField) + "</name>";
            if (amountSet) xml += "\r\n<amount>" + amountField + "</amount>";
            if (startDateSet) xml += "\r\n<startDate>" + XmlUtil.toXsdDate(startDateField) + "</startDate>";
            if (endDateSet) xml += "\r\n<endDate>" + XmlUtil.toXsdDate(endDateField) + "</endDate>";
            return xml;
        }
    }

    public partial class deleteAddOn
    {
        public string addOnCode;

        public string Serialize()
        {
            var xml = "";
            xml += "\r\n<addOnCode>" + SecurityElement.Escape(addOnCode) + "</addOnCode>";
            return xml;
        }
    }

    public partial class subscription
    {
        public string planCode;
        private bool numberOfPaymentsSet;
        private int numberOfPaymentsField;
        public int numberOfPayments
        {
            get { return numberOfPaymentsField; }
            set { numberOfPaymentsField = value; numberOfPaymentsSet = true; }
        }
        private bool startDateSet;
        private DateTime startDateField;
        public DateTime startDate
        {
            get { return startDateField; }
            set { startDateField = value; startDateSet = true; }
        }
        private bool amountSet;
        private long amountField;
        public long amount
        {
            get { return amountField; }
            set { amountField = value; amountSet = true; }
        }

        public List<createDiscount> createDiscounts;
        public List<createAddOn> createAddOns;

        public subscription()
        {
            createDiscounts = new List<createDiscount>();
            createAddOns = new List<createAddOn>();
        }


        public string Serialize()
        {
            var xml = "";
            xml += "\r\n<planCode>" + planCode + "</planCode>";
            if (numberOfPaymentsSet) xml += "\r\n<numberOfPayments>" + numberOfPayments + "</numberOfPayments>";
            if (startDateSet) xml += "\r\n<startDate>" + XmlUtil.toXsdDate(startDateField) + "</startDate>";
            if (amountSet) xml += "\r\n<amount>" + amountField + "</amount>";
            foreach (var createDiscount in createDiscounts)
            {
                xml += "\r\n<createDiscount>" + createDiscount.Serialize() + "\r\n</createDiscount>";
            }
            foreach (var createAddOn in createAddOns)
            {
                xml += "\r\n<createAddOn>" + createAddOn.Serialize() + "\r\n</createAddOn>";
            }

            return xml;
        }
    }

    public partial class filteringType
    {
        private bool prepaidField;
        private bool prepaidSet;
        public bool prepaid
        {
            get { return prepaidField; }
            set { prepaidField = value; prepaidSet = true; }
        }

        private bool internationalField;
        private bool internationalSet;
        public bool international
        {
            get { return internationalField; }
            set { internationalField = value; internationalSet = true; }
        }

        private bool chargebackField;
        private bool chargebackSet;
        public bool chargeback
        {
            get { return chargebackField; }
            set { chargebackField = value; chargebackSet = true; }
        }

        public string Serialize()
        {
            var xml = "";
            if (prepaidSet) xml += "\r\n<prepaid>" + prepaidField.ToString().ToLower() + "</prepaid>";
            if (internationalSet) xml += "\r\n<international>" + internationalField.ToString().ToLower() + "</international>";
            if (chargebackSet) xml += "\r\n<chargeback>" + chargebackField.ToString().ToLower() + "</chargeback>";
            return xml;
        }

    }

    public partial class healthcareIIAS
    {
        public healthcareAmounts healthcareAmounts;
        private IIASFlagType IIASFlagField;
        private bool IIASFlagSet;
        public IIASFlagType IIASFlag
        {
            get { return IIASFlagField; }
            set { IIASFlagField = value; IIASFlagSet = true; }
        }

        public string Serialize()
        {
            var xml = "";
            if (healthcareAmounts != null) xml += "\r\n<healthcareAmounts>" + healthcareAmounts.Serialize() + "</healthcareAmounts>";
            if (IIASFlagSet) xml += "\r\n<IIASFlag>" + IIASFlagField + "</IIASFlag>";
            return xml;
        }
    }

    public partial class lodgingInfo
    {
        public string hotelFolioNumber;
        public DateTime checkInDateField;
        public bool checkInDateSet;
        public DateTime checkInDate
        {
            get
            {
                return checkInDateField;
            }
            set
            {
                checkInDateField = value;
                checkInDateSet = true;
            }
        }

        public DateTime checkOutDateField;
        public bool checkOutDateSet;
        public DateTime checkOutDate
        {
            get
            {
                return checkOutDateField;
            }
            set
            {
                checkOutDateField = value;
                checkOutDateSet = true;
            }
        }

        private int durationField;
        private bool durationSet;
        public int duration
        {
            get
            {
                return durationField;
            }
            set
            {
                durationField = value;
                durationSet = true;
            }
        }

        public string customerServicePhone;
        public lodgingProgramCodeType programCode;

        private int roomRateField;
        private bool roomRateSet;
        public int roomRate
        {
            get
            {
                return roomRateField;
            }
            set
            {
                roomRateField = value;
                roomRateSet = true;
            }
        }


        private int roomTaxField;
        private bool roomTaxSet;
        public int roomTax
        {
            get
            {
                return roomTaxField;
            }
            set
            {
                roomTaxField = value;
                roomTaxSet = true;
            }
        }

        private int numAdultsField;
        private bool numAdultsSet;
        public int numAdults
        {
            get
            {
                return numAdultsField;
            }
            set
            {
                numAdultsField = value;
                numAdultsSet = true;
            }
        }

        /// new 12.24
        private int sequenceIndicatorField;
        private bool sequenceIndicatorSet;
        public int sequenceIndicator
        {
            get 
            { 
                return sequenceIndicatorField;
            }
            set
            {
                sequenceIndicatorField = value;
                sequenceIndicatorSet = true;
            }
        }

        public string propertyLocalPhone;

        private bool fireSafetyIndicatorField;
        private bool fireSafetyIndicatorSet;
        public bool fireSafetyIndicator
        {
            get
            {
                return fireSafetyIndicatorField;
            }
            set
            {
                fireSafetyIndicatorField = value;
                fireSafetyIndicatorSet = true;
            }
        }

        public List<lodgingCharge> lodgingCharges;
        public lodgingInfo()
        {

            lodgingCharges = new List<lodgingCharge>();

        }
        // 12.25 start
        private string bookingIDField;
        private bool bookingIDSet;
        public string bookingID
        {
            get 
            { 
                return bookingIDField; 
            }
            set 
            { 
                bookingIDField = value; 
                bookingIDSet = true;
            }
        }
        private string passengerNameField;
        private bool passengerNameSet;
        public string passengerName
        {
            get 
            { 
                return passengerNameField; 
            }
            set 
            { 
                passengerNameField = value; 
                passengerNameSet = true;
            }
        }
        public propertyAddress propertyAddress;
        private travelPackageIndicatorEnum travelPackageIndicatorField;
        private bool travelPackageIndicatorSet;
        public travelPackageIndicatorEnum travelPackageIndicator
        {
            get 
            {
                return travelPackageIndicatorField; 
            }
            set
            {
                travelPackageIndicatorField = value;
                travelPackageIndicatorSet = true;
            }
        }
        private string smokingPreferenceField;
        private bool smokingPreferenceSet;
        public string smokingPreference
        {
            get
            {
                return smokingPreferenceField;
            }
            set
            {
                smokingPreferenceField = value;
                smokingPreferenceSet = true;
            }
        }
        private int numberOfRoomsField;
        private bool numberOfRoomsSet;
        public int numberOfRooms
        {
            get { return numberOfRoomsField; }
            set { numberOfRoomsField = value; numberOfRoomsSet = true; }
        }

        private string tollFreePhoneNumberField;
        private bool tollFreePhoneNumberSet;
        public string tollFreePhoneNumber
        {
            get 
            { 
                return tollFreePhoneNumberField; 
            }
            set
            {
                tollFreePhoneNumberField = value;
                tollFreePhoneNumberSet = true;
            }
        }
        //12.25 end

        

       /* public lodgingInfo()
        {

            lodgingCharges = new List<lodgingCharge>();

        }*/

        public string Serialize()
        {
            var xml = "";
            if (hotelFolioNumber != null) xml += "\r\n<hotelFolioNumber>" + SecurityElement.Escape(hotelFolioNumber) + "</hotelFolioNumber>";
            if (checkInDateSet) xml += "\r\n<checkInDate>" + checkInDate.ToString("yyyy-MM-dd") + "</checkInDate>";
            if (checkOutDateSet) xml += "\r\n<checkOutDate>" + checkOutDate.ToString("yyyy-MM-dd") + "</checkOutDate>";
            if (durationSet) xml += "\r\n<duration>" + durationField + "</duration>";
            if (customerServicePhone != null) xml += "\r\n<customerServicePhone>" + SecurityElement.Escape(customerServicePhone) + "</customerServicePhone>";
            xml += "\r\n<programCode>" + programCode + "</programCode>";
            if (roomRateSet) xml += "\r\n<roomRate>" + roomRateField + "</roomRate>";
            if (roomTaxSet) xml += "\r\n<roomTax>" + roomTaxField + "</roomTax>";
            if (numAdultsSet) xml += "\r\n<numAdults>" + numAdultsField + "</numAdults>";
            if (sequenceIndicatorSet) xml += "\r\n<sequenceIndicator>" + sequenceIndicatorField + "</sequenceIndicator>";///12.24
            if (propertyLocalPhone != null) xml += "\r\n<propertyLocalPhone>" + propertyLocalPhone + "</propertyLocalPhone>";
            if (fireSafetyIndicatorSet) xml += "\r\n<fireSafetyIndicator>" + fireSafetyIndicatorField + "</fireSafetyIndicator>";
            foreach (var lodgingCharge in lodgingCharges)
            {
                xml += "\r\n<lodgingCharge>" + lodgingCharge.Serialize() + "</lodgingCharge>";
            }
            if (bookingIDSet) xml += "\r\n<bookingID>" + bookingIDField + "</bookingID>";
            if (passengerNameSet) xml += "\r\n<passengerName>" + passengerNameField + "</passengerName>";
            if (propertyAddress != null)
            {
                xml += "\r\n<propertyAddress>" + propertyAddress.Serialize() + "\r\n</propertyAddress>";
            }
            if (travelPackageIndicatorSet) xml += "\r\n<travelPackageIndicator>" + travelPackageIndicatorField + "</travelPackageIndicator>";
            if (smokingPreferenceSet) xml += "\r\n<smokingPreference>" + smokingPreferenceField + "</smokingPreference>";
            if (numberOfRoomsSet) xml += "\r\n<numberOfRooms>" + numberOfRoomsField + "</numberOfRooms>";
            if (tollFreePhoneNumberSet) xml += "\r\n<tollFreePhoneNumber>" + tollFreePhoneNumberField + "</tollFreePhoneNumber>";
            return xml;
        }
    }

    public enum lodgingProgramCodeType
    {
        LODGING = 0,
        NOSHOW,
        ADVANCEDDEPOSIT,
    }

    public partial class lodgingCharge
    {

        private lodgingExtraChargeEnum nameField;
        private bool nameSet;
        public lodgingExtraChargeEnum name
        {
            get { return nameField; }
            set { nameField = value; nameSet = true; }
        }

        public string Serialize()
        {
            var xml = "";

            if (nameSet) xml += "\r\n<name>" + nameField + "</name>";
            return xml;

        }

    }

    public enum lodgingExtraChargeEnum
    {
        OTHER = 0,
        RESTAURANT,
        GIFTSHOP,
        MINIBAR,
        TELEPHONE,
        LAUNDRY,

    }


    public partial class recurringRequest
    {
        public subscription subscription;

        public string Serialize()
        {
            var xml = "";
            if (subscription != null) xml += "\r\n<subscription>" + subscription.Serialize() + "\r\n</subscription>";
            return xml;
        }
    }

    public partial class healthcareAmounts
    {
        private int totalHealthcareAmountField;
        private bool totalHealthcareAmountSet;
        public int totalHealthcareAmount
        {
            get { return totalHealthcareAmountField; }
            set { totalHealthcareAmountField = value; totalHealthcareAmountSet = true; }
        }

        private int RxAmountField;
        private bool RxAmountSet;
        public int RxAmount
        {
            get { return RxAmountField; }
            set { RxAmountField = value; RxAmountSet = true; }
        }

        private int visionAmountField;
        private bool visionAmountSet;
        public int visionAmount
        {
            get { return visionAmountField; }
            set { visionAmountField = value; visionAmountSet = true; }
        }

        private int clinicOtherAmountField;
        private bool clinicOtherAmountSet;
        public int clinicOtherAmount
        {
            get { return clinicOtherAmountField; }
            set { clinicOtherAmountField = value; clinicOtherAmountSet = true; }
        }

        private int dentalAmountField;
        private bool dentalAmountSet;
        public int dentalAmount
        {
            get { return dentalAmountField; }
            set { dentalAmountField = value; dentalAmountSet = true; }
        }


	private int copayAmountField;
	private bool copayAmountSet;
	public int copayAmount
	{
		get { return copayAmountField; }
		set { copayAmountField = value; copayAmountSet = true; }
	}
	
        public string Serialize()
        {
            var xml = "";
            if (totalHealthcareAmountSet) xml += "\r\n<totalHealthcareAmount>" + totalHealthcareAmountField + "</totalHealthcareAmount>";
            if (RxAmountSet) xml += "\r\n<RxAmount>" + RxAmountField + "</RxAmount>";
            if (visionAmountSet) xml += "\r\n<visionAmount>" + visionAmountField + "</visionAmount>";
            if (clinicOtherAmountSet) xml += "\r\n<clinicOtherAmount>" + clinicOtherAmountField + "</clinicOtherAmount>";
            if (dentalAmountSet) xml += "\r\n<dentalAmount>" + dentalAmountField + "</dentalAmount>";
	    if (copayAmountSet) xml += "\r\n<copayAmount>" + copayAmountField + "</copayAmount>";
            return xml;
        }
    }

    public sealed class orderSourceType
    {
        public static readonly orderSourceType ecommerce = new orderSourceType("ecommerce");
        public static readonly orderSourceType installment = new orderSourceType("installment");
        public static readonly orderSourceType mailorder = new orderSourceType("mailorder");
        public static readonly orderSourceType recurring = new orderSourceType("recurring");
        public static readonly orderSourceType retail = new orderSourceType("retail");
        public static readonly orderSourceType telephone = new orderSourceType("telephone");
        public static readonly orderSourceType item3dsAuthenticated = new orderSourceType("3dsAuthenticated");
        public static readonly orderSourceType item3dsAttempted = new orderSourceType("3dsAttempted");
        public static readonly orderSourceType recurringtel = new orderSourceType("recurringtel");
        public static readonly orderSourceType echeckppd = new orderSourceType("echeckppd");
        public static readonly orderSourceType applepay = new orderSourceType("applepay");
        public static readonly orderSourceType androidpay = new orderSourceType("androidpay");

        private orderSourceType(string value) { this.value = value; }
        public string Serialize() { return value; }
        private string value;
    }

    public partial class contact
    {

        public string name;
        public string firstName;
        public string middleInitial;
        public string lastName;
        public string companyName;
        public string addressLine1;
        public string addressLine2;
        public string addressLine3;
        public string city;
        public string state;
        public string zip;
        private countryTypeEnum countryField;
        private bool countrySpecified;
        public countryTypeEnum country
        {
            get { return countryField; }
            set { countryField = value; countrySpecified = true; }
        }
        public string email;
        public string phone;
        public string sellerId; ///new 12.24
        public string url; /// new 12.24


        public string Serialize()
        {
            var xml = "";
            if (name != null) xml += "\r\n<name>" + SecurityElement.Escape(name) + "</name>";
            if (firstName != null) xml += "\r\n<firstName>" + SecurityElement.Escape(firstName) + "</firstName>";
            if (middleInitial != null) xml += "\r\n<middleInitial>" + SecurityElement.Escape(middleInitial) + "</middleInitial>";
            if (lastName != null) xml += "\r\n<lastName>" + SecurityElement.Escape(lastName) + "</lastName>";
            if (companyName != null) xml += "\r\n<companyName>" + SecurityElement.Escape(companyName) + "</companyName>";
            if (addressLine1 != null) xml += "\r\n<addressLine1>" + SecurityElement.Escape(addressLine1) + "</addressLine1>";
            if (addressLine2 != null) xml += "\r\n<addressLine2>" + SecurityElement.Escape(addressLine2) + "</addressLine2>";
            if (addressLine3 != null) xml += "\r\n<addressLine3>" + SecurityElement.Escape(addressLine3) + "</addressLine3>";
            if (city != null) xml += "\r\n<city>" + SecurityElement.Escape(city) + "</city>";
            if (state != null) xml += "\r\n<state>" + SecurityElement.Escape(state) + "</state>";
            if (zip != null) xml += "\r\n<zip>" + SecurityElement.Escape(zip) + "</zip>";
            if (countrySpecified) xml += "\r\n<country>" + countryField + "</country>";
            if (email != null) xml += "\r\n<email>" + SecurityElement.Escape(email) + "</email>";
            if (phone != null) xml += "\r\n<phone>" + SecurityElement.Escape(phone) + "</phone>";
            if (sellerId != null) xml += "\r\n<sellerId>" + SecurityElement.Escape(sellerId) + "</sellerId>"; ///12.24
            if (url != null) xml += "\r\n<url>" + SecurityElement.Escape(url) + "</url>";///12.24
            return xml;
        }
    }

    public enum countryTypeEnum
    {

        /// <remarks/>
        USA,
        AF,
        AX,
        AL,
        DZ,
        AS,
        AD,
        AO,
        AI,
        AQ,
        AG,
        AR,
        AM,
        AW,
        AU,
        AT,
        AZ,
        BS,
        BH,
        BD,
        BB,
        BY,
        BE,
        BZ,
        BJ,
        BM,
        BT,
        BO,
        BQ,
        BA,
        BW,
        BV,
        BR,
        IO,
        BN,
        BG,
        BF,
        BI,
        KH,
        CM,
        CA,
        CV,
        KY,
        CF,
        TD,
        CL,
        CN,
        CX,
        CC,
        CO,
        KM,
        CG,
        CD,
        CK,
        CR,
        CI,
        HR,
        CU,
        CW,
        CY,
        CZ,
        DK,
        DJ,
        DM,
        DO,
        TL,
        EC,
        EG,
        SV,
        GQ,
        ER,
        EE,
        ET,
        FK,
        FO,
        FJ,
        FI,
        FR,
        GF,
        PF,
        TF,
        GA,
        GM,
        GE,
        DE,
        GH,
        GI,
        GR,
        GL,
        GD,
        GP,
        GU,
        GT,
        GG,
        GN,
        GW,
        GY,
        HT,
        HM,
        HN,
        HK,
        HU,
        IS,
        IN,
        ID,
        IR,
        IQ,
        IE,
        IM,
        IL,
        IT,
        JM,
        JP,
        JE,
        JO,
        KZ,
        KE,
        KI,
        KP,
        KR,
        KW,
        KG,
        LA,
        LV,
        LB,
        LS,
        LR,
        LY,
        LI,
        LT,
        LU,
        MO,
        MK,
        MG,
        MW,
        MY,
        MV,
        ML,
        MT,
        MH,
        MQ,
        MR,
        MU,
        YT,
        MX,
        FM,
        MD,
        MC,
        MN,
        MS,
        MA,
        MZ,
        MM,
        NA,
        NR,
        NP,
        NL,
        AN,
        NC,
        NZ,
        NI,
        NE,
        NG,
        NU,
        NF,
        MP,
        NO,
        OM,
        PK,
        PW,
        PS,
        PA,
        PG,
        PY,
        PE,
        PH,
        PN,
        PL,
        PT,
        PR,
        QA,
        RE,
        RO,
        RU,
        RW,
        BL,
        KN,
        LC,
        MF,
        VC,
        WS,
        SM,
        ST,
        SA,
        SN,
        SC,
        SL,
        SG,
        SX,
        SK,
        SI,
        SB,
        SO,
        ZA,
        GS,
        ES,
        LK,
        SH,
        PM,
        SD,
        SR,
        SJ,
        SZ,
        SE,
        CH,
        SY,
        TW,
        TJ,
        TZ,
        TH,
        TG,
        TK,
        TO,
        TT,
        TN,
        TR,
        TM,
        TC,
        TV,
        UG,
        UA,
        AE,
        GB,
        US,
        UM,
        UY,
        UZ,
        VU,
        VA,
        VE,
        VN,
        VG,
        VI,
        WF,
        EH,
        YE,
        ZM,
        ZW,
        RS,
        ME,
    }

    public enum stateTypeEnum
    {

        /// <remarks/>
        AL,
        AK,
        AZ,
        AR,
        CA,
        CO,
        CT,
        DE,
        DC,
        FL,
        GA,
        HI,
        ID,
        IL,
        IN,
        IA,
        KS,
        KY,
        LA,
        ME,
        MD,
        MA,
        MI,
        MN,
        MS,
        MO,
        MT,
        NE,
        NV,
        NH,
        NJ,
        NM,
        NY,
        NC,
        ND,
        OH,
        OK,
        OR,
        PA,
        RI,
        SC,
        SD,
        TN,
        TX,
        UT,
        VT,
        VA,
        WA,
        WV,
        WI,
        WY,
        AB,
        BC,
        MB,
        NB,
        NL,
        NT,
        NS,
        ON,
        PE,
        QC,
        SK,
        YT,
        NU,
    }

    public partial class addressType
    {        
        public string addressLine1;
        public string addressLine2;
        public string addressLine3;
        public string city;
        public string state;
        public string zip;
        private countryTypeEnum countryField;
        private bool countrySpecified;
        public countryTypeEnum country
        {
            get { return countryField; }
            set { countryField = value; countrySpecified = true; }
        }        

        public string Serialize()
        {
            var xml = "";            
            if (addressLine1 != null) xml += "\r\n<addressLine1>" + SecurityElement.Escape(addressLine1) + "</addressLine1>";
            if (addressLine2 != null) xml += "\r\n<addressLine2>" + SecurityElement.Escape(addressLine2) + "</addressLine2>";
            if (addressLine3 != null) xml += "\r\n<addressLine3>" + SecurityElement.Escape(addressLine3) + "</addressLine3>";
            if (city != null) xml += "\r\n<city>" + SecurityElement.Escape(city) + "</city>";
            if (state != null) xml += "\r\n<state>" + SecurityElement.Escape(state) + "</state>";
            if (zip != null) xml += "\r\n<zip>" + SecurityElement.Escape(zip) + "</zip>";
            if (countrySpecified) xml += "\r\n<country>" + countryField + "</country>";            
            return xml;
        }
    }

    public partial class advancedFraudChecksType
    {
        [Obsolete("threatMetrixSessionId is deprecated, please use webSessionId instead.")]
        public string threatMetrixSessionId;
        public string webSessionId;
        private string customAttribute1Field;
        private bool customAttribute1Set;
        public string customAttribute1
        {
            get { return customAttribute1Field; }
            set { customAttribute1Field = value; customAttribute1Set = true; }
        }
        private string customAttribute2Field;
        private bool customAttribute2Set;
        public string customAttribute2
        {
            get { return customAttribute2Field; }
            set { customAttribute2Field = value; customAttribute2Set = true; }
        }
        private string customAttribute3Field;
        private bool customAttribute3Set;
        public string customAttribute3
        {
            get { return customAttribute3Field; }
            set { customAttribute3Field = value; customAttribute3Set = true; }
        }
        private string customAttribute4Field;
        private bool customAttribute4Set;
        public string customAttribute4
        {
            get { return customAttribute4Field; }
            set { customAttribute4Field = value; customAttribute4Set = true; }
        }
        private string customAttribute5Field;
        private bool customAttribute5Set;
        public string customAttribute5
        {
            get { return customAttribute5Field; }
            set { customAttribute5Field = value; customAttribute5Set = true; }
        }

        public string Serialize()
        {
            var xml = "";
            if (threatMetrixSessionId != null) xml += "\r\n<threatMetrixSessionId>" + SecurityElement.Escape(threatMetrixSessionId) + "</threatMetrixSessionId>";
            else if (webSessionId != null) xml += "\r\n<webSessionId>" + SecurityElement.Escape(webSessionId) + "</webSessionId>";
            if (customAttribute1Set) xml += "\r\n<customAttribute1>" + SecurityElement.Escape(customAttribute1Field) + "</customAttribute1>";
            if (customAttribute2Set) xml += "\r\n<customAttribute2>" + SecurityElement.Escape(customAttribute2Field) + "</customAttribute2>";
            if (customAttribute3Set) xml += "\r\n<customAttribute3>" + SecurityElement.Escape(customAttribute3Field) + "</customAttribute3>";
            if (customAttribute4Set) xml += "\r\n<customAttribute4>" + SecurityElement.Escape(customAttribute4Field) + "</customAttribute4>";
            if (customAttribute5Set) xml += "\r\n<customAttribute5>" + SecurityElement.Escape(customAttribute5Field) + "</customAttribute5>";
            return xml;
        }
    }

    public partial class mposType
    {
        public string ksn;
        public string formatId;
        public string encryptedTrack;
        public int track1Status;
        public int track2Status;

        public string Serialize()
        {
            var xml = "";
            if (ksn != null)
            {
                xml += "\r\n<ksn>" + ksn + "</ksn>";
            }
            if (formatId != null)
            {
                xml += "\r\n<formatId>" + formatId + "</formatId>";
            }
            if (encryptedTrack != null)
            {
                xml += "\r\n<encryptedTrack>" + SecurityElement.Escape(encryptedTrack) + "</encryptedTrack>";
            }
            if (track1Status == 0 || track1Status == 1)
            {
                xml += "\r\n<track1Status>" + track1Status + "</track1Status>";
            }
            if (track2Status == 0 || track2Status == 1)
            {
                xml += "\r\n<track2Status>" + track2Status + "</track2Status>";
            }

            return xml;
        }
    }

    public partial class cardType
    {
        public methodOfPaymentTypeEnum type;
        public string number;
        public string expDate;
        public string track;
        public string cardValidationNum;
        public string pin;

        public string Serialize()
        {
            var xml = "";
            if (track == null)
            {
                xml += "\r\n<type>" + methodOfPaymentSerializer.Serialize(type) + "</type>";
                if (number != null)
                {
                    xml += "\r\n<number>" + SecurityElement.Escape(number) + "</number>";
                }
                if (expDate != null)
                {
                    xml += "\r\n<expDate>" + SecurityElement.Escape(expDate) + "</expDate>";
                }
            }
            else
            {
                xml += "\r\n<track>" + SecurityElement.Escape(track) + "</track>";
            }
            if (cardValidationNum != null)
            {
                xml += "\r\n<cardValidationNum>" + SecurityElement.Escape(cardValidationNum) + "</cardValidationNum>";
            }
            if (pin != null)
            {
                xml += "\r\n<pin>" + pin + "</pin>";
            }
            return xml;
        }
    }

    public partial class giftCardCardType
    {
        public methodOfPaymentTypeEnum type;
        public string number;
        public string expDate;
        public string track;
        public string cardValidationNum;
        public string pin;

        public string Serialize()
        {
            var xml = "";
            if (track == null)
            {
                xml += "\r\n<type>" + methodOfPaymentSerializer.Serialize(type) + "</type>";
                if (number != null)
                {
                    xml += "\r\n<number>" + SecurityElement.Escape(number) + "</number>";
                }
                if (expDate != null)
                {
                    xml += "\r\n<expDate>" + SecurityElement.Escape(expDate) + "</expDate>";
                }
            }
            else
            {
                xml += "\r\n<track>" + SecurityElement.Escape(track) + "</track>";
            }
            if (cardValidationNum != null)
            {
                xml += "\r\n<cardValidationNum>" + SecurityElement.Escape(cardValidationNum) + "</cardValidationNum>";
            }
            if (pin != null)
            {
                xml += "\r\n<pin>" + pin + "</pin>";
            }
            return xml;
        }
    }

    public partial class virtualGiftCardType
    {
        public int accountNumberLength
        {
            get { return accountNumberLengthField; }
            set { accountNumberLengthField = value; accountNumberLengthSet = true; }
        }
        private int accountNumberLengthField;
        private bool accountNumberLengthSet;

        public string giftCardBin;

        public string Serialize()
        {
            var xml = "";
            if (accountNumberLengthSet) xml += "\r\n<accountNumberLength>" + accountNumberLengthField + "</accountNumberLength>";
            if (giftCardBin != null) xml += "\r\n<giftCardBin>" + SecurityElement.Escape(giftCardBin) + "</giftCardBin>";
            return xml;
        }

    }

    public class accountUpdate : transactionTypeWithReportGroup
    {
        public string orderId;
        public cardType card;
        public cardTokenType token;

        public override string Serialize()
        {
            var xml = "\r\n<accountUpdate ";

            if (id != null)
            {
                xml += "id=\"" + SecurityElement.Escape(id) + "\" ";
            }
            if (customerId != null)
            {
                xml += "customerId=\"" + SecurityElement.Escape(customerId) + "\" ";
            }
            xml += "reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";

            xml += "\r\n<orderId>" + SecurityElement.Escape(orderId) + "</orderId>";

            if (card != null)
            {
                xml += "\r\n<card>";
                xml += card.Serialize();
                xml += "\r\n</card>";
            }
            else if (token != null)
            {
                xml += "\r\n<token>";
                xml += token.Serialize();
                xml += "\r\n</token>";
            }

            xml += "\r\n</accountUpdate>";

            return xml;
        }
    }

    public partial class finicityUrlRequest : transactionTypeWithReportGroup
    {
        public string firstName;
        public string lastName;
        public string phoneNumber;
        public string email;
    
        public override string Serialize()
        {
            var xml = "\r\n<finicityUrlRequest";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            xml += "\r\n<firstName>" + SecurityElement.Escape(firstName) + "</firstName>";
            xml += "\r\n<lastName>" + lastName + "</lastName>";
            xml += "\r\n<phoneNumber>" + phoneNumber + "</phoneNumber>";
            xml += "\r\n<email>" + email + "</email>";
            xml += "\r\n</finicityUrlRequest>";
            return xml;
        }

    }

    public partial class finicityAccountRequest : transactionTypeWithReportGroup
    {
        public string echeckCustomerId;
      
        public override string Serialize()
        {
            var xml = "\r\n<finicityAccountRequest";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            xml += "\r\n<echeckCustomerId>" + echeckCustomerId + "</echeckCustomerId>";
            xml += "\r\n</finicityAccountRequest>";
            return xml;
        }

    }

    public class accountUpdateFileRequestData
    {
        public string merchantId;
        public accountUpdateFileRequestData()
        {
            merchantId = Properties.Settings.Default.merchantId;
        }
        public accountUpdateFileRequestData(Dictionary<string, string> config)
        {
            merchantId = config["merchantId"];
        }
        public DateTime postDay; //yyyy-MM-dd

        public string Serialize()
        {
            var xml = "\r\n<merchantId>" + SecurityElement.Escape(merchantId) + "</merchantId>";

            if (postDay != null)
            {
                xml += "\r\n<postDay>" + postDay.ToString("yyyy-MM-dd") + "</postDay>";
            }

            return xml;
        }
    }

    public partial class applepayType
    {
        public string data;
        public applepayHeaderType header;
        public string signature;
        public string version;

        public string Serialize()
        {
            var xml = "";
            if (data != null) xml += "\r\n<data>" + SecurityElement.Escape(data) + "</data>";
            if (header != null) xml += "\r\n<header>" + header.Serialize() + "</header>";
            if (signature != null) xml += "\r\n<signature>" + SecurityElement.Escape(signature) + "</signature>";
            if (version != null) xml += "\r\n<version>" + SecurityElement.Escape(version) + "</version>";
            return xml;
        }
    }

    public partial class applepayHeaderType
    {
        public string applicationData;
        public string ephemeralPublicKey;
        public string publicKeyHash;
        public string transactionId;

        public string Serialize()
        {
            var xml = "";
            if (applicationData != null) xml += "\r\n<applicationData>" + SecurityElement.Escape(applicationData) + "</applicationData>";
            if (ephemeralPublicKey != null) xml += "\r\n<ephemeralPublicKey>" + SecurityElement.Escape(ephemeralPublicKey) + "</ephemeralPublicKey>";
            if (publicKeyHash != null) xml += "\r\n<publicKeyHash>" + SecurityElement.Escape(publicKeyHash) + "</publicKeyHash>";
            if (transactionId != null) xml += "\r\n<transactionId>" + SecurityElement.Escape(transactionId) + "</transactionId>";
            return xml;
        }
    }

    public partial class wallet
    {
        public walletWalletSourceType walletSourceType;
        public string walletSourceTypeId;

        public string Serialize()
        {
            var xml = "";
            xml += "\r\n<walletSourceType>" + walletSourceType + "</walletSourceType>";
            if (walletSourceTypeId != null) xml += "\r\n<walletSourceTypeId>" + SecurityElement.Escape(walletSourceTypeId) + "</walletSourceTypeId>";
            return xml;
        }
    }
    ///new 12.26
    public partial class passengerTransportData
    {
        public string passengerName;
        public string ticketNumber;
        public string issuingCarrier;
        public string carrierName;
        public string restrictedTicketIndicator;
        private int numberOfAdultsField;
        private bool numberOfAdultsSet;
        public int numberOfAdults
        {
            get { return numberOfAdultsField; }
            set { numberOfAdultsField = value; numberOfAdultsSet = true; }
        }
        private int numberOfChildrenField;
        private bool numberOfChildrenSet;
        public int numberOfChildren
        {
            get { return numberOfChildrenField; }
            set { numberOfChildrenField = value; numberOfChildrenSet = true; }
        }
        public string customerCode;
        public DateTime arrivalDate;
        public DateTime issueDate;
        public string travelAgencyCode;
        public string travelAgencyName;
        public computerizedReservationSystemEnum computerizedReservationSystemField;
        public bool computerizedReservationSystemSet;
        public computerizedReservationSystemEnum computerizedReservationSystem
        {
            get
            {
                return computerizedReservationSystemField;
            }
            set
            {
                computerizedReservationSystemField = value;
                computerizedReservationSystemSet = true;
            }
        }
        public creditReasonIndicatorEnum creditReasonIndicatorField;
        public bool creditReasonIndicatorSet;
        public creditReasonIndicatorEnum creditReasonIndicator
        {
            get
            {
                return creditReasonIndicatorField;
            }
            set
            {
                creditReasonIndicatorField = value;
                creditReasonIndicatorSet = true;
            }
        }
        public ticketChangeIndicatorEnum ticketChangeIndicatorField;
        public bool ticketChangeIndicatorSet;
        public ticketChangeIndicatorEnum ticketChangeIndicator
        {
            get
            {
                return ticketChangeIndicatorField;
            }
            set
            {
                ticketChangeIndicatorField = value;
                ticketChangeIndicatorSet = true;
            }
        }
        public string ticketIssuerAddress;
        public string exchangeTicketNumber;
        private int exchangeAmountField;
        private bool exchangeAmountSet;
        public int exchangeAmount
        {
            get { return exchangeAmountField; }
            set { exchangeAmountField = value; exchangeAmountSet = true; }
        }
        private int exchangeFeeAmountField;
        private bool exchangeFeeAmountSet;
        public int exchangeFeeAmount
        {
            get { return exchangeFeeAmountField; }
            set { exchangeFeeAmountField = value; exchangeFeeAmountSet = true; }
        }
        public tripLegData tripLegData;
        public string Serialize()
        {
            var xml = "";
            if (passengerName != null) xml += "\r\n<passengerName>" + SecurityElement.Escape(passengerName) + "</passengerName>";
            if (ticketNumber != null) xml += "\r\n<ticketNumber>" + SecurityElement.Escape(ticketNumber) + "</ticketNumber>";
            if (issuingCarrier != null) xml += "\r\n<issuingCarrier>" + SecurityElement.Escape(issuingCarrier) + "</issuingCarrier>";
            if (carrierName != null) xml += "\r\n<carrierName>" + SecurityElement.Escape(carrierName) + "</carrierName>";
            if (restrictedTicketIndicator != null) xml += "\r\n<restrictedTicketIndicator>" + SecurityElement.Escape(restrictedTicketIndicator) + "</restrictedTicketIndicator>";
            if (numberOfAdultsSet) xml += "\r\n<numberOfAdults>" + numberOfAdultsField + "</numberOfAdults>";
            if (numberOfChildrenSet) xml += "\r\n<numberOfChildren>" + numberOfChildrenField + "</numberOfChildren>";
            if (customerCode != null) xml += "\r\n<customerCode>" + SecurityElement.Escape(customerCode) + "</customerCode>";
            if (arrivalDate != null)
            {
                xml += "\r\n<arrivalDate>" + XmlUtil.toXsdDate(arrivalDate) + "</arrivalDate>";
            }
            if (issueDate != null)
            {
                xml += "\r\n<issueDate>" + XmlUtil.toXsdDate(issueDate) + "</issueDate>";
            }
            if (travelAgencyCode != null) xml += "\r\n<travelAgencyCode>" + SecurityElement.Escape(travelAgencyCode) + "</travelAgencyCode>";
            if (travelAgencyName != null) xml += "\r\n<travelAgencyName>" + SecurityElement.Escape(travelAgencyName) + "</travelAgencyName>";
            if (computerizedReservationSystemSet) xml += "\r\n<computerizedReservationSystem>" + computerizedReservationSystemField + "</computerizedReservationSystem>";
            if (creditReasonIndicatorSet) xml += "\r\n<creditReasonIndicator>" + creditReasonIndicatorField + "</creditReasonIndicator>";
            if (ticketChangeIndicatorSet) xml += "\r\n<ticketChangeIndicator>" + ticketChangeIndicatorField + "</ticketChangeIndicator>";
            if (ticketIssuerAddress != null) xml += "\r\n<ticketIssuerAddress>" + SecurityElement.Escape(ticketIssuerAddress) + "</ticketIssuerAddress>";
            if (exchangeTicketNumber != null) xml += "\r\n<exchangeTicketNumber>" + SecurityElement.Escape(exchangeTicketNumber) + "</exchangeTicketNumber>";
            if (exchangeAmountSet) xml += "\r\n<exchangeAmount>" + exchangeAmountField + "</exchangeAmount>";
            if (exchangeFeeAmountSet) xml += "\r\n<exchangeFeeAmount>" + exchangeFeeAmountField + "</exchangeFeeAmount>";
            if (tripLegData != null)
            {
                xml += "\r\n<tripLegData>" + tripLegData.Serialize() + "\r\n</tripLegData>";
            }
            return xml;
        }
    }

    //12.29
    public partial class sellerInfo
    {
        public string accountNumber;
        private int aggregateOrderCountField;
        private bool aggregateOrderCountSet;
        public int aggregateOrderCount
        {
            get { return aggregateOrderCountField; }
            set { aggregateOrderCountField = value; aggregateOrderCountSet = true; }
        }

        private int aggregateOrderDollarsField;
        private bool aggregateOrderDollarsSet;

        public int aggregateOrderDollars
        {
            get { return aggregateOrderDollarsField; }
            set { aggregateOrderDollarsField = value; aggregateOrderDollarsSet = true; }
        }
        public sellerAddress sellerAddress;
        public string createdDate;
        public string domain;
        public string email;
        public string lastUpdateDate;
        public string name;
        public string onboardingEmail;
        public string onboardingIpAddress;
        public string parentEntity;
        public string phone;
        public string sellerId;
        public sellerTagsType sellerTags;
        public string username;

        public string Serialize()
        {
            var xml = "";
            if (accountNumber != null) xml += "\r\n<accountNumber>" + SecurityElement.Escape(accountNumber) + "</accountNumber>";
            if (aggregateOrderCountSet) xml += "\r\n<aggregateOrderCount>" + aggregateOrderCountField + "</aggregateOrderCount>";
            if (aggregateOrderDollarsSet) xml += "\r\n<aggregateOrderDollars>" + aggregateOrderDollarsField + "</aggregateOrderDollars>";
            if (sellerAddress != null)
            {
                xml += "\r\n<sellerAddress>" + sellerAddress.Serialize() + "\r\n</sellerAddress>";
            }
            return xml;
            if (createdDate != null) xml += "\r\n<createdDate>" + SecurityElement.Escape(createdDate) + "</createdDate>";
            if (domain != null) xml += "\r\n<domain>" + SecurityElement.Escape(domain) + "</domain>";
            if (email != null) xml += "\r\n<email>" + SecurityElement.Escape(email) + "</email>";
            if (lastUpdateDate != null) xml += "\r\n<lastUpdateDate>" + SecurityElement.Escape(lastUpdateDate) + "</lastUpdateDate>";
            if (name != null) xml += "\r\n<name>" + SecurityElement.Escape(name) + "</name>";
            if (onboardingEmail != null) xml += "\r\n<onboardingEmail>" + SecurityElement.Escape(onboardingEmail) + "</onboardingEmail>";
            if (onboardingIpAddress != null) xml += "\r\n<onboardingIpAddress>" + SecurityElement.Escape(onboardingIpAddress) + "</onboardingIpAddress>";
            if (parentEntity != null) xml += "\r\n<parentEntity>" + SecurityElement.Escape(parentEntity) + "</parentEntity>";
            if (phone != null) xml += "\r\n<phone>" + SecurityElement.Escape(phone) + "</phone>";
            if (sellerId != null) xml += "\r\n<sellerId>" + SecurityElement.Escape(sellerId) + "</sellerId>";
            if (sellerTags != null)
            {
                xml += "\r\n<sellerTags>" + sellerTags.Serialize() + "\r\n</sellerTags>";
            }
            if (username != null) xml += "\r\n<username>" + SecurityElement.Escape(username) + "</username>";

            return xml;
        }

    }

    public partial class sellerAddress
    {
        public string sellerStreetaddress;
        public string sellerUnit;
        public string sellerPostalcode;
        public string sellerCity;
        public string sellerProvincecode;
        public string sellerCountrycode;

        public string Serialize()
        {
            var xml = "";
            if (sellerStreetaddress != null) xml += "\r\n<sellerStreetaddress>" + SecurityElement.Escape(sellerStreetaddress) + "</sellerStreetaddress>";
            if (sellerUnit != null) xml += "\r\n<sellerUnit>" + SecurityElement.Escape(sellerUnit) + "</sellerUnit>";
            if (sellerPostalcode != null) xml += "\r\n<sellerPostalcode>" + SecurityElement.Escape(sellerPostalcode) + "</sellerPostalcode>";
            if (sellerCity != null) xml += "\r\n<sellerCity>" + SecurityElement.Escape(sellerCity) + "</sellerCity>";
            if (sellerProvincecode != null) xml += "\r\n<sellerProvincecode>" + SecurityElement.Escape(sellerProvincecode) + "</sellerProvincecode>";
            if (sellerCountrycode != null) xml += "\r\n<sellerCountrycode>" + SecurityElement.Escape(sellerCountrycode) + "</sellerCountrycode>";
            return xml;
        }
    }

    public partial class sellerTagsType
    {
        public string tag;

        public string Serialize()
        {
            var xml = "";
            if (tag != null) xml += "\r\n<tag>" + SecurityElement.Escape(tag) + "</tag>";
            return xml;
        }
    }

    //12.33 start
    public partial class subscriptions
    {
        public string subscriptionId;
        public DateTime nextDeliveryDate;

        private periodUnit periodUnitField;
        private bool periodUnitSet;
        public periodUnit periodUnit
        {
            get { return periodUnitField; }
            set { periodUnitField = value; periodUnitSet = true; }
        }

        private int numberOfPeriodsField;
        private bool numberOfPeriodsSet;
        public int numberOfPeriods
        {
            get
            {
                return numberOfPeriodsField;
            }
            set
            {
                numberOfPeriodsField = value;
                numberOfPeriodsSet = true;
            }
        }

        private int regularItemPriceField;
        private bool regularItemPriceSet;
        public int regularItemPrice
        {
            get { return regularItemPriceField; }
            set { regularItemPriceField = value; regularItemPriceSet = true; }
        }

        private int currentPeriodField;
        private bool currentPeriodSet;
        public int currentPeriod
        {
            get
            {
                return currentPeriodField;
            }
            set
            {
                currentPeriodField = value;
                currentPeriodSet = true;
            }
        }

        public string Serialize()
        {
            var xml = "";
            if (subscriptionId != null) xml += "\r\n<subscriptionId>" + SecurityElement.Escape(subscriptionId) + "</subscriptionId>";
            if (nextDeliveryDate != null)
            {
                xml += "\r\n<nextDeliveryDate>" + XmlUtil.toXsdDate(nextDeliveryDate) + "</nextDeliveryDate>";
            }
            if (periodUnitSet)
            {
                xml += "\r\n<periodUnit>" + periodUnitField + "</periodUnit>";
            }
            if (numberOfPeriodsSet) xml += "\r\n<numberOfPeriods>" + numberOfPeriodsField + "</numberOfPeriods>";
            if (regularItemPriceSet) xml += "\r\n<regularItemPrice>" + regularItemPriceField + "</regularItemPrice>";
            if (currentPeriodSet) xml += "\r\n<currentPeriod>" + currentPeriodField + "</currentPeriod>";
            return xml;
        }
    }

    //12.38
    public partial class partialCapture
    {
       
        private int partialCaptureSequenceNumberField;
        private bool partialCaptureSequenceNumberSet;
        public int partialCaptureSequenceNumber
        {
            get { return partialCaptureSequenceNumberField; }
            set { partialCaptureSequenceNumberField = value; partialCaptureSequenceNumberSet = true; }
        }

        private int partialCaptureTotalCountField;
        private bool partialCaptureTotalCountSet;
        public int partialCaptureTotalCount
        {
            get { return partialCaptureTotalCountField; }
            set { partialCaptureTotalCountField = value; partialCaptureTotalCountSet = true; }
        }
        public string Serialize()
        {
            var xml = "";
            if (partialCaptureSequenceNumberSet) xml += "\r\n<partialCaptureSequenceNumber>" + partialCaptureSequenceNumberField + "</partialCaptureSequenceNumber>";
            if (partialCaptureTotalCountSet) xml += "\r\n<partialCaptureTotalCount>" + partialCaptureTotalCountField + "</partialCaptureTotalCount>"; 
            return xml;
        }
    }

    public enum periodUnit
    {
        /// <remarks/>
        WEEK,
        MONTH,
        QUARTER,
        YEAR
    }
    //12.33 end

    public partial class propertyAddress
    {
        public string name;
        public string city;
        public string region;
        private countryTypeEnum countryField;
        private bool countrySet;
        public countryTypeEnum country
        {
            get { return countryField; }
            set { countryField = value; countrySet = true; }
        }

        public string Serialize()
        {
            var xml = "";
            if (name != null) xml += "\r\n<name>" + SecurityElement.Escape(name) + "</name>";
            if (city != null) xml += "\r\n<city>" + SecurityElement.Escape(city) + "</city>";
            if (region != null) xml += "\r\n<region>" + SecurityElement.Escape(region) + "</region>";
            if (countrySet) xml += "\r\n<country>" + countryField + "</country>";
            return xml;
        }
    }

    public partial class tripLegData
    {
        private int tripLegNumberField;
        private bool tripLegNumberSet;
        public int tripLegNumber
        {
            get
            {
                return tripLegNumberField;
            }
            set
            {
                tripLegNumberField = value;
                tripLegNumberSet = true;
            }
        }
        public string departureCode;
        public string carrierCode;
        private serviceClassEnum serviceClassField;
        private bool serviceClassSet;
        public serviceClassEnum serviceClass
        {
            get
            {
                return serviceClassField;
            }
            set
            {
                serviceClassField = value;
                serviceClassSet = true;
            }
        }
        public string stopOverCode;
        public string destinationCode;
        public string fareBasisCode;
        public DateTime departureDate;
        public string originCity;
        public string travelNumber;
        public string departureTime;
        public string arrivalTime;
        public string remarks;

        public string Serialize()
        {
            var xml = "";
            if (tripLegNumberSet) xml += "\r\n<tripLegNumber>" + tripLegNumberField.ToString() + "</tripLegNumber>";
            if (departureCode != null) xml += "\r\n<departureCode>" + SecurityElement.Escape(departureCode) + "</departureCode>";
            if (carrierCode != null) xml += "\r\n<carrierCode>" + SecurityElement.Escape(carrierCode) + "</carrierCode>";
            if (serviceClassSet) xml += "\r\n<serviceClass>" + serviceClassField + "</serviceClass>";
            if (stopOverCode != null) xml += "\r\n<stopOverCode>" + SecurityElement.Escape(stopOverCode) + "</stopOverCode>";
            if (destinationCode != null) xml += "\r\n<destinationCode>" + SecurityElement.Escape(destinationCode) + "</destinationCode>";
            if (fareBasisCode != null) xml += "\r\n<fareBasisCode>" + SecurityElement.Escape(fareBasisCode) + "</fareBasisCode>";
            if (departureDate != null)
            {
                xml += "\r\n<departureDate>" + XmlUtil.toXsdDate(departureDate) + "</departureDate>";
            }
            if (originCity != null) xml += "\r\n<originCity>" + SecurityElement.Escape(originCity) + "</originCity>";
            if (travelNumber != null) xml += "\r\n<travelNumber>" + SecurityElement.Escape(travelNumber) + "</travelNumber>";
            if (departureTime != null) xml += "\r\n<departureTime>" + SecurityElement.Escape(departureTime) + "</departureTime>";
            if (arrivalTime != null) xml += "\r\n<arrivalTime>" + SecurityElement.Escape(arrivalTime) + "</arrivalTime>";
            if (remarks != null) xml += "\r\n<remarks>" + SecurityElement.Escape(remarks) + "</remarks>";
            return xml;
        }
    }

    public partial class pinlessDebitRequestType
    {

        public routingPreferenceEnum routingPreferenceField;
        public bool routingPreferenceSet;
        public routingPreferenceEnum routingPreference
        {
            get
            {
                return routingPreferenceField;
            }
            set
            {
                routingPreferenceField = value;
                routingPreferenceSet = true;
            }
        }

        public preferredDebitNetworksType preferredDebitNetworks;


        public string Serialize()
        {
            var xml = "";
            if (routingPreferenceSet) xml += "\r\n<routingPreference>" + routingPreferenceField + "</routingPreference>";
            if (preferredDebitNetworks != null) xml += "\r\n<preferredDebitNetworks>" + preferredDebitNetworks.Serialize() + "</preferredDebitNetworks>";

            return xml;

        }

    }

    public partial class preferredDebitNetworksType
    {

        public List<string> debitNetworkName;

        public preferredDebitNetworksType()
        {

            debitNetworkName = new List<string>();

        }

        public string Serialize()
        {
            var xml = "";
            if (debitNetworkName.Count > 0)
            {
                foreach (string dnn in debitNetworkName)
                {
                    xml += "\r\n<debitNetworkName>" + dnn + "</debitNetworkName>";
                }
            }

            return xml;

        }

    }


    public enum walletWalletSourceType
    {
        MasterPass,
        VisaCheckout
    }

    public partial class preferredDebitNetworksType
    { }



    public partial class fraudCheck : transactionTypeWithReportGroup
    {

        public advancedFraudChecksType advancedFraudChecks;

        private contact billToAddressField;
        private bool billToAddressSet;
        public contact billToAddress
        {
            get
            {
                return billToAddressField;
            }
            set
            {
                billToAddressField = value; billToAddressSet = true;
            }
        }

        ///new 12.24
        private contact retailerAddressField;
        private bool retailerAddressSet;
        public contact retailerAddress
        {
            get
            {
                return retailerAddressField;
            }
            set
            {
                retailerAddressField = value; retailerAddressSet = true;
            }
        }

        private contact shipToAddressField;
        private bool shipToAddressSet;
        public contact shipToAddress
        {
            get
            {
                return shipToAddressField;
            }
            set
            {
                shipToAddressField = value; shipToAddressSet = true;
            }
        }

        private int amountField;
        private bool amountSet;
        public int amount
        {
            get
            {
                return amountField;
            }
            set
            {
                amountField = value; amountSet = true;
            }
        }

        private eventTypeEnum eventTypeField;
        private bool eventTypeSet;

        public eventTypeEnum eventType
        {
            get
            {
                return eventTypeField;
            }
            set
            {
                eventTypeField = value;
                eventTypeSet = true;

            }
        }

        public string accountLogin;
        public string accountPasshash;

        public override string Serialize()
        {
            var xml = "\r\n<fraudCheck";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (advancedFraudChecks != null) xml += "\r\n<advancedFraudChecks>" + advancedFraudChecks.Serialize() + "\r\n</advancedFraudChecks>";
            if (billToAddressSet) xml += "\r\n<billToAddress>" + billToAddressField.Serialize() + "</billToAddress>"; 
            if (retailerAddressSet) xml += "\r\n<retailerAddress>" + retailerAddressField.Serialize() + "</retailerAddress>";///12.24
            if (shipToAddressSet) xml += "\r\n<shipToAddress>" + shipToAddressField.Serialize() + "</shipToAddress>";
            if (amountSet) xml += "\r\n<amount>" + amountField.ToString() + "</amount>";
            if (eventTypeSet) xml += "\r\n<eventType>" + eventTypeField + "</eventType>";
            if (accountLogin != null) xml += "\r\n<accountLogin>" + SecurityElement.Escape(accountLogin) + "</accountLogin>";
            if (accountPasshash != null) xml += "\r\n<accountPasshash>" + SecurityElement.Escape(accountPasshash) + "</accountPasshash>";

            xml += "\r\n</fraudCheck>";
            return xml;
        }
    }

    public enum eventTypeEnum
    {
        payment,
        login,
        account_creation,
        details_change
    }

    public enum processingType
    {
        undefined,
        accountFunding,
        initialRecurring,
        initialInstallment,
        initialCOF,
        merchantInitiatedCOF,
        cardholderInitiatedCOF
    }

    public enum mandateProviderType
    {
        Merchant,
        Vantiv
    }

    public enum sequenceTypeType
    {
        OneTime,
        FirstRecurring,
        SubsequentRecurring,
        FinalRecurring
    }

    public partial class sepaDirectDebitType
    {
        public mandateProviderType mandateProvider;
        public sequenceTypeType sequenceType;
        public string mandateReferenceField;
        public bool mandateReferenceSet;
        public string mandateReference
        {
            get
            {
                return mandateReferenceField;
            }
            set
            {
                mandateReferenceField = value;
                mandateReferenceSet = true;
            }
        }
        public string mandateUrlField;
        public bool mandateUrlSet;
        public string mandateUrl
        {
            get
            {
                return mandateUrlField;
            }
            set
            {
                mandateUrlField = value;
                mandateUrlSet = true;
            }
        }
        // CES does this work
        public DateTime mandateSignatureDateField;
        public bool mandateSignatureDateSet;
        public DateTime mandateSignatureDate
        {
            get
            {
                return mandateSignatureDateField;
            }
            set
            {
                mandateSignatureDateField = value;
                mandateSignatureDateSet = true;
            }
        }
        public string iban;
        public countryTypeEnum preferredLanguageField;
        public bool preferredLanguageSet;
        public countryTypeEnum preferredLanguage
        {
            get
            {
                return preferredLanguageField;
            }
            set
            {
                preferredLanguageField = value;
                preferredLanguageSet = true;
            }
        }
        public string Serialize()
        {
            var xml = "";
            xml += "\r\n<mandateProvider>" + mandateProvider + "</mandateProvider>";
            xml += "\r\n<sequenceType>" + sequenceType + "</sequenceType>";
            if (mandateReferenceSet)
            {
                xml += "\r\n<mandateReference>" + mandateReference + "</mandateReference>";
            }
            if (mandateUrlSet)
            {
                xml += "\r\n<mandateUrl>" + mandateUrl + "</mandateUrl>";
            }
            if (mandateSignatureDateSet)
            {
                xml += "\r\n<mandateSignatureDate>" + mandateSignatureDate + "</mandateSignatureDate>";
            }
            if (iban != null)
            {
                xml += "\r\n<iban>" + iban + "</iban>";
            }
            if (preferredLanguageSet)
            {
                xml += "\r\n<preferredLanguage>" + preferredLanguage + "</preferredLanguage>";
            }
            return xml;
        }
    }


    public class idealType
    {
        public countryTypeEnum preferredLanguageField;
        public bool preferredLanguageSet;
        public countryTypeEnum preferredLanguage
        {
            get
            {
                return preferredLanguageField;
            }
            set
            {
                preferredLanguageField = value;
                preferredLanguageSet = true;
            }
        }

        public string Serialize()
        {
            var xml = "";
            if (preferredLanguageSet)
            {
                xml += "\r\n<preferredLanguage>" + preferredLanguage + "</preferredLanguage>";
            }
            return xml;
        }
    }

    public class giropayType
    {
        public countryTypeEnum preferredLanguageField;
        public bool preferredLanguageSet;
        public countryTypeEnum preferredLanguage
        {
            get
            {
                return preferredLanguageField;
            }
            set
            {
                preferredLanguageField = value;
                preferredLanguageSet = true;
            }
        }

        public string Serialize()
        {
            var xml = "";
            if (preferredLanguageSet)
            {
                xml += "\r\n<preferredLanguage>" + preferredLanguage + "</preferredLanguage>";
            }
            return xml;
        }
    }

    // The sofort element is a child of the sale transaction that, through its child elements,
    // defines information needed to process an SOFORT (Real-time Bank Transfer) transaction.
    // At this time, you can use the iDeal method of payment in Online transactions only.
    public class sofortType
    {
        public countryTypeEnum preferredLanguageField;
        public bool preferredLanguageSet;
        public countryTypeEnum preferredLanguage
        {
            get
            {
                return preferredLanguageField;
            }
            set
            {
                preferredLanguageField = value;
                preferredLanguageSet = true;
            }
        }

        public string Serialize()
        {
            var xml = "";
            if (preferredLanguageSet)
            {
                xml += "\r\n<preferredLanguage>" + preferredLanguage + "</preferredLanguage>";
            }
            return xml;
        }
    }

    ///new 12.35
    public partial class accountFundingTransactionData
    {
        public string receiverFirstName;
        public string receiverLastName;

        public stateTypeEnum receiverStateFeild;
        public bool receiverStateSet;
        public stateTypeEnum receiverState
        {
            get
            {
                return receiverStateFeild;
            }
            set
            {
                receiverStateFeild = value;
                receiverStateSet = true;
            }
        }

        public countryTypeEnum receiverCountryFeild;
        public bool receiverCountrySet;
       
        public countryTypeEnum receiverCountry
        {
            get
            {
                return receiverCountryFeild;
            }
            set
            {
                receiverCountryFeild = value;
                receiverCountrySet = true;
            }
        }


        public accountFundingTransactionAccountNumberTypeEnum receiverAccountNumberTypeFeild;
        public bool receiverAccountNumberTypeSet;

        public accountFundingTransactionAccountNumberTypeEnum receiverAccountNumberType
        {
            get
            {
                return receiverAccountNumberTypeFeild;
            }
            set
            {
                receiverAccountNumberTypeFeild = value;
                receiverAccountNumberTypeSet = true;
            }
        }
        public string receiverAccountNumber;

        public accountFundingTransactionTypeEnum accountFundingTransactionTypeFeild;
        public bool accountFundingTransactionTypeSet;

        public accountFundingTransactionTypeEnum accountFundingTransactionType
        {
            get
            {
                return accountFundingTransactionTypeFeild;
            }
            set
            {
                accountFundingTransactionTypeFeild = value;
                accountFundingTransactionTypeSet = true;
            }
        }
        public string Serialize()
        {
            var xml = "";
            if (receiverFirstName != null) xml += "\r\n<receiverFirstName>" + SecurityElement.Escape(receiverFirstName) + "</receiverFirstName>";
            if (receiverLastName != null) xml += "\r\n<receiverLastName>" + SecurityElement.Escape(receiverLastName) + "</receiverLastName>";
            if (receiverStateSet)
            {
                xml += "\r\n<receiverState>" + receiverState + "</receiverState>";
            }
            if (receiverCountrySet)
            {
                xml += "\r\n<receiverCountry>" + receiverCountry + "</receiverCountry>";
            }
            if(receiverAccountNumberTypeSet)
            {
                xml += "\r\n<receiverAccountNumberType>" + receiverAccountNumberType + "</receiverAccountNumberType>";
            }
            if (receiverAccountNumber != null) xml += "\r\n<receiverAccountNumber>" + SecurityElement.Escape(receiverAccountNumber) + "</receiverAccountNumber>";
            if (accountFundingTransactionTypeSet)
            {
                xml += "\r\n<accountFundingTransactionType>" + accountFundingTransactionType + "</accountFundingTransactionType>";
            }
            return xml;
        }
    }

    public partial class BNPLAuthorizationRequest : transactionTypeWithReportGroup
    {
        public long amount;
        public string orderId;
        private provider providerField;
        private bool providerSet;
        public provider provider
        {
            get { return providerField; }
            set { providerField = value; providerSet = true; }
        }
        public string postCheckoutRedirectUrl;
        public customerInfo customerInfo;
        public contact billToAddress;
        public contact shipToAddress;
        public enhancedData enhancedData;

        public override string Serialize()
        {
            var xml = "\r\n<BNPLAuthorizationRequest";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
           
            xml += "\r\n<amount>" + amount + "</amount>";
            xml += "\r\n<orderId>" + SecurityElement.Escape(orderId) + "</orderId>";
            if (providerSet)
            {
                xml += "\r\n<provider>" + provider + "</provider>";
            }
            if (postCheckoutRedirectUrl != null)
                {
                xml += "\r\n<postCheckoutRedirectUrl>" + postCheckoutRedirectUrl + "</postCheckoutRedirectUrl>";
            }
            if (customerInfo != null)
            {
                xml += "\r\n<customerInfo>" + customerInfo.Serialize() + "\r\n</customerInfo>";
            }   
            if (billToAddress != null)
            {
                xml += "\r\n<billToAddress>" + billToAddress.Serialize() + "\r\n</billToAddress>";
            }
            if (shipToAddress != null)
            {
                xml += "\r\n<shipToAddress>" + shipToAddress.Serialize() + "\r\n</shipToAddress>";
            }              
            if (enhancedData != null)
            {
                xml += "\r\n<enhancedData>" + enhancedData.Serialize() + "\r\n</enhancedData>";
            }
            xml += "\r\n</BNPLAuthorizationRequest>";
            return xml;
        }
    }
    #endregion

    public partial class BNPLCaptureRequest : transactionTypeWithReportGroup
    {
        public long amount;
        public string orderId;
        private long cnpTxnIdField;
        private bool cnpTxnIdSet;
        public long cnpTxnId
        {
            get
            {
                return cnpTxnIdField;
            }
            set
            {
                cnpTxnIdField = value;
                cnpTxnIdSet = true;
            }
        }
        public override string Serialize()
        {
            var xml = "\r\n<BNPLCaptureRequest";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";

            xml += "\r\n<amount>" + amount + "</amount>";
            xml += "\r\n<orderId>" + SecurityElement.Escape(orderId) + "</orderId>";
            if (cnpTxnIdSet)
            {
                xml += "\r\n<cnpTxnId>" + cnpTxnId + "</cnpTxnId>";
            }
            xml += "\r\n</BNPLCaptureRequest>";
            return xml;
        }
    }

    public partial class BNPLRefundRequest : transactionTypeWithReportGroup
    {
        public long amount;
        public string orderId;
        private long cnpTxnIdField;
        private bool cnpTxnIdSet;
        public long cnpTxnId
        {
            get
            {
                return cnpTxnIdField;
            }
            set
            {
                cnpTxnIdField = value;
                cnpTxnIdSet = true;
            }
        }
        public override string Serialize()
        {
            var xml = "\r\n<BNPLRefundRequest";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";

            xml += "\r\n<amount>" + amount + "</amount>";
            xml += "\r\n<orderId>" + SecurityElement.Escape(orderId) + "</orderId>";
            if (cnpTxnIdSet)
            {
                xml += "\r\n<cnpTxnId>" + cnpTxnId + "</cnpTxnId>";
            }
            xml += "\r\n</BNPLRefundRequest>";
            return xml;
        }
    }

    public partial class BNPLCancelRequest : transactionTypeWithReportGroup
    {
        public long amount;
        public string orderId;
        private long cnpTxnIdField;
        private bool cnpTxnIdSet;
        public long cnpTxnId
        {
            get
            {
                return cnpTxnIdField;
            }
            set
            {
                cnpTxnIdField = value;
                cnpTxnIdSet = true;
            }
        }
        public override string Serialize()
        {
            var xml = "\r\n<BNPLCancelRequest";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";

            xml += "\r\n<amount>" + amount + "</amount>";
            xml += "\r\n<orderId>" + SecurityElement.Escape(orderId) + "</orderId>";
            if (cnpTxnIdSet)
            {
                xml += "\r\n<cnpTxnId>" + cnpTxnId + "</cnpTxnId>";
            }
            xml += "\r\n</BNPLCancelRequest>";
            return xml;
        }
    }

    public partial class BNPLInquiryRequest : transactionTypeWithReportGroup
    {
        public string orderId;
        private long cnpTxnIdField;
        private bool cnpTxnIdSet;
        public long cnpTxnId
        {
            get
            {
                return cnpTxnIdField;
            }
            set
            {
                cnpTxnIdField = value;
                cnpTxnIdSet = true;
            }
        }
        public override string Serialize()
        {
            var xml = "\r\n<BNPLInquiryRequest";
            xml += " id=\"" + SecurityElement.Escape(id) + "\"";
            if (customerId != null)
            {
                xml += " customerId=\"" + SecurityElement.Escape(customerId) + "\"";
            }
            xml += " reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
       
            xml += "\r\n<orderId>" + SecurityElement.Escape(orderId) + "</orderId>";
            if (cnpTxnIdSet)
            {
                xml += "\r\n<cnpTxnId>" + cnpTxnId + "</cnpTxnId>";
            }
            xml += "\r\n</BNPLInquiryRequest>";
            return xml;
        }
    }

    public partial class EncryptionKeyRequest : transactionRequest
    {
        private encryptionKeyRequestEnum encryptionKeyRequestField;
        private bool encryptionKeyRequestSet;
        public encryptionKeyRequestEnum encryptionKeyRequest
        {
            get
            {
                return encryptionKeyRequestField;
            }
            set
            {
                encryptionKeyRequestField = value;
                encryptionKeyRequestSet = true;
            }
        }
        public override string Serialize()
        {
            var xml = " ";
       
            if (encryptionKeyRequestSet)
            {
                xml += "\r<encryptionKeyRequest>" + encryptionKeyRequest + "</encryptionKeyRequest>";
            }
            return xml;
        }
    }

    public class XmlUtil
    {
        public static string toXsdDate(DateTime dateTime)
        {
            var year = dateTime.Year.ToString();
            var month = dateTime.Month.ToString();
            if (dateTime.Month < 10)
            {
                month = "0" + month;
            }
            var day = dateTime.Day.ToString();
            if (dateTime.Day < 10)
            {
                day = "0" + day;
            }
            return year + "-" + month + "-" + day;
        }
    }
}
