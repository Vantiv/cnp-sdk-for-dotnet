using System.Collections.Generic;
using NUnit.Framework;
using System.Threading;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestReserve
    {
        private CnpOnline _cnp;

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            _cnp = new CnpOnline();
        }

        [Test]
        public void ReserveCredit()
        {
            var reserveCredit = new reserveCredit
            {
                // attributes.
                id = "1",
                reportGroup = "Default Report Group",
                // required child elements.
                amount = 1512l,
                fundingSubmerchantId = "value for fundingSubmerchantId",
                fundsTransferId = "value for fundsTransferId"
            };

            var response = _cnp.ReserveCredit(reserveCredit);
            Assert.AreEqual("000", response.response);
        }

        [Test]
        public void TestReserveCreditAsync()
        {
            var reserveCredit = new reserveCredit
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
            var response = _cnp.ReserveCreditAsync(reserveCredit, cancellationToken);
            Assert.AreEqual("000", response.Result.response);
        }
        
        [Test]
        public void ReserveCreditFundingCustomerId()
        {
            var reserveCredit = new reserveCredit
            {
                // attributes.
                id = "1",
                reportGroup = "Default Report Group",
                // required child elements.
                amount = 1512l,
                fundingCustomerId = "value for fundingCustomerId",
                fundsTransferId = "value for fundsTransferId"
            };

            var response = _cnp.ReserveCredit(reserveCredit);
            Assert.AreEqual("000", response.response);
        }

        [Test]
        public void ReserveDebit()
        {
            var reserveDebit = new reserveDebit
            {
                // attributes.
                id = "1",
                reportGroup = "Planets",
                // required child elements.
                amount = 1512l,
                fundingSubmerchantId = "value for fundingSubmerchantId",
                fundsTransferId = "value for fundsTransferId"
            };

            var response = _cnp.ReserveDebit(reserveDebit);
            Assert.AreEqual("000", response.response);
        }

        [Test]
        public void TestReserveDebitAsync()
        {
            var reserveDebit = new reserveDebit
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
            var response = _cnp.ReserveDebitAsync(reserveDebit, cancellationToken);
            Assert.AreEqual("000", response.Result.response);
        }
        
        [Test]
        public void ReserveDebitFundingCustomerId()
        {
            var reserveDebit = new reserveDebit
            {
                // attributes.
                id = "1",
                reportGroup = "Default Report Group",
                // required child elements.
                amount = 1512l,
                fundingCustomerId = "value for fundingCustomerId",
                fundsTransferId = "value for fundsTransferId"
            };

            var response = _cnp.ReserveDebit(reserveDebit);
            Assert.AreEqual("000", response.response);
        }
        
        [Test]
        public void ReserveDebitXMLCharacters()
        {
            var reserveDebit = new reserveDebit
            {
                // attributes.
                id = "1",
                reportGroup = "Default Report Group",
                // required child elements.
                amount = 1512l,
                fundingCustomerId = "value <for> fundingCustomerId",
                fundsTransferId = "value for fundsTransferId"
            };

            var response = _cnp.ReserveDebit(reserveDebit);
            Assert.AreEqual("000", response.response);
        }
    }
}
