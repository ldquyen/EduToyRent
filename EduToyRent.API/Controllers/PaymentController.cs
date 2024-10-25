using EduToyRent.Service.Interfaces;
using EduToyRent.Service.Services;
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

        [HttpPost("create-sale-payment-link/{orderId}")]
        public async Task<IActionResult> CreateSalePayment(int orderId)
        {
            var result = await _payOsService.CreatePaymentLinkForSale(orderId);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }
        [HttpPost("create-rent-payment-link/{orderId}")]
        public async Task<IActionResult> CreateRentPayment(int orderId)
        {
            var result = await _payOsService.CreatePaymentLinkForRent(orderId);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpGet("payment-link-info/{orderId}")]
        public async Task<IActionResult> GetPaymentLinkInformation(int orderId)
        {
            var result = await _payOsService.GetPaymentLinkInformation(orderId);
            return Ok(result);
        }
        [HttpPost("cancel-payment-link/{orderId}")]
        public async Task<IActionResult> CancelPaymentLink(int orderId)
        {
            var result = await _payOsService.CancelPaymentLink(orderId);
            return Ok(result);
        }

        [HttpGet("payment/callback")]
        public async Task<IActionResult> PaymentCallback(WebhookType webhookData)
        {
            return Ok();
        }

    }
}

