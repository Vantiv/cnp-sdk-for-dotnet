using System.Collections.Generic;
using NUnit.Framework;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestEcheckCredit
    {
        private CnpOnline _cnp;
        private Dictionary<string, string> _config;

        [TestFixtureSetUp]
        public void SetUpCnp()
        {
            _config = new Dictionary<string, string>
            {
                {"url", "https://payments.vantivprelive.com/vap/communicator/online"},
                {"reportGroup", "Default Report Group"},
                {"username", "SDKTEAM"},
                {"version", "11.0"},
                {"timeout", "5000"},
                {"merchantId", "1288791"},
                {"password", "V3r5K6v7"},
                {"printxml", "true"},
                {"proxyHost", Properties.Settings.Default.proxyHost},
                {"proxyPort", Properties.Settings.Default.proxyPort},
                {"logFile", Properties.Settings.Default.logFile},
                {"neuterAccountNums", "true"}
            };

            _cnp = new CnpOnline(_config);
        }

        [Test]
        public void simpleEcheckCredit()
        {
            var echeckcredit = new echeckCredit
            {
                id = "1",
                reportGroup = "Planets",
                amount = 12L,
                cnpTxnId = 123456789101112L,
            };

            var response = _cnp.EcheckCredit(echeckcredit);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void noCnpTxnId()
        {
            var echeckcredit = new echeckCredit
            {
                id = "1",
                reportGroup = "Planets",
            };

            try
            {
                _cnp.EcheckCredit(echeckcredit);
                Assert.Fail("Expected exception");
            }
            catch (CnpOnlineException e)
            {
                Assert.IsTrue(e.Message.Contains("Error validating xml data against the schema"));
            }
        }

        [Test]
        public void echeckCreditWithEcheck()
        {
            var echeckcredit = new echeckCredit
            {
                id = "1",
                reportGroup = "Planets",
                amount = 12L,
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
                    city = "Lowell",
                    state = "MA",
                    email = "cnp.com",
                },
                customIdentifier = "CustomIdent"
            };

            var response = _cnp.EcheckCredit(echeckcredit);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void echeckCreditWithToken()
        {
            var echeckcredit = new echeckCredit
            {
                id = "1",
                reportGroup = "Planets",
                amount = 12L,
                orderId = "12345",
                orderSource = orderSourceType.ecommerce,
                echeckToken = new echeckTokenType
                {
                    accType = echeckAccountTypeEnum.Checking,
                    cnpToken = "1234565789012",
                    routingNum = "123456789",
                    checkNum = "123455",
                },

                billToAddress = new contact
                {
                    name = "Bob",
                    city = "Lowell",
                    state = "MA",
                    email = "cnp.com"
                }
            };



            var response = _cnp.EcheckCredit(echeckcredit);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void missingBilling()
        {
            var echeckcredit = new echeckCredit
            {
                id = "1",
                reportGroup = "Planets",
                amount = 12L,
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
                _cnp.EcheckCredit(echeckcredit);
                Assert.Fail("Expected exception");
            }
            catch (CnpOnlineException e)
            {
                Assert.IsTrue(e.Message.Contains("Error validating xml data against the schema"));
            }
        }

        [Test]
        public void echeckCreditWithSecondaryAmountWithOrderIdAndCcdPaymentInfo()
        {
            var echeckcredit = new echeckCredit
            {
                id = "1",
                reportGroup = "Planets",
                amount = 12L,
                secondaryAmount = 50,
                orderId = "12345",
                orderSource = orderSourceType.ecommerce,
                echeck = new echeckType
                {
                    accType = echeckAccountTypeEnum.Checking,
                    accNum = "12345657890",
                    routingNum = "123456789",
                    checkNum = "123455",
                    ccdPaymentInformation = "9876554"
                },
                billToAddress = new contact
                {
                    name = "Bob",
                    city = "Lowell",
                    state = "MA",
                    email = "cnp.com"

                }
            };

            var response = _cnp.EcheckCredit(echeckcredit);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void echeckCreditWithSecondaryAmountWithCnpTxnId()
        {
            var echeckcredit = new echeckCredit
            {
                id = "1",
                reportGroup = "Planets",
                amount = 12L,
                secondaryAmount = 50,
                cnpTxnId = 12345L,
                customIdentifier = "CustomIdent"
            };
            
            var response = _cnp.EcheckCredit(echeckcredit);
            Assert.AreEqual("Approved", response.message);
        }
    }
}
