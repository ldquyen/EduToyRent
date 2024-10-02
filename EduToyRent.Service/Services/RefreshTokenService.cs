using AutoMapper;
using EduToyRent.Service.Interfaces;
using EduToyRent.Repository.Interfaces;
using EduToyRent.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RefreshTokenService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<string> GenerateRefreshTokenAsync()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random); //dien so vao mang
                return Convert.ToBase64String(random); //chuyen mang byte thanh base64
            }
        }

        public async Task SaveRefreshTokenAsync(string tokenId, int userId, string refreshToken)
        {
            _unitOfWork.RefreshTokenRepository.AddRefreshTokenAsync(tokenId, userId, refreshToken);
        }
    }
}
