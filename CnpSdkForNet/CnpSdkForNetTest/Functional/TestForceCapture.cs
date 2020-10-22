using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Threading;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestForceCapture
    {
        private CnpOnline _cnp;

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            _cnp = new CnpOnline();
        }

        [Test]
        public void SimpleForceCaptureWithCard()
        {
            var forcecapture = new forceCapture
            {
                id = "1",
                amount = 106,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                processingType = processingType.accountFunding,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1210"
                },
                merchantCategoryCode = "1111"
            };
            

            var response = _cnp.ForceCapture(forcecapture);
            Assert.AreEqual("Approved", response.message);
        }
        
        [Test]
        public void SimpleForceCaptureWithCardWithLocation()
        {
            var forcecapture = new forceCapture
            {
                id = "1",
                amount = 106,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                processingType = processingType.accountFunding,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1210"
                },
                merchantCategoryCode = "1111"
            };
            

            var response = _cnp.ForceCapture(forcecapture);
            Assert.AreEqual("sandbox", response.location);
            Assert.AreEqual("Approved", response.message);
        }
        
        [Test]
        public void SimpleForceCaptureWithprocessingType()
        {
            var forcecapture = new forceCapture
            {
                id = "1",
                amount = 106,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                processingType = processingType.initialCOF,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1210"
                }
            };

            var response = _cnp.ForceCapture(forcecapture);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleForceCaptureWithMpos()
        {
            var forcecapture = new forceCapture
            {
                id = "1",
                amount = 322,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                mpos = new mposType
                {
                    ksn = "77853211300008E00016",
                    encryptedTrack = "CASE1E185EADD6AFE78C9A214B21313DCD836FDD555FBE3A6C48D141FE80AB9172B963265AFF72111895FE415DEDA162CE8CB7AC4D91EDB611A2AB756AA9CB1A000000000000000000000000000000005A7AAF5E8885A9DB88ECD2430C497003F2646619A2382FFF205767492306AC804E8E64E8EA6981DD",
                    formatId = "30",
                    track1Status = 0,
                    track2Status = 0
                }
            };

            var response = _cnp.ForceCapture(forcecapture);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleForceCaptureWithToken()
        {
            var forcecapture = new forceCapture
            {
                id = "1",
                amount = 106,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                token = new cardTokenType
                {
                    cnpToken = "123456789101112",
                    expDate = "1210",
                    cardValidationNum = "555",
                    type = methodOfPaymentTypeEnum.VI
                }
            };

            var response = _cnp.ForceCapture(forcecapture);
            Assert.AreEqual("Approved", response.message); ;
        }

        [Test]
        public void simpleForceCaptureWithSecondaryAmount()
        {
            forceCapture forcecapture = new forceCapture();
            forcecapture.id = "1";
            forcecapture.amount = 106;
            forcecapture.secondaryAmount = 50;
            forcecapture.orderId = "12344";
            forcecapture.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000000";
            card.expDate = "1210";
            forcecapture.card = card;
            forceCaptureResponse response = _cnp.ForceCapture(forcecapture);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void TestForceCaptureWithCardAsync()
        {
            var forcecapture = new forceCapture
            {
                id = "1",
                amount = 106,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                processingType = processingType.accountFunding,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1210"
                }
            };

            CancellationToken cancellationToken = new CancellationToken(false);
            var response = _cnp.ForceCaptureAsync(forcecapture, cancellationToken);
            Assert.AreEqual("000", response.Result.response);
        }
        [Test]
        public void SimpleForceCaptureBusinessIndicator()
        {
            var forcecapture = new forceCapture
            {
                id = "1",
                amount = 106,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                processingType = processingType.accountFunding,
                businessIndicator = businessIndicatorEnum.consumerBillPayment,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1210"
                },
                merchantCategoryCode = "1111"
            };
            

            var response = _cnp.ForceCapture(forcecapture);
            Assert.AreEqual("Approved", response.message);
        }

    }
}
