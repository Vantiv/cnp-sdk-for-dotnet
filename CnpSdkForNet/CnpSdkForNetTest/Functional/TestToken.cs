using System.Collections.Generic;
using Xunit;

namespace Cnp.Sdk.Test.Functional
{
    public class TestToken
    {
        private CnpOnline _cnp;
        private Dictionary<string, string> _config;

        public TestToken()
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
        public void SimpleToken()
        {
            var registerTokenRequest = new registerTokenRequestType
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                accountNumber = "1233456789103801",
            };

            var rtokenResponse = _cnp.RegisterToken(registerTokenRequest);
            Assert.True(rtokenResponse.message.Equals("Account number was successfully registered", System.StringComparison.InvariantCultureIgnoreCase));
        }


        [Fact]
        public void SimpleTokenWithPayPage()
        {
            var registerTokenRequest = new registerTokenRequestType
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                paypageRegistrationId = "1233456789101112",
            };

            var rtokenResponse = _cnp.RegisterToken(registerTokenRequest);
            Assert.True(rtokenResponse.message.Equals("Account number was successfully registered", System.StringComparison.InvariantCultureIgnoreCase));
        }

        [Fact]
        public void SimpleTokenWithEcheck()
        {
            var registerTokenRequest = new registerTokenRequestType
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                echeckForToken = new echeckForTokenType
                {
                    accNum = "12344565",
                    routingNum = "123476545"
                }
            };

            var rtokenResponse = _cnp.RegisterToken(registerTokenRequest);
            Assert.True(rtokenResponse.message.Equals("Account number was successfully registered", System.StringComparison.InvariantCultureIgnoreCase));
        }

        [Fact]
        public void SimpleTokenWithApplepay()
        {
            var registerTokenRequest = new registerTokenRequestType
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                applepay = new applepayType
                {
                    header = new applepayHeaderType
                    {
                        applicationData = "454657413164",
                        ephemeralPublicKey = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
                        publicKeyHash = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
                        transactionId = "1234"
                    },

                    data = "user",
                    signature = "sign",
                    version = "12345"
                }
            };

            var rtokenResponse = _cnp.RegisterToken(registerTokenRequest);
            Assert.True(rtokenResponse.message.Equals("Account number was successfully registered", System.StringComparison.InvariantCultureIgnoreCase));
            Assert.Equal("0", rtokenResponse.applepayResponse.transactionAmount);
        }

        [Fact]
        public void TokenEcheckMissingRequiredField()
        {
            var registerTokenRequest = new registerTokenRequestType
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                echeckForToken = new echeckForTokenType
                {
                    routingNum = "123476545"
                }
            };

            try
            {
                //expected exception;
                var rtokenResponse = _cnp.RegisterToken(registerTokenRequest);
            }
            catch (CnpOnlineException e)
            {
                Assert.True(e.Message.StartsWith("Error validating xml data against the schema"));
            }
        }

        [Fact]
        public void TestSimpleTokenWithNullableTypeField()
        {
            var registerTokenRequest = new registerTokenRequestType
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                accountNumber = "1233456789103801",
            };
            

            var rtokenResponse = _cnp.RegisterToken(registerTokenRequest);
            Assert.True(rtokenResponse.message.Equals("Account number was successfully registered", System.StringComparison.InvariantCultureIgnoreCase));
            Assert.Null(rtokenResponse.type);
        }
    }
}
