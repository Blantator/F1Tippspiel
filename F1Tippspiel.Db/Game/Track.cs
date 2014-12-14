using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace F1Tippspiel.Db.Game
{
    /// <summary>
    /// Represents a track on which a race can take place
    /// </summary>
    class Track
    {
        [Key]
        public int TrackId { get; set; }

        public string Name { get; set; }

        public string Picture { get; set; }

        public virtual Race Race { get; set; }
    }
}
