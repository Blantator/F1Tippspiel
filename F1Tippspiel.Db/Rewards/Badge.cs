using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F1Tippspiel.Db.Account;

namespace F1Tippspiel.Db.Rewards
{
    /// <summary>
    /// Reprsents a badge which a user can earn for completing relatively complex tasks
    /// like winning a season
    /// </summary>
    class Badge
    {
        [Key]
        public int BadgeId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }

        public virtual ICollection<UserAccount> UserAccounts { get; set; }
    }
}
