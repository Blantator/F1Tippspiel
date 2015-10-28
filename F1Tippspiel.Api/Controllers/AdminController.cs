using F1Tippspiel.Db.Data;
using F1Tippspiel.Db.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace F1Tippspiel.Api.Controllers
{
	[Authorize(Roles ="Admin")]
	[RoutePrefix("api/admin")]
	public class AdminController : ApiController
	{
		[Route("drivers")]
		[HttpGet]
		public async Task<IHttpActionResult> Drivers()
		{
			using (AppContext db = new AppContext())
			{
				List<Driver> drivers = db.Drivers.ToList();
				if (drivers != null)
				{
					return Ok(drivers);
				}
				else
				{
					return Ok(new LinkedList<Driver>());
				}
			}
		}

		[Route("teams")]
		[HttpGet]
		public async Task<IHttpActionResult> Teams()
		{
			using (AppContext db = new AppContext())
			{
				List<Club> teams = db.Clubs.Include("Drivers").AsNoTracking().ToList();
				if(teams != null)
				{
					return Ok(teams);
				}
				else
				{
					return Ok(new LinkedList<Club>());
				}
			}
		}

		[Route("tracks")]
		[HttpGet]
		public async Task<IHttpActionResult> Tracks()
		{
			using (AppContext db = new AppContext())
			{
				List<Track> tracks = db.Tracks.Include("Race.QualifyingResults").Include("Race.Results").AsNoTracking().ToList();
				if (tracks != null)
				{
					return Ok(tracks);
				}
				else
				{
					return Ok(new LinkedList<Club>());
				}
			}
		}
	}
}
