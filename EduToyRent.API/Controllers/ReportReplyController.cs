using EduToyRent.Service.DTOs.ReportDTO;
using EduToyRent.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduToyRent.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportReplyController : ControllerBase
    {
        private readonly IReportReplyService _reportReplyService;
        public ReportReplyController(IReportReplyService reportReplyService)
        {
            _reportReplyService = reportReplyService;
        }

        [HttpPost("reply")]
        public async Task<IActionResult> SendReply([FromForm] CreateReportReplyDTO dto)
        {
            var result = await _reportReplyService.AddReplyAsync(dto);
            if (result.IsSuccess)
                return Ok();

            return BadRequest(result.Errors);
        }

        [HttpGet("report/{reportId}")]
        public async Task<IActionResult> GetRepliesByReport(int reportId)
        {
            var replies = await _reportReplyService.GetRepliesByReportIdAsync(reportId);
            return Ok(replies);
        }
    }
}
