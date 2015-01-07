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

            // Anwendung für die Verwendung eines Cookies zum Speichern von Informationen für den angemeldeten Benutzer aktivieren
            // und ein Cookie zum vorübergehenden Speichern von Informationen zu einem Benutzer zu verwenden, der sich mit dem Anmeldeanbieter eines Drittanbieters anmeldet.
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Anwendung für auf OAuth basierenden Datenfluss konfigurieren
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                AllowInsecureHttp = true
            };

            // Anwendung für die Verwendung eines Trägertokens zum Authentifizieren von Benutzern aktivieren
            app.UseOAuthBearerTokens(OAuthOptions);
        }
    }
}
