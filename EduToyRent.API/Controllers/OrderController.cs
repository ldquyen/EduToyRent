﻿using EduToyRent.API.Helper;
using EduToyRent.Service.Common;
using EduToyRent.Service.DTOs.AccountDTO;
using EduToyRent.Service.DTOs.OrderDTO;
using EduToyRent.Service.Interfaces;
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

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromForm] CreateOrderDTO createOrderDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            CurrentUserObject currentUserObject = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            Result result = await _orderService.CreateOrder(currentUserObject, createOrderDTO);
            if(result.IsSuccess) return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("rental-order-detail")]
        public async Task<IActionResult> CreateRentOrderDetail([FromForm] CreateRentOrderDetailDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _orderService.CreateRentOrderDetail(dto);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result.Errors);
        }

        [HttpPost("CreateSaleOrderDetail")]
        public async Task<IActionResult> CreateSaleOrderDetail([FromForm] CreateSaleOrderDetailDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _orderService.CreateSaleOrderDetail(dto);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result.Errors);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderForStaff(int page = 1)
        {
            Result result = await _orderService.GetAllOrderForStaff(page);
            return Ok(result);
        }

        [HttpPut("confirm")]
        public async Task<IActionResult> ConfirmOrder([FromForm] ConfirmOrderDTO confirmOrderDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Result result = await _orderService.ConfirmOrder(confirmOrderDTO);
            if (result.IsSuccess) return Ok(result);
            return BadRequest(result);
        }
    }
}
