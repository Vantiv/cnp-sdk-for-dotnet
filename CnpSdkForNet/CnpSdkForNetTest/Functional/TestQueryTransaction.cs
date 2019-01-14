using System.Collections.Generic;
using Xunit;

namespace Cnp.Sdk.Test.Functional
{
    public class TestQueryTransaction
    {
        private CnpOnline _cnp;
        private Dictionary<string, string> _config;

        public TestQueryTransaction()
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

        [Fact]
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
            Assert.Equal("150", queryResponse.response);
            Assert.Equal("Original transaction found", queryResponse.message);
            Assert.Equal("000", ((captureResponse)queryResponse.results_max10[0]).response);

        }

        [Fact]
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
            Assert.Equal("150", queryResponse.response);
            Assert.Equal("Original transaction found", queryResponse.message);
            Assert.Equal(2, queryResponse.results_max10.Count);

        }

        [Fact]
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
                Assert.True(false, "Unexpected type. Aborting the test");
            }
            else
            {
                var queryResponse = (queryTransactionUnavailableResponse)response;
                Assert.Equal("152", queryResponse.response);
                Assert.Equal("Original transaction found but response not yet available", queryResponse.message);
            }
            

           
        }

        [Fact]
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

            Assert.Equal("151", queryResponse.response);
            Assert.Equal("Original transaction not found", queryResponse.message);
        }
       
        // Add showStatusOnly of type yesNoType as optional
        [Fact]
        public void SimpleQueryTransactionShowStatusOnly()
        {
            var query = new queryTransaction
            {
                id = "myId",
                reportGroup = "myReportGroup",
                origId = "Auth1",
                origActionType = actionTypeEnum.A,
                origCnpTxnId = 54321,
                showStatusOnly = yesNoTypeEnum.Y
            };

            var response = _cnp.QueryTransaction(query);
            var queryResponse = (queryTransactionResponse)response;

            Assert.NotNull(queryResponse);
            Assert.Equal("150", queryResponse.response);
            Assert.Equal("Original transaction found", queryResponse.message);
            Assert.Equal(1, queryResponse.results_max10.Count);

        }
    }
}
