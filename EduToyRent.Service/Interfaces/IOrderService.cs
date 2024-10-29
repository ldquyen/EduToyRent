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
        Task<dynamic> GetOrderDetailForUser( int orderId, int accountId);
        Task<dynamic> GetOrderOfAccount(int accountId, bool isRent, int status);
        Task<dynamic> ViewOrderRentDetailForSupplier(int accountId);
        Task<dynamic> ViewOrderSaleDetailForSupplier(int accountId);
        Task<dynamic> SupplierConfirmShip(int orderDetailId);
        Task<dynamic> CompleteOrder(int orderId, int accountId);
    }
}
