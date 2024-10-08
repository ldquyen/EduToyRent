using EduToyRent.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.DTOs.AccountDTO
{
    public class AccountDTO
    {
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public string AccountEmail { get; set; }
        public string AccountPassword { get; set; }
        public int? RoleId { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsBan { get; set; }

        //public virtual Role Role { get; set; }
    }
}
