using AutoMapper;
using EduToyRent.DAL.Entities;
using EduToyRent.Repository.Interfaces;
using EduToyRent.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Net.payOS;
using Microsoft.Extensions.Configuration;
using Net.payOS.Types;
using EduToyRent.Service.Common;
using EduToyRent.Service.DTOs.OrderDTO;
using EduToyRent.Service.Exceptions;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EduToyRent.Service.Services
{
    public class PayOSService : IPayOSService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly PayOS payOS;

        public PayOSService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            var clientId = _configuration["PayOSSettings:ClientId"];
            var apiKey = _configuration["PayOSSettings:ApiKey"];
            var checksumKey = _configuration["PayOSSettings:ChecksumKey"];
            payOS = new PayOS(clientId, apiKey, checksumKey);
        }
        public async Task<dynamic> CreatePaymentLinkForSale(int orderId)
        {

            var orderNow = await _unitOfWork.OrderRepository.GetAsync(x => x.OrderId == orderId, includeProperties: "Account,StatusOrder");
            if (orderNow == null)
                return Result.Failure(OrderErrors.OrderIsNull);
            if (orderNow.IsRentalOrder) return Result.Failure(PaymentErrors.WrongPayment);
            var odList = await _unitOfWork.OrderDetailRepository.GetAllAsync(x => x.OrderId == orderNow.OrderId, includeProperties: "Toy", 1, 100);
            var odSaleList = _mapper.Map<List<ODSaleDTO>>(odList);
            List<ItemData> items = new List<ItemData>();
            foreach (var odSale in odSaleList)
            {
                ItemData item = new ItemData(odSale.ToyName, odSale.Quantity, (int)odSale.Price);
                items.Add(item);
            }
            var domain = "http://localhost:3000/";


            PaymentData paymentData = new PaymentData(
                orderCode: orderNow.OrderId,
                amount: (int)orderNow.FinalMoney,
                description: $"Payment for order sale {orderNow.OrderId}",
                items: items,
                cancelUrl: domain,
                returnUrl: domain,

                buyerName: orderNow.Account.AccountName
                );
            try
            {
                CreatePaymentResult createPayment = await payOS.createPaymentLink(paymentData);
                PaymentLinkInformation paymentLinkInformation = await payOS.getPaymentLinkInformation(orderId);
                Payment payment = new Payment()
                {
                    OrderId = orderId,
                    AccountId = orderNow.Account.AccountId,
                    Amount = paymentLinkInformation.amountPaid,
                    PaymentMethod = "PayOS",
                    Status = 0,
                    TransactionId = paymentLinkInformation.id,
                    TransactionDate = DateTime.Parse(paymentLinkInformation.createdAt),
                    BankCode = "",
                    ResponseCode = ""
                };
                await _unitOfWork.PaymentRepository.AddAsync(payment);
                await _unitOfWork.SaveAsync();
                return Result.SuccessWithObject(createPayment.checkoutUrl);
            }
            catch (Exception ex)
            {
                return Result.Failure(PaymentErrors.PaymentError);
            }
        }

        public async Task<dynamic> CreatePaymentLinkForRent(int orderId)
        {

            var orderNow = await _unitOfWork.OrderRepository.GetAsync(x => x.OrderId == orderId, includeProperties: "Account,StatusOrder");
            if (orderNow == null)
                return Result.Failure(OrderErrors.OrderIsNull);
            if (!orderNow.IsRentalOrder) return Result.Failure(PaymentErrors.WrongPayment);
            var odList = await _unitOfWork.OrderDetailRepository.GetAllAsync(x => x.OrderId == orderNow.OrderId, includeProperties: "Toy", 1, 100);
            var odRentList = _mapper.Map<List<ODRentDTO>>(odList);
            List<ItemData> items = new List<ItemData>();
            foreach (var odSale in odRentList)
            {
                ItemData item = new ItemData(odSale.ToyName, odSale.Quantity, (int)odSale.RentalPrice);
                items.Add(item);
            }
            var domain = "http://localhost:7221";

            PaymentData paymentData = new PaymentData(
                orderCode: orderNow.OrderId,
                amount: (int)orderNow.FinalMoney,
                description: $"Payment for order rent {orderNow.OrderId}",
                items: items,
                cancelUrl: domain,
                returnUrl: domain + "/swagger/index.html",

                buyerName: orderNow.Account.AccountName
                );
            try
            {
                CreatePaymentResult createPayment = await payOS.createPaymentLink(paymentData);
                PaymentLinkInformation paymentLinkInformation = await payOS.getPaymentLinkInformation(orderId);
                Payment payment = new Payment()
                {
                    OrderId = orderId,
                    AccountId = orderNow.Account.AccountId,
                    Amount = paymentLinkInformation.amountPaid,
                    PaymentMethod = "PayOS",
                    Status = 0,
                    TransactionId = paymentLinkInformation.id,
                    TransactionDate = DateTime.Parse(paymentLinkInformation.createdAt),
                    BankCode = "",
                    ResponseCode = ""
                };
                await _unitOfWork.PaymentRepository.AddAsync(payment);
                await _unitOfWork.SaveAsync();
                return Result.SuccessWithObject(createPayment.checkoutUrl);
            }
            catch (Exception ex)
            {
                return Result.Failure(PaymentErrors.PaymentError);
            }
        }
        public async Task<dynamic> GetPaymentLinkInformation(int orderId)
        {
            PaymentLinkInformation paymentLinkInformation = await payOS.getPaymentLinkInformation(orderId);
            if (paymentLinkInformation != null)
            {
                int status = 0;
                if (paymentLinkInformation.status == "Pending") status = 0;
                else if (paymentLinkInformation.status == "PAID") status = 1;
                else if (paymentLinkInformation.status == "CANCELLED") status = 2;
                else status = 3;
                var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
                Payment payment = new Payment()
                {
                    OrderId = orderId,
                    AccountId = order.AccountId,
                    Amount = paymentLinkInformation.amountPaid,
                    PaymentMethod = "PayOS",
                    Status = status,
                    TransactionId = paymentLinkInformation.id,
                    TransactionDate = DateTime.Parse(paymentLinkInformation.createdAt),
                    BankCode = "",
                    ResponseCode = ""
                };
                await _unitOfWork.PaymentRepository.UpdatePayment(payment);
            }
            return Result.SuccessWithObject(paymentLinkInformation);
        }
      

        public async Task<dynamic> CancelPaymentLink(int orderId)
        {
            PaymentLinkInformation cancelledPaymentLinkInfo = await payOS.cancelPaymentLink(orderId);
            return Result.Success();
        }

    }
}
