using EduToyRent.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Repository.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<OrderDetail>> GetByOrderIdAsync(int orderId);
        Task AddOrderDetailAsync(OrderDetail orderDetail);
        Task<bool> UpdateOrderStatus(int orderId, int statusId);
        Task<List<Order>> GetOrderRentForUser(int accountId, int status);
        Task<List<Order>> GetOrderSaleForUser(int accountId, int status);
    }
}
