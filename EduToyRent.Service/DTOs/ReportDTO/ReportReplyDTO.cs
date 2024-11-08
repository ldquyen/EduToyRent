using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.DTOs.ReportDTO
{
    public class ReportReplyDTO
    {
        public int MessageId { get; set; }
        public int ReportId { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string MessageContent { get; set; }
        public DateTime SentAt { get; set; }
    }
}
