using System.Collections.Generic;
using NUnit.Framework;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestDeactivate
    {
        private CnpOnline _cnp;

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            _cnp = new CnpOnline();
        }

        [Test]
        public void SimpleDeactivate()
        {
            var deactivate = new deactivate
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
                    expDate = "1215"
                }
            };

            var response = _cnp.Deactivate(deactivate);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("sandbox", response.location);
        }

    }
}
