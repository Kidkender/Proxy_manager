using ManagerProxy2.Services.Repositories;

namespace ManagerProxy2.Services.RepoBaseUnit
{
    public interface IUnitOfWork : IDisposable
    {
        int Complete();
        Task SaveAsync();
        Task DisposeAsync();

        IUser Users { get; }
		IAdmin Admins { get; }
		IWallet Wallets { get; }
    }
}
