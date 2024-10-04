using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.DTOs.RequestFormDTO
{
    public class UpdateRequestDTO
    {
        [Required]
        public int RequestId { get; set; }
        [Required]
        public int RequestStatus { get; set; }
        public string? DenyReason { get; set; }
    }
}
