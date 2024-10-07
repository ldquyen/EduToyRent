using AutoMapper;
using EduToyRent.Service.Common;
using EduToyRent.Service.DTOs.RequestFormDTO;
using EduToyRent.Service.Interfaces;
using EduToyRent.DAL.Entities;
using EduToyRent.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduToyRent.Repository.Repositories;
using EduToyRent.Service.Exceptions;
using System.Drawing;

namespace EduToyRent.Service.Services
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

        public async Task<dynamic> GetAllsRequest(int page = 1, int size = 10)
        {
            var list = await _unitOfWork.RequestFormRepository.GetAllAsync(null, null, page, size);
            var requestList = _mapper.Map<IEnumerable<ResponseRequestListDTO>>(list);
            return Result.SuccessWithObject(requestList);
        }

        public async Task<dynamic> GetRequestById(int requestId)
        {
            var r = await _unitOfWork.RequestFormRepository.GetAsync(x => x.RequestId == requestId);
            var request = _mapper.Map<ResponseRequestDetailDTO>(r);
            return Result.SuccessWithObject(request);
        }

        public async Task<dynamic> GetUnansweredRequest(int page = 1, int size = 10)
        {
            var list = await _unitOfWork.RequestFormRepository.GetAllAsync(x => x.RequestStatus == 0, null, page, size);
            var requestList = _mapper.Map<IEnumerable<ResponseRequestListDTO>>(list);
            return Result.SuccessWithObject(requestList);
        }
        public async Task<dynamic> GetAnsweredRequest(int page = 1, int size = 10)
        {
            var list = await _unitOfWork.RequestFormRepository.GetAllAsync(x => x.RequestStatus != 0, null, page, size);
            var requestList = _mapper.Map<IEnumerable<ResponseRequestListDTO>>(list);
            return Result.SuccessWithObject(requestList);
        }

        public async Task<dynamic> UppdateRequestStatus(UpdateRequestDTO updateRequestDTO, int staffId)
        {
            var existingRequest = await _unitOfWork.RequestFormRepository.GetAsync(x => x.RequestId == updateRequestDTO.RequestId);
            existingRequest.RequestStatus = updateRequestDTO.RequestStatus;
            if (updateRequestDTO.RequestStatus == 2)
            {
                if (string.IsNullOrEmpty(updateRequestDTO.DenyReason))
                    return Result.Failure(RequestErrors.DenyReason);
                existingRequest.DenyReason = updateRequestDTO.DenyReason;
            }
            if(updateRequestDTO.RequestStatus == 1)
            {
                var toy = await _unitOfWork.ToyRepository.GetAsync(x => x.ToyId == existingRequest.ToyId);
                toy.IsActive = true; 
                await _unitOfWork.ToyRepository.UpdateAsync(toy);
            }
            existingRequest.ResponseDate = DateTime.Now;
            existingRequest.ProcessedById = staffId;    
            await _unitOfWork.RequestFormRepository.UpdateAsync(existingRequest);
            await _unitOfWork.SaveAsync();
            return Result.Success();
        }
    }
}
