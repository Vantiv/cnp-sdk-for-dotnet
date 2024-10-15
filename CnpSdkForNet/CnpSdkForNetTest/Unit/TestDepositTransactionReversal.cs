using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using System.Text.RegularExpressions;


namespace Cnp.Sdk.Test.Unit
{
    [TestFixture]
    class TestDepositTransactionReversal
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
        public void TestSurchargeAmount()
        {
            depositTransactionReversal reversal = new depositTransactionReversal();
            reversal.cnpTxnId = 3;
            reversal.amount = 2;
            reversal.surchargeAmount = 1;
            reversal.reportGroup = "Planets";

            var mock = new Mock<Communications>();
            if (config["encrypteOltpPayload"] == "true")
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<cnpOnlineRequest.*<encryptedPayload.*</encryptedPayload>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='12.16' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><depositTransactionReversalResponse><cnpTxnId>3</cnpTxnId></depositTransactionReversalResponse></cnpOnlineResponse>");
            }
            else
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<surchargeAmount>1</surchargeAmount>\r\n.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='12.16' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><depositTransactionReversalResponse><cnpTxnId>3</cnpTxnId></depositTransactionReversalResponse></cnpOnlineResponse>");
            }
            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.DepositTransactionReversal(reversal);
        }

        [Test]
        public void TestSurchargeAmount_Optional()
        {
            depositTransactionReversal reversal = new depositTransactionReversal();
            reversal.cnpTxnId = 3;
            reversal.amount = 2;
            reversal.reportGroup = "Planets";

            var mock = new Mock<Communications>();
            if (config["encrypteOltpPayload"] == "true")
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<cnpOnlineRequest.*<encryptedPayload.*</encryptedPayload>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='12.16' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><depositTransactionReversalResponse><cnpTxnId>3</cnpTxnId></depositTransactionReversalResponse></cnpOnlineResponse>");
            }
            else
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='12.16' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><depositTransactionReversalResponse><cnpTxnId>123</cnpTxnId></depositTransactionReversalResponse></cnpOnlineResponse>");
            }
            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.DepositTransactionReversal(reversal);
        }
        
        [Test]
        public void TestTransactionReversalWithLocation()
        {
            depositTransactionReversal reversal = new depositTransactionReversal();
            reversal.cnpTxnId = 3;
            reversal.amount = 2;
            reversal.surchargeAmount = 1;
            reversal.reportGroup = "Planets";

            var mock = new Mock<Communications>();
            if (config["encrypteOltpPayload"] == "true")
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<cnpOnlineRequest.*<encryptedPayload.*</encryptedPayload>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='12.16' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><depositTransactionReversalResponse><cnpTxnId>123</cnpTxnId><location>sandbox</location></depositTransactionReversalResponse></cnpOnlineResponse>");
            }
            else
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<surchargeAmount>1</surchargeAmount>\r\n.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='12.16' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><depositTransactionReversalResponse><cnpTxnId>123</cnpTxnId><location>sandbox</location></depositTransactionReversalResponse></cnpOnlineResponse>");
            }
            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var response = cnp.DepositTransactionReversal(reversal);
            
            Assert.NotNull(response);
            Assert.AreEqual("sandbox", response.location);
        }

        [Test]
        public void TestTransactionReversalWithPassengerTransportData()  //new testcase for 12.26
        {
            depositTransactionReversal reversal = new depositTransactionReversal();
            reversal.cnpTxnId = 3;
            reversal.amount = 2;
            reversal.surchargeAmount = 1;
            reversal.reportGroup = "Planets";

            var passengerTransportData = new passengerTransportData();
            passengerTransportData.passengerName = "Pia Jaiswal";
            passengerTransportData.ticketNumber = "TR0001";
            passengerTransportData.issuingCarrier = "IC";
            passengerTransportData.carrierName = "Indigo";
            passengerTransportData.restrictedTicketIndicator = "TI2022";
            passengerTransportData.numberOfAdults = 1;
            passengerTransportData.numberOfChildren = 1;
            passengerTransportData.customerCode = "C2011583";
            passengerTransportData.arrivalDate = new System.DateTime(2022, 12, 31);
            passengerTransportData.issueDate = new System.DateTime(2022, 12, 25);
            passengerTransportData.travelAgencyCode = "TAC12345";
            passengerTransportData.travelAgencyName = "Yatra";
            passengerTransportData.computerizedReservationSystem = computerizedReservationSystemEnum.STRT;
            passengerTransportData.creditReasonIndicator = creditReasonIndicatorEnum.A;
            passengerTransportData.ticketChangeIndicator = ticketChangeIndicatorEnum.C;
            passengerTransportData.ticketIssuerAddress = "Hinjewadi";
            passengerTransportData.exchangeTicketNumber = "ETN12345";
            passengerTransportData.exchangeAmount = 12300;
            passengerTransportData.exchangeFeeAmount = 11000;

            var tripLegData = new tripLegData();
            tripLegData.tripLegNumber = 12;
            tripLegData.departureCode = "DC";
            tripLegData.carrierCode = "CC";
            tripLegData.serviceClass = serviceClassEnum.First;
            tripLegData.stopOverCode = "N";
            tripLegData.destinationCode = "DC111";
            tripLegData.fareBasisCode = "FBC12345";
            tripLegData.departureDate = new System.DateTime(2023, 1, 31);
            tripLegData.originCity = "Pune";
            tripLegData.travelNumber = "TN111";
            tripLegData.departureTime = "13:05";
            tripLegData.arrivalTime = "16:10";
            tripLegData.remarks = "NA";
            passengerTransportData.tripLegData = tripLegData;
            reversal.passengerTransportData = passengerTransportData;

            var mock = new Mock<Communications>();
            if (config["encrypteOltpPayload"] == "true")
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<cnpOnlineRequest.*<encryptedPayload.*</encryptedPayload>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='12.16' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><depositTransactionReversalResponse><cnpTxnId>123</cnpTxnId><location>sandbox</location></depositTransactionReversalResponse></cnpOnlineResponse>");
            }
            else
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<ticketNumber>TR0001</ticketNumber>.*<issuingCarrier>IC</issuingCarrier>.*<carrierName>Indigo</carrierName>.*<restrictedTicketIndicator>TI2022</restrictedTicketIndicator>.*<numberOfAdults>1</numberOfAdults>.*<numberOfChildren>1</numberOfChildren>\r\n<customerCode>C2011583</customerCode>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='12.16' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><depositTransactionReversalResponse><cnpTxnId>123</cnpTxnId><location>sandbox</location></depositTransactionReversalResponse></cnpOnlineResponse>");

            }
            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var response = cnp.DepositTransactionReversal(reversal);

            Assert.NotNull(response);
            Assert.AreEqual("sandbox", response.location);
        }

        [Test]
        public void TestTransactionReversalWithShipmentIDSunscription()  //new testcase for 12.33
        {
            depositTransactionReversal reversal = new depositTransactionReversal();
            reversal.cnpTxnId = 3;
            reversal.amount = 2;
            reversal.surchargeAmount = 1;
            reversal.reportGroup = "Planets";

            enhancedData enhancedData = new enhancedData();
            enhancedData.lineItems = new List<lineItemData>();

            var mysubscription = new subscriptions();
            mysubscription.subscriptionId = "823";
            mysubscription.currentPeriod = 922;
            mysubscription.periodUnit = periodUnit.QUARTER;
            mysubscription.numberOfPeriods = 031;
            mysubscription.regularItemPrice = 769;
            mysubscription.nextDeliveryDate = new DateTime(2023, 7, 9);

            var mylineItemData = new lineItemData();
            mylineItemData.itemSequenceNumber = 8;
            mylineItemData.itemDescription = "Ecomm";
            mylineItemData.productCode = "El1331";
            mylineItemData.itemCategory = "Ele Ecomm";
            mylineItemData.itemSubCategory = "home appliaces";
            mylineItemData.productId = "1611";
            mylineItemData.productName = "dryer";
            mylineItemData.shipmentId = "2993";
            mylineItemData.subscription.Add(mysubscription);
            enhancedData.lineItems.Add(mylineItemData);
            reversal.enhancedData = enhancedData;

            var mock = new Mock<Communications>();
            if (config["encrypteOltpPayload"] == "true")
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<cnpOnlineRequest.*<encryptedPayload.*</encryptedPayload>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='12.33' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><depositTransactionReversalResponse><cnpTxnId>123</cnpTxnId><location>sandbox</location></depositTransactionReversalResponse></cnpOnlineResponse>");
            }
            else
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<enhancedData>.*<lineItemData>.*<itemSequenceNumber>8</itemSequenceNumber>.*<itemDescription>Ecomm</itemDescription>.*<productCode>El1331</productCode>.*<itemCategory>Ele Ecomm</itemCategory>.*<itemSubCategory>home appliaces</itemSubCategory>.*<productId>1611</productId>.*<productName>dryer</productName>.*<shipmentId>2993</shipmentId>.*<subscription>.*<subscriptionId>823</subscriptionId>.*<nextDeliveryDate>2023-07-09</nextDeliveryDate>.*<periodUnit>QUARTER</periodUnit>.*<numberOfPeriods>31</numberOfPeriods>.*<regularItemPrice>769</regularItemPrice>.*<currentPeriod>922</currentPeriod></subscription>.*</lineItemData>.*</enhancedData>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='12.33' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><depositTransactionReversalResponse><cnpTxnId>123</cnpTxnId><location>sandbox</location></depositTransactionReversalResponse></cnpOnlineResponse>");
            }
            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var response = cnp.DepositTransactionReversal(reversal);

            Assert.NotNull(response);
            Assert.AreEqual("sandbox", response.location);
        }
    }
}
