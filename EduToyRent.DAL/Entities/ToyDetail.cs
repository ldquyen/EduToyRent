using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.DAL.Entities
{
    public class ToyDetail
    {
        [Key]
        public int ToyDetailId { get; set; }
        public int ToyId { get; set; }
        public bool IsRental { get; set; }
        public decimal? BuyPrice { get; set; }
        public decimal? RentPricePerDay { get; set; }
        public decimal? RentPricePerWeek { get; set; }
        public decimal? RentPricePerTwoWeeks { get; set; }
        public int Stock { get; set; }

        [ForeignKey("ToyId")]
        public virtual Toy Toy { get; set; }
    }
}
