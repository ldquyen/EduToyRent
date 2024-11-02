using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.DTOs.ReviewDTO
{
    public class CreateReviewDTO
    {
        [Required(ErrorMessage = "Toy Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Toy Id must be a positive integer.")]
        public int ToyId { get; set; }

        [Range(0.0, 5.0, ErrorMessage = "Rating must be between 0 and 5.")]
        public float Rating { get; set; }
        public string Comment { get; set; }
    }
}
