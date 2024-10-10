using EduToyRent.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Repository.Interfaces
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task<Cart> GetByAccountIdAsync(int accountId);
        Task<Cart> AddCartAsync(Cart cart);



    }
}
