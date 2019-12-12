using System.Collections.Generic;
using NUnit.Framework;
using System.Threading;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestVendor
    {
        private CnpOnline _cnp;

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            _cnp = new CnpOnline();
        }

        [Test]
        public void VendorCredit()
        {
            var vendorCredit = new vendorCredit
            {
                // attributes.
                id = "1",
                reportGroup = "Default Report Group",
                // required child elements.
                accountInfo = new echeckType()
                {
                    accType = echeckAccountTypeEnum.Savings,
                    accNum = "1234",
                    routingNum = "12345678"
                },
                amount = 1500,
                fundingSubmerchantId = "value for fundingSubmerchantId",
                fundsTransferId = "value for fundsTransferId",
                vendorName = "Vantiv"
            };

            var response = _cnp.VendorCredit(vendorCredit);
            Assert.AreEqual("000", response.response);
        }

        [Test]
        public void VendorCreditWithFundingCustomerId()
        {
            var vendorCredit = new vendorCredit
            {
                // attributes.
                id = "1",
                reportGroup = "Default Report Group",
                // required child elements.
                accountInfo = new echeckType()
                {
                    accType = echeckAccountTypeEnum.Savings,
                    accNum = "1234",
                    routingNum = "12345678"
                },
                amount = 1500,
                fundingCustomerId = "value for fundingCustomerId",
                fundsTransferId = "value for fundsTransferId",
                vendorName = "Vantiv"
            };

            var response = _cnp.VendorCredit(vendorCredit);
            Assert.AreEqual("000", response.response);
        }

        [Test]
        public void TestVendorCreditAsync()
        {
            var vendorCredit = new vendorCredit
            {
                // attributes.
                id = "1",
                reportGroup = "Default Report Group",
                // required child elements.
                accountInfo = new echeckType()
                {
                    accType = echeckAccountTypeEnum.Savings,
                    accNum = "1234",
                    routingNum = "12345678"
                },
                amount = 1500,
                fundingSubmerchantId = "value for fundingSubmerchantId",
                fundsTransferId = "value for fundsTransferId",
                vendorName = "Vantiv"
            };

            CancellationToken cancellationToken = new CancellationToken(false);
            var response = _cnp.VendorCreditAsync(vendorCredit, cancellationToken);
            Assert.AreEqual("000", response.Result.response);
        }
        
        [Test]
        public void ReserveDebit()
        {
            var vendorDebit = new vendorDebit
            {
                // attributes.
                id = "1",
                reportGroup = "Default Report Group",
                // required child elements.
                accountInfo = new echeckType()
                {
                    accType = echeckAccountTypeEnum.Savings,
                    accNum = "1234",
                    routingNum = "12345678"
                },
                amount = 1500,
                fundingSubmerchantId = "value for fundingSubmerchantId",
                fundsTransferId = "value for fundsTransferId",
                vendorName = "WorldPay"
            };

            var response = _cnp.VendorDebit(vendorDebit);
            Assert.AreEqual("000", response.response);
        }

        [Test]
        public void ReserveDebitWithFundingCustomerId()
        {
            var vendorDebit = new vendorDebit
            {
                // attributes.
                id = "1",
                reportGroup = "Default Report Group",
                // required child elements.
                accountInfo = new echeckType()
                {
                    accType = echeckAccountTypeEnum.Savings,
                    accNum = "1234",
                    routingNum = "12345678"
                },
                amount = 1500,
                fundingSubmerchantId = "value for fundingSubmerchantId",
                fundsTransferId = "value for fundsTransferId",
                fundingCustomerId = "value for fundingCustomerId",
                vendorName = "WorldPay"
            };

            var response = _cnp.VendorDebit(vendorDebit);
            Assert.AreEqual("000", response.response);
        }

        [Test]
        public void TestReserveDebitAsync()
        {
            var vendorDebit = new vendorDebit
            {
                // attributes.
                id = "1",
                reportGroup = "Default Report Group",
                // required child elements.
                accountInfo = new echeckType()
                {
                    accType = echeckAccountTypeEnum.Savings,
                    accNum = "1234",
                    routingNum = "12345678"
                },
                amount = 1500,
                fundingSubmerchantId = "value for fundingSubmerchantId",
                fundsTransferId = "value for fundsTransferId",
                vendorName = "WorldPay"
            };

            CancellationToken cancellationToken = new CancellationToken(false);
            var response = _cnp.VendorDebitAsync(vendorDebit, cancellationToken);
            Assert.AreEqual("000", response.Result.response);
        }
    }
}
