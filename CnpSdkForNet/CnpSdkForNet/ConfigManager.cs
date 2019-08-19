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
            _config["sftpUrl"] = Properties.Settings.Default.sftpUrl;
            _config["sftpUsername"] = Properties.Settings.Default.sftpUsername;
            _config["sftpPassword"] = Properties.Settings.Default.sftpPassword;
            _config["knownHostsFile"] = Properties.Settings.Default.knownHostsFile;
            _config["onlineBatchUrl"] = Properties.Settings.Default.onlineBatchUrl;
            _config["onlineBatchPort"] = Properties.Settings.Default.onlineBatchPort;
            _config["requestDirectory"] = Properties.Settings.Default.requestDirectory;
            _config["responseDirectory"] = Properties.Settings.Default.responseDirectory;
            _config["useEncryption"] = Properties.Settings.Default.useEncryption;
            _config["vantivPublicKeyId"] = Properties.Settings.Default.vantivPublicKeyId;
            _config["pgpPassphrase"] = Properties.Settings.Default.pgpPassphrase;
            _config["neuterUserCredentials"] = Properties.Settings.Default.neuterUserCredentials;



        }

        public ConfigManager(Dictionary<string, string> config)
        {
            _config = config;
        }

        //public void configureConfig()
        //{
        //    Console.Write("Please input the URL for online transactions (ex: https://www.testantivcnp.com/sandbox/communicator/online):");
        //    string url = Console.ReadLine();
        //    setProperty("url", url);
        //    Console.Write("reportGroup: ");
        //    string reportGroup = Console.ReadLine();
        //    setProperty("reportGroup", reportGroup);
        //    Console.Write("Please input your presenter user name: ");
        //    string username = Console.ReadLine();
        //    setProperty("username", username);
        //    Console.Write("printxml: ");
        //    string printxml = Console.ReadLine();
        //    setProperty("printxml", printxml);
        //    Console.Write("timeout: ");
        //    string timeout = Console.ReadLine();
        //    setProperty("timeout", timeout);
        //    Console.Write("
        //}

        //private void setProperty(string property, string val)
        //{
        //    if (val.ToLower() == "t" || val.ToLower() == "true")
        //    {
        //        _config[property] = "true";
        //    }
        //    else if (val.ToLower() == "f" || val.ToLower() == "false")
        //    {
        //        _config[property] = "false";
        //    }
        //    else if (val != "")
        //    {
        //        _config[property] = val;
        //    }
        //}

    }
}
