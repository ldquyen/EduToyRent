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
    public class VoucherRepository : Repository<Voucher> , IVoucherRepository
    {
        private readonly EduToyRentDbContext _context;
        public VoucherRepository(EduToyRentDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<decimal> UseVoucherReturnPrice(int voucherId, decimal price)
        {
            var voucher = await _context.Vouchers.FirstOrDefaultAsync(x => x.VoucherId == voucherId);
            if (voucher != null)
            {
                if(voucher.Used >= voucher.Quantity || DateTime.Now > voucher.ExpiredDate || !voucher.IsActive) return price;
                else
                {
                    price *= (1 - (decimal)voucher.Discount / 100);
                    voucher.Used++;
                    await _context.SaveChangesAsync();
                }
            }
            return price;
        }
    }
}
