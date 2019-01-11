using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace Cnp.Sdk.Test.Functional
{
    public class TestAuthReversal
    {

        private CnpOnline _cnp;
        private Dictionary<string, string> _config;

        public TestAuthReversal(ITestOutputHelper output)
        {

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

        [Fact]
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
            Assert.Equal("Approved", response.message);
        }

        [Fact]
        public void TestAuthReversalHandleSpecialCharacters()
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
            Assert.Equal("Approved", response.message);
        }
    }
}
