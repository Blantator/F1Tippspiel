//using F1Tippspiel.Db.Account;
//using F1Tippspiel.Db.Data;
//using F1Tippspiel.Db.Game;
//using F1Tippspiel.Db.Rewards;
//using F1Tippspiel.Db.Tools;
//using F1Tippspiel.Db.Models.Account;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Cryptography;
//using System.Text;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.Security;

//namespace F1Tippspiel.Web.Controllers
//{
//    public class AccountController : Controller
//    {

//        public ActionResult Register()
//        {
//            RegisterUser user = new RegisterUser();

//            return View(user);
//        }

//        [HttpPost, ActionName("register")]
//        public ActionResult RegisterUser(RegisterUser newUser)
//        {
//            if (newUser != null)
//            {
//                UserAccount user;
//                using(AppDb db = new AppDb())
//                {
//                    //if (db.Users.FirstOrDefault(u => u.Email.Equals(newUser.Email)) != null)
//                    //{
//                    //    //there is already an user with this email address
//                    //    ViewData.ModelState.AddModelError("Email", "Diese E-Mail Adresse wird bereits von einem anderen Spieler genutzt!");
//                    //    return View("register", newUser);
//                    //}
//                    if(!newUser.Password.Equals(newUser.Password2))
//                    {
//                        //the passwords provieded by the user do not match
//                        ViewData.ModelState.AddModelError("Password", "Die angegebenen Passwörter stimmen nicht überein!");
//                        return View("register", newUser);
//                    }
//                    user = new UserAccount()
//                    {
//                        Admin = false,
//                        Picture = "/Content/static/img/unknown_user.png",
//                        DisplayName = newUser.Displayname,
//                        //Email = newUser.Email,
//                        LastSeen = DateTime.Now,
//                        Registered = DateTime.Now,
//                        Enabled = true,
//                        //Password = Hasher.GenerateMD5(newUser.Password),
//                        Achievements = new LinkedList<Achievement>(),
//                        RaceBets = new LinkedList<RaceBet>(),
//                        Badges = new LinkedList<Badge>()
//                    };
//                    db.Users.Add(user);

//                    //add player to current season
//                    Season currentSeason = db.Seasons.FirstOrDefault(s => s.Year.Equals(DateTime.Now.Year));
//                    currentSeason.Players.Add(user);

//                    db.SaveChanges();
//                }
//                //user created successful, redirect to welcome page
//                return View("welcome", user);
//            }
//            return View("register");
//        }

//        [HttpPost]
//        public ActionResult Login(LoginCredentials login)
//        {
//            using(AppDb db = new AppDb()){
//                UserAccount user = db.Users.FirstOrDefault(u => u.Email.Equals(login.Username));
//                if (user != null)
//                {
//                    if (user.Password.Equals(Hasher.GenerateMD5(login.Password)))
//                    {
//                        //user credentials are correct
//                        if(db.Seasons.FirstOrDefault(s => s.Year.Equals(DateTime.Now.Year)).Players.Contains(user)){
//                            //the user is registered for the current season => proceed
//                            FormsAuthentication.SetAuthCookie(user.Email, login.Remember);
//                            return RedirectToAction("index", "game");
//                        }
//                        else
//                        {
//                            //the user is registered but has not signed up for the current seaon yet
//                            //TODO: offer user to signup for new season (add account to new season)
//                            ViewData.ModelState.AddModelError("Username", "Sie haben sich nicht für die aktuelle Saiton regiestriert!");
//                            return View("~/Views/Home/Index.cshtml", login);
//                        }
//                    }
//                }
//            }
//            //login failed
//            ViewData.ModelState.AddModelError("Username", "Der Benutzername oder das Passwort stimmt nicht!");
//            ViewData.ModelState.AddModelError("Password", "Der Benutzername oder das Passwort stimmt nicht!");
//            return View("~/Views/Home/Index.cshtml", login);
//        }

//        [Authorize]
//        public ActionResult Logout()
//        {
//            FormsAuthentication.SignOut();
//            return RedirectToAction("index", "home");
//        }

//        public ActionResult ForgotPassword()
//        {
//            return View(new PasswordRecovery());
//        }

//        [HttpPost, ActionName("forgotpassword")]
//        public ActionResult ForgotPassword(PasswordRecovery recovery)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View(recovery);
//            }

//            using (AppDb db = new AppDb())
//            {
//                //UserAccount user = db.Users.FirstOrDefault(u => u.Email.Equals(recovery.EMail));
//                //if (user != null)
//                //{
//                //    //TODO: generate password and send mail
//                //    recovery.Successful = true;
//                //    return View(recovery);
//                //}
//            }

//            //no user found for the provides email address
//            return View(recovery);
//        }

//        /// <summary>
//        /// Is called by ajax during the registration process
//        /// </summary>
//        /// <param name="email">email address to check agains the database</param>
//        /// <returns>true if the email address is not in use by an other user</returns>
//        public string EmailIsFree(string email)
//        {
//            //string isFree = "false";
//            //using (AppDb db = new AppDb())
//            //{
//            //    isFree = (db.Users.FirstOrDefault(u => u.Email.Equals(email)) == null?"true":"false");
//            //}
//            //return isFree;
//        }
//    }
//}