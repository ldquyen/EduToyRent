using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.DTOs.OrderDTO
{
    public class ResponseOrderForSupplierDTO
    {
    }

    public class ReponseOrderSaleForSupplierDTO
    {
        public int OrderDetailId { get; set; }
        public int ToyId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool IsRental { get; set; }
    }

    public class ReponseOrderRentForSupplierDTO
    {
        public int OrderDetailId { get; set; }
        public int ToyId { get; set; }
        public int Quantity { get; set; }
        public DateTime? RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal? RentalPrice { get; set; }
        public bool IsRental { get; set; }
    }
}
