using System.ComponentModel.DataAnnotations;

namespace F1Tippspiel.Db.Models.Account
{
	public class ResetPassword
	{
		[Required]
		public string Email { get; set; }
	}
}