using ManagerProxy2.Models.Context;
using ManagerProxy2.Models.Entity;
using ManagerProxy2.Models.ModelViews;
using ManagerProxy2.Services.Common;
using ManagerProxy2.Services.RepoBaseUnit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManagerProxy2.Services.Repositories
{
    public class UserService: BaseRepository<User>, IUser
    {
        protected DataDbContext context;

        public UserService(DataDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<JsonResult> GetByUserName(string username)
        {
            var result = from u in context.users
						 join w in context.wallets on u.Id equals w.UserId
						 where u.Role == "user" && u.Username == username
						 select new UserView()
						 {
							 Role = u.Role,
							 Email = u.Email ?? "",
							 Id = u.Id,
							 Name = u.Name ?? "",
							 Password = u.Password ?? "",
							 PhotoFileName = u.PhotoFileName ?? "",
							 WalletId = w.Id,
							 TotalMoney = w.TotalMoney,
							 CreatedAt = u.CreatedAt,
							 UpdatedAt = u.UpdatedAt,
						 };
            if (result != null)
            {
                return new JsonResult(result);
            }
            else
                return null;
        }

        public async Task<JsonResult> GetByUserId(int id)
        {
            var result = (from u in context.users
						 join w in context.wallets on u.Id equals w.UserId
						 where u.Role == "user" && u.Id == id
						 select new UserView()
						 {
							 Role = u.Role,
							 Email = u.Email ?? "",
							 Id = u.Id,
							 Name = u.Name ?? "",
							 Password = u.Password ?? "",
							 PhotoFileName = u.PhotoFileName ?? "",
							 WalletId = w.Id,
							 TotalMoney = w.TotalMoney,
							 CreatedAt = u.CreatedAt,
							 UpdatedAt = u.UpdatedAt,
						 }).SingleOrDefault();
            if (result != null)
            {
                return new JsonResult(result);
            }
            else
                return null;
        }

        public async Task<JsonResult> getAllUser()
        {
            var list = (from u in context.users
                       join w in context.wallets on u.Id equals w.UserId
                       where u.Role == "user"
                       select new UserView ()
                       {
                           Role = u.Role,
                           Email = u.Email ?? "",
                           Id = u.Id,
                           Name = u.Name ?? "",
                           Password = u.Password ?? "",
                           PhotoFileName = u.PhotoFileName ?? "",
                           WalletId = w.Id,
                           TotalMoney = w.TotalMoney,
                           CreatedAt = u.CreatedAt,
                           UpdatedAt = u.UpdatedAt,
                       }).Distinct().OrderByDescending(t=>t.UpdatedAt).ToList();
                       
            return new JsonResult(list);
        }

        public async Task<JsonResult> getAllTableUser(PageRequest req)
        {
            var userList = await context.users.Where(t => t.Role == "user").ToListAsync();

            if (!string.IsNullOrEmpty(req.keyword))
            {
                var filter = req.keyword.ToLower().Trim();
                userList = userList.Where(t => t.Username.Trim().ToLower().Contains(filter)
                                                || t.Email.Trim().ToLower().Contains(filter))
                                                .ToList();
            }

            int totalRow = userList.Count();

            var data = userList.Skip((req.PageIndex - 1) * req.PageSize)
                .Take(req.PageSize).ToList();

            var result = new PagedResult<User>()
            {
                TotalRecords = totalRow,
                PageIndex = req.PageIndex,
                PageSize = req.PageSize,
                Items = data
            };

            return new JsonResult(result);
        }

        public bool CheckExistUsername(string username)
        {
            var check = context.users.Where(t=>t.Username == username).FirstOrDefault();
            if(check != null)
                return true;
            else
            return false;
        }

		public async Task<User> GetUserName(string username)
		{
			var check = await context.users.Where(t => t.Username == username).SingleOrDefaultAsync();
			if (check != null)
				return check;
			else
				return null;
		}
	}

    public interface IUser : IBaseRepository<User>
    {
        Task<JsonResult> getAllUser();
        Task<JsonResult> getAllTableUser(PageRequest req);
        Task<JsonResult> GetByUserName(string username);
        Task<JsonResult> GetByUserId(int id);
		Task<User> GetUserName(string username);
		bool CheckExistUsername(string username);
    }    
}
