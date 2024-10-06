using EduToyRent.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.DataAccess.Entities
{
    [Table("StatusOrder")]
    public class StatusOrder
    {
        [Key]
        public int StatusId { get; set; }       // 1: Cho xac nhan  2: Chuan bi gui hang (=> da xac nhan don hang r)
        public string StatusName { get; set; }  // 3: Dang giao hang    4: Da giao hang     5: Hoan hang    6: Tra hang    
        public virtual ICollection<Order> Orders { get; set; } //    7: Hoan tat lay hang   8: Hoan thanh   9: Huy hang
    }
}
