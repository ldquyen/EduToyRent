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
    public class RefreshTokenRepository : Repository<RefreshToken> , IRefreshTokenRepository
    {
        private readonly EduToyRentDbContext _context;
        public RefreshTokenRepository(EduToyRentDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddRefreshTokenAsync(string tokenId, int accountId, string refreshToken)
        {
            RefreshToken rfToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                JwtID = tokenId,
                AccountId = accountId,
                Token = refreshToken,
                IsUsed = false,
                IsRevoke = false,
                IssuedAt = DateTime.UtcNow,
                ExpiredAt = DateTime.UtcNow.AddMinutes(30)
            };
            await _context.RefreshTokens.AddAsync(rfToken);
            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken> CheckRefreshTokenAsync(string refreshToken)
        {
            return await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == refreshToken);
        }

        public async Task UpdateRefreshTokenAsync(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Update(refreshToken);
            await _context.SaveChangesAsync();
        }
    }
}
