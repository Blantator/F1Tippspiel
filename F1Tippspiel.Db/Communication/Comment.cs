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
    /// Represents a comment created by a user 
    /// Comments are allowed at different places.
    /// </summary>
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        public string Message { get; set; }

        public virtual UserAccount Author { get; set; }

        public DateTime DateTime { get; set; }
    }
}
