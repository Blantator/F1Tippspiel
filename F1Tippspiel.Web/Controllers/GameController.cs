using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F1Tippspiel.Web.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        // GET: Game
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Start()
        {
            return PartialView("_index");
        }
    }
}