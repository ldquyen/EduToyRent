using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.DAL.Entities
{
    public class DepositOrder
    {
        [Key]
        public int DepositOrderId { get; set; }
        public int OrderId { get; set; }
        public decimal TotalMoney { get; set; }
        public decimal RefundMoney { get; set; }
        public string BankCode { get; set; }
        public string BankName { get; set; }
        public DateTime DepositDate { get; set; }
        public DateTime? RefundDate { get; set; }
        public int StatusOrder { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
    }
}
