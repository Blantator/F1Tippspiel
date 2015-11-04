using F1Tippspiel.Db.Authentication;
using F1Tippspiel.Db.Communication;
using F1Tippspiel.Db.Game;
using F1Tippspiel.Db.Rewards;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1Tippspiel.Db.Data
{
	public class AppContext : IdentityDbContext<UserAccount>
	{
		public AppContext()
			: base("F1Db"){}

		public AppContext(DbConnection connection)
			: base(connection, true) {}

		public DbSet<Client> Clients { get; set; }
		public DbSet<RefreshToken> RefreshTokens { get; set; }

		public DbSet<Comment> Comments { get; set; }
		public DbSet<UserMessage> UserMessages { get; set; }
		public DbSet<Badge> Badges { get; set; }
		public DbSet<Achievement> Achievements { get; set; }

		public DbSet<Club> Clubs { get; set; }
		public DbSet<Driver> Drivers { get; set; }
		public DbSet<Race> Races { get; set; }
		public DbSet<Track> Tracks { get; set; }
		public DbSet<Season> Seasons { get; set; }
		public DbSet<DriverResult> RaceResults { get; set; }
	}
}
