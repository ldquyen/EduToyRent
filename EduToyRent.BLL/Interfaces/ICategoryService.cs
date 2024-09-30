using EduToyRent.BLL.DTOs.CategoryDTO;
using EduToyRent.BLL.DTOs.ToyDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.BLL.Interfaces
{
    public interface ICategoryService
    {
        Task<dynamic> CreateCategory(CreateNewCategoryDTO createNewCategoryDTO);
    }
}
