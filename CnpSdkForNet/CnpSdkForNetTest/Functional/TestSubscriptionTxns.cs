using System;
using System.Collections.Generic;
using Xunit;

namespace Cnp.Sdk.Test.Functional
{
    public class TestSubscriptionTxns
    {
        private CnpOnline _cnp;
        private Dictionary<string, string> _config;

        public TestSubscriptionTxns()
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
        public void TestUpdateSubscription_Basic()
        {
            var update = new updateSubscription
            {
                billingDate = new DateTime(2002, 10, 9),
                billToAddress = new contact
                {
                    name = "Greg Dake",
                    city = "Lowell",
                    state = "MA",
                    email = "sdksupport@vantiv.com"
                },
                card = new cardType
                {
                    number = "4100000000000001",
                    expDate = "1215",
                    type = methodOfPaymentTypeEnum.VI
                },
                planCode = "abcdefg",
                subscriptionId = 12345
            };
            
            var response = _cnp.UpdateSubscription(update);
            Assert.Equal("Approved", response.message);
        }

        [Fact]
        public void TestUpdateSubscription_WithToken()
        {
            var update = new updateSubscription
            {
                billingDate = new DateTime(2002, 10, 9),
                billToAddress = new contact
                {
                    name = "Greg Dake",
                    city = "Lowell",
                    state = "MA",
                    email = "sdksupport@vantiv.com"
                },
                token = new cardTokenType
                {
                    cnpToken = "987654321098765432",
                    expDate = "0750",
                    cardValidationNum = "798",
                    type = methodOfPaymentTypeEnum.VI,
                    checkoutIdType = "012345678901234567"
                },
                planCode = "abcdefg",
                subscriptionId = 12345
            };

            var response = _cnp.UpdateSubscription(update);
            Assert.Equal("Approved", response.message);
        }
    }
}
