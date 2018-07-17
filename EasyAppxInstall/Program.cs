using EasyAppxInstall.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyAppxInstall
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Install Test");
            Task.Run(async () =>
            {
                await PackageInstallHelper.InstallPackage("C:/Users/colinkiama/source/repos/Yata/YATA/AppPackages/YATA_1.7.1.0_Test/YATA_1.7.1.0_x86_x64_arm.appxbundle");
            }).GetAwaiter().GetResult();
            Console.ReadLine();
        }
    }
}
