using System.Collections.Generic;
using Xunit;

namespace Cnp.Sdk.Test.Functional
{
    public class TestVendor
    {
        private CnpOnline _cnp;
        private Dictionary<string, string> _config;

        public TestVendor()
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
        public void VendorCredit()
        {
            var vendorCredit = new vendorCredit
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
                vendorName = "Vantiv"
            };

            var response = _cnp.VendorCredit(vendorCredit);
            Assert.Equal("000", response.response);
        }

        [Fact]
        public void ReserveDebit()
        {
            var vendorDebit = new vendorDebit
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
                vendorName = "WorldPay"
            };

            var response = _cnp.VendorDebit(vendorDebit);
            Assert.Equal("000", response.response);
        }
    }
}
