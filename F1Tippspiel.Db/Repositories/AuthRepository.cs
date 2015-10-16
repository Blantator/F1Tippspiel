using F1Tippspiel.Db.Authentication;
using F1Tippspiel.Db.Data;
using F1Tippspiel.Db.Game;
using F1Tippspiel.Db.Models.Account;
using F1Tippspiel.Db.Rewards;
using F1Tippspiel.Db.Tools;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1Tippspiel.Db.Repositories
{
	public class AuthRepository : IDisposable
	{
		private AppContext _ctx;

		private UserManager<UserAccount> _userManager;

		public AuthRepository()
		{
			_ctx = new AppContext();
			_userManager = new UserManager<UserAccount>(new UserStore<UserAccount>(_ctx));
		}

		public AuthRepository(AppContext context)
		{
			_ctx = context;
			_userManager = new UserManager<UserAccount>(new UserStore<UserAccount>(_ctx));
		}

		public async Task<IdentityResult> RegisterUser(RegisterUser userModel)
		{
			UserAccount user = new UserAccount
			{
				UserName = userModel.Email,
				Email = userModel.Email,
				EmailConfirmed = true,
				TwoFactorEnabled = false,
				Admin = false,
				Enabled = true,
				DisplayName = userModel.Displayname,
				LastSeen = DateTime.Now,
				Registered = DateTime.Now,
				Picture = "/Content/static/img/unknown_user.png",
				Achievements = new LinkedList<Achievement>(),
				RaceBets = new LinkedList<RaceBet>(),
				Badges = new LinkedList<Badge>()
			};

			var result = await RegisterUser(user, userModel.Password);

			return result;
		}

		public async Task<IdentityResult> RegisterUser(UserAccount userModel, string password)
		{
			var result = await _userManager.CreateAsync(userModel, password);

			return result;
		}

		public async Task<IdentityUser> FindUser(string userName, string password)
		{
			IdentityUser user = await _userManager.FindAsync(userName, password);

			return user;
		}

		public async Task<IdentityUser> FindUser(string emailAddress)
		{
			IdentityUser user = await _userManager.FindByEmailAsync(emailAddress);

			return user;
		}

		public UserAccount FindUserAccount(string emailAddress)
		{
			return _ctx.Users.FirstOrDefault(u => u.Email == emailAddress);
		}

		public async Task<string> ResetPassword(IdentityUser user)
		{
			string newPass = Generator.GenerateNumberString(9);
			string token = "";
			try
			{
				_userManager.UserTokenProvider = new DataProtectorTokenProvider<UserAccount>(new DpapiDataProtectionProvider("F1Tippspiel").Create("PasswordReset"));
				token = await _userManager.GeneratePasswordResetTokenAsync(user.Id);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			IdentityResult result = await _userManager.ResetPasswordAsync(user.Id, token, newPass);
			if (result.Succeeded)
			{
				return newPass;
			}
			else
			{
				return string.Empty;
			}
		}

		public Client FindClient(string clientId)
		{
			var client = _ctx.Clients.Find(clientId);

			return client;
		}

		public async Task<bool> AddRefreshToken(RefreshToken token)
		{

			var existingToken = _ctx.RefreshTokens.Where(r => r.Subject == token.Subject && r.ClientId == token.ClientId).SingleOrDefault();

			if (existingToken != null)
			{
				var result = await RemoveRefreshToken(existingToken);
			}

			_ctx.RefreshTokens.Add(token);

			return await _ctx.SaveChangesAsync() > 0;
		}

		public async Task<bool> RemoveRefreshToken(string refreshTokenId)
		{
			var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

			if (refreshToken != null)
			{
				_ctx.RefreshTokens.Remove(refreshToken);
				return await _ctx.SaveChangesAsync() > 0;
			}

			return false;
		}

		public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
		{
			_ctx.RefreshTokens.Remove(refreshToken);
			return await _ctx.SaveChangesAsync() > 0;
		}

		public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
		{
			var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

			return refreshToken;
		}

		public List<RefreshToken> GetAllRefreshTokens()
		{
			return _ctx.RefreshTokens.ToList();
		}

		public void Dispose()
		{
			_ctx.Dispose();
			_userManager.Dispose();
		}
	}
}
