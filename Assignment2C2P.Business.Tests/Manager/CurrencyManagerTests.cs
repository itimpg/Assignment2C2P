using Assignment2C2P.Business.Manager;
using Assignment2C2P.Business.Manager.Interface;
using Assignment2C2P.DataAccess.Repository.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Assignment2C2P.Business.Tests.Manager
{
    [TestClass]
    public class CurrencyManagerTests
    {
        private ICurrencyManager _manager;
        private Mock<ITransactionRepository> _repository;

        public CurrencyManagerTests()
        {
            _repository = new Mock<ITransactionRepository>();

            _manager = new CurrencyManager(_repository.Object);
        }

        [TestMethod]
        public void GetCurrencies_Should_ReturnDataFromRepository()
        {
            _repository.Setup(x => x.GetCurrencies())
                .Returns(new List<string> { "USD", "THB", "JPY" });

            var result = _manager.GetCurrencies();

            Assert.AreEqual(3, result.Count);
        }
    }
}
