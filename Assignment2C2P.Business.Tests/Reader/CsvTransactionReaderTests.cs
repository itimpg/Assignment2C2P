using Assignment2C2P.Business.Reader;
using Assignment2C2P.Business.Reader.Interface;
using Assignment2C2P.Business.Validator.Interface;
using Assignment2C2P.Shared.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.IO;

namespace Assignment2C2P.Business.Tests.Reader
{
    [TestClass]
    public class CsvTransactionReaderTests
    {
        private ICsvTransactionReader _reader;
        private Mock<ICsvTransactionValidator> _validator;
        private string _fileDirectory;

        public CsvTransactionReaderTests()
        {
            _validator = new Mock<ICsvTransactionValidator>();
            _reader = new CsvTransactionReader(_validator.Object);
            _fileDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reader", "Files", "Csv");
        }

        [TestMethod]
        public void Read_Should_ThrowUnKnowFormatException_When_AnyRecordIsNotEqual5Fields()
        {
            var error = string.Empty;
            _validator.Setup(x => x.Validate(It.IsAny<string[]>(), out error)).Returns(true);

            var filePath = Path.Combine(_fileDirectory, "incorrect.csv");
            using (var stream = File.OpenRead(filePath))
            {
                Assert.ThrowsException<UnKnowFormatException>(() => _reader.Read(stream));
            }
        }

        [TestMethod]
        public void Read_Should_ThrowTransactionValidateErrorException_When_ThereAreAnyErrorFromValidation()
        {
            var error = "error";
            _validator.SetupSequence(x => x.Validate(It.IsAny<string[]>(), out error))
                .Returns(true)
                .Returns(false);

            var filePath = Path.Combine(_fileDirectory, "correct.csv");
            using (var stream = File.OpenRead(filePath))
            {
                var exception = Assert.ThrowsException<TransactionValidateErrorException>(() => _reader.Read(stream));
                Assert.AreEqual(error, exception.Message);
            }
        }

        [TestMethod]
        public void Read_Should_ReturnTransactionItemList_When_ThereIsNoValidationError()
        {
            var error = string.Empty;
            _validator.Setup(x => x.Validate(It.IsAny<string[]>(), out error)).Returns(true);

            var filePath = Path.Combine(_fileDirectory, "correct.csv");
            using (var stream = File.OpenRead(filePath))
            {
                var result = _reader.Read(stream);
                Assert.AreEqual(2, result.Count);
            }
        }
    }
}
