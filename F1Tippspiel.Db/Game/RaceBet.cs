using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1Tippspiel.Db.Game
{
    public class RaceBet
    {
        [Key]
        public int RaceBetId { get; set; }
    }
}
