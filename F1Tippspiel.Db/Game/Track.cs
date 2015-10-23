using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace F1Tippspiel.Db.Game
{
	/// <summary>
	/// Represents a track on which a race can take place
	/// </summary>
	public class Track
	{
		[Key]
		public int TrackId { get; set; }

		public string City { get; set; }
		public string Country { get; set; }
		public string Picture { get; set; }
		public string Name { get; set; }
		/// <summary>
		/// definex a prefix which is used to locate different resources
		/// Example: PrefixName = "italy" -> Banner: italy_banner.png | TrackThumbnail: italy_track.png
		/// </summary>
		public string PrefixName { get; set; }
		public virtual Race Race { get; set; }
	}
}
