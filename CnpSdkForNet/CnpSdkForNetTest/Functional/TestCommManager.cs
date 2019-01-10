using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cnp.Sdk.Test.Functional
{
    public class TestCommManager
    {
        public const String site1Url = "https://multisite1.com";
        public const String site2Url = "https://multisite2.com";
        public const String legacyUrl = "https://legacy.com";

        [Fact]
        public void testInstanceLegacy()
        {
            Dictionary<string,string> _config = new Dictionary<string, string>
            {
                {"proxyHost","websenseproxy"},
                {"proxyPort","8080"},
                {"multiSite", "false"},
                {"printxml", "false"},
                {"printMultiSiteDebug", "true"},
                {"merchantId", "101" },
                {"username", "DOTNET"},
                {"password", "TESTCASE"},
                {"url",legacyUrl }
            };
            CommManager.reset();
	        CommManager cmg = CommManager.instance(_config);
            Assert.NotNull(cmg);
            Assert.False(cmg.getMultiSite());
            Assert.Equal(legacyUrl, cmg.getLegacyUrl());
            
            Dictionary<string, string> _config2 = new Dictionary<string, string>
            {
                {"proxyHost","websenseproxy"},
                {"proxyPort","8080"},
                {"multiSite", "false"},
                {"printxml", "false"},
                {"printMultiSiteDebug", "true"},
                {"merchantId", "101" },
                {"username", "DOTNET"},
                {"password", "TESTCASE"},
                {"url", "https://nowhere.com" }
            };
	        CommManager cmg2 = CommManager.instance(_config2);
            Assert.Equal(legacyUrl, cmg2.getLegacyUrl());  // should be same manager as previous
        }

        [Fact]
        public void testInstanceMultiSite()
        {
            Dictionary<string, string> _config = new Dictionary<string, string>
            {
                {"proxyHost","websenseproxy"},
                {"proxyPort","8080"},
                {"multiSite", "true"},
                {"printxml", "false"},
                {"printMultiSiteDebug", "true"},
                {"merchantId", "101" },
                {"username", "DOTNET"},
                {"password", "TESTCASE"},
                {"url",legacyUrl },
                {"multiSiteUrl1", site1Url},
                {"multiSiteUrl2", site2Url },
                {"multiSiteErrorThreshold", "4"},
                {"maxHoursWithoutSwitch", "48"}
            };
            CommManager.reset();
            CommManager cmg = CommManager.instance(_config);
            Assert.NotNull(cmg);
            Assert.True(cmg.getMultiSite());
            Assert.Equal(cmg.getMultiSiteThreshold(), 4);
            Assert.Equal(cmg.getMultiSiteUrls().Count(), 2);
        }

        [Fact]
        public void testInstanceMultiSiteNoUrls()
        {
            Dictionary<string, string> _config = new Dictionary<string, string>
            {
                {"proxyHost","websenseproxy"},
                {"proxyPort","8080"},
                {"multiSite", "true"},
                {"printxml", "false"},
                {"printMultiSiteDebug", "true"},
                {"merchantId", "101" },
                {"username", "DOTNET"},
                {"password", "TESTCASE"},
                {"url",legacyUrl }
                
            };
            CommManager.reset();
            CommManager cmg = CommManager.instance(_config);
            Assert.NotNull(cmg);
            Assert.False(cmg.getMultiSite());
        }

        [Fact]
        public void testInstanceMultiSiteDefaultProps()
        {
            Dictionary<string, string> _config = new Dictionary<string, string>
            {
                {"proxyHost","websenseproxy"},
                {"proxyPort","8080"},
                {"multiSite", "true"},
                {"printxml", "false"},
                {"printMultiSiteDebug", "true"},
                {"merchantId", "101" },
                {"username", "DOTNET"},
                {"password", "TESTCASE"},
                {"url",legacyUrl },
                {"multiSiteUrl1", site1Url},
                {"multiSiteUrl2", site2Url },
                {"multiSiteErrorThreshold", "102"},
                {"maxHoursWithoutSwitch", "500"}

            };
            CommManager.reset();
            CommManager cmg = CommManager.instance(_config);
            Assert.NotNull(cmg);
            Assert.True(cmg.getMultiSite());
            Assert.Equal(5, cmg.getMultiSiteThreshold());
            Assert.Equal(48, cmg.getMaxHoursWithoutSwitch());
        }

        [Fact]
        public void testInstanceMultiSiteOutOfRange()
        {
            Dictionary<string, string> _config = new Dictionary<string, string>
            {
                {"proxyHost","websenseproxy"},
                {"proxyPort","8080"},
                {"multiSite", "true"},
                {"printxml", "false"},
                {"printMultiSiteDebug", "true"},
                {"merchantId", "101" },
                {"username", "DOTNET"},
                {"password", "TESTCASE"},
                {"url",legacyUrl },
                {"multiSiteUrl1", site1Url},
                {"multiSiteUrl2", site2Url },
            };
            CommManager.reset();
            CommManager cmg = CommManager.instance(_config);
            Assert.NotNull(cmg);
            Assert.True(cmg.getMultiSite());
        }

        [Fact]
        public void testFindUrl_Legacy()
        {
            Dictionary<string, string> _config = new Dictionary<string, string>
            {
                {"proxyHost","websenseproxy"},
                {"proxyPort","8080"},
                {"multiSite", "false"},
                {"printxml", "false"},
                {"printMultiSiteDebug", "true"},
                {"merchantId", "101" },
                {"username", "DOTNET"},
                {"password", "TESTCASE"},
                {"url",legacyUrl }
            };
            CommManager.reset();
            CommManager cmg = CommManager.instance(_config);
            Assert.NotNull(cmg);
            Assert.False(cmg.getMultiSite());
            RequestTarget rt = cmg.findUrl();
            Assert.Equal(legacyUrl, rt.getUrl());
        }

        [Fact]
        public void testFindUrl_MultiSite1()
        { 
            Dictionary<string, string> _config = new Dictionary<string, string>
            {
                {"proxyHost","websenseproxy"},
                {"proxyPort","8080"},
                {"printxml", "false"},
                {"merchantId", "101" },
                {"username", "DOTNET"},
                {"password", "TESTCASE"},

                {"multiSite", "true"},
                {"printMultiSiteDebug", "true"},
                {"url",legacyUrl },
                {"multiSiteUrl1", site1Url},
                {"multiSiteUrl2", site2Url },
                {"multiSiteErrorThreshold", "4"},
                {"maxHoursWithoutSwitch", "48"}
            };
            CommManager.reset();
            CommManager cmg = CommManager.instance(_config);
            Assert.NotNull(cmg);
            Assert.True(cmg.getMultiSite());
            RequestTarget rt = cmg.findUrl();
            Assert.Equal(cmg.getMultiSiteUrls()[cmg.getCurrentMultiSiteUrlIndex()], rt.getUrl());
            Assert.True(rt.getUrl().Equals(site1Url) || rt.getUrl().Equals(site2Url));
        }

        [Fact]
        public void testFindUrl_MultiSite2()
        {
            // test that url is switched when errors reach threshold
            Dictionary<string, string> _config = new Dictionary<string, string>
            {
                {"proxyHost","websenseproxy"},
                {"proxyPort","8080"},
                {"printxml", "false"},
                {"merchantId", "101" },
                {"username", "DOTNET"},
                {"password", "TESTCASE"},

                {"multiSite", "true"},
                {"printMultiSiteDebug", "false"},
                {"url",legacyUrl },
                {"multiSiteUrl1", site1Url},
                {"multiSiteUrl2", site2Url },
                {"multiSiteErrorThreshold", "3"},
                {"maxHoursWithoutSwitch", "48"}
            };
            CommManager.reset();
            CommManager cmg = CommManager.instance(_config);
            Assert.NotNull(cmg);
            Assert.True(cmg.getMultiSite());
            Assert.Equal(cmg.getMultiSiteThreshold(), 3);

            RequestTarget rt1 = cmg.findUrl();
            Assert.Equal(cmg.getMultiSiteUrls()[cmg.getCurrentMultiSiteUrlIndex()], rt1.getUrl());
            cmg.reportResult(rt1, CommManager.REQUEST_RESULT_RESPONSE_TIMEOUT, 0);
            RequestTarget rt2 = cmg.findUrl();
            Assert.Equal(rt1.getUrl(), rt2.getUrl());
            cmg.reportResult(rt2, CommManager.REQUEST_RESULT_RESPONSE_TIMEOUT, 0);
            RequestTarget rt3 = cmg.findUrl();
            Assert.Equal(rt1.getUrl(), rt3.getUrl());
            cmg.reportResult(rt3, CommManager.REQUEST_RESULT_RESPONSE_TIMEOUT, 0);
            Assert.Equal(cmg.getErrorCount(), 3);

            RequestTarget rt4 = cmg.findUrl();
            Assert.False(rt4.getUrl().Equals(rt1.getUrl()));
        }

        [Fact]
        public void testFindUrl_MultiSite3()
        {
            // test that url is switched when errors reach threshold and switched again after errors
            Dictionary<string, string> _config = new Dictionary<string, string>
            {
                {"proxyHost","websenseproxy"},
                {"proxyPort","8080"},
                {"printxml", "false"},
                {"merchantId", "101" },
                {"username", "DOTNET"},
                {"password", "TESTCASE"},

                {"multiSite", "true"},
                {"printMultiSiteDebug", "false"},
                {"url",legacyUrl },
                {"multiSiteUrl1", site1Url},
                {"multiSiteUrl2", site2Url },
                {"multiSiteErrorThreshold", "3"},
                {"maxHoursWithoutSwitch", "48"}
            };
            CommManager.reset();
            CommManager cmg = CommManager.instance(_config);
            Assert.NotNull(cmg);
            Assert.True(cmg.getMultiSite());
            Assert.Equal(cmg.getMultiSiteThreshold(), 3);

            RequestTarget rt1 = cmg.findUrl();
            Assert.Equal(cmg.getMultiSiteUrls()[cmg.getCurrentMultiSiteUrlIndex()], rt1.getUrl());
            cmg.reportResult(rt1, CommManager.REQUEST_RESULT_RESPONSE_TIMEOUT, 0);
            RequestTarget rt2 = cmg.findUrl();
            Assert.Equal(rt1.getUrl(), rt2.getUrl());
            cmg.reportResult(rt2, CommManager.REQUEST_RESULT_RESPONSE_TIMEOUT, 0);
            RequestTarget rt3 = cmg.findUrl();
            Assert.Equal(rt1.getUrl(), rt3.getUrl());
            cmg.reportResult(rt3, CommManager.REQUEST_RESULT_RESPONSE_TIMEOUT, 0);
            Assert.Equal(cmg.getErrorCount(), 3);

            RequestTarget rt4 = cmg.findUrl();
            Assert.False(rt4.getUrl().Equals(rt1.getUrl()));

            RequestTarget rt10 = cmg.findUrl();
            Assert.Equal(cmg.getMultiSiteUrls()[cmg.getCurrentMultiSiteUrlIndex()], rt10.getUrl());
            cmg.reportResult(rt10, CommManager.REQUEST_RESULT_RESPONSE_RECEIVED, 401);
            RequestTarget rt11 = cmg.findUrl();
            Assert.Equal(rt10.getUrl(), rt11.getUrl());
            cmg.reportResult(rt11, CommManager.REQUEST_RESULT_CONNECTION_FAILED, 0);
            RequestTarget rt12 = cmg.findUrl();
            Assert.Equal(rt11.getUrl(), rt12.getUrl());
            cmg.reportResult(rt12, CommManager.REQUEST_RESULT_RESPONSE_TIMEOUT, 0);
            Assert.Equal(cmg.getErrorCount(), 3);

            RequestTarget rt13 = cmg.findUrl();
            Assert.False(rt13.getUrl().Equals(rt11.getUrl()));
            Assert.True(rt13.getUrl().Equals(rt1.getUrl()));
        }

        [Fact]
        public void testFindUrl_MultiSite4()
        {
            // test that url is not switched when errors reported but then succes resets count
            Dictionary<string, string> _config = new Dictionary<string, string>
            {
                {"proxyHost","websenseproxy"},
                {"proxyPort","8080"},
                {"printxml", "false"},
                {"merchantId", "101" },
                {"username", "DOTNET"},
                {"password", "TESTCASE"},

                {"multiSite", "true"},
                {"printMultiSiteDebug", "false"},
                {"url",legacyUrl },
                {"multiSiteUrl1", site1Url},
                {"multiSiteUrl2", site2Url },
                {"multiSiteErrorThreshold", "3"},
                {"maxHoursWithoutSwitch", "0"}
            };
            CommManager.reset();
            CommManager cmg = CommManager.instance(_config);
            Assert.NotNull(cmg);
            Assert.True(cmg.getMultiSite());
            Assert.Equal(cmg.getMultiSiteThreshold(), 3);

            RequestTarget rt1 = cmg.findUrl();
            Assert.Equal(cmg.getMultiSiteUrls()[cmg.getCurrentMultiSiteUrlIndex()], rt1.getUrl());
            cmg.reportResult(rt1, CommManager.REQUEST_RESULT_RESPONSE_TIMEOUT, 0);
            RequestTarget rt2 = cmg.findUrl();
            Assert.Equal(rt1.getUrl(), rt2.getUrl());
            cmg.reportResult(rt2, CommManager.REQUEST_RESULT_RESPONSE_RECEIVED, 200);
            Assert.Equal(0, cmg.getErrorCount());

            RequestTarget rt3 = cmg.findUrl();
            Assert.Equal(rt1.getUrl(), rt3.getUrl());
            cmg.reportResult(rt3, CommManager.REQUEST_RESULT_RESPONSE_RECEIVED, 301);
            Assert.Equal(0, cmg.getErrorCount());
        }

        [Fact]
        public void testFindUrl_MultiSiteMaxHours()
        {
            // test that url is switched when number of hours since last switch exceeds threshold
            Dictionary<string, string> _config = new Dictionary<string, string>
            {
                {"proxyHost","websenseproxy"},
                {"proxyPort","8080"},
                {"printxml", "false"},
                {"merchantId", "101" },
                {"username", "DOTNET"},
                {"password", "TESTCASE"},

                {"multiSite", "true"},
                {"printMultiSiteDebug", "true"},
                {"url",legacyUrl },
                {"multiSiteUrl1", site1Url},
                {"multiSiteUrl2", site2Url },
                {"multiSiteErrorThreshold", "3"},
                {"maxHoursWithoutSwitch", "4"}
            };
            CommManager.reset();
            CommManager cmg = CommManager.instance(_config);
            Assert.NotNull(cmg);
            Assert.True(cmg.getMultiSite());
            Assert.Equal(cmg.getMultiSiteThreshold(), 3);

            RequestTarget rt1 = cmg.findUrl();
            Assert.Equal(cmg.getMultiSiteUrls()[cmg.getCurrentMultiSiteUrlIndex()], rt1.getUrl());
            cmg.reportResult(rt1, CommManager.REQUEST_RESULT_RESPONSE_RECEIVED, 200);
            RequestTarget rt2 = cmg.findUrl();
            Assert.Equal(rt1.getUrl(), rt2.getUrl());
            cmg.reportResult(rt2, CommManager.REQUEST_RESULT_RESPONSE_RECEIVED, 200);

            // set last switch time to 6 hours earlier
            DateTime gc = new DateTime(cmg.getLastSiteSwitchTime() * 10000);
            gc = gc.Add(new System.TimeSpan(-6,0,0));
            cmg.setLastSiteSwitchTime((gc.Ticks/10000));

            RequestTarget rt3 = cmg.findUrl();
            Assert.False(rt3.getUrl().Equals(rt1.getUrl()));
        }


        [Fact]
        public void testReportResult_NotMultiSite()
        {
            Dictionary<string, string> _config = new Dictionary<string, string>
            {
                {"proxyHost","websenseproxy"},
                {"proxyPort","8080"},
                {"multiSite", "false"},
                {"printxml", "false"},
                {"printMultiSiteDebug", "true"},
                {"merchantId", "101" },
                {"username", "DOTNET"},
                {"password", "TESTCASE"},
                {"url",legacyUrl }
            };
            CommManager.reset();
            CommManager cmg = CommManager.instance(_config);
            Assert.NotNull(cmg);
            Assert.False(cmg.getMultiSite());
            Assert.Equal(legacyUrl, cmg.getLegacyUrl());
            cmg.reportResult(new RequestTarget("",1),  1,  0);
        }
    }
}
