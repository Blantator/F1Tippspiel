using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Plus.v1;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F1Tippspiel.Web.Auth
{
    public class GoogleAuth : FlowMetadata
    {
        private static readonly IAuthorizationCodeFlow flow =
            new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = "906663688504-m7q6505kjoosvalij5f2rp7f48d74117.apps.googleusercontent.com",
                    ClientSecret = "oev2awqj_m3x6AMe9S_rqwbn"
                },
                Scopes = new[] {
                    PlusService.Scope.UserinfoEmail,
                    PlusService.Scope.UserinfoProfile
                },
                DataStore = new FileDataStore("Google.Apis.Sample.MVC")
            });

        public override string GetUserId(Controller controller)
        {
            return "f1user";
        }

        public override IAuthorizationCodeFlow Flow
        {
            get { return flow; }
        }
    }
}