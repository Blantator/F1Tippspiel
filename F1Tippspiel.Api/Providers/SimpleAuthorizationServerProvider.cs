using F1Tippspiel.Db.Authentication;
using F1Tippspiel.Db.Repositories;
using F1Tippspiel.Db.Tools;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace F1Tippspiel.Api.Providers
{
	public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
	{
		public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
		{
			string clientId = string.Empty;
			string clientSecret = string.Empty;
			Client client = null;

			if(!context.TryGetBasicCredentials(out clientId, out clientSecret))
			{
				context.TryGetFormCredentials(out clientId, out clientSecret);
			}

			if(context.ClientId == null)
			{
				//clientId should always be provided
				//context.Rejected();
				context.SetError("invalid_clientId", "ClientId should be sent.");
				return;
			}

			using (AuthRepository _repo = new AuthRepository())
			{
				client = _repo.FindClient(context.ClientId);
			}

			if(client == null)
			{
				//the provided clientId is not registered in our database -> reject
				context.SetError("invalid_clientId", string.Format("Client '{0}' is not registered in the system.", context.ClientId));
				return;
			}

			if(client.ApplicationType == ApplicationType.Native)
			{
				if (string.IsNullOrWhiteSpace(clientSecret))
				{
					//Native clients must provide client secret
					context.SetError("invalid_clientId", "Client secret should be sent.");
				}
				else
				{
					if(client.Secret != Hasher.GetHash(clientSecret))
					{
						//the provided client secrent does not match the registered one
						context.SetError("invalid_clientId", "Client secret is invalid");
						return;
					}
				}
			}

			if (!client.Active)
			{
				context.SetError("invalid_clientId", "Client is inactive");
				return;
			}

			context.OwinContext.Set("as:clientAllowedOrigin", client.AllowedOrigin);
			context.OwinContext.Set("as:clientRefreshTokenLifeTime", client.RefreshTokenLifeTime.ToString());

			context.Validated();
			return;
		}

		public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
		{
			var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
			if(allowedOrigin == null) { allowedOrigin = "*"; }

			context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

			UserAccount account;
			using (AuthRepository _repo = new AuthRepository())
			{
				IdentityUser user = await _repo.FindUser(context.UserName, context.Password);

				if (user == null)
				{
					context.SetError("invalid_grant", "The user name or password is incorrect.");
					return;
				}
				account = (UserAccount)user;
			}

			string role = account.Admin ? "admin" : "user";

			var identity = new ClaimsIdentity(context.Options.AuthenticationType);
			identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
			identity.AddClaim(new Claim("sub", context.UserName));
			identity.AddClaim(new Claim("role", role));

			var props = new AuthenticationProperties(new Dictionary<string, string>
			{
				{ "as:client_id", (context.ClientId == null) ? string.Empty : context.ClientId },
				{ "userName", context.UserName },
				{ "displayName", account.DisplayName }
			});
			
			var ticket = new AuthenticationTicket(identity, props);
			context.Validated(ticket);
		}

		public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
		{
			var originalClient = context.Ticket.Properties.Dictionary["as:client_id"];
			var currentClient = context.ClientId;

			if(originalClient != currentClient)
			{
				context.SetError("invalid_clientId", "Refresh token is issued to a different clientId");
				return Task.FromResult<object>(null);
			}

			var newIdentity = new ClaimsIdentity(context.Ticket.Identity);
			newIdentity.AddClaim(new Claim("newClaim", "newValue"));

			var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
			context.Validated(newTicket);

			return Task.FromResult<object>(null);
		}

		/// <summary>
		/// Adds additional information to the responses
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override Task TokenEndpoint(OAuthTokenEndpointContext context)
		{
			foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
			{
				context.AdditionalResponseParameters.Add(property.Key, property.Value);
			}

			return Task.FromResult<object>(null);
		}
	}
}