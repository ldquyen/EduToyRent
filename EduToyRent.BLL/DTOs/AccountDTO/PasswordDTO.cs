using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.BLL.DTOs.AccountDTO
{
    public class PasswordDTO
    {
        [Required(ErrorMessage = "Account password is required.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[\W_]).+$", ErrorMessage = "Account password must contain at least one uppercase letter and one special character.")]
        public string AccountPassword { get; set; }
    }
}