using System;
using System.Globalization;

namespace Assignment2C2P.Shared
{
    public static class DateHelper
    {
        public static string DateFormat = "yyyyMMdd";

        public static string ToString(DateTime dateTime)
        {
            CultureInfo enUS = new CultureInfo("en-US");
            return dateTime.ToString(DateFormat, enUS);
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
