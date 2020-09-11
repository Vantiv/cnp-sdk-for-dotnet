using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using System.Text.RegularExpressions;

namespace Cnp.Sdk.Test.Unit
{
    [TestFixture]
    class TestGiftCard
    {
        private CnpOnline cnp;

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            cnp = new CnpOnline();
        }

        [Test]
        public void TestGiftCardAuthReversalSimple()
        {
            giftCardAuthReversal giftCard = new giftCardAuthReversal();
            giftCard.id = "1";
            giftCard.reportGroup = "Planets";
            giftCard.cnpTxnId = 123456789;
            giftCard.originalRefCode = "abc123";
            giftCard.originalAmount = 500;
            giftCard.originalTxnTime = new DateTime(2017,01,01);
            giftCard.originalSystemTraceId = 123;
            giftCard.originalSequenceNumber = "123456";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<cnpTxnId>123456789</cnpTxnId>\r\n<originalRefCode>abc123</originalRefCode>\r\n<originalAmount>500</originalAmount>\r\n<originalTxnTime>2017-01-01T00:00:00Z</originalTxnTime>\r\n<originalSystemTraceId>123</originalSystemTraceId>\r\n<originalSequenceNumber>123456</originalSequenceNumber>.*", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.18' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><giftCardAuthReversalResponse><cnpTxnId>123</cnpTxnId></giftCardAuthReversalResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            giftCardAuthReversalResponse giftCardAuthReversalResponse = cnp.GiftCardAuthReversal(giftCard);
            Assert.AreEqual(123, giftCardAuthReversalResponse.cnpTxnId);
        }

        [Test]
        public void TestGiftCardAuthReversalWithCard()
        {
            giftCardAuthReversal giftCard = new giftCardAuthReversal();
            giftCard.id = "1";
            giftCard.reportGroup = "Planets";
            giftCard.cnpTxnId = 123456789;
            giftCardCardType card = new giftCardCardType();
            card.type = methodOfPaymentTypeEnum.GC;
            card.number = "414100000000000000";
            card.expDate = "1210";
            giftCard.card = card;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<card>\r\n<type>GC</type>\r\n<number>414100000000000000</number>\r\n<expDate>1210</expDate>\r\n</card>.*", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.18' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><giftCardAuthReversalResponse><cnpTxnId>123</cnpTxnId></giftCardAuthReversalResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            giftCardAuthReversalResponse giftCardAuthReversalResponse = cnp.GiftCardAuthReversal(giftCard);
            Assert.AreEqual(123, giftCardAuthReversalResponse.cnpTxnId);
        }

        [Test]
        public void TestGiftCardCaptureSimple()
        {
            giftCardCapture giftCardCapture = new giftCardCapture();
            giftCardCapture.id = "1";
            giftCardCapture.reportGroup = "Planets";
            giftCardCapture.cnpTxnId = 123456000;
            giftCardCapture.captureAmount = 106;
            giftCardCardType card = new giftCardCardType();
            card.type = methodOfPaymentTypeEnum.GC;
            card.number = "414100000000000000";
            card.expDate = "1210";
            giftCardCapture.card = card;
            giftCardCapture.originalRefCode = "abc123";
            giftCardCapture.originalAmount = 43534345;
            giftCardCapture.originalTxnTime = new DateTime(2017, 01, 01);

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<cnpTxnId>123456000</cnpTxnId>\r\n<captureAmount>106</captureAmount>\r\n<card>\r\n<type>GC</type>\r\n<number>414100000000000000</number>\r\n<expDate>1210</expDate>\r\n</card>\r\n<originalRefCode>abc123</originalRefCode>\r\n<originalAmount>43534345</originalAmount>\r\n<originalTxnTime>2017-01-01T00:00:00Z</originalTxnTime>.*", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><giftCardCaptureResponse><cnpTxnId>123</cnpTxnId></giftCardCaptureResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.GiftCardCapture(giftCardCapture);
        }

        [Test]
        public void TestGiftCardCreditTxnId()
        {
            giftCardCredit credit = new giftCardCredit();
            credit.id = "1";
            credit.reportGroup = "planets";
            credit.cnpTxnId = 123456000;
            credit.creditAmount = 106;
            giftCardCardType card = new giftCardCardType();
            card.type = methodOfPaymentTypeEnum.GC;
            card.number = "4100000000000000";
            card.expDate = "1210";
            credit.card = card;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<cnpTxnId>123456000</cnpTxnId>\r\n<creditAmount>106</creditAmount>\r\n<card>\r\n<type>GC</type>\r\n<number>4100000000000000</number>\r\n<expDate>1210</expDate>\r.*", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><giftCardCreditResponse><cnpTxnId>123</cnpTxnId></giftCardCreditResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.GiftCardCredit(credit);
        }

        [Test]
        public void TestGiftCardCreditOrderId()
        {
            giftCardCredit credit = new giftCardCredit();
            credit.id = "1";
            credit.reportGroup = "planets";
            credit.orderId = "2111";
            credit.creditAmount = 106;
            credit.orderSource = orderSourceType.echeckppd;
            giftCardCardType card = new giftCardCardType();
            card.type = methodOfPaymentTypeEnum.GC;
            card.number = "4100000000000000";
            card.expDate = "1210";
            credit.card = card;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<orderId>2111</orderId>\r\n<creditAmount>106</creditAmount>\r\n<orderSource>echeckppd</orderSource>\r\n<card>\r\n<type>GC</type>\r\n<number>4100000000000000</number>\r\n<expDate>1210</expDate>\r.*", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><giftCardCreditResponse><cnpTxnId>123</cnpTxnId></giftCardCreditResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.GiftCardCredit(credit);
        }
        
        [Test]
        public void TestGiftCardCreditWithLocation()
        {
            giftCardCredit credit = new giftCardCredit();
            credit.id = "1";
            credit.reportGroup = "planets";
            credit.orderId = "2111";
            credit.creditAmount = 106;
            credit.orderSource = orderSourceType.echeckppd;
            giftCardCardType card = new giftCardCardType();
            card.type = methodOfPaymentTypeEnum.GC;
            card.number = "4100000000000000";
            card.expDate = "1210";
            credit.card = card;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<orderId>2111</orderId>\r\n<creditAmount>106</creditAmount>\r\n<orderSource>echeckppd</orderSource>\r\n<card>\r\n<type>GC</type>\r\n<number>4100000000000000</number>\r\n<expDate>1210</expDate>\r.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><giftCardCreditResponse><cnpTxnId>123</cnpTxnId><location>sandbox</location></giftCardCreditResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var response = cnp.GiftCardCredit(credit);
            
            Assert.NotNull(response);
            Assert.AreEqual("sandbox", response.location);
        }
    }
}
