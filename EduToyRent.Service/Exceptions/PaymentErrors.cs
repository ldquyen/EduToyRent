using EduToyRent.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.Exceptions
{
    public class PaymentErrors
    {
        public static Error PaymentError => new("Error payment", "Can not create payment for this order");
        public static Error WrongPayment => new("Null order", "Order cannot payment, try other api");
    }
}
