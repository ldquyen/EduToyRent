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
            if (!await _unitOfWork.ToyRepository.CheckExistToy(createOrderDTO.ToyList))
                return Result.Failure(ToyErrors.NotExistToy);   // neu bao loi nay thi chinh lai dong 29
            if (!await _unitOfWork.ToyRepository.CheckSameTypeOfToy(createOrderDTO.ToyList, createOrderDTO.IsRentalOrder))
                return Result.Failure(ToyErrors.NotSameToy);
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
            // => tiếp tục tạo orderDetail
            await CreateOrderDetailForRent(createOrderDTO, id);
            return Result.Success();
        }
        //
        private async Task<bool> CreateOrderDetailForRent(CreateOrderDTO createOrderDTO, int orderId)
        {
            foreach (var toy in createOrderDTO.ToyList)
            {
                OrderDetail od = new OrderDetail
                {
                    OrderId = orderId,
                    ToyId = toy,
                    Quantity = 1,
                    Price = 10,
                    IsRental = true,
                    RentalDate = createOrderDTO.RentalDate,
                    ReturnDate = createOrderDTO.ReturnDate,
                    RentalPrice = 100,
                };
                await _unitOfWork.OrderDetailRepository.AddAsync(od);
                await _unitOfWork.SaveAsync();
            }
            return true;
        }



        //
        public async Task<dynamic> CreateRentOrderDetail(CreateRentOrderDetailDTO dto)
        {
            var orderDetails = await _unitOfWork.OrderRepository.GetByOrderIdAsync(dto.OrderId);
            if (orderDetails == null)
            {
                return Result.Failure(ToyErrors.NotExistToy);
            }
            var totalMoney = 0m;

            foreach (var orderDetail in orderDetails)
            {
                var toy = await _unitOfWork.ToyRepository.GetByIdAsync(orderDetail.ToyId);
                if (toy == null) return Result.Failure(new Error("ToyNotFound", "Toy not found!"));

                var rentalDuration = (dto.ReturnDate - dto.RentalDate)?.Days ?? 0;

                decimal rentalPrice = 0;

                if (rentalDuration <= 6)
                {
                    rentalPrice = toy.RentPricePerDay.GetValueOrDefault() * rentalDuration;
                }
                else if (rentalDuration == 7)
                {
                    rentalPrice = toy.RentPricePerWeek.GetValueOrDefault();
                }
                else if (rentalDuration <= 14)
                {
                    rentalPrice = toy.RentPricePerWeek.GetValueOrDefault() + (toy.RentPricePerDay.GetValueOrDefault() * (rentalDuration - 7));
                }
                else
                {
                    rentalPrice = toy.RentPricePerTwoWeeks.GetValueOrDefault() + (toy.RentPricePerWeek.GetValueOrDefault() * ((rentalDuration - 14) / 7)) + (toy.RentPricePerDay.GetValueOrDefault() * ((rentalDuration - 14) % 7));
                }

                totalMoney += rentalPrice * orderDetail.Quantity;

                var newOrderDetail = new OrderDetail
                {
                    OrderId = dto.OrderId,
                    ToyId = orderDetail.ToyId,
                    Quantity = orderDetail.Quantity,
                    Price = rentalPrice,
                    IsRental = true,
                    RentalDate = dto.RentalDate,
                    ReturnDate = dto.ReturnDate
                };

                await _unitOfWork.OrderRepository.AddOrderDetailAsync(newOrderDetail);
            }

            var order = await _unitOfWork.OrderRepository.GetByIdAsync(dto.OrderId);
            order.TotalMoney += totalMoney;
            await _unitOfWork.SaveAsync();
            return Result.Success();
        }

        public async Task<dynamic> CreateSaleOrderDetail(CreateSaleOrderDetailDTO dto)
        {
            var orderDetails = await _unitOfWork.OrderRepository.GetByOrderIdAsync(dto.OrderId);
            var totalMoney = 0m;

            foreach (var orderDetail in orderDetails)
            {
                var toy = await _unitOfWork.ToyRepository.GetByIdAsync(orderDetail.ToyId);
                if (toy == null) return Result.Failure(new Error("ToyNotFound", "Toy not found!"));

                totalMoney += toy.BuyPrice.GetValueOrDefault() * orderDetail.Quantity;

                var newOrderDetail = new OrderDetail
                {
                    OrderId = dto.OrderId,
                    ToyId = orderDetail.ToyId,
                    Quantity = orderDetail.Quantity,
                    Price = toy.BuyPrice.GetValueOrDefault(),
                    IsRental = false
                };

                await _unitOfWork.OrderRepository.AddOrderDetailAsync(newOrderDetail);
            }

            var order = await _unitOfWork.OrderRepository.GetByIdAsync(dto.OrderId);
            order.TotalMoney += totalMoney;
            await _unitOfWork.SaveAsync();
            return Result.Success();
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
            if(order == null) return Result.Failure(OrderErrors.OrderIsNull);
            if(confirmOrderDTO.StatusId == 9)
            {
                order.StatusId = confirmOrderDTO.StatusId;
                await _unitOfWork.SaveAsync();
                return Result.Success();
            }
            if(confirmOrderDTO.StatusId == 2)
            {
                if(string.IsNullOrEmpty(confirmOrderDTO.Shipper) || string.IsNullOrEmpty(confirmOrderDTO.ShipperPhone))
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
