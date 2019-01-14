using System.Collections.Generic;
using Xunit;

namespace Cnp.Sdk.Test.Functional
{
    public class TestCapture
    {
        private CnpOnline _cnp;
        private Dictionary<string, string> _config;

        public TestCapture()
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
        public void SimpleCapture()
        {
            var capture = new capture
            {
                id = "1",
                cnpTxnId = 123456000,
                amount = 106,
                payPalNotes = "Notes",
                pin = "1234"
            };

            var response = _cnp.Capture(capture);
            Assert.Equal("Approved", response.message);
        }

        [Fact]
        public void SimpleCaptureWithPartial()
        {
            var capture = new capture
            {
                id = "1",
                cnpTxnId = 123456000,
                amount = 106,
                partial = true,
                payPalNotes = "Notes"
            };


            var response = _cnp.Capture(capture);
            Assert.Equal("Approved", response.message);
        }

        [Fact]
        public void ComplexCapture()
        {
            var capture = new capture
            {
                id = "1",
                cnpTxnId = 123456000,
                amount = 106,
                payPalNotes = "Notes",
                pin = "1234",
                enhancedData = new enhancedData
                {
                    customerReference = "Cnp",
                    salesTax = 50,
                    deliveryType = enhancedDataDeliveryType.TBD
                },
                payPalOrderComplete = true,
                customBilling = new customBilling
                {
                    phone = "51312345678",
                    city = "Lowell",
                    url = "test.com",
                    descriptor = "Nothing",
                }
            };

            var response = _cnp.Capture(capture);
            Assert.Equal("Approved", response.message);
        }

        [Fact]
        public void SimpleCaptureWithSpecial()
        {
            var capture = new capture
            {
                id = "1",
                cnpTxnId = 123456000,
                amount = 106,
                payPalNotes = "<'&\">"
            };
            
            var response = _cnp.Capture(capture);
            Assert.Equal("Approved", response.message);
        }

        [Fact]
        public void SimpleCaptureWithLodgingInfo()
        {
            var capture = new capture
            {
                id = "1",
                cnpTxnId = 123456000,
                amount = 106,
                payPalNotes = "<'&\">",
                lodgingInfo = new lodgingInfo
                {
                    hotelFolioNumber = "12345",
                    checkInDate = new System.DateTime(2017, 1, 18),
                    customerServicePhone = "854213",
                    lodgingCharges = new List<lodgingCharge>(),

                }
            };
            capture.lodgingInfo.lodgingCharges.Add(new lodgingCharge() { name = lodgingExtraChargeEnum.GIFTSHOP });
            var response = _cnp.Capture(capture);
            Assert.Equal("Approved", response.message);
        }
    }
}
