using EasyAppxInstall.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EasyAppxInstall
{
    class Program
    {
        static void Main(string[] args)
        {
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
            Program.Exit();
        }


        public static void BadExit()
        {
            Thread.Sleep(5000);
            Environment.Exit(1);
        }

        public static void Exit()
        {
            Thread.Sleep(5000);
            Environment.Exit(0);
        }
        private static void AddBlankLine()
        {
            Console.WriteLine();
        }
    }
}
