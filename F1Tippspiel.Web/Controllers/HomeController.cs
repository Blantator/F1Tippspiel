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
        // GET: Home
        public ActionResult Index()
        {
            AppDb db = new AppDb();
            Debug.WriteLine(db.Tracks.ToList());
            return View();
        }
    }
}