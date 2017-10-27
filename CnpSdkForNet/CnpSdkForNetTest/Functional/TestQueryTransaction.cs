using System.Collections.Generic;
using NUnit.Framework;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestQueryTransaction
    {
        private CnpOnline _cnp;
        private Dictionary<string, string> _config;

        [TestFixtureSetUp]
        public void SetUpCnp()
        {
            _config = new Dictionary<string, string>
            {
                {"url", "https://www.testvantivcnp.com/sandbox/communicator/online"},
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
        public void SimpleQueryTransaction()
        {
            var query = new queryTransaction
            {
                id = "myId",
                reportGroup = "myReportGroup",
                origId = "Deposit1",
                origActionType = actionTypeEnum.D,
                origCnpTxnId = 54321
            };

            var response = _cnp.QueryTransaction(query);
            var queryResponse = (queryTransactionResponse)response;

            Assert.NotNull(queryResponse);
            Assert.AreEqual("000", queryResponse.response);
            Assert.AreEqual("Original transaction found", queryResponse.message);
            Assert.AreEqual("000", ((captureResponse)queryResponse.results_max10[0]).response);

        }

        [Test]
        public void SimpleQueryTransaction_MultipleResponses()
        {
            var query = new queryTransaction
            {
                id = "myId",
                reportGroup = "myReportGroup",
                origId = "Auth2",
                origActionType = actionTypeEnum.A,
                origCnpTxnId = 54321
            };

            var response = _cnp.QueryTransaction(query);
            var queryResponse = (queryTransactionResponse)response;

            Assert.NotNull(queryResponse);
            Assert.AreEqual("000", queryResponse.response);
            Assert.AreEqual("Original transaction found", queryResponse.message);
            Assert.AreEqual(2, queryResponse.results_max10.Count);

            Assert.AreEqual("000", ((authorizationResponse)queryResponse.results_max10[0]).response);
            Assert.AreEqual("Approved", ((authorizationResponse)queryResponse.results_max10[0]).message);

            Assert.AreEqual("000", ((authorizationResponse)queryResponse.results_max10[1]).response);
            Assert.AreEqual("Approved", ((authorizationResponse)queryResponse.results_max10[1]).message);

        }

        [Test]
        public void TestQueryTransactionUnavailableResponse()
        {
            var query = new queryTransaction
            {
                id = "myId",
                reportGroup = "myReportGroup",
                origId = "Auth",
                origActionType = actionTypeEnum.A,
                origCnpTxnId = 54321
            };

            var response = _cnp.QueryTransaction(query);
            if(response is queryTransactionResponse)
            {
                Assert.Fail("Unexpected type. Aborting the test");
            }
            else
            {
                var queryResponse = (queryTransactionUnavailableResponse)response;
                Assert.AreEqual("152", queryResponse.response);
                Assert.AreEqual("Original transaction found but response not yet available", queryResponse.message);
            }
            

           
        }

        [Test]
        public void TestQueryTransactionNotFoundResponse()
        {
            var query = new queryTransaction
            {
                id = "myId",
                reportGroup = "myReportGroup",
                origId = "Auth0",
                origActionType = actionTypeEnum.A,
                origCnpTxnId = 54321
            };

            var response = _cnp.QueryTransaction(query);
            var queryResponse = (queryTransactionResponse)response;

            Assert.AreEqual("151", queryResponse.response);
            Assert.AreEqual("Original transaction not found", queryResponse.message);
        }
    }
}
