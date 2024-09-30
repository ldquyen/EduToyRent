using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.BLL.DTOs.ToyDTO
{
    public class CreateRentalToyDTO
    {
        [Required]
        public string ToyName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int CategoryId { get; set; }
        //public bool IsRental { get; set; }
        //public decimal? BuyPrice { get; set; }
        [Required]
        public decimal? RentPricePerDay { get; set; }
        [Required]
        public decimal? RentPricePerWeek { get; set; }
        [Required]
        public decimal? RentPricePerTwoWeeks { get; set; }
        [Required]
        public int Stock { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}

