using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.DTOs.OrderDTO
{
    public class ResponseOrderForUser
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string ShippingAddress { get; set; }
        public string ReceivePhoneNumber { get; set; }
        public string StatusName { get; set; }
        public decimal FinalMoney { get; set; }
        public bool PaymentStatus { get; set; }
    }
}
