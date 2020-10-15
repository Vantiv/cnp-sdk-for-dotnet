using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using System.Text.RegularExpressions;


namespace Cnp.Sdk.Test.Unit
{
    [TestFixture]
    class TestCapture
    {
        
        private CnpOnline cnp;

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            cnp = new CnpOnline();
        }

        [Test]
        public void TestSimpleCapture()
        {
            capture capture = new capture();
            capture.cnpTxnId = 3;
            capture.amount = 2;
            capture.payPalNotes = "note";
            capture.reportGroup = "Planets";
            capture.pin = "1234";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<payPalNotes>note</payPalNotes>\r\n<pin>1234</pin>.*", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureResponse><cnpTxnId>123</cnpTxnId></captureResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.Capture(capture);
        }

        [Test]
        public void TestSurchargeAmount()
        {
            capture capture = new capture();
            capture.cnpTxnId = 3;
            capture.amount = 2;
            capture.surchargeAmount = 1;
            capture.payPalNotes = "note";
            capture.reportGroup = "Planets";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<surchargeAmount>1</surchargeAmount>\r\n<payPalNotes>note</payPalNotes>.*", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureResponse><cnpTxnId>123</cnpTxnId></captureResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.Capture(capture);
        }

        [Test]
        public void TestSurchargeAmount_Optional()
        {
            capture capture = new capture();
            capture.cnpTxnId = 3;
            capture.amount = 2;
            capture.payPalNotes = "note";
            capture.reportGroup = "Planets";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<payPalNotes>note</payPalNotes>.*", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureResponse><cnpTxnId>123</cnpTxnId></captureResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.Capture(capture);
        }
        
        [Test]
        public void TestCaptureWithLocation()
        {
            capture capture = new capture();
            capture.cnpTxnId = 3;
            capture.amount = 2;
            capture.payPalNotes = "note";
            capture.reportGroup = "Planets";
            capture.pin = "1234";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<payPalNotes>note</payPalNotes>\r\n<pin>1234</pin>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureResponse><cnpTxnId>123</cnpTxnId><location>sandbox</location></captureResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var response = cnp.Capture(capture);
            
            Assert.NotNull(response);
            Assert.AreEqual("sandbox", response.location);
        }

    }
}
