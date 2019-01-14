using System.Collections.Generic;
using NUnit.Framework;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestEcheckSale
    {
        private CnpOnline _cnp;
        private Dictionary<string, string> _config;

        [TestFixtureSetUp]
        public void SetUpCnp()
        {
            CommManager.reset();
            _config = new Dictionary<string, string>
            {
                {"url", Properties.Settings.Default.url},
                {"reportGroup", "Default Report Group"},
                {"username", "DOTNET"},
                {"version", "11.0"},
                {"timeout", "5000"},
                {"merchantId", "101"},
                {"password", "TESTCASE"},
                {"printxml", "true"},
                {"proxyHost", Properties.Settings.Default.proxyHost},
                {"proxyPort", Properties.Settings.Default.proxyPort},
                {"logFile", Properties.Settings.Default.logFile},
                {"neuterAccountNums", "true"}
            };

            _cnp = new CnpOnline(_config);
        }

        [Test]
        public void SimpleEcheckSaleWithEcheck()
        {
            var echeckSaleObj = new echeckSale
            {
                id = "1",
                reportGroup = "Planets",
                amount = 123456,
                orderId = "12345",
                orderSource = orderSourceType.ecommerce,
                echeck = new echeckType
                {
                    accType = echeckAccountTypeEnum.Checking,
                    accNum = "12345657890",
                    routingNum = "123456789",
                    checkNum = "123455"
                },
                billToAddress = new contact
                {
                    name = "Bob",
                    city = "lowell",
                    state = "MA",
                    email = "cnp.com"
                }
            };


            var response = _cnp.EcheckSale(echeckSaleObj);
            StringAssert.AreEqualIgnoringCase("Approved", response.message);
        }

        [Test]
        public void EcheckSaleWithCnpTxnId()
        {
            var echeckSaleObj = new echeckSale
            {
                id = "1",
                reportGroup = "Planets",
                cnpTxnId = 1234,
                amount = 123456,
                customBilling = new customBilling
                {
                    phone = "123456789",
                    descriptor = "good",
                },
                customIdentifier = "ident",
                orderId = "12345"
            };

            var response = _cnp.EcheckSale(echeckSaleObj);
            StringAssert.AreEqualIgnoringCase("Approved", response.message);
        }

        [Test]
        public void EcheckSaleWithOrderId()
        {
            var echeckSaleObj = new echeckSale
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "1",
                verify = true,
                amount = 1234,
                secondaryAmount = 123456,
                orderSource = orderSourceType.ecommerce,

                billToAddress = new contact
                {
                    name = "Bob",
                    city = "lowell",
                    state = "MA",
                    email = "cnp.com"
                },
                shipToAddress = new contact
                {
                    name = "Bob",
                    city = "lowell",
                    state = "MA",
                    email = "cnp.com"
                },
                echeck = new echeckType
                {
                    accType = echeckAccountTypeEnum.Checking,
                    accNum = "12345657890",
                    routingNum = "123456789",
                    checkNum = "123455"
                },
                customBilling = new customBilling
                {
                    phone = "123456789",
                    descriptor = "good"
                },
                merchantData = new merchantDataType
                {
                    affiliate = "Affiliate",
                    campaign = "campaign",
                    merchantGroupingId = "Merchant Group ID"

                },
                customIdentifier = "ident"
            };

            var response = _cnp.EcheckSale(echeckSaleObj);
            StringAssert.AreEqualIgnoringCase("Approved", response.message);
        }

        [Test]
        public void NoAmount()
        {
            var echeckSaleObj = new echeckSale
            {
                id = "1",
                reportGroup = "Planets"
            };


            try
            {
                //expected exception;
                var response = _cnp.EcheckSale(echeckSaleObj);
            }
            catch (CnpOnlineException e)
            {
                Assert.True(e.Message.StartsWith("Error validating xml data against the schema"));
            }
        }

        [Test]
        public void EcheckSaleWithShipTo()
        {
            var echeckSaleObj = new echeckSale
            {
                id = "1",
                reportGroup = "Planets",
                amount = 123456,
                verify = true,
                orderId = "12345",
                orderSource = orderSourceType.ecommerce,
                echeck = new echeckType
                {
                    accType = echeckAccountTypeEnum.Checking,
                    accNum = "12345657890",
                    routingNum = "123456789",
                    checkNum = "123455"
                },
                billToAddress = new contact
                {
                    name = "Bob",
                    city = "lowell",
                    state = "MA",
                    email = "cnp.com"
                },
                shipToAddress = new contact
                {
                    name = "Bob",
                    city = "lowell",
                    state = "MA",
                    email = "cnp.com"
                }
            };

            var response = _cnp.EcheckSale(echeckSaleObj);
            StringAssert.AreEqualIgnoringCase("Approved", response.message);
        }

        [Test]
        public void EcheckSaleWithEcheckToken()
        {
            echeckSale echeckSaleObj = new echeckSale
            {
                id = "1",
                reportGroup = "Planets",
                amount = 123456,
                verify = true,
                orderId = "12345",
                orderSource = orderSourceType.ecommerce,
                token = new echeckTokenType
                {
                    accType = echeckAccountTypeEnum.Checking,
                    cnpToken = "1234565789012",
                    routingNum = "123456789",
                    checkNum = "123455"
                },
                customBilling = new customBilling
                {
                    phone = "123456789",
                    descriptor = "good"
                },
                billToAddress = new contact
                {
                    name = "Bob",
                    city = "lowell",
                    state = "MA",
                    email = "cnp.com"
                },
            };

            var response = _cnp.EcheckSale(echeckSaleObj);
            StringAssert.AreEqualIgnoringCase("Approved", response.message);
        }

        [Test]
        public void EcheckSaleMissingBilling()
        {
            var echeckSaleObj = new echeckSale
            {
                id = "1",
                reportGroup = "Planets",
                amount = 123456,
                orderId = "12345",
                orderSource = orderSourceType.ecommerce,

                echeck = new echeckType
                {
                    accType = echeckAccountTypeEnum.Checking,
                    accNum = "12345657890",
                    routingNum = "123456789",
                    checkNum = "123455"
                }
            };

            try
            {
                //expected exception;
                var response = _cnp.EcheckSale(echeckSaleObj);
            }
            catch (CnpOnlineException e)
            {
                Assert.True(e.Message.StartsWith("Error validating xml data against the schema"));
            }
        }

        [Test]
        public void SimpleEcheckSale()
        {
            var echeckSaleObj = new echeckSale
            {
                id = "1",
                reportGroup = "Planets",
                cnpTxnId = 123456789101112,
                amount = 12
            };

            var response = _cnp.EcheckSale(echeckSaleObj);
            StringAssert.AreEqualIgnoringCase("Approved", response.message);
        }

        [Test]
        public void SimpleEcheckSaleWithSecondaryAmountWithOrderId()
        {
            var echeckSaleObj = new echeckSale
            {
                id = "1",
                reportGroup = "Planets",
                amount = 123456,
                secondaryAmount = 50,
                orderId = "12345",
                orderSource = orderSourceType.ecommerce,
                echeck = new echeckType
                {
                    accType = echeckAccountTypeEnum.CorpSavings,
                    accNum = "12345657890",
                    routingNum = "123456789",
                    checkNum = "123455"
                },
                billToAddress = new contact
                {
                    name = "Bob",
                    city = "lowell",
                    state = "MA",
                    email = "cnp.com"
                }
            };

            var response = _cnp.EcheckSale(echeckSaleObj);
            StringAssert.AreEqualIgnoringCase("Approved", response.message);
        }

        [Test]
        public void SimpleEcheckSaleWithSecondaryAmount()
        {
            var echeckSaleObj = new echeckSale
            {
                id = "1",
                reportGroup = "Planets",
                amount = 123456,
                secondaryAmount = 50,
                cnpTxnId = 1234565L
            };

            try
            {
                ////expected exception;
                var response = _cnp.EcheckSale(echeckSaleObj);
            }
            catch (CnpOnlineException e)
            {
                Assert.True(e.Message.StartsWith("Error validating xml data against the schema"));
            }
        }
    }
}
