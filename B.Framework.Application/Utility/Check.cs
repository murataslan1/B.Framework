using System;
using System.Collections.Generic;
using B.Framework.Domain.Shared.Utility.Check;

namespace B.Framework.Application.Utility
{
    public class Check
    {
        private static List<Predicate<object>> numericMethodLists = new List<Predicate<object>>()
        {
            CanConvertInt32,
            CanConvertInt64,
            CanConvertDouble,
            CanConvertFloat,
            CanConvertDecimal
        };

        public static void Null(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException(CheckBaseDefinations.Errors.ArgumentIsNull);
        }

        public static void Null(object obj, string message)
        {
            if (obj == null)
                throw new ArgumentNullException(message);
        }


        public static void Null<T>(object obj, string customMessage = "") where T : ArgumentNullException, new()
        {
            if (obj == null)
            {
                if (new ArgumentNullException(customMessage) is T error) throw error;
            }
        }

        public static bool CanConvertInt32(object obj)
        {
            return Int32.TryParse(obj.ToString(), out _);
        }

        public static bool CanConvertInt64(object obj)
        {
            return Int32.TryParse(obj.ToString(), out _);
        }

        public static bool CanConvertDouble(object obj)
        {
            return double.TryParse(obj.ToString(), out _);
        }

        public static bool CanConvertFloat(object obj)
        {
            return float.TryParse(obj.ToString(), out _);
        }

        public static bool CanConvertDecimal(object obj)
        {
            return decimal.TryParse(obj.ToString(), out _);
        }


        public static bool IsNumeric(object obj)
        {
            foreach (var method in numericMethodLists)
            {
                var res = method.Invoke(obj);

                if (res)
                    return true;
            }
            return false;
        }
    }
}