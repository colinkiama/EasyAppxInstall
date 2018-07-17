using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyAppxInstall.Helpers
{
    public static class FileFinderHelper
    {
        public static string FindFileFromDirectory(string directory, string searchPattern)
        {
            string fileName = "";

            string[] files = Directory.GetFiles(directory, searchPattern);
            if (files.Length > 0)
            {
                fileName = files[0];
            }

            return fileName;
        }

        public static string[] FindMultipleFilesFromDirectory(string directory, string searchPattern)
        {
            string[] fileNames = Directory.GetFiles(directory, searchPattern);
            return fileNames;
        }

      
    }
}
