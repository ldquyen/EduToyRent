using AutoMapper;

using EduToyRent.BLL.Interfaces;
using EduToyRent.DAL.Entities;

using EduToyRent.DAL.Interfaces;
using EduToyRent.DAL.Repositories;
using EduToyRent.Repository.Interfaces;
using EduToyRent.Service.DTOs.CartDTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.BLL.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CartService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> AddItemToCart(GetCartRequest request, int accountId)
        {

			Toy existingToy = await _unitOfWork.ToyRepository.GetByIdAsync(request.toyId);
			if (existingToy == null) return false;

            Cart? cart = await _unitOfWork.CartRepository.GetByAccountIdAsync(accountId);
            if (cart == null) 
            {
                cart = new()
                {
                    AccountId = accountId,
                };
                cart = await _unitOfWork.CartRepository.AddCartAsync(cart);
            }

            CartItem? item = await _unitOfWork.CartItemRepository.GetAsync(request.toyId);
            if (item == null)
            { // add
                item = new()
                {
                    CartId = cart.CartId,
                    ToyId = request.toyId,
                    Quantity = request.quantity
                };
                await _unitOfWork.CartItemRepository.AddAsync(item);
                await _unitOfWork.SaveAsync();
                return true;
            }
            //update
            item.Quantity += request.quantity;

            await _unitOfWork.SaveAsync();
            return true;
        }


        public async Task<GetCartResponse> GetCart(int accountId)
        {
			GetCartResponse response = new();
			Cart result = await _unitOfWork.CartRepository.GetByAccountIdAsync(accountId);

			if (result == null)
			{
				result = new()
				{
					AccountId = accountId,
				};
				result = await _unitOfWork.CartRepository.AddCartAsync(result);
				return response;
			}

			List<CartItem>? items = await _unitOfWork.CartItemRepository.GetByCartIdAsync(result.CartId);

			response.CartId = result.CartId;
			response.AccountId = result.AccountId;
			response.CartItems = items;

			return response;
        }

		public async Task<bool> RemoveItemsFromCart(List<int> itemIdList)
		{
			bool result = await _unitOfWork.CartItemRepository.DeleteAsync(itemIdList);
			return result;
		}
    }
}
