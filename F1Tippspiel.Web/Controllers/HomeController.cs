using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using F1Tippspiel.Db.Data;

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
            Debug.WriteLine(_db.Tracks.ToList());
            return View();
        }
    }
}