using EduToyRent.API.Helper;
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
    }
}
