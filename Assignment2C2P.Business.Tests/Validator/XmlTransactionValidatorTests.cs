using Assignment2C2P.Business.Model.Xml;
using Assignment2C2P.Business.Validator;
using Assignment2C2P.Business.Validator.Interface;
using Assignment2C2P.Shared.Exceptions;
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
        public void Validate_Should_NotThrowRecordInvalidException_When_AllFieldsAreValid()
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

            _validator.Validate(trans);
        }

        [TestMethod]
        public void Validate_Should_ThrowRecordInvalidException_When_TransactionIdLengthIsGreatherThan50()
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

            Assert.ThrowsException<RecordInvalidException>(() => _validator.Validate(trans));
        }

        [TestMethod]
        public void Validate_Should_ThrowRecordInvalidException_When_AmountIsNotDecimal()
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

            Assert.ThrowsException<RecordInvalidException>(() => _validator.Validate(trans));
        }

        [TestMethod]
        public void Validate_Should_ThrowRecordInvalidException_When_CurerncyIsNotInISO4217Format()
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

            Assert.ThrowsException<RecordInvalidException>(() => _validator.Validate(trans));
        }

        [TestMethod]
        public void Validate_Should_ThrowRecordInvalidException_When_TransactionDateIsIncorrectFormat()
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

            Assert.ThrowsException<RecordInvalidException>(() => _validator.Validate(trans));
        }

        [TestMethod]
        public void Validate_Should_ThrowRecordInvalidException_When_StatusIsNotInXmlStatus()
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

            Assert.ThrowsException<RecordInvalidException>(() => _validator.Validate(trans));
        }

        [DataTestMethod]
        [DataRow("Approved")]
        [DataRow("Rejected")]
        [DataRow("Done")]
        public void Validate_Should_NotThrowRecordInvalidException_When_TheStatusIs(string status)
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

            _validator.Validate(trans);
        }
    }
}
