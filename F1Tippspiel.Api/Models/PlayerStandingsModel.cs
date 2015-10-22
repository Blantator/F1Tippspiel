using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace F1Tippspiel.Api.Models
{
	public class PlayerStandingsModel
	{
		public string PlayerId { get; set; }
		public string Name { get; set; }
		public int Changed { get; set; }
		public bool PlacedBet { get; set; }
		public bool IsOnline { get; set; }
		public string Email { get; set; }
		public int Points { get; set; }
	}
}