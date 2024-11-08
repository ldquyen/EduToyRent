using EduToyRent.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Repository.Interfaces
{
    public interface IReportReplyRepository
    {
        Task AddReplyAsync(ReportReply reportReply);
        Task<ICollection<ReportReply>> GetRepliesByReportIdAsync(int reportId);
        Task<ICollection<ReportReply>> GetRepliesBySenderIdAsync(int senderId);
    }
}
