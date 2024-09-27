using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.DAL.Entities
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public int Status { get; set; }
        public string TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string BankCode { get; set; }
        public string ResponseCode { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }
    }
}
