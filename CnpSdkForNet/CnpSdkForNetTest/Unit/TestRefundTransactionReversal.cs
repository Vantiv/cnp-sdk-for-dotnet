using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using System.Text.RegularExpressions;


namespace Cnp.Sdk.Test.Unit
{
    [TestFixture]
    class TestRefundTransactionReversal
    {
        
        private CnpOnline cnp;

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            cnp = new CnpOnline();
        }

        [Test]
        public void TestSurchargeAmount()
        {
            refundTransactionReversal reversal = new refundTransactionReversal();
            reversal.cnpTxnId = 3;
            reversal.amount = 2;
            reversal.surchargeAmount = 1;
            reversal.reportGroup = "Planets";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<surchargeAmount>1</surchargeAmount>\r\n.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='12.16' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><refundTransactionReversalResponse><cnpTxnId>3</cnpTxnId></refundTransactionReversalResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.RefundTransactionReversal(reversal);
        }

        [Test]
        public void TestSurchargeAmount_Optional()
        {
            refundTransactionReversal reversal = new refundTransactionReversal();
            reversal.cnpTxnId = 3;
            reversal.amount = 2;
            reversal.reportGroup = "Planets";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='12.16' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><refundTransactionReversalResponse><cnpTxnId>123</cnpTxnId></refundTransactionReversalResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.RefundTransactionReversal(reversal);
        }
        
        [Test]
        public void TestTransactionReversalWithLocation()
        {
            refundTransactionReversal reversal = new refundTransactionReversal();
            reversal.cnpTxnId = 3;
            reversal.amount = 2;
            reversal.surchargeAmount = 1;
            reversal.reportGroup = "Planets";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<surchargeAmount>1</surchargeAmount>\r\n.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='12.16' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><refundTransactionReversalResponse><cnpTxnId>123</cnpTxnId><location>sandbox</location></refundTransactionReversalResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var response = cnp.RefundTransactionReversal(reversal);
            
            Assert.NotNull(response);
            Assert.AreEqual("sandbox", response.location);
        }

    }
}
