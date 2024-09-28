using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.DAL.Entities
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ToyId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool IsRental { get; set; }
        public DateTime? RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal? RentalPrice { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        [ForeignKey("ToyId")]
        public virtual Toy Toy { get; set; }
        public virtual ICollection<ShipDate> ShipDates { get; set; }
    }
}
