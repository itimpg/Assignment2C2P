using System.Globalization;
using System.Linq;

namespace Assignment2C2P.Shared
{
    public static class CurrencyHelper
    {
        public static string[] GetCurrencyCodes()
        {
            return CultureInfo.GetCultures(CultureTypes.AllCultures)
                .Select(culture => culture.NumberFormat.CurrencySymbol)
                .Distinct()
                .ToArray();
        }
    }
}
