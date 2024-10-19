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
        public static Error NotExistToy => new("Not exist toy", "Toys in the current list are not available.");
        public static Error RentalDateToyNull => new("Null date", "Fill full date");
        public static Error ToyListNull => new("Null list", "Toy is null");
        public static Error CannotCreateToyRentOrder => new("Order toy fail", "Cannot create rent order");
        public static Error CannotCreateToySaleOrder => new("Order toy fail", "Cannot create sale order");
        public static Error CannotCreateToyOrder => new("Order toy fail", "Cannot create order");
    }
}
