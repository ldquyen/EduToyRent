using EduToyRent.Service.Common;
using EduToyRent.Service.DTOs.AccountDTO;
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
        Task<ViewToyForRentDetailDTO> ViewToyDetailForRent(int toyId);
        Task<ViewToyForSaleDetailDTO> ViewToyDetailForSale(int toyId);
        Task<Pagination<ViewToyForSaleDTO>> ViewToysForSale(string search, string sort, int pageIndex, int pageSize);
        Task<Pagination<ViewToyForRentDTO>> ViewToysForRent(string search, string sort, int pageIndex, int pageSize);
        Task<Pagination<ViewToyForRentSupplier>> ViewToysForRentAccount(string search, string sort, int pageIndex, int pageSize, CurrentUserObject currentUserObject);
        Task<Pagination<ViewToyForSellSupplier>> ViewToysForSellAccount(string search, string sort, int pageIndex, int pageSize, CurrentUserObject currentUserObject);
        Task<dynamic> DeteleToy(int toyId);
        Task<dynamic> UndoDeteleToy(int toyId);


    }
}
