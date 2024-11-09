
using EduToyRent.Service.Common;
using EduToyRent.Service.DTOs.AccountDTO;
using EduToyRent.Service.DTOs.ReportDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.Interfaces
{
    public interface IReportService
    {
        Task<Result> CreateReportAsync(CreateReportDTO dto);
        Task<Pagination<ReportListDTO>> GetReports(int pageIndex, int pageSize);
        Task<Result> ChangeReportStatus(int reportId);
        Task<Pagination<ReportListDTO>> GetReportsByAccountId(int pageIndex, int pageSize, CurrentUserObject currentUserObject);

    }
}
