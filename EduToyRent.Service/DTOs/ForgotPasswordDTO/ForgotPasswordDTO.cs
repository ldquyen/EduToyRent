using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.DTOs.ForgotPasswordDTO
{
	public class ForgotPasswordDto
	{
		[Required, EmailAddress, Display(Name = "email")]
		public string Email { get; set; }
	}
}
