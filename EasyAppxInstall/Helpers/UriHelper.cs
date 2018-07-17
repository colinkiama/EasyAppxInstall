using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyAppxInstall.Helpers
{
    public static class UriHelper
    {
        public static Uri[] CreateUrisFromPaths(string[] paths)
        {
            Uri[] urisToReturn = new Uri[paths.Length];

            for (int i = 0; i < paths.Length; i++)
            {
                urisToReturn[i] = new Uri(paths[i]);
            }

            return urisToReturn;
        }
    }
}
