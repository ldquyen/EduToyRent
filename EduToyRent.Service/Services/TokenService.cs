using AutoMapper;
using EduToyRent.Service.DTOs.AccountDTO;
using EduToyRent.Service.DTOs.TokenDTO;
using EduToyRent.Service.Interfaces;
using EduToyRent.Repository.Interfaces;
using EduToyRent.Repository.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.Services
{
    public class TokenService : ITokenService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly AppSetting _appSettings;
        private readonly IRefreshTokenService _refreshTokenService;
        public TokenService(IUnitOfWork unitOfWork, IMapper mapper, IOptionsMonitor<AppSetting> appSetting, IRefreshTokenService refreshTokenService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _appSettings = appSetting.CurrentValue;
            _refreshTokenService = refreshTokenService;
            //_refreshTokenRepository, _userRepository
        }

        public async Task<SecurityToken> GenerateTokenAsync(CurrentUserObject currentUserObject)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, currentUserObject.AccountId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Name, currentUserObject.AccountName),
                    new Claim("Phone", currentUserObject.PhoneNumber),
                    new Claim(ClaimTypes.Role, currentUserObject.RoleId.ToString())   ,
                    new Claim(ClaimTypes.Email, currentUserObject.AccountEmail),
                    new Claim("AccountId", currentUserObject.AccountId.ToString())

                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescription);
            return token;
        }
        public async Task<string> GenerateAccessTokenAsync(SecurityToken token)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var accessToken = jwtTokenHandler.WriteToken(token);
            return accessToken;
        }


        public async Task<dynamic> RenewTokenAsync(RenewTokenDTO tokenDTO)
        {
            if (tokenDTO.AccessToken == null || tokenDTO.RefreshToken == null)
            {
                return new TokenResponseDTO
                {
                    Status = false,
                    Message = "Null"
                };
            }
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
            var tokenValidateParam = new TokenValidationParameters
            {

                ValidateIssuer = false,
                ValidateAudience = false,

                ValidateIssuerSigningKey = true,
                //ValidIssuer = _configuration["Jwt:Issuer"],
                //ValidAudience = _configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),

                ClockSkew = TimeSpan.Zero,

                ValidateLifetime = false, // ko kiem tra token het han
            };


            try
            {
                //check 1: AccessToken co dung voi fomat ko
                var tokenInVerification = jwtTokenHandler.ValidateToken(tokenDTO.AccessToken,
                    tokenValidateParam, out var validatedToken);

                //check 2: algorithm, kiem tra thuat toan 
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var res = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                        StringComparison.InvariantCultureIgnoreCase);
                    if (!res) //false
                    {
                        return new TokenResponseDTO
                        {
                            Status = false,
                            Message = "Invalid token"
                        };
                    }
                }

                //check 3: Check accessToken da het han ch? expire?
                var utcExpireDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(x =>
                    x.Type == JwtRegisteredClaimNames.Exp).Value); // Lấy ngày hết hạn của access token
                var expireDate = ConvertUnixTimeToDateTime(utcExpireDate); //Chuyển đổi thời gian hết hạn từ Unix time sang DateTime.
                if (expireDate > DateTime.UtcNow) //Kiểm tra nếu access token chưa hết hạn.
                {
                    return new TokenResponseDTO
                    {
                        Status = false,
                        Message = "Acess token has not expired "
                    };
                }

                //check 4: check freshToken exist in DB
                var storedToken = await _unitOfWork.RefreshTokenRepository.CheckRefreshTokenAsync(tokenDTO.RefreshToken);
                if (storedToken == null)
                {
                    return new TokenResponseDTO
                    {
                        Status = false,
                        Message = "Refresh token deos not exist "
                    };
                }

                //check 5: check refreshToken has used or not? 
                if (storedToken.IsUsed) //Kiểm tra nếu refresh token đã được sử dụng
                {
                    return new TokenResponseDTO
                    {
                        Status = false,
                        Message = "Refresh token has been used "
                    };
                }
                if (storedToken.IsRevoke) //Kiểm tra nếu refresh token đã bị thu hồi.
                {
                    return new TokenResponseDTO
                    {
                        Status = false,
                        Message = "Refresh token has been revoked "
                    };
                }

                //check 6: AccessToken id == JWTID in RefreshToken
                var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value; // Lấy ID của access token
                if (storedToken.JwtID != jti) //Kiểm tra nếu ID của access token không khớp với refresh token.
                {
                    return new TokenResponseDTO
                    {
                        Status = false,
                        Message = "Token doesn't match"
                    };
                }
                storedToken.IsRevoke = true; //Đánh dấu refresh token là đã bị thu hồi.
                storedToken.IsUsed = true;   //Đánh dấu refresh token là đã được sử dụng.
                await _unitOfWork.RefreshTokenRepository.UpdateRefreshTokenAsync(storedToken);
                var newUser = await _unitOfWork.AccountRepository.GetByIdAsync(storedToken.AccountId);
                var currentUserObject1 = new CurrentUserObject
                {
                    AccountId = newUser.AccountId,
                    AccountEmail = newUser.AccountEmail,
                    AccountName = newUser.AccountName,
                    RoleId = newUser.RoleId,
                    PhoneNumber = newUser.PhoneNumber
                };
                var newToken = await GenerateTokenAsync(currentUserObject1);
                var accessToken = await GenerateAccessTokenAsync(newToken);
                var refreshToken = await _refreshTokenService.GenerateRefreshTokenAsync();
                await _refreshTokenService.SaveRefreshTokenAsync(newToken.Id, newUser.AccountId, refreshToken);
                var tokendto = new RenewTokenDTO
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                };
                return new TokenResponseDTO
                {
                    Status = true,
                    Message = "Renew token success",
                    Data = tokendto
                };
            }
            catch (Exception ex)
            {
                return new TokenResponseDTO
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        private DateTime ConvertUnixTimeToDateTime(long utcExpireDate)
        {
            var dateTimeInterval = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeInterval.AddSeconds(utcExpireDate).ToUniversalTime();
            return dateTimeInterval;
        }
    }
}
