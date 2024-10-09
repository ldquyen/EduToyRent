using EduToyRent.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.Exceptions
{
    public class ToyErrors
    {
        public static Error ToyIsNull => new("Null toy", "Toy not found");
        public static Error NotSameToy => new("Not same toy", "The toy in list is not same type with Order");
    }
}
