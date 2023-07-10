using System.Collections.Generic;
using NUnit.Framework;
using System.Threading;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestCapture
    {
        private CnpOnline _cnp;

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            _cnp = new CnpOnline();
        }

        [Test]
        public void SimpleCapture()
        {
            var capture = new capture
            {
                id = "1",
                cnpTxnId = 123456000,
                amount = 106,
                payPalNotes = "Notes",
                pin = "1234"
            };

            var response = _cnp.Capture(capture);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleCaptureWithPartial()
        {
            var capture = new capture
            {
                id = "1",
                cnpTxnId = 123456000,
                amount = 106,
                partial = true,
                payPalNotes = "Notes"
            };


            var response = _cnp.Capture(capture);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void ComplexCapture()
        {
            var capture = new capture
            {
                id = "1",
                cnpTxnId = 123456000,
                amount = 106,
                payPalNotes = "Notes",
                pin = "1234",
                enhancedData = new enhancedData
                {
                    customerReference = "Cnp",
                    salesTax = 50,
                    deliveryType = enhancedDataDeliveryType.TBD
                },
                payPalOrderComplete = true,
                customBilling = new customBilling
                {
                    phone = "51312345678",
                    city = "Lowell",
                    url = "test.com",
                    descriptor = "Nothing",
                }
            };

            var response = _cnp.Capture(capture);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleCaptureWithSpecial()
        {
            var capture = new capture
            {
                id = "1",
                cnpTxnId = 123456000,
                amount = 106,
                payPalNotes = "<'&\">"
            };
            
            var response = _cnp.Capture(capture);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleCaptureWithLodgingInfo()
        {
            var capture = new capture
            {
                id = "1",
                cnpTxnId = 123456000,
                amount = 106,
                payPalNotes = "<'&\">",
                lodgingInfo = new lodgingInfo
                {
                    hotelFolioNumber = "12345",
                    checkInDate = new System.DateTime(2017, 1, 18),
                    customerServicePhone = "854213",
                    lodgingCharges = new List<lodgingCharge>(),

                }
            };
            capture.lodgingInfo.lodgingCharges.Add(new lodgingCharge() { name = lodgingExtraChargeEnum.GIFTSHOP });
            var response = _cnp.Capture(capture);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void TestCaptureAsync()
        {
            var capture = new capture
            {
                id = "1",
                cnpTxnId = 123456000,
                amount = 106,
                payPalNotes = "<'&\">",
                lodgingInfo = new lodgingInfo
                {
                    hotelFolioNumber = "12345",
                    checkInDate = new System.DateTime(2017, 1, 18),
                    customerServicePhone = "854213",
                    lodgingCharges = new List<lodgingCharge>(),

                }
            };
            capture.lodgingInfo.lodgingCharges.Add(new lodgingCharge() { name = lodgingExtraChargeEnum.GIFTSHOP });
            CancellationToken cancellationToken = new CancellationToken(false);
            
            var response = _cnp.CaptureAsync(capture, cancellationToken);
            Assert.AreEqual("000", response.Result.response);
        }
        
        [Test]
        public void SimpleCaptureWithLocation()
        {
            var capture = new capture
            {
                id = "1",
                cnpTxnId = 123456000,
                amount = 106,
                payPalNotes = "Notes",
                pin = "1234"
            };

            var response = _cnp.Capture(capture);
            Assert.AreEqual("sandbox", response.location);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleCaptureWithOptionalOrderID()
        {
            var capture = new capture
            {
                id = "1",
                cnpTxnId = 123456000,
                orderId = "orderidmorethan25charactersareacceptednow",
                amount = 106,
                payPalNotes = "Notes",
                pin = "1234"
            };

            var response = _cnp.Capture(capture);
            Assert.AreEqual("Approved", response.message);
        }
        [Test]
        public void SimpleCaptureWithModifiedLodgingInfo()///12.25
        {
            var capture = new capture
            {
                id = "1",
                cnpTxnId = 123456000,
                orderId = "orderidmorethan25charactersareacceptednow",
                amount = 106,
                payPalNotes = "Notes",
                pin = "1234",
                lodgingInfo = new lodgingInfo
                {
                    bookingID = "BID12345",
                    passengerName = "Pia Jaiswal",
                    propertyAddress = new propertyAddress
                    {
                        name = "Godrej",
                        city = "Pune",
                        region = "WES",
                        country = countryTypeEnum.IN
                    },
                    travelPackageIndicator = travelPackageIndicatorEnum.AirlineReservation,
                    smokingPreference = "N",
                    numberOfRooms = 1,
                    tollFreePhoneNumber = "1234567890"
                }
            };
            var response = _cnp.Capture(capture);
            Assert.AreEqual("Approved", response.message);
        }
        [Test]
        public void SimpleCaptureWithPassengerTransportData()///12.26
        {
            var capture = new capture
            {
                id = "1",
                cnpTxnId = 123456000,
                orderId = "orderidmorethan25charactersareacceptednow",
                amount = 106,
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
            var response = _cnp.Capture(capture);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleCaptureWithForeignRetailerIndicator()///12.31
        {
            var capture = new capture
            {
                id = "1",
                cnpTxnId = 123456000,
                orderId = "defaultOrderId",
                amount = 106,
                lodgingInfo = new lodgingInfo
                {
                    bookingID = "BID1234555",
                    passengerName = "Rohan Sharma",
                    propertyAddress = new propertyAddress
                    {
                        name = "Xpress",
                        city = "Pune",
                        region = "WES",
                        country = countryTypeEnum.IN
                    },
                    travelPackageIndicator = travelPackageIndicatorEnum.AirlineReservation,
                    smokingPreference = "N",
                    numberOfRooms = 1,
                    tollFreePhoneNumber = "1234567890"
                },
                foreignRetailerIndicator= foreignRetailerIndicatorEnum.F

            };
            var response = _cnp.Capture(capture);
            Assert.AreEqual("Approved", response.message);
        }
    }
}
