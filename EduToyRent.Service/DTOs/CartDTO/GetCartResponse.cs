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
		public int CartId { get; set; }
		public int AccountId { get; set; }
		public List<CartItem> CartItems { get; set; } = new List<CartItem>();
	}
}
