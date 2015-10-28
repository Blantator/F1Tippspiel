using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F1Tippspiel.Web.Controllers
{
	public class TemplatesController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Welcome()
		{
			return View("_welcome");
		}

		public ActionResult Rules()
		{
			return View("_rules");
		}

		public ActionResult Game()
		{
			return View("_game");
		}

		public ActionResult Admin()
		{
			return View("_admin");
		}
	}
}