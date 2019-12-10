using System.Collections.Generic;
using NUnit.Framework;
using System;
using System.Threading;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestGiftCard
    {
        private CnpOnline _cnp;

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            CommManager.reset();
            _cnp = new CnpOnline();
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

        public void TestGiftCardAuthReversalAsync()
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

            CancellationToken cancellationToken = new CancellationToken(false);
            var response = _cnp.GiftCardAuthReversalAsync(giftCard, cancellationToken);
            Assert.AreEqual("000", response.Result.response);
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
        public void TestGiftCardCaptureAsync()
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

            CancellationToken cancellationToken = new CancellationToken(false);
            var response = _cnp.GiftCardCaptureAsync(giftCardCapture, cancellationToken);
            Assert.AreEqual("000", response.Result.response);
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

        [Test]
        public void TestGiftCardCreditWithOrderIdAsync()
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

            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            // set the calcellation timeout high so we should never tiemout 
            source.CancelAfter(1000);

            var response = _cnp.GiftCardCreditAsync(creditObj, token);
            Assert.AreEqual("000", response.Result.response);
        }
    }
}
