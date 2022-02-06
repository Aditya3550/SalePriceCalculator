using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WIPFLI.SalePriceCalculator.Entity;

namespace WIPFLI.SalePriceCalculator.DAL
{
    /// <summary>
    /// Developed By Aditya Bhushan Chaturvedi,
    /// Used to provide data using EF or ADO.NET
    /// Currently returning some static project data
    /// Date : 06/02/2022
    /// </summary>
    public class DataLayer<T> : IDisposable
    {
        public Product GetProduct(int id)
        {
            return (from pro in GetProducts() where pro.Id == id select pro).FirstOrDefault();
        }

        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            products.Add(new Product()
            {
                Id = 1,
                Name = "Thumbs up",
                UnitPrice = 100,
                Quantity = 0,
                Discount = 10
            });
            products.Add(new Product()
            {
                Id = 2,
                Name = "Toilet Cleaner",
                UnitPrice = 200,
                Quantity = 0,
                Discount = 10
            });
            products.Add(new Product()
            {
                Id = 3,
                Name = "Mangoes",
                UnitPrice = 300,
                Quantity = 0,
                Discount = 0
            });
            products.Add(new Product()
            {
                Id = 4,
                Name = "Cooking Oil Bottle - 1 liter",
                UnitPrice = 400,
                Quantity = 0,
                Discount = 0
            });
            products.Add(new Product()
            {
                Id = 5,
                Name = "Sugar",
                UnitPrice = 500,
                Quantity = 0,
                Discount = 0
            });
            products.Add(new Product()
            {
                Id = 6,
                Name = "Tea",
                UnitPrice = 600,
                Quantity = 0,
                Discount = 5
            });
            products.Add(new Product()
            {
                Id = 7,
                Name = "Bulbs",
                UnitPrice = 700,
                Quantity = 0,
                Discount = 0
            });
            
            return products;
        }

        public List<Product> GetUnitDiscProducts()
        {
           return (from pro in GetProducts() where pro.Id == 4 select pro).ToList();           
        }

        public void Dispose()
        {
            this.Dispose();
            GC.SuppressFinalize(true);
        }
    }
}
