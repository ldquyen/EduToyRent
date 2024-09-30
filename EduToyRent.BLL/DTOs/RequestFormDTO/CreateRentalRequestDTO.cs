using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.BLL.DTOs.RequestFormDTO
{
    public class CreateRentalRequestDTO
    {
        public int ToyId { get; set; }
        public DateTime RequestDate { get; set; }  = DateTime.Now;
        public int RequestStatus { get; set; } = 0;
        public bool ForRent { get; set; } = true;

    }
}
