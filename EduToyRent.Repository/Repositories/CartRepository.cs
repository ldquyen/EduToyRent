using EduToyRent.DAL.Context;
using EduToyRent.DAL.Entities;
using EduToyRent.DAL.Interfaces;
using EduToyRent.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Repository.Repositories
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        private readonly EduToyRentDbContext _context;
        public CartRepository(EduToyRentDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Cart>> GetByAccountIdAsync(int accountId)
        {
            return await _context.Carts.Where(c => c.AccountId == accountId).ToListAsync();
        }

        public async Task<Cart?> GetRentCart(int accountId)
        {
            return await _context.Carts.FirstOrDefaultAsync(c => c.AccountId == accountId && c.IsRental == true);
        }
        public async Task<Cart?> GetSaleCart(int accountId)
        {
            return await _context.Carts.FirstOrDefaultAsync(c => c.AccountId == accountId && c.IsRental == false);
        }

        public async Task AddCartAsync(Cart cart) 
        {
            var result = await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
        }
    }
}
