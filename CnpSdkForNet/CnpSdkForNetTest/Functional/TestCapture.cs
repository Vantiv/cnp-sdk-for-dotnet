using System.Collections.Generic;
using NUnit.Framework;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestCapture
    {
        private CnpOnline _cnp;
        private Dictionary<string, string> _config;

        [TestFixtureSetUp]
        public void SetUpCnp()
        {
            _config = new Dictionary<string, string>
            {
                {"url", "https://payments.vantivprelive.com/vap/communicator/online"},
                {"reportGroup", "Default Report Group"},
                {"username", "SDKTEAM"},
                {"version", "11.0"},
                {"timeout", "5000"},
                {"merchantId", "1288791"},
                {"password", "V3r5K6v7"},
                {"printxml", "true"},
                {"proxyHost", Properties.Settings.Default.proxyHost},
                {"proxyPort", Properties.Settings.Default.proxyPort},
                {"logFile", Properties.Settings.Default.logFile},
                {"neuterAccountNums", "true"}
            };

            _cnp = new CnpOnline(_config);
        }

        [Test]
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
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void simpleCaptureWithPartial()
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
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void complexCapture()
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
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
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
            Assert.AreEqual("Approved", response.message);
        }
    }
}
