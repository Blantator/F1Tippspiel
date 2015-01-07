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
        /// <summary>
        /// Is called when open auth returns an id which is unknown to the database
        /// -> user needs to register
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            UserProfile profile = TempData["newProfile"] == null ? new UserProfile() : TempData["newProfile"] as UserProfile;

            return View(profile);
        }

        /// <summary>
        /// Is called when open auth returns an id which is known by the database
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            var localAccessToken = TempData["accessToken"];

            return View();
        }
    }
}