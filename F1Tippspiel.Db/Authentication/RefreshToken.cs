using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1Tippspiel.Db.Authentication
{
    /// <summary>
    /// Tokens for keeping a Client logged in for as long the token is valid
    /// </summary>
    public class RefreshToken
    {
        [Key]
        public string Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Subject { get; set; }
        [Required]
        [MaxLength(50)]
        public string ClientId { get; set; }
        /// <summary>
        /// This Value is just for human information, it is not used
        /// </summary>
        public DateTime IssuedUtc { get; set; }
        /// <summary>
        /// This Value is just for human information, it is not used
        /// </summary>
        public DateTime ExpiresUtc { get; set; }
        [Required]
        public string ProtectedTicket { get; set; }
    }
}
