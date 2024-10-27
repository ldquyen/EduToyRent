using EduToyRent.API.Helper;
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

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO createOrderDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            CurrentUserObject currentUserObject = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            Result result = await _orderService.CreateOrder(currentUserObject, createOrderDTO);
            if (result.IsSuccess) return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("/user/{orderId}")]
        public async Task<IActionResult> GetOrderForUser(int orderId)
        {
            Result result = await _orderService.GetOrderDetailForUser(orderId);
            return Ok(result);
        }

        [HttpGet("/staff")]
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

        [Authorize(Policy = "SupplierOnly")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("rent-details-for-supplier")]
        public async Task<IActionResult> ViewOrderRentDetailForSupplier()
        {
            CurrentUserObject currentUserObject = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            var result = await _orderService.ViewOrderRentDetailForSupplier(currentUserObject.AccountId);
            return Ok(result);
        }

        [Authorize(Policy = "SupplierOnly")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("sale-details-for-supplier")]
        public async Task<IActionResult> ViewOrderSaleDetailForSupplier()
        {
            CurrentUserObject currentUserObject = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            var result = await _orderService.ViewOrderSaleDetailForSupplier(currentUserObject.AccountId);
            return Ok(result);
        }


        [HttpPut("supplier-confirm-ship/{orderDetailId}")]
        public async Task<IActionResult> SupplierConfirmShip(int orderDetailId)
        {
            var result = await _orderService.SupplierConfirmShip(orderDetailId);
            return Ok(result);

        }
    }
}
