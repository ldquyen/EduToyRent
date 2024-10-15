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
        public List<int> ToyList { get; set; }
        
    }
}

