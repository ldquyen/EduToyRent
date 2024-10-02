﻿using EduToyRent.API.Helper;
using EduToyRent.BLL.DTOs.AccountDTO;
using EduToyRent.BLL.DTOs.CategoryDTO;
using EduToyRent.BLL.Interfaces;
using EduToyRent.BLL.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduToyRent.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [Authorize(Policy = "StaffOnly")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("category")]
        public async Task<IActionResult> CreateCategory(CreateNewCategoryDTO createNewCategoryDTO)
        {
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                try
                {
                    var result = await _categoryService.CreateCategory(createNewCategoryDTO);
                    if (result.IsSuccess) return Ok(result);
                    return BadRequest(result);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
        }
        [Authorize(Policy = "StaffOnly")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            {
                try
                {
                    var result = await _categoryService.GetListCategory();
                    if (result.IsSuccess) return Ok(result);
                    return BadRequest(result);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
        }
    }
}
