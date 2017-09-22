using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Cnp.Sdk;
using Moq;
using System.Text.RegularExpressions;


namespace Cnp.Sdk.Test.Unit
{
    [TestFixture]
    class TestEcheckVoid
    {
        
        private CnpOnline cnp;

        [TestFixtureSetUp]
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

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<echeckVoid.*<cnpTxnId>123456789.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<cnpOnlineResponse version='8.13' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><echeckVoidResponse><cnpTxnId>123</cnpTxnId></echeckVoidResponse></cnpOnlineResponse>");
     
            Communications mockedCommunication = mock.Object;
            cnp.setCommunication(mockedCommunication);
            cnp.EcheckVoid(echeckVoid);
        }

        [Test]
        public void simpleForceCaptureWithSecondaryAmount()
        {
            forceCapture forcecapture = new forceCapture();
            forcecapture.id = "1";
            forcecapture.amount = 106;
            forcecapture.secondaryAmount = 50;
            forcecapture.orderId = "12344";
            forcecapture.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000000";
            card.expDate = "1210";
            forcecapture.card = card;
            forceCaptureResponse response = cnp.ForceCapture(forcecapture);
            Assert.AreEqual("Approved", response.message);
        }
    }
}
