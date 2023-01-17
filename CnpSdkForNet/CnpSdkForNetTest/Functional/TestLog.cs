using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Threading;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestLog
    {
        private CnpOnline _cnp;

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            _cnp = new CnpOnline();
        }

        [Test]
        public void MultithreadLogWithCards()
        {
            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < 10; i++)
            {
                Thread t = new Thread(new ParameterizedThreadStart(SingleThreadTest));
                t.Start(i);
                threads.Add(t);
            }

            foreach (Thread t in threads)
            {
                t.Join();
            }
            
            TestContext.Progress.WriteLine("Test finished");
        }

        [Test]
        public void LogTest()
        {
            for (int i = 0; i < 10; i++)
            {
                SingleThreadTest(0);
            }
        }

        private void SingleThreadTest(object threadNum)
        {
            var num = (int) threadNum;
            
            for (var i = 0; i < 100; i++)
            {
                TestContext.Progress.WriteLine("Thread [" + num + "] Transaction [" + i + "]");
                var creditObj = new credit
                {
                    id = "1",
                    reportGroup = "planets",
                    amount = 106,
                    orderId = "2111",
                    orderSource = orderSourceType.ecommerce,
                    card = new cardType
                    {
                        type = methodOfPaymentTypeEnum.VI,
                        number = "4100000000000001",
                        expDate = "1210"
                    }
                };

                var response = _cnp.Credit(creditObj);
                TestContext.Progress.WriteLine("Response " + num + ":" + i + " received: " + response.message);
                Assert.AreEqual("Approved", response.message);
                Thread.Sleep(0);
            }
            TestContext.Progress.WriteLine("Thread [" + num + "] end of loop");
            
        }
    }
}