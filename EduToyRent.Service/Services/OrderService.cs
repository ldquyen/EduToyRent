using AutoMapper;
using EduToyRent.DAL.Entities;
using EduToyRent.Repository.Interfaces;
using EduToyRent.Service.Common;
using EduToyRent.Service.DTOs.AccountDTO;
using EduToyRent.Service.DTOs.OrderDTO;
using EduToyRent.Service.Exceptions;
using EduToyRent.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<dynamic> CreateOrder(CurrentUserObject currentUserObject, CreateOrderDTO createOrderDTO)
        {
            var toyIds = createOrderDTO.ToyList.Select(x => x.ToyId).ToList();
            if (!await _unitOfWork.ToyRepository.CheckExistToy(toyIds))
                return Result.Failure(ToyErrors.NotExistToy);
            if (createOrderDTO.ToyList == null)
            {
                return Result.Failure(ToyErrors.NotExistToy);
            }
            if (createOrderDTO.IsRentalOrder)
            {
                if (string.IsNullOrEmpty(createOrderDTO.RentalDate.ToString())
                    || string.IsNullOrEmpty(createOrderDTO.ReturnDate.ToString()))
                {
                    return Result.Failure(ToyErrors.RentalDateToyNull);
                }
            }
            var order = _mapper.Map<Order>(createOrderDTO);
            order.AccountId = currentUserObject.AccountId;
            order.StatusId = 1;
            order.PaymentStatus = false;
            order.TotalMoney = 0;
            order.Discount = 0;
            order.FinalMoney = 0;
            order.OrderDate = DateTime.Now;
            await _unitOfWork.OrderRepository.AddAsync(order);
            await _unitOfWork.SaveAsync();
            int id = order.OrderId;

            if (createOrderDTO.IsRentalOrder)
            {
                if (await CreateOrderDetailForRent(createOrderDTO, id))
                {
                    var odList = await _unitOfWork.OrderDetailRepository.GetAllAsync(x => x.OrderId == id, null, 1, 10);
                    foreach (var odDetail in odList)
                    {
                        odDetail.RentalPrice = await _unitOfWork.ToyRepository.GetMoneyRentByToyId(odDetail.ToyId, odDetail.Quantity, createOrderDTO.RentalDate, createOrderDTO.ReturnDate);
                        await _unitOfWork.OrderDetailRepository.UpdateAsync(odDetail);
                    }
                    await _unitOfWork.SaveAsync();
                    order.TotalMoney = await _unitOfWork.OrderDetailRepository.GetTotalMoney(id);
                    await _unitOfWork.OrderRepository.UpdateAsync(order);
                    await _unitOfWork.SaveAsync();
                    return Result.Success();
                }
                else
                    return Result.Failure(ToyErrors.CannotCreateToyRentOrder);
            }
            else if (!createOrderDTO.IsRentalOrder)
            {
                if (await CreateOrderDetailForSale(createOrderDTO, id))
                {
                    var odList = await _unitOfWork.OrderDetailRepository.GetAllAsync(x => x.OrderId == id, null, 1, 10);
                    foreach (var odDetail in odList)
                    {
                        odDetail.Price = await _unitOfWork.ToyRepository.GetMoneySaleByToyId(odDetail.ToyId, odDetail.Quantity);
                        await _unitOfWork.OrderDetailRepository.UpdateAsync(odDetail);
                    }
                    await _unitOfWork.SaveAsync();
                    order.TotalMoney = await _unitOfWork.OrderDetailRepository.GetTotalMoney(id);
                    await _unitOfWork.OrderRepository.UpdateAsync(order);
                    await _unitOfWork.SaveAsync();
                    return Result.Success();
                }
                else
                    return Result.Failure(ToyErrors.CannotCreateToySaleOrder);
            }
            else
                return Result.Failure(ToyErrors.CannotCreateToyOrder);
        }

        private async Task<bool> CreateOrderDetailForRent(CreateOrderDTO createOrderDTO, int orderId)
        {
            if (createOrderDTO.ToyList != null)
            {
                foreach (var toy in createOrderDTO.ToyList)
                {
                    OrderDetail od = new OrderDetail
                    {
                        OrderId = orderId,
                        ToyId = toy.ToyId,
                        Quantity = toy.Quantity,
                        Price = 0,
                        IsRental = true,
                        RentalDate = createOrderDTO.RentalDate,
                        ReturnDate = createOrderDTO.ReturnDate,
                        RentalPrice = 0,
                    };
                    await _unitOfWork.OrderDetailRepository.AddAsync(od);
                }
                await _unitOfWork.SaveAsync();
                return true;
            }
            else
                return false;
        }
        //await _unitOfWork.ToyRepository.GetMoneyRentByToyId(toy.ToyId, toy.Quantity,createOrderDTO.RentalDate, createOrderDTO.ReturnDate)
        private async Task<bool> CreateOrderDetailForSale(CreateOrderDTO createOrderDTO, int orderId)
        {
            if (createOrderDTO.ToyList != null)
            {
                foreach (var toy in createOrderDTO.ToyList)
                {
                    OrderDetail od = new OrderDetail
                    {
                        OrderId = orderId,
                        ToyId = toy.ToyId,
                        Quantity = toy.Quantity,
                        Price = 0,
                        IsRental = false,
                        RentalDate = createOrderDTO.RentalDate,
                        ReturnDate = createOrderDTO.ReturnDate,
                        RentalPrice = 0,
                    };
                    await _unitOfWork.OrderDetailRepository.AddAsync(od);
                }
                await _unitOfWork.SaveAsync();
                return true;
            }
            else
                return false;
        }




        public async Task<dynamic> GetAllOrderForStaff(int page)
        {
            var orderList = await _unitOfWork.OrderRepository.GetAllAsync(null, null, page, 10);
            var list = _mapper.Map<List<ResponseOrderForStaff>>(orderList);
            return Result.SuccessWithObject(list);
        }

        public async Task<dynamic> ConfirmOrder(ConfirmOrderDTO confirmOrderDTO)
        {
            Order order = await _unitOfWork.OrderRepository.GetAsync(x => x.OrderId == confirmOrderDTO.OrderId);
            if (order == null) return Result.Failure(OrderErrors.OrderIsNull);
            if (confirmOrderDTO.StatusId == 9)
            {
                order.StatusId = confirmOrderDTO.StatusId;
                await _unitOfWork.SaveAsync();
                return Result.Success();
            }
            if (confirmOrderDTO.StatusId == 2)
            {
                if (string.IsNullOrEmpty(confirmOrderDTO.Shipper) || string.IsNullOrEmpty(confirmOrderDTO.ShipperPhone))
                    return Result.Failure(OrderErrors.ShiperInfo);
                order.Shipper = confirmOrderDTO.Shipper;
                order.ShipperPhone = confirmOrderDTO.ShipperPhone;
                order.StatusId = confirmOrderDTO.StatusId;
                await _unitOfWork.OrderRepository.UpdateAsync(order);
                await _unitOfWork.SaveAsync();
                return Result.Success();
            }
            else
                return Result.Failure(OrderErrors.ConfirmStatus);
        }
    }
}
