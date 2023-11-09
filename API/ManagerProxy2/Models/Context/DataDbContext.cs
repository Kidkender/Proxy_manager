using ManagerProxy2.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace ManagerProxy2.Models.Context
{
	public class DataDbContext : DbContext
    {
        public DataDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> users { get; set; }
		public DbSet<Admin> admins { get; set; }
		public DbSet<Wallet> wallets { get; set; }
        public DbSet<WalletHistoryDeposit> deposits { get; set;}
        public DbSet<WalletHistoryTransaction> transactions { get; set; }
        
    }
}
