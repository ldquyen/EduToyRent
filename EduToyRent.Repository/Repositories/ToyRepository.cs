using EduToyRent.DAL.Context;
using EduToyRent.DAL.Entities;
using EduToyRent.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EduToyRent.Repository.Repositories
{
    public class ToyRepository : Repository<Toy>, IToyRepository
    {
        private readonly EduToyRentDbContext _context;
        public ToyRepository(EduToyRentDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Toy> GetToyById(int toyId)
        {
            return await _context.Toys
                .Include(t => t.Category)
                .Include(t => t.Supplier)
                .FirstOrDefaultAsync(t => t.ToyId == toyId);
        }

        public async Task<bool> UpdateToy(Toy toy)
        {
            _context.Toys.Update(toy);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<int> GetRentCount()
        {
            return await _context.Toys.CountAsync(t => t.IsRental);
        }

        public async Task<int> GetSaleCount()
        {
            return await _context.Toys.CountAsync(t => !t.IsRental);
        }

        public async Task<IEnumerable<Toy>> ViewToysForRent(int pageIndex, int pageSize)
        {
            return await _context.Toys
                .Where(t => t.IsRental)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Toy>> ViewToysForSale(int pageIndex, int pageSize)
        {
            return await _context.Toys
                .Where(t => !t.IsRental)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<int> GetCountByToyName(string keyword, bool isRental)
        {
            return await _context.Toys.CountAsync(x => x.ToyName.Contains(keyword) && x.IsRental == isRental && !x.IsDelete);
        }

        public async Task<IEnumerable<Toy>> SearchToysByName(string keyword, bool isRental, int pageIndex, int pageSize)
        {
            pageIndex = pageIndex < 1 ? 1 : pageIndex;

            return await _context.Toys
                .Where(x => x.ToyName.Contains(keyword) && x.IsRental == isRental && !x.IsDelete)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Toy>> SortToysForRent(string sortBy, int pageIndex, int pageSize)
        {
            IQueryable<Toy> toys = _context.Toys.Where(t => t.IsRental);
            switch (sortBy)
            {
                case "price_asc":
                    toys = toys.OrderBy(x => x.RentPricePerDay);
                    break;
                case "price_desc":
                    toys = toys.OrderByDescending(x => x.RentPricePerDay);
                    break;
                case "name_asc":
                    toys = toys.OrderBy(x => x.ToyName);
                    break;
                case "name_desc":
                    toys = toys.OrderByDescending(x => x.ToyName);
                    break;
            }
            return await toys
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Toy>> SortToysForSale(string sortBy, int pageIndex, int pageSize)
        {
            IQueryable<Toy> toys = _context.Toys.Where(t => !t.IsRental);
            switch (sortBy)
            {
                case "price_asc":
                    toys = toys.OrderBy(x => x.BuyPrice);
                    break;
                case "price_desc":
                    toys = toys.OrderByDescending(x => x.BuyPrice);
                    break;
                case "name_asc":
                    toys = toys.OrderBy(x => x.ToyName);
                    break;
                case "name_desc":
                    toys = toys.OrderByDescending(x => x.ToyName);
                    break;
            }
            return await toys
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
