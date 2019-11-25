using System.Collections.Generic;
using NUnit.Framework;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestActivate
    {
        private CnpOnline _cnp;

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            CommManager.reset();
            _cnp = new CnpOnline();
        }

        [Test]
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
            Assert.AreEqual("000", response.response);
        }

        [Test]
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
            Assert.AreEqual("000", response.response);
        }
    }
}
