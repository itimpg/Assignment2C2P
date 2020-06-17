using Microsoft.EntityFrameworkCore;

namespace Assignment2C2P.Domain
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<TransactionItem> Transactions { get; set; }
    }
}
