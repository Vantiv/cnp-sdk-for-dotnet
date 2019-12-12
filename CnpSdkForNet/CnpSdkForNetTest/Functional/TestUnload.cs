using System.Collections.Generic;
using NUnit.Framework;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestUnload
    {
        private CnpOnline _cnp;

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            _cnp = new CnpOnline();
        }

        [Test]
        public void SimpleUnload()
        {
            var unload = new unload
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

            var response = _cnp.Unload(unload);
            Assert.AreEqual("000", response.response);
        }

    }
}
