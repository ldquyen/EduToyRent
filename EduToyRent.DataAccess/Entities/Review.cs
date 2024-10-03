using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.DAL.Entities
{
    [Table("Review")]
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public int ToyId { get; set; }
        public int AccountId { get; set; }
        public float Rating { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("ToyId")]
        public virtual Toy Toy { get; set; }

        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }
    }
}
