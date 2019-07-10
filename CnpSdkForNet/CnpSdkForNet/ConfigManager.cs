using System;
using System.Collections.Generic;

namespace Cnp.Sdk
{
    public class ConfigManager
    {
        // Configuration object containing credentials and settings.
        private Dictionary<string, string> _config;

        public Dictionary<string, string> getConfig()
        {
            return _config;
        }

        public ConfigManager()
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



        }

        public ConfigManager(Dictionary<string, string> config)
        {
            _config = config;
        }

    }
}
