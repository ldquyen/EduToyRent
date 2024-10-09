using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.DTOs.ToyDTO
{
    public class CreateSaleToyDTO
    {
        [Required]
        public string ToyName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int CategoryId { get; set; }
        //public bool IsRental { get; set; }
        [Required]
        public decimal? BuyPrice { get; set; }
        //public decimal? RentPricePerDay { get; set; }
        //public decimal? RentPricePerWeek { get; set; }
        //public decimal? RentPricePerTwoWeeks { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public IFormFile ImageFile { get; set; }
    }
}
