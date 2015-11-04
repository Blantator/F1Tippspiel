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
				List<Club> teams = db.Seasons
					.Include("Clubs")
					.Include("Clubs.Drivers")
					.AsNoTracking()
					.OrderByDescending(o => o.Year)
					.FirstOrDefault()
					.Clubs.ToList();

				List<Driver> drivers = new List<Driver>();
				foreach(Club c in teams)
				{
					c.Drivers.ToList().ForEach(d => { drivers.Add(d); });
				}
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
				List<Club> teams = db.Seasons
					.Include("Clubs")
					.Include("Clubs.Drivers")
					.AsNoTracking()
					.OrderByDescending(o => o.Year)
					.FirstOrDefault()
					.Clubs.ToList(); 
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
				List<Track> tracks = db.Seasons
					.Include("Tracks")
					.Include("Tracks.Race.QualifyingResults")
					.Include("Tracks.Race.Results")
					.AsNoTracking()
					.OrderByDescending(o => o.Year)
					.FirstOrDefault()
					.Tracks.ToList();
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

		[Route("dates")]
		[HttpGet]
		public async Task<IHttpActionResult> Dates()
		{
			using (AppContext db = new AppContext())
			{
				List<Track> tracks = db.Seasons
					.Include("Tracks")
					.Include("Tracks.Race.QualifyingResults")
					.Include("Tracks.Race.Results")
					.AsNoTracking()
					.OrderByDescending(o => o.Year)
					.FirstOrDefault()
					.Tracks.ToList();
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
