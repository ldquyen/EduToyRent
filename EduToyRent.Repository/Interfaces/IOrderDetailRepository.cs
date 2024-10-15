using EduToyRent.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Repository.Interfaces
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        Task<List<OrderDetail>> GetOrderForSupplier(int supplierId);
    }
}
