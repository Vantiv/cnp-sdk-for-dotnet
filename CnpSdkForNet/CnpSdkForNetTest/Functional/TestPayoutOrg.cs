using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;


namespace Cnp.Sdk.Test.Functional {

    [TestFixture]
    internal class TestPayoutOrg {
        private CnpOnline _cnp;
        private Dictionary<string, string> _config;

        [TestFixtureSetUp]
        public void SetUpCnp() {
            CommManager.reset();
            _config = new Dictionary<string, string> {
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
        public void PayoutOrgCredit()
        {
            var payoutOrgCredit = new payoutOrgCredit
            {
                id = "1",
                reportGroup = "Default Report Group",
                amount = 1500,
                fundingCustomerId = "value for fundingCustomerId",
                fundsTransferId = "value for fundsTransferId",
            };

            var response = _cnp.PayoutOrgCredit(payoutOrgCredit);
            Assert.AreEqual("000", response.response);
        }
        
        [Test]
        public void PayoutOrgCreditAsync()
        {
            var payoutOrgCredit = new payoutOrgCredit
            {
                id = "1",
                reportGroup = "Default Report Group",
                amount = 1500,
                fundingCustomerId = "value for fundingCustomerId",
                fundsTransferId = "value for fundsTransferId",
            };

            CancellationToken cancellationToken = new CancellationToken(false);
            var response = _cnp.PayoutOrgCreditAsync(payoutOrgCredit,cancellationToken);
            Assert.AreEqual("000", response.Result.response);
        }
        
        [Test]
        public void PayoutOrgDebit()
        {
            var payoutOrgDebit = new payoutOrgDebit
            {
                id = "1",
                reportGroup = "Default Report Group",
                amount = 1500,
                fundingCustomerId = "value for fundingCustomerId",
                fundsTransferId = "value for fundsTransferId",
            };

            var response = _cnp.PayoutOrgDebit(payoutOrgDebit);
            Assert.AreEqual("000", response.response);
        }
        
        [Test]
        public void PayoutOrgDebitAsync()
        {
            var payoutOrgDebit = new payoutOrgDebit
            {
                id = "1",
                reportGroup = "Default Report Group",
                amount = 1500,
                fundingCustomerId = "value for fundingCustomerId",
                fundsTransferId = "value for fundsTransferId",
            };

            CancellationToken cancellationToken = new CancellationToken(false);
            var response = _cnp.PayoutOrgDebitAsync(payoutOrgDebit,cancellationToken);
            Assert.AreEqual("000", response.Result.response);
        }
    }
}