using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyAppxInstall.Helpers
{
    public class DependencyHelper
    {
        const string appxPattern = "appx";

        public static string[] GetDependencyPaths(string directory, bool dividedIntoArchitectures = false)
        {
            if (dividedIntoArchitectures == false)
            {
                return FileFinderHelper.FindMultipleFilesFromDirectory(directory, appxPattern);
            }
            else
            {
                string archFolderName = GetArchitectureFolderName();
                string directoryToSearch = directory + $"\\{archFolderName}";
                return FileFinderHelper.FindMultipleFilesFromDirectory(directoryToSearch, appxPattern);
            }
        }

        private static string GetArchitectureFolderName()
        {
            string archFolderName = "x86";

            string deviceArch = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE").ToLower();
            if (deviceArch.Contains("arm") && deviceArch.Contains("64"))
            {
                archFolderName = "arm64";
            }

            else if (deviceArch.Contains("arm"))
            {
                archFolderName = "arm";
            }

            else if (Environment.Is64BitOperatingSystem)
            {
                archFolderName = "x64";
            }

            return archFolderName;
        }
    }
}
