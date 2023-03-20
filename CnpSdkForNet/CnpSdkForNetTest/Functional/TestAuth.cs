using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Threading;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestAuth
    {
        private CnpOnline _cnp;
        private Dictionary<string, string> _config;

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            _config = new ConfigManager().getConfig();
            _cnp = new CnpOnline(_config);
        }

        [Test]
        public void SimpleAuthWithCard()
        {
            var authorization = new authorization
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
                customBilling = new customBilling { phone = "1112223333" }
            };
            var response = _cnp.Authorize(authorization);

            DateTime checkDate = new DateTime(0001, 1, 1, 00, 00, 00);

            Assert.AreEqual("000", response.response);
            Assert.AreEqual(checkDate, response.postDate);
        }

        [Test]
        public void SimpleAuthWithMasterCard()
        {
            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "2",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.MC,
                    number = "540000000000000000",
                    expDate = "1210"
                },
                customBilling = new customBilling { phone = "1112223333" }
            };

            var response = _cnp.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.Null(response.networkTransactionId);
        }

        [Test]
        public void SimpleAuthWithCard_CardSuffixResponse()
        {
            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "3",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                processingType = processingType.accountFunding,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    //number = "410070000000000000",
                    number = "400000500000000000",
                    expDate = "1210"
                },
                customBilling = new customBilling { phone = "1112223333" }
            };

            var response = _cnp.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            // SANDBOX BRB
            //Assert.AreEqual("123456", response.cardSuffix);
        }

        [Test]
        public void SimpleAuthWithCard_networkTxnId()
        {
            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "4",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                processingType = processingType.accountFunding,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "410080000000000000",
                    expDate = "1210"
                },
                customBilling = new customBilling { phone = "1112223333" }
            };

            var response = _cnp.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            // SANDBOX BRB
            //Assert.AreEqual("63225578415568556365452427825", response.networkTransactionId);
        }

        [Test]
        public void SimpleAuthWithCard_origTxnIdAndAmount()
        {
            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "5",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                originalNetworkTransactionId = "123456789012345678901234567890",
                originalTransactionAmount = 2500,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "410000000000000000",
                    expDate = "1210"
                },
                customBilling = new customBilling { phone = "1112223333" }
            };

            var response = _cnp.Authorize(authorization);
            Assert.AreEqual("000", response.response);
        }

        [Test]
        public void SimpleAuthWithMpos()
        {
            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                amount = 200,
                orderSource = orderSourceType.ecommerce,
                mpos = new mposType
                {
                    ksn = "77853211300008E00016",
                    encryptedTrack =
                    "CASE1E185EADD6AFE78C9A214B21313DCD836FDD555FBE3A6C48D141FE80AB9172B963265AFF72111895FE415DEDA162CE8CB7AC4D91EDB611A2AB756AA9CB1A000000000000000000000000000000005A7AAF5E8885A9DB88ECD2430C497003F2646619A2382FFF205767492306AC804E8E64E8EA6981DD",
                    formatId = "30",
                    track1Status = 0,
                    track2Status = 0
                }
            };

            var response = _cnp.Authorize(authorization);
            Assert.AreEqual("000", response.response);
        }

        [Test]
        public void AuthWithAmpersand()
        {
            var authorization = new authorization
            {
                id = "1",
                orderId = "7",
                amount = 10010,
                orderSource = orderSourceType.ecommerce,
                billToAddress = new contact
                {
                    name = "John & Jane Smith",
                    addressLine1 = "1 Main St.",
                    city = "Burlington",
                    state = "MA",
                    zip = "01803-3747",
                    country = countryTypeEnum.US
                },
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4457010000000009",
                    expDate = "0112",
                    cardValidationNum = "349"
                }
            };

            var response = _cnp.Authorize(authorization);
            Assert.AreEqual("000", response.response);
        }

        [Test]
        public void SimpleAuthWithPaypal()
        {
            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "8",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                paypal = new payPal
                {
                    payerId = "1234",
                    token = "1234",
                    transactionId = "123456"
                },
                customBilling = new customBilling { phone = "1112223333" }
            };

            var response = _cnp.Authorize(authorization);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleAuthWithAndroidPay()
        {
            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "9",
                amount = 106,
                orderSource = orderSourceType.androidpay,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "414100000000000000",
                    expDate = "1210"
                },
                customBilling = new customBilling { phone = "1112223333" }
            };

            var response = _cnp.Authorize(authorization);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual("01", response.androidpayResponse.expMonth);
            Assert.AreEqual("2050", response.androidpayResponse.expYear);
            Assert.IsNotEmpty(response.androidpayResponse.cryptogram);
        }

        [Test]
        public void SimpleAuthWithApplepayAndSecondaryAmountAndWallet_MasterPass()
        {
            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "10",
                amount = 110,
                secondaryAmount = 50,
                orderSource = orderSourceType.applepay,
                applepay = new applepayType
                {
                    data = "user",
                    signature = "sign",
                    version = "12345",
                    header = new applepayHeaderType
                    {
                        applicationData = "454657413164",
                        ephemeralPublicKey = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
                        publicKeyHash = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
                        transactionId = "1234"
                    }
                },
                wallet = new wallet
                {
                    walletSourceTypeId = "123",
                    walletSourceType = walletWalletSourceType.MasterPass
                }
            };

            var response = _cnp.Authorize(authorization);
            Assert.AreEqual("Insufficient Funds", response.message);
            Assert.AreEqual("110", response.applepayResponse.transactionAmount);
        }

        [Test]
        public void SimpleAuthWithApplepayAndSecondaryAmountAndWallet_VisaCheckout()
        {
            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "11",
                amount = 110,
                secondaryAmount = 50,
                orderSource = orderSourceType.applepay,
                applepay = new applepayType
                {
                    data = "user",
                    signature = "sign",
                    version = "12345",
                    header = new applepayHeaderType
                    {
                        applicationData = "454657413164",
                        ephemeralPublicKey = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
                        publicKeyHash = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
                        transactionId = "1234"
                    }
                },
                wallet = new wallet
                {
                    walletSourceTypeId = "123",
                    walletSourceType = walletWalletSourceType.VisaCheckout
                }
            };

            var response = _cnp.Authorize(authorization);
            Assert.AreEqual("Insufficient Funds", response.message);
            Assert.AreEqual("110", response.applepayResponse.transactionAmount);
        }

        [Test]
        public void PosWithoutCapabilityAndEntryMode()
        {
            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                pos = new pos { cardholderId = posCardholderIdTypeEnum.pin },
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000002",
                    expDate = "1210"
                },
                customBilling = new customBilling { phone = "1112223333" }
            };

            try
            {
                _cnp.Authorize(authorization);
                Assert.Fail("Exception is expected!");
            }
            catch (CnpOnlineException e)
            {
                Assert.True(e.Message.StartsWith("Error validating xml data against the schema"));
            }
        }

        [Test]
        public void TrackData()
        {
            var authorization = new authorization
            {
                id = "AX54321678",
                reportGroup = "RG27",
                orderId = "13",
                amount = 12522L,
                orderSource = orderSourceType.retail,
                billToAddress = new contact { zip = "95032" },
                card = new cardType { track = "%B40000001^Doe/JohnP^06041...?;40001=0604101064200?" },
                pos = new pos
                {
                    capability = posCapabilityTypeEnum.magstripe,
                    entryMode = posEntryModeTypeEnum.completeread,
                    cardholderId = posCardholderIdTypeEnum.signature
                }
            };

            var response = _cnp.Authorize(authorization);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void TestAuthHandleSpecialCharacters()
        {
            var authorization = new authorization
            {
                id = "1",
                reportGroup = "<'&\">",
                orderId = "14",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                paypal = new payPal
                {
                    payerId = "1234",
                    token = "1234",
                    transactionId = "123456"
                }
            };

            var response = _cnp.Authorize(authorization);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void TestNotHavingTheLogFileSettingShouldDefaultItsValueToNull()
        {
            _config.Remove("logFile");

            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "15",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "414100000000000000",
                    expDate = "1210"
                }
            };

            var response = _cnp.Authorize(authorization);
            Assert.AreEqual("000", response.response);
        }

        [Test]
        public void TestNeuterAccountNumsShouldDefaultToFalse()
        {
            _config.Remove("neuterAccountNums");

            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "16",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "414100000000000000",
                    expDate = "1210"
                }
            };

            var response = _cnp.Authorize(authorization);
            Assert.AreEqual("000", response.response);
        }

        [Test]
        public void TestPrintxmlShouldDefaultToFalse()
        {
            _config.Remove("printxml");

            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "17",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "414100000000000000",
                    expDate = "1210"
                }
            };

            var response = _cnp.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            // SANDBOX BRB
            //Assert.AreEqual("63225578415568556365452427825", response.networkTransactionId);
        }

        [Test]
        public void TestEnhancedAuthResponse()
        {
            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100322311199000",
                    expDate = "1210",
                },
                originalNetworkTransactionId = "123456789123456789123456789",
                originalTransactionAmount = 12,
                processingType = processingType.initialRecurring,
            };

            var response = _cnp.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            // SANDBOX BRB
            //Assert.AreEqual("63225578415568556365452427825", response.networkTransactionId);
        }

        [Test]
        public void TestEnhancedAuthResponseWithNetworkResponse()
        {
            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100322311199000",
                    expDate = "1210",
                },
                originalNetworkTransactionId = "123456789123456789123456789",
                originalTransactionAmount = 12,
                processingType = processingType.initialInstallment,
            };
            var response = _cnp.Authorize(authorization);
            Assert.AreEqual("000", response.response);

            // SANDBOX BRB
            //Assert.AreEqual("63225578415568556365452427825", response.networkTransactionId);
            Assert.AreEqual("visa", response.enhancedAuthResponse.networkResponse.endpoint);
            Assert.AreEqual(4, response.enhancedAuthResponse.networkResponse.networkField.fieldNumber);
            Assert.AreEqual("Transaction Amount", response.enhancedAuthResponse.networkResponse.networkField.fieldName);
            Assert.AreEqual("135798642", response.enhancedAuthResponse.networkResponse.networkField.fieldValue);
        }

        [Test]
        public void SimpleAuthWithCardPin()
        {
            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.MC,
                    number = "414100000000000000",
                    expDate = "1210",
                    pin = "1234",
                },
                customBilling = new customBilling { phone = "1112223333" }
            };
            
            var response = _cnp.Authorize(authorization);
            Assert.AreEqual("000", response.response);
        }

        [Test]
        public void SimpleAuthWithAndroidpay()
        {
            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                amount = 106,
                orderSource = orderSourceType.androidpay,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.MC,
                    number = "414100000000000000",
                    expDate = "1210",
                    pin = "1234",
                },
                customBilling = new customBilling { phone = "1112223333" }
            };
            

            var response = _cnp.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("aHR0cHM6Ly93d3cueW91dHViZS5jb20vd2F0Y2g/dj1kUXc0dzlXZ1hjUQ0K", response.androidpayResponse.cryptogram);
            Assert.AreEqual("01", response.androidpayResponse.expMonth);
            Assert.AreEqual("2050", response.androidpayResponse.expYear);
        }

        [Test]
        public void SimpleAuthWithLodgingInfo()
        {

            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                amount = 106,
                orderSource = orderSourceType.androidpay,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.MC,
                    number = "414100000000000000",
                    expDate = "1210",
                    pin = "1234",
                },
                customBilling = new customBilling { phone = "1112223333" },
                lodgingInfo = new lodgingInfo
                {
                    hotelFolioNumber = "12345",
                    checkInDate = new DateTime(2017, 1, 18),
                    customerServicePhone = "854213",
                    lodgingCharges = new List<lodgingCharge>(),

                },

            };

            authorization.lodgingInfo.lodgingCharges.Add(new lodgingCharge() { name = lodgingExtraChargeEnum.GIFTSHOP});
            var response = _cnp.Authorize(authorization);
            Assert.AreEqual("Approved", response.message);

        }

        [Test]
        public void TestAuthAsync()
        {
            var authorization = new authorization
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
                customBilling = new customBilling { phone = "1112223333" }
            };

            CancellationToken cancellationToken = new CancellationToken(false);
            var response = _cnp.AuthorizeAsync(authorization, cancellationToken);

            Assert.AreEqual("000", response.Result.response);
        }

        [Test]
        public void TestAuthAsync_newMerchantId()
        {
            _cnp.SetMerchantId("1234");
            var authorization = new authorization
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
                   customBilling = new customBilling { phone = "1112223333" }
            };

            CancellationToken cancellationToken = new CancellationToken(false);
            var response = _cnp.AuthorizeAsync(authorization, cancellationToken);

            Assert.AreEqual("000", response.Result.response);
        }

        [Test]
        public void SimpleAuthWithSkipRealtimeAUTrue()
        {
            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                skipRealtimeAU = true,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "414100000000000000",
                    expDate = "1210"
                },
                customBilling = new customBilling { phone = "1112223333" }
            };
            var response = _cnp.Authorize(authorization);

            DateTime checkDate = new DateTime(0001, 1, 1, 00, 00, 00);

            Assert.AreEqual("000", response.response);
            Assert.AreEqual(checkDate, response.postDate);
        }

        [Test]
        public void SimpleAuthWithSkipRealtimeAUTrueAsync()
        {
            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                skipRealtimeAU = true,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "414100000000000000",
                    expDate = "1210"
                },
                customBilling = new customBilling { phone = "1112223333" }
            };
            CancellationToken cancellationToken = new CancellationToken(false);
            var response = _cnp.AuthorizeAsync(authorization,cancellationToken).Result;

            DateTime checkDate = new DateTime(0001, 1, 1, 00, 00, 00);

            Assert.AreEqual("000", response.response);
            Assert.AreEqual(checkDate, response.postDate);
        }

        [Test]
        public void SimpleAuthWithSkipRealtimeAUFalse()
        {
            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                skipRealtimeAU = false,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "414100000000000000",
                    expDate = "1210"
                },
                customBilling = new customBilling { phone = "1112223333" }
            };
            var response = _cnp.Authorize(authorization);

            DateTime checkDate = new DateTime(0001, 1, 1, 00, 00, 00);

            Assert.AreEqual("000", response.response);
            Assert.AreEqual(checkDate, response.postDate);
        }

        [Test]
        public void SimpleAuthWithSkipRealtimeAUFalseAsync()
        {
            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                skipRealtimeAU = false,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "414100000000000000",
                    expDate = "1210"
                },
                customBilling = new customBilling { phone = "1112223333" }
            };
            CancellationToken cancellationToken = new CancellationToken(false);
            var response = _cnp.AuthorizeAsync(authorization,cancellationToken).Result;

            DateTime checkDate = new DateTime(0001, 1, 1, 00, 00, 00);

            Assert.AreEqual("000", response.response);
            Assert.AreEqual(checkDate, response.postDate);
        }

        [Test]
        public void SimpleAuthWithLocation()
        {
            var authorization = new authorization
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
                customBilling = new customBilling { phone = "1112223333" }
            };
            var response = _cnp.Authorize(authorization);

            DateTime checkDate = new DateTime(0001, 1, 1, 00, 00, 00);

            Assert.AreEqual("000", response.response);
            Assert.AreEqual(checkDate, response.postDate);
            Assert.AreEqual("sandbox", response.location);
        }
        
        [Test]
        public void SimpleAuthWithBusinessIndicator()
        {
            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                businessIndicator = businessIndicatorEnum.consumerBillPayment,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "414100000000000000",
                    expDate = "1210"
                },
                customBilling = new customBilling { phone = "1112223333" }
            };
            var response = _cnp.Authorize(authorization);

            DateTime checkDate = new DateTime(0001, 1, 1, 00, 00, 00);

            Assert.AreEqual("000", response.response);
            Assert.AreEqual(checkDate, response.postDate);
        }

        [Test]
        public void SimpleAuthWithOrderIDLengthGT25()
        {
            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "orderidmorethan25charactersareacceptednow",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "414100000000000000",
                    expDate = "1210"
                },
                customBilling = new customBilling { phone = "1112223333" }
            };
            var response = _cnp.Authorize(authorization);

            DateTime checkDate = new DateTime(0001, 1, 1, 00, 00, 00);

            Assert.AreEqual("000", response.response);
            Assert.AreEqual(checkDate, response.postDate);
        }

        [Test]
        public void SimpleAuthWithRetailerAddressAndAdditionalCOFdata()///12.24
        {
            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                crypto = false,
                orderChannel = orderChannelEnum.PHONE,
                fraudCheckStatus = "Not Approved",
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "414100000000000000",
                    expDate = "1210"
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
                customBilling = new customBilling { phone = "1112223333" }
            };
            var response = _cnp.Authorize(authorization);

            Assert.AreEqual("000", response.response);
            Assert.AreEqual("sandbox", response.location);
        }

        [Test]
        public void SimpleAuthWithOverridePolicy()///12.25
        {
            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                crypto = false,
                orderChannel = orderChannelEnum.PHONE,
                fraudCheckStatus = "Not Approved",
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "414100000000000000",
                    expDate = "1210"
                },
                overridePolicy = "Override Policy",
                fsErrorCode = "FS Error Code",
                merchantAccountStatus = "Merchant Account Status",
                productEnrolled = productEnrolledEnum.GUARPAY2,
                decisionPurpose = decisionPurposeEnum.INFORMATION_ONLY,
                fraudSwitchIndicator = fraudSwitchIndicatorEnum.PRE,
                customBilling = new customBilling { phone = "1112223333" }
            };
            var response = _cnp.Authorize(authorization);

            Assert.AreEqual("000", response.response);
            Assert.AreEqual("sandbox", response.location);
        }

        [Test]
        public void SimpleAuthWithModifiedLodgingInfo()///12.25
        {
            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                crypto = false,
                orderChannel = orderChannelEnum.PHONE,
                fraudCheckStatus = "Not Approved",
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "414100000000000000",
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
                },
                customBilling = new customBilling { phone = "1112223333" }
            };
            var response = _cnp.Authorize(authorization);

            Assert.AreEqual("000", response.response);
            Assert.AreEqual("sandbox", response.location);
        }
     
        [Test]
        public void SimpleAuthWithPassengerTransportData()
        {
            var authorization = new authorization
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
                    arrivalDate = new DateTime(2022, 12, 31),
                    issueDate = new DateTime(2022, 12, 25),
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
                        departureDate = new DateTime(2023, 1,31),
                        originCity = "Pune",
                        travelNumber = "TN111",
                        departureTime = "13:05",
                        arrivalTime = "16:10",
                        remarks = "NA"
                    }
                }
            };
            var response = _cnp.Authorize(authorization);

            DateTime checkDate = new DateTime(0001, 1, 1, 00, 00, 00);

            Assert.AreEqual("000", response.response);
            Assert.AreEqual(checkDate, response.postDate);
        }

        [Test]
        public void SimpleAuthWithAuthMaxApplied() //12.27
        {
            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "1234401",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "414100000000000000",
                    expDate = "1210"
                },
                customBilling = new customBilling { phone = "1112223333" }
            };
            var response = _cnp.Authorize(authorization);

            DateTime checkDate = new DateTime(0001, 1, 1, 00, 00, 00);

            Assert.AreEqual("000", response.response);
            Assert.AreEqual("000", response.authMax.authMaxResponseCode);
            Assert.AreEqual(true, response.authMax.authMaxApplied);
            Assert.AreEqual(checkDate, response.postDate);
        }
        [Test]
        public void SimpleAuthWithTxnIdAuthMaxApplied() //12.27
        {
            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                cnpTxnId = 10000401   
               
            };
            var response = _cnp.Authorize(authorization);

            DateTime checkDate = new DateTime(0001, 1, 1, 00, 00, 00);

            Assert.AreEqual("000", response.response);
            Assert.AreEqual("000", response.authMax.authMaxResponseCode);
            Assert.AreEqual(true, response.authMax.authMaxApplied);
            Assert.AreEqual(checkDate, response.postDate);
        }
        [Test]
        public void SimpleAuthWithTxnIdAuthMaxAppliedFalse() //12.27
        {
            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                cnpTxnId = 10000402

            };
            var response = _cnp.Authorize(authorization);

            DateTime checkDate = new DateTime(0001, 1, 1, 00, 00, 00);

            Assert.AreEqual("000", response.response);
            Assert.AreEqual(false, response.authMax.authMaxApplied);
            Assert.AreEqual(checkDate, response.postDate);
        }

        [Test]
        public void SimpleAuthWithAuthMaxNotApplied() //12.27
        {
            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "1234402",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "414100000000000000",
                    expDate = "1210"
                },
                customBilling = new customBilling { phone = "1112223333" }
            };
            var response = _cnp.Authorize(authorization);

            DateTime checkDate = new DateTime(0001, 1, 1, 00, 00, 00);

            Assert.AreEqual("000", response.response);
            Assert.AreEqual(false, response.authMax.authMaxApplied);
            Assert.AreEqual(checkDate, response.postDate);
        }

        //12.28,12.29 and 12.30 start
        [Test]
        public void SimpleAuthWithOrdChanelEnumMIT()
        {
            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                authIndicator = authIndicatorEnum.Estimated,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "414100000000000000",
                    expDate = "1210"
                },
                customBilling = new customBilling { phone = "1112223333" },
                orderChannel = orderChannelEnum.MIT
            };
            var response = _cnp.Authorize(authorization);

            DateTime checkDate = new DateTime(0001, 1, 1, 00, 00, 00);

            Assert.AreEqual("000", response.response);
            Assert.AreEqual(checkDate, response.postDate);
        }

        [Test]
        public void SimpleAuthWithSellerInfo()
        {
            var authorization = new authorization
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
                sellerInfo = new sellerInfo
                {
                    accountNumber = "4485581000000005",
                    aggregateOrderCount = 4,
                    aggregateOrderDollars = 100000,
                    sellerAddress = new sellerAddress
                    {
                        sellerStreetaddress = "15 Main Street",
                        sellerUnit = "100 AB",
                        sellerPostalcode = "12345",
                        sellerCity = "San Jose",
                        sellerProvincecode = "MA",
                        sellerCountrycode = "US"
                    },
                    createdDate = "2015-11-12T20:33:09",
                    domain = "vap",
                    email = "bob@example.com",
                    lastUpdateDate = "2015-11-12T20:33:09",
                    name = "bob",
                    onboardingEmail = "bob@example.com",
                    onboardingIpAddress = "75.100.88.78",
                    parentEntity = "abc",
                    phone = "9785510040",
                    sellerId = "123456789",
                    sellerTags = new sellerTagsType
                    {
                     tag = "2"
                    },
                    username = "bob123"
                }
            };
            var response = _cnp.Authorize(authorization);

            DateTime checkDate = new DateTime(0001, 1, 1, 00, 00, 00);

            Assert.AreEqual("000", response.response);
            Assert.AreEqual(checkDate, response.postDate);
        }

        [Test]
        public void SimpleAuthWithAuthIndicatorWithEstimatedEnum()
        {
            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                authIndicator = authIndicatorEnum.Estimated,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "414100000000000000",
                    expDate = "1210"
                },
                customBilling = new customBilling { phone = "1112223333" }
            };
            var response = _cnp.Authorize(authorization);

            DateTime checkDate = new DateTime(0001, 1, 1, 00, 00, 00);

            Assert.AreEqual("000", response.response);
            Assert.AreEqual(checkDate, response.postDate);
        }

        [Test]
        public void SimpleAuthWithAuthIndicatorWithIncreamentalEnum()
        {
            var authorization = new authorization
            {
                id = "1",
                customerId = "CustId",
                reportGroup = "Planets", 
                cnpTxnId = 901097991325067135,
                amount = 106,
                authIndicator = authIndicatorEnum.Incremental,
        
            };
            var response = _cnp.Authorize(authorization);

            DateTime checkDate = new DateTime(0001, 1, 1, 00, 00, 00);

            Assert.AreEqual("000", response.response);
            Assert.AreEqual(checkDate, response.postDate);
        }
        //12.28,12.29 and 12.30 end
    }
}
