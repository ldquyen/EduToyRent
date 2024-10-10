using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.DTOs.OrderDTO
{
    public class ResponseOrderForStaff
    {
        public int OrderId { get; set; }
        public int StatusId { get; set; }
        public bool IsRentalOrder { get; set; }
        public DateTime OrderDate { get; set; }

    }
}
