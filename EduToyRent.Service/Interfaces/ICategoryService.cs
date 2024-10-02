using EduToyRent.Service.DTOs.CategoryDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.Interfaces
{
    public interface ICategoryService
    {
        Task<dynamic> CreateCategory(CreateNewCategoryDTO createNewCategoryDTO);
    }
}
