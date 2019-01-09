using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Cnp.Sdk;
using Moq;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.IO;


namespace Cnp.Sdk.Test.Unit
{
    public class TestXmlFieldsUnserializer
    {
        [Fact]
        public void TestAuthorizationResponseContainsGiftCardResponse()
        {
            String xml = "<authorizationResponse xmlns=\"http://www.vantivcnp.com/schema\"><giftCardResponse></giftCardResponse></authorizationResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(authorizationResponse));
            StringReader reader = new StringReader(xml);
            authorizationResponse authorizationResponse = (authorizationResponse)serializer.Deserialize(reader);

            Assert.NotNull(authorizationResponse.giftCardResponse);
        }

        // CES: Commenting this out because AuthReversal no longer uses giftCardResponse
        
        /*
        [Fact]
        public void TestAuthReversalResponseContainsGiftCardResponse()
        {
            String xml = "<authReversalResponse xmlns=\"http://www.vantivcnp.com/schema\"><giftCardResponse></giftCardResponse></authReversalResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(authReversalResponse));
            StringReader reader = new StringReader(xml);
            authReversalResponse authReversalResponse = (authReversalResponse)serializer.Deserialize(reader);

            Assert.NotNull(authReversalResponse.giftCardResponse);
        }
        */

        [Fact]
        public void TestgiftCardAuthReversalResponseContainsGiftCardResponse()
        {
            String xml = "<giftCardAuthReversalResponse xmlns=\"http://www.vantivcnp.com/schema\"><giftCardResponse></giftCardResponse></giftCardAuthReversalResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(giftCardAuthReversalResponse));
            StringReader reader = new StringReader(xml);
            giftCardAuthReversalResponse giftCardAuthReversalResponse = (giftCardAuthReversalResponse)serializer.Deserialize(reader);

            Assert.NotNull(giftCardAuthReversalResponse.giftCardResponse);
        }

        // CES: Commenting this out because captureResponse no longer uses giftCardResponse
        // I will add a test for giftCardCapture
        /*
        [Fact]
        public void TestCaptureResponseContainsGiftCardResponse()
        {
            String xml = "<captureResponse xmlns=\"http://www.vantivcnp.com/schema\"><giftCardResponse></giftCardResponse></captureResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(captureResponse));
            StringReader reader = new StringReader(xml);
            captureResponse captureResponse = (captureResponse)serializer.Deserialize(reader);

            Assert.NotNull(captureResponse.giftCardResponse);
        }
        */

        [Fact]
        public void TestCaptureResponseContainsFraudResult()
        {
            String xml = "<captureResponse xmlns=\"http://www.vantivcnp.com/schema\"><fraudResult></fraudResult></captureResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(captureResponse));
            StringReader reader = new StringReader(xml);
            captureResponse captureResponse = (captureResponse)serializer.Deserialize(reader);

            Assert.NotNull(captureResponse.fraudResult);
        }

        [Fact]
        public void TestForceCaptureResponseContainsGiftCardResponse()
        {
            String xml = "<forceCaptureResponse xmlns=\"http://www.vantivcnp.com/schema\"><giftCardResponse></giftCardResponse></forceCaptureResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(forceCaptureResponse));
            StringReader reader = new StringReader(xml);
            forceCaptureResponse forceCaptureResponse = (forceCaptureResponse)serializer.Deserialize(reader);

            Assert.NotNull(forceCaptureResponse.giftCardResponse);
        }

        [Fact]
        public void TestForceCaptureResponseContainsFraudResult()
        {
            String xml = "<forceCaptureResponse xmlns=\"http://www.vantivcnp.com/schema\"><fraudResult></fraudResult></forceCaptureResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(forceCaptureResponse));
            StringReader reader = new StringReader(xml);
            forceCaptureResponse forceCaptureResponse = (forceCaptureResponse)serializer.Deserialize(reader);

            Assert.NotNull(forceCaptureResponse.fraudResult);
        }

        [Fact]
        public void TestCaptureGivenAuthResponseContainsGiftCardResponse()
        {
            String xml = "<captureGivenAuthResponse xmlns=\"http://www.vantivcnp.com/schema\"><giftCardResponse></giftCardResponse></captureGivenAuthResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(captureGivenAuthResponse));
            StringReader reader = new StringReader(xml);
            captureGivenAuthResponse captureGivenAuthResponse = (captureGivenAuthResponse)serializer.Deserialize(reader);

            Assert.NotNull(captureGivenAuthResponse.giftCardResponse);
        }

        [Fact]
        public void TestCaptureGivenAuthResponseContainsFraudResult()
        {
            String xml = "<captureGivenAuthResponse xmlns=\"http://www.vantivcnp.com/schema\"><fraudResult></fraudResult></captureGivenAuthResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(captureGivenAuthResponse));
            StringReader reader = new StringReader(xml);
            captureGivenAuthResponse captureGivenAuthResponse = (captureGivenAuthResponse)serializer.Deserialize(reader);

            Assert.NotNull(captureGivenAuthResponse.fraudResult);
        }

        [Fact]
        public void TestSaleResponseContainsGiftCardResponse()
        {
            String xml = "<saleResponse xmlns=\"http://www.vantivcnp.com/schema\"><giftCardResponse></giftCardResponse></saleResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(saleResponse));
            StringReader reader = new StringReader(xml);
            saleResponse saleResponse = (saleResponse)serializer.Deserialize(reader);

            Assert.NotNull(saleResponse.giftCardResponse);
        }

        // Gift card response is now its own class
        //[Fact]
        //public void TestCreditResponseContainsGiftCardResponse()
        //{
        //    String xml = "<creditResponse xmlns=\"http://www.vantivcnp.com/schema\"><giftCardResponse></giftCardResponse></creditResponse>";
        //    XmlSerializer serializer = new XmlSerializer(typeof(creditResponse));
        //    StringReader reader = new StringReader(xml);
        //    creditResponse creditResponse = (creditResponse)serializer.Deserialize(reader);

        //    Assert.NotNull(creditResponse.giftCardResponse);
        //}

        [Fact]
        public void TestCreditResponseContainsFraudResult()
        {
            String xml = "<creditResponse xmlns=\"http://www.vantivcnp.com/schema\"><fraudResult></fraudResult></creditResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(creditResponse));
            StringReader reader = new StringReader(xml);
            creditResponse creditResponse = (creditResponse)serializer.Deserialize(reader);

            Assert.NotNull(creditResponse.fraudResult);
        }

        [Fact]
        public void TestActivateResponse()
        {
            String xml = "<activateResponse reportGroup=\"A\" id=\"3\" customerId=\"4\"  xmlns=\"http://www.vantivcnp.com/schema\"><response>000</response><cnpTxnId>1</cnpTxnId><responseTime>2013-09-05T14:23:45</responseTime><postDate>2013-09-05</postDate><message>Approved</message><fraudResult></fraudResult><giftCardResponse></giftCardResponse></activateResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(activateResponse));
            StringReader reader = new StringReader(xml);
            activateResponse activateResponse = (activateResponse)serializer.Deserialize(reader);

            Assert.Equal("A", activateResponse.reportGroup);
            Assert.Equal("4", activateResponse.customerId);
            Assert.Equal(1, activateResponse.cnpTxnId);
            Assert.Equal("000", activateResponse.response);
            Assert.Equal(new DateTime(2013,9,5,14,23,45), activateResponse.responseTime);
            Assert.Equal(new DateTime(2013,9,5), activateResponse.postDate);
            Assert.Equal("Approved", activateResponse.message);
            Assert.NotNull(activateResponse.fraudResult);
            Assert.NotNull(activateResponse.giftCardResponse);
        }

        [Fact]
        public void TestLoadResponse()
        {
            String xml = "<loadResponse reportGroup=\"A\" id=\"3\" customerId=\"4\" duplicate=\"true\" xmlns=\"http://www.vantivcnp.com/schema\"><cnpTxnId>1</cnpTxnId><orderId>2</orderId><response>000</response><responseTime>2013-09-05T14:23:45</responseTime><postDate>2013-09-05</postDate><message>Approved</message><fraudResult></fraudResult><giftCardResponse></giftCardResponse></loadResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(loadResponse));
            StringReader reader = new StringReader(xml);
            loadResponse loadResponse = (loadResponse)serializer.Deserialize(reader);

            Assert.Equal("A", loadResponse.reportGroup);
            Assert.Equal("3", loadResponse.id);
            Assert.Equal("4", loadResponse.customerId);
            Assert.Equal(1, loadResponse.cnpTxnId);
            Assert.Equal("000", loadResponse.response);
            Assert.Equal(new DateTime(2013, 9, 5, 14, 23, 45), loadResponse.responseTime);
            Assert.Equal(new DateTime(2013, 9, 5), loadResponse.postDate);
            Assert.Equal("Approved", loadResponse.message);
            Assert.NotNull(loadResponse.fraudResult);
            Assert.NotNull(loadResponse.giftCardResponse);
        }

        [Fact]
        public void TestUnloadResponse()
        {
            String xml = "<unloadResponse reportGroup=\"A\" id=\"3\" customerId=\"4\" duplicate=\"true\" xmlns=\"http://www.vantivcnp.com/schema\"><cnpTxnId>1</cnpTxnId><orderId>2</orderId><response>000</response><responseTime>2013-09-05T14:23:45</responseTime><postDate>2013-09-05</postDate><message>Approved</message><fraudResult></fraudResult><giftCardResponse></giftCardResponse></unloadResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(unloadResponse));
            StringReader reader = new StringReader(xml);
            unloadResponse unloadResponse = (unloadResponse)serializer.Deserialize(reader);

            Assert.Equal("A", unloadResponse.reportGroup);
            Assert.Equal("4", unloadResponse.customerId);
            Assert.Equal(1, unloadResponse.cnpTxnId);
            Assert.Equal("000", unloadResponse.response);
            Assert.Equal(new DateTime(2013, 9, 5, 14, 23, 45), unloadResponse.responseTime);
            Assert.Equal(new DateTime(2013, 9, 5), unloadResponse.postDate);
            Assert.Equal("Approved", unloadResponse.message);
            Assert.NotNull(unloadResponse.fraudResult);
            Assert.NotNull(unloadResponse.giftCardResponse);
        }

        [Fact]
        public void TestGiftCardResponse()
        {
            String xml = "<balanceInquiryResponse reportGroup=\"A\" id=\"3\" customerId=\"4\" xmlns=\"http://www.vantivcnp.com/schema\"><giftCardResponse><availableBalance>1</availableBalance><beginningBalance>2</beginningBalance><endingBalance>3</endingBalance><cashBackAmount>4</cashBackAmount></giftCardResponse></balanceInquiryResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(balanceInquiryResponse));
            StringReader reader = new StringReader(xml);
            balanceInquiryResponse balanceInquiryResponse = (balanceInquiryResponse)serializer.Deserialize(reader);
            giftCardResponse giftCardResponse = balanceInquiryResponse.giftCardResponse;

            Assert.Equal("1", giftCardResponse.availableBalance);
            Assert.Equal("2", giftCardResponse.beginningBalance);
            Assert.Equal("3", giftCardResponse.endingBalance);
            Assert.Equal("4", giftCardResponse.cashBackAmount);
        }

        [Fact]
        public void TestBalanceInquiryResponse()
        {
            String xml = "<balanceInquiryResponse reportGroup=\"A\" id=\"3\" customerId=\"4\" xmlns=\"http://www.vantivcnp.com/schema\"><cnpTxnId>1</cnpTxnId><orderId>2</orderId><response>000</response><responseTime>2013-09-05T14:23:45</responseTime><postDate>2013-09-05</postDate><message>Approved</message><fraudResult></fraudResult><giftCardResponse></giftCardResponse></balanceInquiryResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(balanceInquiryResponse));
            StringReader reader = new StringReader(xml);
            balanceInquiryResponse balanceInquiryResponse = (balanceInquiryResponse)serializer.Deserialize(reader);

            Assert.Equal("A", balanceInquiryResponse.reportGroup);
            Assert.Equal("3", balanceInquiryResponse.id);
            Assert.Equal("4", balanceInquiryResponse.customerId);
            Assert.Equal(1, balanceInquiryResponse.cnpTxnId);
            Assert.Equal("000", balanceInquiryResponse.response);
            Assert.Equal(new DateTime(2013, 9, 5, 14, 23, 45), balanceInquiryResponse.responseTime);
            Assert.Equal(new DateTime(2013, 9, 5), balanceInquiryResponse.postDate);
            Assert.Equal("Approved", balanceInquiryResponse.message);
            Assert.NotNull(balanceInquiryResponse.fraudResult);
            Assert.NotNull(balanceInquiryResponse.giftCardResponse);
        }

        [Fact]
        public void TestDeactivateResponse()
        {
            String xml = "<deactivateResponse reportGroup=\"A\" id=\"3\" customerId=\"4\" xmlns=\"http://www.vantivcnp.com/schema\"><cnpTxnId>1</cnpTxnId><orderId>2</orderId><response>000</response><responseTime>2013-09-05T14:23:45</responseTime><postDate>2013-09-05</postDate><message>Approved</message><fraudResult></fraudResult><giftCardResponse></giftCardResponse></deactivateResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(deactivateResponse));
            StringReader reader = new StringReader(xml);
            deactivateResponse deactivateResponse = (deactivateResponse)serializer.Deserialize(reader);

            Assert.Equal("A", deactivateResponse.reportGroup);
            Assert.Equal("3", deactivateResponse.id);
            Assert.Equal("4", deactivateResponse.customerId);
            Assert.Equal(1, deactivateResponse.cnpTxnId);
            Assert.Equal("000", deactivateResponse.response);
            Assert.Equal(new DateTime(2013, 9, 5, 14, 23, 45), deactivateResponse.responseTime);
            Assert.Equal(new DateTime(2013, 9, 5), deactivateResponse.postDate);
            Assert.Equal("Approved", deactivateResponse.message);
            Assert.NotNull(deactivateResponse.fraudResult);
            Assert.NotNull(deactivateResponse.giftCardResponse);
        }

        [Fact]
        public void TestCreatePlanResponse()
        {
            String xml = @"
<createPlanResponse xmlns=""http://www.vantivcnp.com/schema"">
<cnpTxnId>1</cnpTxnId>
<response>000</response>
<message>Approved</message>
<responseTime>2013-09-05T14:23:45</responseTime>
<planCode>thePlan</planCode>
</createPlanResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(createPlanResponse));
            StringReader reader = new StringReader(xml);
            createPlanResponse createPlanResponse = (createPlanResponse)serializer.Deserialize(reader);

            Assert.Equal("1", createPlanResponse.cnpTxnId);
            Assert.Equal("000", createPlanResponse.response);
            Assert.Equal("Approved", createPlanResponse.message);
            Assert.Equal(new DateTime(2013, 9, 5, 14, 23, 45), createPlanResponse.responseTime);
            Assert.Equal("thePlan", createPlanResponse.planCode);
        }

        [Fact]
        public void TestUpdatePlanResponse()
        {
            String xml = @"
<updatePlanResponse xmlns=""http://www.vantivcnp.com/schema"">
<cnpTxnId>1</cnpTxnId>
<response>000</response>
<message>Approved</message>
<responseTime>2013-09-05T14:23:45</responseTime>
<planCode>thePlan</planCode>
</updatePlanResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(updatePlanResponse));
            StringReader reader = new StringReader(xml);
            updatePlanResponse updatePlanResponse = (updatePlanResponse)serializer.Deserialize(reader);

            Assert.Equal("1", updatePlanResponse.cnpTxnId);
            Assert.Equal("000", updatePlanResponse.response);
            Assert.Equal("Approved", updatePlanResponse.message);
            Assert.Equal(new DateTime(2013, 9, 5, 14, 23, 45), updatePlanResponse.responseTime);
            Assert.Equal("thePlan", updatePlanResponse.planCode);
        }

        [Fact]
        public void TestUpdateSubscriptionResponseCanContainTokenResponse()
        {
            String xml = @"
<updateSubscriptionResponse xmlns=""http://www.vantivcnp.com/schema"">
<cnpTxnId>1</cnpTxnId>
<response>000</response>
<message>Approved</message>
<responseTime>2013-09-05T14:23:45</responseTime>
<subscriptionId>123</subscriptionId>
<tokenResponse>
<cnpToken>123456</cnpToken>
</tokenResponse>
</updateSubscriptionResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(updateSubscriptionResponse));
            StringReader reader = new StringReader(xml);
            updateSubscriptionResponse updateSubscriptionResponse = (updateSubscriptionResponse)serializer.Deserialize(reader);
            Assert.Equal("123", updateSubscriptionResponse.subscriptionId);
            Assert.Equal("123456", updateSubscriptionResponse.tokenResponse.cnpToken);
        }

        [Fact]
        public void TestEnhancedAuthResponseCanContainVirtualAccountNumber()
        {
            String xml = @"
<enhancedAuthResponse xmlns=""http://www.vantivcnp.com/schema"">
<virtualAccountNumber>true</virtualAccountNumber>
</enhancedAuthResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(enhancedAuthResponse));
            StringReader reader = new StringReader(xml);
            enhancedAuthResponse enhancedAuthResponse = (enhancedAuthResponse)serializer.Deserialize(reader);
            Assert.True(enhancedAuthResponse.virtualAccountNumber);
        }

        [Fact]
        public void TestEnhancedAuthResponseWithCardProductType()
        {
            String xml = @"
<enhancedAuthResponse xmlns=""http://www.vantivcnp.com/schema"">
<virtualAccountNumber>true</virtualAccountNumber>
<cardProductType>COMMERCIAL</cardProductType>
</enhancedAuthResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(enhancedAuthResponse));
            StringReader reader = new StringReader(xml);
            enhancedAuthResponse enhancedAuthResponse = (enhancedAuthResponse)serializer.Deserialize(reader);
            Assert.True(enhancedAuthResponse.virtualAccountNumber);
            Assert.Equal(cardProductTypeEnum.COMMERCIAL, enhancedAuthResponse.cardProductType);
        }

        [Fact]
        public void TestEnhancedAuthResponseWithNullableEnumFields()
        {
            String xml = @"
<enhancedAuthResponse xmlns=""http://www.vantivcnp.com/schema"">
<virtualAccountNumber>1</virtualAccountNumber>
</enhancedAuthResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(enhancedAuthResponse));
            StringReader reader = new StringReader(xml);
            enhancedAuthResponse enhancedAuthResponse = (enhancedAuthResponse)serializer.Deserialize(reader);
            Assert.True(enhancedAuthResponse.virtualAccountNumber);
            Assert.Null(enhancedAuthResponse.cardProductType);
            Assert.Null(enhancedAuthResponse.affluence);
        }

        //        [Fact]
        //        public void TestAuthReversalResponseCanContainGiftCardResponse()
        //        {
        //            String xml = @"
        //<authReversalResponse xmlns=""http://www.vantivcnp.com/schema"" id=""theId"" customerId=""theCustomerId"" reportGroup=""theReportGroup"">
        //<cnpTxnId>1</cnpTxnId>
        //<orderId>2</orderId>
        //<response>000</response>
        //<responseTime>2013-09-05T14:23:45</responseTime>
        //<postDate>2013-09-05</postDate>
        //<message>Foo</message>
        //<giftCardResponse>
        //<availableBalance>5</availableBalance>
        //</giftCardResponse>
        //</authReversalResponse>";
        //            XmlSerializer serializer = new XmlSerializer(typeof(authReversalResponse));
        //            StringReader reader = new StringReader(xml);
        //            authReversalResponse authReversalResponse = (authReversalResponse)serializer.Deserialize(reader);
        //            Assert.Equal("theId", authReversalResponse.id);
        //            Assert.Equal("theCustomerId", authReversalResponse.customerId);
        //            Assert.Equal("theReportGroup", authReversalResponse.reportGroup);
        //            Assert.Equal(1, authReversalResponse.cnpTxnId);
        //            Assert.Equal("000", authReversalResponse.response);
        //            Assert.Equal(new DateTime(2013, 9, 5, 14, 23, 45), authReversalResponse.responseTime);
        //            Assert.Equal(new DateTime(2013, 9, 5), authReversalResponse.postDate);
        //            Assert.Equal("Foo", authReversalResponse.message);
        //            Assert.Equal("5", authReversalResponse.giftCardResponse.availableBalance);
        //        }

        [Fact]
        public void TestgiftCardAuthReversalResponseCanContainGiftCardResponse()
        {
            String xml = @"
<giftCardAuthReversalResponse xmlns=""http://www.vantivcnp.com/schema"" id=""theId"" customerId=""theCustomerId"" reportGroup=""theReportGroup"">
<cnpTxnId>1</cnpTxnId>
<orderId>2</orderId>
<response>000</response>
<responseTime>2013-09-05T14:23:45</responseTime>
<postDate>2013-09-05</postDate>
<message>Foo</message>
<giftCardResponse>
<availableBalance>5</availableBalance>
</giftCardResponse>
</giftCardAuthReversalResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(giftCardAuthReversalResponse));
            StringReader reader = new StringReader(xml);
            giftCardAuthReversalResponse giftCardAuthReversalResponse = (giftCardAuthReversalResponse)serializer.Deserialize(reader);
            Assert.Equal("theId", giftCardAuthReversalResponse.id);
            Assert.Equal("theCustomerId", giftCardAuthReversalResponse.customerId);
            Assert.Equal("theReportGroup", giftCardAuthReversalResponse.reportGroup);
            Assert.Equal(1, giftCardAuthReversalResponse.cnpTxnId);
            Assert.Equal("000", giftCardAuthReversalResponse.response);
            Assert.Equal(new DateTime(2013, 9, 5, 14, 23, 45), giftCardAuthReversalResponse.responseTime);
            Assert.Equal(new DateTime(2013, 9, 5), giftCardAuthReversalResponse.postDate);
            Assert.Equal("Foo", giftCardAuthReversalResponse.message);
            Assert.Equal("5", giftCardAuthReversalResponse.giftCardResponse.availableBalance);
        }

        [Fact]
        public void TestDepositReversalResponseCanContainGiftCardResponse()
        {
            String xml = @"
<depositReversalResponse xmlns=""http://www.vantivcnp.com/schema"" id=""theId"" customerId=""theCustomerId"" reportGroup=""theReportGroup"">
<cnpTxnId>1</cnpTxnId>
<orderId>2</orderId>
<response>000</response>
<responseTime>2013-09-05T14:23:45</responseTime>
<postDate>2013-09-05</postDate>
<message>Foo</message>
<giftCardResponse>
<availableBalance>5</availableBalance>
</giftCardResponse>
</depositReversalResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(depositReversalResponse));
            StringReader reader = new StringReader(xml);
            depositReversalResponse depositReversalResponse = (depositReversalResponse)serializer.Deserialize(reader);
            Assert.Equal("theId", depositReversalResponse.id);
            Assert.Equal("theCustomerId", depositReversalResponse.customerId);
            Assert.Equal("theReportGroup", depositReversalResponse.reportGroup);
            Assert.Equal(1, depositReversalResponse.cnpTxnId);
            Assert.Equal("000", depositReversalResponse.response);
            Assert.Equal(new DateTime(2013, 9, 5, 14, 23, 45), depositReversalResponse.responseTime);
            Assert.Equal(new DateTime(2013, 9, 5), depositReversalResponse.postDate);
            Assert.Equal("Foo", depositReversalResponse.message);
            Assert.Equal("5", depositReversalResponse.giftCardResponse.availableBalance);
        }

        [Fact]
        public void TestActivateReversalResponseCanContainGiftCardResponse()
        {
            String xml = @"
<activateReversalResponse xmlns=""http://www.vantivcnp.com/schema"" id=""theId"" customerId=""theCustomerId"" reportGroup=""theReportGroup"">
<cnpTxnId>1</cnpTxnId>
<orderId>2</orderId>
<response>000</response>
<responseTime>2013-09-05T14:23:45</responseTime>
<postDate>2013-09-05</postDate>
<message>Foo</message>
<giftCardResponse>
<availableBalance>5</availableBalance>
</giftCardResponse>
</activateReversalResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(activateReversalResponse));
            StringReader reader = new StringReader(xml);
            activateReversalResponse activateReversalResponse = (activateReversalResponse)serializer.Deserialize(reader);
            Assert.Equal("theId", activateReversalResponse.id);
            Assert.Equal("theCustomerId", activateReversalResponse.customerId);
            Assert.Equal("theReportGroup", activateReversalResponse.reportGroup);
            Assert.Equal(1, activateReversalResponse.cnpTxnId);
            Assert.Equal("000", activateReversalResponse.response);
            Assert.Equal(new DateTime(2013, 9, 5, 14, 23, 45), activateReversalResponse.responseTime);
            Assert.Equal(new DateTime(2013, 9, 5), activateReversalResponse.postDate);
            Assert.Equal("Foo", activateReversalResponse.message);
            Assert.Equal("5", activateReversalResponse.giftCardResponse.availableBalance);
        }

        [Fact]
        public void TestDeactivateReversalResponseCanContainGiftCardResponse()
        {
            String xml = @"
<deactivateReversalResponse xmlns=""http://www.vantivcnp.com/schema"" id=""theId"" customerId=""theCustomerId"" reportGroup=""theReportGroup"">
<cnpTxnId>1</cnpTxnId>
<orderId>2</orderId>
<response>000</response>
<responseTime>2013-09-05T14:23:45</responseTime>
<postDate>2013-09-05</postDate>
<message>Foo</message>
<giftCardResponse>
<availableBalance>5</availableBalance>
</giftCardResponse>
</deactivateReversalResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(deactivateReversalResponse));
            StringReader reader = new StringReader(xml);
            deactivateReversalResponse deactivateReversalResponse = (deactivateReversalResponse)serializer.Deserialize(reader);
            Assert.Equal("theId", deactivateReversalResponse.id);
            Assert.Equal("theCustomerId", deactivateReversalResponse.customerId);
            Assert.Equal("theReportGroup", deactivateReversalResponse.reportGroup);
            Assert.Equal(1, deactivateReversalResponse.cnpTxnId);
            Assert.Equal("000", deactivateReversalResponse.response);
            Assert.Equal(new DateTime(2013, 9, 5, 14, 23, 45), deactivateReversalResponse.responseTime);
            Assert.Equal(new DateTime(2013, 9, 5), deactivateReversalResponse.postDate);
            Assert.Equal("Foo", deactivateReversalResponse.message);
            Assert.Equal("5", deactivateReversalResponse.giftCardResponse.availableBalance);
        }

        [Fact]
        public void TestLoadReversalResponseCanContainGiftCardResponse()
        {
            String xml = @"
<loadReversalResponse xmlns=""http://www.vantivcnp.com/schema"" id=""theId"" customerId=""theCustomerId"" reportGroup=""theReportGroup"">
<cnpTxnId>1</cnpTxnId>
<orderId>2</orderId>
<response>000</response>
<responseTime>2013-09-05T14:23:45</responseTime>
<postDate>2013-09-05</postDate>
<message>Foo</message>
<giftCardResponse>
<availableBalance>5</availableBalance>
</giftCardResponse>
</loadReversalResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(loadReversalResponse));
            StringReader reader = new StringReader(xml);
            loadReversalResponse loadReversalResponse = (loadReversalResponse)serializer.Deserialize(reader);
            Assert.Equal("theId", loadReversalResponse.id);
            Assert.Equal("theCustomerId", loadReversalResponse.customerId);
            Assert.Equal("theReportGroup", loadReversalResponse.reportGroup);
            Assert.Equal(1, loadReversalResponse.cnpTxnId);
            Assert.Equal("000", loadReversalResponse.response);
            Assert.Equal(new DateTime(2013, 9, 5, 14, 23, 45), loadReversalResponse.responseTime);
            Assert.Equal(new DateTime(2013, 9, 5), loadReversalResponse.postDate);
            Assert.Equal("Foo", loadReversalResponse.message);
            Assert.Equal("5", loadReversalResponse.giftCardResponse.availableBalance);
        }

        [Fact]
        public void TestUnloadReversalResponseCanContainGiftCardResponse()
        {
            String xml = @"
<unloadReversalResponse xmlns=""http://www.vantivcnp.com/schema"" id=""theId"" customerId=""theCustomerId"" reportGroup=""theReportGroup"">
<cnpTxnId>1</cnpTxnId>
<orderId>2</orderId>
<response>000</response>
<responseTime>2013-09-05T14:23:45</responseTime>
<postDate>2013-09-05</postDate>
<message>Foo</message>
<giftCardResponse>
<availableBalance>5</availableBalance>
</giftCardResponse>
</unloadReversalResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(unloadReversalResponse));
            StringReader reader = new StringReader(xml);
            unloadReversalResponse unloadReversalResponse = (unloadReversalResponse)serializer.Deserialize(reader);
            Assert.Equal("theId", unloadReversalResponse.id);
            Assert.Equal("theCustomerId", unloadReversalResponse.customerId);
            Assert.Equal("theReportGroup", unloadReversalResponse.reportGroup);
            Assert.Equal(1, unloadReversalResponse.cnpTxnId);
            Assert.Equal("000", unloadReversalResponse.response);
            Assert.Equal(new DateTime(2013, 9, 5, 14, 23, 45), unloadReversalResponse.responseTime);
            Assert.Equal(new DateTime(2013, 9, 5), unloadReversalResponse.postDate);
            Assert.Equal("Foo", unloadReversalResponse.message);
            Assert.Equal("5", unloadReversalResponse.giftCardResponse.availableBalance);
        }

        [Fact]
        public void TestActivateResponseCanContainVirtualGiftCardResponse()
        {
            String xml = @"
<activateResponse reportGroup=""A"" id=""3"" customerId=""4"" duplicate=""true"" xmlns=""http://www.vantivcnp.com/schema"">
<cnpTxnId>1</cnpTxnId>
<orderId>2</orderId>
<response>000</response>
<responseTime>2013-09-05T14:23:45</responseTime>
<message>Approved</message>
<virtualGiftCardResponse>
<accountNumber>123</accountNumber>
</virtualGiftCardResponse>
</activateResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(activateResponse));
            StringReader reader = new StringReader(xml);
            activateResponse activateResponse = (activateResponse)serializer.Deserialize(reader);

            Assert.Equal("123",activateResponse.virtualGiftCardResponse.accountNumber);
        }

        [Fact]
        public void TestVirtualGiftCardResponse()
        {
            String xml = @"
<activateResponse reportGroup=""A"" id=""3"" customerId=""4"" duplicate=""true"" xmlns=""http://www.vantivcnp.com/schema"">
<cnpTxnId>1</cnpTxnId>
<orderId>2</orderId>
<response>000</response>
<responseTime>2013-09-05T14:23:45</responseTime>
<message>Approved</message>
<virtualGiftCardResponse>
<accountNumber>123</accountNumber>
<cardValidationNum>abc</cardValidationNum>
</virtualGiftCardResponse>
</activateResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(activateResponse));
            StringReader reader = new StringReader(xml);
            activateResponse activateResponse = (activateResponse)serializer.Deserialize(reader);

            Assert.Equal("123", activateResponse.virtualGiftCardResponse.accountNumber);
            Assert.Equal("abc", activateResponse.virtualGiftCardResponse.cardValidationNum);
        }

        [Fact]
        public void TestAccountUpdaterResponse()
        {
            String xml = @"
<authorizationResponse xmlns=""http://www.vantivcnp.com/schema"">
<accountUpdater>
<extendedCardResponse>
<message>TheMessage</message>
<code>TheCode</code>
</extendedCardResponse>
<newCardInfo>
<type>VI</type>
<number>4100000000000000</number>
<expDate>1000</expDate>
</newCardInfo>
<originalCardInfo>
<type>MC</type>
<number>5300000000000000</number>
<expDate>1100</expDate>
</originalCardInfo>
</accountUpdater>
</authorizationResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(authorizationResponse));
            StringReader reader = new StringReader(xml);
            authorizationResponse authorizationResponse = (authorizationResponse)serializer.Deserialize(reader);
            Assert.Equal("TheMessage", authorizationResponse.accountUpdater.extendedCardResponse.message);
            Assert.Equal("TheCode", authorizationResponse.accountUpdater.extendedCardResponse.code);
            Assert.Equal(methodOfPaymentTypeEnum.VI, authorizationResponse.accountUpdater.newCardInfo.type);
            Assert.Equal("4100000000000000", authorizationResponse.accountUpdater.newCardInfo.number);
            Assert.Equal("1000", authorizationResponse.accountUpdater.newCardInfo.expDate);
            Assert.Equal(methodOfPaymentTypeEnum.MC, authorizationResponse.accountUpdater.originalCardInfo.type);
            Assert.Equal("5300000000000000", authorizationResponse.accountUpdater.originalCardInfo.number);
            Assert.Equal("1100", authorizationResponse.accountUpdater.originalCardInfo.expDate);
        }
    }
}
