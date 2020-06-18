using Assignment2C2P.Shared;
using System;
using System.Collections.Generic;
using System.IO;

namespace Assignment2C2P.Business.Manager.Interface
{
    public interface ITransactionManager
    {
        IList<TransactionViewModel> SearchTransactions(
            string currencyCode, string statusCode, DateTime? dateFrom, DateTime? dateTo);
        void ImportTransactions(Stream stream, string extension);
    }
}
