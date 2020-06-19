using Assignment2C2P.Business.Model.Xml;
using Assignment2C2P.Business.Validator;
using Assignment2C2P.Business.Validator.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Assignment2C2P.Business.Tests.Validator
{
    [TestClass]
    public class XmlTransactionValidatorTests
    {
        private IXmlTransactionValidator _validator;

        public XmlTransactionValidatorTests()
        {
            _validator = new XmlTransactionValidator();
        }

        [TestMethod]
        public void Validate_Should_ReturnTrueWithoutErrorMessage_When_AllFieldsAreValid()
        {
            var trans = new TransactionsTransaction
            {
                id = "Inv00001",
                PaymentDetails = new TransactionsTransactionPaymentDetails
                {
                    Amount = "200.00",
                    CurrencyCode = "USD"
                },
                TransactionDate = "2019-01-23T13:45:10",
                Status = "Rejected"
            };

            var result = _validator.Validate(trans, out string error);
            Assert.IsTrue(result);
            Assert.AreEqual(string.Empty, error);
        }

        [TestMethod]
        public void Validate_Should_ReturnFalseWithErrorMessage_When_TransactionIdLengthIsGreatherThan50()
        {
            var trans = new TransactionsTransaction
            {
                id = "012345678901234567890123456789012345678901234567891",
                PaymentDetails = new TransactionsTransactionPaymentDetails
                {
                    Amount = "200.00",
                    CurrencyCode = "USD"
                },
                TransactionDate = "2019-01-23T13:45:10",
                Status = "Rejected"
            };

            var result = _validator.Validate(trans, out string error);
            Assert.IsFalse(result);
            Assert.IsTrue(!string.IsNullOrEmpty(error));
            Assert.IsTrue(error.Contains("Transaction Id"));
        }

        [TestMethod]
        public void Validate_Should_ReturnFalseWithErrorMessage_When_AmountIsNotDecimal()
        {
            var trans = new TransactionsTransaction
            {
                id = "Inv00001",
                PaymentDetails = new TransactionsTransactionPaymentDetails
                {
                    Amount = "200.00 USD",
                    CurrencyCode = "USD"
                },
                TransactionDate = "2019-01-23T13:45:10",
                Status = "Rejected"
            };

            var result = _validator.Validate(trans, out string error);
            Assert.IsFalse(result);
            Assert.IsTrue(!string.IsNullOrEmpty(error));
            Assert.IsTrue(error.Contains("Amount"));
        }

        [TestMethod]
        public void Validate_Should_ReturnFalseWithErrorMessage_When_CurerncyIsNotInISO4217Format()
        {
            var trans = new TransactionsTransaction
            {
                id = "Inv00001",
                PaymentDetails = new TransactionsTransactionPaymentDetails
                {
                    Amount = "200.00",
                    CurrencyCode = "ABC"
                },
                TransactionDate = "2019-01-23T13:45:10",
                Status = "Rejected"
            };

            var result = _validator.Validate(trans, out string error);
            Assert.IsFalse(result);
            Assert.IsTrue(!string.IsNullOrEmpty(error));
            Assert.IsTrue(error.Contains("CurrencyCode"));
        }

        [TestMethod]
        public void Validate_Should_ReturnFalseWithErrorMessage_When_TransactionDateIsIncorrectFormat()
        {
            var trans = new TransactionsTransaction
            {
                id = "Inv00001",
                PaymentDetails = new TransactionsTransactionPaymentDetails
                {
                    Amount = "200.00",
                    CurrencyCode = "USD"
                },
                TransactionDate = "2019-01-23 13:45:10",
                Status = "Rejected"
            };

            var result = _validator.Validate(trans, out string error);
            Assert.IsFalse(result);
            Assert.IsTrue(!string.IsNullOrEmpty(error));
            Assert.IsTrue(error.Contains("Transaction Date"));
        }

        [TestMethod]
        public void Validate_Should_ReturnFalseWithErrorMessage_When_StatusIsNotInXmlStatus()
        {
            var trans = new TransactionsTransaction
            {
                id = "Inv00001",
                PaymentDetails = new TransactionsTransactionPaymentDetails
                {
                    Amount = "200.00",
                    CurrencyCode = "USD"
                },
                TransactionDate = "2019-01-23T13:45:10",
                Status = "Cancel"
            };

            var result = _validator.Validate(trans, out string error);
            Assert.IsFalse(result);
            Assert.IsTrue(!string.IsNullOrEmpty(error));
            Assert.IsTrue(error.Contains("Status"));
        }

        [TestMethod]
        public void Validate_Should_ReturnFalseWithErrorMessage_When_PaymentDetailsIsNull()
        {
            var trans = new TransactionsTransaction
            {
                id = "Inv00001",
                PaymentDetails = null,
                TransactionDate = "2019-01-23T13:45:10",
                Status = "Cancel"
            };

            var result = _validator.Validate(trans, out string error);
            Assert.IsFalse(result);
            Assert.IsTrue(!string.IsNullOrEmpty(error));
            Assert.IsTrue(error.Contains("Amount"));
            Assert.IsTrue(error.Contains("CurrencyCode"));
        }

        [DataTestMethod]
        [DataRow("Approved")]
        [DataRow("Rejected")]
        [DataRow("Done")]
        public void Validate_Should_ReturnTrueWithoutErrorMessage_When_TheStatusIs(string status)
        {
            var trans = new TransactionsTransaction
            {
                id = "Inv00001",
                PaymentDetails = new TransactionsTransactionPaymentDetails
                {
                    Amount = "200.00",
                    CurrencyCode = "USD"
                },
                TransactionDate = "2019-01-23T13:45:10",
                Status = status
            };

            var result = _validator.Validate(trans, out string error);
            Assert.IsTrue(result);
            Assert.AreEqual(string.Empty, error);
        }
    }
}
