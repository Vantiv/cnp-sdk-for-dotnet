
[![](https://img.shields.io/github/release/vantiv/cnp-sdk-for-dotnet.svg)](https://github.com/Vantiv/cnp-sdk-for-dotnet/releases/latest)
![NuGet](https://img.shields.io/nuget/v/Vantiv.CnpSdkForNet.svg?style=plastic) 
[![GitHub issues](https://img.shields.io/github/issues/Vantiv/cnp-sdk-for-dotnet.svg)](https://github.com/Vantiv/cnp-sdk-for-dotnet/issues) [![Main distribution](https://img.shields.io/badge/zip-download-blue.svg)](https://github.com/Vantiv/cnp-sdk-for-dotnet/releases/latest)




Vantiv eCommerce .NET SDK
=====================

> **WARNING:**
> All major version changes require recertification to the new version. Once certified for the use of a new version, Vantiv modifies your Merchant Profile, allowing you to submit transaction to the Production Environment using the new version. Updating your code without recertification and modification of your Merchant Profile will result in transaction declines. Please consult you Implementation Analyst for additional information about this process.

About Vantiv eCommerce
------------
[Vantiv eCommerce](https://developer.vantiv.com/community/ecommerce) powers the payment processing engines for leading companies that sell directly to consumers through  internet retail, direct response marketing (TV, radio and telephone), and online services. Vantiv eCommerce is the leading authority in card-not-present (CNP) commerce, transaction processing and merchant services.


About this SDK
--------------
The Vantiv eCommerce .NET SDK is a C# implementation of the [CNP](https://developer.vantiv.com/community/ecommerce) XML API. This SDK was created to make it as easy as possible to connect and process your payments with Vantiv eCommerce. This SDK utilizes  the HTTPS protocol to securely connect to Vantiv eCommerce. Using the SDK requires coordination with the Vantiv eCommerce team in order to be provided with credentials for accessing our systems.

Each .NET SDK release supports all of the functionality present in the associated CNP XML version (e.g., SDK v12.4.0 supports Vantiv eCommerce XML v12.4). Please see the online copy of our XSD for CNP XML to get more details on what the Vantiv eCommerce payments engine supports.

This SDK is implemented to support the .NET plaform, including C#, VB.NET and Managed C++ and was created by Vantiv eCommerce. Its intended use is for online transactions processing utilizing your account on the Vantiv eComerce payments engine.

See LICENSE file for details on using this software.

Source Code available from : https://github.com/Vantiv/cnp-sdk-for-dotNet

SDK can be tested in [sandbox environment](https://www.testvantivcnp.com/sandbox/communicator/online). Sandbox does not require valid credentials to begin testing. 

Please contact [Vantiv eCommerce](http://developer.vantiv.com/community/ecommerce) to receive valid merchant credentials in order to run tests successfully or if you require assistance in any way.  We are reachable at sdksupport@fisglobal.com

Setup
-----

1. To install it, just copy CnpSdkForDotNet.dll into your Visual Studio referernces. 

2. You can configure it statically by modifying CnpSdkForDotNet.dll.config or at runtime using the CnpOnline(Dictionary) constructor. If you are just trying it out, the username, password and merchant id don't matter, and you should choose the sandbox url at https://www.testvantivcnp.com/sandbox/communicator/online.

Configuration
------ 
Check out the configuration [gist](https://gist.github.com/VantivSDK/38c7b8f3ea135c036a6a4322b9ddda5e) to get more information on the configurable parameters used by the SDK.


Test
------

1. Create a c# class similar to:  

```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cnp.Sdk;

    class Example
    {
[STAThread]
        public static void Main(String[] args)
        {
            CnpOnline cnp = new CnpOnline();
            sale sale = new sale();
            sale.orderId = "1";
            sale.id = "1";
            sale.amount = 10010;
            sale.orderSource = orderSourceType.ecommerce;
            contact contact = new contact();
            contact.name = "John Smith";
            contact.addressLine1 = "1 Main St.";
            contact.city = "Burlington";
            contact.state = "MA";
            contact.zip = "01803-3747";
            contact.country = countryTypeEnum.US;
            sale.billToAddress = contact;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4457010000000009";
            card.expDate = "0112";
            card.cardValidationNum = "349";
            sale.card = card;

            saleResponse response = cnp.Sale(sale);
            //Display Results
            Console.WriteLine("Response: " + response.response);
            Console.WriteLine("Message: " + response.message);
            Console.WriteLine("Cnp Transaction Id: " + response.cnpTxnId);
            Console.ReadLine();
        }
    }

```

2. Compile and run this file.  You should see the following result:

    Response: 000
    Message: Approved
    Cnp Transaction ID: <your-numeric-cnp-txn-id>

Examples
--------

More examples can be found [Here](http://vantiv.github.io/dotnet/) or in [Functional and Unit Tests](https://github.com/Vantiv/cnp-sdk-for-dotNet/tree/master/CnpSdkForNet/CnpSdkForNetTest)

Support
-------
Please contact Vantiv eCommerce with any further questions.   You can reach us at sdksupport@fisglobal.com.
