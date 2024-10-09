using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.DAL.Entities.Responses
{
	public class GetCartResponse
	{

		public int CartId { get; set; }
		public int AccountId { get; set; }
		public List<CartItem> CartItems { get; set; } = new List<CartItem>();
	}
}
