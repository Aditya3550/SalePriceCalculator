using System;

namespace WIPFLI.SalePriceCalculator.Utility
{
    /// <summary>
    /// Developed By Aditya Bhushan Chaturvedi,
    /// Here all common contents will de displayed.
    /// Date : 06/02/2022
    /// </summary>
    public static class Utility
    {
        public const string COMPANY = "WIPFLI";
        public const string PROJECT = "Store Item Sale Price Calculator";
        public const string AUTHOR = "Aditya Bhushan Chaturvedi";
        public const string UNIT = "Unit Discounts";
        public const string FIX = "Fix Discounts";
        public const string WEEKDAY = "Weekday Discounts";
    }

    public enum WeekDayDiscount
    {
        Sunday = 0,
        Monday = 2,
        Tuesday = 0,
        Wednesday = 5,
        Thursday = 0,
        Friday = 0,
        Saturday = 0
    }
}
