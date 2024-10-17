using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using System.Text.RegularExpressions;

namespace Cnp.Sdk.Test.Unit
{
    [TestFixture]
    internal class TestBNPLInquiryRequest
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
        public void simpleBNPLInquiryRequest()
        {
            var bNPLInquiry = new BNPLInquiryRequest
            {
                id = "1",
                customerId = "12334",
                reportGroup = "Planets",
                cnpTxnId = 12345,
                orderId = " orderId "
            };

            var mock = new Mock<Communications>();
            if (config["encryptOltpPayload"] == "true")
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<cnpOnlineRequest.*<encryptedPayload.*</encryptedPayload>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='12.37' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><BNPLInquiryResponse><cnpTxnId>348408968181194299</cnpTxnId><location>sandbox</location></BNPLInquiryResponse></cnpOnlineResponse>");
            }
            else
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<orderId> orderId </orderId>.*<cnpTxnId>12345</cnpTxnId>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='12.37' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><BNPLInquiryResponse><cnpTxnId>348408968181194299</cnpTxnId><location>sandbox</location></BNPLInquiryResponse></cnpOnlineResponse>");
            }
            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var response = cnp.BNPLInquiry(bNPLInquiry);

            Assert.NotNull(response);
            Assert.AreEqual("sandbox", response.location);

        }
    }
}
