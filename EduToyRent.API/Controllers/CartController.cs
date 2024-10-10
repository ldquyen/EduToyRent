using EduToyRent.BLL.DTOs.CartDTO;
using EduToyRent.BLL.Interfaces;
using EduToyRent.DAL.Entities;
using EduToyRent.DAL.Entities.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduToyRent.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private ICartService _cartService;

        // create constructor later

        [HttpGet("get-cart")]
        public async Task<IActionResult> GetCartCookie([FromRoute] int accountId)
        {
            GetCartResponse response = await _cartService.GetCart(accountId);
            if (response == null) {
                return NotFound();
            };
            return Ok(response);
        }

        [HttpPost("add-item-to-cart")]
        public async Task<IActionResult> AddItemToCart([FromBody] GetCartRequest request)
        {
            bool result = await _cartService.AddItemToCart(request);
            if (result == false)
                return StatusCode(500);
            return Ok();
        }

        [HttpDelete("remove-items-from-cart")]
        public async Task<IActionResult> RemoveItemsFromCart([FromBody] List<int> itemIdList)
        {
			bool result = await _cartService.RemoveItemsFromCart(itemIdList);
			if (!result) return StatusCode(500);
			return Ok();
        }
    }
}
