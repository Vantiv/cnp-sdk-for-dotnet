using System.Collections.Generic;
using NUnit.Framework;
using System;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestGiftCardParentReversal
    {
        private CnpOnline _cnp;

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            _cnp = new CnpOnline();
        }

        [Test]
        public void DepositReversal()
        {
            var reversal = new depositReversal
            {
                id = "1",
                reportGroup = "planets",
                cnpTxnId = 123456000,
                card = new giftCardCardType
                {
                    type = methodOfPaymentTypeEnum.GC,
                    number = "414100000000000000",
                    expDate = "1210",
                    pin = "1234"
                },


                originalRefCode = "123",
                originalAmount = 123,
                originalTxnTime = DateTime.Now,
                originalSystemTraceId = 123,
                originalSequenceNumber = "123456"
            };

            var response = _cnp.DepositReversal(reversal);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void RefundReversal()
        {
            var reversal = new refundReversal
            {
                id = "1",
                reportGroup = "planets",
                cnpTxnId = 123456000,
                card = new giftCardCardType
                {
                    type = methodOfPaymentTypeEnum.GC,
                    number = "414100000000000000",
                    expDate = "1210",
                    pin = "1234"
                },
                originalRefCode = "123",
                originalAmount = 123,
                originalTxnTime = DateTime.Now,
                originalSystemTraceId = 123,
                originalSequenceNumber = "123456"
            };

            var response = _cnp.RefundReversal(reversal);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void ActivateReversal()
        {
            var reversal = new activateReversal
            {
                id = "1",
                reportGroup = "planets",
                cnpTxnId = 123456000,
                virtualGiftCardBin = "123",
                card = new giftCardCardType
                {
                    type = methodOfPaymentTypeEnum.GC,
                    number = "414100000000000000",
                    expDate = "1210",
                    pin = "1234"
                },
                originalRefCode = "123",
                originalAmount = 123,
                originalTxnTime = DateTime.Now,
                originalSystemTraceId = 123,
                originalSequenceNumber = "123456"
            };

            var response = _cnp.ActivateReversal(reversal);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void DeactivateReversal()
        {
            var reversal = new deactivateReversal
            {
                id = "1",
                reportGroup = "planets",
                cnpTxnId = 123456000,
                card = new giftCardCardType
                {
                    type = methodOfPaymentTypeEnum.GC,
                    number = "414100000000000000",
                    expDate = "1210",
                    pin = "1234"
                },


                originalRefCode = "123",
                originalTxnTime = DateTime.Now,
                originalSystemTraceId = 123,
                originalSequenceNumber = "123456"
            };

            var response = _cnp.DeactivateReversal(reversal);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void LoadReversal()
        {
            var reversal = new loadReversal
            {
                id = "1",
                reportGroup = "planets",
                cnpTxnId = 123456000,
                card = new giftCardCardType
                {

                    type = methodOfPaymentTypeEnum.GC,
                    number = "414100000000000000",
                    expDate = "1210",
                    pin = "1234"
                },

                originalRefCode = "123",
                originalAmount = 123,
                originalTxnTime = DateTime.Now,
                originalSystemTraceId = 123,
                originalSequenceNumber = "123456"
            };

            var response = _cnp.LoadReversal(reversal);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void UnloadReversal()
        {
            var reversal = new unloadReversal
            {
                id = "1",
                reportGroup = "planets",
                cnpTxnId = 123456000,
                card = new giftCardCardType
                {
                    type = methodOfPaymentTypeEnum.GC,
                    number = "414100000000000000",
                    expDate = "1210",
                    pin = "1234"

                },

                originalRefCode = "123",
                originalAmount = 123,
                originalTxnTime = DateTime.Now,
                originalSystemTraceId = 123,
                originalSequenceNumber = "123456"
            };

            var response = _cnp.UnloadReversal(reversal);
            Assert.AreEqual("Approved", response.message);
        }
    }
}
