using System.Collections.Generic;
using NUnit.Framework;

namespace Cnp.Sdk.Test.Certification
{
    [TestFixture]
    class TestCert4Echeck
    {
        private CnpOnline cnp;

        [OneTimeSetUp]
        public void SetUp()
        {
            EnvironmentVariableTestFlags.RequirePreliveOnlineTestsEnabled();
            
            var existingConfig = new ConfigManager().getConfig();
            Dictionary<string, string> config = new Dictionary<string, string>();
            config.Add("url", "https://payments.vantivprelive.com/vap/communicator/online");
            config.Add("reportGroup", "Default Report Group");
            config.Add("username", existingConfig["username"]);
            config.Add("timeout", "20000");
            config.Add("merchantId", existingConfig["merchantId"]);
            config.Add("password",existingConfig["password"]);
            config.Add("printxml", "true");
            config.Add("logFile", null);
            config.Add("neuterAccountNums", null);
            config.Add("proxyHost", "");
            config.Add("proxyPort", "");
            
            ConfigManager configManager = new ConfigManager(config);
            cnp = new CnpOnline(configManager.getConfig());
        }

        [OneTimeTearDown]
        public void Dispose()
        {
            Communications.DisposeHttpClient();
        }

        [Test]
        public void Test37()
        {
            echeckVerification verification = new echeckVerification();
            verification.id = "1";
            verification.orderId = "37";
            verification.amount = 3001;
            verification.orderSource = orderSourceType.telephone;
            contact billToAddress = new contact();
            billToAddress.firstName = "Tom";
            billToAddress.lastName = "Black";
            verification.billToAddress = billToAddress;
            echeckType echeck = new echeckType();
            echeck.accNum = "10@BC99999";
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.routingNum = "053100300";
            verification.echeck = echeck;

            echeckVerificationResponse response = cnp.EcheckVerification(verification);
            Assert.AreEqual("301", response.response);
            Assert.AreEqual("Invalid Account Number", response.message);
        }

        [Test]
        public void Test38()
        {
            echeckVerification verification = new echeckVerification();
            verification.id = "1";
            verification.orderId = "38";
            verification.amount = 3002;
            verification.orderSource = orderSourceType.telephone;
            contact billToAddress = new contact();
            billToAddress.firstName = "John";
            billToAddress.lastName = "Smith";
            billToAddress.phone = "999-999-9999";
            verification.billToAddress = billToAddress;
            echeckType echeck = new echeckType();
            echeck.accNum = "1099999999";
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.routingNum = "053000219";
            verification.echeck = echeck;

            echeckVerificationResponse response = cnp.EcheckVerification(verification);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void Test39()
        {
            echeckVerification verification = new echeckVerification();
            verification.id = "1";
            verification.orderId = "39";
            verification.amount = 3003;
            verification.orderSource = orderSourceType.telephone;
            contact billToAddress = new contact();
            billToAddress.firstName = "Robert";
            billToAddress.lastName = "Jones";
            billToAddress.companyName = "Good Goods Inc";
            billToAddress.phone = "9999999999";
            verification.billToAddress = billToAddress;
            echeckType echeck = new echeckType();
            echeck.accNum = "3099999999";
            echeck.accType = echeckAccountTypeEnum.Corporate;
            echeck.routingNum = "053100300";
            verification.echeck = echeck;

            echeckVerificationResponse response = cnp.EcheckVerification(verification);
            Assert.AreEqual("950", response.response);
        }

        [Test]
        public void Test40()
        {
            echeckVerification verification = new echeckVerification();
            verification.id = "1";
            verification.orderId = "40";
            verification.amount = 3004;
            verification.orderSource = orderSourceType.telephone;
            contact billToAddress = new contact();
            billToAddress.firstName = "Peter";
            billToAddress.lastName = "Green";
            billToAddress.companyName = "Green Co";
            billToAddress.phone = "9999999999";
            verification.billToAddress = billToAddress;
            echeckType echeck = new echeckType();
            echeck.accNum = "8099999999";
            echeck.accType = echeckAccountTypeEnum.Corporate;
            echeck.routingNum = "063102152";
            verification.echeck = echeck;

            echeckVerificationResponse response = cnp.EcheckVerification(verification);
            Assert.AreEqual("951", response.response);
            Assert.AreEqual("Absolute Decline", response.message);
        }

        [Test]
        public void Test41()
        {
            echeckSale sale = new echeckSale();
            sale.id = "1";
            sale.orderId = "41";
            sale.amount = 2008;
            sale.orderSource = orderSourceType.telephone;
            contact billToAddress = new contact();
            billToAddress.firstName = "Mike";
            billToAddress.middleInitial = "J";
            billToAddress.lastName = "Hammer";
            sale.billToAddress = billToAddress;
            echeckType echeck = new echeckType();
            echeck.accNum = "10@BC99999";
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.routingNum = "053100300";
            sale.echeck = echeck;

            echeckSalesResponse response = cnp.EcheckSale(sale);
            Assert.AreEqual("301", response.response);
            Assert.AreEqual("Invalid Account Number", response.message);
        }

        [Test]
        public void Test42()
        {
            echeckSale sale = new echeckSale();
            sale.id = "1";
            sale.orderId = "42";
            sale.amount = 2004;
            sale.orderSource = orderSourceType.telephone;
            contact billToAddress = new contact();
            billToAddress.firstName = "Tom";
            billToAddress.lastName = "Black";
            sale.billToAddress = billToAddress;
            echeckType echeck = new echeckType();
            echeck.accNum = "4099999992";
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.routingNum = "211370545";
            sale.echeck = echeck;

            echeckSalesResponse response = cnp.EcheckSale(sale);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void Test43()
        {
            echeckSale sale = new echeckSale();
            sale.id = "1";
            sale.orderId = "43";
            sale.amount = 2007;
            sale.orderSource = orderSourceType.telephone;
            contact billToAddress = new contact();
            billToAddress.firstName = "Peter";
            billToAddress.lastName = "Green";
            billToAddress.companyName = "Green Co";
            sale.billToAddress = billToAddress;
            echeckType echeck = new echeckType();
            echeck.accNum = "6099999992";
            echeck.accType = echeckAccountTypeEnum.Corporate;
            echeck.routingNum = "211370545";
            sale.echeck = echeck;

            echeckSalesResponse response = cnp.EcheckSale(sale);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void Test44()
        {
            echeckSale sale = new echeckSale();
            sale.id = "1";
            sale.orderId = "44";
            sale.amount = 2009;
            sale.orderSource = orderSourceType.telephone;
            contact billToAddress = new contact();
            billToAddress.firstName = "Peter";
            billToAddress.lastName = "Green";
            billToAddress.companyName = "Green Co";
            sale.billToAddress = billToAddress;
            echeckType echeck = new echeckType();
            echeck.accNum = "9099999992";
            echeck.accType = echeckAccountTypeEnum.Corporate;
            echeck.routingNum = "053133052";
            sale.echeck = echeck;

            echeckSalesResponse response = cnp.EcheckSale(sale);
            Assert.AreEqual("900", response.response);
            Assert.AreEqual("Invalid Bank Routing Number", response.message);
        }

        [Test]
        public void Test45()
        {
            echeckCredit credit = new echeckCredit();
            credit.id = "1";
            credit.orderId = "45";
            credit.amount = 1001;
            credit.orderSource = orderSourceType.telephone;
            contact billToAddress = new contact();
            billToAddress.firstName = "John";
            billToAddress.lastName = "Smith";
            credit.billToAddress = billToAddress;
            echeckType echeck = new echeckType();
            echeck.accNum = "10@BC99999";
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.routingNum = "053100300";
            credit.echeck = echeck;

            echeckCreditResponse response = cnp.EcheckCredit(credit);
            Assert.AreEqual("301", response.response);
        }

        [Test]
        public void Test46()
        {
            echeckCredit credit = new echeckCredit();
            credit.id = "1";
            credit.orderId = "46";
            credit.amount = 1003;
            credit.orderSource = orderSourceType.telephone;
            contact billToAddress = new contact();
            billToAddress.firstName = "Robert";
            billToAddress.lastName = "Jones";
            billToAddress.companyName = "Widget Inc";
            credit.billToAddress = billToAddress;
            echeckType echeck = new echeckType();
            echeck.accNum = "3099999999";
            echeck.accType = echeckAccountTypeEnum.Corporate;
            echeck.routingNum = "063102152";
            credit.echeck = echeck;

            echeckCreditResponse response = cnp.EcheckCredit(credit);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void Test47()
        {
            echeckCredit credit = new echeckCredit();
            credit.id = "1";
            credit.orderId = "47";
            credit.amount = 1007;
            credit.orderSource = orderSourceType.telephone;
            contact billToAddress = new contact();
            billToAddress.firstName = "Peter";
            billToAddress.lastName = "Green";
            billToAddress.companyName = "Green Co";
            credit.billToAddress = billToAddress;
            echeckType echeck = new echeckType();
            echeck.accNum = "6099999993";
            echeck.accType = echeckAccountTypeEnum.Corporate;
            echeck.routingNum = "211370545";
            credit.echeck = echeck;

            echeckCreditResponse response = cnp.EcheckCredit(credit);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void Test48()
        {
            echeckCredit credit = new echeckCredit();
            credit.id = "1";
            credit.cnpTxnId = 430000000000000001L;

            echeckCreditResponse response = cnp.EcheckCredit(credit);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void Test49()
        {
            echeckCredit credit = new echeckCredit();
            credit.id = "1";
            credit.cnpTxnId = 2L;

            echeckCreditResponse response = cnp.EcheckCredit(credit);
            Assert.AreEqual("000", response.response);
        }
            
    }
}
