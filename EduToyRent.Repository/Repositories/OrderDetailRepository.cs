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
    public class OrderDetailRepository : Repository<OrderDetail> , IOrderDetailRepository
    {
        private readonly EduToyRentDbContext _context;
        public OrderDetailRepository(EduToyRentDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<OrderDetail>> GetOrderForSupplier(int supplierId)
        {
            return await _context.OrderDetails.Where(x => x.Toy.SupplierId == supplierId).ToListAsync();
        }

        public async Task<decimal> GetTotalMoney(int orderId)
        {
            var orderDetails = await _context.OrderDetails.Where(od => od.OrderId == orderId).ToListAsync();
            if (orderDetails == null || orderDetails.Count == 0)
                return 0;
            decimal totalMoney = 0;
            foreach (var orderDetail in orderDetails)
            {
                if (orderDetail.IsRental)
                {
                    totalMoney += orderDetail.RentalPrice ?? 0; 
                }
                else
                {
                    totalMoney += orderDetail.Price ;
                }
            }
            return totalMoney;
        }

        public async Task<List<OrderDetail>> GetOrderDetailsByOrderId(int orderid)
        {
            return await _context.OrderDetails.Where(x => x.OrderId == orderid).ToListAsync();
        }

        public async Task<List<OrderDetail>> GetOrderRentDetailForSupplier(int supplierid)
        {
            return await _context.OrderDetails.Where(x => x.Toy.SupplierId == supplierid && x.IsRental && x.Order.PaymentStatus).OrderByDescending(x => x.Order.OrderDate).ToListAsync();
        }

        public async Task<List<OrderDetail>> GetOrderSaleDetailForSupplier(int supplierid)
        {
            return await _context.OrderDetails.Where(x => x.Toy.SupplierId == supplierid && !x.IsRental && x.Order.PaymentStatus).OrderByDescending(x => x.Order.OrderDate).ToListAsync();
        }

        public async Task<List<int>> GetOrderDetailIdByOrderId(int orderid)
        {
            return await _context.OrderDetails.Where(x => x.OrderId == orderid).Select(x => x.OrderDetailId).ToListAsync();
        }
    }
}
