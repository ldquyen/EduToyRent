using EduToyRent.DAL.Context;
using EduToyRent.DAL.Entities;
using EduToyRent.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Repository.Repositories
{
    public class AccountVoucherRepository : Repository<AccountVoucher>, IAccountVoucherRepository
    {
        private readonly EduToyRentDbContext _context;
        public AccountVoucherRepository(EduToyRentDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CheckValidAccountVoucher(int voucherId, int accountId)
        {
            var accountVoucher = await _context.AccountVouchers.Where(x => x.VoucherId == voucherId && x.AccountId == accountId).Include(x => x.Voucher).FirstOrDefaultAsync();
            if (accountVoucher == null || accountVoucher.IsUsed || !accountVoucher.Voucher.IsActive || DateTime.Now > accountVoucher.Voucher.ExpiredDate)
                return false;
            return accountVoucher.Voucher.Used < accountVoucher.Voucher.Quantity;
        }

        public async Task UseVoucher(int voucherId, int accountId)
        {
            var accountVoucher = await _context.AccountVouchers.Where(x => x.VoucherId == voucherId && x.AccountId == accountId).FirstOrDefaultAsync();
            accountVoucher.IsUsed = true;
            await _context.SaveChangesAsync();
        }

        public async Task<List<AccountVoucher>> GetVoucherForUser(int accountId)
        {
            var accountVouchers = await _context.AccountVouchers.Where(x => x.AccountId == accountId && !x.IsUsed
                    && x.Voucher.IsActive && DateTime.Now <= x.Voucher.ExpiredDate).Include(x => x.Voucher).ToListAsync();
            return accountVouchers;
        }
    }
}
