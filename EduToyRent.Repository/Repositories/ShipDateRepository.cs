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
    public class ShipDateRepository : Repository<ShipDate>, IShipDateRepository
    {
        private readonly EduToyRentDbContext _context;
        public ShipDateRepository(EduToyRentDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task CreateShipDate(OrderDetail orderDetail)
        {
            ShipDate shipDate = new ShipDate()
            {
                OrderDetailId = orderDetail.OrderDetailId,
                DeliveryDate = DateTime.Now,
                ReturnDate = orderDetail.ReturnDate,
                ShipStatus = 1 //lay hang tu supplier
            };
            await _context.ShipDates.AddAsync(shipDate);
            await _context.SaveChangesAsync();
        }
    }
}
