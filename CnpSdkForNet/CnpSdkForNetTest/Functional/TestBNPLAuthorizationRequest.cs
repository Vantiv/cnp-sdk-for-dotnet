using System.Collections.Generic;
using NUnit.Framework;
using System.Threading;
using System;
using Castle.Core.Configuration;
using System.Runtime.Intrinsics.X86;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestBNPLAuthorizationRequest
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
        public void simpleBNPLAuthorizationRequest()
        {
            var BNPLAuthorization = new BNPLAuthorizationRequest
            {
                id = "1",
                customerId = "12334",
                reportGroup = "Planets",
                amount = 9999,
                orderId = " orderId ",
                provider = provider.AFFIRM,
                postCheckoutRedirectUrl = "noone@litle.com",
                customerInfo = new customerInfo()
                {
                    ssn = "032359685",
                    dob = new DateTime(2001,01,01),
                    customerRegistrationDate = new DateTime(1967, 8, 10, 00, 00, 00),
                    customerType = customerInfoCustomerType.Existing,
                    incomeAmount = 2147483647,
                    incomeCurrency = currencyCodeEnum.AUD,
                    customerCheckingAccount = true,
                    customerSavingAccount = true,
                    employerName = "litle and Co.",
                    customerWorkTelephone = "9782750000",
                    residenceStatus = customerInfoResidenceStatus.Own,
                    yearsAtResidence = 12,
                    yearsAtEmployer = 12,
                    accountUsername = "bobiverse",
                    userAccountNumber = "370000000000010",
                    userAccountEmail = "bob@example.com",
                    membershipId = "3cf0e3bd188949798d4d23d3085953e8",
                    membershipPhone = "442071838750",
                    membershipEmail = "bob@example.com",
                    membershipName = "FRESHPASS",
                    accountCreatedDate = new DateTime(2021, 01, 01, 00, 00, 00),
                    userAccountPhone = "9785510040"
                },
                billToAddress = new contact
                {
                    name = "John & Jane Smith",
                    firstName = "abddf",
                    middleInitial = "c",
                    lastName = "cdf",
                    companyName = "company",
                    addressLine1 = "1 Main St.",
                    addressLine2 = "2 Main St.",
                    addressLine3 = "3 Main st.",
                    city = "Burlington",
                    state = "MA",
                    zip = "01803-3747",
                    country = countryTypeEnum.US,
                    email = "abc@cvn",
                    phone = "12345678",
                    sellerId = "12334",
                    url = "abc@cvb"
                },
                shipToAddress = new contact
                {
                    name = "John & Jane Smith",
                    firstName = "abddf",
                    middleInitial = "c",
                    lastName = "cdf",
                    companyName = "company",
                    addressLine1 = "1 Main St.",
                    addressLine2 = "2 Main St.",
                    addressLine3 = "3 Main st.",
                    city = "Burlington",
                    state = "MA",
                    zip = "01803-3747",
                    country = countryTypeEnum.US,
                    email = "abc@cvn",
                    phone = "12345678",
                    sellerId = "12334",
                    url = "abc@cvb"
                },
                enhancedData = new enhancedData
                {
                    customerReference = "000000008110801",
                    salesTax = 23,
                    deliveryType = enhancedDataDeliveryType.DIG,
                    taxExempt = false,
                    discountAmount = 23,
                    shippingAmount = 23,
                    dutyAmount = 23,
                    shipFromPostalCode = "csd",
                    destinationPostalCode = "123",
                    destinationCountry = countryTypeEnum.USA,
                    invoiceReferenceNumber = "123456",
                    orderDate = new DateTime(2024,12,01),
                    detailTaxes = new List<detailTax>(),
                    lineItems = new List<lineItemData>(),
                    discountCode = "123",
                    discountPercent = 100,
                    fulfilmentMethodType = fulfilmentMethodTypeEnum.COUNTER_PICKUP
                },

            };
            var response = _cnp.BNPLAuthorization(BNPLAuthorization);
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
            BNPLAuthorization.enhancedData.lineItems.Add(mylineItemData);
            Assert.AreEqual("Approved", response.message);
        }
    }
}
