using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Management.Deployment;
using Windows.Storage;


namespace EasyAppxInstall.Helpers
{
    public class PackageInstallHelper
    {
        static bool pkgRegistered = false;
        static PackageManager pkgManager = new PackageManager();
        static Progress<DeploymentProgress> progressCallback = new Progress<DeploymentProgress>(installProgress);

        public static async Task<bool> InstallPackage(string packagePath)
        {
            bool packageRegistered = false;
            try
            {
                DeploymentResult result = await pkgManager.AddPackageAsync(new Uri(packagePath), null, DeploymentOptions.ForceTargetApplicationShutdown).AsTask(progressCallback);
                packageRegistered = checkIfPackageRegistered(result);
            }

            catch (Exception e)
            {
                PrintInstallErrorMessage(e.Message);
            }

            return packageRegistered;
        }

        public static async Task<bool> InstallPackage(string packagePath, string[] dependencyPaths)
        {
            bool packageRegistered = false;

            Uri packageUri = new Uri(packagePath);
            Uri[] dependencyUris = UriHelper.CreateUrisFromPaths(dependencyPaths);

            try
            {
               DeploymentResult result = await pkgManager.AddPackageAsync(packageUri, dependencyUris, DeploymentOptions.ForceTargetApplicationShutdown).AsTask(progressCallback);
               packageRegistered = checkIfPackageRegistered(result);

            }
            catch (Exception e)
            {
                PrintInstallErrorMessage(e.Message);
            }

            return packageRegistered;
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
