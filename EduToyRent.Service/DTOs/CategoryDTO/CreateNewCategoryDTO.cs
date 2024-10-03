    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.DTOs.CategoryDTO
{
    public class CreateNewCategoryDTO
    {
        [Required(ErrorMessage = "Category name is required.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Category name must contain only letters and spaces and do not is Vietnamese.")]
        public string CategoryName { get; set; }
    }
}
