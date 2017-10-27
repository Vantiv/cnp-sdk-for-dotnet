using System.Collections.Generic;
using NUnit.Framework;
using System;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestGiftCard
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
        public void TestGiftCardAuthReversal()
        {
            var giftCard = new giftCardAuthReversal
            {
                id = "1",
                reportGroup = "Planets",
                cnpTxnId = 123,
                card = new giftCardCardType
                {
                    type = methodOfPaymentTypeEnum.GC,
                    number = "414100000000000000",
                    expDate = "1210"
                },

                originalRefCode = "abc123",
                originalAmount = 500,
                originalTxnTime = DateTime.Now,
                originalSystemTraceId = 123,
                originalSequenceNumber = "123456"
            };

            var response = _cnp.GiftCardAuthReversal(giftCard);
            Assert.AreEqual("000", response.response);
        }

        [Test]
        public void TestGiftCardCapture()
        {
            var giftCardCapture = new giftCardCapture
            {
                id = "1",
                reportGroup = "Planets",
                cnpTxnId = 123456000,
                captureAmount = 106,
                card = new giftCardCardType
                {
                    type = methodOfPaymentTypeEnum.GC,
                    number = "414100000000000000",
                    expDate = "1210"
                },
                originalRefCode = "abc123",
                originalAmount = 43534345,
                originalTxnTime = DateTime.Now
            };

            var response = _cnp.GiftCardCapture(giftCardCapture);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void TestGiftCardCapturePartial()
        {
            var giftCardCapture = new giftCardCapture
            {
                id = "1",
                cnpTxnId = 123456000,
                captureAmount = 106,
                card = new giftCardCardType
                {
                    type = methodOfPaymentTypeEnum.GC,
                    number = "414100000000000000",
                    expDate = "1210"
                },

                originalRefCode = "abc123",
                partial = true
            };

            var response = _cnp.GiftCardCapture(giftCardCapture);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void TestGiftCardCreditWithTxnId()
        {
            var creditObj = new giftCardCredit
            {
                id = "1",
                reportGroup = "planets",
                cnpTxnId = 123456000,
                creditAmount = 106,
                card = new giftCardCardType
                {
                    type = methodOfPaymentTypeEnum.GC,
                    number = "4100000000000000",
                    expDate = "1210"
                }
            };

            var response = _cnp.GiftCardCredit(creditObj);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void TestGiftCardCreditWithOrderId()
        {
            var creditObj = new giftCardCredit
            {
                id = "1",
                reportGroup = "planets",
                creditAmount = 106,
                orderId = "2111",
                orderSource = orderSourceType.echeckppd,
                card = new giftCardCardType
                {
                    type = methodOfPaymentTypeEnum.GC,
                    number = "4100000000000000",
                    expDate = "1210"
                }
            };

            var response = _cnp.GiftCardCredit(creditObj);
            Assert.AreEqual("Approved", response.message);
        }
    }
}
