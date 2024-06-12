using System.Collections.Generic;
using NUnit.Framework;
using System.Threading;
using System;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestFinicityUrlRequest
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
        public void simpleFinicityUrlRequest()
        {
            var finicityUrl = new finicityUrlRequest
            {
                id = "1",
                customerId ="12334",
                reportGroup = "Planets",
                firstName = "first",
                lastName = "last",
                email = "bcv@abc"
            };

            var response = _cnp.FinicityUrl(finicityUrl);
            Assert.AreEqual("Approved", response.message);
        }
    }
}
