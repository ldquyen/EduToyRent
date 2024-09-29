using EduToyRent.DAL.Context;
using EduToyRent.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EduToyRentDbContext _context;
        public IAccountRepository AccountRepository { get; set; }
        public IRefreshTokenRepository RefreshTokenRepository { get; set; }



        public UnitOfWork(EduToyRentDbContext context)
        {
            _context = context;
            AccountRepository = new AccountRepository(_context);
            RefreshTokenRepository = new RefreshTokenRepository(_context);  
        }



        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
