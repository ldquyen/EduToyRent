using EduToyRent.DAL.Entities;

namespace EduToyRent.DAL.Interfaces
{
    public interface ICartItemRepository : IRepository<CartItem>
    {
        Task<CartItem?> GetAsync(int toyId);
        Task<bool> DeleteAsync(List<int> itemIdList);
		Task<List<CartItem>?> GetByCartIdAsync(int cartId);



	}
}