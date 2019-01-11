using System;
using System.Collections.Generic;

namespace Cnp.Sdk
{
    public class CommManager
    {
        // Configuration object containing credentials and settings.
        private Dictionary<string, string> _config;
        // 
        private Communications _communication;

        private static readonly object SynLock = new object();

        public const int REQUEST_RESULT_RESPONSE_RECEIVED = 1;
        public const int REQUEST_RESULT_CONNECTION_FAILED = 2;
        public const int REQUEST_RESULT_RESPONSE_TIMEOUT = 3;

        private static CommManager manager = null;

        //protected Properties configuration;
        protected internal Boolean doMultiSite = false;
        protected String legacyUrl;
        protected List<String> multiSiteUrls = new List<String>();
        protected int errorCount = 0;
        protected int currentMultiSiteUrlIndex = 0;
        protected int multiSiteThreshold = 5;
        protected long lastSiteSwitchTime = 0;
        protected int maxHoursWithoutSwitch = 48;
        // date.ToString("yyyy-MM-dd HH:mm:ss")
        protected Boolean printDebug = false;

        public Boolean getMultiSite()
        {
            return doMultiSite;
        }
        public string getLegacyUrl()
        {
            return legacyUrl;
        }
        public List<string> getMultiSiteUrls()
        {
            return multiSiteUrls;
        }
        public int getMultiSiteThreshold()
        {
            return multiSiteThreshold;
        }
        public int getMaxHoursWithoutSwitch()
        {
            return maxHoursWithoutSwitch;
        }
        public int getCurrentMultiSiteUrlIndex()
        {
            return currentMultiSiteUrlIndex;
        }
        public int getErrorCount()
        {
            return errorCount;
        }
        public long getLastSiteSwitchTime()
        {
            return lastSiteSwitchTime;
        }
        public void setLastSiteSwitchTime(long milliseconds)
        {
            lastSiteSwitchTime = milliseconds;
        }

        public static CommManager instance()
        {
            if (manager == null)
            {
                manager = new CommManager();
            }
            return manager;
        }

        public static CommManager instance(Dictionary<string,string> config)
        {
            if (manager == null)
            {
                manager = new CommManager(config);
            }
            return manager;
        }

        public static void reset()
        {
            manager = null;
        }

        private CommManager()
        {

            _config = new Dictionary<string, string>();
            _config["url"] = Properties.Settings.Default.url;
            _config["reportGroup"] = Properties.Settings.Default.reportGroup;
            _config["username"] = Properties.Settings.Default.username;
            _config["printxml"] = Properties.Settings.Default.printxml;
            _config["timeout"] = Properties.Settings.Default.timeout;
            _config["proxyHost"] = Properties.Settings.Default.proxyHost;
            _config["merchantId"] = Properties.Settings.Default.merchantId;
            _config["password"] = Properties.Settings.Default.password;
            _config["proxyPort"] = Properties.Settings.Default.proxyPort;
            _config["logFile"] = Properties.Settings.Default.logFile;
            _config["neuterAccountNums"] = Properties.Settings.Default.neuterAccountNums;
            _config["multiSite"] = Properties.Settings.Default.multiSite;
            _config["printMultiSiteDebug"] = Properties.Settings.Default.printMultiSiteDebug;
            _config["multiSiteUrl1"] = Properties.Settings.Default.multiSiteUrl1;
            _config["multiSiteUrl2"] = Properties.Settings.Default.multiSiteUrl2;
            _config["multiSiteErrorThreshold"] = Properties.Settings.Default.multiSiteErrorThreshold;
            _config["maxHoursWithoutSwitch"] = Properties.Settings.Default.maxHoursWithoutSwitch;

            setupMultiSite();

       
            
        }

        private CommManager(Dictionary<string, string> config)
        {
            _config = config;
            setupMultiSite();
        }


        private void setupMultiSite()
        {
            

            legacyUrl = _config["url"];

            if (_config.ContainsKey("multiSite"))
            {
                doMultiSite = Convert.ToBoolean(_config["multiSite"]);
            }

            if (doMultiSite)
            {
                for (int x = 1; x < 3; x++)
                {
                    if (_config.ContainsKey("multiSiteUrl" + x) && !String.IsNullOrEmpty(_config["multiSiteUrl"+x]))
                    {
                        String siteUrl = _config["multiSiteUrl" + x];
                        if (siteUrl == null)
                        {
                            break;
                        }

                        multiSiteUrls.Add(siteUrl);
                    }
                }
                if (multiSiteUrls.Count == 0)
                {
                    doMultiSite = false;
                }
                else
                {
                    Shuffle(multiSiteUrls);  // shuffle to randomize which one is selected first
                    currentMultiSiteUrlIndex = 0;
                    errorCount = 0;
                    if (_config.ContainsKey("multiSiteErrorThreshold") && !String.IsNullOrEmpty(_config["multiSiteErrorThreshold"]))
                    {
                        String threshold = _config["multiSiteErrorThreshold"];
                        int t = Int32.Parse(threshold);
                        if (t > 0 && t < 100)
                        {
                            multiSiteThreshold = t;
                        }
                        
                    }
                    
                    if (_config.ContainsKey("maxHoursWithoutSwitch") && !String.IsNullOrEmpty(_config["maxHoursWithoutSwitch"]))
                    {
                        String maxHours = _config["maxHoursWithoutSwitch"];
                        int t = Int32.Parse(maxHours);
                        if (t >= 0 && t < 300)
                        {
                            maxHoursWithoutSwitch = t;
                        }
                    }
                    lastSiteSwitchTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                }
            }
        }

        public RequestTarget findUrl()
        {

            String url = legacyUrl;
            if (doMultiSite)
            {

                lock (SynLock)
                {
                    Boolean switchSite = false;
                    String switchReason = "";
                    String currentUrl = multiSiteUrls[currentMultiSiteUrlIndex];
                    if (errorCount < multiSiteThreshold)
                    {
                        if (maxHoursWithoutSwitch > 0)
                        {
                            long diffSinceSwitch = ((DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - lastSiteSwitchTime) / 3600000;
                            if (diffSinceSwitch > maxHoursWithoutSwitch)
                            {
                                switchReason = " more than " + maxHoursWithoutSwitch + " hours since last switch";
                                switchSite = true;
                            }
                        }
                    }
                    else
                    {
                        switchReason = " consecutive error count has reached threshold of " + multiSiteThreshold;
                        switchSite = true;
                    }

                    if (switchSite)
                    {
                        currentMultiSiteUrlIndex++;
                        if (currentMultiSiteUrlIndex >= multiSiteUrls.Count)
                        {
                            currentMultiSiteUrlIndex = 0;
                        }
                        url = multiSiteUrls[currentMultiSiteUrlIndex];
                        errorCount = 0;
                        if (printDebug)
                        {
                            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  Switched to " + url + " because " + switchReason);
                        }
                    }

                    else
                    {
                        url = currentUrl;
                    }
                }
            }
            if (printDebug)
            {
                Console.WriteLine("Selected URL: " + url);
            }
            return new RequestTarget(url, currentMultiSiteUrlIndex);
            
        }

        public void reportResult(RequestTarget target, int result, int statusCode)
        {
            lock (SynLock)
            {
                if (target.getRequestTime() < lastSiteSwitchTime || !doMultiSite)
                {
                    return;
                }
                switch (result)
                {
                    case REQUEST_RESULT_RESPONSE_RECEIVED:
                        if (statusCode == 200)
                        {
                            errorCount = 0;
                        }
                        else if (statusCode >= 400)
                        {
                            errorCount++;
                        }
                        break;
                    case REQUEST_RESULT_CONNECTION_FAILED:
                        errorCount++;
                        break;
                    case REQUEST_RESULT_RESPONSE_TIMEOUT:
                        errorCount++;
                        break;
                }
            }
        }

        private static Random rng = new Random();

        public void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

    }
}
