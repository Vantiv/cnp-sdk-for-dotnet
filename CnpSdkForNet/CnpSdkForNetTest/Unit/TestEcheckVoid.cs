using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using System.Text.RegularExpressions;


namespace Cnp.Sdk.Test.Unit
{
    [TestFixture]
    class TestEcheckVoid
    {

        private CnpOnline cnp;

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            cnp = new CnpOnline();
        }

        [Test]
        public void TestFraudFilterOverride()
        {
            echeckVoid echeckVoid = new echeckVoid();
            echeckVoid.cnpTxnId = 123456789;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<echeckVoid.*<cnpTxnId>123456789.*", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.13' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><echeckVoidResponse><cnpTxnId>123</cnpTxnId><location>sandbox</location></echeckVoidResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var response = cnp.EcheckVoid(echeckVoid);

            Assert.NotNull(response);
            Assert.AreEqual("sandbox", response.location);
        }


    }
}
