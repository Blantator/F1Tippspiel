using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F1Tippspiel.Web.Models.Account
{
    public class LoginCredentials
    {
        [DisplayName("Benutzername")]
        [Required(ErrorMessage="Bitte einen Benutzernamen angeben")]
        [EmailAddress]
        public string Username { get; set; }

        [DisplayName("Passwort")]
        [Required(ErrorMessage="Bitte das Passwort angeben!")]
        public string Password { get; set; }

        [DisplayName("Login merken")]
        public bool Remember { get; set; }
    }
}