using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using System.Text.RegularExpressions;


namespace Cnp.Sdk.Test.Unit
{
    [TestFixture]
    class TestAuthReversal
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
            authReversal reversal = new authReversal();
            reversal.cnpTxnId = 3;
            reversal.amount = 2;
            reversal.surchargeAmount = 1;
            reversal.payPalNotes = "note";
            reversal.reportGroup = "Planets";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<surchargeAmount>1</surchargeAmount>\r\n<payPalNotes>note</payPalNotes>.*", RegexOptions.Singleline) ))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><authReversalResponse><cnpTxnId>123</cnpTxnId></authReversalResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.AuthReversal(reversal);
        }

        [Test]
        public void TestSurchargeAmount_Optional()
        {
            authReversal reversal = new authReversal();
            reversal.cnpTxnId = 3;
            reversal.amount = 2;
            reversal.payPalNotes = "note";
            reversal.reportGroup = "Planets";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<payPalNotes>note</payPalNotes>.*", RegexOptions.Singleline) ))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><authReversalResponse><cnpTxnId>123</cnpTxnId></authReversalResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.AuthReversal(reversal);
        }
        
        [Test]
        public void TestAuthReversalWithLocation()
        {
            authReversal reversal = new authReversal();
            reversal.cnpTxnId = 3;
            reversal.amount = 2;
            reversal.surchargeAmount = 1;
            reversal.payPalNotes = "note";
            reversal.reportGroup = "Planets";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<surchargeAmount>1</surchargeAmount>\r\n<payPalNotes>note</payPalNotes>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><authReversalResponse><cnpTxnId>123</cnpTxnId><location>sandbox</location></authReversalResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var response = cnp.AuthReversal(reversal);
            
            Assert.NotNull(response);
            Assert.AreEqual("sandbox", response.location);
        }
        [Test]
        public void TestAuthReversalWithAdditionalCofData()
        {
            authReversal reversal = new authReversal();
            reversal.cnpTxnId = 3;
            reversal.amount = 2;
            reversal.surchargeAmount = 1;
            reversal.payPalNotes = "note";
            reversal.reportGroup = "Planets";

            var additionalCOFData = new additionalCOFData();
            additionalCOFData.totalPaymentCount = "35";
            additionalCOFData.paymentType = paymentTypeEnum.Fixed_Amount;
            additionalCOFData.uniqueId = "12345wereew233";
            additionalCOFData.frequencyOfMIT = frequencyOfMITEnum.BiWeekly;
            additionalCOFData.validationReference = "re3298rhriw4wrw";
            additionalCOFData.sequenceIndicator = 2;

            reversal.additionalCOFData = additionalCOFData;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<surchargeAmount>1</surchargeAmount>\r\n<payPalNotes>note</payPalNotes>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><authReversalResponse><cnpTxnId>123</cnpTxnId><location>sandbox</location></authReversalResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var response = cnp.AuthReversal(reversal);

            Assert.NotNull(response);
            Assert.AreEqual("sandbox", response.location);
        }

    }
}
