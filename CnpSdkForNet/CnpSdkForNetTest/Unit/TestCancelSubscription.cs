using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Cnp.Sdk;
using Moq;
using System.Text.RegularExpressions;


namespace Cnp.Sdk.Test.Unit
{
    [TestFixture]
    class TestCancelSubscription
    {        
        private CnpOnline cnp;

        [TestFixtureSetUp]
        public void SetUpCnp()
        {
            cnp = new CnpOnline();
        }

        [Test]
        public void TestSimple()
        {
            cancelSubscription update = new cancelSubscription();
            update.subscriptionId = 12345;
           
            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<cnpOnlineRequest.*?<cancelSubscription>\r\n<subscriptionId>12345</subscriptionId>\r\n</cancelSubscription>\r\n</cnpOnlineRequest>.*?.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<cnpOnlineResponse version='8.20' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><cancelSubscriptionResponse><subscriptionId>12345</subscriptionId></cancelSubscriptionResponse></cnpOnlineResponse>");
     
            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.CancelSubscription(update);
        }


    }
}
