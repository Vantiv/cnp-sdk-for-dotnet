using System.Collections.Generic;
using Xunit;

namespace Cnp.Sdk.Test.Functional
{
    public class TestEcheckVerification
    {
        private CnpOnline _cnp;
        private Dictionary<string, string> _config;

        public TestEcheckVerification()
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
        public void SimpleEcheckVerification()
        {
            var echeckVerificationObject = new echeckVerification
            {
                id = "1",
                reportGroup = "Planets",
                amount = 123456,
                orderId = "12345",
                orderSource = orderSourceType.ecommerce,
                echeck = new echeckType
                {
                    accType = echeckAccountTypeEnum.Checking,
                    accNum = "12345657890",
                    routingNum = "123456789",
                    checkNum = "123455"
                },
                billToAddress = new contact
                {
                    name = "Bob",
                    city = "lowell",
                    state = "MA",
                    email = "cnp.com"
                }
            };

            var response = _cnp.EcheckVerification(echeckVerificationObject);
            Assert.True(response.message.Equals("Approved", System.StringComparison.InvariantCultureIgnoreCase));
        }

        [Fact]
        public void EcheckVerificationWithEcheckToken()
        {
            var echeckVerificationObject = new echeckVerification
            {
                id = "1",
                reportGroup = "Planets",
                amount = 123456,
                orderId = "12345",
                orderSource = orderSourceType.ecommerce,
                token = new echeckTokenType
                {
                    accType = echeckAccountTypeEnum.Checking,
                    cnpToken = "1234565789012",
                    routingNum = "123456789",
                    checkNum = "123455"
                },
                billToAddress = new contact
                {
                    name = "Bob",
                    city = "lowell",
                    state = "MA",
                    email = "cnp.com"
                }
            };

            echeckVerificationResponse response = _cnp.EcheckVerification(echeckVerificationObject);
            Assert.True(response.message.Equals("Approved", System.StringComparison.InvariantCultureIgnoreCase));
        }

        [Fact]
        public void TestMissingBillingField()
        {
            var echeckVerificationObject = new echeckVerification
            {
                id = "1",
                reportGroup = "Planets",
                amount = 123,
                orderId = "12345",
                orderSource = orderSourceType.ecommerce,
                echeck = new echeckType
                {
                    accType = echeckAccountTypeEnum.Checking,
                    accNum = "12345657890",
                    routingNum = "123456789",
                    checkNum = "123455"
                }
            };
        
            
            try
            {
                //expected exception;
                echeckVerificationResponse response = _cnp.EcheckVerification(echeckVerificationObject);
            }
            catch (CnpOnlineException e)
            {
                Assert.True(e.Message.StartsWith("Error validating xml data against the schema"));
            }
        }
    }
}
