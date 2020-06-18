using Assignment2C2P.DataAccess.Repository;
using Assignment2C2P.DataAccess.Repository.Interface;
using Assignment2C2P.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment2C2P.DataAccess.Tests.Repository
{
    [TestClass]
    public class TransactionRepositoryTests
    {
        private ITransactionRepository _repository;
        private Mock<AppDbContext> _context;
        private Mock<DbSet<TransactionItem>> _transactionSet;

        public TransactionRepositoryTests()
        {
            _transactionSet = new Mock<DbSet<TransactionItem>>();
            var data = new List<TransactionItem>
            {
                new TransactionItem
                {
                    TransactionId = "Tran1",
                    CurrencyCode = "USD",
                    Status = "A" ,
                    TransactionDate = new DateTime(2020, 1, 1)
                },
                new TransactionItem
                {
                    TransactionId = "Tran2",
                    CurrencyCode = "THB",
                    Status = "R" ,
                    TransactionDate = new DateTime(2020, 2, 1)
                },
                new TransactionItem
                {
                    TransactionId = "Tran3",
                    CurrencyCode = "USD",
                    Status = "D" ,
                    TransactionDate = new DateTime(2020, 3, 1)
                },
                new TransactionItem
                {
                    TransactionId = "Tran4",
                    CurrencyCode = "CAD",
                    Status = "R" ,
                    TransactionDate = new DateTime(2020, 4, 1)
                },
            }.AsQueryable();
            _transactionSet.As<IQueryable<TransactionItem>>().Setup(m => m.Provider).Returns(data.Provider);
            _transactionSet.As<IQueryable<TransactionItem>>().Setup(m => m.Expression).Returns(data.Expression);
            _transactionSet.As<IQueryable<TransactionItem>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _transactionSet.As<IQueryable<TransactionItem>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _context = new Mock<AppDbContext>();
            _context.SetupGet(x => x.Transactions).Returns(_transactionSet.Object);

            _repository = new TransactionRepository(_context.Object);
        }

        [TestMethod]
        public void BulkInsert_Should_InsertAllDataIntoTransactionAndSaveChange_WhenTransactionIsMoreThanZero()
        {
            var transactions = new List<TransactionItem>
            {
                new TransactionItem()
            };

            _repository.BulkInsert(transactions);
            _transactionSet.Verify(x => x.AddRange(It.IsAny<IEnumerable<TransactionItem>>()), Times.Once);
            _context.Verify(x => x.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void BulkInsert_Should_DoNothing_WhenTransactionIsEmpty()
        {
            var transactions = new List<TransactionItem>();

            _repository.BulkInsert(transactions);
            _transactionSet.Verify(x => x.AddRange(It.IsAny<IEnumerable<TransactionItem>>()), Times.Never);
            _context.Verify(x => x.SaveChanges(), Times.Never);
        }

        [TestMethod]
        public void SearchTransactions_Should_GetDataByCurrencyCode_When_CurrencyCodeIsProvided()
        {
            string currencyCode = "CAD";
            string status = string.Empty;
            DateTime? from = null;
            DateTime? to = null;

            var result = _repository.SearchTransactions(currencyCode, status, from, to).ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Tran4", result[0].TransactionId);
        }

        [TestMethod]
        public void SearchTransactions_Should_GetDataByTransactionDate_When_DateFromAndDateToAreProvided()
        {
            string currencyCode = string.Empty;
            string status = string.Empty;
            DateTime? from = new DateTime(2020, 2, 1);
            DateTime? to = new DateTime(2020, 3, 1);

            var result = _repository.SearchTransactions(currencyCode, status, from, to).ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            var expectedIds = new[] { "Tran2", "Tran3" };
            Assert.IsTrue(expectedIds.Contains(result[0].TransactionId));
            Assert.IsTrue(expectedIds.Contains(result[1].TransactionId));
        }

        [TestMethod]
        public void SearchTransactions_Should_GetDataByStatus_When_StatusIsProvided()
        {
            string currencyCode = string.Empty;
            string status = "A";
            DateTime? from = null;
            DateTime? to = null;

            var result = _repository.SearchTransactions(currencyCode, status, from, to).ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Tran1", result[0].TransactionId);
        }

        [TestMethod]
        public void GetCurrencies_Should_ReturnDistinctCurrencyCodeFromTransactions()
        {
            var result = _repository.GetCurrencies();

            Assert.AreEqual(3, result.Count);
            var expectedResults = new[] { "USD", "THB", "CAD" };
            Assert.IsTrue(expectedResults.Contains(result[0]));
            Assert.IsTrue(expectedResults.Contains(result[1]));
            Assert.IsTrue(expectedResults.Contains(result[2]));
        }
    }
}
