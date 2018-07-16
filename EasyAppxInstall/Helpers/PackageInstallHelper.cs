using System;
using System.Collections.Generic;
using Windows.Management.Deployment;
using Windows.Storage;


namespace EasyAppxInstall.Helpers
{
   public class PackageInstallHelper
    {
        StorageFile packageInContext;
        List<Uri> dependencies = new List<Uri>();
        //ValueSet cannot contain values of the URI class which is why there is another list below.
        //This is required to update the progress in a notification using a background task.
        List<string> dependenciesAsString = new List<string>();

        bool pkgRegistered = false;

        private async void showProgressInApp()
        {
          
            PackageManager pkgManager = new PackageManager();
            Progress<DeploymentProgress> progressCallback = new Progress<DeploymentProgress>(installProgress);
            string resultText = "Nothing";

            
            if (dependencies != null && dependencies.Count > 0)
            {
                try
                {
                    var result = await pkgManager.AddPackageAsync(new Uri(packageInContext.Path), dependencies, DeploymentOptions.ForceTargetApplicationShutdown).AsTask(progressCallback);
                    checkIfPackageRegistered(result, resultText);

                }
                catch (Exception e)
                {
                    resultText = e.Message;
                }

            }
            else
            {
                try
                {

                    var result = await pkgManager.AddPackageAsync(new Uri(packageInContext.Path), null, DeploymentOptions.ForceTargetApplicationShutdown).AsTask(progressCallback);
                    checkIfPackageRegistered(result, resultText);
                }

                catch (Exception e)
                {
                    resultText = e.Message;
                }

            }

          
            if (pkgRegistered == true)
            {
                permissionTextBlock.Text = "Completed";
                notification.ShowInstallationHasCompleted(packageInContext.Name);



            }
            else
            {
                resultTextBlock.Text = resultText;
                notification.sendError(resultText);
            }
        }

        private void checkIfPackageRegistered(DeploymentResult result, string resultText)
        {
            if (result.)
            {

            }
            {
                resultText = result.ErrorText;
            }
        }

       
        private void installProgress(DeploymentProgress installProgress)
        {
            double installPercentage = installProgress.percentage;
            string percentageAsString = String.Format($"{installPercentage}%");
        }

        

        

    }
}
