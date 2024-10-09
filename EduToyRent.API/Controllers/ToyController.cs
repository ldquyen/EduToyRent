using EduToyRent.Service.DTOs;
using EduToyRent.Service.DTOs.ToyDTO;
using EduToyRent.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("view")]
        public async Task<IActionResult> ViewToys(int pageIndex = 0, int pageSize = 10)
        {
            var result = await _toyService.ViewToys(pageIndex, pageSize);
            return Ok(result);
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> ViewToyDetail(int id)
        {
            var result = await _toyService.ViewToyDetail(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchToys(string keyword, int pageIndex = 0, int pageSize = 10)
        {
            var result = await _toyService.SearchToys(keyword, pageIndex, pageSize);
            return Ok(result);
        }

        [HttpGet("sort")]
        public async Task<IActionResult> SortToys(string sortBy, int pageIndex = 0, int pageSize = 10)
        {
            var result = await _toyService.SortToys(sortBy, pageIndex, pageSize);
            return Ok(result);
        }

        
    }
}
