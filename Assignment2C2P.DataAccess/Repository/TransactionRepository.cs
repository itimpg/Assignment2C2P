using Assignment2C2P.DataAccess.Repository.Interface;
using Assignment2C2P.Domain;
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

        public IList<TransactionItem> GetTransactions()
        {
            return _context.Transactions.ToList();
        }
    }
}
