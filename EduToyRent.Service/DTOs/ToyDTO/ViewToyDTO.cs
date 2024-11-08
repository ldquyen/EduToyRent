using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.DTOs.ToyDTO
{
    public class ViewToyForRentDTO
    {
        public int ToyId { get; set; }
        public string ToyName { get; set; }
        public string Description { get; set; }
        public decimal? RentPricePerDay { get; set; }
        public int Stock { get; set; }
        public string ImageUrl { get; set; }
    }

    public class ViewToyForSaleDTO
    {
        public int ToyId { get; set; }
        public string ToyName { get; set; }
        public string Description { get; set; }
        public decimal? BuyPrice { get; set; }
        public int Stock { get; set; }
        public string ImageUrl { get; set; }
    }

    public class ViewToyForSaleDetailDTO
    {
        public int ToyId { get; set; }
        public string ToyName { get; set; }
        public string Description { get; set; }
        public decimal? BuyPrice { get; set; }
        public int Stock { get; set; }
        public string SupplierName { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
    }
    public class ViewToyForRentDetailDTO
    {
        public int ToyId { get; set; }
        public string ToyName { get; set; }
        public string Description { get; set; }
        public decimal? RentPricePerDay { get; set; }
        public decimal? RentPricePerWeek { get; set; }
        public decimal? RentPricePerTwoWeeks { get; set; }
        public int Stock { get; set; }
        public string SupplierName { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
    }
    public class ViewToyForRentSupplier
    {
        public int ToyId { get; set; }
        public string ToyName { get; set; }
        public string Description { get; set; }
        public decimal? RentPricePerDay { get; set; }
        public decimal? RentPricePerWeek { get; set; }
        public decimal? RentPricePerTwoWeeks { get; set; }
        public int Stock { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
        public bool IsDelete { get; set; }
        public bool IsActive { get; set; }

    }
    public class ViewToyForSellSupplier
    {
        public int ToyId { get; set; }
        public string ToyName { get; set; }
        public string Description { get; set; }
        public decimal? BuyPrice { get; set; }
        public int Stock { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
        public bool IsDelete { get; set; }
        public bool IsActive { get; set; }

    }
}

