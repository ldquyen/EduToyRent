using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduToyRent.BLL.DTOs.CategoryDTO
{
    public class UpdateCategoryDTO
    {
        [Required(ErrorMessage = "Category Id is required.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Category Id must be only numbers.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Category name must contain only letters and spaces and should not include Vietnamese characters.")]
        public string CategoryName { get; set; }
    }
}
