using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Windows.Management.Deployment;
using Windows.Storage;


namespace EasyAppxInstall.Helpers
{
    public class PackageInstallHelper
    {
        static PackageManager pkgManager = new PackageManager();
        static Progress<DeploymentProgress> progressCallback = new Progress<DeploymentProgress>(installProgress);

        public static async Task InstallPackageFromCurrentDirectory()
        {
            Console.WriteLine("Installing from current directory");
            const string appxPattern = "*.appx";
            const string appxBundlePattern = "*.appxbundle";

            string currentDirectory = Directory.GetCurrentDirectory();
            
            string packagePathFromCurrentDirectory = FileFinderHelper.FindFileFromDirectory(currentDirectory, appxPattern);
            string[] dependencyPaths = DependencyHelper.GetDependencyPaths(currentDirectory + "\\Dependencies\\", true);

            if (packagePathFromCurrentDirectory == "")
            {
                packagePathFromCurrentDirectory = FileFinderHelper.FindFileFromDirectory(currentDirectory, appxBundlePattern);
            }

            if (packagePathFromCurrentDirectory != "")
            {
                if (dependencyPaths.Length > 0)
                {
                    await InstallPackage(packagePathFromCurrentDirectory, dependencyPaths);
                }
                else
                {
                    Console.WriteLine("No dependencies found. Installing package only");
                    await InstallPackage(packagePathFromCurrentDirectory);
                }
            }
            else
            {
                Console.WriteLine("No packages found in current directory.");
                
             
            }

        }

        internal async static Task InstallPackageWithForeignDependencies(string packagePath, string dependencyDirectory)
        {
            string[] dependencyPaths = DependencyHelper.GetDependencyPaths(dependencyDirectory);
            if (dependencyPaths.Length > 0)
            {
                await InstallPackage(packagePath, dependencyPaths);
            }
            else
            {
                Console.WriteLine("No dependencies found at directory. Will try to install only the app package instead.");
                await InstallPackage(packagePath);
            }
        }


        public static async Task InstallPackage(string packagePath)
        {
            Console.WriteLine($"Starting to install package from {packagePath}");
            try
            {
                DeploymentResult result = await pkgManager.AddPackageAsync(new Uri(packagePath, UriKind.Absolute),
                    null, DeploymentOptions.ForceTargetApplicationShutdown).AsTask(progressCallback);
                confirmPackageRegistration(result);
            }

            catch (Exception e)
            {
                PrintInstallErrorMessage(e.Message);
            }


        }

        public static async Task InstallPackage(string packagePath, string[] dependencyPaths)
        {
            Uri packageUri = new Uri(packagePath);
            Uri[] dependencyUris = UriHelper.CreateUrisFromPaths(dependencyPaths);

            try
            {
                DeploymentResult result = await pkgManager.AddPackageAsync(packageUri, dependencyUris,
                    DeploymentOptions.ForceTargetApplicationShutdown).AsTask(progressCallback);
                confirmPackageRegistration(result);
                await InstallPackageWithoutDependencies(packagePath);

            }
            catch (Exception e)
            {
               await InstallPackageWithoutDependencies(packagePath, e.Message);
            }

        }

        

        private static async Task InstallPackageWithoutDependencies(string packagePath, string errorMessageWithDependencies = "Install with dependencies failed")
        {
            Console.WriteLine("\n" + errorMessageWithDependencies);
            Console.WriteLine("Will now attempt to install the package without dependencies.");
            await InstallPackage(packagePath);
        }

        private static void PrintInstallErrorMessage(string message)
        {
            Console.WriteLine("\n" + message);
            Program.BadExit();
        }


        private static void confirmPackageRegistration(DeploymentResult result)
        {
            if (result.ErrorText.Trim().Length > 0)
            {
                Console.WriteLine(result.ErrorText);
                
            }
            else
            {
                Console.WriteLine("\nInstall Completed! - Your newly installed app should is available in the start menu.");
            }
        }


        private static void installProgress(DeploymentProgress installProgress)
        {
            double installPercentage = installProgress.percentage;
            ProgressHelper.PrintProgressToConsole(installPercentage);
        }

    }

}
