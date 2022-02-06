using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using WIPFLI.SalePriceCalculator.BAL.Discount;
using WIPFLI.SalePriceCalculator.BAL.PriceCalculator;
using WIPFLI.SalePriceCalculator.Entity;

namespace WIPFLI.SalePriceCalculator.Tests
{
    /// <summary>
    /// Developed By Aditya Bhushan Chaturvedi,
    /// This Unit Test Cases is only created for PriceCalculator
    /// All Test cases Exception/Negative/Positeve in One Place
    /// Date : 06/02/2022
    /// </summary>
    [TestClass]
    public class SalePriceCalculatorUnitTests
    {
        #region Unit Cases :: Common Expected Exception
        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void PriceCalculator_NotImplementedException()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PriceCalculator_ArgumentNullExceptionProduct()
        {
            var PriceCalculator = new PriceCalculator(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PriceCalculator_NullReferenceExceptionProductQuantity()
        {
            Product product = null;
            var PriceCalculator = new PriceCalculator(product);
            if (product.Quantity <= 0)
            {
                throw new NullReferenceException();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void PriceCalculator_NullReferenceExceptionProductNull()
        {
            Product product = null;
            throw new NullReferenceException();
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void PriceCalculator_GetFixDiscount()
        {
            FixDiscount fixDiscount = new FixDiscount(null);
            Boolean actual = fixDiscount.IsApplicable();
            Assert.AreEqual(false, actual);
        }
        #endregion

        #region Unit Cases :: Type Null Checks
        [TestMethod]
        public void PriceCalculator_ProductInputTypeCheck_Quantity_Short()
        {
            Product product = new Product() { Id = 1, Name = "Test Name", Quantity = 2, UnitPrice = 100.0M };
            Assert.AreEqual(product.Quantity.GetType(), typeof(short));
        }

        [TestMethod]
        public void PriceCalculator_ProductInputTypeCheck_UnitPrice_Decimal()
        {
            Product product = new Product() { Id = 1, Name = "Test Name", Quantity = 2, UnitPrice = 100.0M };
            Assert.AreEqual(product.UnitPrice.GetType(), typeof(decimal));
        }

        [TestMethod]
        public void PriceCalculator_PriceCalculatorNullValueCheck_NotNull()
        {
            Product product = new Product() { Id = 1, Name = "Test Name", Quantity = 2, UnitPrice = 100.0M };
            IPriceCalculator priceCalculator = new PriceCalculator(product);
            Assert.IsNotNull(priceCalculator);
        }

        [TestMethod]
        public void PriceCalculator_PriceCalculatorNullValueCheck_NameNull()
        {
            Product product = new Product() { Id = 1, Name = null, Quantity = 2, UnitPrice = 100.0M };
            IPriceCalculator priceCalculator = new PriceCalculator(product);
            Assert.IsNotNull(priceCalculator);
        }

        [TestMethod]
        public void PriceCalculator_ProductNullValueCheck_NotNull()
        {
            Product product = new Product() { Id = 1, Name = "Test Name", Quantity = 2, UnitPrice = 500.0M };
            Assert.IsNotNull(product);
        }
        #endregion

        #region Unit Cases :: Calculation Checks
        [TestMethod]
        public void PriceCalculator_CalculatedPriceWihoutDiscount()
        {
            Product product = new Product() { Id = 1, Name = "Test Name", Quantity = 2, UnitPrice = 300.0M };
            decimal price = product.Quantity * product.UnitPrice;
            Assert.AreEqual(600.0M, price);
        }

        [TestMethod]
        [DataRow(1, "Thumbs up", 100, 10, 10, 0, 0, 0)]
        [DataRow(2, "Toilet Cleaner", 200, 7, 10, 0, 0, 0)]
        [DataRow(3, "Mangoes", 300, 4, 0, 0, 0, 0)]
        [DataRow(4, "Cooking Oil Bottle - 1 liter", 400, 35, 0, 0, 0, 0)]
        [DataRow(4, "Cooking Oil Bottle - 1 liter", 200, 3, 0, 0, 0, 0)]
        [DataRow(4, "Cooking Oil Bottle - 1 liter", 200, 27, 0, 0, 0, 0)]
        [DataRow(5, "Sugar", 50, 0, 0, 0, 0, 0)]
        [DataRow(6, "Tea", 40, 2, 5, 0, 0, 0)]
        [DataRow(7, "Bulbs", 20, 4, 0, 0, 0, 0)]
        public void PriceCalculator_CheckInitFixDiscount(int id, string name,
            int unitPrice, int quantity, int discount, int fixDiscount, int freeUnits, int weekdayDiscount)
        {
            Product product = new Product()
            {
                Id = id,
                Name = name,
                UnitPrice = unitPrice,
                Quantity = Convert.ToInt16(quantity),
                Discount = discount
            };

            PriceCalculator priceCalculator = new PriceCalculator(product);
            Assert.IsNotNull(priceCalculator.fix);
            Assert.AreEqual(priceCalculator.fix, fixDiscount);
        }


        [TestMethod]
        [DataRow(1, "Thumbs up", 100, 10, 10, 0, 0, 0)]
        [DataRow(2, "Toilet Cleaner", 200, 7, 10, 0, 0, 0)]
        [DataRow(3, "Mangoes", 300, 4, 0, 0, 0, 0)]
        [DataRow(4, "Cooking Oil Bottle - 1 liter", 400, 35, 0, 0, 0, 0)]
        [DataRow(4, "Cooking Oil Bottle - 1 liter", 200, 3, 0, 0, 0, 0)]
        [DataRow(4, "Cooking Oil Bottle - 1 liter", 200, 27, 0, 0, 0, 0)]
        [DataRow(5, "Sugar", 50, 0, 0, 0, 0, 0)]
        [DataRow(6, "Tea", 40, 2, 5, 0, 0, 0)]
        [DataRow(7, "Bulbs", 20, 4, 0, 0, 0, 0)]
        public void PriceCalculator_CheckInitFreeUnits(int id, string name,
            int unitPrice, int quantity, int discount, int fixDiscount, int freeUnits, int weekdayDiscount)
        {
            Product product = new Product()
            {
                Id = id,
                Name = name,
                UnitPrice = unitPrice,
                Quantity = Convert.ToInt16(quantity),
                Discount = discount
            };

            PriceCalculator priceCalculator = new PriceCalculator(product);
            Assert.IsNotNull(priceCalculator.freeUnits);
            Assert.AreEqual(priceCalculator.freeUnits, freeUnits);
        }


        [TestMethod]
        [DataRow(1, "Thumbs up", 100, 10, 10, 0, 0, 0)]
        [DataRow(2, "Toilet Cleaner", 200, 7, 10, 0, 0, 0)]
        [DataRow(3, "Mangoes", 300, 4, 0, 0, 0, 0)]
        [DataRow(4, "Cooking Oil Bottle - 1 liter", 400, 35, 0, 0, 0, 0)]
        [DataRow(4, "Cooking Oil Bottle - 1 liter", 200, 3, 0, 0, 0, 0)]
        [DataRow(4, "Cooking Oil Bottle - 1 liter", 200, 27, 0, 0, 0, 0)]
        [DataRow(5, "Sugar", 50, 0, 0, 0, 0, 0)]
        [DataRow(6, "Tea", 40, 2, 5, 0, 0, 0)]
        [DataRow(7, "Bulbs", 20, 4, 0, 0, 0, 0)]
        public void PriceCalculator_CheckInitWeekdayDiscount(int id, string name,
            int unitPrice, int quantity, int discount, int fixDiscount, int freeUnits, int weekdayDiscount)
        {
            Product product = new Product()
            {
                Id = id,
                Name = name,
                UnitPrice = unitPrice,
                Quantity = Convert.ToInt16(quantity),
                Discount = discount
            };

            PriceCalculator priceCalculator = new PriceCalculator(product);
            Assert.IsNotNull(priceCalculator.weekday);
            Assert.AreEqual(priceCalculator.weekday, weekdayDiscount);
        }


        [TestMethod]
        [DataRow(1, "Thumbs up", 100, 10, 10, 0, 0, 0)]
        [DataRow(2, "Toilet Cleaner", 200, 7, 10, 0, 0, 0)]
        [DataRow(3, "Mangoes", 300, 4, 0, 0, 0, 0)]
        [DataRow(4, "Cooking Oil Bottle - 1 liter", 400, 35, 0, 0, 0, 0)]
        [DataRow(4, "Cooking Oil Bottle - 1 liter", 200, 3, 0, 0, 0, 0)]
        [DataRow(4, "Cooking Oil Bottle - 1 liter", 200, 27, 0, 0, 0, 0)]
        [DataRow(5, "Sugar", 50, 0, 0, 0, 0, 0)]
        [DataRow(6, "Tea", 40, 2, 5, 0, 0, 0)]
        [DataRow(7, "Bulbs", 20, 4, 0, 0, 0, 0)]
        public void PriceCalculator_CheckInitAllDiscount(int id, string name,
            int unitPrice, int quantity, int discount, int fixDiscount, int freeUnits, int weekdayDiscount)
        {
            Product product = new Product()
            {
                Id = id,
                Name = name,
                UnitPrice = unitPrice,
                Quantity = Convert.ToInt16(quantity),
                Discount = discount
            };

            PriceCalculator priceCalculator = new PriceCalculator(product);
            Assert.IsNotNull(priceCalculator.fix);
            Assert.IsNotNull(priceCalculator.freeUnits);
            Assert.IsNotNull(priceCalculator.weekday);
            Assert.AreEqual(priceCalculator.fix, fixDiscount);
            Assert.AreEqual(priceCalculator.freeUnits, freeUnits);
            Assert.AreEqual(priceCalculator.weekday, weekdayDiscount);
        }

        [TestMethod]
        [DataRow(1, "Thumbs up", 100, 10, 10, 100, 0, 0)]
        [DataRow(2, "Toilet Cleaner", 200, 5, 10, 100, 0, 0)]
        [DataRow(3, "Mangoes", 300, 4, 0, 0, 0, 0)]
        [DataRow(4, "Cooking Oil Bottle - 1 liter", 400, 35, 0, 0, 0, 0)]
        [DataRow(4, "Cooking Oil Bottle - 1 liter", 200, 3, 0, 0, 0, 0)]
        [DataRow(4, "Cooking Oil Bottle - 1 liter", 200, 27, 0, 0, 0, 0)]
        [DataRow(5, "Sugar", 50, 0, 0, 0, 0, 0)]
        [DataRow(6, "Tea", 50, 2, 5, 5, 0, 0)]
        [DataRow(7, "Bulbs", 20, 4, 0, 0, 0, 0)]
        public void PriceCalculator_CaculateFixedDiscount(int id, string name,
           int unitPrice, int quantity, int discount, int fixDiscount, int freeUnits, int weekdayDiscount)
        {
            Product product = new Product()
            {
                Id = id,
                Name = name,
                UnitPrice = unitPrice,
                Quantity = Convert.ToInt16(quantity),
                Discount = discount
            };

            PriceCalculator priceCalculator = new PriceCalculator(product);
            priceCalculator.CalculatePrice();
            Assert.IsNotNull(priceCalculator.fix);
            Assert.AreEqual(priceCalculator.fix, fixDiscount);
        }

        [TestMethod]
        [DataRow(1, "Thumbs up", 100, 10, 10, 100, 0, 0)]
        [DataRow(2, "Toilet Cleaner", 200, 5, 10, 100, 0, 0)]
        [DataRow(3, "Mangoes", 300, 4, 0, 0, 0, 0)]
        [DataRow(4, "Cooking Oil Bottle - 1 liter", 400, 30, 0, 0, 10, 0)]
        [DataRow(4, "Cooking Oil Bottle - 1 liter", 200, 3, 0, 0, 1, 0)]
        [DataRow(4, "Cooking Oil Bottle - 1 liter", 200, 13, 0, 0, 4, 0)]
        [DataRow(5, "Sugar", 50, 0, 0, 0, 0, 0)]
        [DataRow(6, "Tea", 50, 2, 5, 5, 0, 0)]
        [DataRow(7, "Bulbs", 20, 4, 0, 0, 0, 0)]
        public void PriceCalculator_CaculateFreeUnits(int id, string name,
         int unitPrice, int quantity, int discount, int fixDiscount, int freeUnits, int weekdayDiscount)
        {
            Product product = new Product()
            {
                Id = id,
                Name = name,
                UnitPrice = unitPrice,
                Quantity = Convert.ToInt16(quantity),
                Discount = discount
            };

            PriceCalculator priceCalculator = new PriceCalculator(product);
            priceCalculator.CalculatePrice();

            Assert.IsNotNull(priceCalculator.freeUnits);
            Assert.AreEqual(priceCalculator.freeUnits, freeUnits);
        }


        /// <summary>
        /// To check this Change your system date select Monday to check 2 % and Webnesday to check 5% discounts
        /// To Test This Method Passing Static Monday or Webnesday To calculate Disciunts
        /// For Monday CalculatePrice("Monday");
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="unitPrice"></param>
        /// <param name="quantity"></param>
        /// <param name="discount"></param>
        /// <param name="fixDiscount"></param>
        /// <param name="freeUnits"></param>
        /// <param name="weekdayDiscount"></param>
        [TestMethod]
        [DataRow(1, "Thumbs up", 100, 10, 10, 100, 0, 20)]
        [DataRow(2, "Toilet Cleaner", 200, 5, 10, 100, 0, 20)]
        [DataRow(3, "Mangoes", 200, 4, 0, 0, 0, 16)]
        [DataRow(4, "Cooking Oil Bottle - 1 liter", 400, 30, 0, 0, 10, 240)]
        [DataRow(4, "Cooking Oil Bottle - 1 liter", 200, 3, 0, 0, 1, 12)]
        [DataRow(4, "Cooking Oil Bottle - 1 liter", 300, 10, 0, 0, 3, 60)]
        [DataRow(5, "Sugar", 500, 2, 0, 0, 0, 20)]
        [DataRow(6, "Tea", 50, 2, 5, 5, 0, 2)]
        [DataRow(7, "Bulbs", 50, 4, 0, 0, 0, 4)]
        public void PriceCalculator_CaculateWeekdayDiscountMonday(int id, string name,
       int unitPrice, int quantity, int discount, int fixDiscount, int freeUnits, int weekdayDiscount)
        {
            Product product = new Product()
            {
                Id = id,
                Name = name,
                UnitPrice = unitPrice,
                Quantity = Convert.ToInt16(quantity),
                Discount = discount
            };

            PriceCalculator priceCalculator = new PriceCalculator(product);
            priceCalculator.CalculatePrice(DayOfWeek.Monday.ToString());

            Assert.IsNotNull(priceCalculator.weekday);
            Assert.AreEqual(priceCalculator.weekday, weekdayDiscount);
        }


        /// <summary>
        /// To check this Change your system date select Monday to check 2 % and Webnesday to check 5% discounts
        /// To Test This Method Passing Static Monday or Webnesday To calculate Disciunts
        /// For Wednesday CalculatePrice("Wednesday");
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="unitPrice"></param>
        /// <param name="quantity"></param>
        /// <param name="discount"></param>
        /// <param name="fixDiscount"></param>
        /// <param name="freeUnits"></param>
        /// <param name="weekdayDiscount"></param>
        [TestMethod]
        [DataRow(1, "Thumbs up", 100, 10, 10, 100, 0, 50)]
        [DataRow(2, "Toilet Cleaner", 200, 5, 10, 100, 0, 50)]
        [DataRow(3, "Mangoes", 200, 4, 0, 0, 0, 40)]
        [DataRow(4, "Cooking Oil Bottle - 1 liter", 400, 30, 0, 0, 10, 600)]
        [DataRow(4, "Cooking Oil Bottle - 1 liter", 200, 3, 0, 0, 1, 30)]
        [DataRow(4, "Cooking Oil Bottle - 1 liter", 300, 10, 0, 0, 3, 150)]
        [DataRow(5, "Sugar", 500, 2, 0, 0, 0, 50)]
        [DataRow(6, "Tea", 50, 2, 5, 5, 0, 5)]
        [DataRow(7, "Bulbs", 50, 4, 0, 0, 0, 10)]
        public void PriceCalculator_CaculateWeekdayDiscountWebnesday(int id, string name,
       int unitPrice, int quantity, int discount, int fixDiscount, int freeUnits, int weekdayDiscount)
        {
            Product product = new Product()
            {
                Id = id,
                Name = name,
                UnitPrice = unitPrice,
                Quantity = Convert.ToInt16(quantity),
                Discount = discount
            };

            PriceCalculator priceCalculator = new PriceCalculator(product);
            priceCalculator.CalculatePrice(DayOfWeek.Wednesday.ToString());

            Assert.IsNotNull(priceCalculator.weekday);
            Assert.AreEqual(priceCalculator.weekday, weekdayDiscount);
        }


        [TestMethod]
        [DataRow(1, "Thumbs up", 100, 10, 10, 100, 0, 50)]
        [DataRow(2, "Toilet Cleaner", 200, 5, 10, 100, 0, 50)]
        [DataRow(3, "Mangoes", 200, 4, 0, 0, 0, 40)]
        [DataRow(4, "Cooking Oil Bottle - 1 liter", 400, 30, 0, 0, 10, 600)]
        [DataRow(4, "Cooking Oil Bottle - 1 liter", 200, 3, 0, 0, 1, 30)]
        [DataRow(4, "Cooking Oil Bottle - 1 liter", 300, 10, 0, 0, 3, 150)]
        [DataRow(5, "Sugar", 500, 2, 0, 0, 0, 50)]
        [DataRow(6, "Tea", 50, 2, 5, 5, 0, 5)]
        [DataRow(7, "Bulbs", 50, 4, 0, 0, 0, 10)]
        public void PriceCalculator_CaculateAllDiscountWebnesday(int id, string name,
    int unitPrice, int quantity, int discount, int fixDiscount, int freeUnits, int weekdayDiscount)
        {
            Product product = new Product()
            {
                Id = id,
                Name = name,
                UnitPrice = unitPrice,
                Quantity = Convert.ToInt16(quantity),
                Discount = discount
            };

            PriceCalculator priceCalculator = new PriceCalculator(product);
            priceCalculator.CalculatePrice(DayOfWeek.Wednesday.ToString());
            Assert.IsNotNull(priceCalculator.fix);
            Assert.AreEqual(priceCalculator.fix, fixDiscount);
            Assert.IsNotNull(priceCalculator.weekday);
            Assert.AreEqual(priceCalculator.weekday, weekdayDiscount);
            Assert.IsNotNull(priceCalculator.freeUnits);
            Assert.AreEqual(priceCalculator.freeUnits, freeUnits);
        }

        /// <summary>
        /// To check this Change your system date select Monday to check 2 % and Webnesday to check 5% discounts
        /// To Test This Method Passing Static Monday or Webnesday To calculate Disciunts
        /// For Monday CalculatePrice("Monday");
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="unitPrice"></param>
        /// <param name="quantity"></param>
        /// <param name="discount"></param>
        /// <param name="fixDiscount"></param>
        /// <param name="freeUnits"></param>
        /// <param name="weekdayDiscount"></param>
        [TestMethod]
        [DataRow(1, "Thumbs up", 100, 10, 10, 100, 0, 20)]
        [DataRow(2, "Toilet Cleaner", 200, 5, 10, 100, 0, 20)]
        [DataRow(3, "Mangoes", 200, 4, 0, 0, 0, 16)]
        [DataRow(4, "Cooking Oil Bottle - 1 liter", 400, 30, 0, 0, 10, 240)]
        [DataRow(4, "Cooking Oil Bottle - 1 liter", 200, 3, 0, 0, 1, 12)]
        [DataRow(4, "Cooking Oil Bottle - 1 liter", 300, 10, 0, 0, 3, 60)]
        [DataRow(5, "Sugar", 500, 2, 0, 0, 0, 20)]
        [DataRow(6, "Tea", 50, 2, 5, 5, 0, 2)]
        [DataRow(7, "Bulbs", 50, 4, 0, 0, 0, 4)]
        public void PriceCalculator_CaculateAllDiscountMonday(int id, string name,
  int unitPrice, int quantity, int discount, int fixDiscount, int freeUnits, int weekdayDiscount)
        {
            Product product = new Product()
            {
                Id = id,
                Name = name,
                UnitPrice = unitPrice,
                Quantity = Convert.ToInt16(quantity),
                Discount = discount
            };

            PriceCalculator priceCalculator = new PriceCalculator(product);
            priceCalculator.CalculatePrice(DayOfWeek.Monday.ToString());
            Assert.IsNotNull(priceCalculator.fix);
            Assert.AreEqual(priceCalculator.fix, fixDiscount);
            Assert.IsNotNull(priceCalculator.weekday);
            Assert.AreEqual(priceCalculator.weekday, weekdayDiscount);
            Assert.IsNotNull(priceCalculator.freeUnits);
            Assert.AreEqual(priceCalculator.freeUnits, freeUnits);
        }

        private static List<Product> GetSampleProducts()
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
        #endregion
    }
}
