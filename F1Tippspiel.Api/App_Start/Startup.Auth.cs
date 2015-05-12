using System;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;

namespace ApiLocalLogin
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        // Weitere Informationen zum Konfigurieren der Authentifizierung finden Sie unter "http://go.microsoft.com/fwlink/?LinkId=301864".
        public void ConfigureAuth(IAppBuilder app)
        {


        }
    }
}
