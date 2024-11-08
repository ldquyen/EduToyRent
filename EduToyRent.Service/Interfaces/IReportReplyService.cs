using EduToyRent.Service.Common;
using EduToyRent.Service.DTOs.ReportDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.Interfaces
{
    public interface IReportReplyService
    {
        Task<Result> AddReplyAsync(CreateReportReplyDTO dto);
        Task<ICollection<ReportReplyDTO>> GetRepliesByReportIdAsync(int reportId);
    }
}
