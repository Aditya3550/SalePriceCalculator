using System;
using System.Collections.Generic;
using System.Text;
using WIPFLI.SalePriceCalculator.Entity;

namespace WIPFLI.SalePriceCalculator.BAL.PriceCalculator
{
    public interface IPriceCalculator
    {
        decimal CalculatePrice(string currentDay = "");
    }
}
