using EduToyRent.Service.Common;
using EduToyRent.Service.DTOs.ToyDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.Interfaces
{
    public interface IToyService
    {
        Task<dynamic> ChangeToyStatus(int toyId);
        Task<dynamic> CreateRentalToy(CreateRentalToyDTO createRentalToyDTO, string fileURL, int supplierID);
        Task<dynamic> CreateSaleToy(CreateSaleToyDTO createSaleToyDTO, string fileURL, int supplierID);
        Task<dynamic> GetToyByToyId(int toyId);
        Task<Result> UpdateToyInfo(int toyId, UpdateToyDTO updateToyDTO);
        Task<Pagination<ViewToyForRentDTO>> ViewToysForRent(int pageIndex, int pageSize);
        Task<Pagination<ViewToyForSaleDTO>> ViewToysForSale(int pageIndex, int pageSize);
        Task<ViewToyForRentDetailDTO> ViewToyDetailForRent(int toyId);
        Task<ViewToyForSaleDetailDTO> ViewToyDetailForSale(int toyId);
        Task<Pagination<ViewToyForRentDTO>> SearchRentByName(string keyword, int pageIndex, int pageSize);
        Task<Pagination<ViewToyForSaleDTO>> SearchSaleByName(string keyword, int pageIndex, int pageSize);
        Task<Pagination<ViewToyForRentDTO>> SortToysForRent(string sortBy, int pageIndex, int pageSize);
        Task<Pagination<ViewToyForSaleDTO>> SortToysForSale(string sortBy, int pageIndex, int pageSize);
    }
}
