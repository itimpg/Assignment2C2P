using Assignment2C2P.Business.Validator;
using Assignment2C2P.Business.Validator.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Assignment2C2P.Business.Tests.Validator
{
    [TestClass]
    public class CsvTransactionValidatorTests
    {
        private ICsvTransactionValidator _validator;

        public CsvTransactionValidatorTests()
        {
            _validator = new CsvTransactionValidator();
        }

        [TestMethod]
        public void Validate_Should_NotThrowRecordInvalidException_When_AllFieldsAreValid()
        {
            var fields = new[] { "Invoice0000001", "1,000.00", "USD", "20/02/2019 12:33:16", "Approved" };
            var result = _validator.Validate(fields, out string error);
            Assert.IsTrue(result);
            Assert.AreEqual(string.Empty, error);
        }

        [TestMethod]
        public void Validate_Should_ThrowRecordInvalidException_When_TransactionIdLengthIsGreatherThan50()
        {
            var fields = new[] { "012345678901234567890123456789012345678901234567890", "1,000.00", "USD", "20/02/2019 12:33:16", "Approved" };
            var result = _validator.Validate(fields, out string error);
            Assert.IsFalse(result);
            Assert.IsTrue(!string.IsNullOrEmpty(error));
            Assert.IsTrue(error.Contains("Transaction Id"));
        }

        [TestMethod]
        public void Validate_Should_ThrowRecordInvalidException_When_AmountIsNotDecimal()
        {
            var fields = new[] { "Invoice0000001", "1,000.00 USD", "USD", "20/02/2019 12:33:16", "Approved" };
            var result = _validator.Validate(fields, out string error);
            Assert.IsFalse(result);
            Assert.IsTrue(!string.IsNullOrEmpty(error));
            Assert.IsTrue(error.Contains("Amount"));
        }

        [TestMethod]
        public void Validate_Should_ThrowRecordInvalidException_When_CurerncyIsNotInISO4217Format()
        {
            var fields = new[] { "Invoice0000001", "1,000.00", "ABC", "20/02/2019 12:33:16", "Approved" };
            var result = _validator.Validate(fields, out string error);
            Assert.IsFalse(result);
            Assert.IsTrue(!string.IsNullOrEmpty(error));
            Assert.IsTrue(error.Contains("CurrencyCode"));
        }

        [TestMethod]
        public void Validate_Should_ThrowRecordInvalidException_When_TransactionDateIsIncorrectFormat()
        {
            var fields = new[] { "Invoice0000001", "1,000.00", "USD", "20/02/2019T12:33:16", "Approved" };
            var result = _validator.Validate(fields, out string error);
            Assert.IsFalse(result);
            Assert.IsTrue(!string.IsNullOrEmpty(error));
            Assert.IsTrue(error.Contains("Transaction Date"));
        }

        [TestMethod]
        public void Validate_Should_ThrowRecordInvalidException_When_StatusIsNotInCsvStatus()
        {
            var fields = new[] { "Invoice0000001", "1,000.00", "USD", "20/02/2019 12:33:16", "Cancel" };
            var result = _validator.Validate(fields, out string error);
            Assert.IsFalse(result);
            Assert.IsTrue(!string.IsNullOrEmpty(error));
            Assert.IsTrue(error.Contains("Status"));
        }

        [DataTestMethod]
        [DataRow("Approved")]
        [DataRow("Failed")]
        [DataRow("Finished")]
        public void Validate_Should_NotThrowRecordInvalidException_When_TheStatusIs(string status)
        {
            var fields = new[] { "Invoice0000001", "1,000.00", "USD", "20/02/2019 12:33:16", status };
            var result = _validator.Validate(fields, out string error);
            Assert.IsTrue(result);
            Assert.AreEqual(string.Empty, error);
        }
    }
}
