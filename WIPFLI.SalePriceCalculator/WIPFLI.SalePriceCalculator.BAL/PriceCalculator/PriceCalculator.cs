using System;
using System.Collections.Generic;
using System.Text;
using WIPFLI.SalePriceCalculator.BAL.Discount;
using WIPFLI.SalePriceCalculator.Entity;
using WIPFLI.SalePriceCalculator.Utility;

namespace WIPFLI.SalePriceCalculator.BAL.PriceCalculator
{
    /// <summary>
    /// Developed By Aditya Bhushan Chaturvedi,
    /// This is Important class for calulating all types of discounts
    /// Date : 06/02/2022
    /// </summary>
    public class PriceCalculator : IPriceCalculator
    {
        Product product = null;
        public decimal fix = 0;
        public decimal freeUnits = 0;
        public decimal weekday = 0;
        public PriceCalculator(Product product)
        {
            this.product = product ?? throw new ArgumentNullException(nameof(product));
        }

        public decimal CalculatePrice(string currentDay = "")
        {
            decimal price = 0;
            if (product != null)
            {
                price = product.Quantity * product.UnitPrice;
                price = PriceAfterDiscount(price, currentDay);
            }
            else
            {
                throw new NullReferenceException();
            }
            return price;
        }

        private decimal PriceAfterDiscount(decimal price, string currentDay = "")
        {
            GetFixDiscount();
            if (fix == 0)
            {
                GetUnitDiscount();
            }
            GetWeekdayDiscount(currentDay);
            price -= fix + weekday;
            return price;
        }

        private void GetFixDiscount()
        {
            FixDiscount fixDiscount = new FixDiscount(product);
            if (fixDiscount.IsApplicable())
            {
                fix = fixDiscount.CalculateDiscount();
            }
        }

        private void GetUnitDiscount()
        {
            UnitDiscount unitDiscount = new UnitDiscount(product);
            if (unitDiscount.IsApplicable())
            {
                freeUnits = unitDiscount.CalculateDiscount();
            }
        }

        private void GetWeekdayDiscount(string currentDay = "")
        {
            // currentDay = "Wednesday"; // For Testing
            // currentDay = "Monday"; // For Testing
            if (string.IsNullOrEmpty(currentDay))
                currentDay = DateTime.Now.DayOfWeek.ToString();

            if (currentDay == WeekDayDiscount.Wednesday.ToString())
            {
                weekday = (int)WeekDayDiscount.Wednesday;
            }
            else if (currentDay == WeekDayDiscount.Monday.ToString())
            {
                weekday = (int)WeekDayDiscount.Monday;
            }
            else
            {
                weekday = 0;
            }

            if (weekday > 0)
            {
                weekday = (product.UnitPrice * product.Quantity) * (weekday / 100);
            }
        }
    }
}
