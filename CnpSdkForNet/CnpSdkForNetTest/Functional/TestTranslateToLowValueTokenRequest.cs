using System.Collections.Generic;
using NUnit.Framework;

namespace Cnp.Sdk.Test.Functional
{
    internal class TestTranslateToLowValueTokenRequest
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
            Assert.AreEqual("822", queryResponse.response);

        }


        [Test]
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
            Assert.AreEqual("821", queryResponse.response);

        }

        [Test]
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
            Assert.AreEqual("803", queryResponse.response);

        }

    }
}
