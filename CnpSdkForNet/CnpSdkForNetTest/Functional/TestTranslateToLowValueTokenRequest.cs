using System.Collections.Generic;
using NUnit.Framework;
using System.Threading;

namespace Cnp.Sdk.Test.Functional
{
    internal class TestTranslateToLowValueTokenRequest
    {
        private CnpOnline _cnp;

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            CommManager.reset();
            _cnp = new CnpOnline();
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

        [Test]
        public void TestTranslateToLowValueTokenRequestAsync()
        {

            var query = new translateToLowValueTokenRequest
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "2121",
                token = "822",
            };

            CancellationToken cancellationToken = new CancellationToken(false);
            var response = _cnp.TranslateToLowValueTokenRequestAsync(query, cancellationToken);
            var queryResponse = (translateToLowValueTokenResponse)response.Result;

            Assert.NotNull(queryResponse);
            Assert.AreEqual("822", queryResponse.response);

        }

    }
}
