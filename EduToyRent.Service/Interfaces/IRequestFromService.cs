using EduToyRent.Service.DTOs.RequestFormDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.Interfaces
{
    public interface IRequestFromService
    {
        Task<dynamic> CreateRentalRequest(CreateRentalRequestDTO createRentalRequestDTO);
        Task<dynamic> CreateSaleRequest(CreateSaleRequestDTO createSaleRequestDTO);
        Task<dynamic> GetAllsRequest(int page = 1, int size = 10);
        Task<dynamic> GetRequestById(int requestId);
        Task<dynamic> UppdateRequestStatus(UpdateRequestDTO updateRequestDTO, int staffId);
    }
}
