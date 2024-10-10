using EduToyRent.BLL.DTOs.CartDTO;
using EduToyRent.DAL.Entities;
using EduToyRent.DAL.Entities.Responses;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.BLL.Interfaces
{
    public interface ICartService
    {
        public Task<GetCartResponse> GetCart(int accountId);

        public Task<bool> AddItemToCart(GetCartRequest request);

		Task<bool> RemoveItemsFromCart(List<int> itemIdList);
	}
}
