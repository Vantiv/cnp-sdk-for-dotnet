using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using System.Text.RegularExpressions;


namespace Cnp.Sdk.Test.Unit
{
    [TestFixture]
    class TestTransactionReversal
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
            transactionReversal reversal = new transactionReversal();
            reversal.cnpTxnId = 3;
            reversal.amount = 2;
            reversal.surchargeAmount = 1;
            reversal.reportGroup = "Planets";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<surchargeAmount>1</surchargeAmount>\r\n.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='12.16' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><transactionReversalResponse><cnpTxnId>3</cnpTxnId></transactionReversalResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.TransactionReversal(reversal);
        }

        [Test]
        public void TestSurchargeAmount_Optional()
        {
            transactionReversal reversal = new transactionReversal();
            reversal.cnpTxnId = 3;
            reversal.amount = 2;
            reversal.reportGroup = "Planets";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='12.16' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><transactionReversalResponse><cnpTxnId>123</cnpTxnId></transactionReversalResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.TransactionReversal(reversal);
        }
        
        [Test]
        public void TestTransactionReversalWithLocation()
        {
            transactionReversal reversal = new transactionReversal();
            reversal.cnpTxnId = 3;
            reversal.amount = 2;
            reversal.surchargeAmount = 1;
            reversal.reportGroup = "Planets";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<surchargeAmount>1</surchargeAmount>\r\n.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='12.16' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><transactionReversalResponse><cnpTxnId>123</cnpTxnId><location>sandbox</location></transactionReversalResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var response = cnp.TransactionReversal(reversal);
            
            Assert.NotNull(response);
            Assert.AreEqual("sandbox", response.location);
        }

    }
}
