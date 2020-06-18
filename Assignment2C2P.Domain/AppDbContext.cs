using Microsoft.EntityFrameworkCore;

namespace Assignment2C2P.Domain
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<TransactionItem> Transactions { get; set; }
    }
}
