using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1Tippspiel.Db.Account
{
    /// <summary>
    /// Represents a connection to a identity provider like google, facebook or twitter
    /// </summary>
    public class AccountProvider
    {
        public int AccountProviderId { get; set; }
        public string ProviderSpecificId { get; set; }

        public IdentityProvider Provider { get; set; }
    }

    public enum IdentityProvider
    {
        Google = 0,
        Twitter = 1,
        Facebook = 2
    }
}
