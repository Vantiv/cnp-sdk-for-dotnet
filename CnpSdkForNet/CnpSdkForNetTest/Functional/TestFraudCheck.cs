using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestFraudCheck
    {
        private CnpOnline _cnp;

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            _cnp = new CnpOnline();
        }

        [Test]
        public void TestCustomAttribute7TriggeredRules()
        {
            var fraudCheck = new fraudCheck
            {
                id = "1",


                reportGroup = "Planets",
                advancedFraudChecks = new advancedFraudChecksType
                {
                    threatMetrixSessionId = "101",
                    customAttribute1 = "pass",
                    customAttribute2 = "42",
                    customAttribute3 = "7",
                    customAttribute4 = "jkl",
                    customAttribute5 = "mno",
                }
            };

            var fraudCheckResponse = _cnp.FraudCheck(fraudCheck);

            Assert.NotNull(fraudCheckResponse);
            Assert.AreEqual(42, fraudCheckResponse.advancedFraudResults.deviceReputationScore);
            Assert.AreEqual(7, fraudCheckResponse.advancedFraudResults.triggeredRule.Length);
            Assert.AreEqual("triggered_rule_1", fraudCheckResponse.advancedFraudResults.triggeredRule[0]);
            Assert.AreEqual("triggered_rule_2", fraudCheckResponse.advancedFraudResults.triggeredRule[1]);
            Assert.AreEqual("triggered_rule_3", fraudCheckResponse.advancedFraudResults.triggeredRule[2]);
            Assert.AreEqual("triggered_rule_4", fraudCheckResponse.advancedFraudResults.triggeredRule[3]);
            Assert.AreEqual("triggered_rule_5", fraudCheckResponse.advancedFraudResults.triggeredRule[4]);
            Assert.AreEqual("triggered_rule_6", fraudCheckResponse.advancedFraudResults.triggeredRule[5]);
            Assert.AreEqual("triggered_rule_7", fraudCheckResponse.advancedFraudResults.triggeredRule[6]);
        }

        [Test]
        public void TestFraudCheckWithWebSessionId()
        {
            var fraudCheck = new fraudCheck
            {
                id = "1",
                reportGroup = "Planets",
                advancedFraudChecks = new advancedFraudChecksType
                {
                    customAttribute1 = "fail",
                    customAttribute2 = "60",
                    customAttribute3 = "7",
                    customAttribute4 = "jkl",
                    customAttribute5 = "mno",
                    webSessionId = "abc123"
                },
                billToAddress = new contact
                {
                    firstName = "Bob",
                    lastName = "Bagels",
                    addressLine1 = "37 Main Street",
                    city = "Augusta",
                    state = "Wisconsin",
                    zip = "28209"
                },
                shipToAddress = new contact
                {
                    firstName = "P",
                    lastName = "Sherman",
                    addressLine1 = "42 Wallaby Way",
                    city = "Sydney",
                    state = "New South Wales",
                    zip = "2127"
                },
                amount = 51699
            };

            var fraudCheckResponse = _cnp.FraudCheck(fraudCheck);
            Assert.NotNull(fraudCheckResponse);
            Assert.AreEqual("sandbox", fraudCheckResponse.location);
            Assert.AreEqual("Approved", fraudCheckResponse.message);
            Assert.AreEqual("fail", fraudCheckResponse.advancedFraudResults.deviceReviewStatus);
        }

        [Test]
        public void TestFraudCheckWithAddressAndAmount()
        {
            var fraudCheck = new fraudCheck
            {
                id = "1",
                reportGroup = "Planets",
                advancedFraudChecks = new advancedFraudChecksType
                {
                    customAttribute1 = "fail",
                    customAttribute2 = "60",
                    customAttribute3 = "7",
                    customAttribute4 = "jkl",
                    customAttribute5 = "mno",
                    threatMetrixSessionId = "123"
                },
                billToAddress = new contact
                {
                    firstName = "Bob",
                    lastName = "Bagels",
                    addressLine1 = "37 Main Street",
                    city = "Augusta",
                    state = "Wisconsin",
                    zip = "28209"
                },
                shipToAddress = new contact
                {
                    firstName = "P",
                    lastName = "Sherman",
                    addressLine1 = "42 Wallaby Way",
                    city = "Sydney",
                    state = "New South Wales",
                    zip = "2127"
                },
                amount = 51699
            };

            var fraudCheckResponse = _cnp.FraudCheck(fraudCheck);
            Assert.NotNull(fraudCheckResponse);
            Assert.AreEqual("sandbox", fraudCheckResponse.location);
            Assert.AreEqual("Call Discover", fraudCheckResponse.message);
            Assert.AreEqual("fail", fraudCheckResponse.advancedFraudResults.deviceReviewStatus);

        }

        [Test]
        public void TestFraudCheckWithAddressAndAmountAsync()
        {
            var fraudCheck = new fraudCheck
            {
                id = "1",
                reportGroup = "Planets",
                advancedFraudChecks = new advancedFraudChecksType
                {
                    customAttribute1 = "fail",
                    customAttribute2 = "60",
                    customAttribute3 = "7",
                    customAttribute4 = "jkl",
                    customAttribute5 = "mno",
                    threatMetrixSessionId = "123"
                },
                billToAddress = new contact
                {
                    firstName = "Bob",
                    lastName = "Bagels",
                    addressLine1 = "37 Main Street",
                    city = "Augusta",
                    state = "Wisconsin",
                    zip = "28209"
                },
                shipToAddress = new contact
                {
                    firstName = "P",
                    lastName = "Sherman",
                    addressLine1 = "42 Wallaby Way",
                    city = "Sydney",
                    state = "New South Wales",
                    zip = "2127"
                },
                amount = 51699
            };
            CancellationToken cancellationToken = new CancellationToken(false);
            var response = _cnp.FraudCheckAsync(fraudCheck, cancellationToken);
            var fraudCheckResponse = (fraudCheckResponse)response.Result;
            Assert.NotNull(fraudCheckResponse);
            Assert.AreEqual("sandbox", fraudCheckResponse.location);
            Assert.AreEqual("Call Discover", fraudCheckResponse.message);
            Assert.AreEqual("fail", fraudCheckResponse.advancedFraudResults.deviceReviewStatus);

        }
    }
}
