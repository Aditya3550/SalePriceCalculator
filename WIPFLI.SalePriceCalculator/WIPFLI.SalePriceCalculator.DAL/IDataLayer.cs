using System;
using System.Collections.Generic;
using WIPFLI.SalePriceCalculator.Entity;

namespace WIPFLI.SalePriceCalculator.DAL
{
    public interface IDataLayer<T> : IEnumerable<T> where T : class
    {
        //List<T> GetProducts();
        //T GetProduct(int id);
        //List<T> GetUnitDiscProducts();
    }
}
