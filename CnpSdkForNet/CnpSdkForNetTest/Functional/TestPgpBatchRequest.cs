using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Cnp.Sdk;
using System.IO;
using System.Linq;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    class TestPgpBatchRequest
    {
        private cnpRequest _cnp;
        private Dictionary<string, string> _config;
        
        
        [TestFixtureSetUp]
        public void SetUp()
        {
            _config = new Dictionary<string, string>();
            _config["url"] = Properties.Settings.Default.url;
            _config["reportGroup"] = Properties.Settings.Default.reportGroup;
            _config["username"] = Environment.GetEnvironmentVariable("encUsername");
            _config["printxml"] = Properties.Settings.Default.printxml;
            _config["timeout"] = Properties.Settings.Default.timeout;
            _config["proxyHost"] = Properties.Settings.Default.proxyHost;
            _config["merchantId"] = Environment.GetEnvironmentVariable("encMerchantId");
            _config["password"] = Environment.GetEnvironmentVariable("encPassword");
            _config["proxyPort"] = Properties.Settings.Default.proxyPort;
            _config["sftpUrl"] = Properties.Settings.Default.sftpUrl;
            _config["sftpUsername"] = Environment.GetEnvironmentVariable("encSftpUsername");
            _config["sftpPassword"] = Environment.GetEnvironmentVariable("encSftpPassword");
            _config["knownHostsFile"] = Properties.Settings.Default.knownHostsFile;
            _config["requestDirectory"] = Properties.Settings.Default.requestDirectory;
            _config["responseDirectory"] = Properties.Settings.Default.responseDirectory;
            _config["useEncryption"] = "true";
            _config["vantivPublicKeyId"] = Environment.GetEnvironmentVariable("vantivPublicKeyId");
            _config["pgpPassphrase"] = Environment.GetEnvironmentVariable("pgpPassphrase");
        }
        
        

        [Test]
        public void TestSimpleBatchPgp()
        {
            _cnp = new cnpRequest(_config);
            batchRequest cnpBatchRequest = new batchRequest(_config);
            cnpBatchRequest.SameDayFunding(true);
            cnpBatchRequest.id = "123";

            var payFacCredit = new payFacCredit
            {
                id = "id",
                fundingSubmerchantId = "123456789",
                fundsTransferId = "123467",
                amount = 107L
            };
            cnpBatchRequest.addPayFacCredit(payFacCredit);

            var payFacDebit = new payFacDebit
            {
                id = "id",
                fundingSubmerchantId = "123456789",
                fundsTransferId = "123467",
                amount = 107L
            };
            cnpBatchRequest.addPayFacDebit(payFacDebit);

            var submerchantCredit = new submerchantCredit
            {
                id = "id",
                fundingSubmerchantId = "123456789",
                submerchantName = "Test",
                fundsTransferId = "123467",
                amount = 107L,
                accountInfo = new echeckType
                {
                    accType = echeckAccountTypeEnum.Corporate,
                    accNum = "1092969901",
                    routingNum = "011075150",
                    checkNum = "123456789",
                    ccdPaymentInformation = "description"
                },
                customIdentifier = "abc123"
            };
            cnpBatchRequest.addSubmerchantCredit(submerchantCredit);

            var submerchantDebit = new submerchantDebit
            {
                id = "id",
                fundingSubmerchantId = "123456789",
                submerchantName = "Test",
                fundsTransferId = "123467",
                amount = 107L,
                accountInfo = new echeckType
                {
                    accType = echeckAccountTypeEnum.Corporate,
                    accNum = "1092969901",
                    routingNum = "011075150",
                    checkNum = "123456789",
                    ccdPaymentInformation = "description"
                },
                customIdentifier = "abc123"
            };
            cnpBatchRequest.addSubmerchantDebit(submerchantDebit);

            var reserveCredit = new reserveCredit
            {
                id = "id",
                fundingSubmerchantId = "123456789",
                fundsTransferId = "123467",
                amount = 107L
            };
            cnpBatchRequest.addReserveCredit(reserveCredit);

            var reserveDebit = new reserveDebit
            {
                id = "id",
                fundingSubmerchantId = "123456789",
                fundsTransferId = "123467",
                amount = 107L
            };
            cnpBatchRequest.addReserveDebit(reserveDebit);

            var vendorCredit = new vendorCredit
            {
                id = "id",
                fundingSubmerchantId = "123456789",
                vendorName = "Test",
                fundsTransferId = "123467",
                amount = 107L,
                accountInfo = new echeckType
                {
                    accType = echeckAccountTypeEnum.Corporate,
                    accNum = "1092969901",
                    routingNum = "011075150",
                    checkNum = "123456789",
                    ccdPaymentInformation = "description"
                }
            };
            cnpBatchRequest.addVendorCredit(vendorCredit);

            var vendorDebit = new vendorDebit
            {
                id = "id",
                fundingSubmerchantId = "123456789",
                vendorName = "Test",
                fundsTransferId = "123467",
                amount = 107L,
                accountInfo = new echeckType
                {
                    accType = echeckAccountTypeEnum.Corporate,
                    accNum = "1092969901",
                    routingNum = "011075150",
                    checkNum = "123456789",
                    ccdPaymentInformation = "description"
                }
            };
            cnpBatchRequest.addVendorDebit(vendorDebit);

            var physicalCheckCredit = new physicalCheckCredit
            {
                id = "id",
                fundingSubmerchantId = "123456789",
                fundsTransferId = "123467",
                amount = 107L
            };
            cnpBatchRequest.addPhysicalCheckCredit(physicalCheckCredit);

            var physicalCheckDebit = new physicalCheckDebit
            {
                id = "id",
                fundingSubmerchantId = "123456789",
                fundsTransferId = "123467",
                amount = 107L
            };
            cnpBatchRequest.addPhysicalCheckDebit(physicalCheckDebit);

            _cnp.addBatch(cnpBatchRequest);

            string batchName = _cnp.sendToCnp();
            
            //Check if the .xml batch request file exists inside "Requests" directory
            var requestDir = _cnp.getRequestDirectory();
            var entries = Directory.EnumerateFiles(requestDir);
            var targetEntry = Path.Combine(requestDir, batchName.Replace(".encrypted", ""));
            Assert.True(entries.Contains(targetEntry));
            
            // check if "encrypted" directory is present inside "Requests" directory
            var encryptedRequestDir = Path.Combine(requestDir, "encrypted");
            Assert.True(Directory.Exists(encryptedRequestDir));
            
            //Check if the .xml.encrypted batch request file exists inside "Requests/encrypted" directory
            entries = Directory.EnumerateFiles(encryptedRequestDir);
            targetEntry = Path.Combine(encryptedRequestDir, batchName);
            Assert.True(entries.Contains(targetEntry));

            _cnp.blockAndWaitForResponse(batchName, estimatedResponseTime(2 * 2, 10 * 2));

            cnpResponse cnpResponse = _cnp.receiveFromCnp(batchName);

            //Check if the .xml batch response file exists inside "Responses" directory
            var responseDir = _cnp.getResponseDirectory();
            entries = Directory.EnumerateFiles(responseDir);
            targetEntry = Path.Combine(responseDir, batchName.Replace(".encrypted", ""));
            Assert.True(entries.Contains(targetEntry));
            
            // check if "encrypted" directory is present inside "Responses" directory
            var encryptedResponseDir = Path.Combine(responseDir, "encrypted");
            Assert.True(Directory.Exists(encryptedResponseDir));
            
            //Check if the .xml.encrypted batch response file exists inside "Responses/encrypted" directory
            entries = Directory.EnumerateFiles(encryptedResponseDir);
            targetEntry = Path.Combine(encryptedResponseDir, batchName);
            Assert.True(entries.Contains(targetEntry));

            Assert.NotNull(cnpResponse);
            Assert.AreEqual("0", cnpResponse.response);
            Assert.AreEqual("Valid Format", cnpResponse.message);

            batchResponse cnpBatchResponse = cnpResponse.nextBatchResponse();
            while (cnpBatchResponse != null)
            {
                payFacCreditResponse payFacCreditResponse = cnpBatchResponse.nextPayFacCreditResponse();
                while (payFacCreditResponse != null)
                {
                    Assert.AreEqual("000", payFacCreditResponse.response);

                    payFacCreditResponse = cnpBatchResponse.nextPayFacCreditResponse();
                }

                payFacDebitResponse payFacDebitResponse = cnpBatchResponse.nextPayFacDebitResponse();
                while (payFacDebitResponse != null)
                {
                    Assert.AreEqual("000", payFacDebitResponse.response);

                    payFacDebitResponse = cnpBatchResponse.nextPayFacDebitResponse();
                }

                submerchantCreditResponse submerchantCreditResponse = cnpBatchResponse.nextSubmerchantCreditResponse();
                while (submerchantCreditResponse != null)
                {
                    Assert.AreEqual("000", submerchantCreditResponse.response);

                    submerchantCreditResponse = cnpBatchResponse.nextSubmerchantCreditResponse();
                }

                submerchantDebitResponse submerchantDebitResponse = cnpBatchResponse.nextSubmerchantDebitResponse();
                while (submerchantDebitResponse != null)
                {
                    Assert.AreEqual("000", submerchantDebitResponse.response);

                    submerchantDebitResponse = cnpBatchResponse.nextSubmerchantDebitResponse();
                }

                reserveCreditResponse reserveCreditResponse = cnpBatchResponse.nextReserveCreditResponse();
                while (reserveCreditResponse != null)
                {
                    Assert.AreEqual("000", reserveCreditResponse.response);

                    reserveCreditResponse = cnpBatchResponse.nextReserveCreditResponse();
                }

                reserveDebitResponse reserveDebitResponse = cnpBatchResponse.nextReserveDebitResponse();
                while (reserveDebitResponse != null)
                {
                    Assert.AreEqual("000", reserveDebitResponse.response);

                    reserveDebitResponse = cnpBatchResponse.nextReserveDebitResponse();
                }

                vendorCreditResponse vendorCreditResponse = cnpBatchResponse.nextVendorCreditResponse();
                while (vendorCreditResponse != null)
                {
                    Assert.AreEqual("000", vendorCreditResponse.response);

                    vendorCreditResponse = cnpBatchResponse.nextVendorCreditResponse();
                }

                vendorDebitResponse vendorDebitResponse = cnpBatchResponse.nextVendorDebitResponse();
                while (vendorDebitResponse != null)
                {
                    Assert.AreEqual("000", vendorDebitResponse.response);

                    vendorDebitResponse = cnpBatchResponse.nextVendorDebitResponse();
                }

                physicalCheckCreditResponse physicalCheckCreditResponse = cnpBatchResponse.nextPhysicalCheckCreditResponse();
                while (physicalCheckCreditResponse != null)
                {
                    Assert.AreEqual("000", physicalCheckCreditResponse.response);

                    physicalCheckCreditResponse = cnpBatchResponse.nextPhysicalCheckCreditResponse();
                }

                physicalCheckDebitResponse physicalCheckDebitResponse = cnpBatchResponse.nextPhysicalCheckDebitResponse();
                while (physicalCheckDebitResponse != null)
                {
                    Assert.AreEqual("000", physicalCheckDebitResponse.response);

                    physicalCheckDebitResponse = cnpBatchResponse.nextPhysicalCheckDebitResponse();
                }

                cnpBatchResponse = cnpResponse.nextBatchResponse();
            }
        }
        
        private int estimatedResponseTime(int numAuthsAndSales, int numRest)
        {
            return (int)(5 * 60 * 1000 + 2.5 * 1000 + numAuthsAndSales * (1 / 5) * 1000 + numRest * (1 / 50) * 1000) * 5;
        }
    }
}