using System.Collections.Generic;
using NUnit.Framework;
using System.Threading;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestBalanceInquiry
    {
        private CnpOnline _cnp;

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            _cnp = new CnpOnline();
        }

        [Test]
        public void SimpleBalanceInquiry()
        {
            var balanceInquiry = new balanceInquiry
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                card = new giftCardCardType
                {
                    type = methodOfPaymentTypeEnum.GC,
                    number = "414100000000000000",
                    cardValidationNum = "123",
                    expDate = "1215",
                }
            };
        
        var response = _cnp.BalanceInquiry(balanceInquiry);
        Assert.AreEqual("000", response.response);
        Assert.AreEqual("sandbox", response.location);
        }

        [Test]
        public void TestBalanceInquiryAsync()
        {
            var balanceInquiry = new balanceInquiry
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                card = new giftCardCardType
                {
                    type = methodOfPaymentTypeEnum.GC,
                    number = "414100000000000000",
                    cardValidationNum = "123",
                    expDate = "1215",
                }
            };

            CancellationToken cancellationToken = new CancellationToken(false);
            var response = _cnp.BalanceInquiryAsync(balanceInquiry, cancellationToken);
            Assert.AreEqual("000", response.Result.response);
        }

        [Test]
        public void TestBalanceInquiryAsync_newMerchantId()
        {
            _cnp.SetMerchantId("1234");
            var balanceInquiry = new balanceInquiry
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                card = new giftCardCardType
                {
                    type = methodOfPaymentTypeEnum.GC,
                    number = "414100000000000000",
                    cardValidationNum = "123",
                    expDate = "1215",
                }
            };

            CancellationToken cancellationToken = new CancellationToken(false);
            var response = _cnp.BalanceInquiryAsync(balanceInquiry, cancellationToken);
            Assert.AreEqual("000", response.Result.response);
        }

    }
}
