using Assignment2C2P.Business.Reader;
using Assignment2C2P.Business.Reader.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Assignment2C2P.Business.Tests.Reader
{
    [TestClass]
    public class TransactionReaderFactoryTests
    {
        private ITransactionReaderFactory _factory;

        public TransactionReaderFactoryTests()
        {
            var xmlReader = new Mock<IXmlTransactionReader>();
            var csvReader = new Mock<ICsvTransactionReader>();
            _factory = new TransactionReaderFactory(xmlReader.Object, csvReader.Object);
        }

        [TestMethod]
        public void GetTransactionReader_Should_ReturnXmlTransactionReader_When_ExtensionIsXml()
        {
            var reader = _factory.GetTransactionReader(".xml");
            Assert.IsTrue(reader is IXmlTransactionReader);
        }

        [TestMethod]
        public void GetTransactionReader_Should_ReturnXmlTransactionReader_When_ExtensionIsCsv()
        {
            var reader = _factory.GetTransactionReader(".csv");
            Assert.IsTrue(reader is ICsvTransactionReader);
        }

        [TestMethod]
        public void GetTransactionReader_Should_ReturnXmlTransactionReader_When_ExtensionIsNotXmlOrCsv()
        {
            var reader = _factory.GetTransactionReader(".txt");
            Assert.IsNull(reader);
        }
    }
}
