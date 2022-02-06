using System;

namespace WIPFLI.SalePriceCalculator.Entity
{
    /// <summary>
    /// Developed By Aditya Bhushan Chaturvedi,
    /// Date : 06/02/2022
    /// </summary>
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Int16 Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
    }
}
