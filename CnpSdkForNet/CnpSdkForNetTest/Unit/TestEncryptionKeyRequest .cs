using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using System.Text.RegularExpressions;
using System;

namespace Cnp.Sdk.Test.Unit
{
    [TestFixture]
    class TestEncryptionKeyRequest
    {

        private CnpOnline cnp;
        Dictionary<String, String> config;

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            cnp = new CnpOnline();
            config = new ConfigManager().getConfig();
        }

        [Test]
        public void TestSimpleEncryptionKeyRequest()
        {
            var enc = new EncryptionKeyRequest();
            enc.encryptionKeyRequest = encryptionKeyRequestEnum.PREVIOUS;
            var mock = new Mock<Communications>();
            if (config["encrypteOltpPayload"] == "true")
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<encryptionKeyRequest>PREVIOUS</encryptionKeyRequest>.*", RegexOptions.Singleline)))
                 .Returns("<cnpOnlineResponse version='12.40' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><encryptionKeyResponse> <encryptionKeySequence>10000</encryptionKeySequence></encryptionKeyResponse></cnpOnlineResponse>");
            }
            else
            {
                mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<encryptionKeyRequest>PREVIOUS</encryptionKeyRequest>.*", RegexOptions.Singleline)))
                .Returns("<cnpOnlineResponse version='12.40' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><encryptionKeyResponse> <encryptionKeySequence>10000</encryptionKeySequence></encryptionKeyResponse></cnpOnlineResponse>");

            }
            var mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            var encryptionKeyResponse = cnp.encryptionKey(enc);

            Assert.NotNull(encryptionKeyResponse);
            Assert.AreEqual(10000, encryptionKeyResponse.encryptionKeySequence);
        }
    }
}
