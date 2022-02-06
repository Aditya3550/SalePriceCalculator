using System;
using System.Collections.Generic;
using System.Text;

namespace WIPFLI.SalePriceCalculator.BAL.Logger
{
    public interface ILogger
    {
        void LogInfo(string message, ConsoleColor color = ConsoleColor.White);
        void LogDebug(string message);
        void LogError(string message);
    }
}
