using ManagerProxy.Models.ModelViews;
using ManagerProxy2.Models.Context;
using ManagerProxy2.Models.Entity;
using ManagerProxy2.Models.ModelViews;
using ManagerProxy2.Services.RepoBaseUnit;

namespace ManagerProxy2.Services.Repositories
{
	public class WalletService : BaseRepository<Wallet>, IWallet
	{
		protected DataDbContext context;
		public WalletService(DataDbContext _context) : base(_context)
		{
			context = _context;
		}

		#region Wallet
		public Wallet GetWalletByUserId(int id)
		{
			var model = context.wallets.Where(t=>t.UserId == id).FirstOrDefault();
			return model;
		}
		public WalletView GetWalletByUserName(string username)
		{
			var result = (from w in context.wallets
						  join u in context.users on w.UserId equals u.Id
						  where u.Username == username
						  select new WalletView()
						  {
							  Id = w.Id,
							  UserId = w.UserId,
							  Username = u.Username,
							  Email = u.Email ?? "",
							  TotalMoney = w.TotalMoney,
						  }).SingleOrDefault();
			return result;
		}
		public WalletView GetWalletUserId(int id)
		{
			var result = (from w in context.wallets
						  join u in context.users on w.UserId equals u.Id
						  where u.Id == id
						  select new WalletView()
						  {
							  Id = w.Id,
							  UserId = w.UserId,
							  Username = u.Username,
							  Email = u.Email ?? "",
							  TotalMoney = w.TotalMoney,
						  }).SingleOrDefault();
			return result;
		}
		public WalletView GetWalletById(int id)
		{
			var result = (from w in context.wallets
						  join u in context.users on w.UserId equals u.Id
						  where w.Id == id
						  select new WalletView()
						  {
							  Id = w.Id,
							  UserId = w.UserId,
							  Username = u.Username,
							  Email = u.Email ?? "",
							  TotalMoney = w.TotalMoney,
						  }).SingleOrDefault();
			return result;
		}
		public IEnumerable<WalletView> GetAllWallet()
		{
			var result = (from w in context.wallets
						  join u in context.users on w.UserId equals u.Id
						  select new WalletView()
						  {
							  Id = w.Id,
							  UserId = w.UserId,
							  Username = u.Username,
							  Email = u.Email ?? "",
							  TotalMoney = w.TotalMoney,
						  }).ToList();
			return result;
		}
		public IEnumerable<WalletView> GetAllWalletById(int id)
		{
			var result = (from w in context.wallets
						  join u in context.users on w.UserId equals u.Id
						  where w.Id == id
						  select new WalletView()
						  {
							  Id = w.Id,
							  UserId = w.UserId,
							  Username = u.Username,
							  Email = u.Email ?? "",
							  TotalMoney = w.TotalMoney,
						  }).ToList();
			return result;
		}
		public IEnumerable<WalletView> GetAllWalletByUserId(int id)
		{
			var result = (from w in context.wallets
						  join u in context.users on w.UserId equals u.Id
						  where w.Id == id
						  select new WalletView()
						  {
							  Id = u.Id,
							  UserId = w.UserId,
							  Username = u.Username,
							  Email = u.Email ?? "",
							  TotalMoney = w.TotalMoney,
						  }).ToList();
			return result;
		}
		public IEnumerable<WalletView> GetAllWalletByUserName(string username)
		{
			var result = (from w in context.wallets
						  join u in context.users on w.UserId equals u.Id
						  where u.Username == username
						  select new WalletView()
						  {
							  Id = w.Id,
							  UserId = w.UserId,
							  Username = u.Username,
							  Email = u.Email ?? "",
							  TotalMoney = w.TotalMoney,
						  }).ToList();
			return result;
		}
		#endregion

		#region Deposit
		public void AddDeposit(WalletHistoryDeposit deposit)
		{
			context.deposits.Add(deposit);
		}
		public DepositView GetDepositById(int id)
		{
			var result = (from d in context.deposits
						  join w in context.wallets on d.WalletId equals w.Id
						  join u in context.users on w.UserId equals u.Id
						  where d.Id == id
						  select new DepositView()
						  {
							  Id = d.Id,
							  UserId = w.UserId,
							  Username = u.Username,
							  WalletId = w.Id,
							  Money = d.Money,
							  Email = u.Email ?? "",
							  Name = u.Name ?? "",
							  Created = d.CreatedAt,
							  TotalMoney = w.TotalMoney,
						  }).SingleOrDefault();
			return result;
		}
		public DepositView GetDepositByUserId(int userid)
		{
			var result = (from d in context.deposits
						  join w in context.wallets on d.WalletId equals w.Id
						  join u in context.users on w.UserId equals u.Id
						  where u.Id == userid
						  select new DepositView()
						  {
							  Id = d.Id,
							  UserId = w.UserId,
							  Username = u.Username,
							  WalletId = w.Id,
							  Money = d.Money,
							  Email = u.Email ?? "",
							  Name = u.Name ?? "",
							  Created = d.CreatedAt,
							  TotalMoney = w.TotalMoney,
						  }).SingleOrDefault();
			return result;
		}
		public DepositView GetDepositByUserName(string username)
		{
			var result = (from d in context.deposits
						  join w in context.wallets on d.WalletId equals w.Id
						  join u in context.users on w.UserId equals u.Id
						  where u.Username == username
						  select new DepositView()
						  {
							  Id = d.Id,
							  UserId = w.UserId,
							  Username = u.Username,
							  WalletId = w.Id,
							  Money = d.Money,
							  Email = u.Email ?? "",
							  Name = u.Name ?? "",
							  Created = d.CreatedAt,
							  TotalMoney = w.TotalMoney,
						  }).SingleOrDefault();
			return result;
		}
		public IEnumerable<DepositView> GetAllDeposit()
		{
			var result = (from d in context.deposits
						  join w in context.wallets on d.WalletId equals w.Id
						  join u in context.users on w.UserId equals u.Id
						  select new DepositView()
						  {
							  Id = d.Id,
							  UserId = w.UserId,
							  Username = u.Username,
							  WalletId = w.Id,
							  Money = d.Money,
							  Email = u.Email ?? "",
							  Name = u.Name ?? "",
							  Created = d.CreatedAt,
							  TotalMoney = w.TotalMoney,
						  }).ToList();
			return result;
		}
		public IEnumerable<DepositView> GetAllDepositById(int id)
		{
			var result = (from d in context.deposits
						  join w in context.wallets on d.WalletId equals w.Id
						  join u in context.users on w.UserId equals u.Id
						  where d.Id == id
						  select new DepositView()
						  {
							  Id = d.Id,
							  UserId = w.UserId,
							  Username = u.Username,
							  WalletId = w.Id,
							  Money = d.Money,
							  Email = u.Email ?? "",
							  Name = u.Name ?? "",
							  Created = d.CreatedAt,
							  TotalMoney = w.TotalMoney,
						  }).ToList();
			return result;
		}
		public IEnumerable<DepositView> GetAllDepositByUserId(int userId)
		{
			var result = (from d in context.deposits
						  join w in context.wallets on d.WalletId equals w.Id
						  join u in context.users on w.UserId equals u.Id
						  where u.Id == userId
						  select new DepositView()
						  {
							  Id = d.Id,
							  UserId = w.UserId,
							  Username = u.Username,
							  WalletId = w.Id,
							  Money = d.Money,
							  Email = u.Email ?? "",
							  Name = u.Name ?? "",
							  Created = d.CreatedAt,
							  TotalMoney = w.TotalMoney,
						  }).ToList();
			return result;
		}
		public IEnumerable<DepositView> GetAllDepositByUserName(string username)
		{
			var result = (from d in context.deposits
						  join w in context.wallets on d.WalletId equals w.Id
						  join u in context.users on w.UserId equals u.Id
						  where u.Username == username
						  select new DepositView()
						  {
							  Id = d.Id,
							  UserId = w.UserId,
							  Username = u.Username,
							  WalletId = w.Id,
							  Money = d.Money,
							  Email = u.Email ?? "",
							  Name = u.Name ?? "",
							  Created = d.CreatedAt,
							  TotalMoney = w.TotalMoney,
						  }).ToList();
			return result;
		}
		#endregion

		#region Transaction
		public void AddTransaction(WalletHistoryTransaction transaction)
		{
			context.transactions.Add(transaction);
		}
		public TransactionView GetTrasactionById(int id)
		{
			var result = (from t in context.transactions
						  join u1 in context.users on t.UserFromId equals u1.Id
						  join u2 in context.users on t.UserToId equals u2.Id
						  where t.Id == id
						  select new TransactionView()
						  {
							  Id = t.Id,
							  UserFromId = u1.Id,
							  UserToId = u2.Id,
							  EmailFrom = u1.Email ?? "",
							  EmailTo = u2.Email ?? "",
							  Money = t.Money,
							  NameFrom = u1.Name ?? "",
							  NameTo = u2.Name ?? "",
							  UsernameFrom = u1.Username ?? "",
							  UsernameTo = u2.Username ?? "",
							  Created = t.CreatedAt
						  }).SingleOrDefault();
			return result;
		}
		public IEnumerable<TransactionView> GetAllTransaction()
		{
			var result = (from t in context.transactions
						  join u1 in context.users on t.UserFromId equals u1.Id
						  join u2 in context.users on t.UserToId equals u2.Id
						  select new TransactionView()
						  {
							  Id = t.Id,
							  UserFromId = u1.Id,
							  UserToId = u2.Id,
							  EmailFrom = u1.Email ?? "",
							  EmailTo = u2.Email ?? "",
							  Money = t.Money,
							  NameFrom = u1.Name ?? "",
							  NameTo = u2.Name ?? "",
							  UsernameFrom = u1.Username ?? "",
							  UsernameTo = u2.Username ?? "",
							  Created = t.CreatedAt
						  }).ToList();
			return result;
		}
		public IEnumerable<TransactionView> GetAllTransactionByUserFromId(int id)
		{
			var result = (from t in context.transactions
						  join u1 in context.users on t.UserFromId equals u1.Id
						  join u2 in context.users on t.UserToId equals u2.Id
						  where u1.Id == id
						  select new TransactionView()
						  {
							  Id = t.Id,
							  UserFromId = u1.Id,
							  UserToId = u2.Id,
							  EmailFrom = u1.Email ?? "",
							  EmailTo = u2.Email ?? "",
							  Money = t.Money,
							  NameFrom = u1.Name ?? "",
							  NameTo = u2.Name ?? "",
							  UsernameFrom = u1.Username ?? "",
							  UsernameTo = u2.Username ?? "",
							  Created = t.CreatedAt
						  }).ToList();
			return result;
		}
		public IEnumerable<TransactionView> GetAllTransactionByUserToId(int id)
		{
			var result = (from t in context.transactions
						  join u1 in context.users on t.UserFromId equals u1.Id
						  join u2 in context.users on t.UserToId equals u2.Id
						  where u2.Id == id
						  select new TransactionView()
						  {
							  Id = t.Id,
							  UserFromId = u1.Id,
							  UserToId = u2.Id,
							  EmailFrom = u1.Email ?? "",
							  EmailTo = u2.Email ?? "",
							  Money = t.Money,
							  NameFrom = u1.Name ?? "",
							  NameTo = u2.Name ?? "",
							  UsernameFrom = u1.Username ?? "",
							  UsernameTo = u2.Username ?? "",
							  Created = t.CreatedAt
						  }).ToList();
			return result;
		}
		public IEnumerable<TransactionView> GetAllTransactionByUsernameFrom(string username)
		{
			var result = (from t in context.transactions
						  join u1 in context.users on t.UserFromId equals u1.Id
						  join u2 in context.users on t.UserToId equals u2.Id
						  where u1.Username == username
						  select new TransactionView()
						  {
							  Id = t.Id,
							  UserFromId = u1.Id,
							  UserToId = u2.Id,
							  EmailFrom = u1.Email ?? "",
							  EmailTo = u2.Email ?? "",
							  Money = t.Money,
							  NameFrom = u1.Name ?? "",
							  NameTo = u2.Name ?? "",
							  UsernameFrom = u1.Username ?? "",
							  UsernameTo = u2.Username ?? "",
							  Created = t.CreatedAt
						  }).ToList();
			return result;
		}
		public IEnumerable<TransactionView> GetAllTransactionByUsernameTo( string username)
		{
			var result = (from t in context.transactions
						  join u1 in context.users on t.UserFromId equals u1.Id
						  join u2 in context.users on t.UserToId equals u2.Id
						  where u2.Username == username
						  select new TransactionView()
						  {
							  Id = t.Id,
							  UserFromId = u1.Id,
							  UserToId = u2.Id,
							  EmailFrom = u1.Email ?? "",
							  EmailTo = u2.Email ?? "",
							  Money = t.Money,
							  NameFrom = u1.Name ?? "",
							  NameTo = u2.Name ?? "",
							  UsernameFrom = u1.Username ?? "",
							  UsernameTo = u2.Username ?? "",
							  Created = t.CreatedAt
						  }).ToList();
			return result;
		}
		#endregion
	}

	public interface IWallet : IBaseRepository<Wallet>
	{
		void AddDeposit(WalletHistoryDeposit deposit);
		void AddTransaction(WalletHistoryTransaction transaction);
		Wallet GetWalletByUserId(int id);
		WalletView GetWalletByUserName(string username);
		WalletView GetWalletUserId(int id);
		WalletView GetWalletById(int id);
		IEnumerable<WalletView> GetAllWallet();
		IEnumerable<WalletView> GetAllWalletByUserId(int id);
		IEnumerable<WalletView> GetAllWalletByUserName(string username);
		DepositView GetDepositById(int id);
		DepositView GetDepositByUserId(int userid);
		DepositView GetDepositByUserName(string username);
		IEnumerable<DepositView> GetAllDepositByUserId(int userId);
		IEnumerable<DepositView> GetAllDepositByUserName(string username);
		TransactionView GetTrasactionById(int id);
		IEnumerable<TransactionView> GetAllTransaction();
		IEnumerable<TransactionView> GetAllTransactionByUserFromId(int id);
		IEnumerable<TransactionView> GetAllTransactionByUserToId(int id);
		IEnumerable<TransactionView> GetAllTransactionByUsernameFrom(string username);
		IEnumerable<TransactionView> GetAllTransactionByUsernameTo(string username);
	}
}
