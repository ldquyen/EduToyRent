using EduToyRent.Service.DTOs.ToyDTO;
using EduToyRent.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateToyInfo(int id, [FromBody] UpdateToyDTO updateToyDTO)
        {
            var result = await _toyService.UpdateToyInfo(id, updateToyDTO);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("ViewToysRent")]
        public async Task<IActionResult> GetToysForRent(int pageIndex = 0, int pageSize = 10)
        {
            var result = await _toyService.ViewToysForRent(pageIndex, pageSize);
            return Ok(result);
        }

        [HttpGet("ViewToysSale")]
        public async Task<IActionResult> GetToysForSale(int pageIndex = 0, int pageSize = 10)
        {
            var result = await _toyService.ViewToysForSale(pageIndex, pageSize);
            return Ok(result);
        }

        [HttpGet("detail/rent/{toyId}")]
        public async Task<IActionResult> ViewToyDetailForRent(int toyId)
        {
            var result = await _toyService.ViewToyDetailForRent(toyId);
            return Ok(result);
        }

        [HttpGet("detail/sale/{toyId}")]
        public async Task<IActionResult> ViewToyDetailForSale(int toyId)
        {
            var result = await _toyService.ViewToyDetailForSale(toyId);
            return Ok(result);
        }

        [HttpGet("search/rent")]
        public async Task<IActionResult> SearchRentToys(string keyword, [Required] int pageIndex, [Required] int pageSize)
        {
            if (pageIndex < 1 || pageSize < 1)
            {
                return BadRequest("PageIndex and PageSize must be greater than 0.");
            }
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            var result = await _toyService.SearchRentByName(keyword, pageIndex, pageSize);

            return Ok(result);
        }

        [HttpGet("search/sale")]
        public async Task<IActionResult> SearchSaleToys(string keyword, [Required] int pageIndex,  [Required] int pageSize)
        {
            if (pageIndex < 1 || pageSize < 1)
            {
                return BadRequest("PageIndex and PageSize must be greater than 0.");
            }
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            var result = await _toyService.SearchSaleByName(keyword, pageIndex, pageSize);

            return Ok(result);
        }


        [HttpGet("SortToysForSale")]
        public async Task<IActionResult> SortToysForSale(string sortBy, int pageIndex = 0, int pageSize = 10)
        {
            var result = await _toyService.SortToysForSale(sortBy, pageIndex, pageSize);
            return Ok(result);
        }
        [HttpGet("SortToysForRent")]
        public async Task<IActionResult> SortToysForRent(string sortBy, int pageIndex = 0, int pageSize = 10)
        {
            var result = await _toyService.SortToysForRent(sortBy, pageIndex, pageSize);
            return Ok(result);
        }
    }
}
