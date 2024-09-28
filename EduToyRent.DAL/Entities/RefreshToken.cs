using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.DAL.Entities
{
    [Table("RefreshToken")]
    public class RefreshToken
    {
        [Key]
        public Guid Id { get; set; }
        public string AccountId { get; set; }
        public string Token { get; set; }
        public string JwtID { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevoke { get; set; }
        public DateTime ExpiredAt { get; set; }
        public DateTime IssuedAt { get; set; }
        // Navigation property
        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }
    }
}
