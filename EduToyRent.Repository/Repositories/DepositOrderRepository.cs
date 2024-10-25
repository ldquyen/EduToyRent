using EduToyRent.DAL.Context;
using EduToyRent.DAL.Entities;
using EduToyRent.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Repository.Repositories
{
    public class DepositOrderRepository : Repository<DepositOrder>, IDepositOrderRepository
    {
        private readonly EduToyRentDbContext _context;
        public DepositOrderRepository(EduToyRentDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task CreateDepositOrder(Order order, string bankcode, string bankname)
        {
            DepositOrder depositOrder = new DepositOrder();
            depositOrder.OrderId = order.OrderId;
            depositOrder.TotalMoney = (order.FinalMoney / 2m);
            depositOrder.RefundMoney = 0;
            depositOrder.BankCode = bankcode;
            depositOrder.BankName = bankname;
            depositOrder.DepositDate = order.OrderDate;
            depositOrder.StatusOrder = 1;
            await _context.AddAsync(depositOrder);
            await _context.SaveChangesAsync();
        }
    }
}
