using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.DTOs.ToyDTO
{
    public class ViewToyDTO
    {
        public int ToyId { get; set; }
        public string ToyName { get; set; }
        public string Description { get; set; }
        public decimal? RentPricePerDay { get; set; }
        public string ImageUrl { get; set; }
    }
    public class ViewToyDetailDTO : ViewToyDTO
    {
        public string SupplierName { get; set; }
        public string CategoryName { get; set; }
    }
}
