using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;

namespace Cnp.Sdk.Test.Functional {
    [TestFixture]
    public class TestCustomer {
        private CnpOnline _cnp;
        private Dictionary<string, string> _config;

        [OneTimeSetUp]
        public void SetUpCnp() {
            CommManager.reset();
            _config = new Dictionary<string, string> {
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
        public void AddCustomerCredit() {
            var customerCredit = new customerCredit {
                id = "11",
                reportGroup = "Default Report Group",

                fundingCustomerId = "value for fundingCustomerId",
                customerName = "value for customerName",
                fundsTransferId = "value for fundsTransferId",
                amount = 500,
                customIdentifier = "WorldPay",
                accountInfo = new echeckType() {
                    accType = echeckAccountTypeEnum.Checking,
                    accNum = "1092969901",
                    routingNum = "011075150",
                    checkNum = "123455",
                }
            };

            var response = _cnp.CustomerCredit(customerCredit);
            Assert.AreEqual("000", response.response);
        }
        
        [Test]
        public void AddCustomerCreditAsync() {
            var customerCredit = new customerCredit {
                id = "11",
                reportGroup = "Default Report Group",

                fundingCustomerId = "value for fundingCustomerId",
                customerName = "value for customerName",
                fundsTransferId = "value for fundsTransferId",
                amount = 500,
                customIdentifier = "WorldPay",
                accountInfo = new echeckType() {
                    accType = echeckAccountTypeEnum.Checking,
                    accNum = "1092969901",
                    routingNum = "011075150",
                    checkNum = "123455",
                }
            };

            CancellationToken cancellationToken = new CancellationToken(false);
            var response = _cnp.CustomerCreditAsync(customerCredit,cancellationToken);
            Assert.AreEqual("000", response.Result.response);
        }
        
        [Test]
        public void AddCustomerCreditNullName() {
            var customerCredit = new customerCredit {
                id = "11",
                reportGroup = "Default Report Group",

                fundingCustomerId = "value for fundingCustomerId",
                fundsTransferId = "value for fundsTransferId",
                amount = 500,
                customIdentifier = "WorldPay",
                accountInfo = new echeckType() {
                    accType = echeckAccountTypeEnum.Checking,
                    accNum = "1092969901",
                    routingNum = "011075150",
                    checkNum = "123455",
                }
            };

            Assert.Throws<CnpOnlineException>(() => { _cnp.CustomerCredit(customerCredit); });
        }
        
        [Test]
        public void AddCustomerCreditAsyncNullName() {
            var customerCredit = new customerCredit {
                id = "11",
                reportGroup = "Default Report Group",

                fundingCustomerId = "value for fundingCustomerId",
                fundsTransferId = "value for fundsTransferId",
                amount = 500,
                customIdentifier = "WorldPay",
                accountInfo = new echeckType() {
                    accType = echeckAccountTypeEnum.Checking,
                    accNum = "1092969901",
                    routingNum = "011075150",
                    checkNum = "123455",
                }
            };

            CancellationToken cancellationToken = new CancellationToken(false);
            Assert.Throws<AggregateException>(() => { var _ = _cnp.CustomerCreditAsync(customerCredit,cancellationToken).Result; });
        }
        
        [Test]
        public void AddCustomerDebit() {
            var customerDebit = new customerDebit {
                id = "11",
                reportGroup = "Default Report Group",

                fundingCustomerId = "value for fundingCustomerId",
                customerName = "value for customerName",
                fundsTransferId = "value for fundsTransferId",
                amount = 500,
                customIdentifier = "WorldPay",
                accountInfo = new echeckType() {
                    accType = echeckAccountTypeEnum.Checking,
                    accNum = "1092969901",
                    routingNum = "011075150",
                    checkNum = "123455",
                }
            };

            var response = _cnp.CustomerDebit(customerDebit);
            Assert.AreEqual("000", response.response);
        }
        
        [Test]
        public void AddCustomerDebitAsync() {
            var customerDebit = new customerDebit {
                id = "11",
                reportGroup = "Default Report Group",

                fundingCustomerId = "value for fundingCustomerId",
                customerName = "value for customerName",
                fundsTransferId = "value for fundsTransferId",
                amount = 500,
                customIdentifier = "WorldPay",
                accountInfo = new echeckType() {
                    accType = echeckAccountTypeEnum.Checking,
                    accNum = "1092969901",
                    routingNum = "011075150",
                    checkNum = "123455",
                }
            };

            CancellationToken cancellationToken = new CancellationToken(false);
            var response = _cnp.CustomerDebitAsync(customerDebit,cancellationToken);
            Assert.AreEqual("000", response.Result.response);
        }
        
        [Test]
        public void AddCustomerDebitNullName() {
            var customerDebit = new customerDebit {
                id = "11",
                reportGroup = "Default Report Group",

                fundingCustomerId = "value for fundingCustomerId",
                fundsTransferId = "value for fundsTransferId",
                amount = 500,
                customIdentifier = "WorldPay",
                accountInfo = new echeckType() {
                    accType = echeckAccountTypeEnum.Checking,
                    accNum = "1092969901",
                    routingNum = "011075150",
                    checkNum = "123455",
                }
            };

            Assert.Throws<CnpOnlineException>(() => { _cnp.CustomerDebit(customerDebit); });
        }
        
        [Test]
        public void AddCustomerDebitAsyncNullName() {
            var customerDebit = new customerDebit {
                id = "11",
                reportGroup = "Default Report Group",

                fundingCustomerId = "value for fundingCustomerId",
                fundsTransferId = "value for fundsTransferId",
                amount = 500,
                customIdentifier = "WorldPay",
                accountInfo = new echeckType() {
                    accType = echeckAccountTypeEnum.Checking,
                    accNum = "1092969901",
                    routingNum = "011075150",
                    checkNum = "123455",
                }
            };

            CancellationToken cancellationToken = new CancellationToken(false);
            Assert.Throws<AggregateException>(() => { var _ = _cnp.CustomerDebitAsync(customerDebit,cancellationToken).Result; });
        }
        
        [Test]
        [Ignore("Sandbox does not check for negative amounts. Production does check.")]
        public void AddCustomerDebitNegativeAmount() {
            var customerDebit = new customerDebit {
                id = "11",
                reportGroup = "Default Report Group",

                fundingCustomerId = "value for fundingCustomerId",
                customerName = "value for customerName",
                fundsTransferId = "value for fundsTransferId",
                amount = -500,
                customIdentifier = "WorldPay",
                accountInfo = new echeckType() {
                    accType = echeckAccountTypeEnum.Checking,
                    accNum = "1092969901",
                    routingNum = "011075150",
                    checkNum = "123455",
                }
            };

            var response = _cnp.CustomerDebit(customerDebit);
            Assert.AreEqual("340", response.response);
        }
        
        [Test]
        public void AddCustomerDebitNameTooLong() {
            var customerDebit = new customerDebit {
                id = "11",
                reportGroup = "Default Report Group",

                fundingCustomerId = "123456789012345678901234567890123456789012345678901234567890",
                customerName = "value for customerName",
                fundsTransferId = "value for fundsTransferId",
                amount = 500,
                customIdentifier = "WorldPay",
                accountInfo = new echeckType() {
                    accType = echeckAccountTypeEnum.Checking,
                    accNum = "1092969901",
                    routingNum = "011075150",
                    checkNum = "123455",
                }
            };

            Assert.Throws<CnpOnlineException>(() => { _cnp.CustomerDebit(customerDebit); });
        }
        
        [Test]
        public void AddCustomerDebitAsyncNameTooLong() {
            var customerDebit = new customerDebit {
                id = "11",
                reportGroup = "Default Report Group",

                fundingCustomerId = "123456789012345678901234567890123456789012345678901234567890",
                customerName = "value for customerName",
                fundsTransferId = "value for fundsTransferId",
                amount = 500,
                customIdentifier = "WorldPay",
                accountInfo = new echeckType() {
                    accType = echeckAccountTypeEnum.Checking,
                    accNum = "1092969901",
                    routingNum = "011075150",
                    checkNum = "123455",
                }
            };

            CancellationToken cancellationToken = new CancellationToken(false);
            Assert.Throws<AggregateException>(() => { var _ = _cnp.CustomerDebitAsync(customerDebit,cancellationToken).Result; });
        }
    }
}