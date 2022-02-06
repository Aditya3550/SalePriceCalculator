using System;
using System.Collections.Generic;
using System.Text;
using WIPFLI.SalePriceCalculator.Entity;

namespace WIPFLI.SalePriceCalculator.BAL.Discount
{
    /// <summary>
    /// Developed By Aditya Bhushan Chaturvedi,
    /// Date : 06/02/2022
    /// </summary>
    public interface IDiscount
    {
        bool IsApplicable();
        decimal CalculateDiscount();
    }
}
