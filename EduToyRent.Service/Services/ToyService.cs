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
using EduToyRent.Repository.Repositories;

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
        public async Task<Pagination<ViewToyForRentDTO>> ViewToysForRent(string search, string sort, int pageIndex, int pageSize)
        {
            var toys = await _unitOfWork.ToyRepository.GetToysForRent();
            if (string.IsNullOrEmpty(search) && string.IsNullOrEmpty(sort))
            {
                var totalToysCount = toys.Count;
                toys = toys.Skip(pageIndex * pageSize).Take(pageSize).ToList();

                return new Pagination<ViewToyForRentDTO>
                {
                    TotalItemsCount = totalToysCount,
                    PageSize = pageSize,
                    PageIndex = pageIndex,
                    Items = toys.Select(t => new ViewToyForRentDTO
                    {
                        ToyId = t.ToyId,
                        ToyName = t.ToyName,
                        Description = t.Description,
                        RentPricePerDay = t.RentPricePerDay,
                        ImageUrl = t.ImageUrl
                    }).ToList()
                };
            }
            if (!string.IsNullOrEmpty(search))
            {
                toys = toys.Where(t => t.ToyName.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            toys = sort switch
            {
                "price_asc" => toys.OrderBy(t => t.RentPricePerDay).ToList(),
                "price_desc" => toys.OrderByDescending(t => t.RentPricePerDay).ToList(),
                "name_asc" => toys.OrderBy(t => t.ToyName).ToList(),
                "name_desc" => toys.OrderByDescending(t => t.ToyName).ToList(),
                _ => toys.OrderBy(t => t.ToyName).ToList()
            };

            int totalItemsCount = toys.Count;

            toys = toys.Skip(pageIndex * pageSize).Take(pageSize).ToList();

            var paginationResult = new Pagination<ViewToyForRentDTO>
            {
                TotalItemsCount = totalItemsCount,
                PageSize = pageSize,
                PageIndex = pageIndex,
                Items = toys.Select(t => new ViewToyForRentDTO
                {
                    ToyId = t.ToyId,
                    ToyName = t.ToyName,
                    Description = t.Description,
                    RentPricePerDay = t.RentPricePerDay,
                    ImageUrl = t.ImageUrl
                }).ToList()
            };

            return paginationResult;
        }

        public async Task<Pagination<ViewToyForSaleDTO>> ViewToysForSale(string search, string sort, int pageIndex, int pageSize)
        {
            var toys = await _unitOfWork.ToyRepository.GetToysForSale();
            if (string.IsNullOrEmpty(search) && string.IsNullOrEmpty(sort))
            {
                var totalToysCount = toys.Count;
                toys = toys.Skip(pageIndex * pageSize).Take(pageSize).ToList();

                return new Pagination<ViewToyForSaleDTO>
                {
                    TotalItemsCount = totalToysCount,
                    PageSize = pageSize,
                    PageIndex = pageIndex,
                    Items = toys.Select(t => new ViewToyForSaleDTO
                    {
                        ToyId = t.ToyId,
                        ToyName = t.ToyName,
                        Description = t.Description,
                        BuyPrice = t.BuyPrice,
                        ImageUrl = t.ImageUrl
                    }).ToList()
                };
            }
            if (!string.IsNullOrEmpty(search))
            {
                toys = toys.Where(t => t.ToyName.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            toys = sort switch
            {
                "price_asc" => toys.OrderBy(t => t.BuyPrice).ToList(),
                "price_desc" => toys.OrderByDescending(t => t.BuyPrice).ToList(),
                "name_asc" => toys.OrderBy(t => t.ToyName).ToList(),
                "name_desc" => toys.OrderByDescending(t => t.ToyName).ToList(),
                _ => toys.OrderBy(t => t.ToyName).ToList()
            };

            int totalItemsCount = toys.Count;

            toys = toys.Skip(pageIndex * pageSize).Take(pageSize).ToList();

            var paginationResult = new Pagination<ViewToyForSaleDTO>
            {
                TotalItemsCount = totalItemsCount,
                PageSize = pageSize,
                PageIndex = pageIndex,
                Items = toys.Select(t => new ViewToyForSaleDTO
                {
                    ToyId = t.ToyId,
                    ToyName = t.ToyName,
                    Description = t.Description,
                    BuyPrice = t.BuyPrice,
                    ImageUrl = t.ImageUrl
                }).ToList()
            };

            return paginationResult;
        }
    }
}
