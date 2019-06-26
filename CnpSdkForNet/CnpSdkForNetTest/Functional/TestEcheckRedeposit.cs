using System.Collections.Generic;
using NUnit.Framework;
using System.Threading;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestEcheckRedeposit
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
        public void simpleEcheckRedeposit()
        {
            var echeckredeposit = new echeckRedeposit
            {
                id = "1",
                reportGroup = "Planets",
                cnpTxnId = 123456
            };

            var response = _cnp.EcheckRedeposit(echeckredeposit);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void EcheckRedepositWithEcheck()
        {
            var echeckredeposit = new echeckRedeposit
            {
                id = "1",
                reportGroup = "Planets",
                cnpTxnId = 123456,
                echeck = new echeckType
                {
                    accType = echeckAccountTypeEnum.Checking,
                    accNum = "12345657890",
                    routingNum = "123456789",
                    checkNum = "123455",
                },

                customIdentifier = "CustomIdent"
            };
            
            var response = _cnp.EcheckRedeposit(echeckredeposit);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void EcheckRedepositWithEcheckToken()
        {
            var echeckredeposit = new echeckRedeposit
            {
                id = "1",
                reportGroup = "Planets",
                cnpTxnId = 123456,
                token = new echeckTokenType
                {
                    accType = echeckAccountTypeEnum.Checking,
                    cnpToken = "1234565789012",
                    routingNum = "123456789",
                    checkNum = "123455"
                }
            };

            var response = _cnp.EcheckRedeposit(echeckredeposit);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void EcheckRedepositAsync()
        {
            var echeckredeposit = new echeckRedeposit
            {
                id = "1",
                reportGroup = "Planets",
                cnpTxnId = 123456,
                token = new echeckTokenType
                {
                    accType = echeckAccountTypeEnum.Checking,
                    cnpToken = "1234565789012",
                    routingNum = "123456789",
                    checkNum = "123455"
                }
            };

            CancellationToken cancellationToken = new CancellationToken(false);
            var response = _cnp.EcheckRedepositAsync(echeckredeposit, cancellationToken);
            Assert.AreEqual("000", response.Result.response);
        }

    }
}
