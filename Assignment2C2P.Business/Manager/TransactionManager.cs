using Assignment2C2P.Business.Manager.Interface;
using Assignment2C2P.DataAccess.Repository.Interface;
using Assignment2C2P.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment2C2P.Business.Manager
{
    public class TransactionManager : ITransactionManager
    {
        private ITransactionRepository _repository;

        public TransactionManager(ITransactionRepository repository)
        {
            _repository = repository;
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
