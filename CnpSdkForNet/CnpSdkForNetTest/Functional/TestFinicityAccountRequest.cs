using System.Collections.Generic;
using NUnit.Framework;
using System.Threading;
using System;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestFinicityAccountRequest
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
        public void simpleFinicityAccountRequest()
        {
            var finicityAccount = new finicityAccountRequest
            {
                id = "1",
                customerId = "12334",
                reportGroup = "Planets",
                echeckCustomerId = "ABC"
            };

            var response = _cnp.FinicityAccount(finicityAccount);
            Assert.AreEqual("Approved", response.message);
        }
    }
}
