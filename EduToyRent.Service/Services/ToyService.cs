using AutoMapper;
using EduToyRent.Service.Common;
using EduToyRent.Service.DTOs.RequestFormDTO;
using EduToyRent.Service.DTOs.ToyDTO;
using EduToyRent.Service.Interfaces;
using EduToyRent.DAL.Entities;
using EduToyRent.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduToyRent.Service.Exceptions;

namespace EduToyRent.Service.Services
{
    public class ToyService : IToyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRequestFromService _requestFromService;

        public ToyService(IUnitOfWork unitOfWork, IMapper mapper, IRequestFromService requestFromService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _requestFromService = requestFromService;
        }

        public async Task<dynamic> ChangeToyStatus(int toyId)
        {
            var toy = await _unitOfWork.ToyRepository.GetAsync(x => x.ToyId == toyId);
            toy.IsActive = !toy.IsActive;
            return Result.Success();
        }

        public async Task<dynamic> CreateRentalToy(CreateRentalToyDTO createRentalToyDTO, string fileURL, int supplierID)
        {
            var rentalToy = _mapper.Map<Toy>(createRentalToyDTO);
            rentalToy.ImageUrl = fileURL;
            rentalToy.SupplierId = supplierID;
            rentalToy.IsRental = true;
            rentalToy.IsActive = false;
            rentalToy.IsDelete = false;
            await _unitOfWork.ToyRepository.AddAsync(rentalToy);
            await _unitOfWork.SaveAsync();
            CreateRentalRequestDTO createRentalRequestDTO = new CreateRentalRequestDTO();
            createRentalRequestDTO.ToyId = rentalToy.ToyId;
            await _requestFromService.CreateRentalRequest(createRentalRequestDTO);
            return Result.Success();
        }

        public async Task<dynamic> CreateSaleToy(CreateSaleToyDTO createSaleToyDTO, string fileURL, int supplierID)
        {
            var saleToy = _mapper.Map<Toy>(createSaleToyDTO);
            saleToy.ImageUrl = fileURL;
            saleToy.SupplierId = supplierID;
            saleToy.IsRental = false;
            saleToy.IsActive = false;
            saleToy.IsDelete = false;
            await _unitOfWork.ToyRepository.AddAsync(saleToy);
            await _unitOfWork.SaveAsync();
            CreateSaleRequestDTO createSaleRequestDTO = new CreateSaleRequestDTO();
            createSaleRequestDTO.ToyId = saleToy.ToyId;
            await _requestFromService.CreateSaleRequest(createSaleRequestDTO);
            return Result.Success();
        }

        public async Task<dynamic> GetToyByToyId(int toyId)
        {
            var toy = await _unitOfWork.ToyRepository.GetAsync(x => x.ToyId == toyId, includeProperties: "Supplier,Category");
            if (toy == null) return Result.Failure(ToyErrors.ToyIsNull);
            
            if (toy.IsRental)
            {
                var rentalToy = _mapper.Map<ResponseRentalToyDTO>(toy);
                rentalToy.CategoryName = toy.Category.CategoryName;
                rentalToy.SupplierName = toy.Supplier.AccountName;
                return Result.SuccessWithObject(rentalToy);
            }
            else
            {
                var saleToy = _mapper.Map<ResponseSaleToyDTO>(toy);
                saleToy.CategoryName = toy.Category.CategoryName;
                saleToy.SupplierName = toy.Supplier.AccountName;
                return Result.SuccessWithObject(saleToy);
            }
        }
    }
}
