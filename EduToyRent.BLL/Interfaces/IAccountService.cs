﻿using EduToyRent.BLL.DTOs.AccountDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.BLL.Interfaces
{
    public interface IAccountService
    {
        Task<dynamic> SignUpAccount(SignupAccountDTO signupAccountDTO);
    }
}
