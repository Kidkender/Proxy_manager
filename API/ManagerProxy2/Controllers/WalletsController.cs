using ManagerProxy2.Models.Entity;
using ManagerProxy2.Services.RepoBaseUnit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace ManagerProxy.Controllers
{
	[Route("api/[controller]/[Action]")]
	[ApiController]
	public class WalletsController : ControllerBase
	{
		private readonly IUnitOfWork _unit;
		public WalletsController(IUnitOfWork unit) { 
			_unit = unit;
		}

		#region Wallet
		[HttpGet]
		[Route("{id}")]
		public ActionResult GetWalletId(int id)
		{
			try
			{
				var tasks = _unit.Wallets.Get(id);
				return Ok(tasks);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Internal server error: " + ex);
			}
		}

		[HttpGet]
		[Route("{id}")]
		public ActionResult GetWalletByUserId(int id)
		{
			try
			{
				var tasks = _unit.Wallets.GetWalletUserId(id);
				return Ok(tasks);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Internal server error: " + ex);
			}
		}

		[HttpGet]
		[Route("{user}")]
		public ActionResult GetWalletByUserName(string user)
		{
			try
			{
				var tasks = _unit.Wallets.GetWalletByUserName(user);
				return Ok(tasks);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Internal server error: " + ex);
			}
		}

		[HttpGet]
		public ActionResult GetAllWallet()
		{
			try
			{
				var tasks = _unit.Wallets.GetAllWallet();
				return Ok(tasks);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Internal server error: " + ex);
			}
		}
		#endregion

		#region Deposit
		[HttpPost]
		public ActionResult Deposit(WalletHistoryDeposit model)
		{
			try
			{
				if (model == null)
				{
					return BadRequest("Chưa nhập đủ thông tin");
				}
				if (!ModelState.IsValid)
				{
					return BadRequest("Không có dữ liệu này trong Database");
				}
				if (model.Money <= 0)
				{
					return BadRequest("Số tiền nạp phải lớn hơn không");
				}
				var models = new WalletHistoryDeposit
				{
					CreatedAt = DateTime.Now,
					Id = 0,
					Money = model.Money,
					Note = model.Note,
					WalletId = model.WalletId,
				};
				_unit.Wallets.AddDeposit(models);
				_unit.Complete();

				var wallet = _unit.Wallets.Get(model.WalletId);
				wallet.TotalMoney += models.Money;
				_unit.Wallets.Update(wallet);
				_unit.Complete();

				return Ok(models);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Internal server error: " + ex);
			}
		}
		[HttpGet]
		[Route("{id}")]
		public ActionResult GetDepositById(int id)
		{
			try
			{
				var tasks = _unit.Wallets.GetDepositById(id);
				return Ok(tasks);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Internal server error: " + ex);
			}
		}

		[HttpGet]
		[Route("{id}")]
		public ActionResult GetAllDepositByUserId(int id)
		{
			try
			{
				var tasks = _unit.Wallets.GetAllDepositByUserId(id);
				return Ok(tasks);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Internal server error: " + ex);
			}
		}
		[HttpGet]
		[Route("{user}")]
		public ActionResult GetAllDepositByUserName(string user)
		{
			try
			{
				var tasks = _unit.Wallets.GetAllDepositByUserName(user);
				return Ok(tasks);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Internal server error: " + ex);
			}
		}
		#endregion

		#region Transaction

		[HttpPost]
		public ActionResult Transaction(WalletHistoryTransaction model)
		{
			try
			{
				if (model == null)
				{
					return BadRequest("Chưa nhập đủ thông tin");
				}
				if (!ModelState.IsValid)
				{
					return BadRequest("Không có dữ liệu này trong Database");
				}
				if (model.Money <= 0)
				{
					return BadRequest("Số tiền chuyển phải lớn hơn 0");
				}	
				var w1 = _unit.Wallets.GetWalletByUserId(model.UserFromId);
				if (w1 == null)
				{
					return BadRequest("Người chuyển không tồn tại");
				}

				if (w1.TotalMoney < model.Money)
				{
					return BadRequest("Người chuyển không đủ tiền");
				}

				var w2 = _unit.Wallets.GetWalletByUserId(model.UserToId);
				if (w2 == null)
				{
					return BadRequest("Người nhận không tồn tại");
				}

				var models = new WalletHistoryTransaction
				{
					CreatedAt = DateTime.Now,
					Id = 0,
					Money = model.Money,
					Note = model.Note,
					UserFromId = model.UserFromId,
					UserToId = model.UserToId
				};
				_unit.Wallets.AddTransaction(models);
				_unit.Complete();

				w1.TotalMoney -= model.Money;
				_unit.Wallets.Update(w1);
				_unit.Complete();

				w2.TotalMoney += model.Money;	
				_unit.Wallets.Update(w2);
				_unit.Complete();

				return Ok(models);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Internal server error: " + ex);
			}
		}

		[HttpGet]
		[Route("{id}")]
		public ActionResult GetTransactionById(int id)
		{
			try
			{
				var tasks = _unit.Wallets.GetTrasactionById(id);
				return Ok(tasks);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Internal server error: " + ex);
			}
		}
		[HttpGet]
		[Route("{id}")]
		public ActionResult GetAllTransactionByUserIdFrom(int id)
		{
			try
			{
				var tasks = _unit.Wallets.GetAllTransactionByUserFromId(id);
				return Ok(tasks);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Internal server error: " + ex);
			}
		}
		[HttpGet]
		[Route("{id}")]
		public ActionResult GetAllTransactionByUserIdTo(int id)
		{
			try
			{
				var tasks = _unit.Wallets.GetAllTransactionByUserToId(id);
				return Ok(tasks);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Internal server error: " + ex);
			}
		}
		[HttpGet]
		[Route("{user}")]
		public ActionResult GetAllTransactionByUsernameFrom(string user)
		{
			try
			{
				var tasks = _unit.Wallets.GetAllTransactionByUsernameFrom(user);
				return Ok(tasks);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Internal server error: " + ex);
			}
		}
		[HttpGet]
		[Route("{user}")]
		public ActionResult GetAllTransactionByUsernameTo(string user)
		{
			try
			{
				var tasks = _unit.Wallets.GetAllTransactionByUsernameTo(user);
				return Ok(tasks);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Internal server error: " + ex);
			}
		}
		#endregion
	}
}
