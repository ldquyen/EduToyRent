using AutoMapper;
using EduToyRent.DAL.Entities;
using EduToyRent.DAL.Interfaces;
using EduToyRent.DAL.Repositories;
using EduToyRent.Repository.Interfaces;
using EduToyRent.Service.Common;
using EduToyRent.Service.DTOs.CartDTO;
using EduToyRent.Service.Exceptions;
using EduToyRent.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.Services
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

        public async Task<dynamic> AddItemToCart(GetCartRequest request, int accountId)
        {

			Toy existingToy = await _unitOfWork.ToyRepository.GetByIdAsync(request.toyId);
			if (existingToy == null) return Result.Failure(ToyErrors.ToyIsNull);

            Cart? cart = await _unitOfWork.CartRepository.GetByAccountIdAsync(accountId);
            if (cart == null) 
            {
                cart = new()
                {
                    AccountId = accountId,
                };
                await _unitOfWork.CartRepository.AddAsync(cart);
                await _unitOfWork.SaveAsync();
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
            }
			else
			{
				//update
				item.Quantity += request.quantity;
				await _unitOfWork.CartItemRepository.UpdateAsync(item);
			}
			await _unitOfWork.SaveAsync();
			return Result.Success();
        }


        public async Task<dynamic> GetCart(int accountId)
        {
			Cart cart = await _unitOfWork.CartRepository.GetByAccountIdAsync(accountId);

			if (cart == null)
			{
				cart = new()
				{
					AccountId = accountId,
				};
				await _unitOfWork.CartRepository.AddAsync(cart);
                await _unitOfWork.SaveAsync();
				return Result.Success();
			}

			List<CartItem>? items = await _unitOfWork.CartItemRepository.GetByCartIdAsync(cart.CartId);
            var response = _mapper.Map<List<GetCartResponse>>(items);
			return Result.SuccessWithObject(response);
        }

		public async Task<bool> RemoveItemsFromCart(List<int> itemIdList)
		{
			bool result = await _unitOfWork.CartItemRepository.DeleteAsync(itemIdList);
			return result;
		}
    }
}
