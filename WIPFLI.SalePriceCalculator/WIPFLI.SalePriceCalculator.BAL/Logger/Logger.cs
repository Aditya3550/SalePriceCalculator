using System;
using System.Collections.Generic;
using System.Text;

namespace WIPFLI.SalePriceCalculator.BAL.Logger
{
    public class Logger : ILogger
    {
        public Logger()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
        }

        public void LogInfo(string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Write(message);
        }

        public void LogDebug(string message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Write(message);
        }

        public void LogError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Write(message);
        }

        private static void Write(string message)
        {
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
