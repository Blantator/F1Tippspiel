using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace F1Tippspiel.Db.Game
{
	public class Race
	{
		[Key]
		public int RaceId { get; set; }

		public int TrackId { get; set; }

		public DateTime Qualifying { get; set; }
		public DateTime RaceTime { get; set; }
		/// <summary>
		/// Race results (first 3)
		/// </summary>
		public virtual ICollection<DriverResult> Results { get; set; }

		/// <summary>
		/// Qualifying results (first 7)
		/// </summary>
		public virtual ICollection<DriverResult> QualifyingResults { get; set; }
	}
}