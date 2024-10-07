using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.DTOs.ToyDTO
{
    public class ResponseSaleToyDTO
    {
        public int ToyId { get; set; }
        public string ToyName { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public bool IsRental { get; set; }
        public decimal? BuyPrice { get; set; }
        public int Stock { get; set; }
        public string SupplierName { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
