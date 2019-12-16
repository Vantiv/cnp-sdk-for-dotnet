using System.Collections.Generic;
using NUnit.Framework;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestLoad
    {
        private CnpOnline _cnp;

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            _cnp = new CnpOnline();
        }

        [Test]
        public void SimpleLoad()
        {
            var load = new load
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

            var response = _cnp.Load(load);
            Assert.AreEqual("000", response.response);
        }

    }
}
