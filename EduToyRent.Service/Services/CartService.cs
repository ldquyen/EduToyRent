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
			try
			{
				Toy toy = await _unitOfWork.ToyRepository.GetByIdAsync(request.toyId);
				if (toy == null) return Result.Failure(ToyErrors.ToyIsNull);

				var carts = await _unitOfWork.CartRepository.GetByAccountIdAsync(accountId);
				if (carts == null)
				{
					Cart cartrent = new()
					{
						AccountId = accountId,
						IsRental = true,
					};
					await _unitOfWork.CartRepository.AddCartAsync(cartrent);
					Cart cartsale = new()
					{
						AccountId = accountId,
						IsRental = false,
					};
					await _unitOfWork.CartRepository.AddCartAsync(cartsale);
				}
				Cart cart = new Cart();
				if (toy.IsRental)
					cart = await _unitOfWork.CartRepository.GetRentCart(accountId);
				else
					cart = await _unitOfWork.CartRepository.GetSaleCart(accountId);


				CartItem? item = await _unitOfWork.CartItemRepository.GetCartItem(request.toyId, cart.CartId);
				if (item == null)
				{ // add
					item = new()
					{
						CartId = cart.CartId,
						ToyId = request.toyId,
						Quantity = request.quantity < toy.Stock ? request.quantity : toy.Stock,
					};
					await _unitOfWork.CartItemRepository.AddAsync(item);
				}
				else
				{
					//update
					if (item.Quantity + request.quantity > toy.Stock)
					{
						item.Quantity = toy.Stock;
					}
					else item.Quantity += request.quantity;

					await _unitOfWork.CartItemRepository.UpdateAsync(item);
				}
				await _unitOfWork.SaveAsync();
				return Result.Success();
			} catch (Exception ex) {
				return null;
			}
        }


        public async Task<dynamic> GetCart(int accountId, bool isRent)
        {
            Cart cart = new Cart();
            if (isRent)
            {
                cart = await _unitOfWork.CartRepository.GetRentCart(accountId);
                if (cart == null)
                {
                    Cart cartrent = new()
                    {
                        AccountId = accountId,
                        IsRental = true,
                    };
                    await _unitOfWork.CartRepository.AddCartAsync(cartrent);
                    return Result.Success();
                }
            }
            else
            {
                cart = await _unitOfWork.CartRepository.GetSaleCart(accountId);
                if (cart == null)
                {
                    Cart cartsale = new()
                    {
                        AccountId = accountId,
                        IsRental = false,
                    };
                    await _unitOfWork.CartRepository.AddCartAsync(cartsale);
                    return Result.Success();
                }
            }
            List<CartItem>? items = await _unitOfWork.CartItemRepository.GetByCartIdAsync(cart.CartId);
            var response = _mapper.Map<List<GetCartResponse>>(items);
            return Result.SuccessWithObject(response);
        }

        public async Task<bool> RemoveItemsFromCart(List<int> itemIdList)
        {
			try
			{
				bool result = await _unitOfWork.CartItemRepository.DeleteAsync(itemIdList);
				return result;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return false;
			}
            
        }
    }
}
