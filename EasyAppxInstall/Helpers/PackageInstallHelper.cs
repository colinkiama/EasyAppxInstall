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

            bool packageRegistered = false;
            string currentDirectory = Directory.GetCurrentDirectory();
            string packageFromCurrentDirectory = FileFinderHelper.FindFileFromDirectory(currentDirectory, appxPattern);
            if (packageFromCurrentDirectory == "")
            {
                packageFromCurrentDirectory = FileFinderHelper.FindFileFromDirectory(currentDirectory, appxBundlePattern);
            }

            if (packageFromCurrentDirectory == "")
            {
                await InstallPackage(packageFromCurrentDirectory);
            }

        }

        internal static Task InstallPackageWithForeignDependencies(string packagePath, string dependencyDirectory)
        {
            throw new NotImplementedException();
        }

        internal static Task InstallPackage(string packagePath, string certificatePath, string dependencyDirectoryPath)
        {
            throw new NotImplementedException();
        }

        internal static Task InstallPackageWithForeignDependencies(string v)
        {
            throw new NotImplementedException();
        }

        public static async Task InstallPackage(string packagePath)
        {
            Console.WriteLine($"Starting to install package from {packagePath}");
            bool packageRegistered = false;
            try
            {
                DeploymentResult result = await pkgManager.AddPackageAsync(new Uri(packagePath, UriKind.Absolute), 
                    null, DeploymentOptions.ForceTargetApplicationShutdown).AsTask(progressCallback);
                checkIfPackageRegistered(result);
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
                checkIfPackageRegistered(result);

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

        private static bool checkIfPackageRegistered(DeploymentResult result)
        {
            bool isRegistered = false;
            if (result.ErrorText.Trim().Length > 0)
            {
                Console.WriteLine(result.ErrorText);
            }
            else
            {
                Console.WriteLine("Install Completed! - Your newly installed app should be available in the start menu");
                isRegistered = true;
            }

            return isRegistered;
        }


        private static void installProgress(DeploymentProgress installProgress)
        {
            double installPercentage = installProgress.percentage;
            ProgressHelper.PrintProgressToConsole(installPercentage);

        }

    }

}
