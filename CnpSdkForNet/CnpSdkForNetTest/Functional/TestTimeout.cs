using System.Collections.Generic;
using System.Net;
using NUnit.Framework;

namespace Cnp.Sdk.Test.Functional {
    [TestFixture]
    internal class TestTimeout
    {
        private CnpOnline _cnp;
        private Dictionary<string, string> _config;
        
        [Test]
        public void TestTimeoutNotDefined()
        {
            CommManager.reset();
            _config = new ConfigManager().getConfig();
            _config.Remove("timeout");
            
            _cnp = new CnpOnline(_config);
            
            var registerTokenRequest = new registerTokenRequestType
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                accountNumber = "1233456789103801",
            };

            var rtokenResponse = _cnp.RegisterToken(registerTokenRequest);
            StringAssert.AreEqualIgnoringCase("Account number was successfully registered", rtokenResponse.message);
        }

        [Test]
        public void TestTimeoutNotParsable()
        {
            CommManager.reset();
            _config = new ConfigManager().getConfig();
            _config["timeout"] = "notparsableasint";

            _cnp = new CnpOnline(_config);
            
            var registerTokenRequest = new registerTokenRequestType
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                accountNumber = "1233456789103801",
            };

            var rtokenResponse = _cnp.RegisterToken(registerTokenRequest);
            StringAssert.AreEqualIgnoringCase("Account number was successfully registered", rtokenResponse.message);
        }

        [Test]
        public void TestTimeoutReached() {
            CommManager.reset();
            _config = new ConfigManager().getConfig();
            _config["timeout"] = "1";

            _cnp = new CnpOnline(_config);

            var registerTokenRequest = new registerTokenRequestType {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                accountNumber = "1233456789103801",
            };

            Assert.Throws<WebException>(() => { _cnp.RegisterToken(registerTokenRequest); });
        }
    }
}