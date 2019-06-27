using System.Collections.Generic;
using NUnit.Framework;
using System.Threading;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestReserve
    {
        private CnpOnline _cnp;
        private Dictionary<string, string> _config;

        [TestFixtureSetUp]
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
        public void ReserveCredit()
        {
            var reserveCredit = new reserveCredit
            {
                // attributes.
                id = "1",
                reportGroup = "Default Report Group",
                // required child elements.
                amount = 1500,
                fundingSubmerchantId = "value for fundingSubmerchantId",
                fundsTransferId = "value for fundsTransferId"
            };

            var response = _cnp.ReserveCredit(reserveCredit);
            Assert.AreEqual("000", response.response);
        }

        [Test]
        public void TestReserveCreditAsync()
        {
            var reserveCredit = new reserveCredit
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
            var response = _cnp.ReserveCreditAsync(reserveCredit, cancellationToken);
            Assert.AreEqual("000", response.Result.response);
        }

        [Test]
        public void ReserveDebit()
        {
            var reserveDebit = new reserveDebit
            {
                // attributes.
                id = "1",
                reportGroup = "Planets",
                // required child elements.
                amount = 1500,
                fundingSubmerchantId = "value for fundingSubmerchantId",
                fundsTransferId = "value for fundsTransferId"
            };

            var response = _cnp.ReserveDebit(reserveDebit);
            Assert.AreEqual("000", response.response);
        }

        [Test]
        public void TestReserveDebitAsync()
        {
            var reserveDebit = new reserveDebit
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
            var response = _cnp.ReserveDebitAsync(reserveDebit, cancellationToken);
            Assert.AreEqual("000", response.Result.response);
        }
    }
}
