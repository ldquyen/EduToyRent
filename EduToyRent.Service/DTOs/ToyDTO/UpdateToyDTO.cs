using System.ComponentModel.DataAnnotations;

namespace EduToyRent.Service.DTOs.ToyDTO
{
    public class UpdateToyDTO
    {
        [Required]
        public int ToyId { get; set; }
        public string ToyName { get; set; }
        public string Description { get; set; }
        public decimal? BuyPrice { get; set; }
        public decimal? RentPricePerDay { get; set; }
        public decimal? RentPricePerWeek { get; set; }
        public decimal? RentPricePerTwoWeeks { get; set; }
        public int Stock { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
    }
}
