using System.Collections.Generic;
using NUnit.Framework;

namespace Cnp.Sdk.Test.Unit
{
    [TestFixture]
    internal class TestCommunications
    {
        private Dictionary<string, string> config;
        private Communications objectUnderTest;

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            config = new Dictionary<string, string> {["url"] = "https://example.com"};
            objectUnderTest = new Communications(config);
        }

        [Test]
        public void TestSettingProxyPropertiesToNullShouldTurnOffProxy()
        {
            config["proxyHost"] = null;
            config["proxyPort"] = null;

            Assert.IsFalse(objectUnderTest.IsProxyOn());
        }

        [Test]
        public void TestSettingProxyPropertiesToEmptyShouldTurnOffProxy()
        {
            config["proxyHost"] = "";
            config["proxyPort"] = "";

            Assert.IsFalse(objectUnderTest.IsProxyOn());
        }

        [Test]
        public void TestSettingLogFileToEmptyShouldTurnOffLogFile()
        {
            config["logFile"] = "";

            Assert.IsFalse(objectUnderTest.IsValidConfigValueSet("logFile"));
        }

        [Test]
        public void TestConfigNotPresentInDictionary()
        {
            Assert.IsFalse(objectUnderTest.IsValidConfigValueSet("logFile"));
        }
    }
}
