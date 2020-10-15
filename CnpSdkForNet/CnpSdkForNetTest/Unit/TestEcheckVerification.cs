using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using System.Text.RegularExpressions;


namespace Cnp.Sdk.Test.Unit
{
    [TestFixture]
    class TestEcheckVerification
    {
        
        private CnpOnline cnp;

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            cnp = new CnpOnline();
        }

        [Test]
        public void TestMerchantData()
        {
            echeckVerification echeckVerification = new echeckVerification();
            echeckVerification.orderId = "1";
            echeckVerification.amount = 2;
            echeckVerification.orderSource = orderSourceType.ecommerce;
            echeckVerification.billToAddress = new contact();
            echeckVerification.billToAddress.addressLine1 = "900";
            echeckVerification.billToAddress.city = "ABC";
            echeckVerification.billToAddress.state = "MA";
            echeckVerification.merchantData = new merchantDataType();
            echeckVerification.merchantData.campaign = "camp";
            echeckVerification.merchantData.affiliate = "affil";
            echeckVerification.merchantData.merchantGroupingId = "mgi";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<echeckVerification.*<orderId>1</orderId>.*<amount>2</amount.*<merchantData>.*<campaign>camp</campaign>.*<affiliate>affil</affiliate>.*<merchantGroupingId>mgi</merchantGroupingId>.*</merchantData>.*", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.13' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><echeckVerificationResponse><cnpTxnId>123</cnpTxnId><location>sandbox</location></echeckVerificationResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var response = cnp.EcheckVerification(echeckVerification);

            Assert.NotNull(response);
            Assert.AreEqual("sandbox", response.location);
        }
    }
}
