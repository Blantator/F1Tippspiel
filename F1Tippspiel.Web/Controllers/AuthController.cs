using F1Tippspiel.Db.Account;
using F1Tippspiel.Db.Data;
using F1Tippspiel.Web.Auth;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Plus.v1;
using Google.Apis.Plus.v1.Data;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TweetSharp;

namespace F1Tippspiel.Web.Controllers
{
    public class AuthController : Controller
    {
        AppDb _db;

        public AuthController()
        {
            _db = new AppDb();
        }

        public AuthController(AppDb db)
        {
            _db = db;
        }

        /// <summary>
        /// Performs a login with Google
        /// </summary>
        /// <param name="cancelToken"></param>
        /// <returns></returns>
        public async Task<ActionResult> GoogleLogin(CancellationToken cancelToken)
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

            UserAccount account = _db.Users.FirstOrDefault(u => u.UniqueId.Equals(me.Id));
            if(account == null){
                //this is a new user   
                UserProfile profile = new UserProfile(){
                    Name = me.Name.GivenName + " " + me.Name.FamilyName,
                    Email = me.Emails.ElementAt(0).Value,
                    DisplayName = me.DisplayName,
                    Picture = me.Image.Url,
                    UniqueId = me.Id,
                    Provder = IdentityProvider.Google
                };
                TempData["newProfile"] = profile; 
                return RedirectToAction("register", "account");
            }else{
                //this is a registered user -> login
            }

            return View();
        }

        private string _consumerKey = "fgZC4hs4O5EMlw9ALtADSW229";
        private string _consumerSecret = "ddYAkWW4cff2YwrF7FTo8ZoY5FowtDADZuQFWAz1JfkHakWZLY";

        public ActionResult TwitterLogin()
        {
            // Step 1 - Retrieve an OAuth Request Token
            TwitterService service = new TwitterService(_consumerKey, _consumerSecret);

            // This is the registered callback URL
            OAuthRequestToken requestToken = service.GetRequestToken("http://127.0.0.1:49374/Auth/TwitterCallback");

            // Step 2 - Redirect to the OAuth Authorization URL
            Uri uri = service.GetAuthorizationUri(requestToken);
            return new RedirectResult(uri.ToString(), false /*permanent*/);
        }

        public ActionResult TwitterCallback(string oauth_token, string oauth_verifier)
        {
            var requestToken = new OAuthRequestToken { Token = oauth_token };

            // Step 3 - Exchange the Request Token for an Access Token
            TwitterService service = new TwitterService(_consumerKey, _consumerSecret);
            OAuthAccessToken accessToken = service.GetAccessToken(requestToken, oauth_verifier);

            // Step 4 - User authenticates using the Access Token
            service.AuthenticateWith(accessToken.Token, accessToken.TokenSecret);
            TwitterUser user = service.VerifyCredentials(new VerifyCredentialsOptions());

            UserAccount account = _db.Users.FirstOrDefault(u => u.UniqueId.Equals(user.Id.ToString()));
            if (account == null)
            {
                //this is a new user   
                UserProfile profile = new UserProfile()
                {
                    Name = user.Name,
                    Email = "",
                    DisplayName = user.ScreenName,
                    Picture = user.ProfileImageUrl,
                    UniqueId = user.Id.ToString(),
                    Provder = IdentityProvider.Twitter
                };
                TempData["newProfile"] = profile;
                return RedirectToAction("register", "account");
            }
            else
            {
                //this is a registered user -> login
            }

            return View();
        }
    }
}