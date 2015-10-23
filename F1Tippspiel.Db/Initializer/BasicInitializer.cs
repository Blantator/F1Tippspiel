using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F1Tippspiel.Db.Authentication;
using F1Tippspiel.Db.Data;
using F1Tippspiel.Db.Game;
using F1Tippspiel.Db.Rewards;
using F1Tippspiel.Db.Tools;
using F1Tippspiel.Db.Repositories;

namespace F1Tippspiel.Db.Initializer
{
	public class BasicInitializer : CreateDatabaseIfNotExists<AppContext>
	{
		protected override async void Seed(AppContext context)
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
				UserName = "bigbasti@gmail.com",
				Admin = true,
				DisplayName = "bigbasti",
				Email = "bigbasti@gmail.com",
				LastSeen = DateTime.Now,
				Registered = DateTime.Now,
				EmailConfirmed = true,
				TwoFactorEnabled = false,
				Enabled = true,
				//Password = Hasher.GenerateMD5("12011021"),
				Achievements = new LinkedList<Achievement>(),
				RaceBets = new LinkedList<RaceBet>(),
				Badges = new LinkedList<Badge>()
			};
			//using (var authRep = new AuthRepository())
			//{
			//    var res = authRep.RegisterUser(admin, "12011021");
			//    Console.WriteLine("User created successfully: " + res.Succeeded);
			//}
			

			Race melRace = new Race()
			{
				Qualifying = DateTime.Now.AddDays(1),
				RaceTime = DateTime.Now.AddDays(2)
			};

			Track melbourne = new Track()
			{
				City = "Melbourne",
				Country = "Australia",
				Name = "Melbourne Ring",
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

			Season season2015 = new Season()
			{
				 Year = 2015,
				 Clubs = new Collection<Club>(),
				 Players = new Collection<UserAccount>(),
				 Tracks = new Collection<Track>()
			};

			Client testClient = new Client()
			{
				Id = "postman",											//REST Tool for Crome
				Active = true,											//Is Allowed to request API
				AllowedOrigin = "*",									//for dev only
				ApplicationType = ApplicationType.JavaScript,			//JS Apps don't need to provide secret
				Name = "Development testclient",						//foo bar
				RefreshTokenLifeTime = 7200,							//token lifetime in seconds (30min)
				Secret = "lCXDroz4HhR1EIx8qaz3C13z/quTXBkQ3Q5hj7Qx3aA=" //App secret, only relevant for native Apps
			};

			mclaren.Drivers.Add(alonso);
			mclaren.Drivers.Add(vettel);

			admin.Achievements.Add(firstBet);
			admin.Achievements.Add(manyLogins);
			admin.Badges.Add(seasonWinner);
			
			//season2015.Players.Add(admin);
			season2015.Clubs.Add(mclaren);
			season2015.Tracks.Add(melbourne);

			context.Clients.Add(testClient);

			context.Seasons.Add(season2015);
			context.SaveChanges();

			context.Dispose();
			using (var authRep = new AuthRepository())
			{
				var res = await authRep.RegisterUser(admin, "12011021");
			}
			using (AppContext db = new AppContext())
			{
				Season ls = db.Seasons.OrderByDescending(o => o.SeasonId).First();
				if(ls != null)
				{
					UserAccount ua = db.Users.Where(u => u.Email == admin.Email).FirstOrDefault();
					if (ua != null)
					{
						ls.Players.Add(ua);
						db.SaveChanges();
					}
				}
			}
		}
	}
}
