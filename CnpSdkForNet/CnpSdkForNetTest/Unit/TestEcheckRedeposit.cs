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
    class TestEcheckRedeposit
    {
        
        private CnpOnline cnp;

        [TestFixtureSetUp]
        public void SetUpCnp()
        {
            cnp = new CnpOnline();
        }

        [Test]
        public void TestMerchantData()
        {
            echeckRedeposit echeckRedeposit = new echeckRedeposit();
            echeckRedeposit.cnpTxnId = 1;
            echeckRedeposit.merchantData = new merchantDataType();
            echeckRedeposit.merchantData.campaign = "camp";
            echeckRedeposit.merchantData.affiliate = "affil";
            echeckRedeposit.merchantData.merchantGroupingId = "mgi";
            echeckRedeposit.customIdentifier = "customIdent";
           
            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<echeckRedeposit.*<cnpTxnId>1</cnpTxnId>.*<merchantData>.*<campaign>camp</campaign>.*<affiliate>affil</affiliate>.*<merchantGroupingId>mgi</merchantGroupingId>.*</merchantData>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<cnpOnlineResponse version='8.13' response='0' message='Valid Format' xmlns='http://www.vantivcnp.com/schema'><echeckRedepositResponse><cnpTxnId>123</cnpTxnId></echeckRedepositResponse></cnpOnlineResponse>");
     
            Communications mockedCommunication = mock.Object;
            cnp.SetCommunication(mockedCommunication);
            cnp.EcheckRedeposit(echeckRedeposit);
        }            
    }
}
