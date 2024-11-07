using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.DTOs.VoucherDTO
{
    public class VoucherForAccountDTO
    {
        public int VoucherId { get; set; }
        public bool IsUsed { get; set; }
        public string VoucherName { get; set; }
        public DateTime ExpiredDate { get; set; }
        public float Discount { get; set; }
        public bool IsActive { get; set; }
    }
}
