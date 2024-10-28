using EduToyRent.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Repository.Interfaces
{
    public interface IVoucherRepository : IRepository<Voucher>
    {
        Task<decimal> UseVoucherReturnPrice(int voucherId, decimal price);
    }
}
