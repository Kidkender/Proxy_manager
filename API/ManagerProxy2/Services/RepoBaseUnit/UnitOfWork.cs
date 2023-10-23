using ManagerProxy2.Models.Context;
using ManagerProxy2.Services.Repositories;

namespace ManagerProxy2.Services.RepoBaseUnit
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataDbContext _context;
        public UnitOfWork(DataDbContext context)
        {
            _context = context;

            Users = new UserService(_context);
            Wallets = new WalletService(_context);
            Admins = new AdminService(_context);
        }

        public IUser Users { get; private set; }
		public IWallet Wallets { get; private set; }
		public IAdmin Admins { get; private set; }
		public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async System.Threading.Tasks.Task DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async System.Threading.Tasks.Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
