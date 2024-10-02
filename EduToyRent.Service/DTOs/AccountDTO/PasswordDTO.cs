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
        [Required]
        [MinLength(6, ErrorMessage = "Password should be minimum 6 characters")]
        public string password;
    }
}
