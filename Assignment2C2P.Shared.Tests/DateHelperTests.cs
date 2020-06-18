using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Assignment2C2P.Shared.Tests
{
    [TestClass]
    public class DateHelperTests
    {
        [TestMethod]
        public void ToString_Should_ConvertDateToString()
        {
            var result = DateHelper.ToString(new DateTime(2020, 7, 9));
            Assert.AreEqual("20200709", result);
        }

        [TestMethod]
        public void ToDate_Should_ConvertStringToDate_When_InputTextIsValidFormat()
        {
            var result = DateHelper.ToDate("20200709");

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Equals(new DateTime(2020, 7, 9)));
        }

        [TestMethod]
        public void ToDate_Should_ReturnNullableDate_When_InputTextIsInvalidFormat()
        {
            var result = DateHelper.ToDate("20201309");

            Assert.IsNull(result);
        }
    }
}
