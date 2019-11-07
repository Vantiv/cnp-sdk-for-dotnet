using System.Collections.Generic;
using NUnit.Framework;
using System.Threading;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestPhysicalCheck
    {
        private CnpOnline _cnp;
        private Dictionary<string, string> _config;

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            CommManager.reset();
            _config = new Dictionary<string, string>
            {
                {"url", Properties.Settings.Default.url},
                {"reportGroup", "Default Report Group"},
                {"username", "DOTNET"},
                {"version", "11.0"},
                {"timeout", "5000"},
                {"merchantId", "101"},
                {"password", "TESTCASE"},
                {"printxml", "true"},
                {"proxyHost", Properties.Settings.Default.proxyHost},
                {"proxyPort", Properties.Settings.Default.proxyPort},
                {"logFile", Properties.Settings.Default.logFile},
                {"neuterAccountNums", "true"}
            };

            _cnp = new CnpOnline(_config);
        }

        [Test]
        public void PhysicalCheckCredit()
        {
            var physicalCheckCredit = new physicalCheckCredit
            {
                // attributes.
                id = "1",
                reportGroup = "Default Report Group",
                // required child elements.
                amount = 1500,
                fundingSubmerchantId = "value for fundingSubmerchantId",
                fundsTransferId = "value for fundsTransferId"
            };

            var response = _cnp.PhysicalCheckCredit(physicalCheckCredit);
            Assert.AreEqual("000", response.response);
        }
        
        [Test]
        public void PhysicalCheckCreditWithFundingCustomerId()
        {
            var physicalCheckCredit = new physicalCheckCredit
            {
                // attributes.
                id = "1",
                reportGroup = "Default Report Group",
                // required child elements.
                amount = 1500,
                fundingSubmerchantId = "value for fundingSubmerchantId",
                fundsTransferId = "value for fundsTransferId",
                fundingCustomerId = "value for fundingCustomerId",
            };

            var response = _cnp.PhysicalCheckCredit(physicalCheckCredit);
            Assert.AreEqual("000", response.response);
        }

        [Test]
        public void TestPhysicalCheckCreditAsync()
        {
            var physicalCheckCredit = new physicalCheckCredit
            {
                // attributes.
                id = "1",
                reportGroup = "Default Report Group",
                // required child elements.
                amount = 1500,
                fundingSubmerchantId = "value for fundingSubmerchantId",
                fundsTransferId = "value for fundsTransferId"
            };

            CancellationToken cancellationToken = new CancellationToken(false);
            var response = _cnp.PhysicalCheckCreditAsync(physicalCheckCredit, cancellationToken);
            Assert.AreEqual("000", response.Result.response);
        }
        
        [Test]
        public void PhysicalCheckDebit()
        {
            var physicalCheckDebit = new physicalCheckDebit
            {
                // attributes.
                id = "1",
                reportGroup = "Planets",
                // required child elements.
                amount = 1500,
                fundingSubmerchantId = "value for fundingSubmerchantId",
                fundsTransferId = "value for fundsTransferId"
            };

            var response = _cnp.PhysicalCheckDebit(physicalCheckDebit);
            Assert.AreEqual("000", response.response);
        }

        [Test]
        public void PhysicalCheckDebitWithFundingCustomerId()
        {
            var physicalCheckDebit = new physicalCheckDebit
            {
                // attributes.
                id = "1",
                reportGroup = "Planets",
                // required child elements.
                amount = 1500,
                fundingSubmerchantId = "value for fundingSubmerchantId",
                fundsTransferId = "value for fundsTransferId",
                fundingCustomerId = "value for fundingCustomerId",
            };

            var response = _cnp.PhysicalCheckDebit(physicalCheckDebit);
            Assert.AreEqual("000", response.response);
        }

        [Test]
        public void TestPhysicalCheckDebitAsync()
        {
            var physicalCheckDebit = new physicalCheckDebit
            {
                // attributes.
                id = "1",
                reportGroup = "Planets",
                // required child elements.
                amount = 1500,
                fundingSubmerchantId = "value for fundingSubmerchantId",
                fundsTransferId = "value for fundsTransferId"
            };

            CancellationToken cancellationToken = new CancellationToken(false);
            var response = _cnp.PhysicalCheckDebitAsync(physicalCheckDebit, cancellationToken);
            Assert.AreEqual("000", response.Result.response);
        }
    }
}
