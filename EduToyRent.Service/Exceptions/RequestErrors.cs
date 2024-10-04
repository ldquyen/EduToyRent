using EduToyRent.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.Exceptions
{
    public class RequestErrors
    {
        public static Error DenyReason => new("Deny reason null", "The deny reason is not null.");
    }
}
