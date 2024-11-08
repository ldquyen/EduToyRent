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
    public class ReportReplyRepository : Repository<ReportReply>, IReportReplyRepository
    {
        private readonly EduToyRentDbContext _context;
        public ReportReplyRepository(EduToyRentDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task AddReplyAsync(ReportReply reportReply)
        {
            await _context.ReportReplies.AddAsync(reportReply);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<ReportReply>> GetRepliesByReportIdAsync(int reportId)
        {
            return await _context.ReportReplies
                .Where(r => r.ReportId == reportId)
                .OrderBy(r => r.SentAt)
                .ToListAsync();
        }

        public async Task<ICollection<ReportReply>> GetRepliesBySenderIdAsync(int senderId)
        {
            return await _context.ReportReplies
                .Where(r => r.SenderId == senderId)
                .OrderByDescending(r => r.SentAt)
                .ToListAsync();
        }
    }
}
