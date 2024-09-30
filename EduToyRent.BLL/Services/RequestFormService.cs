using AutoMapper;
using EduToyRent.BLL.Common;
using EduToyRent.BLL.DTOs.RequestFormDTO;
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
    public class RequestFormService : IRequestFromService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RequestFormService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<dynamic> CreateRentalRequest(CreateRentalRequestDTO createRentalRequestDTO)
        {
            var rentalRequest = _mapper.Map<RequestForm>(createRentalRequestDTO);
            await _unitOfWork.RequestFormRepository.AddAsync(rentalRequest);
            await _unitOfWork.SaveAsync();
            return Result.Success();
        }

        public async Task<dynamic> CreateSaleRequest(CreateSaleRequestDTO createSaleRequestDTO)
        {
            var saleRequest = _mapper.Map<RequestForm>(createSaleRequestDTO);
            await _unitOfWork.RequestFormRepository.AddAsync(saleRequest);
            await _unitOfWork.SaveAsync();
            return Result.Success();
        }
    }
}
