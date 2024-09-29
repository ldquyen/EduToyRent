using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.BLL.DTOs.AccountDTO
{
    public class SignupAccountDTO
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Account name must contain only letters.")]
        public string AccountName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string AccountEmail { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Password should be minimum 6 characters")]
        public string AccountPassword { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "Phone number must be 10 characters.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; }
    }
}
