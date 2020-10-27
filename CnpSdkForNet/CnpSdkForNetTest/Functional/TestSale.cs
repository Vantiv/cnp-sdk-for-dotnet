using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Threading;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestSale
    {
        private CnpOnline _cnp;

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            _cnp = new CnpOnline();
        }

        [Test]
        public void SimpleSaleWithTaxTypeIdentifier()
        {
            var saleObj = new sale
            {
                amount = 106,
                cnpTxnId = 123456,
                id = "1",
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000000",
                    expDate = "1210"
                },
                enhancedData = new enhancedData
                {
                    customerReference = "000000008110801",
                    salesTax = 23,
                    deliveryType = enhancedDataDeliveryType.DIG,
                    taxExempt = false,
                    detailTaxes = new List<detailTax>()


                }
            };

            var myDetailTax = new detailTax();
            myDetailTax.taxIncludedInTotal = true;
            myDetailTax.taxAmount = 23;
            myDetailTax.taxTypeIdentifier = taxTypeIdentifierEnum.Item00;
            myDetailTax.cardAcceptorTaxId = "58-1942497";
            saleObj.enhancedData.detailTaxes.Add(myDetailTax);

            var responseObj = _cnp.Sale(saleObj);
            StringAssert.AreEqualIgnoringCase("Approved", responseObj.message);
        }
        
        [Test]
        public void SimpleSaleWithTaxTypeIdentifierWithLocation()
        {
            var saleObj = new sale
            {
                amount = 106,
                cnpTxnId = 123456,
                id = "1",
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000000",
                    expDate = "1210"
                },
                enhancedData = new enhancedData
                {
                    customerReference = "000000008110801",
                    salesTax = 23,
                    deliveryType = enhancedDataDeliveryType.DIG,
                    taxExempt = false,
                    detailTaxes = new List<detailTax>()


                }
            };

            var myDetailTax = new detailTax();
            myDetailTax.taxIncludedInTotal = true;
            myDetailTax.taxAmount = 23;
            myDetailTax.taxTypeIdentifier = taxTypeIdentifierEnum.Item00;
            myDetailTax.cardAcceptorTaxId = "58-1942497";
            saleObj.enhancedData.detailTaxes.Add(myDetailTax);

            var responseObj = _cnp.Sale(saleObj);
            Assert.AreEqual("sandbox", responseObj.location);
            StringAssert.AreEqualIgnoringCase("Approved", responseObj.message);
        }

        [Test]
        public void SimpleSaleWithCard()
        {
            var saleObj = new sale
            {
                id = "1",
                amount = 106,
                cnpTxnId = 123456,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000000",
                    expDate = "1210"
                }
            };
            Console.WriteLine(saleObj.Serialize());
            var responseObj = _cnp.Sale(saleObj);
            StringAssert.AreEqualIgnoringCase("Approved", responseObj.message);
        }

        [Test]
        public void SimpleSaleWithMpos()
        {
            var saleObj = new sale
            {
                id = "1",
                amount = 106,
                cnpTxnId = 123456,
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

            var responseObj = _cnp.Sale(saleObj);
            StringAssert.AreEqualIgnoringCase("Approved", responseObj.message);
        }

        [Test]
        public void SimpleSaleWithPayPal()
        {
            var saleObj = new sale
            {
                id = "1",
                amount = 106,
                cnpTxnId = 123456,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                paypal = new payPal
                {
                    payerId = "1234",
                    token = "1234",
                    transactionId = "123456"
                }
            };

            var responseObj = _cnp.Sale(saleObj);
            StringAssert.AreEqualIgnoringCase("Approved", responseObj.message);
        }

        [Test]
        public void SimpleSaleWithApplepayAndSecondaryAmountAndWallet()
        {
            var saleObj = new sale
            {
                id = "1",
                amount = 110,
                secondaryAmount = 50,
                cnpTxnId = 123456,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                applepay = new applepayType
                {
                    header = new applepayHeaderType
                    {
                        applicationData = "454657413164",
                        ephemeralPublicKey = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
                        publicKeyHash = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
                        transactionId = "1234"
                    },
                    data = "user",
                    signature = "sign",
                    version = "12345"
                },
                wallet = new Sdk.wallet
                {
                    walletSourceTypeId = "123",
                    walletSourceType = walletWalletSourceType.MasterPass
                }
            };

            var responseObj = _cnp.Sale(saleObj);
            Assert.AreEqual("Insufficient Funds", responseObj.message);
            Assert.AreEqual("110", responseObj.applepayResponse.transactionAmount);
        }

        [Test]
        public void SimpleSaleWithInvalidFraudCheck()
        {
            var saleObj = new sale
            {
                id = "1",
                amount = 106,
                cnpTxnId = 123456,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000000",
                    expDate = "1210"
                },
                cardholderAuthentication = new fraudCheckType
                {
                    authenticationValue = "123456789012345678901234567890123456789012345678901234567890",
                }
            };

            try
            {
                var responseObj = _cnp.Sale(saleObj);
            }
            catch (CnpOnlineException e)
            {
                Assert.True(e.Message.StartsWith("Error validating xml data against the schema"));
            }
        }

        [Test]
        public void SimpleSaleWithDirectDebit()
        {
            var saleObj = new sale
            {
                id = "1",
                amount = 106,
                cnpTxnId = 123456,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                sepaDirectDebit = new sepaDirectDebitType
                {
                    mandateProvider = mandateProviderType.Merchant,
                    sequenceType = sequenceTypeType.FirstRecurring,
                    iban = "123456789123456789",
                    preferredLanguage = countryTypeEnum.US
                }
            };

            var responseObj = _cnp.Sale(saleObj);
            StringAssert.AreEqualIgnoringCase("Approved", responseObj.message);
        }

        [Test]
        public void SimpleSaleWithProcessTypeNetIdTranAmt()
        {
            var saleObj = new sale
            {
                id = "1",
                amount = 106,
                cnpTxnId = 123456,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000000",
                    expDate = "1210"
                },

                processingType = processingType.initialRecurring,
                originalNetworkTransactionId = "123456789123456789123456789",
                originalTransactionAmount = 12
            };

            var responseObj = _cnp.Sale(saleObj);
            StringAssert.AreEqualIgnoringCase("Approved", responseObj.message);
        }

        [Test]
        public void SimpleSaleWithUndefinedProcessTypeNetIdTranAmt()
        {
            var saleObj = new sale
            {
                id = "1",
                amount = 106,
                cnpTxnId = 123456,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000000",
                    expDate = "1210"
                },

                processingType = processingType.undefined,
                originalNetworkTransactionId = "123456789123456789123456789",
                originalTransactionAmount = 12
            };

            var responseObj = _cnp.Sale(saleObj);
            StringAssert.AreEqualIgnoringCase("Approved", responseObj.message);
        }

        [Test]
        public void SimpleSaleWithIdealResponse()
        {
            var saleObj = new sale
            {
                id = "1",
                amount = 106,
                cnpTxnId = 123456,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                ideal = new idealType
                {
                    preferredLanguage = countryTypeEnum.US
                }
            };

            var responseObj = _cnp.Sale(saleObj);
            StringAssert.AreEqualIgnoringCase("Approved", responseObj.message);
        }

        [Test]
        public void SimpleSaleWithGiropayResponse()
        {
            var saleObj = new sale
            {
                id = "1",
                amount = 106,
                cnpTxnId = 123456,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                giropay = new giropayType
                {
                    preferredLanguage = countryTypeEnum.US
                }
            };

            var responseObj = _cnp.Sale(saleObj);
            StringAssert.AreEqualIgnoringCase("Approved", responseObj.message);
        }

        [Test]
        public void SimpleSaleWithSofortResponse()
        {
            var saleObj = new sale
            {
                id = "1",
                amount = 106,
                cnpTxnId = 123456,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                sofort = new sofortType
                {
                    preferredLanguage = countryTypeEnum.US
                }
            };

            var responseObj = _cnp.Sale(saleObj);
            StringAssert.AreEqualIgnoringCase("Approved", responseObj.message);
        }


        [Test]
        public void SimpleSaleWithLodginInfo()
        {
            var saleObj = new sale
            {
                id = "1",
                amount = 106,
                cnpTxnId = 123456,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                sofort = new sofortType
                {
                    preferredLanguage = countryTypeEnum.US
                },
                lodgingInfo = new lodgingInfo
                {
                    hotelFolioNumber = "12345",
                    checkInDate = new DateTime(2017, 1, 18),
                    customerServicePhone = "854213",
                    lodgingCharges = new List<lodgingCharge>(),

                }
            };
            saleObj.lodgingInfo.lodgingCharges.Add(new lodgingCharge() { name = lodgingExtraChargeEnum.GIFTSHOP });
            var responseObj = _cnp.Sale(saleObj);
            StringAssert.AreEqualIgnoringCase("Approved", responseObj.message);
        }

        [Test]
        public void SimpleSaleWithPinlessDebitRequest()
        {
            var saleObj = new sale
            {
                id = "1",
                amount = 106,
                cnpTxnId = 123456,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                sofort = new sofortType
                {
                    preferredLanguage = countryTypeEnum.US
                },
                pinlessDebitRequest = new pinlessDebitRequestType { routingPreference = routingPreferenceEnum .pinlessDebitOnly}

            };
            
            var responseObj = _cnp.Sale(saleObj);
            StringAssert.AreEqualIgnoringCase("Approved", responseObj.message);
        }

        [Test]
        public void TestSaleWithCardAsync()
        {
            var saleObj = new sale
            {
                id = "1",
                amount = 106,
                cnpTxnId = 123456,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000000",
                    expDate = "1210"
                }
            };
            Console.WriteLine(saleObj.Serialize());
            CancellationToken cancellationToken = new CancellationToken(false);
            var responseObj = _cnp.SaleAsync(saleObj, cancellationToken);
            StringAssert.AreEqualIgnoringCase("000", responseObj.Result.response);
        }

        [Test]
        public void SimpleSaleWithSkipRealtimeAUTrue() {
            var saleObj = new sale {
                id = "1",
                amount = 106,
                cnpTxnId = 123456,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                skipRealtimeAU = true,
                card = new cardType {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000000",
                    expDate = "1210"
                }
            };
            Console.WriteLine(saleObj.Serialize());
            var responseObj = _cnp.Sale(saleObj);
            StringAssert.AreEqualIgnoringCase("Approved", responseObj.message);
        }

        [Test]
        public void SimpleSaleWithSkipRealtimeAUTrueAsync() {
            var saleObj = new sale {
                id = "1",
                amount = 106,
                cnpTxnId = 123456,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                skipRealtimeAU = true,
                card = new cardType {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000000",
                    expDate = "1210"
                }
            };
            Console.WriteLine(saleObj.Serialize());
            CancellationToken cancellationToken = new CancellationToken(false);
            var responseObj = _cnp.SaleAsync(saleObj,cancellationToken).Result;
            StringAssert.AreEqualIgnoringCase("Approved", responseObj.message);
        }

        [Test]
        public void SimpleSaleWithSkipRealtimeAUFalse() {
            var saleObj = new sale {
                id = "1",
                amount = 106,
                cnpTxnId = 123456,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                skipRealtimeAU = false,
                card = new cardType {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000000",
                    expDate = "1210"
                }
            };
            Console.WriteLine(saleObj.Serialize());
            var responseObj = _cnp.Sale(saleObj);
            StringAssert.AreEqualIgnoringCase("Approved", responseObj.message);
        }

        [Test]
        public void SimpleSaleWithSkipRealtimeAUFalseAsync() {
            var saleObj = new sale {
                id = "1",
                amount = 106,
                cnpTxnId = 123456,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                skipRealtimeAU = false,
                card = new cardType {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000000",
                    expDate = "1210"
                }
            };
            Console.WriteLine(saleObj.Serialize());
            CancellationToken cancellationToken = new CancellationToken(false);
            var responseObj = _cnp.SaleAsync(saleObj,cancellationToken).Result;
            StringAssert.AreEqualIgnoringCase("Approved", responseObj.message);
        }
        
        
        [Test]
        public void SimpleSaleWithBusinessIndicator()
        {
            var saleObj = new sale
            {
                id = "1",
                amount = 106,
                cnpTxnId = 123456,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                businessIndicator = businessIndicatorEnum.consumerBillPayment,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000000",
                    expDate = "1210"
                }
            };
            Console.WriteLine(saleObj.Serialize());
            var responseObj = _cnp.Sale(saleObj);
            StringAssert.AreEqualIgnoringCase("Approved", responseObj.message);
        }
    }
}
