﻿using ManagerProxy2.Models.Entity;
using ManagerProxy2.Models.ModelViews;
using ManagerProxy2.Services.RepoBaseUnit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ManagerProxy2.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class AdminsController : ControllerBase
    {
        private readonly IUnitOfWork _unit;
        public AdminsController(IUnitOfWork unit) { 
            _unit = unit;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetByUserId(int id)
        {
            try
            {
                var tasks = await _unit.Admins.GetByUserId(id);
                if(tasks == null)
                    return BadRequest("User not exist!!");
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex);
            }
        }

        [HttpGet]
        [Route("{username}")]
        public async Task<ActionResult> GetByUserName(string username)
        {
            try
            {
                var tasks = await _unit.Admins.GetByUserName(username);
				if (tasks == null)
					return BadRequest("User not exist!!");
				return Ok(tasks);
			}
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var tasks = await _unit.Admins.getAllUser();
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex);
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("{id}")]
        public async Task<ActionResult> Update(UserModel user ,int id)
        {
			try
			{
				var tasks = _unit.Admins.Get(id);
				if (tasks == null)
					return BadRequest("User not exist!!");

                var u = _unit.Users.Get(tasks.UserId);
                if (u == null)
					return BadRequest("User not exist!!");

                u.UpdatedAt = DateTime.Now;
                u.Name = user.Name;
                u.Email = user.Email;
                u.PhotoFileName = user.PhotoFileName;

                _unit.Users.Update(u);
                _unit.Complete();

				return Ok(u);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Internal server error: " + ex);
			}
		}


		[HttpPost]
		[Authorize(Roles = "admin")]
		[Route("{id}")]
		public async Task<ActionResult> Destroy(int id)
		{
			try
			{
                var model = _unit.Admins.Get(id);
				if (model == null)
					return BadRequest("User not exist");

				var tasks = _unit.Users.Get(model.UserId);
				if (tasks == null)
					return BadRequest("User not exist");

                _unit.Admins.Remove(model);
                _unit.Complete();

                var wallet = _unit.Wallets.GetWalletByUserId(model.UserId);
                _unit.Wallets.Remove(wallet);
                _unit.Complete();

				_unit.Users.Remove(tasks);
				_unit.Complete();

				return Ok(tasks);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Internal server error: " + ex);
			}
		}

		[HttpPost]
        public async Task<ActionResult> Register(User user)
        {
            try
            {
                var check = _unit.Users.CheckExistUsername(user.Username);
				if (check)
                {
                    return BadRequest("Username existed");
                }
                else
                {
                    var u = new User
                    {
                        Id = 0,
                        Name = user.Name,   
                        Username = user.Username,
                        Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
                        PhotoFileName = user.PhotoFileName,
                        Email = user.Email,
                        Role = "admin",
                        UpdatedAt = DateTime.Now,
                        CreatedAt = DateTime.Now,
                    };
                    _unit.Users.Add(u);
                    _unit.Complete();

                    Wallet wallet = new Wallet();
                    wallet.TotalMoney = 0;
                    wallet.UserId = u.Id;
                    wallet.Id = 0;
                    _unit.Wallets.Add(wallet);
					_unit.Complete();

                    Admin admin = new Admin()
                    {
                        Id = 0,
                        Password = u.Password,
                        UserId = u.Id,
                        Username = u.Username,
                    };

					_unit.Admins.Add(admin);
					_unit.Complete();

					return Ok( new JsonResult( 
                        new { admin, user = u, wallet })
                    );
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(SignInModel s)
        {
            try
            {
                if (s.Username != null)
                {
                    var u = await _unit.Users.GetUserName(s.Username);
                    
                    string Token;
                    if(u.Role != "admin")
                    {
						return BadRequest("User not permisson");
					}
                    if (u == null)
                    {
                        return BadRequest("User not Found or Wrong Password.");
                    }
                    else
                    {
                        bool bc = BCrypt.Net.BCrypt.Verify(s.Password, u.Password);
                        if (bc == false)
                        {
                            return BadRequest("User not Found or Wrong Password.");
                        }
                        Token = CreateToken(u);
                    }

                    return Ok(new JsonResult(Token));
                }
                else return BadRequest("Username is valid");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex);
            }

        }

        private string CreateToken(User u)
        {
            if (u != null)
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, u.Username),
                    new Claim(ClaimTypes.Role, u.Role),
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("dIwIV4lWGYkPlk0hcUuu9rYihYa74NHNxm0xruJmAaT.MrgySyq"));
                var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
                var token = new JwtSecurityToken(
                    "API",
                    "UI",
                    claims: claims,
                    expires: DateTime.Now.AddMonths(1),
                    signingCredentials: cred);
                var jwt = new JwtSecurityTokenHandler().WriteToken(token);
                return jwt;
            }
            else
                return "";
        }
    }
}
