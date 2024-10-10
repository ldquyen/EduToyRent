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
    }
}
