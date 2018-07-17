using System;
using System.Collections.Generic;
using System.IO;
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
            const string appxPattern = "*.appx";
            const string appxBundlePattern = "*.appxbundle";

            string currentDirectory = Directory.GetCurrentDirectory();
            
            string packagePathFromCurrentDirectory = FileFinderHelper.FindFileFromDirectory(currentDirectory, appxPattern);
            string[] dependencyPaths = DependencyHelper.GetDependencyPaths(currentDirectory + "\\Dependencies", true);

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
                    await InstallPackage(packagePathFromCurrentDirectory);
                }
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

            }
            catch (Exception e)
            {
                PrintInstallErrorMessage(e.Message);
            }

        }

        private static void PrintInstallErrorMessage(string message)
        {
            Console.WriteLine(message);
        }


        private static void confirmPackageRegistration(DeploymentResult result)
        {
            if (result.ErrorText.Trim().Length > 0)
            {
                Console.WriteLine(result.ErrorText);
            }
            else
            {
                Console.WriteLine("Install Completed! - Your newly installed app should be available in the start menu");
            }
        }


        private static void installProgress(DeploymentProgress installProgress)
        {
            double installPercentage = installProgress.percentage;
            ProgressHelper.PrintProgressToConsole(installPercentage);
        }

    }

}
