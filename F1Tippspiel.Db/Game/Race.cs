using System;
using System.ComponentModel.DataAnnotations;

namespace F1Tippspiel.Db.Game
{
    class Race
    {
        [Key]
        public int RaceId { get; set; }

        public string City { get; set; }
        public string Country { get; set; }

        public string Picture { get; set; }

        public DateTime Qualifying { get; set; }
        public DateTime RaceTime { get; set; }
    }
}