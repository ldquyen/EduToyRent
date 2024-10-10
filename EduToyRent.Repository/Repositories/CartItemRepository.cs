using EduToyRent.DAL.Context;
using EduToyRent.DAL.Entities;
using EduToyRent.DAL.Interfaces;
using EduToyRent.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.DAL.Repositories
{
    public class CartItemRepository : Repository<CartItem>, ICartItemRepository
    {
        private readonly EduToyRentDbContext _context;
        public CartItemRepository(EduToyRentDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<CartItem?> GetAsync(int toyId)
        {
            return await _context.CartItems.FirstOrDefaultAsync(c => c.ToyId == toyId);
        }

		public async Task<List<CartItem>?> GetByCartIdAsync(int cartId)
		{
			return await _context.CartItems.Where(c => c.CartId == cartId).ToListAsync();
		}

        public async Task<bool> DeleteAsync(List<int> itemIdList)
        {
			CartItem? existingItem;
			try
			{
				foreach (var item in itemIdList)
				{
					existingItem = await _context.CartItems.FirstOrDefaultAsync(c => c.CartItemId == item);
					if (existingItem != null) { _context.CartItems.Remove(existingItem); }
				}
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
            
        }
    }
}
