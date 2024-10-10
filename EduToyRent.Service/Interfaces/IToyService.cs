using EduToyRent.Service.Common;
using EduToyRent.Service.DTOs;
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
        Task<dynamic> CreateRentalToy(CreateRentalToyDTO createRentalToyDTO, string fileURL, int supplierID);
        Task<dynamic> CreateSaleToy(CreateSaleToyDTO createSaleToyDTO, string fileURL, int supplierID);
        Task<dynamic> ChangeToyStatus(int toyId);
        Task<dynamic> GetToyByToyId(int toyId);
        Task<Result> UpdateToyInfo(int toyId, UpdateToyDTO updateToyDTO);
        Task<Pagination<ViewToyDTO>> ViewToys(int pageIndex, int pageSize);
        Task<ViewToyDetailDTO> ViewToyDetail(int toyId);
        Task<Pagination<ViewToyDTO>> SearchToys(string keyword, int pageIndex, int pageSize);
        Task<Pagination<ViewToyDTO>> SortToys(string sortBy, int pageIndex, int pageSize);
        
    }
}
