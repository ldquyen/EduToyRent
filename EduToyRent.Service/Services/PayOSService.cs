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
            if(orderNow.IsRentalOrder) return Result.Failure(PaymentErrors.WrongPayment);
            var odList = await _unitOfWork.OrderDetailRepository.GetAllAsync(x => x.OrderId == orderNow.OrderId, includeProperties: "Toy", 1, 100);
            var odSaleList = _mapper.Map<List<ODSaleDTO>>(odList);
            List<ItemData> items = new List<ItemData>();
            foreach (var odSale in odSaleList)
            {
                ItemData item = new ItemData(odSale.ToyName, odSale.Quantity, (int)odSale.Price);
                items.Add(item);
            }
            var domain = "http://localhost:7221";

           
            PaymentData paymentData = new PaymentData(
                orderCode: orderNow.OrderId,
                amount: (int)orderNow.FinalMoney,
                description: $"Payment for order {orderNow.OrderId}",
                items: items,
                cancelUrl: domain,
                returnUrl: domain + "/swagger/index.html",

                buyerName: orderNow.Account.AccountName
                );
            try
            {
                CreatePaymentResult createPayment = await payOS.createPaymentLink(paymentData);
                PaymentLinkInformation paymentLinkInformation = await payOS.getPaymentLinkInformation(orderId);


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
            return Result.SuccessWithObject(paymentLinkInformation);
        }

        public async Task<dynamic> CancelPaymentLink(int orderId)
        {
            PaymentLinkInformation cancelledPaymentLinkInfo = await payOS.cancelPaymentLink(orderId);
            return Result.Success();
        }

    }
}

/*
 http://localhost:7221/?
code=00
id=bf1b1e12945742a29dfdfec0b8069881
cancel=false                                success
status=PAID&
orderCode=14
 */

/* Payment Data
 
{
  "orderCode": 0,
  "amount": 0,
  "description": "string",
  "items": [
    {
      "name": "string",
      "quantity": 0,
      "price": 0
    }
  ],
  "cancelUrl": "string",
  "returnUrl": "string",
  "signature": "string",
  "buyerName": "string",
  "buyerEmail": "string",
  "buyerPhone": "string",
  "buyerAddress": "string",
  "expiredAt": 0
}
 
orderCode	 :long
amount		 :int
description	 :String
items	     :List<ItemData>
cancelUrl    :String
returnUrl    :String

orderCode	Mã đơn hàng	                                                                                                        long
amount	Số tiền thanh toán	                                                                                                    int
description	Mô tả cho thanh toán, được dùng làm nội dung chuyển khoản	                                                        String
items	Danh sách các sản phẩm	                                                                                                List<ItemData>
cancelUrl	Đường dẫn sẽ được chuyển tiếp đến trang web hoặc ứng dụng của bạn khi người dùng bấm hủy đơn hàng	                String
returnUrl	Đường dẫn sẽ được chuyển tiếp đến trang web hoặc ứng dụng của bạn khi người dùng đã thanh toán đơn hàng thành công	String

 */