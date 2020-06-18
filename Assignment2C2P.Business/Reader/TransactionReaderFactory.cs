using Assignment2C2P.Business.Reader.Interface;

namespace Assignment2C2P.Business.Reader
{
    public class TransactionReaderFactory : ITransactionReaderFactory
    {
        private readonly IXmlTransactionReader _xmlReader;
        private readonly ICsvTransactionReader _csvReader;

        public TransactionReaderFactory(
            IXmlTransactionReader xmlReader,
            ICsvTransactionReader csvReader)
        {
            _xmlReader = xmlReader;
            _csvReader = csvReader;
        }

        public ITransactionReader GetTransactionReader(string extension)
        {
            switch (extension)
            {
                case ".xml": return _xmlReader;
                case ".csv": return _csvReader;
            }

            return null;
        }
    }
}
