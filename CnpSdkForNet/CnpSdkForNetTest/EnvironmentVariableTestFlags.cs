using System;
using NUnit.Framework;

namespace Cnp.Sdk.Test
{
    public class EnvironmentVariableTestFlags
    {
        public static bool ArePGPFunctionalTestsEnabled()
        {
            var pgpFunctionalTestsEnabled = Environment.GetEnvironmentVariable("pgpFunctionalTestsEnabled");
            if (pgpFunctionalTestsEnabled == null) {
                Console.WriteLine("pgpFunctionalTestsEnabled environment variable is not defined. Defaulting to true.");
                return true;
            }

            return !pgpFunctionalTestsEnabled.ToLower().Equals("false");
        }
        
        public static bool ArePreliveBatchTestsEnabled()
        {
            var preliveBatchTestsEnabled = Environment.GetEnvironmentVariable("preliveBatchTestsEnabled");
            if (preliveBatchTestsEnabled == null) {
                Console.WriteLine("preliveBatchTestsEnabled environment variable is not defined. Defaulting to false.");
                return false;
            }

            return !preliveBatchTestsEnabled.ToLower().Equals("false");
        }
        
        public static bool ArePreliveOnlineTestsEnabled()
        {
            var preliveOnlineTestsEnabled = Environment.GetEnvironmentVariable("preliveOnlineTestsEnabled");
            if (preliveOnlineTestsEnabled == null) {
                Console.WriteLine("preliveOnlineTestsEnabled environment variable is not defined. Defaulting to true.");
                return true;
            }

            return !preliveOnlineTestsEnabled.ToLower().Equals("false");
        }
        
        public static void RequirePGPFunctionalTestsEnabled()
        {
            if (!ArePGPFunctionalTestsEnabled())
            {
                Assert.Ignore("PGP functional tests are disabled.");
            }
        }
        
        public static void RequirePreliveBatchTestsEnabled()
        {
            if (!ArePreliveBatchTestsEnabled())
            {
                Assert.Ignore("Prelive batch tests are disabled.");
            }
        }

        public static void RequirePreliveOnlineTestsEnabled()
        {
            if (!ArePreliveOnlineTestsEnabled())
            {
                Assert.Ignore("Prelive online tests are disabled.");
            }
        }
    }
}