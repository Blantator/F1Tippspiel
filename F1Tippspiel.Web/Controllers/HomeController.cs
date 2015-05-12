using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using F1Tippspiel.Db.Data;
using Google.Apis.Auth.OAuth2.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Plus.v1;
using Google.Apis.Services;
using Google.Apis.Plus.v1.Data;
using F1Tippspiel.Web.Models.Account;

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
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "game");
            }
            return View(new LoginCredentials());
        }

        public ActionResult Rules()
        {
            return View();
        }


    }
}