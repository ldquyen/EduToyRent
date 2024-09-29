using EduToyRent.BLL.DTOs.AccountDTO;
using EduToyRent.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.BLL.Interfaces
{
    public interface IAuthService
    {
        Task<dynamic> LoginAsync(LoginDTO loginDTO);
    }
}
