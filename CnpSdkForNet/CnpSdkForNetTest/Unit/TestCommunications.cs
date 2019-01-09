 using System.Collections.Generic;
using Xunit;



namespace Cnp.Sdk.Test.Unit
{

    public class TestCommunications
    {
        private Communications _objectUnderTest = new Communications();
        

        [Fact]
        public void TestSettingProxyPropertiesToNullShouldTurnOffProxy()
        {
            var config = new Dictionary<string, string> { { "proxyHost", null }, { "proxyPort", null } };

            Assert.False(_objectUnderTest.IsProxyOn(config));

        }

        [Fact]
        public void TestSettingProxyPropertiesToEmptyShouldTurnOffProxy()
        {
            var config = new Dictionary<string, string> { { "proxyHost", "" }, { "proxyPort", "" } };

            Assert.False(_objectUnderTest.IsProxyOn(config));

        }

        [Fact]
        public void TestSettingLogFileToEmptyShouldTurnOffLogFile()
        {
            var config = new Dictionary<string, string> { { "logFile", "" } };

            Assert.False(_objectUnderTest.IsValidConfigValueSet(config, "logFile"));

            config = null;

            Assert.False(_objectUnderTest.IsValidConfigValueSet(config, "logFile"));

        }

        [Fact]
        public void TestConfigNotPresentInDictionary()
        {
            var config = new Dictionary<string, string> { };

            Assert.False(_objectUnderTest.IsValidConfigValueSet(config, "logFile"));

        }


    }
}
