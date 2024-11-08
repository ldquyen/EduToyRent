using EduToyRent.DAL.Context;
using EduToyRent.DataAccess.Entities;
using EduToyRent.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Repository.Repositories
{
    public class ReportRepository : Repository<Report>, IReportRepository
    {
        private readonly EduToyRentDbContext _context;
        public ReportRepository(EduToyRentDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task AddReport(Report report)
        {
            await _context.Set<Report>().AddAsync(report);
            await _context.SaveChangesAsync();
        }
        public async Task<ICollection<Report>> GetReports(int skip, int take)
        {
            return await _context.Reports
                .Include(r => r.Toy)
                .Include(r => r.Account)
                .OrderByDescending(r => r.ReportDate)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<int> GetTotalReportsCount()
        {
            return await _context.Reports.CountAsync();
        }

        public async Task<Report> GetReportById(int reportId)
        {
            return await _context.Reports.FindAsync(reportId);
        }

        public async Task UpdateReport(Report report)
        {
            _context.Reports.Update(report);
            await _context.SaveChangesAsync();
        }
    }
}
