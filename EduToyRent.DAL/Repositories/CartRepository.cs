using EduToyRent.DAL.Context;
using EduToyRent.DAL.Entities;
using EduToyRent.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.DAL.Repositories
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        private readonly EduToyRentDbContext _context;
        public CartRepository(EduToyRentDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Cart> GetByAccountIdAsync(int accountId)
        {
            return await _context.Carts.FirstOrDefaultAsync(c => c.AccountId == accountId);
        }

        public async Task<Cart> AddCartAsync(Cart cart) 
        {
            var result = await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
            return cart;
        }
    }
}
