using EduToyRent.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.Exceptions
{
    public static class AccountError
    {
        public static Error Cannotban => new("Account", "This account can not be banned!!");
        public static Error CannotCreate => new("Account", "Can not create new account.");
    }

}
