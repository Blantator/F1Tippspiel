using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F1Tippspiel.Db.Authentication;

namespace F1Tippspiel.Db.Communication
{
    /// <summary>
    /// Represents a message sent from  user to a different user.
    /// Comparable to an E-Mail
    /// </summary>
    public class UserMessage
    {
        [Key]
        public int MessageId { get; set; }

        public string Message { get; set; }

        public virtual UserAccount Sender { get; set; }
        public virtual UserAccount Receiver { get; set; }

        public DateTime DateTime { get; set; }
    }
}
