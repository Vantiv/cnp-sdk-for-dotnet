using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using System.Text.RegularExpressions;


namespace Cnp.Sdk.Test.Unit
{
    [TestFixture]
    class TestCredit
    {
        
        private CnpOnline cnp;

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            cnp = new CnpOnline();
        }

        [Test]
        public void TestActionReasonOnOrphanedRefund()
        {
            credit credit = new credit();
            credit.orderId = "12344";
            credit.amount = 2;
            credit.orderSource = orderSourceType.ecommerce;
            credit.reportGroup = "Planets";
            credit.actionReason = "SUSPECT_FRAUD";
           
            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<actionReason>SUSPECT_FRAUD</actionReason>.*", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><creditResponse><cnpTxnId>123</cnpTxnId></creditResponse></cnpOnlineResponse>");
     
            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.Credit(credit);
        }

        [Test]
        public void TestOrderSource_Set()
        {
            credit credit = new credit();
            credit.orderId = "12344";
            credit.amount = 2;
            credit.orderSource = orderSourceType.ecommerce;
            credit.reportGroup = "Planets";
            // credit.pin = "1234";
            // .*<pin>1234</pin>

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<credit.*<amount>2</amount>.*<orderSource>ecommerce</orderSource>.*</credit>.*", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><creditResponse><cnpTxnId>123</cnpTxnId></creditResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.Credit(credit);
        }

        [Test]
        public void TestSecondaryAmount_Orphan()
        {
            credit credit = new credit();
            credit.amount = 2;
            credit.secondaryAmount = 1;
            credit.orderId = "3";
            credit.orderSource = orderSourceType.ecommerce;
            credit.reportGroup = "Planets";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<orderId>3</orderId>\r\n<amount>2</amount>\r\n<secondaryAmount>1</secondaryAmount>\r\n<orderSource>ecommerce</orderSource>.*", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><creditResponse><cnpTxnId>123</cnpTxnId></creditResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.Credit(credit);
        }

        [Test]
        public void TestSecondaryAmount_Tied()
        {
            credit credit = new credit();
            credit.amount = 2;
            credit.secondaryAmount = 1;
            credit.cnpTxnId = 3;
            credit.processingInstructions = new processingInstructions();
            credit.reportGroup = "Planets";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<cnpTxnId>3</cnpTxnId>\r\n<amount>2</amount>\r\n<secondaryAmount>1</secondaryAmount>\r\n<process.*", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><creditResponse><cnpTxnId>123</cnpTxnId></creditResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.Credit(credit);
        }

        [Test]
        public void TestSurchargeAmount_Tied()
        {
            credit credit = new credit();
            credit.amount = 2;
            credit.surchargeAmount = 1;
            credit.cnpTxnId = 3;
            credit.processingInstructions = new processingInstructions();
            credit.reportGroup = "Planets";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<cnpTxnId>3</cnpTxnId>\r\n<amount>2</amount>\r\n<surchargeAmount>1</surchargeAmount>\r\n<process.*", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><creditResponse><cnpTxnId>123</cnpTxnId></creditResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.Credit(credit);
        }

        [Test]
        public void TestSurchargeAmount_TiedOptional()
        {
            credit credit = new credit();
            credit.amount = 2;
            credit.cnpTxnId = 3;
            credit.reportGroup = "Planets";
            credit.processingInstructions = new processingInstructions();

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<cnpTxnId>3</cnpTxnId>\r\n<amount>2</amount>\r\n<processi.*", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><creditResponse><cnpTxnId>123</cnpTxnId></creditResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.Credit(credit);
        }

        [Test]
        public void TestSurchargeAmount_Orphan()
        {
            credit credit = new credit();
            credit.amount = 2;
            credit.surchargeAmount = 1;
            credit.orderId = "3";
            credit.orderSource = orderSourceType.ecommerce;
            credit.reportGroup = "Planets";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<orderId>3</orderId>\r\n<amount>2</amount>\r\n<surchargeAmount>1</surchargeAmount>\r\n<orderSource>ecommerce</orderSource>.*", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><creditResponse><cnpTxnId>123</cnpTxnId></creditResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.Credit(credit);
        }

        [Test]
        public void TestSurchargeAmount_OrphanOptional()
        {
            credit credit = new credit();
            credit.amount = 2;
            credit.orderId = "3";
            credit.orderSource = orderSourceType.ecommerce;
            credit.reportGroup = "Planets";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<orderId>3</orderId>\r\n<amount>2</amount>\r\n<orderSource>ecommerce</orderSource>.*", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><creditResponse><cnpTxnId>123</cnpTxnId></creditResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.Credit(credit);
        }

        [Test]
        public void TestPos_Tied()
        {
            credit credit = new credit();
            credit.amount = 2;
            credit.pos = new pos();
            credit.pos.terminalId = "abc123";
            credit.cnpTxnId = 3;
            credit.reportGroup = "Planets";
            credit.payPalNotes = "notes";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<cnpTxnId>3</cnpTxnId>\r\n<amount>2</amount>\r\n<pos>\r\n<terminalId>abc123</terminalId></pos>\r\n<payPalNotes>.*", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><creditResponse><cnpTxnId>123</cnpTxnId></creditResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.Credit(credit);
        }

        [Test]
        public void TestPos_TiedOptional()
        {
            credit credit = new credit();
            credit.amount = 2;
            credit.cnpTxnId = 3;
            credit.reportGroup = "Planets";
            credit.payPalNotes = "notes";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<cnpTxnId>3</cnpTxnId>\r\n<amount>2</amount>\r\n<payPalNotes>.*", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><creditResponse><cnpTxnId>123</cnpTxnId></creditResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.Credit(credit);
        }

        [Test]
        public void TestCreditWithPin()
        {
            credit credit = new credit();
            credit.id = "1";
            credit.reportGroup = "planets";
            credit.cnpTxnId = 123456000;
            credit.pin = "1234";
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000001";
            card.expDate = "1210";
            credit.card = card;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<credit.*<pin>1234</pin>.*</credit>.*", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><creditResponse><cnpTxnId>123</cnpTxnId></creditResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.Credit(credit);
        }

        [Test]
        public void TestCreditWithMCC()
        {
            credit credit = new credit();
            credit.amount = 2;
            credit.merchantCategoryCode = "0111";
            credit.orderId = "3";
            credit.orderSource = orderSourceType.ecommerce;
            credit.reportGroup = "Planets";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<orderId>3</orderId>\r\n<amount>2</amount>\r\n<orderSource>ecommerce</orderSource>\r\n<merchantCategoryCode>0111</merchantCategoryCode>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><creditResponse><cnpTxnId>123</cnpTxnId></creditResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.Credit(credit);
        }
        
        [Test]
        public void TestCreditWithLocation()
        {
            credit credit = new credit();
            credit.orderId = "12344";
            credit.amount = 2;
            credit.orderSource = orderSourceType.ecommerce;
            credit.reportGroup = "Planets";
            credit.actionReason = "SUSPECT_FRAUD";
           
            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<actionReason>SUSPECT_FRAUD</actionReason>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><creditResponse><cnpTxnId>123</cnpTxnId><location>sandbox</location></creditResponse></cnpOnlineResponse>");
     
            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var response = cnp.Credit(credit);
            
            Assert.NotNull(response);
            Assert.AreEqual("sandbox", response.location);
        }
        [Test]
        public void TestCreditWithAdditionalCofData()
        {
            credit credit = new credit();
            credit.orderId = "12344";
            credit.amount = 2;
            credit.orderSource = orderSourceType.ecommerce;
            credit.reportGroup = "Planets";
            credit.actionReason = "SUSPECT_FRAUD";

            var additionalCOFData = new additionalCOFData();
            additionalCOFData.totalPaymentCount = "35";
            additionalCOFData.paymentType = paymentTypeEnum.Fixed_Amount;
            additionalCOFData.uniqueId = "12345wereew233";
            additionalCOFData.frequencyOfMIT = frequencyOfMITEnum.BiWeekly;
            additionalCOFData.validationReference = "re3298rhriw4wrw";
            additionalCOFData.sequenceIndicator = 2;

            credit.additionalCOFData = additionalCOFData;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<actionReason>SUSPECT_FRAUD</actionReason>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><creditResponse><cnpTxnId>123</cnpTxnId><location>sandbox</location></creditResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var response = cnp.Credit(credit);

            Assert.NotNull(response);
            Assert.AreEqual("sandbox", response.location);
        }

        [Test]
        public void TestCreditWithPassengerTransportData() //new testcase for 12.26
        {
            credit credit = new credit();
            credit.orderId = "12344";
            credit.amount = 2;
            credit.orderSource = orderSourceType.ecommerce;
            credit.reportGroup = "Planets";
            credit.actionReason = "SUSPECT_FRAUD";

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
            credit.passengerTransportData = passengerTransportData;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<ticketNumber>TR0001</ticketNumber>.*<issuingCarrier>IC</issuingCarrier>.*<carrierName>Indigo</carrierName>.*<restrictedTicketIndicator>TI2022</restrictedTicketIndicator>.*<numberOfAdults>1</numberOfAdults>.*<numberOfChildren>1</numberOfChildren>\r\n<customerCode>C2011583</customerCode>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><creditResponse><cnpTxnId>123</cnpTxnId><location>sandbox</location></creditResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var response = cnp.Credit(credit);

            Assert.NotNull(response);
            Assert.AreEqual("sandbox", response.location);
        }

        [Test]
        public void TestCreditWithShipmentIdAndSubscription()
        {
            credit credit = new credit();
            credit.id = "1";
            credit.reportGroup = "planets";
            credit.cnpTxnId = 123456000;
            credit.pin = "1234";
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000001";
            card.expDate = "1210";
            credit.card = card;

            enhancedData enhancedData = new enhancedData();
            enhancedData.lineItems = new List<lineItemData>();
            
            var mysubscription = new subscriptions();
            mysubscription.subscriptionId = "123";
            mysubscription.currentPeriod = 114;
            mysubscription.periodUnit = periodUnit.YEAR;
            mysubscription.numberOfPeriods = 123;
            mysubscription.regularItemPrice = 69;
            mysubscription.nextDeliveryDate = new DateTime(2017, 1, 1);

            var mylineItemData = new lineItemData();
            mylineItemData.itemSequenceNumber = 1;
            mylineItemData.itemDescription = "Electronics";
            mylineItemData.productCode = "El03";
            mylineItemData.itemCategory = "E Appiances";
            mylineItemData.itemSubCategory = "appliaces";
            mylineItemData.productId = "1023";
            mylineItemData.productName = "dyer";
            mylineItemData.shipmentId = "2124";
            mylineItemData.subscription.Add(mysubscription);
            enhancedData.lineItems.Add(mylineItemData);
            credit.enhancedData = enhancedData;



            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<enhancedData>.*<lineItemData>.*<itemSequenceNumber>1</itemSequenceNumber>.*<itemDescription>Electronics</itemDescription>.*<productCode>El03</productCode>.*<itemCategory>E Appiances</itemCategory>.*<itemSubCategory>appliaces</itemSubCategory>.*<productId>1023</productId>.*<productName>dyer</productName>.*<shipmentId>2124</shipmentId>.*<subscription>.*<subscriptionId>123</subscriptionId>.*<nextDeliveryDate>2017-01-01</nextDeliveryDate>.*<periodUnit>YEAR</periodUnit>.*<numberOfPeriods>123</numberOfPeriods>.*<regularItemPrice>69</regularItemPrice>.*<currentPeriod>114</currentPeriod></subscription>.*</lineItemData></enhancedData>", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='12.33' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><creditResponse><cnpTxnId>123</cnpTxnId></creditResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.Credit(credit);
        }
    }
}
