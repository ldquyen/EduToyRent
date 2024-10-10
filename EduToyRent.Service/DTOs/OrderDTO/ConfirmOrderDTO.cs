using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.DTOs.OrderDTO
{
    public class ConfirmOrderDTO
    {
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int StatusId { get; set; }
        public string? Shipper { get; set; }
        public string? ShipperPhone { get; set; }
    }
}

/*
     [Key]
    public int OrderId { get; set; }
    public int AccountId { get; set; }
    public int StatusId { get; set; }
    public bool PaymentStatus { get; set; }
    public string ShippingAddress { get; set; }
    public decimal TotalMoney { get; set; }
    public int? Discount { get; set; }
    public decimal FinalMoney { get; set; }
    public DateTime OrderDate { get; set; }
    public bool IsRentalOrder { get; set; }
    public string ReceivePhoneNumber { get; set; }
    public string? Shipper { get; set; }
    public string? ShipperPhone { get; set; }

    [ForeignKey("AccountId")]
    public virtual Account Account { get; set; }
    [ForeignKey("StatusId")]
    public virtual StatusOrder StatusOrder { get; set; }
    public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    public virtual DepositOrder DepositOrders { get; set; }
    public virtual ICollection<Payment> Payments { get; set; }

}
 */