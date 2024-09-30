using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.DAL.Entities
{
    [Table("RequestForm")]
    public class RequestForm
    {
        [Key]
        public int RequestId { get; set; }
        public int ToyId { get; set; }
        public int? ProcessedById { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? ResponseDate { get; set; }
        public int RequestStatus { get; set; }
        public bool ForRent { get; set; } 
        public string? DenyReason {  get; set; }

        [ForeignKey("ToyId")]
        public virtual Toy Toy { get; set; }

        [ForeignKey("ProcessedById")]
        public virtual Account Account { get; set; }
    }
}
