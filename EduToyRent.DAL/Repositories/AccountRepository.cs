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
    public class AccountRepository : Repository<Account> , IAccountRepository
    {
        private readonly EduToyRentDbContext _context;
        public AccountRepository(EduToyRentDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Account> LoginAsync(string email, string password)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.AccountEmail == email && a.AccountPassword == password);
        }

    }
}
