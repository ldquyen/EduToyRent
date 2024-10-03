using EduToyRent.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EduToyRent.DataAccess.Entities
{
    [Table("Report")]
    public class Report
    {
        [Key]
        public int ReportId { get; set; }
        public int ToyId { get; set; }
        public int ReportById { get; set; }
        public string ReportDetail { get; set; }
        public DateTime ReportDate { get; set; }
        [ForeignKey("ToyId")]
        public virtual Toy Toy { get; set; }

        [ForeignKey("ReportById")]
        public virtual Account Account { get; set; }
    }
}
