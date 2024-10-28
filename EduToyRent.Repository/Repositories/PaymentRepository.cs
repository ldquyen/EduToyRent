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
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        private readonly EduToyRentDbContext _context;
        public PaymentRepository(EduToyRentDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task UpdatePayment(Payment payment)
        {
            var pay = await _context.Payments.FirstOrDefaultAsync(x => x.TransactionId == payment.TransactionId);
            if (pay == null)
            {
                await _context.Payments.AddAsync(payment);
                await _context.SaveChangesAsync();
            }
            else
            {
                pay.Status = payment.Status;
                _context.Payments.Update(pay);
                await _context.SaveChangesAsync();
            }
            
        }

    }
}
