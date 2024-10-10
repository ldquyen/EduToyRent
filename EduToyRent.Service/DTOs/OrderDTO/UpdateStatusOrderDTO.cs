using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.DTOs.OrderDTO
{
    public class UpdateStatusOrderDTO
    {
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int StatusId { get; set; }
    }
}
