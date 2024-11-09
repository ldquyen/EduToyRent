using EduToyRent.API.Helper;
using EduToyRent.Service.DTOs.AccountDTO;
using EduToyRent.Service.DTOs.ReportDTO;
using EduToyRent.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
        public async Task<IActionResult> ChangeReportStatus([FromQuery] int reportId)
        {
            var result = await _reportService.ChangeReportStatus(reportId);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok();
        }
        [Authorize(Policy = "SupplierOnly")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("reports-by-account")]
        public async Task<IActionResult> GetReportsByAccountId(int pageIndex = 0, int pageSize = 10)
        {
            CurrentUserObject currentUserObject = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            var paginatedReports = await _reportService.GetReportsByAccountId(pageIndex, pageSize, currentUserObject);
            return Ok(paginatedReports);
        }
    }
}
