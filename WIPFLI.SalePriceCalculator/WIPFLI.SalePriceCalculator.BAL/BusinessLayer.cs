using System;
using System.Collections.Generic;
using WIPFLI.SalePriceCalculator.Entity;
using WIPFLI.SalePriceCalculator.DAL;
using WIPFLI.SalePriceCalculator.BAL.Logger;

namespace WIPFLI.SalePriceCalculator.BAL
{
    /// <summary>
    /// Developed By Aditya Bhushan Chaturvedi,
    /// BAL to communicate from DAL
    /// Date : 06/02/2022
    /// </summary>
    public class BusinessLayer : IBusinessLayer, IDisposable
    {
        private readonly DataLayer<Product> dataLayer = null;
        public BusinessLayer()
        {
            this.dataLayer = new DataLayer<Product>();
        }

        public List<Product> GetProducts()
        {
            return dataLayer.GetProducts();
        }

        public List<Product> GetUnitDiscProducts()
        {
            return dataLayer.GetUnitDiscProducts();
        }

        public Product GetProduct(int id)
        {
            return dataLayer.GetProduct(id);
        }

        public void Dispose()
        {
            this.Dispose();
            GC.SuppressFinalize(true);
        }
    }
}
