using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduToyRent.Service.Common;


namespace EduToyRent.Service.Exceptions
{
    public static class ChangePasswordErrors
    {
        public static Error WrongOldPassword => new("Change Password", "Your old password is wrong!!");
    }
}
