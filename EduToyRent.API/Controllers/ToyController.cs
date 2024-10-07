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
    }
}
