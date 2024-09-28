using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.DAL.Entities
{
    [Table("AccountVoucher")]
    public class AccountVoucher
    {
        public int AccountId { get; set; }
        public int VoucherId { get; set; }
        public bool IsUsed { get; set; }

        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }

        [ForeignKey("VoucherId")]
        public virtual Voucher Voucher { get; set; }
    }
}
