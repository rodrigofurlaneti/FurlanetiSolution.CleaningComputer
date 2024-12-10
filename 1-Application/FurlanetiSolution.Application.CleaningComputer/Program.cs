using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;

namespace SystemOptimizer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "System Optimizer - 2024";

            while (true)
            {
                Console.Clear();
                Console.WriteLine("##############################");
                Console.WriteLine("# System Optimizer 2024 #");
                Console.WriteLine("##############################");
                Console.WriteLine("Choose an option:");
                Console.WriteLine("[1] Quick Cleanup");
                Console.WriteLine("[2] Advanced Cleanup");
                Console.WriteLine("[3] Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        QuickCleanup();
                        break;
                    case "2":
                        AdvancedCleanup();
                        break;
                    case "3":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

                Console.WriteLine("Press any key to return to the menu...");
                Console.ReadKey();
            }
        }

        static void QuickCleanup()
        {
            Console.Clear();
            Console.WriteLine("Starting quick cleanup...");

            CleanTemporaryFiles();
            CleanPrefetchFiles();
            CleanBrowserHistory();

            Console.WriteLine("Quick cleanup completed!");
        }

        static void AdvancedCleanup()
        {
            Console.Clear();
            Console.WriteLine("Starting advanced cleanup...");

            CleanTemporaryFiles();
            CleanPrefetchFiles();
            CleanBrowserHistory();
            CleanCrashDumps();
            CleanWindowsUpdateCache();
            CleanExplorerHistory();
            RunCleanMgr();

            Console.WriteLine("Advanced cleanup completed!");
        }

        static void CleanTemporaryFiles()
        {
            Console.WriteLine("Cleaning temporary files...");

            var tempPaths = new[]
            {
                Path.GetTempPath(),
                Environment.GetEnvironmentVariable("TMP"),
                Environment.GetEnvironmentVariable("TEMP")
            };

            foreach (var path in tempPaths)
            {
                if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
                {
                    try
                    {
                        Directory.Delete(path, true);
                        Directory.CreateDirectory(path);
                        Console.WriteLine($"Cleaned: {path}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error cleaning {path}: {ex.Message}");
                    }
                }
            }
        }

        static void CleanPrefetchFiles()
        {
            Console.WriteLine("Cleaning prefetch files...");
            string prefetchPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Prefetch");

            try
            {
                if (Directory.Exists(prefetchPath))
                {
                    Directory.Delete(prefetchPath, true);
                    Directory.CreateDirectory(prefetchPath);
                    Console.WriteLine("Prefetch files cleaned.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error cleaning prefetch files: {ex.Message}");
            }
        }

        static void CleanBrowserHistory()
        {
            Console.WriteLine("Cleaning browser history...");
            try
            {
                ExecuteCommand("RunDll32.exe InetCpl.cpl,ClearMyTracksByProcess 255");
                Console.WriteLine("Browser history cleaned.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error cleaning browser history: {ex.Message}");
            }
        }

        static void CleanCrashDumps()
        {
            Console.WriteLine("Cleaning crash dumps...");
            string crashDumpPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Minidump");

            try
            {
                if (Directory.Exists(crashDumpPath))
                {
                    Directory.Delete(crashDumpPath, true);
                    Console.WriteLine("Crash dumps cleaned.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error cleaning crash dumps: {ex.Message}");
            }
        }

        static void CleanWindowsUpdateCache()
        {
            Console.WriteLine("Cleaning Windows Update cache...");
            string windowsUpdatePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "$hf_mig$");

            try
            {
                if (Directory.Exists(windowsUpdatePath))
                {
                    Directory.Delete(windowsUpdatePath, true);
                    Console.WriteLine("Windows Update cache cleaned.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error cleaning Windows Update cache: {ex.Message}");
            }
        }

        static void CleanExplorerHistory()
        {
            Console.WriteLine("Cleaning Explorer history...");
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\RunMRU", true);
                if (key != null)
                {
                    key.DeleteSubKeyTree("");
                    key.Close();
                    Console.WriteLine("Explorer history cleaned.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error cleaning Explorer history: {ex.Message}");
            }
        }

        static void RunCleanMgr()
        {
            Console.WriteLine("Running advanced Cleanmgr...");
            try
            {
                ExecuteCommand("cleanmgr.exe /sagerun:1");
                Console.WriteLine("Cleanmgr completed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error running Cleanmgr: {ex.Message}");
            }
        }

        static void ExecuteCommand(string command)
        {
            ProcessStartInfo psi = new ProcessStartInfo("cmd.exe", $"/c {command}")
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(psi))
            {
                process.WaitForExit();
            }
        }
    }
}
