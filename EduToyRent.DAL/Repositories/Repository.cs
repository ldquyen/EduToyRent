using EduToyRent.DAL.Context;
using EduToyRent.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly EduToyRentDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(EduToyRentDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            //await _context.SaveChangesAsync();
        }
        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            //await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            //await _context.SaveChangesAsync();
        }
    }
}
