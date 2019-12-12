using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    public class TestSubscriptionTxns
    {
        private CnpOnline _cnp;

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            _cnp = new CnpOnline();
        }

        [Test]
        public void TestUpdateSubscription_Basic()
        {
            var update = new updateSubscription
            {
                billingDate = new DateTime(2002, 10, 9),
                billToAddress = new contact
                {
                    name = "Greg Dake",
                    city = "Lowell",
                    state = "MA",
                    email = "sdksupport@vantiv.com"
                },
                card = new cardType
                {
                    number = "4100000000000001",
                    expDate = "1215",
                    type = methodOfPaymentTypeEnum.VI
                },
                planCode = "abcdefg",
                subscriptionId = 12345
            };
            
            var response = _cnp.UpdateSubscription(update);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void TestUpdateSubscription_WithToken()
        {
            var update = new updateSubscription
            {
                billingDate = new DateTime(2002, 10, 9),
                billToAddress = new contact
                {
                    name = "Greg Dake",
                    city = "Lowell",
                    state = "MA",
                    email = "sdksupport@vantiv.com"
                },
                token = new cardTokenType
                {
                    cnpToken = "987654321098765432",
                    expDate = "0750",
                    cardValidationNum = "798",
                    type = methodOfPaymentTypeEnum.VI,
                    checkoutId = "012345678901234567"
                },
                planCode = "abcdefg",
                subscriptionId = 12345
            };

            var response = _cnp.UpdateSubscription(update);
            Assert.AreEqual("Approved", response.message);
        }
    }
}
