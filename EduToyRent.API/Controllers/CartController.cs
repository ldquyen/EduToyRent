
using EduToyRent.API.Helper;
using EduToyRent.BLL.Interfaces;
using EduToyRent.Service.DTOs.AccountDTO;
using EduToyRent.Service.DTOs.CartDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduToyRent.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	[Authorize]
    public class CartController : ControllerBase
    {
        private ICartService _cartService;

		public CartController(ICartService cartService)
		{
			_cartService = cartService;
		}

		[HttpGet("get-cart")]
		public async Task<IActionResult> GetCart()
		{
			try
			{
				CurrentUserObject currentUserObject = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
				GetCartResponse response = await _cartService.GetCart(currentUserObject.AccountId);
				if (response == null)
				{
					return NotFound();
				};
				return Ok(response);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
			
		}

		[HttpPost("add-item-to-cart")]
		public async Task<IActionResult> AddItemToCart([FromBody] GetCartRequest request)
		{
			try
			{
				CurrentUserObject currentUserObject = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
				bool result = await _cartService.AddItemToCart(request, currentUserObject.AccountId);
				if (result == false)
					return StatusCode(500);
				return Ok();
			} catch (Exception ex) 
			{
				return StatusCode(500, ex.Message);
			};
			
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
