using F1Tippspiel.Web.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F1Tippspiel.Web.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Register()
        {
            UserProfile profile = TempData["newProfile"] == null ? new UserProfile() : TempData["newProfile"] as UserProfile;

            return View(profile);
        }
    }
}