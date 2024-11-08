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
    public class ShipDateRepository : Repository<ShipDate>, IShipDateRepository
    {
        private readonly EduToyRentDbContext _context;
        public ShipDateRepository(EduToyRentDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CreateShipDate(OrderDetail orderDetail, string? Shipper, string? ShipperPhone)
        {
            ShipDate shipDate = new ShipDate()
            {
                OrderDetailId = orderDetail.OrderDetailId,
                DeliveryDate = DateTime.Now,
                ReturnDate = orderDetail.ReturnDate,
                ShipStatus = 1, //lay hang tu supplier
                Shipper = Shipper,
                ShipperPhone  = ShipperPhone
            };
            await _context.ShipDates.AddAsync(shipDate);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CheckShip(int orderDetailId)
        {
            return await _context.ShipDates.AnyAsync(x => x.OrderDetailId == orderDetailId);   
        }

        public async Task<bool> CheckAllShip(List<int> orderDetailIds)
        {
            foreach (int id in orderDetailIds)
            {
                var sd = await _context.ShipDates.FirstOrDefaultAsync(x => x.OrderDetailId == id);
                if (sd == null) return false;
            }
            return true;
        }
    }
}
