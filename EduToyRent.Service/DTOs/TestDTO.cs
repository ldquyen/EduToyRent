using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.DTOs
{
    public class TestDTO
    {

        [Required]
        public List<int> ToyList { get; set; }
    }
}
