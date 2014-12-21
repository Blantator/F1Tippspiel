using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F1Tippspiel.Db.Account;

namespace F1Tippspiel.Db.Game
{
    /// <summary>
    /// Represents a whole f1-season 
    /// Including all races, tracks, clubs, drivers and players
    /// </summary>
    public class Season
    {
        [Key]
        public int SeasonId { get; set; }

        public int Year { get; set; }

        public virtual ICollection<UserAccount> Players { get; set; }
        public virtual ICollection<Club> Clubs { get; set; }
        public virtual ICollection<Track> Tracks{ get; set; }
    }
}
