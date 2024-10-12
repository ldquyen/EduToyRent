
using EduToyRent.Service.DTOs.CartDTO;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.Interfaces
{
    public interface ICartService
    {
        public Task<dynamic> GetCart(int accountId);

        public Task<dynamic> AddItemToCart(GetCartRequest request, int accountId);

		Task<bool> RemoveItemsFromCart(List<int> itemIdList);
	}
}
