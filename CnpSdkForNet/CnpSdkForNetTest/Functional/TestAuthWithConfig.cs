using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Threading;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestAuthWithConfig
    {
        private CnpOnline _cnp;
        private Dictionary<string, string> _config;

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            _config = GetCnpConfig(); ;
            _cnp = new CnpOnline(_config);
        }

        [Test]
        public void SimpleUnsafeConfig()
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


            Assert.AreEqual("000", response.response);
        }

        static Dictionary<string, string> GetCnpConfig()
        {
            Dictionary<string, string> _cnpConfig = new Dictionary<string, string>();
            _cnpConfig.Add("url", "https://www.testvantivcnp.com/sandbox/communicator/online");
            _cnpConfig.Add("merchantId", "128");
            _cnpConfig.Add("reportGroup", "Default Report Group");
            _cnpConfig.Add("username", "asd");
            _cnpConfig.Add("password", "sdf");
            _cnpConfig.Add("printxml", "true");
            _cnpConfig.Add("timeout", "900000");
            _cnpConfig.Add("proxyHost", "");
            _cnpConfig.Add("proxyPort", "");
            _cnpConfig.Add("logFile", "cnpLogFile.log");
            _cnpConfig.Add("neuterAccountNums", "");
            _cnpConfig.Add("sftpUrl", "");
            _cnpConfig.Add("sftpUsername", "");
            _cnpConfig.Add("sftpPassword", "");
            _cnpConfig.Add("onlineBatchUrl", "");
            _cnpConfig.Add("onlineBatchPort", "");
            _cnpConfig.Add("requestDirectory", "");
            _cnpConfig.Add("responseDirectory", "");
            _cnpConfig.Add("useEncryption", "");
            _cnpConfig.Add("vantivPublicKeyId", "");
            _cnpConfig.Add("pgpPassphrase", "");
            _cnpConfig.Add("neuterUserCredentials", "");
            _cnpConfig.Add("maxConnections", "");
            return _cnpConfig;
        }
    }
}