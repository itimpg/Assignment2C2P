using Assignment2C2P.DataAccess.Repository.Interface;
using Assignment2C2P.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment2C2P.DataAccess.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private AppDbContext _context;

        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public void BulkInsert(IEnumerable<TransactionItem> items)
        {
            if (!items.Any())
            {
                return;
            }

            _context.Transactions.AddRange(items);
            _context.SaveChanges();
        }

        public List<string> GetCurrencies()
        {
            return _context.Transactions.Select(x => x.CurrencyCode)
                .Distinct().ToList();
        }

        public IEnumerable<TransactionItem> SearchTransactions(
            string currencyCode, string statusCode, DateTime? dateFrom, DateTime? dateTo)
        {
            var query = _context.Transactions
                .Where(x => (string.IsNullOrEmpty(currencyCode) || x.CurrencyCode == currencyCode)
                    && (string.IsNullOrEmpty(statusCode) || x.Status == statusCode));

            if (dateFrom.HasValue && dateTo.HasValue)
            {
                query = query.Where(x => x.TransactionDate >= dateFrom.Value && x.TransactionDate <= dateTo.Value);
            }

            return query.AsEnumerable();
        }
    }
}
