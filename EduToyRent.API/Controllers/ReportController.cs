using EduToyRent.Service.DTOs.ReportDTO;
using EduToyRent.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduToyRent.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpPost("create-report")] //Create Report for User - Phu
        [Authorize(Policy = "UserOnly")]
        public async Task<IActionResult> CreateReport([FromQuery] CreateReportDTO dto)
        {
            var result = await _reportService.CreateReportAsync(dto);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result.Error);
        }
        [HttpGet("get-report")] //Get Reports for Staff - Phu
        public async Task<IActionResult> GetReports(int pageIndex = 0, int pageSize = 10)
        {
            var paginatedReports = await _reportService.GetReports(pageIndex, pageSize);
            return Ok(paginatedReports);
        }

        [HttpPut("change-report-status")] //Update Report status - Phu
        [Authorize(Policy = "StaffOnly")]
        public async Task<IActionResult> ChangeReportStatus([FromQuery] ChangeReportStatusDTO dto)
        {
            var result = await _reportService.ChangeReportStatus(dto);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }
    }
}
