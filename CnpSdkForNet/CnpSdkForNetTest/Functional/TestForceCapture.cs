using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Threading;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestForceCapture
    {
        private CnpOnline _cnp;

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            _cnp = new CnpOnline();
        }

        [Test]
        public void SimpleForceCaptureWithCard()
        {
            var forcecapture = new forceCapture
            {
                id = "1",
                amount = 106,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                processingType = processingType.accountFunding,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1210"
                },
                merchantCategoryCode = "1111"
            };
            

            var response = _cnp.ForceCapture(forcecapture);
            Assert.AreEqual("Approved", response.message);
        }
        
        [Test]
        public void SimpleForceCaptureWithCardWithLocation()
        {
            var forcecapture = new forceCapture
            {
                id = "1",
                amount = 106,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                processingType = processingType.accountFunding,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1210"
                },
                merchantCategoryCode = "1111"
            };
            

            var response = _cnp.ForceCapture(forcecapture);
            Assert.AreEqual("sandbox", response.location);
            Assert.AreEqual("Approved", response.message);
        }
        
        [Test]
        public void SimpleForceCaptureWithprocessingType()
        {
            var forcecapture = new forceCapture
            {
                id = "1",
                amount = 106,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                processingType = processingType.initialCOF,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1210"
                }
            };

            var response = _cnp.ForceCapture(forcecapture);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleForceCaptureWithMpos()
        {
            var forcecapture = new forceCapture
            {
                id = "1",
                amount = 322,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                mpos = new mposType
                {
                    ksn = "77853211300008E00016",
                    encryptedTrack = "CASE1E185EADD6AFE78C9A214B21313DCD836FDD555FBE3A6C48D141FE80AB9172B963265AFF72111895FE415DEDA162CE8CB7AC4D91EDB611A2AB756AA9CB1A000000000000000000000000000000005A7AAF5E8885A9DB88ECD2430C497003F2646619A2382FFF205767492306AC804E8E64E8EA6981DD",
                    formatId = "30",
                    track1Status = 0,
                    track2Status = 0
                }
            };

            var response = _cnp.ForceCapture(forcecapture);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleForceCaptureWithToken()
        {
            var forcecapture = new forceCapture
            {
                id = "1",
                amount = 106,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                token = new cardTokenType
                {
                    cnpToken = "123456789101112",
                    expDate = "1210",
                    cardValidationNum = "555",
                    type = methodOfPaymentTypeEnum.VI
                }
            };

            var response = _cnp.ForceCapture(forcecapture);
            Assert.AreEqual("Approved", response.message); ;
        }

        [Test]
        public void simpleForceCaptureWithSecondaryAmount()
        {
            forceCapture forcecapture = new forceCapture();
            forcecapture.id = "1";
            forcecapture.amount = 106;
            forcecapture.secondaryAmount = 50;
            forcecapture.orderId = "12344";
            forcecapture.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000000";
            card.expDate = "1210";
            forcecapture.card = card;
            forceCaptureResponse response = _cnp.ForceCapture(forcecapture);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void TestForceCaptureWithCardAsync()
        {
            var forcecapture = new forceCapture
            {
                id = "1",
                amount = 106,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                processingType = processingType.accountFunding,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1210"
                }
            };

            CancellationToken cancellationToken = new CancellationToken(false);
            var response = _cnp.ForceCaptureAsync(forcecapture, cancellationToken);
            Assert.AreEqual("000", response.Result.response);
        }
        [Test]
        public void SimpleForceCaptureBusinessIndicator()
        {
            var forcecapture = new forceCapture
            {
                id = "1",
                amount = 106,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                processingType = processingType.accountFunding,
                businessIndicator = businessIndicatorEnum.consumerBillPayment,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1210"
                },
                merchantCategoryCode = "1111"
            };
            

            var response = _cnp.ForceCapture(forcecapture);
            Assert.AreEqual("Approved", response.message);
        }
        [Test]
        public void SimpleForceCaptureWithModifiedLodgingInfo()///12.25
        {
            var forcecapture = new forceCapture
            {
                id = "1",
                amount = 106,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                processingType = processingType.accountFunding,
                businessIndicator = businessIndicatorEnum.consumerBillPayment,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1210"
                },
                merchantCategoryCode = "1111",
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
            var response = _cnp.ForceCapture(forcecapture);
            Assert.AreEqual("Approved", response.message);
        }
        [Test]
        public void SimpleForceCaptureWithPassengerTransportData()///12.26
        {
            var forcecapture = new forceCapture
            {
                id = "1",
                amount = 106,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                processingType = processingType.accountFunding,
                businessIndicator = businessIndicatorEnum.consumerBillPayment,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1210"
                },
                merchantCategoryCode = "1111",
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
            var response = _cnp.ForceCapture(forcecapture);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleForceCaptureWithForeignRetailerIndicator()///12.31
        {
            var forcecapture = new forceCapture
            {
                id = "1",
                amount = 177,
                orderId = "1234455",
                orderSource = orderSourceType.ecommerce,
                processingType = processingType.accountFunding,
                businessIndicator = businessIndicatorEnum.consumerBillPayment,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1210"
                },
                merchantCategoryCode = "2222",
                lodgingInfo = new lodgingInfo
                {
                    bookingID = "BID1234556",
                    passengerName = "Sumit Patil",
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
                    tollFreePhoneNumber = "1234567777"
                },
                foreignRetailerIndicator = foreignRetailerIndicatorEnum.F
            };
            var response = _cnp.ForceCapture(forcecapture);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleForceCaptureWithShipmentIdAndSubscription()///12.33
        {
            var forcecapture = new forceCapture
            {
                id = "1",
                amount = 177,
                orderId = "1234455",
                orderSource = orderSourceType.ecommerce,
                processingType = processingType.accountFunding,
                businessIndicator = businessIndicatorEnum.consumerBillPayment,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1210"
                },
                enhancedData = new enhancedData
                {
                    customerReference = "000000008110801",
                    salesTax = 23,
                    deliveryType = enhancedDataDeliveryType.DIG,
                    taxExempt = false,
                    detailTaxes = new List<detailTax>(),
                    lineItems = new List<lineItemData>(),

                },
                foreignRetailerIndicator = foreignRetailerIndicatorEnum.F,
                accountFundingTransactionData = new accountFundingTransactionData()
                {
                    receiverFirstName = "abcc",
                    receiverLastName = "cde",
                    receiverCountry = countryTypeEnum.US,
                    receiverState = stateTypeEnum.AL,
                    receiverAccountNumber = "4141000",

                }
            };
            var mysubscription = new subscriptions();
            mysubscription.subscriptionId = "123";
            mysubscription.currentPeriod = 112;
            mysubscription.periodUnit = periodUnit.MONTH;
            mysubscription.numberOfPeriods = 123;
            mysubscription.regularItemPrice = 69;
            mysubscription.nextDeliveryDate = new DateTime(2017, 1, 1);

            var mylineItemData = new lineItemData();
            mylineItemData.itemSequenceNumber = 1;
            mylineItemData.itemDescription = "Electronics";
            mylineItemData.productCode = "El01";
            mylineItemData.itemCategory = "Ele Appiances";
            mylineItemData.itemSubCategory = "home appliaces";
            mylineItemData.productId = "1001";
            mylineItemData.productName = "dryer";
            mylineItemData.shipmentId = "2543";
            mylineItemData.subscription.Add(mysubscription);
            forcecapture.enhancedData.lineItems.Add(mylineItemData);

            var response = _cnp.ForceCapture(forcecapture);
            Assert.AreEqual("Approved", response.message);
        }
    }
}
