using Assignment2C2P.Domain;
using System;
using System.Collections.Generic;

namespace Assignment2C2P.DataAccess.Repository.Interface
{
    public interface ITransactionRepository
    {
        void BulkInsert(IEnumerable<TransactionItem> items);
        IEnumerable<TransactionItem> SearchTransactions(
            string currencyCode, string statusCode, DateTime? dateFrom, DateTime? dateTo);
    }
}
