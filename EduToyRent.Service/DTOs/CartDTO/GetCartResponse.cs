using EduToyRent.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.DTOs.CartDTO
{
	public class GetCartResponse
	{
        public int CartItemId { get; set; }
        public int ToyId { get; set; }
        public int Quantity { get; set; }
    }
}
