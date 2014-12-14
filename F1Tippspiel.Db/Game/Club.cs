using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1Tippspiel.Db.Game
{
    /// <summary>
    /// Represents a f1-racing-club
    /// </summary>
    class Club
    {
        [Key]
        public int ClubId { get; set; }

        public string Name { get; set; }

        public string Logo { get; set; }

        public virtual ICollection<Driver> Drivers { get; set; }
    }
}
