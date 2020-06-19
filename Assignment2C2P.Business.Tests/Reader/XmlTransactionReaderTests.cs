using Assignment2C2P.Business.Model.Xml;
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
    public class XmlTransactionReaderTests
    {
        private IXmlTransactionReader _reader;
        private Mock<IXmlTransactionValidator> _validator;
        private string _fileDirectory;

        public XmlTransactionReaderTests()
        {
            _validator = new Mock<IXmlTransactionValidator>();
            _reader = new XmlTransactionReader(_validator.Object);
            _fileDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reader", "Files", "Xml");
        }

        [TestMethod]
        public void Read_Should_ThrowUnKnowFormatException_When_AnyRecordIsNotEqual5Fields()
        {
            var filePath = Path.Combine(_fileDirectory, "incorrect.xml");
            using (var stream = File.OpenRead(filePath))
            {
                Assert.ThrowsException<UnKnowFormatException>(() => _reader.Read(stream));
            }
        }

        [TestMethod]
        public void Read_Should_ThrowTransactionValidateErrorException_When_TransactionIsEmpty()
        {
            var filePath = Path.Combine(_fileDirectory, "empty.xml");
            using (var stream = File.OpenRead(filePath))
            {
                var exception = Assert.ThrowsException<TransactionValidateErrorException>(() => _reader.Read(stream));
                Assert.AreEqual("Transaction is empty", exception.Message);
            }
        }

        [TestMethod]
        public void Read_Should_ThrowTransactionValidateErrorException_When_ThereAreAnyErrorFromValidation()
        {
            var error = "error";
            _validator.SetupSequence(x => x.Validate(It.IsAny<TransactionsTransaction>(), out error))
                .Returns(true)
                .Returns(false);

            var filePath = Path.Combine(_fileDirectory, "correct.xml");
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
            _validator.Setup(x => x.Validate(It.IsAny<TransactionsTransaction>(), out error)).Returns(true);

            var filePath = Path.Combine(_fileDirectory, "correct.xml");
            using (var stream = File.OpenRead(filePath))
            {
                var result = _reader.Read(stream);
                Assert.AreEqual(2, result.Count);
            }
        }
    }
}
