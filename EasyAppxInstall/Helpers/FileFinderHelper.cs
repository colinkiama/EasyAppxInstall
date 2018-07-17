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
        public static string FindFileFromDirectory(string currentDirectory, string searchPattern)
        {
            string fileName = "";

            string[] files = Directory.GetFiles(currentDirectory, searchPattern);
            if (files.Length > 0)
            {
                fileName = files[0];
            }

            return fileName;
        }

      
    }
}
