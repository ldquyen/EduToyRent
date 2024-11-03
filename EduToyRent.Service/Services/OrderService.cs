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
            foreach (var toy in createOrderDTO.ToyList)
            {
                if (await _unitOfWork.ToyRepository.CheckQuantity(toy.Quantity, toy.ToyId)) return Result.Failure(ToyErrors.QuantityHigher);
            }
            if (createOrderDTO.IsRentalOrder)
            {
                if (string.IsNullOrEmpty(createOrderDTO.RentalDate.ToString()) || string.IsNullOrEmpty(createOrderDTO.ReturnDate.ToString()))
                    return Result.Failure(ToyErrors.RentalDateToyNull);
            }
            if (createOrderDTO.VoucherId != 0)
            {
                if (!await _unitOfWork.AccountVoucherRepository.CheckValidAccountVoucher(createOrderDTO.VoucherId, currentUserObject.AccountId))
                    return Result.Failure(VoucherErrors.CannotUseVoucher);
            }
            var order = _mapper.Map<Order>(createOrderDTO);
            order.AccountId = currentUserObject.AccountId;
            order.StatusId = 1;
            order.PaymentStatus = false;
            order.TotalMoney = 0;
            order.Discount = 0;
            order.FinalMoney = 0;
            order.OrderDate = DateTime.Now;
            var save = await _unitOfWork.OrderRepository.AddAsync(order);
            await _unitOfWork.SaveAsync();
            int id = order.OrderId;

            if (createOrderDTO.IsRentalOrder)
            {
                if (await CreateOrderDetailForRent(createOrderDTO, id))
                {
                    var odList = await _unitOfWork.OrderDetailRepository.GetAllAsync(x => x.OrderId == id, null, 1, 100);
                    foreach (var odDetail in odList)
                    {
                        odDetail.RentalPrice = await _unitOfWork.ToyRepository.GetMoneyRentByToyId(odDetail.ToyId, odDetail.Quantity, createOrderDTO.RentalDate, createOrderDTO.ReturnDate);
                        await _unitOfWork.ToyRepository.SubtractQuantity(odDetail.ToyId, odDetail.Quantity);
                        var update1 = await _unitOfWork.OrderDetailRepository.UpdateAsync(odDetail);
                    }
                    await _unitOfWork.SaveAsync();
                    order.TotalMoney = await _unitOfWork.OrderDetailRepository.GetTotalMoney(id);
                    decimal discountMoney = await _unitOfWork.VoucherRepository.UseVoucherReturnPrice(createOrderDTO.VoucherId, order.TotalMoney);
                    if (discountMoney != order.TotalMoney)
                    {
                        order.FinalMoney = discountMoney;
                        await _unitOfWork.AccountVoucherRepository.UseVoucher(createOrderDTO.VoucherId, currentUserObject.AccountId);
                    }
                    else order.FinalMoney = order.TotalMoney;
                    var update2 = await _unitOfWork.OrderRepository.UpdateAsync(order);
                    await _unitOfWork.SaveAsync();
                    return Result.SuccessWithObject(id);
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
                        await _unitOfWork.ToyRepository.SubtractQuantity(odDetail.ToyId, odDetail.Quantity);
                        var update3 = await _unitOfWork.OrderDetailRepository.UpdateAsync(odDetail);
                    }
                    await _unitOfWork.SaveAsync();
                    order.TotalMoney = await _unitOfWork.OrderDetailRepository.GetTotalMoney(id);
                    decimal discountMoney = await _unitOfWork.VoucherRepository.UseVoucherReturnPrice(createOrderDTO.VoucherId, order.TotalMoney);
                    if (discountMoney != order.TotalMoney)
                    {
                        order.FinalMoney = discountMoney;
                        await _unitOfWork.AccountVoucherRepository.UseVoucher(createOrderDTO.VoucherId, currentUserObject.AccountId);
                    }
                    else order.FinalMoney = order.TotalMoney;
                    var update4 = await _unitOfWork.OrderRepository.UpdateAsync(order);
                    await _unitOfWork.SaveAsync();
                    return Result.SuccessWithObject(id);
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
                    var save = await _unitOfWork.OrderDetailRepository.AddAsync(od);
                }
                await _unitOfWork.SaveAsync();
                return true;
            }
            else
                return false;
        }
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
                    var save = await _unitOfWork.OrderDetailRepository.AddAsync(od);
                }
                await _unitOfWork.SaveAsync();
                return true;
            }
            else
                return false;
        }
        public async Task<dynamic> GetOrderDetailForUser(int orderId, int accountId)
        {
            var order = await _unitOfWork.OrderRepository.GetAsync(x => x.OrderId == orderId, includeProperties: "Account,StatusOrder");
            if (order.AccountId != accountId)
                return Result.Failure(OrderErrors.OrderOfAccountIsWrong);
            var odList = await _unitOfWork.OrderDetailRepository.GetAllAsync(x => x.OrderId == orderId, includeProperties: "Toy", 1, 20);
            if (order.IsRentalOrder)
            {
                var responseOrderDTO = _mapper.Map<ResponseOrderRentDetailForUserDTO>(order);
                responseOrderDTO.OrdersDetail = _mapper.Map<List<ODRentDTO>>(odList);
                return Result.SuccessWithObject(responseOrderDTO);
            }
            else
            {
                var responseOrderDTO = _mapper.Map<ResponseOrderSaleDetailForUserDTO>(order);
                responseOrderDTO.OrdersDetail = _mapper.Map<List<ODSaleDTO>>(odList);
                return Result.SuccessWithObject(responseOrderDTO);
            }
        }
        public async Task<dynamic> GetOrderOfAccount(int accountId, bool isRent, int status)
        {
            if (isRent)
            {
                var orders = await _unitOfWork.OrderRepository.GetOrderRentForUser(accountId, status);
                var list = _mapper.Map<List<ResponseOrderForUser>>(orders);
                return Result.SuccessWithObject(list);
            }
            else
            {
                var orders = await _unitOfWork.OrderRepository.GetOrderSaleForUser(accountId, status);
                var list = _mapper.Map<List<ResponseOrderForUser>>(orders);
                return Result.SuccessWithObject(list);
            }
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
            if (order.StatusId == 1)
            {
                if (confirmOrderDTO.StatusId == 9)
                {
                    order.StatusId = confirmOrderDTO.StatusId;
                    await _unitOfWork.SaveAsync();
                    return Result.Success();
                }else if (confirmOrderDTO.StatusId == 2)
                {
                    if (string.IsNullOrEmpty(confirmOrderDTO.Shipper) || string.IsNullOrEmpty(confirmOrderDTO.ShipperPhone))
                        return Result.Failure(OrderErrors.ShiperInfo);
                    order.Shipper = confirmOrderDTO.Shipper;
                    order.ShipperPhone = confirmOrderDTO.ShipperPhone;
                    order.StatusId = confirmOrderDTO.StatusId;
                    var update = await _unitOfWork.OrderRepository.UpdateAsync(order);
                    await _unitOfWork.SaveAsync();
                    return Result.Success();
                }else return Result.Failure(OrderErrors.ConfirmStatus);
            }
            else
                return Result.Failure(OrderErrors.ConfirmStatus);
        }
        public async Task<dynamic> ViewOrderRentDetailForSupplier(int accountId)
        {
            var odList = await _unitOfWork.OrderDetailRepository.GetOrderRentDetailForSupplier(accountId);
            var list = _mapper.Map<List<ReponseOrderRentForSupplierDTO>>(odList);
            return list;
        }
        public async Task<dynamic> ViewOrderSaleDetailForSupplier(int accountId)
        {
            var odList = await _unitOfWork.OrderDetailRepository.GetOrderSaleDetailForSupplier(accountId);
            var list = _mapper.Map<List<ReponseOrderSaleForSupplierDTO>>(odList);
            return list;
        }
        public async Task<dynamic> SupplierConfirmShip(int orderDetailId)
        {
            var od = await _unitOfWork.OrderDetailRepository.GetByIdAsync(orderDetailId);
            var listid = await _unitOfWork.OrderDetailRepository.GetOrderDetailIdByOrderId(od.OrderId);
            if (!await _unitOfWork.ShipDateRepository.CheckShip(od.OrderDetailId))
            {
                await _unitOfWork.ShipDateRepository.CreateShipDate(od);
                if (await _unitOfWork.ShipDateRepository.CheckAllShip(listid))
                    await _unitOfWork.OrderRepository.UpdateOrderStatus(od.OrderId, 3);
            }
            return Result.Success();
        }
        public async Task<dynamic> CompleteOrder(int orderId, int accountId)
        {
            var od = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
            if (od.AccountId != accountId)
            {
                return Result.Failure(OrderErrors.OrderOfAccountIsWrong);
            }

            if (!od.IsRentalOrder)
            {
                if (od.StatusId == 3)
                {
                    od.StatusId = 8;
                    var update = await _unitOfWork.OrderRepository.UpdateAsync(od);
                    await _unitOfWork.SaveAsync();
                    return Result.Success();
                }
                else
                {
                    return Result.Failure(OrderErrors.WrongOrderStatus);
                }
            }
            else
            {
                if (od.StatusId == 3)
                {
                    od.StatusId = 7;
                    var update = await _unitOfWork.OrderRepository.UpdateAsync(od);
                    await _unitOfWork.SaveAsync();
                    return Result.Success();
                }
                else
                {
                    return Result.Failure(OrderErrors.OrderMustbeReturn);
                }
            }
        }
        public async Task<dynamic> ReturnOrderRent(int orderId, int accountId)
        {
            var od = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
            if (od.AccountId != accountId)
                return Result.Failure(OrderErrors.OrderOfAccountIsWrong);
            if (od.IsRentalOrder)
            {
                if(od.StatusId == 7)
                {
                    od.StatusId = 5;
                    var update = await _unitOfWork.OrderRepository.UpdateAsync(od);
                    await _unitOfWork.SaveAsync();
                    return Result.Success();
                }
                else
                {
                    return Result.Failure(OrderErrors.OrderMustbeReturn);
                }
            }
            else
                return Result.Failure(OrderErrors.WrongOrderStatus);
        }

        public async Task<dynamic> GetReturnOrderForStaff()
        {
            return 0;
        }

    }
}
