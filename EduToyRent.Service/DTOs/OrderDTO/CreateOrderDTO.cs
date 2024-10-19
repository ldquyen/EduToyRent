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
        [Required]
        public List<CartToyDTO> ToyList { get; set; }
        public DateTime? RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }

    public class CartToyDTO
    {
        public int ToyId { get; set; }
        public int Quantity { get; set; }
    }

    public class CreateRentOrderDetailDTO
    {
        [Required]
        public int OrderId { get; set; }
        [Required]
        public DateTime? RentalDate { get; set; }
        [Required]
        public DateTime? ReturnDate { get; set; }
    }
    public class CreateSaleOrderDetailDTO
    {
        [Required]
        public int OrderId { get; set; }
    }
}

