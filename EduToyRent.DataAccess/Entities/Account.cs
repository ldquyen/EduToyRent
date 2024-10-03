using EduToyRent.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.DAL.Entities
{
    [Table("Account")]
    public class Account
    {
        [Key]
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public string AccountEmail { get; set; }
        public string AccountPassword { get; set; }
        public int? RoleId { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsBan { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual ICollection<Toy> Toys { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<AccountVoucher> AccountVouchers { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
        public virtual ICollection<RequestForm> RequestForms { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
       // test git hub
    }
}
