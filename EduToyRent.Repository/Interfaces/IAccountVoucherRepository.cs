using EduToyRent.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Repository.Interfaces
{
    public interface IAccountVoucherRepository : IRepository<AccountVoucher>
    {
        Task<bool> CheckValidAccountVoucher(int voucherId, int accountId);
        Task UseVoucher(int voucherId, int accountId);
        Task<List<AccountVoucher>> GetVoucherForUser(int accountId);
    }
}
