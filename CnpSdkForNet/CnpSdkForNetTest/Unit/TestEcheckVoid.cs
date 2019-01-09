using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Cnp.Sdk;
using Moq;
using System.Text.RegularExpressions;


namespace Cnp.Sdk.Test.Unit
{
    public class TestEcheckVoid
    {
        
        private CnpOnline cnp = new CnpOnline();
        

        [Fact]
        public void TestFraudFilterOverride()
        {
            echeckVoid echeckVoid = new echeckVoid();
            echeckVoid.cnpTxnId = 123456789;
           
            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<echeckVoid.*<cnpTxnId>123456789.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<cnpOnlineResponse version='8.13' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><echeckVoidResponse><cnpTxnId>123</cnpTxnId></echeckVoidResponse></cnpOnlineResponse>");
     
            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.EcheckVoid(echeckVoid);
        }

        
    }
}
