using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.BLL.DTOs.CartDTO
{
	public class GetCartRequest
	{
		public int toyId {  get; set; } 
		public int accountId { get; set; }
		public int quantity { get; set; }
	}
}
