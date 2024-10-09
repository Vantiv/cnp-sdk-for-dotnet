﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using Moq;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Net;

namespace Cnp.Sdk.Test.Unit
{
    [TestFixture]
    class TestAdvancedFraudCheck
    {

        private CnpOnline cnp;        

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            cnp = new CnpOnline();
        }

        [Test]
        public void TestNoCustomAttributes()
        {
            fraudCheck fraudCheck = new fraudCheck();
            advancedFraudChecksType advancedFraudCheck = new advancedFraudChecksType();
            fraudCheck.advancedFraudChecks = advancedFraudCheck;
            advancedFraudCheck.threatMetrixSessionId = "123";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<threatMetrixSessionId>123</threatMetrixSessionId>\r\n.*", RegexOptions.Singleline)))
                .Returns(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent("<cnpOnlineResponse version='10.1' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><fraudCheckResponse id='127' reportGroup='Planets' customerId=''><cnpTxnId>742802348034313000</cnpTxnId><response>000</response><message>Approved</message><advancedFraudResults><deviceReviewStatus>pass</deviceReviewStatus><deviceReputationScore>42</deviceReputationScore><triggeredRule>triggered_rule_default</triggeredRule></advancedFraudResults></fraudCheckResponse></cnpOnlineResponse >") });

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            fraudCheckResponse fraudCheckResponse = cnp.FraudCheck(fraudCheck);

            Assert.NotNull(fraudCheckResponse);
            Assert.AreEqual("pass", fraudCheckResponse.advancedFraudResults.deviceReviewStatus);
        }

        [Test]
        public void TestCustomAttribute1()
        {
            fraudCheck fraudCheck = new fraudCheck();
            advancedFraudChecksType advancedFraudCheck = new advancedFraudChecksType();
            fraudCheck.advancedFraudChecks = advancedFraudCheck;
            advancedFraudCheck.threatMetrixSessionId = "123";
            advancedFraudCheck.customAttribute1 = "abc";

            var mock = new Mock<Communications>();
            
            mock.Setup(Communications => Communications.HttpPost(It.IsRegex("..*<threatMetrixSessionId>123</threatMetrixSessionId>\r\n<customAttribute1>abc</customAttribute1>\r\n.*", RegexOptions.Singleline)))
                .Returns(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent("<cnpOnlineResponse version='10.1' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><fraudCheckResponse id='127' reportGroup='Planets' customerId=''><cnpTxnId>742802348034313000</cnpTxnId><response>000</response><message>Approved</message><advancedFraudResults><deviceReviewStatus>pass</deviceReviewStatus><deviceReputationScore>42</deviceReputationScore><triggeredRule>triggered_rule_default</triggeredRule></advancedFraudResults></fraudCheckResponse></cnpOnlineResponse >") });

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            fraudCheckResponse fraudCheckResponse = cnp.FraudCheck(fraudCheck);

            Assert.NotNull(fraudCheckResponse);
            Assert.AreEqual(42, fraudCheckResponse.advancedFraudResults.deviceReputationScore);
        }

        [Test]
        public void TestCustomAttribute2()
        {
            fraudCheck fraudCheck = new fraudCheck();
            advancedFraudChecksType advancedFraudCheck = new advancedFraudChecksType();
            fraudCheck.advancedFraudChecks = advancedFraudCheck;
            advancedFraudCheck.threatMetrixSessionId = "123";
            advancedFraudCheck.customAttribute1 = "abc";
            advancedFraudCheck.customAttribute2 = "def";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex("..*<threatMetrixSessionId>123</threatMetrixSessionId>\r\n<customAttribute1>abc</customAttribute1>\r\n<customAttribute2>def</customAttribute2>\r\n.*", RegexOptions.Singleline)))
                .Returns(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent("<cnpOnlineResponse version='10.1' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><fraudCheckResponse id='127' reportGroup='Planets' customerId=''><cnpTxnId>742802348034313000</cnpTxnId><response>000</response><message>Approved</message><advancedFraudResults><deviceReviewStatus>pass</deviceReviewStatus><deviceReputationScore>42</deviceReputationScore><triggeredRule>triggered_rule_default</triggeredRule></advancedFraudResults></fraudCheckResponse></cnpOnlineResponse >") });

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            fraudCheckResponse fraudCheckResponse = cnp.FraudCheck(fraudCheck);

            Assert.NotNull(fraudCheckResponse);
            Assert.AreEqual("triggered_rule_default", fraudCheckResponse.advancedFraudResults.triggeredRule[0]);
        }

        [Test]
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

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex("..*<threatMetrixSessionId>123</threatMetrixSessionId>\r\n<customAttribute1>abc</customAttribute1>\r\n<customAttribute2>def</customAttribute2>\r\n<customAttribute3>ghi</customAttribute3>\r\n.*", RegexOptions.Singleline)))
                .Returns(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent("<cnpOnlineResponse version='10.1' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><fraudCheckResponse id='127' reportGroup='Planets' customerId=''><cnpTxnId>742802348034313000</cnpTxnId><response>000</response><message>Approved</message><advancedFraudResults><deviceReviewStatus>pass</deviceReviewStatus><deviceReputationScore>42</deviceReputationScore><triggeredRule>triggered_rule_default</triggeredRule></advancedFraudResults></fraudCheckResponse></cnpOnlineResponse >") });

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            fraudCheckResponse fraudCheckResponse = cnp.FraudCheck(fraudCheck);

            Assert.NotNull(fraudCheckResponse);
            Assert.AreEqual("Approved", fraudCheckResponse.message);
        }

        [Test]
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

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex("..*<threatMetrixSessionId>123</threatMetrixSessionId>\r\n<customAttribute1>abc</customAttribute1>\r\n<customAttribute2>def</customAttribute2>\r\n<customAttribute3>ghi</customAttribute3>\r\n<customAttribute4>jkl</customAttribute4>\r\n.*", RegexOptions.Singleline)))
                .Returns(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent("<cnpOnlineResponse version='10.1' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><fraudCheckResponse id='127' reportGroup='Planets' customerId=''><cnpTxnId>742802348034313000</cnpTxnId><response>000</response><message>Approved</message><advancedFraudResults><deviceReviewStatus>pass</deviceReviewStatus><deviceReputationScore>42</deviceReputationScore><triggeredRule>triggered_rule_default</triggeredRule></advancedFraudResults></fraudCheckResponse></cnpOnlineResponse >") });

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            fraudCheckResponse fraudCheckResponse = cnp.FraudCheck(fraudCheck);

            Assert.NotNull(fraudCheckResponse);
            Assert.AreEqual("000", fraudCheckResponse.response);
        }

        [Test]
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

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex("..*<threatMetrixSessionId>123</threatMetrixSessionId>\r\n<customAttribute1>abc</customAttribute1>\r\n<customAttribute2>def</customAttribute2>\r\n<customAttribute3>ghi</customAttribute3>\r\n<customAttribute4>jkl</customAttribute4>\r\n<customAttribute5>mno</customAttribute5>\r\n.*", RegexOptions.Singleline)))
                .Returns(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent("<cnpOnlineResponse version='10.1' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><fraudCheckResponse id='127' reportGroup='Planets' customerId=''><cnpTxnId>742802348034313000</cnpTxnId><response>000</response><message>Approved</message><advancedFraudResults><deviceReviewStatus>pass</deviceReviewStatus><deviceReputationScore>42</deviceReputationScore><triggeredRule>triggered_rule_default</triggeredRule></advancedFraudResults></fraudCheckResponse></cnpOnlineResponse >") });

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            fraudCheckResponse fraudCheckResponse = cnp.FraudCheck(fraudCheck);

            Assert.NotNull(fraudCheckResponse);
            Assert.AreEqual(742802348034313000, fraudCheckResponse.cnpTxnId);
        }

        [Test]
        public void TestAdvancedFraudChecksTypeWebSessionId()
        {
            fraudCheck fraudCheck = new fraudCheck();
            advancedFraudChecksType advancedFraudCheck = new advancedFraudChecksType();
            fraudCheck.advancedFraudChecks = advancedFraudCheck;
            advancedFraudCheck.webSessionId = "ajhgsdjh";
            advancedFraudCheck.customAttribute1 = "abc";
            advancedFraudCheck.customAttribute2 = "def";
            advancedFraudCheck.customAttribute3 = "ghi";
            advancedFraudCheck.customAttribute4 = "jkl";
            advancedFraudCheck.customAttribute5 = "mno";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex("..*<webSessionId>ajhgsdjh</webSessionId>\r\n<customAttribute1>abc</customAttribute1>\r\n<customAttribute2>def</customAttribute2>\r\n<customAttribute3>ghi</customAttribute3>\r\n<customAttribute4>jkl</customAttribute4>\r\n<customAttribute5>mno</customAttribute5>\r\n.*", RegexOptions.Singleline)))
                .Returns(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent("<cnpOnlineResponse version='10.1' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><fraudCheckResponse id='127' reportGroup='Planets' customerId=''><cnpTxnId>742802348034313000</cnpTxnId><response>000</response><message>Approved</message><advancedFraudResults><deviceReviewStatus>pass</deviceReviewStatus><deviceReputationScore>42</deviceReputationScore><triggeredRule>triggered_rule_default</triggeredRule></advancedFraudResults></fraudCheckResponse></cnpOnlineResponse >") });

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            fraudCheckResponse fraudCheckResponse = cnp.FraudCheck(fraudCheck);

            Assert.NotNull(fraudCheckResponse);
            Assert.AreEqual(742802348034313000, fraudCheckResponse.cnpTxnId);
        }

    }
}
