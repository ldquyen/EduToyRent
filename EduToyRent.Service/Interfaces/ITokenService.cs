using EduToyRent.Service.DTOs.AccountDTO;
using EduToyRent.Service.DTOs.TokenDTO;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.Interfaces
{
    public interface ITokenService
    {
        Task<SecurityToken> GenerateTokenAsync(CurrentUserObject currentUserObject);
        Task<dynamic> RenewTokenAsync(RenewTokenDTO tokenDTO);
        Task<string> GenerateAccessTokenAsync(SecurityToken token);
    }
}
