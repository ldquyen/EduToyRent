using EduToyRent.API.Helper;
using EduToyRent.Service.DTOs.AccountDTO;
using EduToyRent.Service.DTOs.ReviewDTO;
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
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        [Authorize(Policy = "UserOnly")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("review")]
        public async Task<IActionResult> CreateReview(CreateReviewDTO createReviewDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CurrentUserObject currentUserObject = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
                var result = await _reviewService.CreateReview(createReviewDTO, currentUserObject);
                if (result.IsSuccess) return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet()]
        public async Task<IActionResult> GetReviews()
        {
            try
            {
                var result = await _reviewService.GetListReview();
                if (result.IsSuccess) return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
