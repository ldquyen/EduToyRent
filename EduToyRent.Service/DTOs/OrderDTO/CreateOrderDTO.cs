using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.DTOs.OrderDTO
{
    public class CreateOrderDTO
    {
        [Required]
        public string ShippingAddress { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "Phone number must be 10 characters.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string ReceivePhoneNumber { get; set; }
        [Required]
        public bool IsRentalOrder { get; set; }
    }
}

/*
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
        public string Shipper { get; set; }
        public string ShipperPhone { get; set; }
 */
