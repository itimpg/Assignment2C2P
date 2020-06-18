namespace Assignment2C2P.Business.Reader.Interface
{
    public interface ITransactionReaderFactory
    {
        ITransactionReader GetTransactionReader(string extension);
    }
}
