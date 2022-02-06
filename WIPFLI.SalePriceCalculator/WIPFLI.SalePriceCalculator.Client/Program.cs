using System;
using WIPFLI.SalePriceCalculator.BAL;
using WIPFLI.SalePriceCalculator.Entity;
using WIPFLI.SalePriceCalculator.Utility;
using System.Collections.Generic;
using System.Linq;
using WIPFLI.SalePriceCalculator.BAL.PriceCalculator;
using WIPFLI.SalePriceCalculator.BAL.Logger;

namespace WIPFLI.SalePriceCalculator.Client
{
    /// <summary>
    /// Developed By Aditya Bhushan Chaturvedi,
    /// Main Class for Price Calculator Program
    /// Date : 06/02/2022
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            #region Common Objects Declartions
            Product product = new Product();
            var logger = new Logger();
            #endregion        
            try
            {
                List<Product> allProducts = GetProducts(logger);
                product = GetUserInputs(logger, allProducts, product);
                PriceCalculator priceCalculator = CalculateDiscount(logger, product);
                DisplayDetailedDiscount(logger, product, priceCalculator);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }

        /// <summary>
        /// Calculate Discount Main logic called
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        private static PriceCalculator CalculateDiscount(Logger logger, Product product)
        {
            logger.LogInfo("------------------------------------", ConsoleColor.Gray);
            logger.LogInfo("Total price is: " + product.UnitPrice * product.Quantity, ConsoleColor.Gray);
            PriceCalculator priceCalculator = new PriceCalculator(product);
            decimal discountedPrice = priceCalculator.CalculatePrice();
            logger.LogInfo("Discounted price is: " + discountedPrice, ConsoleColor.Gray);
            return priceCalculator;
        }

        /// <summary>
        /// Display detailed message for all types of discounts applied
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="product"></param>
        /// <param name="priceCalculator"></param>
        private static void DisplayDetailedDiscount(Logger logger, Product product, PriceCalculator priceCalculator)
        {
            logger.LogDebug("------------------------------------");
            if (priceCalculator.fix > 0)
            {
                logger.LogDebug(Utility.Utility.FIX + " : " + priceCalculator.fix);
            }

            if (priceCalculator.freeUnits > 0)
            {
                logger.LogDebug(Utility.Utility.UNIT + " : " + priceCalculator.freeUnits + " Free Unit of " + product.Name);
            }

            if (priceCalculator.weekday > 0)
            {
                logger.LogDebug(Utility.Utility.WEEKDAY + " : " + priceCalculator.weekday);
            }
            logger.LogDebug("------------------------------------");
        }

        /// <summary>
        /// Method is used to get User inputs i.e. product id and quantity
        /// </summary>
        /// <param name="allProducts"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        private static Product GetUserInputs(Logger logger, List<Product> allProducts, Product product)
        {
            string userInput = string.Empty;
            bool isNumber;
            int productId = 0;
            short quantity = 0;

            logger.LogInfo(string.Format("Kinldy Enter Product ID - From above list: "), ConsoleColor.Cyan);
            userInput = Console.ReadLine();
            isNumber = int.TryParse(userInput, out productId);
            product.Id = productId;

            product = (from pro in allProducts where pro.Id == product.Id select pro).FirstOrDefault();
            logger.LogInfo(string.Format("Kinldy Enter Product Quantity - Should not exceed to 100: "), ConsoleColor.Cyan);

            userInput = Console.ReadLine();
            isNumber = short.TryParse(userInput, out quantity);
            product.Quantity = quantity;
            return product;
        }

        /// <summary>
        /// Get some Static projects after following layered architechture.
        /// </summary>
        /// <param name="logger"></param>
        /// <returns></returns>
        private static List<Product> GetProducts(Logger logger)
        {
            logger.LogInfo(Utility.Utility.COMPANY + " :: " + Utility.Utility.PROJECT, ConsoleColor.Yellow);
            logger.LogInfo("Developed by :: " + Utility.Utility.AUTHOR, ConsoleColor.Yellow);
            BusinessLayer businessLayer = new BusinessLayer();
            var allProducts = businessLayer.GetProducts();
            logger.LogInfo("------------------------------------", ConsoleColor.Green);
            foreach (Product products in allProducts)
            {
                logger.LogInfo("Product ID: - > " + products.Id + " Price - > " + products.UnitPrice + " Name - > " + products.Name, ConsoleColor.Yellow);
            }
            logger.LogInfo("------------------------------------", ConsoleColor.Green);
            return allProducts;
        }
    }
}
