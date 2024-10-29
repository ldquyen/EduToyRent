
using Azure;
using EduToyRent.API.Helper;
using EduToyRent.Service.Common;
using EduToyRent.Service.DTOs.AccountDTO;
using EduToyRent.Service.DTOs.CartDTO;
using EduToyRent.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
        [Authorize(Policy = "UserOnly")]        //.get rent cart
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("rental-cart")]
		public async Task<IActionResult> GetCartForRent()
		{
			try
			{
				CurrentUserObject currentUserObject = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
				Result response = await _cartService.GetCart(currentUserObject.AccountId, true);
				if(response.IsSuccess) return Ok(response);
				return BadRequest(response);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
			
		}

        [Authorize(Policy = "UserOnly")]        //.get sale cart
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("sale-cart")]
        public async Task<IActionResult> GetCartForSale()
        {
            try
            {
                CurrentUserObject currentUserObject = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
                Result response = await _cartService.GetCart(currentUserObject.AccountId, false);
                if (response.IsSuccess) return Ok(response);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [Authorize(Policy = "UserOnly")]        //.add item to cart
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("add-item-to-cart")]
		public async Task<IActionResult> AddItemToCart([FromForm] GetCartRequest request)
		{
			try
			{
				CurrentUserObject currentUserObject = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
				Result result = await _cartService.AddItemToCart(request, currentUserObject.AccountId);
                if (result.IsSuccess) return Ok(result);
                return BadRequest(result);
            } catch (Exception ex) 
			{
				return StatusCode(500, ex.Message);
			};
			
		}

        [Authorize(Policy = "UserOnly")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("remove-items-from-cart")]
        public async Task<IActionResult> RemoveItemsFromCart([FromBody] List<int> itemIdList)
        {
			bool result = await _cartService.RemoveItemsFromCart(itemIdList);
			if (!result) return StatusCode(500);
			return Ok();
        }
    }
}
