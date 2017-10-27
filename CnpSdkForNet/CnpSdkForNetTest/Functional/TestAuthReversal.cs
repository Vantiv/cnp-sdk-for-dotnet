using System.Collections.Generic;
using NUnit.Framework;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestAuthReversal
    {
        private CnpOnline _cnp;
        private Dictionary<string, string> _config;

        [TestFixtureSetUp]
        public void SetUpCnp()
        {
            _config = new Dictionary<string, string>
            {
                {"url", "https://payments.vantivprelive.com/vap/communicator/online"},
                {"reportGroup", "Default Report Group"},
                {"username", "SDKTEAM"},
                {"version", "11.0"},
                {"timeout", "5000"},
                {"merchantId", "1288791"},
                {"password", "V3r5K6v7"},
                {"printxml", "true"},
                {"proxyHost", Properties.Settings.Default.proxyHost},
                {"proxyPort", Properties.Settings.Default.proxyPort},
                {"logFile", Properties.Settings.Default.logFile},
                {"neuterAccountNums", "true"}
            };

            _cnp = new CnpOnline(_config);
        }

        [Test]
        public void SimpleAuthReversal()
        {
            var reversal = new authReversal
            {
                id = "1",
                reportGroup = "Planets",
                cnpTxnId = 12345678000L,
                amount = 106,
                payPalNotes = "Notes"
            };

            var response = _cnp.AuthReversal(reversal);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void testAuthReversalHandleSpecialCharacters()
        {
            var reversal = new authReversal
            {
                id = "1",
                reportGroup = "Planets",
                cnpTxnId = 12345678000L,
                amount = 106,
                payPalNotes = "<'&\">"
            };


            var response = _cnp.AuthReversal(reversal);
            Assert.AreEqual("Approved", response.message);
        }
    }
}
