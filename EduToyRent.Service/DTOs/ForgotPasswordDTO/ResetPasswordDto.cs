using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.DTOs.ForgotPasswordDTO
{
	public class ResetPasswordDto
	{
		[Required, EmailAddress, Display(Name = "email")]
		public string Email { get; set; }

		[Required, NotNull]
		public string OTP { get; set; }

		[Required(ErrorMessage = "Old password is required.")]
		[RegularExpression(@"^(?=.*[A-Z])(?=.*[\W_]).+$", ErrorMessage = "Account password must contain at least one uppercase letter and one special character.")]
		[MinLength(6, ErrorMessage = "Password should be minimum 6 characters")]
		public string NewPassword { get; set; }
	}
}
