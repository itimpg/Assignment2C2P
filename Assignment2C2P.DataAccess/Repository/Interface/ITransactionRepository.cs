using Assignment2C2P.Domain;
using System.Collections.Generic;

namespace Assignment2C2P.DataAccess.Repository.Interface
{
    public interface ITransactionRepository
    {
        void BulkInsert(IEnumerable<TransactionItem> items);
        IList<TransactionItem> GetTransactions();
    }
}
