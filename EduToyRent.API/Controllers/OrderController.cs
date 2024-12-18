﻿using EduToyRent.API.Helper;
using EduToyRent.DAL.Entities;
using EduToyRent.Service.Common;
using EduToyRent.Service.DTOs.AccountDTO;
using EduToyRent.Service.DTOs.OrderDTO;
using EduToyRent.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduToyRent.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize(Policy = "UserOnly")]    //.Create order
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO createOrderDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            CurrentUserObject currentUserObject = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            Result result = await _orderService.CreateOrder(currentUserObject, createOrderDTO);
            if (result.IsSuccess) return Ok(result);
            return BadRequest(result);
        }

        [Authorize(Policy = "UserOnly")]    //.View order of that user
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("user")]
        public async Task<IActionResult> GetOrderOfAccount([FromQuery] bool isRent, int status)
        {
            CurrentUserObject currentUserObject = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            var result = await _orderService.GetOrderOfAccount(currentUserObject.AccountId, isRent, status);
            return Ok(result);
        }

        [Authorize(Policy = "UserOnly")]    //.View detail that order
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("order-detail/user/{orderId}")]
        public async Task<IActionResult> GetOrderDetailForUser(int orderId)
        {
            CurrentUserObject currentUserObject = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            Result result = await _orderService.GetOrderDetailForUser(orderId, currentUserObject.AccountId);
            return Ok(result);
        }

        [Authorize(Policy = "StaffOnly")]   //.Staff view all new order
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("staff")]
        public async Task<IActionResult> GetOrderForStaff(int page = 1)
        {
            Result result = await _orderService.GetAllOrderForStaff(page);
            return Ok(result);
        }

        [Authorize(Policy = "StaffOnly")]   //.Staff confirm that order(status == 1) is oke or cancel that order
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("staff/confirm")]
        public async Task<IActionResult> ConfirmOrder([FromForm] ConfirmOrderDTO confirmOrderDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Result result = await _orderService.ConfirmOrder(confirmOrderDTO);
            if (result.IsSuccess) return Ok(result);
            return BadRequest(result);
        }

        [Authorize(Policy = "SupplierOnly")]    //.Supplier view rent order
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("rent-details-for-supplier")]
        public async Task<IActionResult> ViewOrderRentDetailForSupplier()
        {
            CurrentUserObject currentUserObject = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            var result = await _orderService.ViewOrderRentDetailForSupplier(currentUserObject.AccountId);       //fix1
            return Ok(result);
        }

        [Authorize(Policy = "SupplierOnly")]    //.Supplier view sale order
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] //fix
        [HttpGet("sale-details-for-supplier")]
        public async Task<IActionResult> ViewOrderSaleDetailForSupplier()
        {
            CurrentUserObject currentUserObject = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            var result = await _orderService.ViewOrderSaleDetailForSupplier(currentUserObject.AccountId);
            return Ok(result);
        }

        [Authorize(Policy = "SupplierOnly")]    //.Supplier view info order to ship
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("information-ship/{orderDetailId}")]          
        public async Task<IActionResult> SupplierViewInfoToShip(int orderDetailId)
        {
            var result = await _orderService.GetInfoShipForSupplier(orderDetailId);
            return Ok(result);

        }

        [Authorize(Policy = "SupplierOnly")]    //.Supplier confirm order
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("supplier-confirm-ship")]          //api cu: supplier-confirm-ship/{orderDetailId}
        public async Task<IActionResult> SupplierConfirmShip(SupplierConfirmDTO supplierConfirmDTO)
        {
            var result = await _orderService.SupplierConfirmShip(supplierConfirmDTO);
            return Ok(result);

        }

        [Authorize(Policy = "UserOnly")]    //.User complete order
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] 
        [HttpPut("user-complete-order/{orderId}")]
        public async Task<IActionResult> UserCompleteOrder(int orderId)
        {
            CurrentUserObject currentUserObject = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            var result = await _orderService.CompleteOrder(orderId, currentUserObject.AccountId);
            return Ok(result);
        }
        [Authorize(Policy = "UserOnly")]    //.User return order
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("user-return-order/{orderId}")]
        public async Task<IActionResult> UserReturnOrder(int orderId)
        {
            CurrentUserObject currentUserObject = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            var result = await _orderService.ReturnOrderRent(orderId, currentUserObject.AccountId);
            return Ok(result);
        }


    }
}
