using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Cnp.Sdk;
using System.IO;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    class TestBatchRequest
    {
        private cnpRequest cnp;
        private Dictionary<string, string> invalidConfig;
        private Dictionary<string, string> invalidSftpConfig;

        [SetUp]
        public void setUpBeforeTest()
        {
            cnp = new cnpRequest();
        }

        [Test]
        public void SimpleBatch()
        {
            batchRequest cnpBatchRequest = new batchRequest();
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

            cnp.addBatch(cnpBatchRequest);

            string batchName = cnp.sendToCnp();

            cnp.blockAndWaitForResponse(batchName, estimatedResponseTime(2 * 2, 10 * 2));

            cnpResponse cnpResponse = cnp.receiveFromCnp(batchName);

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