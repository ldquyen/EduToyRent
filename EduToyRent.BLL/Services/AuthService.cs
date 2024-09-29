using AutoMapper;
using EduToyRent.BLL.Common;
using EduToyRent.BLL.DTOs.AccountDTO;
using EduToyRent.BLL.DTOs.TokenDTO;
using EduToyRent.BLL.Exceptions;
using EduToyRent.BLL.Interfaces;
using EduToyRent.DAL.Entities;
using EduToyRent.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenService _refreshTokenService;

        public AuthService(IUnitOfWork unitOfWork , IMapper mapper, ITokenService tokenService, IRefreshTokenService refreshTokenService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;   
            _tokenService = tokenService;
            _refreshTokenService = refreshTokenService;
        }
        public async Task<dynamic> LoginAsync(LoginDTO loginDTO)
        {
            var logacc = _mapper.Map<Account>(loginDTO);
            string hashPassword = await HashPassword.HassPass(logacc.AccountPassword);
            var account = await _unitOfWork.AccountRepository.LoginAsync(logacc.AccountEmail, hashPassword);
            if (account != null)
            {
                if (account.IsBan) return Result.Failure(LoginErrors.AccountIsBan);
                var currentUser = _mapper.Map<CurrentUserObject>(account);
                var token = await _tokenService.GenerateTokenAsync(currentUser);
                var accesstoken = await _tokenService.GenerateAccessTokenAsync(token);
                var refreshtoken = await _refreshTokenService.GenerateRefreshTokenAsync();
                await _refreshTokenService.SaveRefreshTokenAsync(token.Id, account.AccountId, refreshtoken);
                var tokenDTO = new TokenDTO
                {
                    AccessToken = accesstoken,
                    RefreshToken = refreshtoken
                };
                return Result.SuccessWithObject(tokenDTO);
            }
            else
            {
                return Result.Failure(LoginErrors.InvalidAccount);
            }
            
        }


    }
}
