using EduToyRent.DAL.Entities;
using EduToyRent.Repository.Interfaces;

namespace EduToyRent.DAL.Interfaces
{
    public interface ICartItemRepository : IRepository<CartItem>
    {
        Task<CartItem?> GetCartItem(int toyId, int cartId);
        Task<bool> DeleteAsync(List<int> itemIdList);
		Task<List<CartItem>?> GetByCartIdAsync(int cartId);



	}
}