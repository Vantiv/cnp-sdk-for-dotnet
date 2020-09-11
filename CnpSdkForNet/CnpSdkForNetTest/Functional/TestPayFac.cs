using System.Collections.Generic;
using NUnit.Framework;
using System.Threading;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestPayFac
    {
        private CnpOnline _cnp;

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            _cnp = new CnpOnline();
        }

        [Test]
        public void PayFacCredit()
        {
            var payFacCredit = new payFacCredit
            {
                // attributes.
                id = "1",
                reportGroup = "Default Report Group",
                // required child elements.
                amount = 1512l,
                fundingSubmerchantId = "value for fundingSubmerchantId",
                fundsTransferId = "value for fundsTransferId"
            };

            var response = _cnp.PayFacCredit(payFacCredit);
            Assert.AreEqual("500", response.response);
            Assert.AreEqual("sandbox", response.location);
        }

        [Test]
        public void TestPayFacCreditAsync()
        {
            var payFacCredit = new payFacCredit
            {
                // attributes.
                id = "1",
                reportGroup = "Default Report Group",
                // required child elements.
                amount = 1512l,
                fundingSubmerchantId = "value for fundingSubmerchantId",
                fundsTransferId = "value for fundsTransferId"
            };

            CancellationToken cancellationToken = new CancellationToken(false);
            var response = _cnp.PayFacCreditAsync(payFacCredit, cancellationToken);
            Assert.AreEqual("500", response.Result.response);
        }

        [Test]
        public void PayFacDebit()
        {
            var payFacDebit = new payFacDebit
            {
                // attributes.
                id = "1",
                reportGroup = "Planets",
                // required child elements.
                amount = 1512l,
                fundingSubmerchantId = "value for fundingSubmerchantId",
                fundsTransferId = "value for fundsTransferId"
            };

            var response = _cnp.PayFacDebit(payFacDebit);
            Assert.AreEqual("500", response.response);
            Assert.AreEqual("sandbox", response.location);
        }

        [Test]
        public void TestPayFacDebitAsync()
        {
            var payFacDebit = new payFacDebit
            {
                // attributes.
                id = "1",
                reportGroup = "Planets",
                // required child elements.
                amount = 1512l,
                fundingSubmerchantId = "value for fundingSubmerchantId",
                fundsTransferId = "value for fundsTransferId"
            };

            CancellationToken cancellationToken = new CancellationToken(false);
            var response = _cnp.PayFacDebitAsync(payFacDebit, cancellationToken);
            Assert.AreEqual("500", response.Result.response);
            Assert.AreEqual("sandbox", response.Result.location);
        }
    }
}
