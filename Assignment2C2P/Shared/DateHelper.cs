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
            DateTime result;
            CultureInfo enUS = new CultureInfo("en-US");
            if (DateTime.TryParseExact(dateTimeString, DateFormat, enUS, DateTimeStyles.None, out result))
            {
                return result;
            }
            return null;
        }
    }
}
