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

        //const string testPkgPath = "C:/Users/colinkiama/source/repos/Yata/YATA/AppPackages/YATA_1.7.1.0_Test/YATA_1.7.1.0_x86_x64_arm.appxbundle";

        static void Main(string[] args)
        {
            Console.WriteLine("Installing certificates then installing package");
            if (args.Length > 0)
            {
                CertInstallHelper.InstallCert(args[0]);
                AddBlankLine();

                if (args.Length == 1)
                {
                    Task.Run(async () =>
                    {
                        await PackageInstallHelper.InstallPackage(args[0]);
                    }).GetAwaiter().GetResult();
                }

                else if (args[0] == "/f")
                {
                    Task.Run(async () =>
                    {
                        await PackageInstallHelper.InstallPackageWithForeignDependencies(args[1], args[2]);
                    }).GetAwaiter().GetResult();
                }

            }
            else
            {
                CertInstallHelper.InstallCert();
                AddBlankLine();
                Task.Run(async () =>
                {
                    await PackageInstallHelper.InstallPackageFromCurrentDirectory();
                }).GetAwaiter().GetResult();
            }

            Console.ReadLine();
        }

        private static void AddBlankLine()
        {
            Console.WriteLine();
        }
    }
}
