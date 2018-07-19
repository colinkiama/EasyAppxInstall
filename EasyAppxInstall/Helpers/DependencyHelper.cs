using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyAppxInstall.Helpers
{
    public class DependencyHelper
    {
        const string appxPattern = "*.appx";

        const string armString = "arm";
        const string arm64String = "arm64";
        const string x64String = "x64";
        const string x86String = "x86";

        public static string[] GetDependencyPaths(string directory, bool dividedIntoArchitectures = false)
        {
            if (dividedIntoArchitectures == false)
            {
                return FileFinderHelper.FindMultipleFilesFromDirectory(directory, appxPattern);
            }
            else
            {
                string[] dependencies = tryToGetCorrectDependencies(directory);
                return dependencies;
            }
        }

        private static string[] tryToGetCorrectDependencies(string directory)
        {
            string archFolderName = GetArchitectureFolderName();

            string[] dependenciesToReturn;

            string searchDirectory = UpdateSearchDirectory(directory, archFolderName);
            Console.WriteLine(searchDirectory);
            if (archFolderName == arm64String)
            {
                dependenciesToReturn = FileFinderHelper.FindMultipleFilesFromDirectory(searchDirectory, appxPattern);
                if (dependenciesToReturn.Length > 0)
                {
                    return dependenciesToReturn;
                }
                else
                {
                    archFolderName = armString;
                    searchDirectory = UpdateSearchDirectory(searchDirectory, archFolderName);
                    if (dependenciesToReturn.Length > 0)
                    {
                        return dependenciesToReturn;
                    }
                    else
                    {
                        archFolderName = armString;
                        searchDirectory = UpdateSearchDirectory(directory, archFolderName);
                        dependenciesToReturn = FileFinderHelper.FindMultipleFilesFromDirectory(searchDirectory, appxPattern);
                        if (dependenciesToReturn.Length > 0)
                        {
                            return dependenciesToReturn;
                        }
                        else
                        {
                            archFolderName = x86String;
                            searchDirectory = UpdateSearchDirectory(directory, archFolderName);
                            dependenciesToReturn = FileFinderHelper.FindMultipleFilesFromDirectory(searchDirectory, appxPattern);
                            return dependenciesToReturn;
                        }
                    }
                }
            }
            else if (archFolderName == x64String)
            {
                dependenciesToReturn = FileFinderHelper.FindMultipleFilesFromDirectory(searchDirectory, appxPattern);
                if (dependenciesToReturn.Length > 0)
                {
                    return dependenciesToReturn;
                }
                else
                {
                    archFolderName = x86String;
                    searchDirectory = UpdateSearchDirectory(directory, archFolderName);
                    dependenciesToReturn = FileFinderHelper.FindMultipleFilesFromDirectory(searchDirectory, appxPattern);
                    return dependenciesToReturn;
                }
            }

            else if (archFolderName == x86String)
            {
                dependenciesToReturn = FileFinderHelper.FindMultipleFilesFromDirectory(searchDirectory, appxPattern);
                return dependenciesToReturn;
            }

            else if (archFolderName == armString)
            {
                dependenciesToReturn = FileFinderHelper.FindMultipleFilesFromDirectory(searchDirectory, appxPattern);
                return dependenciesToReturn;
            }

            else
            {
                dependenciesToReturn = new string[0];
                return dependenciesToReturn;

            }
        }








        private static string UpdateSearchDirectory(string directory, string archFolderName)
        {
            Console.WriteLine(directory + $"\\{archFolderName}");
            return directory + $"\\{archFolderName}";
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
