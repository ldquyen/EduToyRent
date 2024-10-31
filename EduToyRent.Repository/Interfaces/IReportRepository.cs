using EduToyRent.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Repository.Interfaces
{
    public interface IReportRepository
    {
        Task AddReport(Report report);
        Task<ICollection<Report>> GetReports(int skip, int take);
        Task<int> GetTotalReportsCount();
        Task<Report> GetReportById(int reportId);
        Task UpdateReport(Report report);
    }
}
