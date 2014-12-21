using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F1Tippspiel.Db.Rewards;

namespace F1Tippspiel.Db.Account
{
    class UserAccount
    {
        [Key]
        public int UserId { get; set; }

        //public virtual AccountProvider AccountProvider { get; set; }

        public string Name { get; set; }
        public string DisplayName { get; set; }

        public string Email { get; set; }
        public string NotificationEmail { get; set; }

        public bool Admin { get; set; }
        public bool Enabled { get; set; }

        public DateTime Registered { get; set; }
        public DateTime LastSeen { get; set; }

        public virtual ICollection<Achievement> Achievements { get; set; }
        public virtual ICollection<Badge> Badges { get; set; }

    }
}
