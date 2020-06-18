using Assignment2C2P.Domain;
using System.Collections.Generic;
using System.IO;

namespace Assignment2C2P.Business.Reader.Interface
{
    public interface ITransactionReader
    {
        IList<TransactionItem> Read(Stream stream);
    }
}
