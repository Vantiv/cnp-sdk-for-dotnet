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

        [Test]
        public void TestTransactionReversalWithPassengerTransportData()  //new testcase for 12.26
        {
            refundTransactionReversal reversal = new refundTransactionReversal();
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

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<ticketNumber>TR0001</ticketNumber>.*<issuingCarrier>IC</issuingCarrier>.*<carrierName>Indigo</carrierName>.*<restrictedTicketIndicator>TI2022</restrictedTicketIndicator>.*<numberOfAdults>1</numberOfAdults>.*<numberOfChildren>1</numberOfChildren>\r\n<customerCode>C2011583</customerCode>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='12.16' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><refundTransactionReversalResponse><cnpTxnId>123</cnpTxnId><location>sandbox</location></refundTransactionReversalResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var response = cnp.RefundTransactionReversal(reversal);

            Assert.NotNull(response);
            Assert.AreEqual("sandbox", response.location);
        }

        [Test]
        public void TestTransactionReversalWithShipmentIDSunscription()  //new testcase for 12.33
        {
            refundTransactionReversal reversal = new refundTransactionReversal();
            reversal.cnpTxnId = 3;
            reversal.amount = 2;
            reversal.surchargeAmount = 1;
            reversal.reportGroup = "Planets";

            enhancedData enhancedData = new enhancedData();
            enhancedData.lineItems = new List<lineItemData>();

            var mysubscription = new subscriptions();
            mysubscription.subscriptionId = "803";
            mysubscription.currentPeriod = 922;
            mysubscription.periodUnit = periodUnit.WEEK;
            mysubscription.numberOfPeriods = 331;
            mysubscription.regularItemPrice = 700;
            mysubscription.nextDeliveryDate = new DateTime(2023, 9, 9);

            var mylineItemData = new lineItemData();
            mylineItemData.itemSequenceNumber = 8;
            mylineItemData.itemDescription = "Planets";
            mylineItemData.productCode = "A12234";
            mylineItemData.itemCategory = "Ele Bussi";
            mylineItemData.itemSubCategory = "home appliaces";
            mylineItemData.productId = "1601";
            mylineItemData.productName = "dryer";
            mylineItemData.shipmentId = "2003";
            mylineItemData.subscription.Add(mysubscription);
            enhancedData.lineItems.Add(mylineItemData);
            reversal.enhancedData = enhancedData;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<enhancedData>.*<lineItemData>.*<itemSequenceNumber>8</itemSequenceNumber>.*<itemDescription>Planets</itemDescription>.*<productCode>A12234</productCode>.*<itemCategory>Ele Bussi</itemCategory>.*<itemSubCategory>home appliaces</itemSubCategory>.*<productId>1601</productId>.*<productName>dryer</productName>.*<shipmentId>2003</shipmentId>.*<subscription>.*<subscriptionId>803</subscriptionId>.*<nextDeliveryDate>2023-09-09</nextDeliveryDate>.*<periodUnit>WEEK</periodUnit>.*<numberOfPeriods>331</numberOfPeriods>.*<regularItemPrice>700</regularItemPrice>.*<currentPeriod>922</currentPeriod></subscription>.*</lineItemData>.*</enhancedData>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='12.33' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><refundTransactionReversalResponse><cnpTxnId>123</cnpTxnId><location>sandbox</location></refundTransactionReversalResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var response = cnp.RefundTransactionReversal(reversal);

            Assert.NotNull(response);
            Assert.AreEqual("sandbox", response.location);
        }
    }
}
