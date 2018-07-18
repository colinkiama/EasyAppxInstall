using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyAppxInstall.Helpers
{
    public static class ProgressHelper
    {
        const char progressChar = '-';
        const int progressBarSpacing = 11;
        const int maxProgressChars = 10;
        public static void PrintProgressToConsole(double installPercentage)
        {
            int progressCharsToPrint = (int)installPercentage / maxProgressChars;
            StringBuilder sb = new StringBuilder();
            sb.Append("Installing: ");
            AddProgressBar(ref sb, progressCharsToPrint);
            AddSpacingToString(ref sb, progressCharsToPrint);
            AddProgressValue(ref sb, installPercentage);
            string progressString = sb.ToString();
            PrintProgressStringToConsole(progressString, installPercentage);            
        }


        private static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        private static void PrintProgressStringToConsole(string progressString, double installPercentage)
        {
            ClearCurrentConsoleLine();
            Console.Write($"{progressString}");
           
            
            
        }

        private static void AddProgressValue(ref StringBuilder sb, double installPercentage)
        {
            sb.Append(installPercentage + "%" + " Complete");
        }

        private static void AddSpacingToString(ref StringBuilder sb, int progressCharsToPrint)
        {
            int spacesToAdd = progressBarSpacing - progressCharsToPrint;
            for (int i = 0; i < spacesToAdd; i++)
            {
                sb.Append(" ");
            }
        }

        private static void AddProgressBar(ref StringBuilder sb, int progressCharsToPrint)
        {
            for (int i = 0; i < progressCharsToPrint; i++)
            {
                sb.Append(progressChar);
            }
        }
    }
}
