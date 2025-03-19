using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Threading;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestRealtimeIncrementalAuth
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
        public void realtimeIncrementalAuth()
        {
            var realtimeIncAuth = new realtimeIncrementalAuthorization
            {
                id = "1",
                reportGroup = "Planets",
                cnpTxnId = 12345,
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
                originalNetworkTransactionId = "1234",
                originalRetrievalReferenceNumber = "12345",
                cumulativeAmount = 500,
                originalTransactionAmount = 480
            };
            var response = _cnp.realtimeIncrementalAuth(realtimeIncAuth);

            DateTime checkDate = new DateTime(0001, 1, 1, 00, 00, 00);

            Assert.AreEqual("000", response.response);
            Assert.AreEqual(checkDate, response.postDate);
        }
    }
}