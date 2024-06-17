using System.Collections.Generic;
using NUnit.Framework;
using System.Threading;
using System;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestCredit
    {
        private CnpOnline _cnp;

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            _cnp = new CnpOnline();
        }

        [Test]
        public void SimpleCreditWithCard()
        {
            var creditObj = new credit
            {
                id = "1",
                reportGroup = "planets",
                amount = 106,
                orderId = "2111",
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1210"
                }
            };

            var response = _cnp.Credit(creditObj);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleCreditWithMpos()
        {
            var creditObj = new credit
            {
                id = "1",
                reportGroup = "planets",
                amount = 106,
                orderId = "2111",
                orderSource = orderSourceType.ecommerce,
                mpos = new mposType
                {
                    ksn = "77853211300008E00016",
                    encryptedTrack = "CASE1E185EADD6AFE78C9A214B21313DCD836FDD555FBE3A6C48D141FE80AB9172B963265AFF72111895FE415DEDA162CE8CB7AC4D91EDB611A2AB756AA9CB1A000000000000000000000000000000005A7AAF5E8885A9DB88ECD2430C497003F2646619A2382FFF205767492306AC804E8E64E8EA6981DD",
                    formatId = "30",
                    track1Status = 0,
                    track2Status = 0,
                }
            };

            creditResponse response = _cnp.Credit(creditObj);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleCreditWithPaypal()
        {
            var creditObj = new credit
            {
                id = "1",
                reportGroup = "planets",
                amount = 106,
                orderId = "123456",
                orderSource = orderSourceType.ecommerce,
                paypal = new payPal { payerId = "1234" }
            };

            var response = _cnp.Credit(creditObj);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void PaypalNotes()
        {
            var creditObj = new credit
            {
                id = "1",
                reportGroup = "planets",
                amount = 106,
                orderId = "123456",
                payPalNotes = "Hello",
                orderSource = orderSourceType.ecommerce,

                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1210",
                }
            };

            var response = _cnp.Credit(creditObj);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void ProcessingInstructionAndAmexData()
        {
            var creditObj = new credit
            {
                id = "1",
                reportGroup = "planets",
                amount = 2000,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                processingInstructions = new processingInstructions { bypassVelocityCheck = true },
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000000",
                    expDate = "1210"
                }
            };

            var response = _cnp.Credit(creditObj);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleCreditWithCardAndSpecialCharacters()
        {
            var creditObj = new credit
            {
                id = "1",
                reportGroup = "planets",
                amount = 106,
                orderId = "<&'>",
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000<>0000001",
                    expDate = "1210"
                }
            };

            var response = _cnp.Credit(creditObj);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleCreditWithPin()
        {
            var creditObj = new credit
            {
                id = "1",
                reportGroup = "planets",
                cnpTxnId = 123456000,
                pin = "1234",
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1210"
                }
            };

            var response = _cnp.Credit(creditObj);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void TestCreditWithCardAsync()
        {
            var creditObj = new credit
            {
                id = "1",
                reportGroup = "planets",
                amount = 106,
                orderId = "2111",
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1210"
                }
            };
            
            CancellationToken cancellationToken = new CancellationToken(false);
            var response = _cnp.CreditAsync(creditObj, cancellationToken);
            Assert.AreEqual("000", response.Result.response);
        }
        
        [Test]
        public void SimpleCreditWithCardWithLocation()
        {
            var creditObj = new credit
            {
                id = "1",
                reportGroup = "planets",
                amount = 106,
                orderId = "2111",
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1210"
                }
            };

            var response = _cnp.Credit(creditObj);
            Assert.AreEqual("sandbox", response.location);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void TestCreditWithCardAsync_newMerchantId()
        {
            _cnp.SetMerchantId("1234");
            var creditObj = new credit
            {
                id = "1",
                reportGroup = "planets",
                amount = 106,
                orderId = "2111",
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1210"
                }
            };

            CancellationToken cancellationToken = new CancellationToken(false);
            var response = _cnp.CreditAsync(creditObj, cancellationToken);
            Assert.AreEqual("000", response.Result.response);
        }
        [Test]
        public void SimpleCreditWithBusinessIndicator()
        {
            var creditObj = new credit
            {
                id = "1",
                reportGroup = "planets",
                amount = 106,
                orderId = "2111",
                orderSource = orderSourceType.ecommerce,
                businessIndicator = businessIndicatorEnum.consumerBillPayment,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1210"
                }
            };

            var response = _cnp.Credit(creditObj);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleCreditWithPinAndOptionalOrderID()
        {
            var creditObj = new credit
            {
                id = "1",
                reportGroup = "planets",
                cnpTxnId = 123456000,
                orderId = "2111",
                pin = "1234",
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1210"
                }
            };

            var response = _cnp.Credit(creditObj);
            Assert.AreEqual("Approved", response.message);
        }
        [Test]
        public void SimpleCreditWithModifiedLodgingInfo()///12.25
        {
            var creditObj = new credit
            {
                id = "1",
                reportGroup = "planets",
                cnpTxnId = 123456000,
                orderId = "2111",
                pin = "1234",
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1210"
                },
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
            var response = _cnp.Credit(creditObj);
            Assert.AreEqual("Approved", response.message);
        }
        [Test]
        public void SimpleCreditWithAdditionalCOFdata()///12.26
        {

            var creditObj = new credit
            {
                id = "1",
                reportGroup = "planets",
                cnpTxnId = 123456000,
                orderId = "2111",
                pin = "1234",
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1210"
                },
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

            var response = _cnp.Credit(creditObj);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleCreditWithPassengerTransportData() //12.26
        {
            var creditObj = new credit
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "414100000000000000",
                    expDate = "1210"
                },
                customBilling = new customBilling { phone = "1112223333" },
                passengerTransportData= new passengerTransportData
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
                        departureDate = new System.DateTime(2023, 1,31),
                        originCity = "Pune",
                        travelNumber = "TN111",
                        departureTime = "13:05",
                        arrivalTime = "16:10",
                        remarks = "NA"
                    }
                },
                accountFundingTransactionData = new accountFundingTransactionData()
                {
                    receiverFirstName = "abcc",
                    receiverLastName = "cde",
                    receiverCountry = countryTypeEnum.US,
                    receiverState = stateTypeEnum.AL,
                    receiverAccountNumber = "4141000",

                }
            };
            var response = _cnp.Credit(creditObj);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleCreditWithShipmentIdAndSubscription() //12.33
        {
            var creditObj = new credit
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "414100000000000000",
                    expDate = "1210"
                },
                customBilling = new customBilling { phone = "1112223333" },
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
                },
                enhancedData = new enhancedData
                {
                    customerReference = "000000008110801",
                    salesTax = 23,
                    deliveryType = enhancedDataDeliveryType.DIG,
                    taxExempt = false,
                    detailTaxes = new List<detailTax>(),
                    lineItems = new List<lineItemData>(),

                }
            };
            var mysubscription = new subscriptions();
            mysubscription.subscriptionId = "123";
            mysubscription.currentPeriod = 114;
            mysubscription.periodUnit = periodUnit.YEAR;
            mysubscription.numberOfPeriods = 123;
            mysubscription.regularItemPrice = 69;
            mysubscription.nextDeliveryDate = new DateTime(2017, 1, 1);

            var mylineItemData = new lineItemData();
            mylineItemData.itemSequenceNumber = 1;
            mylineItemData.itemDescription = "Electronics";
            mylineItemData.productCode = "El03";
            mylineItemData.itemCategory = "E Appiances";
            mylineItemData.itemSubCategory = "appliaces";
            mylineItemData.productId = "1023";
            mylineItemData.productName = "dyer";
            mylineItemData.shipmentId = "2124";
            mylineItemData.subscription.Add(mysubscription);
            creditObj.enhancedData.lineItems.Add(mylineItemData);

            var response = _cnp.Credit(creditObj);
            Assert.AreEqual("Approved", response.message);
        }
    }
}
