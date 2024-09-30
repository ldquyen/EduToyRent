﻿using EduToyRent.BLL.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.BLL.Exceptions
{
    public static class LoginErrors
    {
        public static Error AccountIsBan => new("Account", "Your account is banned!!");
        public static Error InvalidAccount => new("Null", "Wrong email or password");
    }
}
