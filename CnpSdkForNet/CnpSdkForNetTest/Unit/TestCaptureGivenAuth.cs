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

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            cnp = new CnpOnline();
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

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<secondaryAmount>1</secondaryAmount>\r\n<orderSource>ecommerce</orderSource>.*", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureGivenAuthResponse><cnpTxnId>123</cnpTxnId></captureGivenAuthResponse></cnpOnlineResponse>");

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

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<surchargeAmount>1</surchargeAmount>\r\n<orderSource>ecommerce</orderSource>.*", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureGivenAuthResponse><cnpTxnId>123</cnpTxnId></captureGivenAuthResponse></cnpOnlineResponse>");

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

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<orderSource>ecommerce</orderSource>.*", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureGivenAuthResponse><cnpTxnId>123</cnpTxnId></captureGivenAuthResponse></cnpOnlineResponse>");

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

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*</merchantData>\r\n<debtRepayment>true</debtRepayment>\r\n</captureGivenAuth>.*", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.19' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureGivenAuthResponse><cnpTxnId>123</cnpTxnId></captureGivenAuthResponse></cnpOnlineResponse>");

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

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*</merchantData>\r\n<debtRepayment>false</debtRepayment>\r\n</captureGivenAuth>.*", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.19' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureGivenAuthResponse><cnpTxnId>123</cnpTxnId></captureGivenAuthResponse></cnpOnlineResponse>");

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

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*</merchantData>\r\n</captureGivenAuth>.*", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.19' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureGivenAuthResponse><cnpTxnId>123</cnpTxnId></captureGivenAuthResponse></cnpOnlineResponse>");

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

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<orderSource>ecommerce</orderSource>\r\n<processingType>initialRecurring</processingType>\r\n<originalNetworkTransactionId>abc123</originalNetworkTransactionId>\r\n<originalTransactionAmount>1234</originalTransactionAmount>.*", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureGivenAuthResponse><cnpTxnId>123</cnpTxnId></captureGivenAuthResponse></cnpOnlineResponse>");

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

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<orderSource>ecommerce</orderSource>\r\n<originalNetworkTransactionId>abc123</originalNetworkTransactionId>\r\n<originalTransactionAmount>1234</originalTransactionAmount>.*", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureGivenAuthResponse><cnpTxnId>123</cnpTxnId></captureGivenAuthResponse></cnpOnlineResponse>");

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

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<orderSource>ecommerce</orderSource>\r\n<merchantCategoryCode>0111</merchantCategoryCode>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureGivenAuthResponse><cnpTxnId>123</cnpTxnId></captureGivenAuthResponse></cnpOnlineResponse>");

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

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<secondaryAmount>1</secondaryAmount>\r\n<orderSource>ecommerce</orderSource>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureGivenAuthResponse><cnpTxnId>123</cnpTxnId><location>sandbox</location></captureGivenAuthResponse></cnpOnlineResponse>");

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

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<zip>01867-4456</zip>.*<email>mikasa@cnp.com</email>.*<sellerId>s1234</sellerId>.*<url>www.google.com</url>.*<frequencyOfMIT>BiWeekly</frequencyOfMIT>.*<crypto>False</crypto>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><captureGivenAuthResponse><cnpTxnId>123</cnpTxnId><location>sandbox</location></captureGivenAuthResponse></cnpOnlineResponse>");

            var mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var response = cnp.CaptureGivenAuth(capture);

            Assert.NotNull(response);
            Assert.AreEqual("sandbox", response.location);
        }
    }
}
