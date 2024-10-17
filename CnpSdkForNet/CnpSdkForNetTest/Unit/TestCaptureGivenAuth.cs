using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using System.Text.RegularExpressions;


namespace Cnp.Sdk.Test.Unit
{
    [TestFixture]
    class TestCaptureGivenAuth
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
        public void TestSecondaryAmount()
        {
            captureGivenAuth capture = new captureGivenAuth();
            capture.amount = 2;
            capture.secondaryAmount = 1;
            capture.orderSource = orderSourceType.ecommerce;
            capture.reportGroup = "Planets";

            var mock = new Mock<Communications>();
            if (config["encryptOltpPayload"] == "true")
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<cnpOnlineRequest.*<encryptedPayload.*</encryptedPayload>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureGivenAuthResponse><cnpTxnId>123</cnpTxnId></captureGivenAuthResponse></cnpOnlineResponse>");
            }
            else
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<secondaryAmount>1</secondaryAmount>\r\n<orderSource>ecommerce</orderSource>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureGivenAuthResponse><cnpTxnId>123</cnpTxnId></captureGivenAuthResponse></cnpOnlineResponse>");
            }
            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.CaptureGivenAuth(capture);
        }

        [Test]
        public void TestSurchargeAmount()
        {
            captureGivenAuth capture = new captureGivenAuth();
            capture.amount = 2;
            capture.surchargeAmount = 1;
            capture.orderSource = orderSourceType.ecommerce;
            capture.reportGroup = "Planets";

            var mock = new Mock<Communications>();
            if (config["encryptOltpPayload"] == "true")
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<cnpOnlineRequest.*<encryptedPayload.*</encryptedPayload>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureGivenAuthResponse><cnpTxnId>123</cnpTxnId></captureGivenAuthResponse></cnpOnlineResponse>");
            }
            else
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<surchargeAmount>1</surchargeAmount>\r\n<orderSource>ecommerce</orderSource>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureGivenAuthResponse><cnpTxnId>123</cnpTxnId></captureGivenAuthResponse></cnpOnlineResponse>");
            }
            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.CaptureGivenAuth(capture);
        }

        [Test]
        public void TestSurchargeAmount_Optional()
        {
            captureGivenAuth capture = new captureGivenAuth();
            capture.amount = 2;
            capture.orderSource = orderSourceType.ecommerce;
            capture.reportGroup = "Planets";

            var mock = new Mock<Communications>();
            if (config["encryptOltpPayload"] == "true")
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<cnpOnlineRequest.*<encryptedPayload.*</encryptedPayload>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureGivenAuthResponse><cnpTxnId>123</cnpTxnId></captureGivenAuthResponse></cnpOnlineResponse>");
            }
            else
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<orderSource>ecommerce</orderSource>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureGivenAuthResponse><cnpTxnId>123</cnpTxnId></captureGivenAuthResponse></cnpOnlineResponse>");
            }
            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.CaptureGivenAuth(capture);
        }

        [Test]
        public void TestDebtRepayment_True()
        {
            captureGivenAuth captureGivenAuth = new captureGivenAuth();
            captureGivenAuth.merchantData = new merchantDataType();
            captureGivenAuth.debtRepayment = true;

            var mock = new Mock<Communications>();
            if (config["encryptOltpPayload"] == "true")
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<cnpOnlineRequest.*<encryptedPayload.*</encryptedPayload>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureGivenAuthResponse><cnpTxnId>123</cnpTxnId></captureGivenAuthResponse></cnpOnlineResponse>");
            }
            else
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*</merchantData>\r\n<debtRepayment>true</debtRepayment>\r\n</captureGivenAuth>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='8.19' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureGivenAuthResponse><cnpTxnId>123</cnpTxnId></captureGivenAuthResponse></cnpOnlineResponse>");
            }
            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.CaptureGivenAuth(captureGivenAuth);
        }

        [Test]
        public void TestDebtRepayment_False()
        {
            captureGivenAuth captureGivenAuth = new captureGivenAuth();
            captureGivenAuth.merchantData = new merchantDataType();
            captureGivenAuth.debtRepayment = false;

            var mock = new Mock<Communications>();
            if (config["encryptOltpPayload"] == "true")
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<cnpOnlineRequest.*<encryptedPayload.*</encryptedPayload>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureGivenAuthResponse><cnpTxnId>123</cnpTxnId></captureGivenAuthResponse></cnpOnlineResponse>");
            }
            else
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*</merchantData>\r\n<debtRepayment>false</debtRepayment>\r\n</captureGivenAuth>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='8.19' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureGivenAuthResponse><cnpTxnId>123</cnpTxnId></captureGivenAuthResponse></cnpOnlineResponse>");
            }
            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.CaptureGivenAuth(captureGivenAuth);
        }

        [Test]
        public void TestDebtRepayment_Optional()
        {
            captureGivenAuth captureGivenAuth = new captureGivenAuth();
            captureGivenAuth.merchantData = new merchantDataType();

            var mock = new Mock<Communications>();
            if (config["encryptOltpPayload"] == "true")
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<cnpOnlineRequest.*<encryptedPayload.*</encryptedPayload>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureGivenAuthResponse><cnpTxnId>123</cnpTxnId></captureGivenAuthResponse></cnpOnlineResponse>");
            }
            else
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*</merchantData>\r\n</captureGivenAuth>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='8.19' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureGivenAuthResponse><cnpTxnId>123</cnpTxnId></captureGivenAuthResponse></cnpOnlineResponse>");
            }
            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.CaptureGivenAuth(captureGivenAuth);
        }
        [Test]
        public void TestProcessingType()
        {
            captureGivenAuth capture = new captureGivenAuth();
            capture.amount = 2;
            capture.orderSource = orderSourceType.ecommerce;
            capture.reportGroup = "Planets";
            capture.processingType = processingType.initialRecurring;
            capture.originalNetworkTransactionId = "abc123";
            capture.originalTransactionAmount = 1234;

            var mock = new Mock<Communications>();
            if (config["encryptOltpPayload"] == "true")
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<cnpOnlineRequest.*<encryptedPayload.*</encryptedPayload>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureGivenAuthResponse><cnpTxnId>123</cnpTxnId></captureGivenAuthResponse></cnpOnlineResponse>");
            }
            else
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<orderSource>ecommerce</orderSource>\r\n<processingType>initialRecurring</processingType>\r\n<originalNetworkTransactionId>abc123</originalNetworkTransactionId>\r\n<originalTransactionAmount>1234</originalTransactionAmount>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureGivenAuthResponse><cnpTxnId>123</cnpTxnId></captureGivenAuthResponse></cnpOnlineResponse>");
            }
            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.CaptureGivenAuth(capture);
        }

        [Test]
        public void TestUndefinedProcessingType()
        {
            captureGivenAuth capture = new captureGivenAuth();
            capture.amount = 2;
            capture.orderSource = orderSourceType.ecommerce;
            capture.reportGroup = "Planets";
            capture.processingType = processingType.undefined;
            capture.originalNetworkTransactionId = "abc123";
            capture.originalTransactionAmount = 1234;

            var mock = new Mock<Communications>();
            if (config["encryptOltpPayload"] == "true")
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<cnpOnlineRequest.*<encryptedPayload.*</encryptedPayload>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureGivenAuthResponse><cnpTxnId>123</cnpTxnId></captureGivenAuthResponse></cnpOnlineResponse>");
            }
            else
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<orderSource>ecommerce</orderSource>\r\n<originalNetworkTransactionId>abc123</originalNetworkTransactionId>\r\n<originalTransactionAmount>1234</originalTransactionAmount>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureGivenAuthResponse><cnpTxnId>123</cnpTxnId></captureGivenAuthResponse></cnpOnlineResponse>");
            }
            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.CaptureGivenAuth(capture);
        }

        [Test]
        public void TestCaptureAuthWithMCC()
        {
            captureGivenAuth capture = new captureGivenAuth();
            capture.amount = 2;
            capture.merchantCategoryCode = "0111";
            capture.orderSource = orderSourceType.ecommerce;
            capture.reportGroup = "Planets";

            var mock = new Mock<Communications>();
            if (config["encryptOltpPayload"] == "true")
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<cnpOnlineRequest.*<encryptedPayload.*</encryptedPayload>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureGivenAuthResponse><cnpTxnId>123</cnpTxnId></captureGivenAuthResponse></cnpOnlineResponse>");
            }
            else
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<orderSource>ecommerce</orderSource>\r\n<merchantCategoryCode>0111</merchantCategoryCode>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureGivenAuthResponse><cnpTxnId>123</cnpTxnId></captureGivenAuthResponse></cnpOnlineResponse>");
            }
            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.CaptureGivenAuth(capture);
        }
        
        [Test]
        public void TestCaptureGivenAuthWithLocation()
        {
            captureGivenAuth capture = new captureGivenAuth();
            capture.amount = 2;
            capture.secondaryAmount = 1;
            capture.orderSource = orderSourceType.ecommerce;
            capture.reportGroup = "Planets";

            var mock = new Mock<Communications>();
            if (config["encryptOltpPayload"] == "true")
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<cnpOnlineRequest.*<encryptedPayload.*</encryptedPayload>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureGivenAuthResponse><cnpTxnId>123</cnpTxnId><location>sandbox</location></captureGivenAuthResponse></cnpOnlineResponse>");
            }
            else
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<secondaryAmount>1</secondaryAmount>\r\n<orderSource>ecommerce</orderSource>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureGivenAuthResponse><cnpTxnId>123</cnpTxnId><location>sandbox</location></captureGivenAuthResponse></cnpOnlineResponse>");
            }
            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var response = cnp.CaptureGivenAuth(capture);
            
            Assert.NotNull(response);
            Assert.AreEqual("sandbox", response.location);
        }

        [Test]
        public void TestSimpleCaptureGivenAuthWithRetailerAddressAndAdditionalCOFdata() ///new testcase 12.24
        {
            captureGivenAuth capture = new captureGivenAuth();
            capture.orderId = "12344";
            capture.amount = 2;
            capture.orderSource = orderSourceType.ecommerce;
            capture.reportGroup = "Planets";
            capture.id = "thisisid";
            capture.businessIndicator = businessIndicatorEnum.fundTransfer;
            capture.crypto = false;

            var retailerAddress = new contact();
            retailerAddress.name = "Mikasa Ackerman";
            retailerAddress.addressLine1 = "1st Main Street";
            retailerAddress.city = "Burlington";
            retailerAddress.state = "MA";
            retailerAddress.country = countryTypeEnum.USA;
            retailerAddress.email = "mikasa@cnp.com";
            retailerAddress.zip = "01867-4456";
            retailerAddress.sellerId = "s1234";
            retailerAddress.url = "www.google.com";
            capture.retailerAddress = retailerAddress;

            var additionalCOFData = new additionalCOFData();
            additionalCOFData.totalPaymentCount = "35";
            additionalCOFData.paymentType = paymentTypeEnum.Fixed_Amount;
            additionalCOFData.uniqueId = "12345wereew233";
            additionalCOFData.frequencyOfMIT = frequencyOfMITEnum.BiWeekly;
            additionalCOFData.validationReference = "re3298rhriw4wrw";
            additionalCOFData.sequenceIndicator = 2;

            capture.additionalCOFData = additionalCOFData;

            var mock = new Mock<Communications>();
            if (config["encryptOltpPayload"] == "true")
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<cnpOnlineRequest.*<encryptedPayload.*</encryptedPayload>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureGivenAuthResponse><cnpTxnId>123</cnpTxnId><location>sandbox</location></captureGivenAuthResponse></cnpOnlineResponse>");
            }
            else
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<zip>01867-4456</zip>.*<email>mikasa@cnp.com</email>.*<sellerId>s1234</sellerId>.*<url>www.google.com</url>.*<frequencyOfMIT>BiWeekly</frequencyOfMIT>.*<crypto>false</crypto>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureGivenAuthResponse><cnpTxnId>123</cnpTxnId><location>sandbox</location></captureGivenAuthResponse></cnpOnlineResponse>");
            }
            var mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var response = cnp.CaptureGivenAuth(capture);

            Assert.NotNull(response);
            Assert.AreEqual("sandbox", response.location);
        }


        [Test]
        public void TestSimpleCaptureGivenAuthWithPassengerTransportData() //new testcase for 12.26
        {
            captureGivenAuth capture = new captureGivenAuth();
            capture.orderId = "12344";
            capture.amount = 2;
            capture.orderSource = orderSourceType.ecommerce;
            capture.reportGroup = "Planets";
            capture.id = "thisisid";
            capture.businessIndicator = businessIndicatorEnum.fundTransfer;
            capture.crypto = false;

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
            capture.passengerTransportData = passengerTransportData;

            var mock = new Mock<Communications>();
            if (config["encryptOltpPayload"] == "true")
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<cnpOnlineRequest.*<encryptedPayload.*</encryptedPayload>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureGivenAuthResponse><cnpTxnId>123</cnpTxnId><location>sandbox</location></captureGivenAuthResponse></cnpOnlineResponse>");
            }
            else
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<ticketNumber>TR0001</ticketNumber>.*<issuingCarrier>IC</issuingCarrier>.*<carrierName>Indigo</carrierName>.*<restrictedTicketIndicator>TI2022</restrictedTicketIndicator>.*<numberOfAdults>1</numberOfAdults>.*<numberOfChildren>1</numberOfChildren>\r\n<customerCode>C2011583</customerCode>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureGivenAuthResponse><cnpTxnId>123</cnpTxnId><location>sandbox</location></captureGivenAuthResponse></cnpOnlineResponse>");
            }
            var mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var response = cnp.CaptureGivenAuth(capture);

            Assert.NotNull(response);
            Assert.AreEqual("sandbox", response.location);
        }

        [Test]
        public void TestSimpleCaptureGivenAuthWithForeignRetailerIndicator()///12.31
        {
            captureGivenAuth capture = new captureGivenAuth();
            capture.orderId = "12344";
            capture.amount = 2;
            capture.orderSource = orderSourceType.ecommerce;
            capture.reportGroup = "Planets";
            capture.id = "thisisid";
            capture.businessIndicator = businessIndicatorEnum.fundTransfer;
            capture.crypto = false;

            var passengerTransportData = new passengerTransportData();
            passengerTransportData.passengerName = "Mark Brook";
            passengerTransportData.ticketNumber = "TR0001";
            passengerTransportData.issuingCarrier = "IC";
            passengerTransportData.carrierName = "American Airways";
            passengerTransportData.restrictedTicketIndicator = "TI2022";
            passengerTransportData.numberOfAdults = 1;
            passengerTransportData.numberOfChildren = 1;
            passengerTransportData.customerCode = "C2011583";
            passengerTransportData.arrivalDate = new System.DateTime(2022, 12, 31);
            passengerTransportData.issueDate = new System.DateTime(2022, 12, 25);
            passengerTransportData.travelAgencyCode = "TAC12345";
            passengerTransportData.travelAgencyName = "Travel";
            passengerTransportData.computerizedReservationSystem = computerizedReservationSystemEnum.STRT;
            passengerTransportData.creditReasonIndicator = creditReasonIndicatorEnum.A;
            passengerTransportData.ticketChangeIndicator = ticketChangeIndicatorEnum.C;
            passengerTransportData.ticketIssuerAddress = "High street";
            passengerTransportData.exchangeTicketNumber = "ETN12345";
            passengerTransportData.exchangeAmount = 12300;
            passengerTransportData.exchangeFeeAmount = 11000;

            capture.passengerTransportData = passengerTransportData;
            capture.foreignRetailerIndicator = foreignRetailerIndicatorEnum.F;

            var mock = new Mock<Communications>();
            if (config["encryptOltpPayload"] == "true")
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<cnpOnlineRequest.*<encryptedPayload.*</encryptedPayload>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='12.31' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureGivenAuthResponse><cnpTxnId>123</cnpTxnId><location>sandbox</location></captureGivenAuthResponse></cnpOnlineResponse>");
            }
            else
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<ticketNumber>TR0001</ticketNumber>.*<issuingCarrier>IC</issuingCarrier>.*<carrierName>American Airways</carrierName>.*<restrictedTicketIndicator>TI2022</restrictedTicketIndicator>.*<numberOfAdults>1</numberOfAdults>.*<numberOfChildren>1</numberOfChildren>\r\n<customerCode>C2011583</customerCode>.*<foreignRetailerIndicator>F</foreignRetailerIndicator>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='12.31' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureGivenAuthResponse><cnpTxnId>123</cnpTxnId><location>sandbox</location></captureGivenAuthResponse></cnpOnlineResponse>");
            }

            var mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var response = cnp.CaptureGivenAuth(capture);

            Assert.NotNull(response);
            Assert.AreEqual("sandbox", response.location);
        }

        [Test]
        public void TestSimpleCaptureGivenAuthWithShipmentIDSubscription()///12.33
        {
            captureGivenAuth capture = new captureGivenAuth();
            capture.orderId = "12344";
            capture.amount = 2;
            capture.orderSource = orderSourceType.ecommerce;
            capture.reportGroup = "Planets";
            capture.id = "thisisid";
            capture.businessIndicator = businessIndicatorEnum.fundTransfer;
            capture.crypto = false;
            enhancedData enhancedData = new enhancedData();
            enhancedData.lineItems = new List<lineItemData>();

            var mysubscription = new subscriptions();
            mysubscription.subscriptionId = "123";
            mysubscription.currentPeriod = 112;
            mysubscription.periodUnit = periodUnit.WEEK;
            mysubscription.numberOfPeriods = 131;
            mysubscription.regularItemPrice = 169;
            mysubscription.nextDeliveryDate = new DateTime(2023, 3, 2);

            var mylineItemData = new lineItemData();
            mylineItemData.itemSequenceNumber = 1;
            mylineItemData.itemDescription = "Ecomm";
            mylineItemData.productCode = "El11";
            mylineItemData.itemCategory = "Ele Appiances";
            mylineItemData.itemSubCategory = "home appliaces";
            mylineItemData.productId = "1111";
            mylineItemData.productName = "dryer";
            mylineItemData.shipmentId = "2593";
            mylineItemData.subscription.Add(mysubscription);
            enhancedData.lineItems.Add(mylineItemData);
         
            capture.enhancedData=enhancedData;
            capture.foreignRetailerIndicator = foreignRetailerIndicatorEnum.F;

            var mock = new Mock<Communications>();
            if (config["encryptOltpPayload"] == "true")
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<cnpOnlineRequest.*<encryptedPayload.*</encryptedPayload>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='12.33' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureGivenAuthResponse><cnpTxnId>123</cnpTxnId><location>sandbox</location></captureGivenAuthResponse></cnpOnlineResponse>");
            }
            else
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<enhancedData>.*<lineItemData>.*<itemSequenceNumber>1</itemSequenceNumber>.*<itemDescription>Ecomm</itemDescription>.*<productCode>El11</productCode>.*<itemCategory>Ele Appiances</itemCategory>.*<itemSubCategory>home appliaces</itemSubCategory>.*<productId>1111</productId>.*<productName>dryer</productName>.*<shipmentId>2593</shipmentId>.*<subscription>.*<subscriptionId>123</subscriptionId>.*<nextDeliveryDate>2023-03-02</nextDeliveryDate>.*<periodUnit>WEEK</periodUnit>.*<numberOfPeriods>131</numberOfPeriods>.*<regularItemPrice>169</regularItemPrice>.*<currentPeriod>112</currentPeriod></subscription>.*</lineItemData>.*</enhancedData>.*<foreignRetailerIndicator>F</foreignRetailerIndicator>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='12.33' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureGivenAuthResponse><cnpTxnId>123</cnpTxnId><location>sandbox</location></captureGivenAuthResponse></cnpOnlineResponse>");
            }
            var mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var response = cnp.CaptureGivenAuth(capture);

            Assert.NotNull(response);
            Assert.AreEqual("sandbox", response.location);
        }
    }
}
