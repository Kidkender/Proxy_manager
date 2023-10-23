using ManagerProxy2.Models.Context;
using ManagerProxy2.Models.Entity;
using ManagerProxy2.Models.ModelViews;
using ManagerProxy2.Services.RepoBaseUnit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManagerProxy2.Services.Repositories
{
	public class WalletService : BaseRepository<Wallet>, IWallet
	{
		protected DataDbContext context;
		public WalletService(DataDbContext _context) : base(_context)
		{
			context = _context;
		}

		//public Task<JsonResult> Deposit(int id)
		//{
			//var result = (from d in context.deposits
			//			  join w in context.wallets on d.WalletId equals w.Id
			//			  join u in context.users on w.UserId equals u.Id
			//			  where d.Id == id
			//			  select new UserView()
			//			  {
			//				  Role = u.Role,
			//				  Email = u.Email ?? "",
			//				  Id = u.Id,
			//				  Name = u.Name ?? "",
			//				  Password = u.Password ?? "",
			//				  PhotoFileName = u.PhotoFileName ?? "",
			//				  WalletId = w.Id,
			//				  TotalMoney = w.TotalMoney,
			//				  CreatedAt = u.CreatedAt,
			//				  UpdatedAt = u.UpdatedAt,
			//			  }).SingleOrDefault();
			//if (result != null)
			//{
			//	return new JsonResult(result);
			//}
			//else
			//	return null;
		//}

		public async Task<Wallet> GetByUserId(int id)
		{
			var model = await context.wallets.Where(t=>t.UserId == id).SingleOrDefaultAsync();
			if (model == null)
				return null;
			return model;
		}
	}

	public interface IWallet : IBaseRepository<Wallet>
	{
		Task<Wallet> GetByUserId(int id);
	}
}
