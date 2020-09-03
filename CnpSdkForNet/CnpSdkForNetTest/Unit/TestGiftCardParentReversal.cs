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
    class TestGiftCardParentReversal
    {
        private CnpOnline cnp;

        [TestFixtureSetUp]
        public void SetUpCnp()
        {
            cnp = new CnpOnline();
        }

        [Test]
        public void DepositReversal()
        {
            depositReversal reversal = new depositReversal();
            reversal.id = "1";
            reversal.reportGroup = "planets";
            reversal.cnpTxnId = 123456000;
            giftCardCardType card = new giftCardCardType();
            card.type = methodOfPaymentTypeEnum.GC;
            card.number = "414100000000000000";
            card.expDate = "1210";
            card.pin = "1234";
            reversal.card = card;
            reversal.originalRefCode = "123";
            reversal.originalAmount = 123;
            reversal.originalTxnTime = new DateTime(2017, 01, 01);
            reversal.originalSystemTraceId = 123;
            reversal.originalSequenceNumber = "123456";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<cnpTxnId>123456000</cnpTxnId>\r\n<card>\r\n<type>GC</type>\r\n<number>414100000000000000</number>\r\n<expDate>1210</expDate>\r\n<pin>1234</pin>\r\n</card>\r\n<originalRefCode>123</originalRefCode>\r\n<originalAmount>123</originalAmount>\r\n<originalTxnTime>2017-01-01T00:00:00Z</originalTxnTime>\r\n<originalSystemTraceId>123</originalSystemTraceId>\r\n<originalSequenceNumber>123456</originalSequenceNumber>.*", RegexOptions.Singleline)  ))
                    .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><authReversalResponse><cnpTxnId>123</cnpTxnId></authReversalResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.DepositReversal(reversal);
        }

        [Test]
        public void RefundReversal()
        {
            refundReversal reversal = new refundReversal();
            reversal.id = "1";
            reversal.reportGroup = "planets";
            reversal.cnpTxnId = 123456000;
            giftCardCardType card = new giftCardCardType();
            card.type = methodOfPaymentTypeEnum.GC;
            card.number = "414100000000000000";
            card.expDate = "1210";
            card.pin = "1234";
            reversal.card = card;
            reversal.originalRefCode = "123";
            reversal.originalAmount = 123;
            reversal.originalTxnTime = new DateTime(2017, 01, 01);
            reversal.originalSystemTraceId = 123;
            reversal.originalSequenceNumber = "123456";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<cnpTxnId>123456000</cnpTxnId>\r\n<card>\r\n<type>GC</type>\r\n<number>414100000000000000</number>\r\n<expDate>1210</expDate>\r\n<pin>1234</pin>\r\n</card>\r\n<originalRefCode>123</originalRefCode>\r\n<originalAmount>123</originalAmount>\r\n<originalTxnTime>2017-01-01T00:00:00Z</originalTxnTime>\r\n<originalSystemTraceId>123</originalSystemTraceId>\r\n<originalSequenceNumber>123456</originalSequenceNumber>.*", RegexOptions.Singleline)  ))
                    .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><authReversalResponse><cnpTxnId>123</cnpTxnId></authReversalResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.RefundReversal(reversal);
        }

        [Test]
        public void ActivateReversal()
        {
            activateReversal reversal = new activateReversal();
            reversal.id = "1";
            reversal.reportGroup = "planets";
            reversal.cnpTxnId = 123456000;
            reversal.virtualGiftCardBin = "123";
            giftCardCardType card = new giftCardCardType();
            card.type = methodOfPaymentTypeEnum.GC;
            card.number = "414100000000000000";
            card.expDate = "1210";
            card.pin = "1234";
            reversal.card = card;
            reversal.originalRefCode = "123";
            reversal.originalAmount = 123;
            reversal.originalTxnTime = new DateTime(2017, 01, 01);
            reversal.originalSystemTraceId = 123;
            reversal.originalSequenceNumber = "123456";
            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<cnpTxnId>123456000</cnpTxnId>\r\n<virtualGiftCardBin>123</virtualGiftCardBin>\r\n<card>\r\n<type>GC</type>\r\n<number>414100000000000000</number>\r\n<expDate>1210</expDate>\r\n<pin>1234</pin>\r\n</card>\r\n<originalRefCode>123</originalRefCode>\r\n<originalAmount>123</originalAmount>\r\n<originalTxnTime>2017-01-01T00:00:00Z</originalTxnTime>\r\n<originalSystemTraceId>123</originalSystemTraceId>\r\n<originalSequenceNumber>123456</originalSequenceNumber>.*", RegexOptions.Singleline)  ))
                    .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><authReversalResponse><cnpTxnId>123</cnpTxnId></authReversalResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.ActivateReversal(reversal);
        }

        [Test]
        public void DeactivateReversal()
        {
            deactivateReversal reversal = new deactivateReversal();
            reversal.id = "1";
            reversal.reportGroup = "planets";
            reversal.cnpTxnId = 123456000;
            giftCardCardType card = new giftCardCardType();
            card.type = methodOfPaymentTypeEnum.GC;
            card.number = "414100000000000000";
            card.expDate = "1210";
            card.pin = "1234";
            reversal.card = card;
            reversal.originalRefCode = "123";
            reversal.originalTxnTime = new DateTime(2017, 01, 01);
            reversal.originalSystemTraceId = 123;
            reversal.originalSequenceNumber = "123456";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<cnpTxnId>123456000</cnpTxnId>\r\n<card>\r\n<type>GC</type>\r\n<number>414100000000000000</number>\r\n<expDate>1210</expDate>\r\n<pin>1234</pin>\r\n</card>\r\n<originalRefCode>123</originalRefCode>\r\n<originalTxnTime>2017-01-01T00:00:00Z</originalTxnTime>\r\n<originalSystemTraceId>123</originalSystemTraceId>\r\n<originalSequenceNumber>123456</originalSequenceNumber>.*", RegexOptions.Singleline)  ))
                    .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><authReversalResponse><cnpTxnId>123</cnpTxnId></authReversalResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.DeactivateReversal(reversal);
        }

        [Test]
        public void LoadReversal()
        {
            loadReversal reversal = new loadReversal();
            reversal.id = "1";
            reversal.reportGroup = "planets";
            reversal.cnpTxnId = 123456000;
            giftCardCardType card = new giftCardCardType();
            card.type = methodOfPaymentTypeEnum.GC;
            card.number = "414100000000000000";
            card.expDate = "1210";
            card.pin = "1234";
            reversal.card = card;
            reversal.originalRefCode = "123";
            reversal.originalAmount = 123;
            reversal.originalTxnTime = new DateTime(2017, 01, 01);
            reversal.originalSystemTraceId = 123;
            reversal.originalSequenceNumber = "123456";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<cnpTxnId>123456000</cnpTxnId>\r\n<card>\r\n<type>GC</type>\r\n<number>414100000000000000</number>\r\n<expDate>1210</expDate>\r\n<pin>1234</pin>\r\n</card>\r\n<originalRefCode>123</originalRefCode>\r\n<originalAmount>123</originalAmount>\r\n<originalTxnTime>2017-01-01T00:00:00Z</originalTxnTime>\r\n<originalSystemTraceId>123</originalSystemTraceId>\r\n<originalSequenceNumber>123456</originalSequenceNumber>.*", RegexOptions.Singleline)  ))
                    .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><authReversalResponse><cnpTxnId>123</cnpTxnId></authReversalResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.LoadReversal(reversal);
        }

        [Test]
        public void UnloadReversal()
        {
            unloadReversal reversal = new unloadReversal();
            reversal.id = "1";
            reversal.reportGroup = "planets";
            reversal.cnpTxnId = 123456000;
            giftCardCardType card = new giftCardCardType();
            card.type = methodOfPaymentTypeEnum.GC;
            card.number = "414100000000000000";
            card.expDate = "1210";
            card.pin = "1234";
            reversal.card = card;
            reversal.originalRefCode = "123";
            reversal.originalAmount = 123;
            reversal.originalTxnTime = new DateTime(2017, 01, 01);
            reversal.originalSystemTraceId = 123;
            reversal.originalSequenceNumber = "123456";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<cnpTxnId>123456000</cnpTxnId>\r\n<card>\r\n<type>GC</type>\r\n<number>414100000000000000</number>\r\n<expDate>1210</expDate>\r\n<pin>1234</pin>\r\n</card>\r\n<originalRefCode>123</originalRefCode>\r\n<originalAmount>123</originalAmount>\r\n<originalTxnTime>2017-01-01T00:00:00Z</originalTxnTime>\r\n<originalSystemTraceId>123</originalSystemTraceId>\r\n<originalSequenceNumber>123456</originalSequenceNumber>.*", RegexOptions.Singleline)  ))
                    .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><authReversalResponse><cnpTxnId>123</cnpTxnId></authReversalResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.UnloadReversal(reversal);
        }
    }
}
