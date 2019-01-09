using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Cnp.Sdk;
using Moq;
using System.Text.RegularExpressions;


namespace Cnp.Sdk.Test.Unit
{
    public class TestVoid
    {
        
        private CnpOnline cnp = new CnpOnline();

        [Fact]
        public void TestRecyclingDataOnVoidResponse()
        {
            voidTxn voidTxn = new voidTxn();
            voidTxn.cnpTxnId = 123;
           
            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<cnpOnlineResponse version='8.16' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><voidResponse><cnpTxnId>123</cnpTxnId><response>000</response><responseTime>2013-01-31T15:48:09</responseTime><postDate>2013-01-31</postDate><message>Approved</message><recycling><creditCnpTxnId>456</creditCnpTxnId></recycling></voidResponse></cnpOnlineResponse>");
     
            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            voidResponse response = cnp.DoVoid(voidTxn);
            Assert.Equal(123, response.cnpTxnId);
            Assert.Equal(456, response.recycling.creditCnpTxnId);
        }

        [Fact]
        public void TestRecyclingDataOnVoidResponseIsOptional()
        {
            voidTxn voidTxn = new voidTxn();
            voidTxn.cnpTxnId = 123;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<cnpOnlineResponse version='8.16' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><voidResponse><cnpTxnId>123</cnpTxnId><response>000</response><responseTime>2013-01-31T15:48:09</responseTime><postDate>2013-01-31</postDate><message>Approved</message></voidResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            voidResponse response = cnp.DoVoid(voidTxn);
            Assert.Equal(123, response.cnpTxnId);
            Assert.Null(response.recycling);
        }

    }
}
