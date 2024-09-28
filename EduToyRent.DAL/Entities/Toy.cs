using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.DAL.Entities
{
    [Table("Toy")]
    public class Toy
    {
        [Key]
        public int ToyId { get; set; }
        public string ToyName { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public bool IsRental { get; set; }
        public decimal? BuyPrice { get; set; }
        public decimal? RentPricePerDay { get; set; }
        public decimal? RentPricePerWeek { get; set; }
        public decimal? RentPricePerTwoWeeks { get; set; }
        public int Stock { get; set; }
        public int SupplierId { get; set; }
        public string ImageToy { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [ForeignKey("SupplierId")]
        public virtual Account Supplier { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
