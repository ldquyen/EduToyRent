﻿using EduToyRent.Service.Interfaces;
using EduToyRent.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;

namespace EduToyRent.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPayOSService _payOsService;

        public PaymentController(IPayOSService payOsService)
        {
            _payOsService = payOsService;
        }

        [HttpPost("create-sale-payment-link/{orderId}")]    //.paay sale  order
        public async Task<IActionResult> CreateSalePayment(int orderId)
        {
            var result = await _payOsService.CreatePaymentLinkForSale(orderId);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpPost("create-rent-payment-link/{orderId}")]    //.pay rent order
        public async Task<IActionResult> CreateRentPayment(int orderId)
        {
            var result = await _payOsService.CreatePaymentLinkForRent(orderId);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpPost("create-rent-payment2-link/{orderId}")]    //.pay rent order
        public async Task<IActionResult> CreateRent2Payment(int orderId)
        {
            var result = await _payOsService.CreatePaymentLinkForRent2(orderId);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpGet("payment-link-info/{orderId}")]        //.pay info
        public async Task<IActionResult> GetPaymentLinkInformation(int orderId)
        {
            var result = await _payOsService.GetPaymentLinkInformation(orderId);
            return Ok(result);
        }

        [HttpPost("cancel-payment-link/{orderId}")]     //.cancel payment
        public async Task<IActionResult> CancelPaymentLink(int orderId)
        {
            var result = await _payOsService.CancelPaymentLink(orderId);
            return Ok(result);
        }

        [HttpGet("payment/callback")]           //.test
        public async Task<IActionResult> PaymentCallback(WebhookType webhookData)
        {
            return Ok();
        }

        [Authorize(Policy = "StaffOnly")]      
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("staff/{status}")]
        public async Task<IActionResult> GetPaymentForStaff(int status)
        {
            var result = await _payOsService.GetAllPaymentForStaff(status);
            return Ok(result);
        }

        [HttpGet("order-id/{paymentId}")]
        public async Task<IActionResult> GetOrderIdByPaymentId(int paymentId)
        {
            var result = await _payOsService.GetOrderIdByPaymentId(paymentId);
            return Ok(result);
        }
    }
}

