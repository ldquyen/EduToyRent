using EduToyRent.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.Exceptions
{
    public class UpdateInfomationErrors
    {
        public static Error AccountIsNotExist => new("Account", "Your account is not exist!!");
    }
}
