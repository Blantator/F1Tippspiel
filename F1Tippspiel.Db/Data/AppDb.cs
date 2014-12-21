using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F1Tippspiel.Db.Account;
using F1Tippspiel.Db.Communication;
using F1Tippspiel.Db.Game;
using F1Tippspiel.Db.Rewards;

namespace F1Tippspiel.Db.Data
{
    class AppDb : DbContext
    {
        public AppDb() : base("F1Db"){}

        public AppDb(DbConnection connection)
            : base(connection, true) { }

        public DbSet<UserAccount> Users { get; set; }
        //public DbSet<AccountProvider AccountProviders { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserMessage> UserMessages { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<Achievement> Achievements { get; set; }

        public DbSet<Club> Clubs { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<RaceResult> RaceResults { get; set; }
    }
}
