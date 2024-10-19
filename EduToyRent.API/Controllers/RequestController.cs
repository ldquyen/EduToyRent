﻿using EduToyRent.API.Helper;
using EduToyRent.Service.DTOs.AccountDTO;
using EduToyRent.Service.DTOs.RequestFormDTO;
using EduToyRent.Service.DTOs.ToyDTO;
using EduToyRent.Service.Interfaces;
using EduToyRent.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Policy = "SupplierOnly")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("rental")]
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
        [Authorize(Policy = "SupplierOnly")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("sale")]
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

        [HttpGet()]
        public async Task<IActionResult> GetAllRequests(int page = 1)
        {
            var requests = await _requestFromService.GetAllsRequest(page, 10);
            return Ok(requests);
        }

        [HttpGet("unanswered-requests")]
        public async Task<IActionResult> GetUnansweredRequests(int page = 1)
        {
            var requests = await _requestFromService.GetUnansweredRequest(page, 10);
            return Ok(requests);
        }

        [HttpGet("answered-requests")]
        public async Task<IActionResult> GetAnsweredRequests(int page = 1)
        {
            var requests = await _requestFromService.GetAnsweredRequest(page, 10);
            return Ok(requests);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRequest(int id)
        {
            var requests = await _requestFromService.GetRequestById(id);
            return Ok(requests);
        }

        [Authorize(Policy = "StaffOnly")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("status")]
        public async Task<IActionResult> UpdateRequestStatus([FromBody] UpdateRequestDTO updateRequestDTO)
        {
            CurrentUserObject currentUserObject = await TokenHelper.Instance.GetThisUserInfo(HttpContext);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updateResult = await _requestFromService.UppdateRequestStatus(updateRequestDTO, currentUserObject.AccountId);

            if (!updateResult.IsSuccess)
                return BadRequest(updateResult);
            
            return Ok(updateResult);
        }
    }
}
// chỉnh tên  api request 