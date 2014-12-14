using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1Tippspiel.Db.Game
{
    /// <summary>
    /// Represents a f1-driver
    /// </summary>
    class Driver
    {
        [Key]
        public int DriverId { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }
    }
}
