using iForecast.Entities;
using Microsoft.EntityFrameworkCore;

namespace iForecast.Services.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<SubAccount> SubAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
