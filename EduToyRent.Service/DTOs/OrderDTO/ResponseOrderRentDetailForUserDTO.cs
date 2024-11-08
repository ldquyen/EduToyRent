using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.DTOs.OrderDTO
{
    public class ResponseOrderRentDetailForUserDTO
    {
        public string AccountName { get; set; }
        public string ShippingAddress { get; set; }
        public string ReceivePhoneNumber { get; set; }
        public string StatusName { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsRentalOrder { get; set; }
        public decimal TotalMoney { get; set; }
        public int? Discount { get; set; }
        public decimal FinalMoney { get; set; }
        public bool PaymentStatus { get; set; }
        public List<ODRentDTO> OrdersDetail { get; set; }
    }


    public class ODRentDTO
    {
        public string ToyName { get; set; }
        public int Quantity { get; set; }
        public DateTime? RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal? RentalPrice { get; set; }
        public string? ShipperPhone { get; set; }
    }
}
