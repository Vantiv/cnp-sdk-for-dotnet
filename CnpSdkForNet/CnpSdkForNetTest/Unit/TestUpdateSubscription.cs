using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using System.Text.RegularExpressions;


namespace Cnp.Sdk.Test.Unit
{
    [TestFixture]
    internal class TestUpdateSubscription
    {        
        private CnpOnline _cnp;

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            _cnp = new CnpOnline();
        }

        [Test]
        public void TestSimple()
        {
            var update = new updateSubscription
            {
                billingDate = new DateTime(2002, 10, 9),
                billToAddress = new contact
                {
                    name = "Greg Dake",
                    city = "Lowell",
                    state = "MA",
                    email = "sdksupport@vantiv.com"
                },
                card = new cardType
                {
                    number = "4100000000000001",
                    expDate = "1215",
                    type = methodOfPaymentTypeEnum.VI
                },
                planCode = "abcdefg",
                subscriptionId = 12345
            };
            
            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<cnpOnlineRequest.*?<updateSubscription>\r\n<subscriptionId>12345</subscriptionId>\r\n<planCode>abcdefg</planCode>\r\n<billToAddress>\r\n<name>Greg Dake</name>.*?</billToAddress>\r\n<card>\r\n<type>VI</type>.*?</card>\r\n<billingDate>2002-10-09</billingDate>\r\n</updateSubscription>\r\n</cnpOnlineRequest>.*?.*", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.20' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><updateSubscriptionResponse ><cnpTxnId>456</cnpTxnId><response>000</response><message>Approved</message><responseTime>2013-09-04</responseTime><subscriptionId>12345</subscriptionId></updateSubscriptionResponse></cnpOnlineResponse>");
     
            var mockedCommunication = mock.Object;
            _cnp.SetCommunication(mockedCommunication);
            var response = _cnp.UpdateSubscription(update);
            Assert.AreEqual("12345", response.subscriptionId);
            Assert.AreEqual("456", response.cnpTxnId);
            Assert.AreEqual("000", response.response);
            Assert.NotNull(response.responseTime);
        }

        [Test]
        public void TestWithToken()
        {
            var update = new updateSubscription
            {
                billingDate = new DateTime(2002, 10, 9),
                billToAddress = new contact
                {
                    name = "Greg Dake",
                    city = "Lowell",
                    state = "MA",
                    email = "sdksupport@vantiv.com"
                },
                token = new cardTokenType
                {
                    cnpToken = "987654321098765432",
                    expDate = "0750",
                    cardValidationNum = "798",
                    type = methodOfPaymentTypeEnum.VI,
                    checkoutId = "0123456789012345678"
                },
                planCode = "abcdefg",
                subscriptionId = 12345
            };
            
            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<cnpOnlineRequest.*?<updateSubscription>\r\n<subscriptionId>12345</subscriptionId>\r\n<planCode>abcdefg</planCode>\r\n<billToAddress>\r\n<name>Greg Dake</name>.*?</billToAddress>\r\n<token>.*?<checkoutId>0123456789012345678</checkoutId>\r\n</token>\r\n<billingDate>2002-10-09</billingDate>\r\n</updateSubscription>\r\n</cnpOnlineRequest>.*?.*", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.20' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><updateSubscriptionResponse ><cnpTxnId>456</cnpTxnId><response>000</response><message>Approved</message><responseTime>2013-09-04</responseTime><subscriptionId>12345</subscriptionId></updateSubscriptionResponse></cnpOnlineResponse>");
     
            var mockedCommunication = mock.Object;
            _cnp.SetCommunication(mockedCommunication);
            var response = _cnp.UpdateSubscription(update);
            Assert.AreEqual("12345", response.subscriptionId);
            Assert.AreEqual("456", response.cnpTxnId);
            Assert.AreEqual("000", response.response);
            Assert.NotNull(response.responseTime);
        }
    }
}
