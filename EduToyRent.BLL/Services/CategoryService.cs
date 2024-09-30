using AutoMapper;
using EduToyRent.BLL.Common;
using EduToyRent.BLL.DTOs.CategoryDTO;
using EduToyRent.BLL.Interfaces;
using EduToyRent.DAL.Entities;
using EduToyRent.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<dynamic> CreateCategory(CreateNewCategoryDTO createNewCategoryDTO)
        {
            var category = _mapper.Map<Category>(createNewCategoryDTO);
            await _unitOfWork.CategoryRepository.AddAsync(category);
            await _unitOfWork.SaveAsync();
            return Result.Success();
        }
    }
}
