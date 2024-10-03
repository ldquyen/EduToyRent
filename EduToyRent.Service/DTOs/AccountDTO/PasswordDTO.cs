using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.DTOs.AccountDTO
{
    public class PasswordDTO
    {
        [Required(ErrorMessage = "Account password is required.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[\W_]).+$", ErrorMessage = "Account password must contain at least one uppercase letter and one special character.")]
        [MinLength(6, ErrorMessage = "Password should be minimum 6 characters")]
        public string AccountPassword { get; set; }

    }
}
