using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F1Tippspiel.Db.Account;
using F1Tippspiel.Db.Data;
using F1Tippspiel.Db.Game;
using F1Tippspiel.Db.Rewards;

namespace F1Tippspiel.Db.Initializer
{
    public class BasicInitializer : DropCreateDatabaseAlways<AppDb>
    {
        protected override void Seed(AppDb context)
        {
            //To have some date to work with seed a new season with basic configuration:
            // Season
            // - Some Clubs with drivers
            // - two users one admin and default
            // - some achievements and badges for the user
            // - some messages from one user to the other

            Achievement manyLogins = new Achievement()
            {
                Description = "You logged in at least 10 times",
                Name = "Addicted",
                Icon = "some/icon.png"
            };

            Achievement firstBet = new Achievement()
            {
                Description = "You were the first to place a bet for the next race",
                Name = "Speedy Gonzales",
                Icon = "some/speedy.png"
            };

            Badge seasonWinner = new Badge()
            {
                Description = "You won a season",
                Name = "Champion",
                Icon = "badges/champion.png"
            };

            UserAccount admin = new UserAccount()
            {
                Admin = true,
                DisplayName = "admin",
                Email = "email@admin.com",
                LastSeen = DateTime.Now,
                Enabled = true,
                Name = "Mr Admin",
                NotificationEmail = "email@admin.com",
                Registered = DateTime.Now,
                Achievements = new Collection<Achievement>(),
                Badges = new Collection<Badge>()
            };

            Race melRace = new Race()
            {
                City = "Melbourne",
                Country = "Australia",
                Picture = "city/melbourne.png",
                Qualifying = DateTime.Now.AddDays(1),
                RaceTime = DateTime.Now.AddDays(2)
            };

            Track melbourne = new Track()
            {
                Name = "Melbourne",
                Picture = "tracks/melbourne",
                Race = melRace
            };

            Driver alonso = new Driver()
            {
                Name = "Fernando Alonso",
                Image = "drivers/alonso.png"
            };
            Driver vettel = new Driver()
            {
                Name = "Sebastian Vettel",
                Image = "drivers/vettel.png"
            };

            Club mclaren = new Club()
            {
                Name = "Mclaren",
                Logo = "clubs/mclaren.png",
                Drivers = new Collection<Driver>()
            };

            Season season2014 = new Season()
            {
                 Year = 2014,
                 Clubs = new Collection<Club>(),
                 Players = new Collection<UserAccount>(),
                 Tracks = new Collection<Track>()
            };

            mclaren.Drivers.Add(alonso);
            mclaren.Drivers.Add(vettel);

            admin.Achievements.Add(firstBet);
            admin.Achievements.Add(manyLogins);
            admin.Badges.Add(seasonWinner);

            season2014.Players.Add(admin);
            season2014.Clubs.Add(mclaren);
            season2014.Tracks.Add(melbourne);

            context.Seasons.Add(season2014);
            context.SaveChanges();

        }
    }
}
