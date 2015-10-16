using F1Tippspiel.Db.Data;
using F1Tippspiel.Db.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1Tippspiel.Db.Repositories
{
	public class SeasonRepository : IDisposable
	{
		private AppContext _ctx;

		public SeasonRepository()
		{
			_ctx = new AppContext();
		}

		public SeasonRepository(AppContext context)
		{
			_ctx = context;
		}

		public Season LatestSeason()
		{
			return _ctx.Seasons.OrderByDescending(o => o.SeasonId).First();
		}

		public Season CurrentSeason()
		{
			return _ctx.Seasons.FirstOrDefault(s => s.Year == DateTime.Now.Year);
		}

		public bool Save()
		{
			return _ctx.SaveChanges() > 0;
		}

		public void Dispose()
		{
			_ctx.Dispose();
		}
	}
}
