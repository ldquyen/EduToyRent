using AutoMapper;
using EduToyRent.Service.Common;
using EduToyRent.Service.DTOs.AccountDTO;
using EduToyRent.Service.DTOs.TokenDTO;
using EduToyRent.Service.Exceptions;
using EduToyRent.Service.Interfaces;
using EduToyRent.DAL.Entities;
using EduToyRent.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.Services
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
