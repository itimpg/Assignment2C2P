using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Assignment2C2P.Shared.Tests
{
    [TestClass]
    public class CurrencyHelperTests
    {
        [TestMethod]
        public void GetCurrencyCodes_Should_ReturnCurrencyCodeByIso4217()
        {
            var result = CurrencyHelper.GetCurrencyCodes();

            Assert.IsTrue(result.Contains("EUR"));
            Assert.IsTrue(result.Contains("USD"));
            Assert.IsTrue(result.Contains("THB"));
        }
    }
}
