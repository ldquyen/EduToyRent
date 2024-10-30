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
        public static Error InvalidVoucherQuantity => new("Voucher", "Voucher has been used all");
        public static Error VoucherDeativated => new("Voucher", "Voucher has been deactivate");
        public static Error UsedAllCantActive => new("Voucher", "Voucher has been used all can not be activated");
        public static Error CannotUseVoucher => new("Voucher", "Can not use this voucher");

    }
}
