using EduToyRent.Service.DTOs.RequestFormDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.Interfaces
{
    public interface IRequestFromService
    {
        Task<dynamic> CreateRentalRequest(CreateRentalRequestDTO createRentalRequestDTO);
        Task<dynamic> CreateSaleRequest(CreateSaleRequestDTO createSaleRequestDTO);
    }
}
