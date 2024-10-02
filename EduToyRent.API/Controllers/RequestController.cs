using EduToyRent.API.Helper;
using EduToyRent.Service.DTOs.AccountDTO;
using EduToyRent.Service.DTOs.RequestFormDTO;
using EduToyRent.Service.DTOs.ToyDTO;
using EduToyRent.Service.Interfaces;
using EduToyRent.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduToyRent.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestFromService _requestFromService;
        private readonly IToyService _toyService;
        private readonly IFirebaseService _firebaseService;

        public RequestController(IRequestFromService requestFromService, IToyService toyService, IFirebaseService firebaseService)
        {
            _requestFromService = requestFromService;
            _toyService = toyService;
            _firebaseService = firebaseService;
        }

        [HttpPost("create-rental")]
        public async Task<IActionResult> CreateRental([FromForm] CreateRentalToyDTO createRentalToyDTO)
        {
            CurrentUserObject currentUserObject = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var fileUrl = await _firebaseService.UploadImage(createRentalToyDTO.ImageFile, "ToyImage");

            if (string.IsNullOrEmpty(fileUrl))
            {
                return BadRequest("Failed to upload image.");
            }

            var createToyResult = await _toyService.CreateRentalToy(createRentalToyDTO, fileUrl, currentUserObject.AccountId);
            if (!createToyResult.IsSuccess)
                return BadRequest(createToyResult);
            return Ok(createToyResult);           
        }

        [HttpPost("create-sale")]
        public async Task<IActionResult> CreateSale([FromForm] CreateSaleToyDTO createSaleToyDTO)
        {
            CurrentUserObject currentUserObject = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var fileUrl = await _firebaseService.UploadImage(createSaleToyDTO.ImageFile , "ToyImage");

            if (string.IsNullOrEmpty(fileUrl))
            {
                return BadRequest("Failed to upload image.");
            }

            var createToyResult = await _toyService.CreateSaleToy(createSaleToyDTO, fileUrl, currentUserObject.AccountId);
            if (!createToyResult.IsSuccess)
                return BadRequest(createToyResult);
            return Ok(createToyResult);
        }
    }
}
