using F1Tippspiel.Db.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1Tippspiel.Db.Game
{
	public class RaceBet
	{
		[Key]
		public int Id { get; set; }

		/// <summary>
		/// Player who placed this bet
		/// </summary>
		public UserAccount Player { get; set; }

		/// <summary>
		/// Date & Time of the original Bet
		/// </summary>
		public DateTime Date { get; set; }

		//The race for which the bet was placed
		public virtual Race Race { get; set; }

		public virtual Driver Place1 { get; set; }

		public virtual Driver Place2 { get; set; }

		public virtual Driver Place3 { get; set; }
	}
}
