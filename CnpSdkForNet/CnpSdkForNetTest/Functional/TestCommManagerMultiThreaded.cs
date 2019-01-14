using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Cnp.Sdk.Test.Functional
{
    internal class TestCommManagerMultiThreaded
    {
        List<Thread> testPool = new List<Thread>();

        int threadCount = 100;
        int cycleCount = 1000;
        Dictionary<string, string> _config;
        [TestFixtureSetUp]
        public void setup() {
            CommManager.reset();
            _config = new Dictionary<string, string>
                {
                    {"proxyHost","websenseproxy"},
                    {"proxyPort","8080"},
                    {"multiSite", "true"},
                    {"printxml", "true"},
                    {"printMultiSiteDebug", "false"},
                    { "url", Properties.Settings.Default.url }
        };
        }
        
        

        [Test]
        public void testMultiThreaded()
        {

            try { 
                
                for (int x = 0; x < threadCount; x++)
                {
                    performanceTest pt = new performanceTest(1000 + x, cycleCount, _config);
                    ThreadStart threadDelegate = new ThreadStart(pt.runPerformanceTest);
                    Thread t = new Thread(threadDelegate);
                    testPool.Add(t);
                }
            }    catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            performTest();

        }

        class performanceTest
        {
            long threadId;
            long requestCount = 0;
            long successCount = 0;
            long failedCount = 0;
            int cycleCount;
            Dictionary<string, string> config;

            public performanceTest(long idNumber, int numCycles, Dictionary<string,string> props)
            {
                threadId = idNumber;
                config = props;
                cycleCount = numCycles;
            }

            public void runPerformanceTest()
            {
                Random rand = new Random();
                long startTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                long totalTransactionTime = 0;

                for (int n = 0; n < cycleCount; n++)
                {
                    requestCount++;
                    RequestTarget target = CommManager.instance(config).findUrl();
                    try
                    {
                        int sleepTime = 100 + rand.Next(500);
                        totalTransactionTime += sleepTime;
                        Thread.Sleep(sleepTime);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                    CommManager.instance(config).reportResult(target, CommManager.REQUEST_RESULT_RESPONSE_RECEIVED, 200);
                }
                long duration = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond - startTime;
                Console.WriteLine("Thread " + threadId + " completed. Total Requests:" + requestCount + "  Elapsed Time:" + (duration / 1000) + " secs    Average Txn Time:" + (totalTransactionTime / requestCount) + " ms");
            }
        }

        

        public void performTest()
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

    }

}
