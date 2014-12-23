using F1Tippspiel.Db.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        /// 
        [Required(ErrorMessage="Bitte geben Sie einen Namen an")]
        [DisplayName("Ihr Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bitte geben Sie einen Anzeigenamen an")]
        [DisplayName("Ihr Anzeigename")]
        public string DisplayName { get; set; }

        public string Picture { get; set; }

        [Required(ErrorMessage = "Bitte geben Sie eine E-Mailadresse an")]
        [DisplayName("Ihre E-Mail Adresse")]
        [RegularExpression("([a-zA-Z0-9]+@[a-zA-Z0-9]+\\.[a-zA-Z0-9]+)", ErrorMessage = "Bitte eine gültige E-Mail Adresse angeben")]
        public string Email { get; set; }

        [Required]
        public string UniqueId { get; set; }

        public IdentityProvider Provider { get; set; }
    }
}