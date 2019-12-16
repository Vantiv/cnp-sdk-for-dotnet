using System.Collections.Generic;
using NUnit.Framework;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestHttpActionEvent
    {
        private CnpOnline _cnp;

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            _cnp = new CnpOnline();
        }

        [Test]
        public void TestHttpEvents()
        {
            var requestCount = 0;
            var responseCount = 0;
            var httpActionCount = 0;

            _cnp.HttpAction += (sender, args) =>
            {
                var eventArgs = (Communications.HttpActionEventArgs)args;
                httpActionCount++;
                if (eventArgs.RequestType == Communications.RequestType.Request)
                {
                    requestCount++;
                }
                else if (eventArgs.RequestType == Communications.RequestType.Response)
                {
                    responseCount++;
                }
            };

            var capture = new capture
            {
                cnpTxnId = 123456000,
                amount = 106,
                id = "1"
            };

            _cnp.Capture(capture);
            Assert.AreEqual(httpActionCount, 2);
            Assert.AreEqual(requestCount, 1);
            Assert.AreEqual(responseCount, 1);
        }
    }
}
