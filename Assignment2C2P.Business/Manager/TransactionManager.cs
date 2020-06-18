using Assignment2C2P.Business.Manager.Interface;
using Assignment2C2P.Business.Reader.Interface;
using Assignment2C2P.DataAccess.Repository.Interface;
using Assignment2C2P.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assignment2C2P.Business.Manager
{
    public class TransactionManager : ITransactionManager
    {
        private ITransactionRepository _repository;
        private ITransactionReaderFactory _readerFactory;

        public TransactionManager(
            ITransactionRepository repository,
            ITransactionReaderFactory readerFactory)
        {
            _repository = repository;
            _readerFactory = readerFactory;
        }

        public void ImportTransactions(Stream stream, string extension)
        {
            var reader = _readerFactory.GetTransactionReader(extension);
            var items = reader.Read(stream);
            _repository.BulkInsert(items);
        }

        public IList<TransactionViewModel> SearchTransactions(
            string currencyCode, string statusCode, DateTime? dateFrom, DateTime? dateTo)
        {
            var transactions = _repository.SearchTransactions(currencyCode, statusCode, dateFrom, dateTo);
            return transactions.Select(t => new TransactionViewModel
            {
                Id = t.TransactionId,
                Payment = $"{t.Amount.ToString(".00")} {t.CurrencyCode}",
                Status = t.Status
            }).ToList();
        }
    }
}
