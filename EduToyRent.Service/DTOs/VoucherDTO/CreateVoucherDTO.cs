using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.DTOs.VoucherDTO
{
    public class CreateVoucherDTO
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Voucher name must contain only letters and spaces.")]
        public string VoucherName { get; set; }

        [Required]
        [FutureDate(ErrorMessage = "Expired date must be in the future.")]
        public DateTime ExpiredDate { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "Discount must be greater than 0 and less than 100.")]
        public float Discount { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
        public int Quantity { get; set; }
    }
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateTime)
            {
                if (dateTime <= DateTime.Now)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            return ValidationResult.Success;
        }
    }
}
