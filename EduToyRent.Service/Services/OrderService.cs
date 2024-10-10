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
            int id = order.OrderId; // => tiếp tục tạo orderDetail
            return Result.Success();
        }

        public async Task<dynamic> GetAllOrder()
        {
            
        }
    }
}
