using EduToyRent.Service.Common;
using EduToyRent.Service.DTOs;
using EduToyRent.Service.DTOs.ToyDTO;
using EduToyRent.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using EduToyRent.Service.DTOs.AccountDTO;
using EduToyRent.API.Helper;


namespace EduToyRent.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToyController : ControllerBase
    {
        private readonly IToyService _toyService;
        public ToyController(IToyService toyService)
        {
            _toyService = toyService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetToyByToyId(int id)
        {
            var result = await _toyService.GetToyByToyId(id);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPut("update{id}")] //Update Toy Information - Phu
        public async Task<IActionResult> UpdateToyInfo(int id, [FromBody] UpdateToyDTO updateToyDTO)
        {
            var result = await _toyService.UpdateToyInfo(id, updateToyDTO);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("detail/rent/{toyId}")] //View Toy Sale/Rent Detail - Phu
        public async Task<IActionResult> ViewToyDetailForRent(int toyId)
        {
            var result = await _toyService.ViewToyDetailForRent(toyId);
            return Ok(result);
        }

        [HttpGet("detail/sale/{toyId}")] //View Toy Sale/Rent Detail - Phu
        public async Task<IActionResult> ViewToyDetailForSale(int toyId)
        {
            var result = await _toyService.ViewToyDetailForSale(toyId);
            return Ok(result);
        }

        [HttpGet("view-toys/rent")] //Get Toys Sale/Rent - Phu 
        public async Task<ActionResult<Pagination<ViewToyForRentDTO>>> ViewToysForRent([FromQuery] string search = null,
        [FromQuery] string sort = null,
        [FromQuery] int pageIndex = 0,
        [FromQuery] int pageSize = 10)
        {
            var result = await _toyService.ViewToysForRent(search, sort, pageIndex, pageSize);
            return Ok(result);
        }

        [HttpGet("view-toys/sale")] //Get Toys Sale/Rent - Phu 
        public async Task<ActionResult<Pagination<ViewToyForSaleDTO>>> ViewToysForSale([FromQuery] string search = null,
        [FromQuery] string sort = null,
        [FromQuery] int pageIndex = 0,
        [FromQuery] int pageSize = 10)
        {
            var result = await _toyService.ViewToysForSale(search, sort, pageIndex, pageSize);
            return Ok(result);
        }
        [Authorize(Policy = "SupplierOnly")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("view-toys/for-rent-supplier")]
        public async Task<ActionResult<Pagination<ViewToyForRentSupplier>>> ViewToysForRentAccount([FromQuery] string search = null,
       [FromQuery] string sort = null,
       [FromQuery] int pageIndex = 0,
       [FromQuery] int pageSize = 10)
        {
            CurrentUserObject currentUserObject = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            var result = await _toyService.ViewToysForRentAccount(search, sort, pageIndex, pageSize, currentUserObject);
            return Ok(result);
        }
        [Authorize(Policy = "SupplierOnly")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("view-toys/sell-supplier")]
        public async Task<ActionResult<Pagination<ViewToyForSellSupplier>>> ViewToysSellAccount([FromQuery] string search = null,
        [FromQuery] string sort = null,
        [FromQuery] int pageIndex = 0,
        [FromQuery] int pageSize = 10)
        {
            CurrentUserObject currentUserObject = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            var result = await _toyService.ViewToysForSellAccount(search, sort, pageIndex, pageSize, currentUserObject);
            return Ok(result);
        }
    }
}
