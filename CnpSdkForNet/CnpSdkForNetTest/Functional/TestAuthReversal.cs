﻿using System.Collections.Generic;
using NUnit.Framework;
using System.Threading;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestAuthReversal
    {
        private CnpOnline _cnp;

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            _cnp = new CnpOnline();
        }

        [Test]
        public void SimpleAuthReversal()
        {
            var reversal = new authReversal
            {
                id = "1",
                reportGroup = "Planets",
                cnpTxnId = 12345678000L,
                amount = 106,
                payPalNotes = "Notes"
            };

            var response = _cnp.AuthReversal(reversal);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void TestAuthReversalHandleSpecialCharacters()
        {
            var reversal = new authReversal
            {
                id = "1",
                reportGroup = "Planets",
                cnpTxnId = 12345678000L,
                amount = 106,
                payPalNotes = "<'&\">"
            };


            var response = _cnp.AuthReversal(reversal);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void TestAuthReversalAsync()
        {
            var reversal = new authReversal
            {
                id = "1",
                reportGroup = "Planets",
                cnpTxnId = 12345678000L,
                amount = 106,
                payPalNotes = "<'&\">"
            };

            CancellationToken cancellationToken = new CancellationToken(false);
            var response = _cnp.AuthReversalAsync(reversal, cancellationToken);
            Assert.AreEqual("000", response.Result.response);
        }
        
        [Test]
        public void SimpleAuthReversalWithLocation()
        {
            var reversal = new authReversal
            {
                id = "1",
                reportGroup = "Planets",
                cnpTxnId = 12345678000L,
                amount = 106,
                payPalNotes = "Notes"
            };

            var response = _cnp.AuthReversal(reversal);
            Assert.AreEqual("sandbox", response.location);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void TestAuthReversalAsync_newMerchantId()
        {
            _cnp.SetMerchantId("1234");
            var reversal = new authReversal
            {
                id = "1",
                reportGroup = "Planets",
                cnpTxnId = 12345678000L,
                amount = 106,
                payPalNotes = "<'&\">"
            };

            CancellationToken cancellationToken = new CancellationToken(false);
            var response = _cnp.AuthReversalAsync(reversal, cancellationToken);
            Assert.AreEqual("000", response.Result.response);
        }
        [Test]
        public void SimpleAuthReversalWithAdditionalCOFdata()///12.26
        {

            var reversal = new authReversal
            {
                id = "1",
                reportGroup = "Planets",
                cnpTxnId = 12345678000L,
                amount = 106,
                
                additionalCOFData = new additionalCOFData()
                {
                    totalPaymentCount = "35",
                    paymentType = paymentTypeEnum.Fixed_Amount,
                    uniqueId = "12345wereew233",
                    frequencyOfMIT = frequencyOfMITEnum.BiWeekly,
                    validationReference = "re3298rhriw4wrw",
                    sequenceIndicator = 2
                }
            };

            var response = _cnp.AuthReversal(reversal);
            Assert.AreEqual("Approved", response.message);
        }
    }
}
