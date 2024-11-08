using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.DTOs.OrderDTO
{
    public class InformationForShipDTO
    {
        public string AccountName { get; set; }
        public string ShippingAddress { get; set; }
        public string ReceivePhoneNumber { get; set; }
    }
}
