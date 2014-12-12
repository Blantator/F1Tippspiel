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
    /// Achievements are small prizes users can get for using the app.
    /// like logging in 100 times.
    /// </summary>
    class Achievement
    {
        [Key]
        public int AchievementId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }

        public virtual ICollection<UserAccount> UserAccounts { get; set; }
    }
}
