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
        public async Task<int> GetCountAsync()
        {
            return await _context.Toys.CountAsync();
        }
        public async Task<IEnumerable<Toy>> GetAllAsync(int pageIndex, int pageSize)
        {
            return await _context.Toys
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<int> GetCountByName(string keyword)
        {
            return await _context.Toys.CountAsync(x => x.ToyName.Contains(keyword));
        }

        public async Task<IEnumerable<Toy>> SearchByName(string keyword, int pageIndex, int pageSize)
        {
            return await _context.Toys
                .Where(x => x.ToyName.Contains(keyword))
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<IEnumerable<Toy>> SortToy(string sortBy, int pageIndex, int pageSize)
        {
            IQueryable<Toy> toys = _context.Toys;
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
                default:
                    break;
            }
            return await toys
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<bool> CheckSameTypeOfToy(List<int> toyIds, bool isRent)
        {
            if (toyIds == null || !toyIds.Any()) return false;
            var toys = await _context.Toys.Where(x => toyIds.Contains(x.ToyId)).ToListAsync();
            if (toys.Count == 0) return false;
            bool firstToy = toys.First().IsRental;
            if(firstToy != isRent) return false;
            return toys.All(x => x.IsRental == firstToy);
        }
    }
}
