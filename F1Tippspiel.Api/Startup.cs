using F1Tippspiel.Api.Providers;
using F1Tippspiel.Db.Data;
using F1Tippspiel.Db.Initializer;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(F1Tippspiel.Api.Startup))]
namespace F1Tippspiel.Api
{
	public class Startup
	{
		public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }

		public void Configuration(IAppBuilder app)
		{
			ConfigureOAuth(app);
			HttpConfiguration config = new HttpConfiguration();
			WebApiConfig.Register(config);
			app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
			app.UseWebApi(config);

			Database.SetInitializer(new BasicInitializer());
		}

		public void ConfigureOAuth(IAppBuilder app)
		{
			OAuthBearerOptions = new OAuthBearerAuthenticationOptions();
			OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
			{
				AllowInsecureHttp = true,
				TokenEndpointPath = new PathString("/api/token"),
				AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
				RefreshTokenProvider = new SimpleRefreshTokenProvider(),
				Provider = new SimpleAuthorizationServerProvider()
			};

			// Token Generation
			app.UseOAuthAuthorizationServer(OAuthServerOptions);
			app.UseOAuthBearerAuthentication(OAuthBearerOptions);

		}
	}
}