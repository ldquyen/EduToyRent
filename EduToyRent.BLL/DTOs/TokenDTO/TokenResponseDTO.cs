using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.BLL.DTOs.TokenDTO
{
    public class TokenResponseDTO
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
