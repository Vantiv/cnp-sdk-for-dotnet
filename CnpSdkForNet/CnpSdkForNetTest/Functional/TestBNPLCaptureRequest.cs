using System.Collections.Generic;
using NUnit.Framework;
using System.Threading;
using System;
using Castle.Core.Configuration;
using System.Runtime.Intrinsics.X86;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestBNPLCaptureRequest
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
        public void simpleBNPLCaptureRequest()
        {
            var bNPLCapture = new BNPLCaptureRequest
            {
                id = "1",
                customerId = "12334",
                reportGroup = "Planets",
                cnpTxnId = 12345,
                amount = 9999,
                orderId = " orderId "
            };
            var response = _cnp.BNPLCapture(bNPLCapture);
            Assert.AreEqual("Approved", response.message);
        }


    }
}
