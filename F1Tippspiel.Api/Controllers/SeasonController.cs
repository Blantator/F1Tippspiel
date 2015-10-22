using F1Tippspiel.Api.Models;
using F1Tippspiel.Db.Authentication;
using F1Tippspiel.Db.Data;
using F1Tippspiel.Db.Game;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace F1Tippspiel.Api.Controllers
{
	[RoutePrefix("api/season")]
	public class SeasonController : ApiController
	{
		[Authorize]
		[Route("")]
		public async Task<IHttpActionResult> Get()
		{
			int year = 0;
			using (AppContext db = new AppContext())
			{
				Season season = db.Seasons.OrderByDescending(o => o.Year).FirstOrDefault();
				if(season == null)
				{
					year = -1;
				}
				else
				{
					year = season.Year;
				}
			}

			dynamic expando = new ExpandoObject();
			expando.currentSeason = year;

			return Ok(expando);
		}

		[Authorize]
		[Route("currentPlayerStandings")]
		[HttpGet]
		public async Task<IHttpActionResult> PlayerStandings()
		{
			using (AppContext db = new AppContext())
			{
				Season season = db.Seasons.OrderByDescending(o => o.Year).FirstOrDefault();
				if (season != null)
				{
					LinkedList<PlayerStandingsModel> standings = new LinkedList<PlayerStandingsModel>();
					//TODO: Calculate points for player
					//TODO: Calculate change since last race for player
					foreach(UserAccount u in season.Players)
					{
						standings.AddLast(new PlayerStandingsModel()
						{
							Changed = 0,
							Email = u.Email,
							IsOnline = (DateTime.Now - u.LastSeen) < TimeSpan.FromMinutes(5),
							Name = u.DisplayName,
							PlacedBet = true,
							PlayerId = u.Id,
							Points = 13
						});
					}

					return Ok(standings);
				}
			}
			
			//If we're here something is wrong
			return InternalServerError();
		}
	}
}
