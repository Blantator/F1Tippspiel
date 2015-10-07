using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F1Tippspiel.Db.Rewards;
using F1Tippspiel.Db.Game;
using Microsoft.AspNet.Identity.EntityFramework;

namespace F1Tippspiel.Db.Account
{
    public class UserAccount : IdentityUser
    {
        //[Key]
        //public int UserId { get; set; }

        public string Picture { get; set; }
        public string DisplayName { get; set; }

        //public string Password { get; set; }
        //public string Email { get; set; }

        public bool Admin { get; set; }
        public bool Enabled { get; set; }

        public DateTime Registered { get; set; }
        public DateTime LastSeen { get; set; }

        public virtual ICollection<Achievement> Achievements { get; set; }
        public virtual ICollection<Badge> Badges { get; set; }
        public virtual ICollection<RaceBet> RaceBets { get; set; }
    }
}
