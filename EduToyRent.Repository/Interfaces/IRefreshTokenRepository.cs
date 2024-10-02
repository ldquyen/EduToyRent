using EduToyRent.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Repository.Interfaces
{
    public interface IRefreshTokenRepository 
    {
        Task AddRefreshTokenAsync(string tokenId, int userId, string refreshToken);
        Task<RefreshToken> CheckRefreshTokenAsync(string refreshToken);
        Task UpdateRefreshTokenAsync(RefreshToken refreshToken);
    }
}
