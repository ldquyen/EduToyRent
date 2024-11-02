using EduToyRent.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.Exceptions
{
    public class ReviewErrors
    {
        public static Error InvalidToyId => new("Review", "Inputed id does not exist!!");
    }
}
