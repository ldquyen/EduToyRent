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
        public async Task<Result> UpdateToyInfo(int toyId, UpdateToyDTO updateToyDTO)
        {
            var toy = await _unitOfWork.ToyRepository.GetToyById(toyId);
            if (toy == null)
                return Result.Failure(ToyErrors.ToyIsNull);
            _mapper.Map(updateToyDTO, toy);
            var updateResult = await _unitOfWork.ToyRepository.UpdateToy(toy);
            if (!updateResult)
                return Result.Failure(new Error("UpdateFailed", "Failed to update toy information"));
            return Result.Success();
        }
        public async Task<Pagination<ViewToyForRentDTO>> ViewToysForRent(int pageIndex, int pageSize)
        {
            var totalItemsCount = await _unitOfWork.ToyRepository.GetRentCount();
            var toys = await _unitOfWork.ToyRepository.ViewToysForRent(pageIndex, pageSize);

            var toyDTOs = _mapper.Map<IEnumerable<ViewToyForRentDTO>>(toys);
            return new Pagination<ViewToyForRentDTO>
            {
                TotalItemsCount = totalItemsCount,
                PageSize = pageSize,
                PageIndex = pageIndex,
                Items = toyDTOs.ToList()
            };
        }

        public async Task<Pagination<ViewToyForSaleDTO>> ViewToysForSale(int pageIndex, int pageSize)
        {
            var totalItemsCount = await _unitOfWork.ToyRepository.GetSaleCount();
            var toys = await _unitOfWork.ToyRepository.ViewToysForSale(pageIndex, pageSize);

            var toyDTOs = _mapper.Map<IEnumerable<ViewToyForSaleDTO>>(toys);
            return new Pagination<ViewToyForSaleDTO>
            {
                TotalItemsCount = totalItemsCount,
                PageSize = pageSize,
                PageIndex = pageIndex,
                Items = toyDTOs.ToList()
            };
        }

        public async Task<ViewToyForRentDetailDTO> ViewToyDetailForRent(int toyId)
        {
            var toy = await _unitOfWork.ToyRepository.GetToyById(toyId);
            if (toy == null || !toy.IsRental)
            {
                throw new Exception("Rent toy not found!");
            }

            var detailDTO = new ViewToyForRentDetailDTO
            {
                ToyId = toy.ToyId,
                ToyName = toy.ToyName,
                Description = toy.Description,
                RentPricePerDay = toy.RentPricePerDay,
                RentPricePerWeek = toy.RentPricePerWeek,
                RentPricePerTwoWeeks = toy.RentPricePerTwoWeeks,
                SupplierName = toy.Supplier?.AccountName,
                CategoryName = toy.Category?.CategoryName,
                ImageUrl = toy.ImageUrl
            };

            return detailDTO;
        }

        public async Task<ViewToyForSaleDetailDTO> ViewToyDetailForSale(int toyId)
        {
            var toy = await _unitOfWork.ToyRepository.GetToyById(toyId);
            if (toy == null || toy.IsRental)
            {
                throw new Exception("Toy not found!");
            }

            var detailDTO = new ViewToyForSaleDetailDTO
            {
                ToyId = toy.ToyId,
                ToyName = toy.ToyName,
                Description = toy.Description,
                BuyPrice = toy.BuyPrice,
                SupplierName = toy.Supplier?.AccountName,
                CategoryName = toy.Category?.CategoryName,
                ImageUrl = toy.ImageUrl
            };

            return detailDTO;
        }

        public async Task<Pagination<ViewToyForRentDTO>> SearchRentByName(string keyword, int pageIndex, int pageSize)
        {
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            var totalItemsCount = await _unitOfWork.ToyRepository.GetCountByToyName(keyword, true);
            var toys = await _unitOfWork.ToyRepository.SearchToysByName(keyword, true, pageIndex, pageSize);
            var toyDTOs = _mapper.Map<IEnumerable<ViewToyForRentDTO>>(toys);

            return new Pagination<ViewToyForRentDTO>
            {
                TotalItemsCount = totalItemsCount,
                PageSize = pageSize,
                PageIndex = pageIndex,
                Items = toyDTOs.ToList()
            };
        }

        public async Task<Pagination<ViewToyForSaleDTO>> SearchSaleByName(string keyword, int pageIndex, int pageSize)
        {
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            var totalItemsCount = await _unitOfWork.ToyRepository.GetCountByToyName(keyword, false);
            var toys = await _unitOfWork.ToyRepository.SearchToysByName(keyword, false, pageIndex, pageSize);
            var toyDTOs = _mapper.Map<IEnumerable<ViewToyForSaleDTO>>(toys);

            return new Pagination<ViewToyForSaleDTO>
            {
                TotalItemsCount = totalItemsCount,
                PageSize = pageSize,
                PageIndex = pageIndex,
                Items = toyDTOs.ToList()
            };
        }

        public async Task<Pagination<ViewToyForRentDTO>> SortToysForRent(string sortBy, int pageIndex, int pageSize)
        {
            var totalItemsCount = await _unitOfWork.ToyRepository.GetRentCount();
            var toys = await _unitOfWork.ToyRepository.SortToysForRent(sortBy, pageIndex, pageSize);
            var toyDtos = _mapper.Map<ICollection<ViewToyForRentDTO>>(toys);
            return new Pagination<ViewToyForRentDTO>
            {
                TotalItemsCount = totalItemsCount,
                PageSize = pageSize,
                PageIndex = pageIndex,
                Items = toyDtos
            };
        }

        public async Task<Pagination<ViewToyForSaleDTO>> SortToysForSale(string sortBy, int pageIndex, int pageSize)
        {
            var totalItemsCount = await _unitOfWork.ToyRepository.GetSaleCount();
            var toys = await _unitOfWork.ToyRepository.SortToysForSale(sortBy, pageIndex, pageSize);
            var toyDtos = _mapper.Map<ICollection<ViewToyForSaleDTO>>(toys);
            return new Pagination<ViewToyForSaleDTO>
            {
                TotalItemsCount = totalItemsCount,
                PageSize = pageSize,
                PageIndex = pageIndex,
                Items = toyDtos
            };
        }

    }
}
