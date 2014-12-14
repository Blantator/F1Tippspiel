using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1Tippspiel.Db.Game
{
    /// <summary>
    /// Represents one result for one driver for a specific race or qualifying
    /// A full race or qualifying result contains several of this objects 
    /// </summary>
    class RaceResult
    {
        [Key]
        public int Id { get; set; }

        public virtual Race Race { get; set; }

        public virtual Driver Driver { get; set; }

        /// <summary>
        /// The Position achieved by the driver 
        /// </summary>
        public int Position { get; set; }
    }
}
