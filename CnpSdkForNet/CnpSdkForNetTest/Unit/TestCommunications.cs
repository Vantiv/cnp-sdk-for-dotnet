using System.Collections.Generic;
using NUnit.Framework;



namespace Cnp.Sdk.Test.Unit
{
    [TestFixture]
    internal class TestCommunications
    {
        private Communications _objectUnderTest;

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            Dictionary<string, string> config = new Dictionary<string, string>();
            _objectUnderTest = new Communications(config);
        }

        [Test]
        public void TestSettingProxyPropertiesToNullShouldTurnOffProxy()
        {
            var config = new Dictionary<string, string> { { "proxyHost", null }, { "proxyPort", null } };

            Assert.IsFalse(_objectUnderTest.IsProxyOn(config));

        }

        [Test]
        public void TestSettingProxyPropertiesToEmptyShouldTurnOffProxy()
        {
            var config = new Dictionary<string, string> { { "proxyHost", "" }, { "proxyPort", "" } };

            Assert.IsFalse(_objectUnderTest.IsProxyOn(config));

        }

        [Test]
        public void TestSettingLogFileToEmptyShouldTurnOffLogFile()
        {
            var config = new Dictionary<string, string> { { "logFile", "" } };

            Assert.IsFalse(_objectUnderTest.IsValidConfigValueSet(config, "logFile"));

            config = null;

            Assert.IsFalse(_objectUnderTest.IsValidConfigValueSet(config, "logFile"));

        }

        [Test]
        public void TestConfigNotPresentInDictionary()
        {
            var config = new Dictionary<string, string> { };

            Assert.IsFalse(_objectUnderTest.IsValidConfigValueSet(config, "logFile"));

        }


    }
}
