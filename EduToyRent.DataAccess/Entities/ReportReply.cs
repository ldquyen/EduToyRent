using EduToyRent.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.DataAccess.Entities
{
    [Table("ReportReply")]
    public class ReportReply
    {
        [Key]
        public int MessageId { get; set; }

        public int ReportId { get; set; }

        [Required]
        public int SenderId { get; set; }

        [Required]
        public int ReceiverId { get; set; }

        [Required]
        [MaxLength(3000)]
        public string MessageContent { get; set; }

        public DateTime SentAt { get; set; } = DateTime.UtcNow;
        [ForeignKey("ReportId")]
        public virtual Report Report { get; set; }
        [ForeignKey("SenderId")]
        public virtual Account Sender { get; set; }

        [ForeignKey("ReceiverId")]
        public virtual Account Receiver { get; set; }
    }
}
