using EduToyRent.API.Helper;
using EduToyRent.DAL.Entities;
using EduToyRent.Service.DTOs.AccountDTO;
using EduToyRent.Service.DTOs.CategoryDTO;
using EduToyRent.Service.DTOs.VoucherDTO;
using EduToyRent.Service.Interfaces;
using EduToyRent.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduToyRent.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IVoucherService _voucherService;
        public VoucherController(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }
        [Authorize(Policy = "StaffOnly")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("new-voucher")]
        public async Task<IActionResult> CreateVoucher(CreateVoucherDTO createVoucherDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _voucherService.CreateVoucher(createVoucherDTO);
                if (result.IsSuccess) return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [Authorize(Policy = "StaffOnly")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("list-vouchers")]
        public async Task<IActionResult> GetVouchers()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _voucherService.GetListVoucher();
                if (result.IsSuccess) return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [Authorize(Policy = "StaffOnly")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("assign-voucher-to-user")]
        public async Task<IActionResult> GiveVoucherToAccount([FromForm] int accountid, [FromForm] int voucherid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _voucherService.GiveVoucherToAccount(accountid, voucherid);
                if (result.IsSuccess) return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [Authorize(Policy = "StaffOnly")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("active-deactive-voucher")]
        public async Task<IActionResult> ActiveDeactiveVoucher([FromForm] int voucherid, [FromForm] int flag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _voucherService.ActiveDeactiveVoucher(voucherid, flag);
                if (result.IsSuccess) return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [Authorize(Policy = "StaffOnly")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("voucher-information")]
        public async Task<IActionResult> EditVoucherInformation([FromForm] int voucherid, [FromForm] EditVoucherDTO editVoucherDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _voucherService.EditVoucher(voucherid, editVoucherDTO);
                if (result.IsSuccess) return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Policy = "UserOnly")]    //.get voucher
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("for-account")]
        public async Task<IActionResult> GetVouchersForUser()
        {
            CurrentUserObject currentUserObject = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            var result = await _voucherService.GetVoucherForUser(currentUserObject.AccountId);
            return Ok(result);
        }
    }
}
