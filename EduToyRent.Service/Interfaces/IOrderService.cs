using EduToyRent.Service.Common;
using EduToyRent.Service.DTOs.AccountDTO;
using EduToyRent.Service.DTOs.OrderDTO;

namespace EduToyRent.Service.Interfaces
{
    public interface IOrderService
    {
        Task<dynamic> CreateOrder(CurrentUserObject currentUserObject, CreateOrderDTO createOrderDTO);
        Task<dynamic> GetAllOrderForStaff(int page);
        Task<dynamic> ConfirmOrder(ConfirmOrderDTO confirmOrderDTO);
        
    }
}
