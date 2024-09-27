using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.DAL.Entities
{
    public class Voucher
    {
        [Key]
        public int VoucherId { get; set; }
        public string VoucherName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpiredDate { get; set; }
        public float Discount { get; set; }
        public int Quantity { get; set; }
        public int Used { get; set; }
        public bool IsActive { get; set; }  
        public virtual ICollection<AccountVoucher> AccountVouchers { get; set; }
    }
}
