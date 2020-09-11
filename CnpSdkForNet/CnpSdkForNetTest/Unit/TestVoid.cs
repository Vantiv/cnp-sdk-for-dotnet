using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using System.Text.RegularExpressions;


namespace Cnp.Sdk.Test.Unit
{
    [TestFixture]
    class TestVoid
    {
        
        private CnpOnline cnp;

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            cnp = new CnpOnline();
        }

        [Test]
        public void TestRecyclingDataOnVoidResponse()
        {
            voidTxn voidTxn = new voidTxn();
            voidTxn.cnpTxnId = 123;
           
            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='8.16' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><voidResponse><cnpTxnId>123</cnpTxnId><response>000</response><responseTime>2013-01-31T15:48:09</responseTime><postDate>2013-01-31</postDate><message>Approved</message><recyclingResponse><creditCnpTxnId>456</creditCnpTxnId></recyclingResponse><location>sandbox</location></voidResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            voidResponse response = cnp.DoVoid(voidTxn);
            Assert.AreEqual(123, response.cnpTxnId);
            Assert.AreEqual(456, response.recyclingResponse.creditCnpTxnId);
            Assert.AreEqual("sandbox", response.location);
        }

        [Test]
        public void TestRecyclingDataOnVoidResponseIsOptional()
        {
            voidTxn voidTxn = new voidTxn();
            voidTxn.cnpTxnId = 123;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*", RegexOptions.Singleline)  ))
                .Returns("<cnpOnlineResponse version='8.16' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><voidResponse><cnpTxnId>123</cnpTxnId><response>000</response><responseTime>2013-01-31T15:48:09</responseTime><postDate>2013-01-31</postDate><message>Approved</message></voidResponse></cnpOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            voidResponse response = cnp.DoVoid(voidTxn);
            Assert.AreEqual(123, response.cnpTxnId);
            Assert.IsNull(response.recyclingResponse);
        }

    }
}
