using EduToyRent.DAL.Entities;
using Net.payOS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.Interfaces
{
    public interface IPayOSService
    {
        Task<dynamic> CreatePaymentLinkForSale(int orderId);
        Task<dynamic> CreatePaymentLinkForRent(int orderId);
        Task<dynamic> CreatePaymentLinkForRent2(int orderId);
        Task<dynamic> GetPaymentLinkInformation(int orderId);
        Task<dynamic> CancelPaymentLink(int orderId);
    }
}
