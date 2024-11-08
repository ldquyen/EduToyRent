using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.DataAccess.Entities
{
	[Table("ResetPasswordOTP")]
	public class ResetPasswordOTP
    {
		[Key]
		public int id;

		public string Email { get; set; }
		public string OTP { get; set; }
		public DateTime expires {  get; set; }
    }
}
