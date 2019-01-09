using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Cnp.Sdk;
using Moq;
using System.Text.RegularExpressions;


namespace Cnp.Sdk.Test.Unit
{
    public class TestForceCapture
    {
        
        private CnpOnline cnp = new CnpOnline();
        

        [Fact]
        public void TestSecondaryAmount()
        {
            forceCapture capture = new forceCapture();
            capture.amount = 2;
            capture.secondaryAmount = 1;
            capture.orderSource = orderSourceType.ecommerce;
            capture.reportGroup = "Planets";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<secondaryAmount>1</secondaryAmount>\r\n<orderSource>ecommerce</orderSource>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><forceCaptureResponse><cnpTxnId>123</cnpTxnId></forceCaptureResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.ForceCapture(capture);
        }

        [Fact]
        public void TestSurchargeAmount()
        {
            forceCapture capture = new forceCapture();
            capture.amount = 2;
            capture.surchargeAmount = 1;
            capture.orderSource = orderSourceType.ecommerce;
            capture.reportGroup = "Planets";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<surchargeAmount>1</surchargeAmount>\r\n<orderSource>ecommerce</orderSource>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><forceCaptureResponse><cnpTxnId>123</cnpTxnId></forceCaptureResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.ForceCapture(capture);
        }


        [Fact]
        public void TestSurchargeAmount_Optional()
        {
            forceCapture capture = new forceCapture();
            capture.amount = 2;
            capture.orderSource = orderSourceType.ecommerce;
            capture.reportGroup = "Planets";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<orderSource>ecommerce</orderSource>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><forceCaptureResponse><cnpTxnId>123</cnpTxnId></forceCaptureResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.ForceCapture(capture);
        }

        [Fact]
        public void TestDebtRepayment_True()
        {
            forceCapture forceCapture = new forceCapture();
            forceCapture.merchantData = new merchantDataType();
            forceCapture.debtRepayment = true;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*</merchantData>\r\n<debtRepayment>true</debtRepayment>\r\n</forceCapture>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<cnpOnlineResponse version='8.19' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><forceCaptureResponse><cnpTxnId>123</cnpTxnId></forceCaptureResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.ForceCapture(forceCapture);
        }

        [Fact]
        public void TestDebtRepayment_False()
        {
            forceCapture forceCapture = new forceCapture();
            forceCapture.merchantData = new merchantDataType();
            forceCapture.debtRepayment = false;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*</merchantData>\r\n<debtRepayment>false</debtRepayment>\r\n</forceCapture>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<cnpOnlineResponse version='8.19' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><forceCaptureResponse><cnpTxnId>123</cnpTxnId></forceCaptureResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.ForceCapture(forceCapture);
        }

        [Fact]
        public void TestDebtRepayment_Optional()
        {
            forceCapture forceCapture = new forceCapture();
            forceCapture.merchantData = new merchantDataType();

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*</merchantData>\r\n</forceCapture>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<cnpOnlineResponse version='8.19' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><forceCaptureResponse><cnpTxnId>123</cnpTxnId></forceCaptureResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.ForceCapture(forceCapture);
        }

        [Fact]
        public void TestProcessingType()
        {
            forceCapture capture = new forceCapture();
            capture.amount = 2;
            capture.orderSource = orderSourceType.ecommerce;
            capture.reportGroup = "Planets";
            capture.processingType = processingTypeEnum.initialRecurring;
            

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<orderSource>ecommerce</orderSource>\r\n<processingType>initialRecurring</processingType>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><forceCaptureResponse><cnpTxnId>123</cnpTxnId></forceCaptureResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.ForceCapture(capture);
        }

        [Fact]
        public void TestUndefinedProcessingType()
        {
            forceCapture capture = new forceCapture();
            capture.amount = 2;
            capture.orderSource = orderSourceType.ecommerce;
            capture.reportGroup = "Planets";
            capture.processingType = processingTypeEnum.undefined;


            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<orderSource>ecommerce</orderSource>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<cnpOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><forceCaptureResponse><cnpTxnId>123</cnpTxnId></forceCaptureResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.ForceCapture(capture);
        }
    }
}
