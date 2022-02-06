using System;
using System.Collections.Generic;
using WIPFLI.SalePriceCalculator.Entity;

namespace WIPFLI.SalePriceCalculator.BAL
{
    public interface IBusinessLayer
    {
        List<Product> GetProducts();
        Product GetProduct(int id);
        List<Product> GetUnitDiscProducts();
    }
}
