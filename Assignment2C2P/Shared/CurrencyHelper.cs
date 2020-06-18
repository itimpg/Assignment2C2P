using System.Globalization;
using System.Linq;

namespace Assignment2C2P.Shared
{
    public static class CurrencyHelper
    {
        public static string[] GetCurrencyCodes()
        {
            return CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                .Select(x => (new RegionInfo(x.LCID)).ISOCurrencySymbol)
                .Distinct()
                .OrderBy(x => x)
                .ToArray();
        }
    }
}
