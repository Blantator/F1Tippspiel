using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using F1Tippspiel.Db.Data;
using Google.Apis.Auth.OAuth2.Mvc;
using System.Threading;
using F1Tippspiel.Web.Auth;
using System.Threading.Tasks;
using Google.Apis.Plus.v1;
using Google.Apis.Services;
using Google.Apis.Plus.v1.Data;

namespace F1Tippspiel.Web.Controllers
{
    public class HomeController : Controller
    {
        AppDb _db;

        public HomeController()
        {
            _db = new AppDb();
        }

        public HomeController(AppDb db)
        {
            _db = db;
        }

        // GET: Home
        public ActionResult Index()
        {
            //Debug.WriteLine(_db.Tracks.ToList());
            return View();
        }

        public async Task<ActionResult> Login(CancellationToken cancelToken)
        {
            var result = await new AuthorizationCodeMvcApp(this, new GoogleAuth()).AuthorizeAsync(cancelToken);

            if (result.Credential == null)
                return new RedirectResult(result.RedirectUri);

            var plusService = new PlusService(new BaseClientService.Initializer
            {
                HttpClientInitializer = result.Credential,
                ApplicationName = "F1 Tippspiel"
            });

            Person me = plusService.People.Get("me").Execute();

            return View();
        }

        public ActionResult start()
        {
            return PartialView("_index");
        }
    }
}