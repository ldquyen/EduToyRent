using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.BLL.Interfaces
{
    public interface IRefreshTokenService
    {
        Task<string> GenerateRefreshTokenAsync();
        Task SaveRefreshTokenAsync(string tokenId, int accountId, string refreshToken);
    }
}
