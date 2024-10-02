using EduToyRent.Service.DTOs.AccountDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.Interfaces
{
    public interface IAccountService
    {
        Task<dynamic> SignUpAccount(SignupAccountDTO signupAccountDTO);
        Task<dynamic> ChangePassword(PasswordDTO password, CurrentUserObject currentUserObject);
        Task<dynamic> UpdateProfile(EditAccountProfileDTO editAccountProfileDTO, CurrentUserObject currentUserObject);
        Task<dynamic> GetProfile(CurrentUserObject currentUserObject);
    }
}
