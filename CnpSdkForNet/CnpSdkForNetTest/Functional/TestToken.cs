using System.Collections.Generic;
using NUnit.Framework;
using System.Threading;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestToken
    {
        private CnpOnline _cnp;

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            CommManager.reset();
            _cnp = new CnpOnline();
        }

        [Test]
        public void SimpleToken()
        {
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
        public void SimpleTokenWithPayPage()
        {
            var registerTokenRequest = new registerTokenRequestType
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                paypageRegistrationId = "1233456789101112",
            };

            var rtokenResponse = _cnp.RegisterToken(registerTokenRequest);
            StringAssert.AreEqualIgnoringCase("Account number was successfully registered", rtokenResponse.message);
        }

        [Test]
        public void SimpleTokenWithEcheck()
        {
            var registerTokenRequest = new registerTokenRequestType
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                echeckForToken = new echeckForTokenType
                {
                    accNum = "12344565",
                    routingNum = "123476545"
                }
            };

            var rtokenResponse = _cnp.RegisterToken(registerTokenRequest);
            StringAssert.AreEqualIgnoringCase("Account number was successfully registered", rtokenResponse.message);
        }

        [Test]
        public void SimpleTokenWithApplepay()
        {
            var registerTokenRequest = new registerTokenRequestType
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                applepay = new applepayType
                {
                    header = new applepayHeaderType
                    {
                        applicationData = "454657413164",
                        ephemeralPublicKey = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
                        publicKeyHash = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
                        transactionId = "1234"
                    },

                    data = "user",
                    signature = "sign",
                    version = "12345"
                }
            };

            var rtokenResponse = _cnp.RegisterToken(registerTokenRequest);
            StringAssert.AreEqualIgnoringCase("Account number was successfully registered", rtokenResponse.message);
            Assert.AreEqual("0", rtokenResponse.applepayResponse.transactionAmount);
        }

        [Test]
        public void TokenEcheckMissingRequiredField()
        {
            var registerTokenRequest = new registerTokenRequestType
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                echeckForToken = new echeckForTokenType
                {
                    routingNum = "123476545"
                }
            };

            try
            {
                //expected exception;
                var rtokenResponse = _cnp.RegisterToken(registerTokenRequest);
            }
            catch (CnpOnlineException e)
            {
                Assert.True(e.Message.StartsWith("Error validating xml data against the schema"));
            }
        }

        [Test]
        public void TestSimpleTokenWithNullableTypeField()
        {
            var registerTokenRequest = new registerTokenRequestType
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                accountNumber = "1233456789103801",
            };
            

            var rtokenResponse = _cnp.RegisterToken(registerTokenRequest);
            StringAssert.AreEqualIgnoringCase("Account number was successfully registered", rtokenResponse.message);
            Assert.IsNull(rtokenResponse.type);
        }

        [Test]
        public void TestTokenAsync()
        {
            var registerTokenRequest = new registerTokenRequestType
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                accountNumber = "1233456789103801",
            };

            CancellationToken cancellationToken = new CancellationToken(false);
            var rtokenResponse = _cnp.RegisterTokenAsync(registerTokenRequest, cancellationToken);
            StringAssert.AreEqualIgnoringCase("801", rtokenResponse.Result.response);
        }
    }
}
