using System;
using System.Globalization;

namespace Assignment2C2P.Shared
{
    public static class DateHelper
    {
        public static string DateFormat = "yyyyMMdd";

        public static string ToString(DateTime dateTime)
        {
            return dateTime.ToString(DateFormat, CultureInfo.InvariantCulture);
        }

        public static DateTime? ToDate(string dateTimeString)
        {
            if (DateTime.TryParseExact(dateTimeString, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
            {
                return result;
            }
            return null;
        }
    }
}
