
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using EduToyRent.Service.DTOs.AccountDTO;
using EduToyRent.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EduToyRent.API.Helper;
using Google.Apis.Upload;
using EduToyRent.Service.DTOs.ForgotPasswordDTO;
using EduToyRent.Service.Common;


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

        [HttpPost("sign-up")]       //.sign up
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

        [HttpPost("sign-up-supplier")]
        public async Task<IActionResult> SignUpSupplier([FromBody] SignupAccountDTO signupAccountDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _accountService.SignUpAccountToySupplier(signupAccountDTO);
                if (result.IsSuccess)
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Policy = "AdminOnly")]  // khac tri (admin tao tk staff)
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("sign-up-staff")]
        public async Task<IActionResult> SignUpStaff([FromBody] SignupAccountDTO signupAccountDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _accountService.SignUpStaffToySupplier(signupAccountDTO);
                if (result.IsSuccess)
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Policy = "UserOnly")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("edit-profile")]

        public async Task<IActionResult> UpdateProfile([FromBody] EditAccountProfileDTO editAccountProfileDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CurrentUserObject currentUserObject = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
                var result = await _accountService.UpdateProfile(editAccountProfileDTO, currentUserObject);
                if (result.IsSuccess) return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [Authorize(Policy = "UserOnly")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CurrentUserObject currentUserObject = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
                var result = await _accountService.GetProfile(currentUserObject);
                if (result.IsSuccess) return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [Authorize(Policy = "UserOnly")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword(PasswordDTO password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CurrentUserObject currentUserObject = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
                var result = await _accountService.ChangePassword(password, currentUserObject);
                if (result.IsSuccess) return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [Authorize(Policy = "StaffOnly")]  // khac tri (staff xem danh sach cua customer va supplier)
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<IActionResult> GetViewAll(int page = 1 )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _accountService.ViewAllAccount(page);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Policy = "AdminOnly")]  //khac tri (Admin xem tat ca tk cua staff)
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("staff-list")]
        public async Task<IActionResult> GetStaffAccount(int page = 1)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _accountService.ViewAllStaffAccount(page);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Policy = "StaffOnly")]  //khac tri (staff ban tk supplier va customer)
        [HttpPut("ban-user/{accountId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> BanAccount(int accountId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            { 
                var result = await _accountService.BanUserAccount(accountId);
                if (result != null)
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Policy = "AdminOnly")] //khac tri (admin ban tk cua staff)
        [HttpPut("ban-staff/{accountId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> BanStaffAccount(int accountId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _accountService.BanStaffAccount(accountId);
                if (result != null)
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

		[HttpPost("forgot-password")] // (hieu) gui yeu cau reset password
		[AllowAnonymous]
		public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto request)
		{
			if (!ModelState.IsValid) 
				return BadRequest(ModelState); 

			Result result = await _accountService.SendPasswordResetOTP(request);

			if (result.IsFailure) return StatusCode(500, result.Error);
			return Ok();
		}

		[HttpPost("reset-password")] // (hieu) dung OTP reset password
		[AllowAnonymous]
		public async Task<IActionResult> ResetPassword(ResetPasswordDto request)
		{
			if(!ModelState.IsValid)
				return BadRequest(ModelState);

			Result result = await _accountService.ResetPasswordUsingOTP(request);

			if (result.IsFailure) return StatusCode(500, result.Error);
			return Ok();
		}

    }
}
