using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Text.RegularExpressions;

namespace Cnp.Sdk.Test.Unit
{
    
    public class TestAdvancedFraudCheck
    {

        private CnpOnline cnp = new CnpOnline();
        

        [Fact]
        public void TestNoCustomAttributes()
        {
            fraudCheck fraudCheck = new fraudCheck();
            advancedFraudChecksType advancedFraudCheck = new advancedFraudChecksType();
            fraudCheck.advancedFraudChecks = advancedFraudCheck;
            advancedFraudCheck.threatMetrixSessionId = "123";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<threatMetrixSessionId>123</threatMetrixSessionId>\r\n.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<cnpOnlineResponse version='10.1' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><fraudCheckResponse id='127' reportGroup='Planets' customerId=''><cnpTxnId>742802348034313000</cnpTxnId><response>000</response><message>Approved</message><advancedFraudResults><deviceReviewStatus>pass</deviceReviewStatus><deviceReputationScore>42</deviceReputationScore><triggeredRule>triggered_rule_default</triggeredRule></advancedFraudResults></fraudCheckResponse></cnpOnlineResponse >");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            fraudCheckResponse fraudCheckResponse = cnp.FraudCheck(fraudCheck);

            Assert.NotNull(fraudCheckResponse);
            Assert.Equal("pass", fraudCheckResponse.advancedFraudResults.deviceReviewStatus);
        }

        [Fact]
        public void TestCustomAttribute1()
        {
            fraudCheck fraudCheck = new fraudCheck();
            advancedFraudChecksType advancedFraudCheck = new advancedFraudChecksType();
            fraudCheck.advancedFraudChecks = advancedFraudCheck;
            advancedFraudCheck.threatMetrixSessionId = "123";
            advancedFraudCheck.customAttribute1 = "abc";

            var mock = new Mock<Communications>();
            
            mock.Setup(Communications => Communications.HttpPost(It.IsRegex("..*<threatMetrixSessionId>123</threatMetrixSessionId>\r\n<customAttribute1>abc</customAttribute1>\r\n.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<cnpOnlineResponse version='10.1' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><fraudCheckResponse id='127' reportGroup='Planets' customerId=''><cnpTxnId>742802348034313000</cnpTxnId><response>000</response><message>Approved</message><advancedFraudResults><deviceReviewStatus>pass</deviceReviewStatus><deviceReputationScore>42</deviceReputationScore><triggeredRule>triggered_rule_default</triggeredRule></advancedFraudResults></fraudCheckResponse></cnpOnlineResponse >");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            fraudCheckResponse fraudCheckResponse = cnp.FraudCheck(fraudCheck);

            Assert.NotNull(fraudCheckResponse);
            Assert.Equal(42, fraudCheckResponse.advancedFraudResults.deviceReputationScore);
        }

        [Fact]
        public void TestCustomAttribute2()
        {
            fraudCheck fraudCheck = new fraudCheck();
            advancedFraudChecksType advancedFraudCheck = new advancedFraudChecksType();
            fraudCheck.advancedFraudChecks = advancedFraudCheck;
            advancedFraudCheck.threatMetrixSessionId = "123";
            advancedFraudCheck.customAttribute1 = "abc";
            advancedFraudCheck.customAttribute2 = "def";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex("..*<threatMetrixSessionId>123</threatMetrixSessionId>\r\n<customAttribute1>abc</customAttribute1>\r\n<customAttribute2>def</customAttribute2>\r\n.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<cnpOnlineResponse version='10.1' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><fraudCheckResponse id='127' reportGroup='Planets' customerId=''><cnpTxnId>742802348034313000</cnpTxnId><response>000</response><message>Approved</message><advancedFraudResults><deviceReviewStatus>pass</deviceReviewStatus><deviceReputationScore>42</deviceReputationScore><triggeredRule>triggered_rule_default</triggeredRule></advancedFraudResults></fraudCheckResponse></cnpOnlineResponse >");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            fraudCheckResponse fraudCheckResponse = cnp.FraudCheck(fraudCheck);

            Assert.NotNull(fraudCheckResponse);
            Assert.Equal("triggered_rule_default", fraudCheckResponse.advancedFraudResults.triggeredRule[0]);
        }

        [Fact]
        public void TestCustomAttribute3()
        {
            fraudCheck fraudCheck = new fraudCheck();
            advancedFraudChecksType advancedFraudCheck = new advancedFraudChecksType();
            fraudCheck.advancedFraudChecks = advancedFraudCheck;
            advancedFraudCheck.threatMetrixSessionId = "123";
            advancedFraudCheck.customAttribute1 = "abc";
            advancedFraudCheck.customAttribute2 = "def";
            advancedFraudCheck.customAttribute3 = "ghi";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex("..*<threatMetrixSessionId>123</threatMetrixSessionId>\r\n<customAttribute1>abc</customAttribute1>\r\n<customAttribute2>def</customAttribute2>\r\n<customAttribute3>ghi</customAttribute3>\r\n.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<cnpOnlineResponse version='10.1' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><fraudCheckResponse id='127' reportGroup='Planets' customerId=''><cnpTxnId>742802348034313000</cnpTxnId><response>000</response><message>Approved</message><advancedFraudResults><deviceReviewStatus>pass</deviceReviewStatus><deviceReputationScore>42</deviceReputationScore><triggeredRule>triggered_rule_default</triggeredRule></advancedFraudResults></fraudCheckResponse></cnpOnlineResponse >");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            fraudCheckResponse fraudCheckResponse = cnp.FraudCheck(fraudCheck);

            Assert.NotNull(fraudCheckResponse);
            Assert.Equal("Approved", fraudCheckResponse.message);
        }

        [Fact]
        public void TestCustomAttribute4()
        {
            fraudCheck fraudCheck = new fraudCheck();
            advancedFraudChecksType advancedFraudCheck = new advancedFraudChecksType();
            fraudCheck.advancedFraudChecks = advancedFraudCheck;
            advancedFraudCheck.threatMetrixSessionId = "123";
            advancedFraudCheck.customAttribute1 = "abc";
            advancedFraudCheck.customAttribute2 = "def";
            advancedFraudCheck.customAttribute3 = "ghi";
            advancedFraudCheck.customAttribute4 = "jkl";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex("..*<threatMetrixSessionId>123</threatMetrixSessionId>\r\n<customAttribute1>abc</customAttribute1>\r\n<customAttribute2>def</customAttribute2>\r\n<customAttribute3>ghi</customAttribute3>\r\n<customAttribute4>jkl</customAttribute4>\r\n.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<cnpOnlineResponse version='10.1' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><fraudCheckResponse id='127' reportGroup='Planets' customerId=''><cnpTxnId>742802348034313000</cnpTxnId><response>000</response><message>Approved</message><advancedFraudResults><deviceReviewStatus>pass</deviceReviewStatus><deviceReputationScore>42</deviceReputationScore><triggeredRule>triggered_rule_default</triggeredRule></advancedFraudResults></fraudCheckResponse></cnpOnlineResponse >");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            fraudCheckResponse fraudCheckResponse = cnp.FraudCheck(fraudCheck);

            Assert.NotNull(fraudCheckResponse);
            Assert.Equal("000", fraudCheckResponse.response);
        }

        [Fact]
        public void TestCustomAttribute5()
        {
            fraudCheck fraudCheck = new fraudCheck();
            advancedFraudChecksType advancedFraudCheck = new advancedFraudChecksType();
            fraudCheck.advancedFraudChecks = advancedFraudCheck;
            advancedFraudCheck.threatMetrixSessionId = "123";
            advancedFraudCheck.customAttribute1 = "abc";
            advancedFraudCheck.customAttribute2 = "def";
            advancedFraudCheck.customAttribute3 = "ghi";
            advancedFraudCheck.customAttribute4 = "jkl";
            advancedFraudCheck.customAttribute5 = "mno";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex("..*<threatMetrixSessionId>123</threatMetrixSessionId>\r\n<customAttribute1>abc</customAttribute1>\r\n<customAttribute2>def</customAttribute2>\r\n<customAttribute3>ghi</customAttribute3>\r\n<customAttribute4>jkl</customAttribute4>\r\n<customAttribute5>mno</customAttribute5>\r\n.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<cnpOnlineResponse version='10.1' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><fraudCheckResponse id='127' reportGroup='Planets' customerId=''><cnpTxnId>742802348034313000</cnpTxnId><response>000</response><message>Approved</message><advancedFraudResults><deviceReviewStatus>pass</deviceReviewStatus><deviceReputationScore>42</deviceReputationScore><triggeredRule>triggered_rule_default</triggeredRule></advancedFraudResults></fraudCheckResponse></cnpOnlineResponse >");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            fraudCheckResponse fraudCheckResponse = cnp.FraudCheck(fraudCheck);

            Assert.NotNull(fraudCheckResponse);
            Assert.Equal(742802348034313000, fraudCheckResponse.cnpTxnId);
        }

    }
}
