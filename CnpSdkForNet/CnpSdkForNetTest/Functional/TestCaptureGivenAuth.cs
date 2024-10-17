using System.Collections.Generic;
using NUnit.Framework;
using System;
using System.Threading;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestCaptureGivenAuth
    {
        private CnpOnline _cnp;

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            _cnp = new CnpOnline();
        }

        [Test]
        public void SimpleCaptureGivenAuthWithCard()
        {
            var capturegivenauth = new captureGivenAuth
            {
                id = "1",
                amount = 106,
                orderId = "12344",
                authInformation = new authInformation
                {
                    authDate = new DateTime(2002, 10, 9),
                    authCode = "543216",
                    authAmount = 12345,
                },
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000000",
                    expDate = "1210",
                },
                processingType = processingType.accountFunding,
                originalNetworkTransactionId = "abc123",
                originalTransactionAmount = 123456789
            };

            var response = _cnp.CaptureGivenAuth(capturegivenauth);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleCaptureGivenAuthWithMpos()
        {
            var capturegivenauth = new captureGivenAuth
            {
                id = "1",
                amount = 500,
                orderId = "12344",
                authInformation = new authInformation
                {
                    authDate = new DateTime(2002, 10, 9),
                    authCode = "543216",
                    authAmount = 12345
                },
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

            var response = _cnp.CaptureGivenAuth(capturegivenauth);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleCaptureGivenAuthWithToken()
        {
            var capturegivenauth = new captureGivenAuth
            {
                id = "1",
                amount = 106,
                orderId = "12344",
                authInformation = new authInformation
                {
                    authDate = new DateTime(2002, 10, 9),
                    authCode = "543216",
                    authAmount = 12345,
                },

                orderSource = orderSourceType.ecommerce,
                token = new cardTokenType
                {
                    cnpToken = "123456789101112",
                    expDate = "1210",
                    cardValidationNum = "555",
                    type = methodOfPaymentTypeEnum.VI
                }
            };

            var response = _cnp.CaptureGivenAuth(capturegivenauth);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void ComplexCaptureGivenAuth()
        {
            var capturegivenauth = new captureGivenAuth
            {
                id = "1",
                amount = 106,
                orderId = "12344",
                authInformation = new authInformation
                {
                    authDate = new DateTime(2002, 10, 9),
                    authCode = "543216",
                    authAmount = 12345
                },
                billToAddress = new contact
                {
                    name = "Bob",
                    city = "lowell",
                    state = "MA",
                    email = "cnp.com"
                },

                processingInstructions = new processingInstructions
                {
                    bypassVelocityCheck = true
                },
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000000",
                    expDate = "1210"
                },
                processingType = processingType.initialInstallment,
                originalNetworkTransactionId = "abc123",
                originalTransactionAmount = 123456789
            };



            var response = _cnp.CaptureGivenAuth(capturegivenauth);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void AuthInfo()
        {
            var capturegivenauth = new captureGivenAuth
            {
                id = "1",
                amount = 106,
                orderId = "12344",
                authInformation = new authInformation
                {
                    authDate = new DateTime(2002, 10, 9),
                    authCode = "543216",
                    authAmount = 12345,
                    fraudResult = new fraudResult
                    {
                        avsResult = "12",
                        cardValidationResult = "123",
                        authenticationResult = "1",
                        advancedAVSResult = "123"
                    }
                },
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000000",
                    expDate = "1210"
                },
                processingType = processingType.initialRecurring,
                originalNetworkTransactionId = "abc123",
                originalTransactionAmount = 123456789,
            };

            var response = _cnp.CaptureGivenAuth(capturegivenauth);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleCaptureGivenAuthWithTokenAndSpecialCharacters()
        {
            var capturegivenauth = new captureGivenAuth
            {
                id = "1",
                amount = 106,
                orderId = "<'&\">",
                authInformation = new authInformation
                {
                    authDate = new DateTime(2002, 10, 9),
                    authCode = "543216",
                    authAmount = 12345
                },
                orderSource = orderSourceType.ecommerce,
                token = new cardTokenType
                {
                    cnpToken = "123456789101112",
                    expDate = "1210",
                    cardValidationNum = "555",
                    type = methodOfPaymentTypeEnum.VI
                }
            };

            var response = _cnp.CaptureGivenAuth(capturegivenauth);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleCaptureGivenAuthWithSecondaryAmount()
        {
            var capturegivenauth = new captureGivenAuth
            {
                id = "1",
                amount = 106,
                secondaryAmount = 50,
                orderId = "12344",
                authInformation = new authInformation
                {
                    authDate = new DateTime(2002, 10, 9),
                    authCode = "543216",
                    authAmount = 12345
                },

                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000000",
                    expDate = "1210",
                },

                processingType = processingType.accountFunding,
                originalNetworkTransactionId = "abc123",
                originalTransactionAmount = 123456789
            };

            var response = _cnp.CaptureGivenAuth(capturegivenauth);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void TestCaptureGivenAuthAsync()
        {
            var capturegivenauth = new captureGivenAuth
            {
                id = "1",
                amount = 106,
                orderId = "12344",
                authInformation = new authInformation
                {
                    authDate = new DateTime(2002, 10, 9),
                    authCode = "543216",
                    authAmount = 12345,
                },
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000000",
                    expDate = "1210",
                },
                processingType = processingType.accountFunding,
                originalNetworkTransactionId = "abc123",
                originalTransactionAmount = 123456789
            };
            CancellationToken cancellationToken = new CancellationToken(false);
            var response = _cnp.CaptureGivenAuthAsync(capturegivenauth, cancellationToken);
            Assert.AreEqual("000", response.Result.response);
        }
        
        [Test]
        public void SimpleCaptureGivenAuthWithCardWithLocation()
        {
            var capturegivenauth = new captureGivenAuth
            {
                id = "1",
                amount = 106,
                orderId = "12344",
                authInformation = new authInformation
                {
                    authDate = new DateTime(2002, 10, 9),
                    authCode = "543216",
                    authAmount = 12345,
                },
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000000",
                    expDate = "1210",
                },
                processingType = processingType.accountFunding,
                originalNetworkTransactionId = "abc123",
                originalTransactionAmount = 123456789,
                accountFundingTransactionData = new accountFundingTransactionData()
                {
                    receiverFirstName = "abcc",
                    receiverLastName = "cde",
                    receiverCountry = countryTypeEnum.US,
                    receiverState = stateTypeEnum.AL,
                    receiverAccountNumber = "4141000",

                },
                typeOfDigitalCurrency = "asv",
                conversionAffiliateId = "1",
            };

            var response = _cnp.CaptureGivenAuth(capturegivenauth);
            Assert.AreEqual("sandbox", response.location);
            Assert.AreEqual("Approved", response.message);
        }
        
        [Test]
        public void SimpleCaptureGivenAuthWithBusinessIndicator()
        {
            var capturegivenauth = new captureGivenAuth
            {
                id = "1",
                amount = 106,
                orderId = "12344",
                authInformation = new authInformation
                {
                    authDate = new DateTime(2002, 10, 9),
                    authCode = "543216",
                    authAmount = 12345,
                },
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000000",
                    expDate = "1210",
                },
                processingType = processingType.accountFunding,
                businessIndicator = businessIndicatorEnum.consumerBillPayment,
                originalNetworkTransactionId = "abc123",
                originalTransactionAmount = 123456789
            };

            var response = _cnp.CaptureGivenAuth(capturegivenauth);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleCaptureGivenAuthWithRetailerAddressAndAdditionalCOFdata()///12.24
        {
            
            var capturegivenauth = new captureGivenAuth
            {
                id = "1",
                amount = 106,
                orderId = "12344",
                crypto = false,
                authInformation = new authInformation
                {
                    authDate = new DateTime(2002, 10, 9),
                    authCode = "543216",
                    authAmount = 12345,
                },
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000000",
                    expDate = "1210",
                },
                retailerAddress = new contact
                {
                    name = "Mikasa Ackerman",
                    addressLine1 = "1st Main Street",
                    city = "Burlington",
                    state = "MA",
                    country = countryTypeEnum.USA,
                    email = "mikasa@cnp.com",
                    zip = "01867-4456",
                    sellerId = "s1234",
                    url = "www.google.com"
                },
                additionalCOFData = new additionalCOFData()
                {
                    totalPaymentCount = "35",
                    paymentType = paymentTypeEnum.Fixed_Amount,
                    uniqueId = "12345wereew233",
                    frequencyOfMIT = frequencyOfMITEnum.BiWeekly,
                    validationReference = "re3298rhriw4wrw",
                    sequenceIndicator = 2
                },
                processingType = processingType.accountFunding,
                originalNetworkTransactionId = "abc123",
                originalTransactionAmount = 123456789
            };

            var response = _cnp.CaptureGivenAuth(capturegivenauth);
            Assert.AreEqual("Approved", response.message);
        }
        [Test]
        public void SimpleCaptureGivenAuthWithModifiedLodgingInfo()///12.25
        {
            var capturegivenauth = new captureGivenAuth
            {
                id = "1",
                amount = 106,
                orderId = "12344",
                crypto = false,
                authInformation = new authInformation
                {
                    authDate = new DateTime(2002, 10, 9),
                    authCode = "543216",
                    authAmount = 12345,
                },
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000000",
                    expDate = "1210",
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
            var response = _cnp.CaptureGivenAuth(capturegivenauth);
            Assert.AreEqual("Approved", response.message);
        }
        [Test]
        public void SimpleCaptureGivenAuthWithPassengerTransportData()///12.26
        {
            var capturegivenauth = new captureGivenAuth
            {
                id = "1",
                amount = 106,
                orderId = "12344",
                crypto = false,
                authInformation = new authInformation
                {
                    authDate = new DateTime(2002, 10, 9),
                    authCode = "543216",
                    authAmount = 12345,
                },
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000000",
                    expDate = "1210",
                },
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
            var response = _cnp.CaptureGivenAuth(capturegivenauth);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleCaptureGivenAuthWithForeignRetailerIndicator()///12.31
        {
            var capturegivenauth = new captureGivenAuth
            {
                id = "1",
                amount = 1176,
                orderId = "123777774",
                crypto = false,
                authInformation = new authInformation
                {
                    authDate = new DateTime(2023, 4, 9),
                    authCode = "543216",
                    authAmount = 6532,
                },
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000000",
                    expDate = "1210",
                },
                lodgingInfo = new lodgingInfo
                {
                    bookingID = "BID23455",
                    passengerName = "Pratik Jaiswal",
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
                },
                foreignRetailerIndicator = foreignRetailerIndicatorEnum.F
            };
            var response = _cnp.CaptureGivenAuth(capturegivenauth);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleCaptureGivenAuthWithShipmentIDSubscription()//12.33
        {
            var capturegivenauth = new captureGivenAuth
            {
                id = "1",
                amount = 1176,
                orderId = "12377",
                crypto = false,
                authInformation = new authInformation
                {
                    authDate = new DateTime(2023, 4, 9),
                    authCode = "543216",
                    authAmount = 6532,
                },
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000000",
                    expDate = "1210",
                },
                enhancedData = new enhancedData
                {
                    customerReference = "000000008110801",
                    salesTax = 27,
                    deliveryType = enhancedDataDeliveryType.DIG,
                    taxExempt = false,
                    lineItems = new List<lineItemData>(),

                },
            };
            var mysubscription = new subscriptions();
            mysubscription.subscriptionId = "123";
            mysubscription.currentPeriod = 112;
            mysubscription.periodUnit = periodUnit.WEEK;
            mysubscription.numberOfPeriods = 131;
            mysubscription.regularItemPrice = 169;
            mysubscription.nextDeliveryDate = new DateTime(2023, 3, 2);

            var mylineItemData = new lineItemData();
            mylineItemData.itemSequenceNumber = 1;
            mylineItemData.itemDescription = "Ecomm";
            mylineItemData.productCode = "El11";
            mylineItemData.itemCategory = "Ele Appiances";
            mylineItemData.itemSubCategory = "home appliaces";
            mylineItemData.productId = "1111";
            mylineItemData.productName = "dryer";
            mylineItemData.shipmentId = "2593";
            mylineItemData.subscription.Add(mysubscription);
            capturegivenauth.enhancedData.lineItems.Add(mylineItemData);
            capturegivenauth.foreignRetailerIndicator = foreignRetailerIndicatorEnum.F;
            
            var response = _cnp.CaptureGivenAuth(capturegivenauth);
            Assert.AreEqual("Approved", response.message);
        }
    }
}
