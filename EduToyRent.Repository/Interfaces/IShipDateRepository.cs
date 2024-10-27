using EduToyRent.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Repository.Interfaces
{
    public interface IShipDateRepository : IRepository<ShipDate>
    {
        Task CreateShipDate(OrderDetail orderDetail);
        Task<bool> CheckAllShip(List<int> orderDetailIds);
        Task<bool> CheckShip(int orderDetailId);
    }
}
