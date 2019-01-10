using System.Collections.Generic;
using Xunit;
using System;

namespace Cnp.Sdk.Test.Functional
{
    public class TestGiftCard
    {
        private CnpOnline _cnp;
        private Dictionary<string, string> _config;

        public TestGiftCard()
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
            Assert.Equal("000", response.response);
        }

        [Fact]
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
            Assert.Equal("Approved", response.message);
        }

        [Fact]
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
            Assert.Equal("Approved", response.message);
        }

        [Fact]
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
            Assert.Equal("Approved", response.message);
        }

        [Fact]
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
            Assert.Equal("Approved", response.message);
        }
    }
}
