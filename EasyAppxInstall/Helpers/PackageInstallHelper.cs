using System;
using System.Collections.Generic;
using Windows.Management.Deployment;
using Windows.Storage;


namespace EasyAppxInstall.Helpers
{
    public class PackageInstallHelper
    {
        public static string resultText = "";
        static bool pkgRegistered = false;
        static PackageManager pkgManager = new PackageManager();
        static Progress<DeploymentProgress> progressCallback = new Progress<DeploymentProgress>(installProgress);

        public static async void InstallPackage(string packagePath)
        {
            try
            {
                var result = await pkgManager.AddPackageAsync(new Uri(packagePath), null, DeploymentOptions.ForceTargetApplicationShutdown).AsTask(progressCallback);
                checkIfPackageRegistered(result, resultText);
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public static async void InstallPackage(string packagePath, string[] dependencyPaths)
        {
            Uri packageUri = new Uri(packagePath);

            Uri[] dependencyUris = UriHelper.CreateUrisFromPaths(dependencyPaths);
            try
            {
                var result = await pkgManager.AddPackageAsync(packageUri, dependencyUris, DeploymentOptions.ForceTargetApplicationShutdown).AsTask(progressCallback);
                checkIfPackageRegistered(result, resultText);

            }
            catch (Exception e)
            {
                resultText = e.Message;
            }
        }

        private static void checkIfPackageRegistered(DeploymentResult result, string resultText)
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
