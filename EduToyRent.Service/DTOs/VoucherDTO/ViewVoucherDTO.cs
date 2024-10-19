using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.DTOs.VoucherDTO
{
    public class ViewVoucherDTO
    {
        public int VoucherId { get; set; }
        public string VoucherName { get; set; }
        public DateTime ExpiredDate { get; set; }
        public float Discount { get; set; }
        public int Quantity { get; set; }
        public int Used { get; set; }
        public bool IsActive { get; set; }
    }
}
