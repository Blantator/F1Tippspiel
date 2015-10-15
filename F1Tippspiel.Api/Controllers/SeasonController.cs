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
    }
}
