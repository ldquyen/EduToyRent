using EduToyRent.Service.DTOs.AccountDTO;
using EduToyRent.Service.DTOs.TokenDTO;
using EduToyRent.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduToyRent.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;

        public AuthController(IAuthService authService, ITokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;
        }
        
        [HttpPost("sign-in")]       //.login
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.LoginAsync(loginDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("renew-token")]   //.renew
        public async Task<IActionResult> RenewToken(RenewTokenDTO renewTokenDTO)
        {
            var token = await _tokenService.RenewTokenAsync(renewTokenDTO);
            return Ok(token);
        }
    }
}
