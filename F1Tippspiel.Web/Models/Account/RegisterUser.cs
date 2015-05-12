using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F1Tippspiel.Web.Models.Account
{
    public class RegisterUser
    {
        [Required(ErrorMessage="Bitte geben Sie einen Anzeigenamen an")]
        [DisplayName("Anzeigename")]
        public string Displayname { get; set; }

        [Required(ErrorMessage="Bitte geben Sie eine Emailadressen an")]
        [DisplayName("E-Mail Adresse")]
        [DataType(DataType.EmailAddress, ErrorMessage="Bitte eine gültige E-Mail Adresse angeben")]
        [Remote("EmailIsFree", "account", ErrorMessage="Diese E-Mail Adresse ist bereits in Verwendung!")]
        public string Email { get; set; }

        [Required(ErrorMessage="Bitte geben Sie ein Passwort an")]
        [DisplayName("Passwort")]
        public string Password { get; set; }

        [DisplayName("Passwort wiederholen")]
        [Required]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Die Passwörter stimmen nicht überein!")]
        public string Password2 { get; set; }
    }
}