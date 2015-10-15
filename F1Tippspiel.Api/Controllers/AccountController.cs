using F1Tippspiel.Db;
using F1Tippspiel.Db.Models.Account;
using F1Tippspiel.Db.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace F1Tippspiel.Api.Controllers
{
	[RoutePrefix("api/account")]
	public class AccountController : ApiController
	{
		private AuthRepository _repo = null;

		public AccountController()
		{
			_repo = new AuthRepository();
		}

		[AllowAnonymous]
		[Route("register")]
		public async Task<IHttpActionResult> Register(RegisterUser userModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			IdentityResult result = await _repo.RegisterUser(userModel);
			IHttpActionResult errorResult = GetErrorResult(result);

			if(errorResult != null)
			{
				return errorResult;
			}

			return Ok();
		}

		[AllowAnonymous]
		[Route("resetpassword")]
		public async Task<IHttpActionResult> ResetPassword(ResetPassword resetPasswordModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			IdentityUser user = await _repo.FindUser(resetPasswordModel.Email);
			if(user != null)
			{
				string newPass = await _repo.ResetPassword(user);
				if (newPass != string.Empty)
				{
					//TODO: Send new password to the user
					Console.WriteLine("NEW PASSWORD: " + newPass);
					return Ok();
				}
			}

			//when we come so far there's something wrong
			return BadRequest();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_repo.Dispose();
			}
			base.Dispose(disposing);
		}

		private IHttpActionResult GetErrorResult(IdentityResult result)
		{
			if(result == null)
			{
				return InternalServerError();
			}

			if (!result.Succeeded)
			{
				if(result.Errors != null)
				{
					foreach(string error in result.Errors)
					{
						ModelState.AddModelError("", error);
					}
				}
				if (ModelState.IsValid)
				{
					//No errors available, so just send empty BadRequest
					return BadRequest();
				}
				return BadRequest(ModelState);
			}
			return null;
		}
	}
}
