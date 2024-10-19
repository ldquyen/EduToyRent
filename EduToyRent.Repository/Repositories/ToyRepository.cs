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

        public async Task<bool> CheckSameTypeOfToy(List<int> toyIds, bool isRent)
        {
            if (toyIds == null || !toyIds.Any()) return false;
            var toys = await _context.Toys.Where(x => toyIds.Contains(x.ToyId)).ToListAsync();
            if (toys.Count == 0) return false;
            bool firstToy = toys.First().IsRental;
            if (firstToy != isRent) return false;
            return toys.All(x => x.IsRental == firstToy);
        }

        public async Task<bool> CheckExistToy(List<int> toyIds)
        {
            var toys = await _context.Toys.ToListAsync();
            var hasInvalidToys = toys.Any(toy => toyIds.Contains(toy.ToyId) && (!toy.IsActive || toy.IsDelete || toy.Stock <= 0));
            return !hasInvalidToys;

        }

        public async Task<decimal> GetMoneySaleByToyId(int toyId, int quantity)
        {
            Toy toy = await _context.Toys.FirstOrDefaultAsync(x => x.ToyId == toyId);
            decimal totalSale = 0;
            totalSale = (toy.BuyPrice ?? 0) * quantity;
            return totalSale;
        }

        public async Task<decimal> GetMoneyRentByToyId(int toyId, int quantity, DateTime? rentalDate, DateTime? returnDate)
        {
            Toy toy = await _context.Toys.FirstOrDefaultAsync(x => x.ToyId == toyId);
            int totalDays = (returnDate.Value - rentalDate.Value).Days;
            decimal totalRent = 0;

            if (totalDays < 7) 
            {
                totalRent = totalDays * (toy.RentPricePerDay ?? 0); // duoi 7
            }else if(totalDays == 7)
            {
                totalRent = (toy.RentPricePerWeek ?? 0); // bang 7
            }
            else if (totalDays < 14) //duoi 2 tuan
            {
                totalRent = (toy.RentPricePerWeek ?? 0) + ((totalDays - 7) * (toy.RentPricePerDay ?? 0));
            }
            else if(totalDays == 14) // bang 2 tuan
            {
                totalRent = (toy.RentPricePerTwoWeeks ?? 0);
            }
            else // hon 2 tuan
            {
                totalRent = (toy.RentPricePerTwoWeeks ?? 0) + ((totalDays - 14) * (toy.RentPricePerDay ?? 0));
            }
            return totalRent * quantity;
        }
    }
}
