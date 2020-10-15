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

        public ConfigManager() : this(new Dictionary<string, string>
        {
            ["url"] = Properties.Settings.Default.url,
            ["reportGroup"] = Properties.Settings.Default.reportGroup,
            ["username"] = Properties.Settings.Default.username,
            ["printxml"] = Properties.Settings.Default.printxml,
            ["timeout"] = Properties.Settings.Default.timeout,
            ["proxyHost"] = Properties.Settings.Default.proxyHost,
            ["merchantId"] = Properties.Settings.Default.merchantId,
            ["password"] = Properties.Settings.Default.password,
            ["proxyPort"] = Properties.Settings.Default.proxyPort,
            ["logFile"] = Properties.Settings.Default.logFile,
            ["neuterAccountNums"] = Properties.Settings.Default.neuterAccountNums,
            ["sftpUrl"] = Properties.Settings.Default.sftpUrl,
            ["sftpUsername"] = Properties.Settings.Default.sftpUsername,
            ["sftpPassword"] = Properties.Settings.Default.sftpPassword,
            ["onlineBatchUrl"] = Properties.Settings.Default.onlineBatchUrl,
            ["onlineBatchPort"] = Properties.Settings.Default.onlineBatchPort,
            ["requestDirectory"] = Properties.Settings.Default.requestDirectory,
            ["responseDirectory"] = Properties.Settings.Default.responseDirectory,
            ["useEncryption"] = Properties.Settings.Default.useEncryption,
            ["vantivPublicKeyId"] = Properties.Settings.Default.vantivPublicKeyId,
            ["pgpPassphrase"] = Properties.Settings.Default.pgpPassphrase,
            ["neuterUserCredentials"] = Properties.Settings.Default.neuterUserCredentials,
            ["maxConnections"] = Properties.Settings.Default.maxConnections
        }) { }

        public ConfigManager(Dictionary<string, string> config)
        {
            _config = config;
        }
    }
}
