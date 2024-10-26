using EduToyRent.Service.Common;
using EduToyRent.Service.DTOs;
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

        [HttpGet("view-toys/rent")]
        public async Task<ActionResult<Pagination<ViewToyForRentDTO>>> ViewToysForRent([FromQuery] string search = null,
        [FromQuery] string sort = null,
        [FromQuery] int pageIndex = 0,
        [FromQuery] int pageSize = 10)
        {
            var result = await _toyService.ViewToysForRent(search, sort, pageIndex, pageSize);
            return Ok(result);
        }

        [HttpGet("view-toys/sale")]
        public async Task<ActionResult<Pagination<ViewToyForSaleDTO>>> ViewToysForSale([FromQuery] string search = null,
        [FromQuery] string sort = null,
        [FromQuery] int pageIndex = 0,
        [FromQuery] int pageSize = 10)
        {
            var result = await _toyService.ViewToysForSale(search, sort, pageIndex, pageSize);
            return Ok(result);
        }
    }
}
