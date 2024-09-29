using EduToyRent.BLL.DTOs.AccountDTO;
using EduToyRent.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduToyRent.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] SignupAccountDTO signupAccountDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _accountService.SignUpAccount(signupAccountDTO);
                if (result.IsSuccess)
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
