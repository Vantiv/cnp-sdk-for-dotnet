using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;


namespace Cnp.Sdk
{
    struct ProcResult
    {
        public string output;
        public string error;
        public int status;
    }


    public class PgpHelper
    {
        private const int Success = 0;
        private static string GpgPath = Properties.Settings.Default.gnuPgDir;
        private const string GpgExecutable = "gpg";
        private const string GpgConfExecutable = "gpgconf";

        public static string GetExecutablePath(string path)
        {
            if (!File.Exists(path) && File.Exists(path + ".exe"))
            {
                return path + ".exe";
            }

            return path;
        }

        public static void EncryptFile(string inputFileName, string outputFileName, string recipientKeyId)
        {
            const string commandFormat = @"--batch --yes --armor --trust-model always --output {0} --recipient {1} --encrypt {2}";

            var procResult = ExecuteCommandSync(string.Format(commandFormat, outputFileName, recipientKeyId, inputFileName), GpgExecutable);
            if (procResult.status != Success)
            {
                if (procResult.error.ToLower().Contains("no public key"))
                {
                    throw new CnpOnlineException("Please make sure that the recipient Key ID is correct and is added to your gpg keyring.\n" + procResult.error);
                }

                else if (Regex.IsMatch(procResult.error,string.Format("can't open .{0}", Regex.Escape(inputFileName))))
                {
                    throw new CnpOnlineException("Please make sure the input file exists and has read permission.\n" + procResult.error);
                }

                else
                {
                    throw new CnpOnlineException(procResult.error);
                }               
            }

            Console.WriteLine("Encrypted with key id " + recipientKeyId + " successfully!");
        }

        public static string EncryptString(string toBeEncryptedString, string publicKeyPath)
        {         
            try
            {
                // Write the string to a temporary file
                string tempInputPath = "temp_input.txt";
                File.WriteAllText(tempInputPath, toBeEncryptedString);


                const string commandFormat = @"--batch --yes --armor --trust-model always --recipient-file {0} --encrypt {1}";

                var procResult = ExecuteCommandSync1(string.Format(commandFormat, publicKeyPath, tempInputPath), GpgExecutable);
                // Read the encrypted content from the temporary file
                string tempOutputPath = "temp_input.txt.asc";
                string encryptedString = File.ReadAllText(tempOutputPath);

                // Clean up temporary files
                File.Delete(tempInputPath);
                File.Delete(tempOutputPath);

                return encryptedString;
            }
            catch (Exception ex)
            {
                throw new Exception($"Encrypting the string has failed!\n{ex.Message}");
            }
        }

        public static void DecryptFile(string inputFileName, string outputFileName, string passphrase)
        {
            // Set up the commands for GPG >=2.1 and <2.1
            string commandFormat = @"--batch --trust-model always --output {0} --passphrase {1} --decrypt {2}";
            string commandFormatPinentryLoop = @"--batch --trust-model always --pinentry-mode loopback --output {0} --passphrase {1} --decrypt {2}";
            if (File.Exists(outputFileName))
            {
                File.Delete(outputFileName);
            }

            // Run the command for GPG >=2.1. If it doesn't work (<2.1), then use 2.0 and earlier.
            // If it works, reset the passphrase so it isn't saved (GPG >=2.1).
            var procResult = ExecuteCommandSync(string.Format(commandFormatPinentryLoop, outputFileName, passphrase, inputFileName),GpgExecutable);
            if (procResult.status != Success && procResult.error.ToLower().Contains("gpg: invalid option \"--pinentry-mode\""))
            {
                procResult = ExecuteCommandSync(string.Format(commandFormat, outputFileName, passphrase, inputFileName),GpgExecutable);
            }
            else
            {
                var result = ExecuteCommandSync(@"--kill gpg-agent", GpgConfExecutable);
                Console.WriteLine("Status: " + result.status);
                Console.WriteLine("Output: " + result.output);
                Console.WriteLine("Error: " + result.error);
            }

            if (procResult.status != Success)
            {
                if (procResult.error.ToLower().Contains("gpg: public key decryption failed: bad passphrase"))
                {
                    throw new CnpOnlineException("Please make sure that the passphrase is correct.\n" + procResult.error);
                }

                else if (procResult.error.ToLower().Contains("gpg: decryption failed: no secret key"))
                {
                    throw new CnpOnlineException("Please make sure that your merchant secret key is added to your gpg keyring.\n" + procResult.error);
                }

                else if (Regex.IsMatch(procResult.error,string.Format("can't open .{0}", Regex.Escape(inputFileName))))
                {
                    throw new CnpOnlineException("Please make sure the input file exists and has read permission.\n" + procResult.error);
                }

                else
                {
                    throw new CnpOnlineException(procResult.error);
                }
            }
        }

        public static string ImportPrivateKey(string keyFilePath, string passphrase)
        {
            const string commandFormat = @"--import --passphrase-fd 0 --pinentry-mode loopback {0}";

            var procResult = ExecuteCommandSync(string.Format(commandFormat, keyFilePath), passphrase);
            if (procResult.status != Success)
            {
                throw new CnpOnlineException(procResult.error);
            }

            return ExtractKeyId(procResult.error);
        }

        public static string ImportPublicKey(string keyFilePath)
        {
            const string commandFormat = @"--import {0}";

            var procResult = ExecuteCommandSync(string.Format(commandFormat, keyFilePath), GpgExecutable);
            if (procResult.status != Success)
            {
                throw new CnpOnlineException(procResult.error);
            }

            return ExtractKeyId(procResult.error);
        }


        private static ProcResult ExecuteCommandSyncWithPassphrase(string command, string passphrase)
        {
            string path = GetExecutablePath(Path.Combine(GpgPath, GpgExecutable));

            var procStartInfo = new ProcessStartInfo(path, command)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true
            };

            var proc = new Process { StartInfo = procStartInfo };
            proc.Start();
            proc.StandardInput.WriteLine(passphrase);
            proc.StandardInput.Flush();
            proc.WaitForExit();

            return new ProcResult
            {
                output = proc.StandardOutput.ReadToEnd(),
                error = proc.StandardError.ReadToEnd(),
                status = proc.ExitCode
            };
        }


        private static ProcResult ExecuteCommandSync(string command, string executable)
        {
            string path = GetExecutablePath(Path.Combine(GpgPath, executable));

            var procStartInfo = new ProcessStartInfo(path, command)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true
            };

            var proc = new Process { StartInfo = procStartInfo };
            proc.Start();
            proc.StandardInput.Flush();
            proc.WaitForExit();

            return new ProcResult
            {
                output = proc.StandardOutput.ReadToEnd(),
                error = proc.StandardError.ReadToEnd(),
                status = proc.ExitCode
            };
        }

        private static string ExtractKeyId(string result)
        {
            return result.Split(':')[1].Split(' ')[2].Substring(8);
        }

        public static string ExecuteCommandSync1(string command, string executablePath)
        {
            string path = GetExecutablePath(Path.Combine(GpgPath, executablePath));
            try
            {
                // Create the process start info
                var procStartInfo = new ProcessStartInfo(path, command)
                {
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                // Create the process
                var proc = new Process
                {
                    StartInfo = procStartInfo
                };

                // Start the process
                proc.Start();

                // Read the output
                string result = proc.StandardOutput.ReadToEnd();
                string error = proc.StandardError.ReadToEnd();

                // Wait for the process to exit
                proc.WaitForExit();

                // Return the result or error
                return string.IsNullOrEmpty(error) ? result : error;
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                return $"Error: {ex.Message}";
            }
        }
    }
}

