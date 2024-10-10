using EduToyRent.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.Exceptions
{
    public class OrderErrors
    {
        public static Error OrderIsNull => new("Null order", "Order not found");
        public static Error ConfirmStatus => new("Confirm order", "Order status id must be 2 or 9");
        public static Error ShiperInfo => new("Shipper infomation", "Shipper infomation must not be null");

    }
}
