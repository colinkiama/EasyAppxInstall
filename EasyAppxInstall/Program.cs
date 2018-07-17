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
            const string testPkgPath = "C:/Users/colinkiama/source/repos/Yata/YATA/AppPackages/YATA_1.7.1.0_Test/YATA_1.7.1.0_x86_x64_arm.appxbundle";

            Console.WriteLine("Full Test");
            CertInstallHelper.InstallCertFromSignedPackage(testPkgPath);
            Task.Run(async () =>
            {
                await PackageInstallHelper.InstallPackage(testPkgPath);
            }).GetAwaiter().GetResult();
            Console.ReadLine();
        }
    }
}
