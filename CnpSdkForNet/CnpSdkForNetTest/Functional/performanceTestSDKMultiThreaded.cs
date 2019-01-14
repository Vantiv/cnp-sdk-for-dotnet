using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Cnp.Sdk.Test.Functional
{
    class performanceTestSDKMultiThreaded
    {
        List<Thread> testPool = new List<Thread>();

        static String merchantId = "07103229";

        //Before doing any releases, run this test. it times out on jenkins, but should run locally (in several hours). 
        //if it runs locally, you can put the ignore tag back and push it
        [Ignore] 
        [Test]
        public void runTest()
        {
            for (int x = 0; x < 50; x++)
            {
                PerformanceTest pt = new PerformanceTest(1000 + x);
                ThreadStart threadDelegate = new ThreadStart(pt.runPerformanceTest);
                Thread t = new Thread(threadDelegate);
                testPool.Add(t);
            }
            PerformTest();
        }

        public void PerformTest()
        {
            foreach (Thread t in testPool)
            {
                t.Start();
            }

            // wait for them to finish
            Boolean allDone = false;
            while (!allDone)
            {
                int doneCount = 0;
                foreach (Thread t in testPool)
                {
                    if (t.IsAlive == false)
                    {
                        doneCount++;
                    }
                }
                if (doneCount == testPool.Count())
                {
                    allDone = true;
                }
                else
                {
                    try
                    {
                        Thread.Sleep(1000);
                    }
                    catch (Exception e)
                    {
                        // TODO Auto-generated catch block
                        Console.WriteLine(e.ToString());
                    }
                }
            }
            Console.WriteLine("All test threads have completed");
        }

        class PerformanceTest
        {
            CnpOnline cnp;
            long threadId;
            long requestCount = 0;
            long successCount = 0;
            long failedCount = 0;
            public PerformanceTest(long idNumber)
            {
                threadId = idNumber;
                Dictionary<string, string> _config = new Dictionary<string, string>();
                try
                {
                    CommManager.reset();
                    _config = new Dictionary<string, string>
                    {
                        {"proxyHost","websenseproxy"},
                        {"proxyPort","8080"},
                        {"multiSite", "true"},
                        {"printxml", "false"},
                        {"printMultiSiteDebug", "false"},
                        {"merchantId", "101" },
                        {"username", "DOTNET"},
                        {"password", "TESTCASE"}
                    };
                    cnp = new CnpOnline(_config);
                }
                catch (Exception e)
                {
                    // TODO Auto-generated catch block
                    Console.WriteLine(e.ToString());
                }
            }

            public void runPerformanceTest()
            {
                Random rand = new Random();
                long startTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                long totalTransactionTime = 0;
                for (int n = 0; n < 1000; n++)
                {
                    totalTransactionTime += doCycle();
                    try
                    {
                        int sleepTime = rand.Next(50);
                        Thread.Sleep(sleepTime);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
                long duration = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond - startTime;
                Console.WriteLine("Thread " + threadId + " completed. Total Requests:" + requestCount + "  Success:" + successCount + "  Failed:" + failedCount + "  Elapsed Time:" + (duration / 1000) + " secs    Average Txn Time:" + (totalTransactionTime / requestCount) + " ms");
            }

            private long doCycle()
            {
                requestCount++;
                authorization authorization = new authorization();
                authorization.reportGroup = "123456";
                authorization.orderId = ("" + threadId + "-" + DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);
                authorization.amount = (106L);
                authorization.orderSource = (orderSourceType.ecommerce);
                authorization.id = "id" + threadId;
                cardType card = new cardType();
                card.type = methodOfPaymentTypeEnum.VI;
                card.number = "4100000000000000";
                card.expDate = "1210";
                authorization.card = card;

                long startTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                authorizationResponse response = cnp.Authorize(authorization);
                long responseTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond - startTime;
                Assert.AreEqual("123456", response.reportGroup);
                if (response.response =="000")
                {
                    successCount++;
                }
                else
                {
                    failedCount++;
                }
                return responseTime;
            }
        }
    }
}
