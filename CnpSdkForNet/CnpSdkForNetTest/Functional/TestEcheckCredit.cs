using System.Collections.Generic;
using Xunit;

namespace Cnp.Sdk.Test.Functional
{
    public class TestEcheckCredit
    {
        private CnpOnline _cnp;
        private Dictionary<string, string> _config;

        public TestEcheckCredit()
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

        [Fact]
        public void SimpleEcheckCredit()
        {
            var echeckcredit = new echeckCredit
            {
                id = "1",
                reportGroup = "Planets",
                amount = 12L,
                cnpTxnId = 123456789101112L,
            };

            var response = _cnp.EcheckCredit(echeckcredit);
            Assert.Equal("Approved", response.message);
        }

        [Fact]
        public void NoCnpTxnId()
        {
            var echeckcredit = new echeckCredit
            {
                id = "1",
                reportGroup = "Planets",
            };

            try
            {
                _cnp.EcheckCredit(echeckcredit);
                Assert.True(false, "Expected exception");
            }
            catch (CnpOnlineException e)
            {
                Assert.True(e.Message.Contains("Error validating xml data against the schema"));
            }
        }

        [Fact]
        public void EcheckCreditWithEcheck()
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
            Assert.Equal("Approved", response.message);
        }

        [Fact]
        public void EcheckCreditWithToken()
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
            Assert.Equal("Approved", response.message);
        }

        [Fact]
        public void MissingBilling()
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
                Assert.True(false, "Expected exception");
            }
            catch (CnpOnlineException e)
            {
                Assert.True(e.Message.Contains("Error validating xml data against the schema"));
            }
        }

        [Fact]
        public void EcheckCreditWithSecondaryAmountWithOrderIdAndCcdPaymentInfo()
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
            Assert.Equal("Approved", response.message);
        }

        [Fact]
        public void EcheckCreditWithSecondaryAmountWithCnpTxnId()
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
            Assert.Equal("Approved", response.message);
        }
    }
}
