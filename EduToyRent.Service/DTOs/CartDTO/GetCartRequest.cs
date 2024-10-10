using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.DTOs.CartDTO
{
	public class GetCartRequest
	{
		[Required]
		public int toyId { get; set; }
		[Required]
		[Range(1, int.MaxValue)]
		public int quantity { get; set; }
	}
}
