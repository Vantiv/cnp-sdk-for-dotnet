using System.Collections.Generic;
using Xunit;

namespace Cnp.Sdk.Test.Functional
{
    public class TestSubmerchant
    {
        private CnpOnline _cnp;
        private Dictionary<string, string> _config;

        public TestSubmerchant()
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
        public void SubmerchantCredit()
        {
            var submerchantCredit = new submerchantCredit
            {
                // attributes.
                id = "1",
                reportGroup = "Default Report Group",
                // required child elements.
                accountInfo = new echeckType()
                {
                    accType = echeckAccountTypeEnum.Savings,
                    accNum = "1234",
                    routingNum = "12345678"
                },
                amount = 1500,
                fundingSubmerchantId = "value for fundingSubmerchantId",
                fundsTransferId = "value for fundsTransferId",
                submerchantName = "Vantiv",
                customIdentifier = "WorldPay"
            };

            var response = _cnp.SubmerchantCredit(submerchantCredit);
            Assert.Equal("000", response.response);
        }

        [Fact]
        public void SubmerchantDebit()
        {
            var submerchantDebit = new submerchantDebit
            {
                // attributes.
                id = "1",
                reportGroup = "Default Report Group",
                // required child elements.
                accountInfo = new echeckType()
                {
                    accType = echeckAccountTypeEnum.Savings,
                    accNum = "1234",
                    routingNum = "12345678"
                },
                amount = 1500,
                fundingSubmerchantId = "value for fundingSubmerchantId",
                fundsTransferId = "value for fundsTransferId",
                submerchantName = "Vantiv",
                customIdentifier = "WorldPay"
            };

            var response = _cnp.SubmerchantDebit(submerchantDebit);
            Assert.Equal("000", response.response);
        }
    }
}
