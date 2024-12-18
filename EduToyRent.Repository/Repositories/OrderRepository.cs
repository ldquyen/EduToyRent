﻿using EduToyRent.DAL.Context;
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
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly EduToyRentDbContext _context;
        public OrderRepository(EduToyRentDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<OrderDetail>> GetByOrderIdAsync(int orderId)
        {
            return await _context.OrderDetails
                                 .Where(od => od.OrderId == orderId)
                                 .ToListAsync();
        }
        public async Task AddOrderDetailAsync(OrderDetail orderDetail)
        {
            await _context.OrderDetails.AddAsync(orderDetail);
            await _context.SaveChangesAsync();  
        }

        public async Task<bool> UpdateOrderStatus(int orderId, int statusId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.OrderId == orderId);
            order.StatusId = statusId;
            if(statusId == 2) order.PaymentStatus = true;
            _context.Update(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Order>> GetOrderRentForUser(int accountId, int status)
        {
            return await _context.Orders.Where(x => x.AccountId == accountId && x.IsRentalOrder && x.StatusId == status).Include(x => x.StatusOrder).OrderByDescending(x => x.OrderDate).ToListAsync();
        }
        public async Task<List<Order>> GetOrderSaleForUser(int accountId, int status)
        {
            return await _context.Orders.Where(x => x.AccountId == accountId && !x.IsRentalOrder && x.StatusId == status).Include(x => x.StatusOrder).OrderByDescending(x => x.OrderDate).ToListAsync();
        }
    }
}
