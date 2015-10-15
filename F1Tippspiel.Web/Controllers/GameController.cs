using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F1Tippspiel.Web.Controllers
{
    public class GameController : Controller
    {
        // GET: Game
        public ActionResult Start()
        {
            return View("_start");
        }
    }
}