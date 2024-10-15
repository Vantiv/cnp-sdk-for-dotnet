using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using System.Text.RegularExpressions;

namespace Cnp.Sdk.Test.Unit
{
    [TestFixture]
    internal class TestBNPLRefundRequest
    {

        private CnpOnline cnp;
        Dictionary<String, String> config;
        [OneTimeSetUp]
        public void SetUpCnp()
        {
            cnp = new CnpOnline();
            config = new ConfigManager().getConfig();
        }

        [Test]
        public void simpleBNPLRefundRequest()
        {
            var bNPLRefund = new BNPLRefundRequest
            {
                id = "1",
                customerId = "12334",
                reportGroup = "Planets",
                cnpTxnId = 12345,
                amount = 9999,
                orderId = " orderId "
            };

            var mock = new Mock<Communications>();
            if (config["encrypteOltpPayload"] == "true")
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<cnpOnlineRequest.*<encryptedPayload.*</encryptedPayload>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='12.37' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><BNPLRefundResponse><cnpTxnId>348408968181194299</cnpTxnId><location>sandbox</location></BNPLRefundResponse></cnpOnlineResponse>");
            }
            else
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>9999</amount>.*<orderId> orderId </orderId>.*<cnpTxnId>12345</cnpTxnId>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='12.37' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><BNPLRefundResponse><cnpTxnId>348408968181194299</cnpTxnId><location>sandbox</location></BNPLRefundResponse></cnpOnlineResponse>");
            }
            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var response = cnp.BNPLRefund(bNPLRefund);

            Assert.NotNull(response);
            Assert.AreEqual("sandbox", response.location);

        }
    }
}
