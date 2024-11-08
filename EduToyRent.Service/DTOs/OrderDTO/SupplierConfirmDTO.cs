using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.DTOs.OrderDTO
{
    public class SupplierConfirmDTO
    {
        public int OrderDetailId { get; set; }
        public string? Shipper { get; set; }
        public string? ShipperPhone { get; set; }
    }
}
