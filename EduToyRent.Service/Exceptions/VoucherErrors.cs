using EduToyRent.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.Exceptions
{
    public class VoucherErrors
    {
        public static Error InvalidAccountIdAndVoucherId => new("Voucher", "Inputed account or voucher does not exist!!");
        public static Error InvalidVoucherId => new("Voucher", "Inputed voucher does not exist!!");
        public static Error InvalidFlag => new("Voucher", "Flag must be 1(active) or 0(deactive)");


    }
}
