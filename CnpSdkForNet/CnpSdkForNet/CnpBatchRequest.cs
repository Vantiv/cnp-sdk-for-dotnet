using System;
using System.Collections.Generic;
using System.IO;
using System.Security;

namespace Cnp.Sdk
{
    public partial class batchRequest
    {
        public string id;
        public string merchantId;
        public string reportGroup;

        public Dictionary<string, string> config;

        public string batchFilePath;
        private string tempBatchFilePath;
        private cnpFile cnpFile;
        private cnpTime cnpTime;
        private string requestDirectory;
        private string responseDirectory;

        private int numAuthorization;
        private int numAccountUpdates;
        private int numCapture;
        private int numGiftCardCapture;
        private int numCredit;
        private int numGiftCardCredit;
        private int numSale;
        private int numAuthReversal;
        private int numGiftCardAuthReversal;
        private int numEcheckCredit;
        private int numEcheckVerification;
        private int numEcheckSale;
        private int numRegisterTokenRequest;
        private int numForceCapture;
        private int numCaptureGivenAuth;
        private int numEcheckRedeposit;
        private int numEcheckPreNoteSale;
        private int numEcheckPreNoteCredit;
        private int numUpdateCardValidationNumOnToken;
        private int numUpdateSubscriptions;
        private int numCancelSubscriptions;
        private int numCreatePlans;
        private int numUpdatePlans;
        private int numActivates;
        private int numDeactivates;
        private int numLoads;
        private int numUnloads;
        private int numBalanceInquiries;
        private int numPayFacCredit;
        private int numSubmerchantCredit;
        private int numReserveCredit;
        private int numVendorCredit;
        private int numPhysicalCheckCredit;
        private int numPayFacDebit;
        private int numSubmerchantDebit;
        private int numReserveDebit;
        private int numVendorDebit;
        private int numPhysicalCheckDebit;
        private int numFundingInstructionVoid;
        private int numFastAccessFunding;
        private int numTranslateToLowValueTokenRequests;
        private int numPayoutOrgCredit;
        private int numPayoutOrgDebit; 
        private int numCustomerCredit;
        private int numCustomerDebit;
        private int numTransactionReversal;

        private long sumOfAuthorization;
        private long sumOfAuthReversal;
        private long sumOfGiftCardAuthReversal;
        private long sumOfCapture;
        private long sumOfGiftCardCapture;
        private long sumOfCredit;
        private long sumOfGiftCardCredit;
        private long sumOfSale;
        private long sumOfForceCapture;
        private long sumOfEcheckSale;
        private long sumOfEcheckCredit;
        private long sumOfEcheckVerification;
        private long sumOfCaptureGivenAuth;
        private long activateAmount;
        private long loadAmount;
        private long unloadAmount;
        private long payFacCreditAmount;
        private long submerchantCreditAmount;
        private long reserveCreditAmount;
        private long vendorCreditAmount;
        private long physicalCheckCreditAmount;
        private long payFacDebitAmount;
        private long submerchantDebitAmount;
        private long reserveDebitAmount;
        private long vendorDebitAmount;
        private long physicalCheckDebitAmount;
        private long fastAccessFundingAmount;
        private long payoutOrgCreditAmount;
        private long payoutOrgDebitAmount;
        private long customerCreditAmount;
        private long customerDebitAmount;
        private long sumOfTransactionReversal;

        private bool sameDayFunding;

        private const string accountUpdateErrorMessage = "Account Updates need to exist in their own batch request!";

        public batchRequest()
        {
            config = new Dictionary<string, string>();
            ConfigManager configManager = new ConfigManager();
            config = configManager.getConfig();
            //config["url"] = Properties.Settings.Default.url;
            //config["reportGroup"] = Properties.Settings.Default.reportGroup;
            //config["username"] = Properties.Settings.Default.username;
            //config["printxml"] = Properties.Settings.Default.printxml;
            //config["timeout"] = Properties.Settings.Default.timeout;
            //config["proxyHost"] = Properties.Settings.Default.proxyHost;
            //config["merchantId"] = Properties.Settings.Default.merchantId;
            //config["password"] = Properties.Settings.Default.password;
            //config["proxyPort"] = Properties.Settings.Default.proxyPort;
            //config["sftpUrl"] = Properties.Settings.Default.sftpUrl;
            //config["sftpUsername"] = Properties.Settings.Default.sftpUsername;
            //config["sftpPassword"] = Properties.Settings.Default.sftpPassword;
            //config["requestDirectory"] = Properties.Settings.Default.requestDirectory;
            //config["responseDirectory"] = Properties.Settings.Default.responseDirectory;

            initializeRequest();
        }

        public batchRequest(Dictionary<string, string> config)
        {
            this.config = config;

            initializeRequest();
        }

        private void initializeRequest()
        {
            requestDirectory = Path.Combine(config["requestDirectory"],"Requests") + Path.DirectorySeparatorChar;
            responseDirectory = Path.Combine(config["responseDirectory"],"Responses") + Path.DirectorySeparatorChar;

            cnpFile = new cnpFile();
            cnpTime = new cnpTime();

            numAuthorization = 0;
            numAuthReversal = 0;
            numGiftCardAuthReversal = 0;
            numCapture = 0;
            numCaptureGivenAuth = 0;
            numGiftCardCapture = 0;
            numCredit = 0;
            numGiftCardCredit = 0;
            numEcheckCredit = 0;
            numEcheckRedeposit = 0;
            numEcheckPreNoteSale = 0;
            numEcheckPreNoteCredit = 0;
            numEcheckSale = 0;
            numEcheckVerification = 0;
            numForceCapture = 0;
            numRegisterTokenRequest = 0;
            numSale = 0;
            numUpdateCardValidationNumOnToken = 0;
            numUpdateSubscriptions = 0;
            numCancelSubscriptions = 0;
            numPayFacCredit = 0;
            numSubmerchantCredit = 0;
            numReserveCredit = 0;
            numVendorCredit = 0;
            numCustomerCredit = 0;
            numPayoutOrgCredit = 0;
            numPhysicalCheckCredit = 0;
            numPayFacDebit = 0;
            numSubmerchantDebit = 0;
            numReserveDebit = 0;
            numVendorDebit = 0;
            numCustomerDebit = 0;
            numPayoutOrgDebit = 0;
            numPhysicalCheckDebit = 0;
            numFundingInstructionVoid = 0;
            numFastAccessFunding = 0;
            numTranslateToLowValueTokenRequests = 0;
            numPayoutOrgCredit = 0;
            numPayoutOrgDebit = 0;
            numCustomerCredit = 0;
            numCustomerDebit = 0;
            numTransactionReversal = 0;
            sumOfAuthorization = 0;
            sumOfAuthReversal = 0;
            sumOfGiftCardAuthReversal = 0;
            sumOfCapture = 0;
            sumOfGiftCardCapture = 0;
            sumOfCredit = 0;
            sumOfGiftCardCredit = 0;
            sumOfSale = 0;
            sumOfForceCapture = 0;
            sumOfEcheckSale = 0;
            sumOfEcheckCredit = 0;
            sumOfEcheckVerification = 0;
            sumOfCaptureGivenAuth = 0;
            payFacCreditAmount = 0;
            submerchantCreditAmount = 0;
            reserveCreditAmount = 0;
            vendorCreditAmount = 0;
            customerCreditAmount = 0;
            payoutOrgCreditAmount = 0;
            physicalCheckCreditAmount = 0;
            payFacDebitAmount = 0;
            submerchantDebitAmount = 0;
            reserveDebitAmount = 0;
            vendorDebitAmount = 0;
            customerDebitAmount = 0;
            payoutOrgDebitAmount = 0;
            physicalCheckDebitAmount = 0;
            fastAccessFundingAmount = 0;
            sumOfTransactionReversal = 0;
            sameDayFunding = false;
        }

        public string getResponseDirectory()
        {
            return responseDirectory;
        }

        public string getRequestDirectory()
        {
            return requestDirectory;
        }

        public void setCnpFile(cnpFile cnpFile)
        {
            this.cnpFile = cnpFile;
        }

        public cnpFile getCnpFile()
        {
            return cnpFile;
        }

        public void setCnpTime(cnpTime cnpTime)
        {
            this.cnpTime = cnpTime;
        }

        public cnpTime getCnpTime()
        {
            return cnpTime;
        }

        public int getNumAuthorization()
        {
            return numAuthorization;
        }

        public int getNumAccountUpdates()
        {
            return numAccountUpdates;
        }

        public int getNumCapture()
        {
            return numCapture;
        }

        public int getGiftCardCapture()
        {
            return numGiftCardCapture;
        }


        public int getNumCredit()
        {
            return numCredit;
        }

        public int getNumGiftCardCredit()
        {
            return numGiftCardCredit;
        }

        public int getNumSale()
        {
            return numSale;
        }

        public int getNumAuthReversal()
        {
            return numAuthReversal;
        }
        
        public int getNumTransactionReversal()
        {
            return numTransactionReversal;
        }

        public int getNumGiftCardAuthReversal()
        {
            return numGiftCardAuthReversal;
        }

        public int getNumEcheckCredit()
        {
            return numEcheckCredit;
        }

        public int getNumEcheckVerification()
        {
            return numEcheckVerification;
        }

        public int getNumEcheckSale()
        {
            return numEcheckSale;
        }

        public int getNumRegisterTokenRequest()
        {
            return numRegisterTokenRequest;
        }

        public int getNumForceCapture()
        {
            return numForceCapture;
        }

        public int getNumCaptureGivenAuth()
        {
            return numCaptureGivenAuth;
        }

        public int getNumEcheckRedeposit()
        {
            return numEcheckRedeposit;
        }

        public int getNumEcheckPreNoteSale()
        {
            return numEcheckPreNoteSale;
        }

        public int getNumEcheckPreNoteCredit()
        {
            return numEcheckPreNoteCredit;
        }

        public int getNumUpdateCardValidationNumOnToken()
        {
            return numUpdateCardValidationNumOnToken;
        }

        public int getNumUpdateSubscriptions()
        {
            return numUpdateSubscriptions;
        }

        public int getNumCancelSubscriptions()
        {
            return numCancelSubscriptions;
        }

        public int getNumCreatePlans()
        {
            return numCreatePlans;
        }

        public int getNumUpdatePlans()
        {
            return numUpdatePlans;
        }

        public int getNumActivates()
        {
            return numActivates;
        }

        public int getNumDeactivates()
        {
            return numDeactivates;
        }

        public int getNumLoads()
        {
            return numLoads;
        }

        public int getNumUnloads()
        {
            return numUnloads;
        }

        public int getNumBalanceInquiries()
        {
            return numBalanceInquiries;
        }

        public int getNumPayFacCredit()
        {
            return numPayFacCredit;
        }

        public int getNumSubmerchantCredit()
        {
            return numSubmerchantCredit;
        }

        public int getNumReserveCredit()
        {
            return numReserveCredit;
        }

        public int getNumVendorCredit()
        {
            return numVendorCredit;
        }

        public int getNumCustomerCredit()
        {
            return numCustomerCredit;
        }

        public int getNumPayoutOrgCredit()
        {
            return numPayoutOrgCredit;
        }

        public int getNumPhysicalCheckCredit()
        {
            return numPhysicalCheckCredit;
        }

        public int getNumPayFacDebit()
        {
            return numPayFacDebit;
        }

        public int getNumSubmerchantDebit()
        {
            return numSubmerchantDebit;
        }

        public int getNumReserveDebit()
        {
            return numReserveDebit;
        }

        public int getNumVendorDebit()
        {
            return numVendorDebit;
        }

        public int getNumCustomerDebit()
        {
            return numCustomerDebit;
        }

        public int getNumPayoutOrgDebit()
        {
            return numPayoutOrgDebit;
        }

        public int getNumPhysicalCheckDebit()
        {
            return numPhysicalCheckDebit;
        }

        public long getNumFundingInstructionVoid()
        {
            return numFundingInstructionVoid;
        }

        public int getNumFastAccessFunding()
        {
            return numFastAccessFunding;
        }

        public int getNumTranslateToLowValueTokenRequests()
        {
            return numTranslateToLowValueTokenRequests;
        }

        public long getLoadAmount()
        {
            return loadAmount;
        }

        public long getUnloadAmount()
        {
            return unloadAmount;
        }

        public long getActivateAmount()
        {
            return activateAmount;
        }

        public long getSumOfAuthorization()
        {
            return sumOfAuthorization;
        }

        public long getSumOfAuthReversal()
        {
            return sumOfAuthReversal;
        }
        
        public long getSumOfTransactionReversal()
        {
            return sumOfTransactionReversal;
        }

        public long getSumOfGiftCardAuthReversal()
        {
            return sumOfGiftCardAuthReversal;
        }

        public long getSumOfCapture()
        {
            return sumOfCapture;
        }

        public long getSumOfGiftCardCapture()
        {
            return sumOfGiftCardCapture;
        }

        public long getSumOfCredit()
        {
            return sumOfCredit;
        }

        public long getSumOfGiftCardCredit()
        {
            return sumOfGiftCardCredit;
        }

        public long getSumOfSale()
        {
            return sumOfSale;
        }

        public long getSumOfForceCapture()
        {
            return sumOfForceCapture;
        }

        public long getSumOfEcheckSale()
        {
            return sumOfEcheckSale;
        }

        public long getSumOfEcheckCredit()
        {
            return sumOfEcheckCredit;
        }

        public long getSumOfEcheckVerification()
        {
            return sumOfEcheckVerification;
        }

        public long getSumOfCaptureGivenAuth()
        {
            return sumOfCaptureGivenAuth;
        }

        public long getPayFacCreditAmount()
        {
            return payFacCreditAmount;
        }

        public long getSubmerchantCreditAmount()
        {
            return submerchantCreditAmount;
        }

        public long getReserveCreditAmount()
        {
            return reserveCreditAmount;
        }

        public long getVendorCreditAmount()
        {
            return vendorCreditAmount;
        }

        public long getCustomerCreditAmount()
        {
            return customerCreditAmount;
        }

        public long getPayoutOrgCreditAmount()
        {
            return payoutOrgCreditAmount;
        }

        public long getPhysicalCheckCreditAmount()
        {
            return physicalCheckCreditAmount;
        }

        public long getPayFacDebitAmount()
        {
            return payFacDebitAmount;
        }

        public long getSubmerchantDebitAmount()
        {
            return submerchantDebitAmount;
        }

        public long getReserveDebitAmount()
        {
            return reserveDebitAmount;
        }

        public long getVendorDebitAmount()
        {
            return vendorDebitAmount;
        }

        public long getCustomerDebitAmount()
        {
            return customerDebitAmount;
        }

        public long getPayoutOrgDebitAmount()
        {
            return payoutOrgDebitAmount;
        }

        public long getPhysicalCheckDebitAmount()
        {
            return physicalCheckDebitAmount;
        }

        public long getFastAccessFundingAmount()
        {
            return fastAccessFundingAmount;
        }
 
        public void addAuthorization(authorization authorization)
        {
            if (numAccountUpdates == 0)
            {
                numAuthorization++;
                sumOfAuthorization += authorization.amount;
                fillInReportGroup(authorization);
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, authorization);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addCapture(capture capture)
        {
            if (numAccountUpdates == 0)
            {
                numCapture++;
                sumOfCapture += capture.amount;
                fillInReportGroup(capture);
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, capture);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addGiftCardCapture(giftCardCapture giftCardCapture)
        {
            if (numAccountUpdates == 0)
            {
                numGiftCardCapture++;
                sumOfGiftCardCapture += giftCardCapture.captureAmount;
                fillInReportGroup(giftCardCapture);
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, giftCardCapture);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addCredit(credit credit)
        {
            if (numAccountUpdates == 0)
            {
                numCredit++;
                sumOfCredit += credit.amount;
                fillInReportGroup(credit);
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, credit);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addGiftCardCredit(giftCardCredit giftCardCredit)
        {
            if (numAccountUpdates == 0)
            {
                numGiftCardCredit++;
                sumOfGiftCardCredit += giftCardCredit.creditAmount;
                fillInReportGroup(giftCardCredit);
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, giftCardCredit);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addSale(sale sale)
        {
            if (numAccountUpdates == 0)
            {
                numSale++;
                sumOfSale += sale.amount;
                fillInReportGroup(sale);
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, sale);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addAuthReversal(authReversal authReversal)
        {
            if (numAccountUpdates == 0)
            {
                numAuthReversal++;
                sumOfAuthReversal += authReversal.amount;
                fillInReportGroup(authReversal);
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, authReversal);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }
        
        public void addTransactionReversal(transactionReversal txnReversal)
        {
            if (numAccountUpdates == 0)
            {
                numTransactionReversal++;
                sumOfTransactionReversal += txnReversal.amount;
                fillInReportGroup(txnReversal);
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, txnReversal);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addGiftCardAuthReversal(giftCardAuthReversal giftCardAuthReversal)
        {
            if (numAccountUpdates == 0)
            {
                numGiftCardAuthReversal++;
                sumOfGiftCardAuthReversal += giftCardAuthReversal.originalAmount;
                fillInReportGroup(giftCardAuthReversal);
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, giftCardAuthReversal);

            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addEcheckCredit(echeckCredit echeckCredit)
        {
            if (numAccountUpdates == 0)
            {
                numEcheckCredit++;
                sumOfEcheckCredit += echeckCredit.amount;
                fillInReportGroup(echeckCredit);
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, echeckCredit);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addEcheckVerification(echeckVerification echeckVerification)
        {
            if (numAccountUpdates == 0)
            {
                numEcheckVerification++;
                sumOfEcheckVerification += echeckVerification.amount;
                fillInReportGroup(echeckVerification);
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, echeckVerification);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addEcheckSale(echeckSale echeckSale)
        {
            if (numAccountUpdates == 0)
            {
                numEcheckSale++;
                sumOfEcheckSale += echeckSale.amount;
                fillInReportGroup(echeckSale);
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, echeckSale);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addRegisterTokenRequest(registerTokenRequestType registerTokenRequestType)
        {
            if (numAccountUpdates == 0)
            {
                numRegisterTokenRequest++;
                fillInReportGroup(registerTokenRequestType);
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, registerTokenRequestType);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addForceCapture(forceCapture forceCapture)
        {
            if (numAccountUpdates == 0)
            {
                numForceCapture++;
                sumOfForceCapture += forceCapture.amount;
                fillInReportGroup(forceCapture);
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, forceCapture);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addCaptureGivenAuth(captureGivenAuth captureGivenAuth)
        {
            if (numAccountUpdates == 0)
            {
                numCaptureGivenAuth++;
                sumOfCaptureGivenAuth += captureGivenAuth.amount;
                fillInReportGroup(captureGivenAuth);
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, captureGivenAuth);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addEcheckRedeposit(echeckRedeposit echeckRedeposit)
        {
            if (numAccountUpdates == 0)
            {
                numEcheckRedeposit++;
                fillInReportGroup(echeckRedeposit);
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, echeckRedeposit);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addEcheckPreNoteSale(echeckPreNoteSale echeckPreNoteSale)
        {
            if (numAccountUpdates == 0)
            {
                numEcheckPreNoteSale++;
                fillInReportGroup(echeckPreNoteSale);
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, echeckPreNoteSale);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addEcheckPreNoteCredit(echeckPreNoteCredit echeckPreNoteCredit)
        {
            if (numAccountUpdates == 0)
            {
                numEcheckPreNoteCredit++;
                fillInReportGroup(echeckPreNoteCredit);
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, echeckPreNoteCredit);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addUpdateCardValidationNumOnToken(updateCardValidationNumOnToken updateCardValidationNumOnToken)
        {
            if (numAccountUpdates == 0)
            {
                numUpdateCardValidationNumOnToken++;
                fillInReportGroup(updateCardValidationNumOnToken);
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, updateCardValidationNumOnToken);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addUpdateSubscription(updateSubscription updateSubscription)
        {
            if (numAccountUpdates == 0)
            {
                numUpdateSubscriptions++;
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, updateSubscription);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addCancelSubscription(cancelSubscription cancelSubscription)
        {
            if (numAccountUpdates == 0)
            {
                numCancelSubscriptions++;
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, cancelSubscription);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addCreatePlan(createPlan createPlan)
        {
            if (numAccountUpdates == 0)
            {
                numCreatePlans++;
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, createPlan);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addUpdatePlan(updatePlan updatePlan)
        {
            if (numAccountUpdates == 0)
            {
                numUpdatePlans++;
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, updatePlan);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addActivate(activate activate)
        {
            if (numAccountUpdates == 0)
            {
                numActivates++;
                activateAmount += activate.amount;
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, activate);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addDeactivate(deactivate deactivate)
        {
            if (numAccountUpdates == 0)
            {
                numDeactivates++;
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, deactivate);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addLoad(load load)
        {
            if (numAccountUpdates == 0)
            {
                numLoads++;
                loadAmount += load.amount;
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, load);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addUnload(unload unload)
        {
            if (numAccountUpdates == 0)
            {
                numUnloads++;
                unloadAmount += unload.amount;
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, unload);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addBalanceInquiry(balanceInquiry balanceInquiry)
        {
            if (numAccountUpdates == 0)
            {
                numBalanceInquiries++;
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, balanceInquiry);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addAccountUpdate(accountUpdate accountUpdate)
        {
            if (isOnlyAccountUpdates())
            {
                numAccountUpdates++;
                fillInReportGroup(accountUpdate);
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, accountUpdate);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addSubmerchantCredit(submerchantCredit submerchantCredit)
        {
            if (numAccountUpdates == 0)
            {
                numSubmerchantCredit++;
                submerchantCreditAmount += (long)submerchantCredit.amount;
                fillInReportGroup(submerchantCredit);
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, submerchantCredit);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addPayFacCredit(payFacCredit payFacCredit)
        {
            if (numAccountUpdates == 0)
            {
                numPayFacCredit++;
                payFacCreditAmount += (long)payFacCredit.amount;
                fillInReportGroup(payFacCredit);
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, payFacCredit);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addReserveCredit(reserveCredit reserveCredit)
        {
            if (numAccountUpdates == 0)
            {
                numReserveCredit++;
                reserveCreditAmount += (long)reserveCredit.amount;
                fillInReportGroup(reserveCredit);
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, reserveCredit);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addVendorCredit(vendorCredit vendorCredit)
        {
            if (numAccountUpdates == 0)
            {
                numVendorCredit++;
                vendorCreditAmount += (long)vendorCredit.amount;
                fillInReportGroup(vendorCredit);
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, vendorCredit);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addCustomerCredit(customerCredit customerCredit)
        {
            if (numAccountUpdates == 0)
            {
                numCustomerCredit++;
                customerCreditAmount += (long)customerCredit.amount;
                fillInReportGroup(customerCredit);
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, customerCredit);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addPayoutOrgCredit(payoutOrgCredit payoutOrgCredit)
        {
            if (numAccountUpdates == 0)
            {
                numPayoutOrgCredit++;
                payoutOrgCreditAmount += (long)payoutOrgCredit.amount;
                fillInReportGroup(payoutOrgCredit);
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, payoutOrgCredit);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addPhysicalCheckCredit(physicalCheckCredit physicalCheckCredit)
        {
            if (numAccountUpdates == 0)
            {
                numPhysicalCheckCredit++;
                physicalCheckCreditAmount += (long)physicalCheckCredit.amount;
                fillInReportGroup(physicalCheckCredit);
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, physicalCheckCredit);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addSubmerchantDebit(submerchantDebit submerchantDebit)
        {
            if (numAccountUpdates == 0)
            {
                numSubmerchantDebit++;
                submerchantDebitAmount += (long)submerchantDebit.amount;
                fillInReportGroup(submerchantDebit);
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, submerchantDebit);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addPayFacDebit(payFacDebit payFacDebit)
        {
            if (numAccountUpdates == 0)
            {
                numPayFacDebit++;
                payFacDebitAmount += (long)payFacDebit.amount;
                fillInReportGroup(payFacDebit);
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, payFacDebit);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addReserveDebit(reserveDebit reserveDebit)
        {
            if (numAccountUpdates == 0)
            {
                numReserveDebit++;
                reserveDebitAmount += (long)reserveDebit.amount;
                fillInReportGroup(reserveDebit);
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, reserveDebit);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addVendorDebit(vendorDebit vendorDebit)
        {
            if (numAccountUpdates == 0)
            {
                numVendorDebit++;
                vendorDebitAmount += (long)vendorDebit.amount;
                fillInReportGroup(vendorDebit);
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, vendorDebit);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addCustomerDebit(customerDebit customerDebit)
        {
            if (numAccountUpdates == 0)
            {
                numCustomerDebit++;
                customerDebitAmount += (long)customerDebit.amount;
                fillInReportGroup(customerDebit);
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, customerDebit);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }
        
        public void addPayoutOrgDebit(payoutOrgDebit payoutOrgDebit)
        {
            if (numAccountUpdates == 0)
            {
                numPayoutOrgDebit++;
                payoutOrgDebitAmount += (long)payoutOrgDebit.amount;
                fillInReportGroup(payoutOrgDebit);
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, payoutOrgDebit);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addPhysicalCheckDebit(physicalCheckDebit physicalCheckDebit)
        {
            if (numAccountUpdates == 0)
            {
                numPhysicalCheckDebit++;
                physicalCheckDebitAmount += (long)physicalCheckDebit.amount;
                fillInReportGroup(physicalCheckDebit);
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, physicalCheckDebit);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addFundingInstructionVoid(fundingInstructionVoid fundingInstructionVoid)
        {
            if (numAccountUpdates == 0)
            {
                numFundingInstructionVoid++;
                fillInReportGroup(fundingInstructionVoid);
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, fundingInstructionVoid);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void addfastAccessFunding(fastAccessFunding fastAccessFunding)
        {
            if (numAccountUpdates == 0)
            {
                numFastAccessFunding++;
                fastAccessFundingAmount += (long)fastAccessFunding.amount;
                fillInReportGroup(fastAccessFunding);
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, fastAccessFunding);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }


        public void addTranslateToLowValueTokenRequest(translateToLowValueTokenRequest translateToLowValueTokenRequest)
        {
            if (numAccountUpdates == 0)
            {
                numTranslateToLowValueTokenRequests++;
                fillInReportGroup(translateToLowValueTokenRequest);
                tempBatchFilePath = saveElement(cnpFile, cnpTime, tempBatchFilePath, translateToLowValueTokenRequest);
            }
            else
            {
                throw new CnpOnlineException(accountUpdateErrorMessage);
            }
        }

        public void SameDayFunding(bool setSameDayFunding)
        {
            sameDayFunding = setSameDayFunding;
        }


        // Create an xml file for the batch request.
        public string Serialize()
        {
            var xmlHeader = generateXmlHeader();

            var xmlFooter = "</batchRequest>\r\n";

            batchFilePath = cnpFile.createRandomFile(requestDirectory, null, "_batchRequest.xml", cnpTime);

            cnpFile.AppendLineToFile(batchFilePath, xmlHeader);
            cnpFile.AppendFileToFile(batchFilePath, tempBatchFilePath);
            cnpFile.AppendLineToFile(batchFilePath, xmlFooter);

            tempBatchFilePath = null;

            return batchFilePath;
        }

        public string generateXmlHeader()
        {
            var xmlHeader = "\r\n<batchRequest id=\"" + id + "\"\r\n";

            if (numAuthorization != 0)
            {
                xmlHeader += "numAuths=\"" + numAuthorization + "\"\r\n";
                xmlHeader += "authAmount=\"" + sumOfAuthorization + "\"\r\n";
            }

            if (numAuthReversal != 0)
            {
                xmlHeader += "numAuthReversals=\"" + numAuthReversal + "\"\r\n";
                xmlHeader += "authReversalAmount=\"" + sumOfAuthReversal + "\"\r\n";
            }

            if (numTransactionReversal != 0)
            {
                xmlHeader += "numTransactionReversals=\"" + numTransactionReversal + "\"\r\n";
                xmlHeader += "transactionReversalAmount=\"" + sumOfTransactionReversal + "\"\r\n"; 
            }

            if (numGiftCardAuthReversal != 0)
            {
                xmlHeader += "numGiftCardAuthReversals=\"" + numGiftCardAuthReversal + "\"\r\n";
                xmlHeader += "giftCardAuthReversalOriginalAmount=\"" + sumOfGiftCardAuthReversal + "\"\r\n";
            }

            if (numCapture != 0)
            {
                xmlHeader += "numCaptures=\"" + numCapture + "\"\r\n";
                xmlHeader += "captureAmount=\"" + sumOfCapture + "\"\r\n";
            }

            if (numGiftCardCapture != 0)
            {
                xmlHeader += "numGiftCardCaptures=\"" + numGiftCardCapture + "\"\r\n";
                xmlHeader += "giftCardCaptureAmount=\"" + sumOfGiftCardCapture + "\"\r\n";
            }

            if (numCredit != 0)
            {

                xmlHeader += "numCredits=\"" + numCredit + "\"\r\n";
                xmlHeader += "creditAmount=\"" + sumOfCredit + "\"\r\n";
            }

            if (numGiftCardCredit != 0)
            {
                xmlHeader += "numGiftCardCredits=\"" + numGiftCardCredit + "\"\r\n";
                xmlHeader += "giftCardCreditAmount=\"" + sumOfGiftCardCredit + "\"\r\n";
            }

            if (numForceCapture != 0)
            {

                xmlHeader += "numForceCaptures=\"" + numForceCapture + "\"\r\n";
                xmlHeader += "forceCaptureAmount=\"" + sumOfForceCapture + "\"\r\n";
            }

            if (numSale != 0)
            {

                xmlHeader += "numSales=\"" + numSale + "\"\r\n";
                xmlHeader += "saleAmount=\"" + sumOfSale + "\"\r\n";
            }

            if (numCaptureGivenAuth != 0)
            {

                xmlHeader += "numCaptureGivenAuths=\"" + numCaptureGivenAuth + "\"\r\n";
                xmlHeader += "captureGivenAuthAmount=\"" + sumOfCaptureGivenAuth + "\"\r\n";
            }

            if (numEcheckSale != 0)
            {

                xmlHeader += "numEcheckSales=\"" + numEcheckSale + "\"\r\n";
                xmlHeader += "echeckSalesAmount=\"" + sumOfEcheckSale + "\"\r\n";
            }

            if (numEcheckCredit != 0)
            {

                xmlHeader += "numEcheckCredit=\"" + numEcheckCredit + "\"\r\n";
                xmlHeader += "echeckCreditAmount=\"" + sumOfEcheckCredit + "\"\r\n";
            }

            if (numEcheckVerification != 0)
            {

                xmlHeader += "numEcheckVerification=\"" + numEcheckVerification + "\"\r\n";
                xmlHeader += "echeckVerificationAmount=\"" + sumOfEcheckVerification + "\"\r\n";
            }

            if (numEcheckRedeposit != 0)
            {
                xmlHeader += "numEcheckRedeposit=\"" + numEcheckRedeposit + "\"\r\n";
            }

            if (numEcheckPreNoteSale != 0)
            {
                xmlHeader += "numEcheckPreNoteSale=\"" + numEcheckPreNoteSale + "\"\r\n";
            }

            if (numEcheckPreNoteCredit != 0)
            {
                xmlHeader += "numEcheckPreNoteCredit=\"" + numEcheckPreNoteCredit + "\"\r\n";
            }

            if (numAccountUpdates != 0)
            {
                xmlHeader += "numAccountUpdates=\"" + numAccountUpdates + "\"\r\n";
            }

            if (numRegisterTokenRequest != 0)
            {
                xmlHeader += "numTokenRegistrations=\"" + numRegisterTokenRequest + "\"\r\n";
            }

            if (numUpdateCardValidationNumOnToken != 0)
            {
                xmlHeader += "numUpdateCardValidationNumOnTokens=\"" + numUpdateCardValidationNumOnToken + "\"\r\n";
            }

            if (numUpdateSubscriptions != 0)
            {
                xmlHeader += "numUpdateSubscriptions=\"" + numUpdateSubscriptions + "\"\r\n";
            }

            if (numCancelSubscriptions != 0)
            {
                xmlHeader += "numCancelSubscriptions=\"" + numCancelSubscriptions + "\"\r\n";
            }

            if (numCreatePlans != 0)
            {
                xmlHeader += "numCreatePlans=\"" + numCreatePlans + "\"\r\n";
            }

            if (numUpdatePlans != 0)
            {
                xmlHeader += "numUpdatePlans=\"" + numUpdatePlans + "\"\r\n";
            }

            if (numActivates != 0)
            {
                xmlHeader += "numUpdateActivates=\"" + numActivates + "\"\r\n";
                xmlHeader += "activateAmount=\"" + activateAmount + "\"\r\n";
            }

            if (numDeactivates != 0)
            {
                xmlHeader += "numDeactivates=\"" + numDeactivates + "\"\r\n";
            }

            if (numLoads != 0)
            {
                xmlHeader += "numLoads=\"" + numLoads + "\"\r\n";
                xmlHeader += "loadAmount=\"" + loadAmount + "\"\r\n";
            }

            if (numUnloads != 0)
            {
                xmlHeader += "numUnloads=\"" + numUnloads + "\"\r\n";
                xmlHeader += "unloadAmount=\"" + unloadAmount + "\"\r\n";
            }

            if (numBalanceInquiries != 0)
            {
                xmlHeader += "numBalanceInquirys=\"" + numBalanceInquiries + "\"\r\n";
            }

            if (numPayFacCredit != 0)
            {

                xmlHeader += "numPayFacCredit=\"" + numPayFacCredit + "\"\r\n";
                xmlHeader += "payFacCreditAmount=\"" + payFacCreditAmount + "\"\r\n";
            }

            if (numSubmerchantCredit != 0)
            {

                xmlHeader += "numSubmerchantCredit=\"" + numSubmerchantCredit + "\"\r\n";
                xmlHeader += "submerchantCreditAmount=\"" + submerchantCreditAmount + "\"\r\n";
            }

            if (numReserveCredit != 0)
            {

                xmlHeader += "numReserveCredit=\"" + numReserveCredit + "\"\r\n";
                xmlHeader += "reserveCreditAmount=\"" + reserveCreditAmount + "\"\r\n";
            }

            if (numVendorCredit != 0)
            {

                xmlHeader += "numVendorCredit=\"" + numVendorCredit + "\"\r\n";
                xmlHeader += "vendorCreditAmount=\"" + vendorCreditAmount + "\"\r\n";
            }

            if (numCustomerCredit != 0)
            {

                xmlHeader += "numCustomerCredit=\"" + numCustomerCredit + "\"\r\n";
                xmlHeader += "customerCreditAmount=\"" + customerCreditAmount + "\"\r\n";
            }

            if (numPhysicalCheckCredit != 0)
            {

                xmlHeader += "numPhysicalCheckCredit=\"" + numPhysicalCheckCredit + "\"\r\n";
                xmlHeader += "physicalCheckCreditAmount=\"" + physicalCheckCreditAmount + "\"\r\n";
            }
            if (numPayoutOrgCredit != 0)
            {

                xmlHeader += "numPayoutOrgCredit=\"" + numPayoutOrgCredit + "\"\r\n";
                xmlHeader += "numPayoutOrgCreditAmount=\"" + payoutOrgCreditAmount + "\"\r\n";
            }

            if (numFundingInstructionVoid != 0)
            {
                xmlHeader += "numFundingInstructionVoid=\"" + numFundingInstructionVoid + "\"\r\n";
            }

            if (numFastAccessFunding != 0)
            {
                xmlHeader += "numFastAccessFunding=\"" + numFastAccessFunding + "\"\r\n";
                xmlHeader += "fastAccessFundingAmount=\"" + fastAccessFundingAmount + "\"\r\n";
            }

            if (numTranslateToLowValueTokenRequests != 0)
            {
                xmlHeader += "numTranslateToLowValueTokenRequests=\"" + numTranslateToLowValueTokenRequests + "\"\r\n";
            }

            if (numPayFacDebit != 0)
            {

                xmlHeader += "numPayFacDebit=\"" + numPayFacDebit + "\"\r\n";
                xmlHeader += "payFacDebitAmount=\"" + payFacDebitAmount + "\"\r\n";
            }

            if (numSubmerchantDebit != 0)
            {

                xmlHeader += "numSubmerchantDebit=\"" + numSubmerchantDebit + "\"\r\n";
                xmlHeader += "submerchantDebitAmount=\"" + submerchantDebitAmount + "\"\r\n";
            }

            if (numReserveDebit != 0)
            {

                xmlHeader += "numReserveDebit=\"" + numReserveDebit + "\"\r\n";
                xmlHeader += "reserveDebitAmount=\"" + reserveDebitAmount + "\"\r\n";
            }

            if (numVendorDebit != 0)
            {

                xmlHeader += "numVendorDebit=\"" + numVendorDebit + "\"\r\n";
                xmlHeader += "vendorDebitAmount=\"" + vendorDebitAmount + "\"\r\n";
            }

            if (numCustomerDebit != 0)
            {

                xmlHeader += "numCustomerDebit=\"" + numCustomerDebit + "\"\r\n";
                xmlHeader += "customerDebitAmount=\"" + customerDebitAmount + "\"\r\n";
            }

            if (numPhysicalCheckDebit != 0)
            {

                xmlHeader += "numPhysicalCheckDebit=\"" + numPhysicalCheckDebit + "\"\r\n";
                xmlHeader += "physicalCheckDebitAmount=\"" + physicalCheckDebitAmount + "\"\r\n";
            }

            if (numPayoutOrgDebit != 0)
            {

                xmlHeader += "numPayoutOrgDebit=\"" + numPayoutOrgDebit + "\"\r\n";
                xmlHeader += "numPayoutOrgDebitAmount=\"" + payoutOrgDebitAmount + "\"\r\n";
            }

            if (sameDayFunding)
            {
                xmlHeader += "sameDayFunding=\"" + sameDayFunding.ToString().ToLower() + "\"\r\n";
            }

            xmlHeader += "merchantSdk=\"DotNet;" + CnpVersion.CurrentCNPSDKVersion + "\"\r\n";

            xmlHeader += "merchantId=\"" + config["merchantId"] + "\">\r\n";
            return xmlHeader;
        }

        private string saveElement(cnpFile cnpFile, cnpTime cnpTime, string filePath, transactionRequest element)
        {
            string fPath;
            fPath = cnpFile.createRandomFile(requestDirectory, Path.GetFileName(filePath), "_temp_batchRequest.xml", cnpTime);

            cnpFile.AppendLineToFile(fPath, element.Serialize());

            return fPath;
        }

        private void fillInReportGroup(transactionTypeWithReportGroup txn)
        {
            if (txn.reportGroup == null)
            {
                txn.reportGroup = config["reportGroup"];
            }
        }

        private void fillInReportGroup(transactionTypeWithReportGroupAndPartial txn)
        {
            if (txn.reportGroup == null)
            {
                txn.reportGroup = config["reportGroup"];
            }
        }

        private bool isOnlyAccountUpdates()
        {
            var result = numAuthorization == 0
                && numCapture == 0
                && numCredit == 0
                && numSale == 0
                && numAuthReversal == 0
                && numTransactionReversal == 0
                && numEcheckCredit == 0
                && numEcheckVerification == 0
                && numEcheckSale == 0
                && numRegisterTokenRequest == 0
                && numForceCapture == 0
                && numCaptureGivenAuth == 0
                && numEcheckRedeposit == 0
                && numEcheckPreNoteSale == 0
                && numEcheckPreNoteCredit == 0
                && numUpdateCardValidationNumOnToken == 0
                && numUpdateSubscriptions == 0
                && numCancelSubscriptions == 0
                && numCreatePlans == 0
                && numUpdatePlans == 0
                && numActivates == 0
                && numDeactivates == 0
                && numLoads == 0
                && numUnloads == 0
                && numBalanceInquiries == 0
                && numPayFacCredit == 0
                && numSubmerchantCredit == 0
                && numReserveCredit == 0
                && numVendorCredit == 0
                && numCustomerCredit == 0
                && numPayoutOrgCredit == 0
                && numPhysicalCheckCredit == 0
                && numFundingInstructionVoid == 0
                && numFastAccessFunding == 0
                && numTranslateToLowValueTokenRequests == 0
                && numPayFacDebit == 0
                && numSubmerchantDebit == 0
                && numReserveDebit == 0
                && numVendorDebit == 0
                && numCustomerDebit == 0
                && numPayoutOrgDebit == 0
                && numPhysicalCheckDebit == 0
                && numGiftCardAuthReversal == 0
                && numGiftCardCapture == 0
                && numGiftCardCredit == 0;

            return result;
        }
    }

    public class RFRRequest
    {
        public long cnpSessionId;
        public accountUpdateFileRequestData accountUpdateFileRequestData;

        private cnpTime cnpTime;
        private cnpFile cnpFile;
        private string requestDirectory;
        private string responseDirectory;

        private Dictionary<string, string> config;

        public RFRRequest()
        {
            config = new Dictionary<string, string>();
            ConfigManager configManager = new ConfigManager();
            config = configManager.getConfig();
            //config["url"] = Properties.Settings.Default.url;
            //config["reportGroup"] = Properties.Settings.Default.reportGroup;
            //config["username"] = Properties.Settings.Default.username;
            //config["printxml"] = Properties.Settings.Default.printxml;
            //config["timeout"] = Properties.Settings.Default.timeout;
            //config["proxyHost"] = Properties.Settings.Default.proxyHost;
            //config["merchantId"] = Properties.Settings.Default.merchantId;
            //config["password"] = Properties.Settings.Default.password;
            //config["proxyPort"] = Properties.Settings.Default.proxyPort;
            //config["sftpUrl"] = Properties.Settings.Default.sftpUrl;
            //config["sftpUsername"] = Properties.Settings.Default.sftpUsername;
            //config["sftpPassword"] = Properties.Settings.Default.sftpPassword;
            //config["requestDirectory"] = Properties.Settings.Default.requestDirectory;
            //config["responseDirectory"] = Properties.Settings.Default.responseDirectory;

            cnpTime = new cnpTime();
            cnpFile = new cnpFile();

            requestDirectory = Path.Combine(config["requestDirectory"],"Requests") + Path.DirectorySeparatorChar;
            responseDirectory = Path.Combine(config["responseDirectory"],"Responses") + Path.DirectorySeparatorChar;
        }

        public RFRRequest(Dictionary<string, string> config)
        {
            this.config = config;

            initializeRequest();
        }

        private void initializeRequest()
        {
            requestDirectory = Path.Combine(config["requestDirectory"],"Requests") + Path.DirectorySeparatorChar;
            responseDirectory = Path.Combine(config["responseDirectory"],"Responses") + Path.DirectorySeparatorChar;

            cnpFile = new cnpFile();
            cnpTime = new cnpTime();
        }

        public string getRequestDirectory()
        {
            return requestDirectory;
        }

        public string getResponseDirectory()
        {
            return responseDirectory;
        }

        public void setConfig(Dictionary<string, string> config)
        {
            this.config = config;
        }

        public void setCnpFile(cnpFile cnpFile)
        {
            this.cnpFile = cnpFile;
        }

        public cnpFile getCnpFile()
        {
            return cnpFile;
        }

        public void setCnpTime(cnpTime cnpTime)
        {
            this.cnpTime = cnpTime;
        }

        public cnpTime getCnpTime()
        {
            return cnpTime;
        }

        public string Serialize()
        {
            var xmlHeader = "\r\n<RFRRequest xmlns=\"http://www.vantivcnp.com/schema\">";
            var xmlFooter = "\r\n</RFRRequest>";

            var filePath = cnpFile.createRandomFile(requestDirectory, null, "_RFRRequest.xml", cnpTime);

            var xmlBody = "";

            if (accountUpdateFileRequestData != null)
            {
                xmlBody += "\r\n<accountUpdateFileRequestData>";
                xmlBody += accountUpdateFileRequestData.Serialize();
                xmlBody += "\r\n</accountUpdateFileRequestData>";
            }
            else
            {
                xmlBody += "\r\n<cnpSessionId>" + cnpSessionId + "</cnpSessionId>";
            }
            cnpFile.AppendLineToFile(filePath, xmlHeader);
            cnpFile.AppendLineToFile(filePath, xmlBody);
            cnpFile.AppendLineToFile(filePath, xmlFooter);

            return filePath;
        }
    }

    public partial class echeckPreNoteCredit : transactionTypeWithReportGroup
    {

        private string orderIdField;

        private orderSourceType orderSourceField;

        private contact billToAddressField;

        private echeckType echeckField;

        private merchantDataType merchantDataField;

        /// <remarks/>
        public string orderId
        {
            get
            {
                return orderIdField;
            }
            set
            {
                orderIdField = value;
            }
        }

        /// <remarks/>
        public orderSourceType orderSource
        {
            get
            {
                return orderSourceField;
            }
            set
            {
                orderSourceField = value;
            }
        }

        /// <remarks/>
        public contact billToAddress
        {
            get
            {
                return billToAddressField;
            }
            set
            {
                billToAddressField = value;
            }
        }

        /// <remarks/>
        public echeckType echeck
        {
            get
            {
                return echeckField;
            }
            set
            {
                echeckField = value;
            }
        }

        /// <remarks/>
        public merchantDataType merchantData
        {
            get
            {
                return merchantDataField;
            }
            set
            {
                merchantDataField = value;
            }
        }

        public override string Serialize()
        {
            var xml = "\r\n<echeckPreNoteCredit ";

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

            if (orderSource != null)
            {
                xml += "\r\n<orderSource>";
                xml += orderSource.Serialize();
                xml += "</orderSource>";
            }

            if (billToAddress != null)
            {
                xml += "\r\n<billToAddress>";
                xml += billToAddress.Serialize();
                xml += "\r\n</billToAddress>";
            }

            if (echeck != null)
            {
                xml += "\r\n<echeck>";
                xml += echeck.Serialize();
                xml += "\r\n</echeck>";
            }

            if (merchantData != null)
            {
                xml += "\r\n<merchantData>";
                xml += merchantData.Serialize();
                xml += "\r\n</merchantData>";
            }

            xml += "\r\n</echeckPreNoteCredit>";

            return xml;
        }
    }

    public partial class echeckPreNoteSale : transactionTypeWithReportGroup
    {

        private string orderIdField;

        private orderSourceType orderSourceField;

        private contact billToAddressField;

        private echeckType echeckField;

        private merchantDataType merchantDataField;

        /// <remarks/>
        public string orderId
        {
            get
            {
                return orderIdField;
            }
            set
            {
                orderIdField = value;
            }
        }

        /// <remarks/>
        public orderSourceType orderSource
        {
            get
            {
                return orderSourceField;
            }
            set
            {
                orderSourceField = value;
            }
        }

        /// <remarks/>
        public contact billToAddress
        {
            get
            {
                return billToAddressField;
            }
            set
            {
                billToAddressField = value;
            }
        }

        /// <remarks/>
        public echeckType echeck
        {
            get
            {
                return echeckField;
            }
            set
            {
                echeckField = value;
            }
        }

        /// <remarks/>
        public merchantDataType merchantData
        {
            get
            {
                return merchantDataField;
            }
            set
            {
                merchantDataField = value;
            }
        }

        public override string Serialize()
        {
            var xml = "\r\n<echeckPreNoteSale ";

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

            if (orderSource != null)
            {
                xml += "\r\n<orderSource>";
                xml += orderSource.Serialize();
                xml += "</orderSource>";
            }

            if (billToAddress != null)
            {
                xml += "\r\n<billToAddress>";
                xml += billToAddress.Serialize();
                xml += "\r\n</billToAddress>";
            }

            if (echeck != null)
            {
                xml += "\r\n<echeck>";
                xml += echeck.Serialize();
                xml += "\r\n</echeck>";
            }

            if (merchantData != null)
            {
                xml += "\r\n<merchantData>";
                xml += merchantData.Serialize();
                xml += "\r\n</merchantData>";
            }

            xml += "\r\n</echeckPreNoteSale>";

            return xml;
        }
    }

    public partial class submerchantCredit : transactionTypeWithReportGroup
    {

        public string fundingSubmerchantId { get; set; }

        public string submerchantName { get; set; }

        public string fundsTransferId { get; set; }

        public long? amount { get; set; }

        public echeckType accountInfo { get; set; }

        public string customIdentifier { get; set; }

        public override string Serialize()
        {
            var xml = "\r\n<submerchantCredit ";

            if (id != null)
                xml += "id=\"" + SecurityElement.Escape(id) + "\" ";
            if (customerId != null)
                xml += "customerId=\"" + SecurityElement.Escape(customerId) + "\" ";
            xml += "reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (fundingSubmerchantId != null)
                xml += "\r\n<fundingSubmerchantId>" + SecurityElement.Escape(fundingSubmerchantId) + "</fundingSubmerchantId>";
            if (submerchantName != null)
                xml += "\r\n<submerchantName>" + SecurityElement.Escape(submerchantName) + "</submerchantName>";
            if (fundsTransferId != null)
                xml += "\r\n<fundsTransferId>" + SecurityElement.Escape(fundsTransferId) + "</fundsTransferId>";
            if (amount != null)
                xml += "\r\n<amount>" + amount + "</amount>";
            if (accountInfo != null)
            {
                xml += "\r\n<accountInfo>";
                xml += accountInfo.Serialize();
                xml += "</accountInfo>";
            }
            if (customIdentifier != null)
                xml += "\r\n<customIdentifier>" + customIdentifier + "</customIdentifier>";

            xml += "\r\n</submerchantCredit>";

            return xml;
        }
    }

    public partial class payFacCredit : transactionTypeWithReportGroup
    {

        public string fundingSubmerchantId { get; set; }

        public string fundsTransferId { get; set; }

        public long? amount { get; set; }

        public override string Serialize()
        {
            var xml = "\r\n<payFacCredit ";

            if (id != null)
                xml += "id=\"" + SecurityElement.Escape(id) + "\" ";
            if (customerId != null)
                xml += "customerId=\"" + SecurityElement.Escape(customerId) + "\" ";
            xml += "reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (fundingSubmerchantId != null)
                xml += "\r\n<fundingSubmerchantId>" + SecurityElement.Escape(fundingSubmerchantId) + "</fundingSubmerchantId>";
            if (fundsTransferId != null)
                xml += "\r\n<fundsTransferId>" + SecurityElement.Escape(fundsTransferId) + "</fundsTransferId>";
            if (amount != null)
                xml += "\r\n<amount>" + amount + "</amount>";

            xml += "\r\n</payFacCredit>";

            return xml;
        }
    }

    public partial class reserveCredit : transactionTypeWithReportGroup
    {

        public string fundingSubmerchantId { get; set; }

        public string fundingCustomerId { get; set; }

        public string fundsTransferId { get; set; }

        public long? amount { get; set; }

        public override string Serialize()
        {
            var xml = "\r\n<reserveCredit ";

            if (id != null)
                xml += "id=\"" + SecurityElement.Escape(id) + "\" ";
            if (customerId != null)
                xml += "customerId=\"" + SecurityElement.Escape(customerId) + "\" ";
            xml += "reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (fundingSubmerchantId != null)
                xml += "\r\n<fundingSubmerchantId>" + SecurityElement.Escape(fundingSubmerchantId) + "</fundingSubmerchantId>";
            else if (fundingCustomerId != null)
                xml += "\r\n<fundingCustomerId>" + SecurityElement.Escape(fundingCustomerId) + "</fundingCustomerId>";
            if (fundsTransferId != null)
                xml += "\r\n<fundsTransferId>" + SecurityElement.Escape(fundsTransferId) + "</fundsTransferId>";
            if (amount != null)
                xml += "\r\n<amount>" + amount + "</amount>";

            xml += "\r\n</reserveCredit>";

            return xml;
        }
    }

    public partial class vendorCredit : transactionTypeWithReportGroup
    {

        public string fundingSubmerchantId { get; set; }

        public string fundingCustomerId { get; set; }

        public string vendorName { get; set; }

        public string fundsTransferId { get; set; }

        public long? amount { get; set; }

        public echeckType accountInfo { get; set; }

        public override string Serialize()
        {
            var xml = "\r\n<vendorCredit ";

            if (id != null)
                xml += "id=\"" + SecurityElement.Escape(id) + "\" ";
            if (customerId != null)
                xml += "customerId=\"" + SecurityElement.Escape(customerId) + "\" ";
            xml += "reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (fundingSubmerchantId != null)
                xml += "\r\n<fundingSubmerchantId>" + SecurityElement.Escape(fundingSubmerchantId) + "</fundingSubmerchantId>";
            else if (fundingCustomerId != null)
                xml += "\r\n<fundingCustomerId>" + SecurityElement.Escape(fundingCustomerId) + "</fundingCustomerId>";
            if (vendorName != null)
                xml += "\r\n<vendorName>" + SecurityElement.Escape(vendorName) + "</vendorName>";
            if (fundsTransferId != null)
                xml += "\r\n<fundsTransferId>" + SecurityElement.Escape(fundsTransferId) + "</fundsTransferId>";
            if (amount != null)
                xml += "\r\n<amount>" + amount + "</amount>";
            if (accountInfo != null)
            {
                xml += "\r\n<accountInfo>";
                xml += accountInfo.Serialize();
                xml += "</accountInfo>";
            }

            xml += "\r\n</vendorCredit>";

            return xml;
        }
    }

    public partial class customerCredit : transactionTypeWithReportGroup
    {
        public string fundingCustomerId { get; set; }

        public string customerName { get; set; }

        public string fundsTransferId { get; set; }

        public long? amount { get; set; }

        public echeckType accountInfo { get; set; }

        public string customIdentifier { get; set; }

        public override string Serialize()
        {
            var xml = "\r\n<customerCredit ";

            if (id != null)
                xml += "id=\"" + SecurityElement.Escape(id) + "\" ";
            if (customerId != null)
                xml += "customerId=\"" + SecurityElement.Escape(customerId) + "\" ";
            xml += "reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (fundingCustomerId != null)
                xml += "\r\n<fundingCustomerId>" + SecurityElement.Escape(fundingCustomerId) + "</fundingCustomerId>";
            if (customerName != null)
                xml += "\r\n<customerName>" + SecurityElement.Escape(customerName) + "</customerName>";
            if (fundsTransferId != null)
                xml += "\r\n<fundsTransferId>" + SecurityElement.Escape(fundsTransferId) + "</fundsTransferId>";
            if (amount != null)
                xml += "\r\n<amount>" + amount + "</amount>";
            if (accountInfo != null)
            {
                xml += "\r\n<accountInfo>";
                xml += accountInfo.Serialize();
                xml += "</accountInfo>";
            }
            if (customIdentifier != null)
                xml += "\r\n<customIdentifier>" + SecurityElement.Escape(customIdentifier) + "</customIdentifier>";

            xml += "\r\n</customerCredit>";

            return xml;
        }
    }

    public partial class payoutOrgCredit : transactionTypeWithReportGroup
    {

        public string fundingCustomerId { get; set; }

        public string fundsTransferId { get; set; }

        public long? amount { get; set; }

        public override string Serialize()
        {
            var xml = "\r\n<payoutOrgCredit ";

            if (id != null)
                xml += "id=\"" + SecurityElement.Escape(id) + "\" ";
            if (customerId != null)
                xml += "customerId=\"" + SecurityElement.Escape(customerId) + "\" ";
            xml += "reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (fundingCustomerId != null)
                xml += "\r\n<fundingCustomerId>" + SecurityElement.Escape(fundingCustomerId) + "</fundingCustomerId>";
            if (fundsTransferId != null)
                xml += "\r\n<fundsTransferId>" + SecurityElement.Escape(fundsTransferId) + "</fundsTransferId>";
            if (amount != null)
                xml += "\r\n<amount>" + amount + "</amount>";

            xml += "\r\n</payoutOrgCredit>";

            return xml;
        }
    }

    public partial class physicalCheckCredit : transactionTypeWithReportGroup
    {

        public string fundingSubmerchantId { get; set; }

        public string fundingCustomerId { get; set; }

        public string fundsTransferId { get; set; }

        public long? amount { get; set; }

        public override string Serialize()
        {
            var xml = "\r\n<physicalCheckCredit ";

            if (id != null)
                xml += "id=\"" + SecurityElement.Escape(id) + "\" ";
            if (customerId != null)
                xml += "customerId=\"" + SecurityElement.Escape(customerId) + "\" ";
            xml += "reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (fundingSubmerchantId != null)
                xml += "\r\n<fundingSubmerchantId>" + SecurityElement.Escape(fundingSubmerchantId) + "</fundingSubmerchantId>";
            else if (fundingCustomerId != null)
                xml += "\r\n<fundingCustomerId>" + SecurityElement.Escape(fundingCustomerId) + "</fundingCustomerId>";
            if (fundsTransferId != null)
                xml += "\r\n<fundsTransferId>" + SecurityElement.Escape(fundsTransferId) + "</fundsTransferId>";
            if (amount != null)
                xml += "\r\n<amount>" + amount + "</amount>";

            xml += "\r\n</physicalCheckCredit>";

            return xml;
        }
    }

    public partial class submerchantDebit : transactionTypeWithReportGroup
    {

        public string fundingSubmerchantId { get; set; }

        public string fundingCustomerId { get; set; }

        public string submerchantName { get; set; }

        public string fundsTransferId { get; set; }

        public long? amount { get; set; }

        public echeckType accountInfo { get; set; }

        public string customIdentifier { get; set; }

        public override string Serialize()
        {
            var xml = "\r\n<submerchantDebit ";

            if (id != null)
                xml += "id=\"" + SecurityElement.Escape(id) + "\" ";
            if (customerId != null)
                xml += "customerId=\"" + SecurityElement.Escape(customerId) + "\" ";
            xml += "reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (fundingSubmerchantId != null)
                xml += "\r\n<fundingSubmerchantId>" + SecurityElement.Escape(fundingSubmerchantId) + "</fundingSubmerchantId>";
            else if (fundingCustomerId != null)
                xml += "\r\n<fundingCustomerId>" + SecurityElement.Escape(fundingCustomerId) + "</fundingCustomerId>";
            if (submerchantName != null)
                xml += "\r\n<submerchantName>" + SecurityElement.Escape(submerchantName) + "</submerchantName>";
            if (fundsTransferId != null)
                xml += "\r\n<fundsTransferId>" + SecurityElement.Escape(fundsTransferId) + "</fundsTransferId>";
            if (amount != null)
                xml += "\r\n<amount>" + amount + "</amount>";
            if (accountInfo != null)
            {
                xml += "\r\n<accountInfo>";
                xml += accountInfo.Serialize();
                xml += "</accountInfo>";
            }
            if (customIdentifier != null)
                xml += "\r\n<customIdentifier>" + customIdentifier + "</customIdentifier>";

            xml += "\r\n</submerchantDebit>";

            return xml;
        }
    }

    public partial class payFacDebit : transactionTypeWithReportGroup
    {

        public string fundingSubmerchantId { get; set; }

        public string fundsTransferId { get; set; }

        public long? amount { get; set; }

        public override string Serialize()
        {
            var xml = "\r\n<payFacDebit ";

            if (id != null)
                xml += "id=\"" + SecurityElement.Escape(id) + "\" ";
            if (customerId != null)
                xml += "customerId=\"" + SecurityElement.Escape(customerId) + "\" ";
            xml += "reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (fundingSubmerchantId != null)
                xml += "\r\n<fundingSubmerchantId>" + SecurityElement.Escape(fundingSubmerchantId) + "</fundingSubmerchantId>";
            if (fundsTransferId != null)
                xml += "\r\n<fundsTransferId>" + SecurityElement.Escape(fundsTransferId) + "</fundsTransferId>";
            if (amount != null)
                xml += "\r\n<amount>" + amount + "</amount>";

            xml += "\r\n</payFacDebit>";

            return xml;
        }
    }

    public partial class reserveDebit : transactionTypeWithReportGroup
    {

        public string fundingSubmerchantId { get; set; }

        public string fundingCustomerId { get; set; }

        public string fundsTransferId { get; set; }

        public long? amount { get; set; }

        public override string Serialize()
        {
            var xml = "\r\n<reserveDebit ";

            if (id != null)
                xml += "id=\"" + SecurityElement.Escape(id) + "\" ";
            if (customerId != null)
                xml += "customerId=\"" + SecurityElement.Escape(customerId) + "\" ";
            xml += "reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (fundingSubmerchantId != null)
                xml += "\r\n<fundingSubmerchantId>" + SecurityElement.Escape(fundingSubmerchantId) + "</fundingSubmerchantId>";
            else if (fundingCustomerId != null)
                xml += "\r\n<fundingCustomerId>" + SecurityElement.Escape(fundingCustomerId) + "</fundingCustomerId>";
            if (fundsTransferId != null)
                xml += "\r\n<fundsTransferId>" + SecurityElement.Escape(fundsTransferId) + "</fundsTransferId>";
            if (amount != null)
                xml += "\r\n<amount>" + amount + "</amount>";

            xml += "\r\n</reserveDebit>";

            return xml;
        }
    }

    public partial class vendorDebit : transactionTypeWithReportGroup
    {

        public string fundingSubmerchantId { get; set; }

        public string fundingCustomerId { get; set; }

        public string vendorName { get; set; }

        public string fundsTransferId { get; set; }

        public long? amount { get; set; }

        public echeckType accountInfo { get; set; }

        public override string Serialize()
        {
            var xml = "\r\n<vendorDebit ";

            if (id != null)
                xml += "id=\"" + SecurityElement.Escape(id) + "\" ";
            if (customerId != null)
                xml += "customerId=\"" + SecurityElement.Escape(customerId) + "\" ";
            xml += "reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (fundingSubmerchantId != null)
                xml += "\r\n<fundingSubmerchantId>" + SecurityElement.Escape(fundingSubmerchantId) + "</fundingSubmerchantId>";
            else if (fundingCustomerId != null)
                xml += "\r\n<fundingCustomerId>" + SecurityElement.Escape(fundingCustomerId) + "</fundingCustomerId>";
            if (vendorName != null)
                xml += "\r\n<vendorName>" + SecurityElement.Escape(vendorName) + "</vendorName>";
            if (fundsTransferId != null)
                xml += "\r\n<fundsTransferId>" + SecurityElement.Escape(fundsTransferId) + "</fundsTransferId>";
            if (amount != null)
                xml += "\r\n<amount>" + amount + "</amount>";
            if (accountInfo != null)
            {
                xml += "\r\n<accountInfo>";
                xml += accountInfo.Serialize();
                xml += "</accountInfo>";
            }

            xml += "\r\n</vendorDebit>";

            return xml;
        }
    }

    public partial class customerDebit : transactionTypeWithReportGroup
    {
        public string fundingCustomerId { get; set; }

        public string customerName { get; set; }

        public string fundsTransferId { get; set; }

        public long? amount { get; set; }

        public echeckType accountInfo { get; set; }

        public string customIdentifier { get; set; }

        public override string Serialize()
        {
            var xml = "\r\n<customerDebit ";

            if (id != null)
                xml += "id=\"" + SecurityElement.Escape(id) + "\" ";
            if (customerId != null)
                xml += "customerId=\"" + SecurityElement.Escape(customerId) + "\" ";
            xml += "reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (fundingCustomerId != null)
                xml += "\r\n<fundingCustomerId>" + SecurityElement.Escape(fundingCustomerId) + "</fundingCustomerId>";
            if (customerName != null)
                xml += "\r\n<customerName>" + SecurityElement.Escape(customerName) + "</customerName>";
            if (fundsTransferId != null)
                xml += "\r\n<fundsTransferId>" + SecurityElement.Escape(fundsTransferId) + "</fundsTransferId>";
            if (amount != null)
                xml += "\r\n<amount>" + amount + "</amount>";
            if (accountInfo != null)
            {
                xml += "\r\n<accountInfo>";
                xml += accountInfo.Serialize();
                xml += "</accountInfo>";
            }
            if (customIdentifier != null)
                xml += "\r\n<customIdentifier>" + SecurityElement.Escape(customIdentifier) + "</customIdentifier>";

            xml += "\r\n</customerDebit>";

            return xml;
        }
    }

    public partial class payoutOrgDebit : transactionTypeWithReportGroup
    {

        public string fundingCustomerId { get; set; }

        public string fundsTransferId { get; set; }

        public long? amount { get; set; }

        public override string Serialize()
        {
            var xml = "\r\n<payoutOrgDebit ";

            if (id != null)
                xml += "id=\"" + SecurityElement.Escape(id) + "\" ";
            if (customerId != null)
                xml += "customerId=\"" + SecurityElement.Escape(customerId) + "\" ";
            xml += "reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (fundingCustomerId != null)
                xml += "\r\n<fundingCustomerId>" + SecurityElement.Escape(fundingCustomerId) + "</fundingCustomerId>";
            if (fundsTransferId != null)
                xml += "\r\n<fundsTransferId>" + SecurityElement.Escape(fundsTransferId) + "</fundsTransferId>";
            if (amount != null)
                xml += "\r\n<amount>" + amount + "</amount>";

            xml += "\r\n</payoutOrgDebit>";

            return xml;
        }
    }

    public partial class physicalCheckDebit : transactionTypeWithReportGroup
    {

        public string fundingSubmerchantId { get; set; }

        public string fundingCustomerId { get; set; }

        public string fundsTransferId { get; set; }

        public long? amount { get; set; }

        public override string Serialize()
        {
            var xml = "\r\n<physicalCheckDebit ";

            if (id != null)
                xml += "id=\"" + SecurityElement.Escape(id) + "\" ";
            if (customerId != null)
                xml += "customerId=\"" + SecurityElement.Escape(customerId) + "\" ";
            xml += "reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (fundingSubmerchantId != null)
                xml += "\r\n<fundingSubmerchantId>" + SecurityElement.Escape(fundingSubmerchantId) + "</fundingSubmerchantId>";
            else if (fundingCustomerId != null)
                xml += "\r\n<fundingCustomerId>" + SecurityElement.Escape(fundingCustomerId) + "</fundingCustomerId>";
            if (fundsTransferId != null)
                xml += "\r\n<fundsTransferId>" + SecurityElement.Escape(fundsTransferId) + "</fundsTransferId>";
            if (amount != null)
                xml += "\r\n<amount>" + amount + "</amount>";

            xml += "\r\n</physicalCheckDebit>";

            return xml;
        }
    }

    public partial class fundingInstructionVoid : transactionTypeWithReportGroup
    {
        public long? cnpTxnId { get; set; }

        public override string Serialize()
        {
            var xml = "\r\n<fundingInstructionVoid ";

            if (id != null)
                xml += "id=\"" + SecurityElement.Escape(id) + "\" ";
            if (customerId != null)
                xml += "customerId=\"" + SecurityElement.Escape(customerId) + "\" ";
            xml += "reportGroup=\"" + SecurityElement.Escape(reportGroup) + "\">";
            if (cnpTxnId != null)
                xml += "\r\n<cnpTxnId>" + cnpTxnId + "</cnpTxnId>";
            xml += "\r\n</fundingInstructionVoid>";

            return xml;
        }
    }
}
