using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using WIPFLI.SalePriceCalculator.Entity;

namespace WIPFLI.SalePriceCalculator.BAL.Discount
{
    /// <summary>
    /// Developed By Aditya Bhushan Chaturvedi,
    /// Date : 06/02/2022
    /// </summary>
    public class UnitDiscount : IDiscount
    {
        Product product = null;
        public UnitDiscount(Product product)
        {
            this.product = product;
        }

        public decimal CalculateDiscount()
        {
            if (product.Quantity >= 3)
            {
                return product.Quantity / 3;
            }
            else
            {
                return 0;
            }
        }

        public bool IsApplicable()
        {
            BusinessLayer businessLayer = new BusinessLayer();
            var unitDiscProduct = (businessLayer.GetUnitDiscProducts()).FirstOrDefault();
            return product.Id == unitDiscProduct.Id ? true : false;
        }
    }
}
