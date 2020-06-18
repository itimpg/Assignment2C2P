using Assignment2C2P.Business.Manager;
using Assignment2C2P.Business.Manager.Interface;
using Assignment2C2P.Business.Reader.Interface;
using Assignment2C2P.DataAccess.Repository.Interface;
using Assignment2C2P.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;

namespace Assignment2C2P.Business.Tests.Manager
{
    [TestClass]
    public class TransactionManagerTests
    {
        private ITransactionManager _manager;
        private Mock<ITransactionRepository> _repository;
        private Mock<ITransactionReaderFactory> _readerFactory;

        public TransactionManagerTests()
        {
            _repository = new Mock<ITransactionRepository>();
            _readerFactory = new Mock<ITransactionReaderFactory>();

            _manager = new TransactionManager(_repository.Object, _readerFactory.Object);
        }

        [TestMethod]
        public void SearchTransactions_Should_ReturnTransactionViewModel()
        {
            _repository
                .Setup(x => x.SearchTransactions(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>()))
                .Returns(new List<TransactionItem>
                {
                    new TransactionItem
                    {
                        TransactionId = "TRAN001",
                        Amount = 200,
                        CurrencyCode = "USD",
                        TransactionDate = new DateTime(2020, 1, 1),
                        Status = "A"
                    }
                });

            var result = _manager.SearchTransactions(null, null, null, null);

            Assert.AreEqual(1, result.Count);
            var resultItem = result[0];

            Assert.AreEqual("TRAN001", resultItem.Id);
            Assert.AreEqual("200.00 USD", resultItem.Payment);
            Assert.AreEqual("A", resultItem.Status);
        }

        [TestMethod]
        public void ImportTransactions_Should_ReadDataAndInsertTransactions()
        {
            _readerFactory
                .Setup(x => x.GetTransactionReader(It.IsAny<string>()))
                .Returns(() =>
                {
                    var reader = new Mock<ITransactionReader>();
                    reader.Setup(x => x.Read(It.IsAny<Stream>())).Returns(new List<TransactionItem>());
                    return reader.Object;
                });

            Stream stream = null;
            string extension = string.Empty;

            _manager.ImportTransactions(stream, extension);

            _repository.Verify(x => x.BulkInsert(It.IsAny<IEnumerable<TransactionItem>>()), Times.Once);
        }
    }
}
