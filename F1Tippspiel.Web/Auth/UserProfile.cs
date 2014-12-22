using F1Tippspiel.Db.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace F1Tippspiel.Web.Auth
{
    /// <summary>
    /// Represents a user profile with the basic information provided by the IdentityProvider
    /// This information is the common ground between the Identity Providers
    /// </summary>
    public class UserProfile
    {
        /// <summary>
        /// "Surname_Familyname"
        /// </summary>
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Picture { get; set; }
        public string Email { get; set; }
        public string UniqueId { get; set; }
        public IdentityProvider Provder { get; set; }
    }
}