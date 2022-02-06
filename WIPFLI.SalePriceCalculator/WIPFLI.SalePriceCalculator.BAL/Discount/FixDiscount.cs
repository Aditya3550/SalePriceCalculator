using System;
using System.Collections.Generic;
using System.Text;
using WIPFLI.SalePriceCalculator.Entity;

namespace WIPFLI.SalePriceCalculator.BAL.Discount
{
    /// <summary>
    /// Developed By Aditya Bhushan Chaturvedi,
    /// Class to Calculator fix discount
    /// Date : 06/02/2022
    /// </summary>
    public class FixDiscount : IDiscount
    {
        Product product = null;
        public FixDiscount(Product product)
        {
            this.product = product;
        }

        public decimal CalculateDiscount()
        {
            return product.Discount > 0 ? (product.UnitPrice * product.Quantity) * (product.Discount / 100) : (product.UnitPrice * product.Quantity);
        }

        public bool IsApplicable()
        {
            return product.Discount > 0 ? true : false;
        }
    }
}
