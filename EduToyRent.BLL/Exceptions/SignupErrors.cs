using EduToyRent.BLL.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.BLL.Exceptions
{
    public class SignupErrors
    {
        public static Error DuplicateEmail => new("Duplicate Email", "The email is exist!!");
        public static Error DuplicatePhone => new("Duplicate phone", "The phone is exist!!");
    }
}
