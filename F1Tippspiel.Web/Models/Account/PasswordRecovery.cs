using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F1Tippspiel.Web.Models.Account
{
    public class PasswordRecovery
    {
        [Required(ErrorMessage = "Bitte geben Sie Ihre E-Mail Adresse an!")]
        [DisplayName("E-Mail Adresse")]
        [EmailAddress]
        public string EMail { get; set; }

        public bool Successful { get; set; }

        public PasswordRecovery()
        {
            Successful = false;
        }
    }
}