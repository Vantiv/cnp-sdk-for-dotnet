using System.Collections.Generic;
using NUnit.Framework;
using System.Threading;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestDeposiTransactionReversal
    {
        private CnpOnline _cnp;

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            _cnp = new CnpOnline();
        }

        [Test]
        public void SimpleTransactionReversal()
        {
            var reversal = new depositTransactionReversal
            {
                id = "1",
                reportGroup = "Planets",
                cnpTxnId = 12345678000L,
                amount = 106,
            };

            var response = _cnp.DepositTransactionReversal(reversal);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void TestTransactionReversalHandleSpecialCharacters()
        {
            var reversal = new depositTransactionReversal()
            {
                id = "1",
                reportGroup = "Planets",
                cnpTxnId = 12345678000L,
                amount = 106,
                customerId = "<'&\">"
            };


            var response = _cnp.DepositTransactionReversal(reversal);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void TestTransactionReversalAsync()
        {
            var reversal = new depositTransactionReversal
            {
                id = "1",
                reportGroup = "Planets",
                cnpTxnId = 12345678000L,
                amount = 106,
                customerId = "<'&\">"
            };

            CancellationToken cancellationToken = new CancellationToken(false);
            var response = _cnp.DepositTransactionReversalAsync(reversal, cancellationToken);
            Assert.AreEqual("000", response.Result.response);
        }
        
        [Test]
        public void TestSimpleTransactionReversalWithLocation()
        {
            var reversal = new depositTransactionReversal
            {
                id = "1",
                reportGroup = "Planets",
                cnpTxnId = 12345678000L,
                amount = 106,
            };

            var response = _cnp.DepositTransactionReversal(reversal);
            Assert.AreEqual("sandbox", response.location);
            Assert.AreEqual("Approved", response.message);
        }
        
        [Test]
        public void TestTransactionReversalWithRecycling()
        {
            var reversal = new depositTransactionReversal
            {
                id = "1",
                reportGroup = "Planets",
                cnpTxnId = 12345678000L,
                amount = 106,
            };

            var response = _cnp.DepositTransactionReversal(reversal);
            Assert.AreEqual("sandbox", response.location);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual(12345678000L, response.recyclingResponse.creditCnpTxnId);
        }

        [Test]
        public void TestTransactionReversalWithPassengerTransportData()
        {
            var reversal = new depositTransactionReversal
            {
                id = "1",
                reportGroup = "Planets",
                cnpTxnId = 12345678000L,
                passengerTransportData = new passengerTransportData
                {
                    passengerName = "Pia Jaiswal",
                    ticketNumber = "TR0001",
                    issuingCarrier = "IC",
                    carrierName = "Indigo",
                    restrictedTicketIndicator = "TI2022",
                    numberOfAdults = 1,
                    numberOfChildren = 1,
                    customerCode = "C2011583",
                    arrivalDate = new System.DateTime(2022, 12, 31),
                    issueDate = new System.DateTime(2022, 12, 25),
                    travelAgencyCode = "TAC12345",
                    travelAgencyName = "Yatra",
                    computerizedReservationSystem = computerizedReservationSystemEnum.STRT,
                    creditReasonIndicator = creditReasonIndicatorEnum.A,
                    ticketChangeIndicator = ticketChangeIndicatorEnum.C,
                    ticketIssuerAddress = "Hinjewadi",
                    exchangeTicketNumber = "ETN12345",
                    exchangeAmount = 12300,
                    exchangeFeeAmount = 11000,
                    tripLegData = new tripLegData
                    {
                        tripLegNumber = 12,
                        departureCode = "DC",
                        carrierCode = "CC",
                        serviceClass = serviceClassEnum.First,
                        stopOverCode = "N",
                        destinationCode = "DC111",
                        fareBasisCode = "FBC12345",
                        departureDate = new System.DateTime(2023, 1, 31),
                        originCity = "Pune",
                        travelNumber = "TN111",
                        departureTime = "13:05",
                        arrivalTime = "16:10",
                        remarks = "NA"
                    }
                }
            };

            var response = _cnp.DepositTransactionReversal(reversal);
            Assert.AreEqual("sandbox", response.location);
            Assert.AreEqual("Approved", response.message);
        }
    }
}
