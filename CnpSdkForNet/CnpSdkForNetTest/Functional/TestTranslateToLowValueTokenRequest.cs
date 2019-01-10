using System.Collections.Generic;
using Xunit;

namespace Cnp.Sdk.Test.Functional
{
    public class TestTranslateToLowValueTokenRequest
    {
        private CnpOnline _cnp;
        private Dictionary<string, string> _config;

        public TestTranslateToLowValueTokenRequest()
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
        public void SimpleTranslateToLowValueTokenRequest()
        {

            var query = new translateToLowValueTokenRequest
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "2121",
                token = "822",
            };

            var response = _cnp.TranslateToLowValueTokenRequest(query);
            var queryResponse = (translateToLowValueTokenResponse)response;

            Assert.NotNull(queryResponse);
            Assert.Equal("822", queryResponse.response);

        }


        [Fact]
        public void SimpleTranslateToLowValueTokenRequestWithDiffToken()
        {

            var query = new translateToLowValueTokenRequest
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "2121",
                token = "821",
            };

            var response = _cnp.TranslateToLowValueTokenRequest(query);
            var queryResponse = (translateToLowValueTokenResponse)response;

            Assert.NotNull(queryResponse);
            Assert.Equal("821", queryResponse.response);

        }

        [Fact]
        public void SimpleTranslateToLowValueTokenRequestWithDefaultToken()
        {

            var query = new translateToLowValueTokenRequest
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "2121",
                token = "55",
            };

            var response = _cnp.TranslateToLowValueTokenRequest(query);
            var queryResponse = (translateToLowValueTokenResponse)response;

            Assert.NotNull(queryResponse);
            Assert.Equal("803", queryResponse.response);

        }

    }
}
