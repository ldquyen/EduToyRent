using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.DAL.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int AccountId { get; set; }
        public int StatusOrder { get; set; }
        public bool PaymentStatus { get; set; }
        public string ShippingAddress { get; set; }
        public decimal TotalMoney { get; set; }
        public int? Discount { get; set; }
        public decimal FinalMoney { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsRentalOrder { get; set; }

        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<DepositOrder> DepositOrders { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }

}
