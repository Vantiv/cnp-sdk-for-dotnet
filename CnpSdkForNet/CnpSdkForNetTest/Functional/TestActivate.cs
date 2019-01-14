using System.Collections.Generic;
using Xunit;

namespace Cnp.Sdk.Test.Functional
{
    public class TestActivate
    {
        private CnpOnline _cnp;
        private Dictionary<string, string> _config;

        public TestActivate()
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
        public void SimpleActivate()
        {
            var activate = new activate
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                amount = 1500,
                orderSource = orderSourceType.ecommerce,
                card = new giftCardCardType
                {
                    type = methodOfPaymentTypeEnum.GC,
                    number = "414100000000000000",
                    cardValidationNum = "123",
                    expDate = "1215"
                }
            };
            var response = _cnp.Activate(activate);
            Assert.Equal("000", response.response);
        }

        [Fact]
        public void VirtualGiftCardActivate()
        {
            var activate = new activate
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                amount = 1500,
                orderSource = orderSourceType.ecommerce,
                virtualGiftCard = new virtualGiftCardType
                {
                    accountNumberLength = 123,
                    giftCardBin = "123"
                }
            };

            var response = _cnp.Activate(activate);
            Assert.Equal("000", response.response);
        }
    }
}
