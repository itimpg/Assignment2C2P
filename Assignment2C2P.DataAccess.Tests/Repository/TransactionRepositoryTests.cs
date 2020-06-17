using Assignment2C2P.DataAccess.Repository;
using Assignment2C2P.DataAccess.Repository.Interface;
using Assignment2C2P.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
                new TransactionItem { TransactionId = "Tran1" },
                new TransactionItem { TransactionId = "Tran2" },
                new TransactionItem { TransactionId = "Tran3" },
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
        public void GetTransactions_Should_ReturnDataFromTransactionDbSet()
        {
            var result = _repository.GetTransactions();

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }
    }
}
