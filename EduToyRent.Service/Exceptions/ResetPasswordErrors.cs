using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduToyRent.Service.Common;

namespace EduToyRent.Service.Exceptions
{
    public static class ResetPasswordErrors
    {
		public static Error EmailNotFound => new("Email not found", "Email not found.");

		public static Error IncorrectOTP => new("Incorrect OTP", "Incorrect OTP");

		public static Error OTPExpired => new("OTP Expired", "OTP Expired");
    }
}
