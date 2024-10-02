using AutoMapper;
using EduToyRent.Service.Common;
using EduToyRent.Service.DTOs.CategoryDTO;
using EduToyRent.Service.Interfaces;
using EduToyRent.DAL.Entities;
using EduToyRent.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.Services
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
        public async Task<dynamic> GetListCategory()
        {
            var list = await _unitOfWork.CategoryRepository.GetAllAsync();
            List<ListCategoryDTO> listCategoryDTO = new List<ListCategoryDTO>();
            foreach (var category in list)
            {
                var dto = new ListCategoryDTO
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName
                };

                listCategoryDTO.Add(dto);
            }
            return Result.SuccessWithObject(listCategoryDTO);
        }
        public async Task<dynamic> UpdateCategory(UpdateCategoryDTO updateCategoryDTO)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(updateCategoryDTO.CategoryId);
            category.CategoryName = updateCategoryDTO.CategoryName;
            await _unitOfWork.CategoryRepository.UpdateAsync(category);
            await _unitOfWork.SaveAsync();
            return Result.Success();
        }
    }
}
