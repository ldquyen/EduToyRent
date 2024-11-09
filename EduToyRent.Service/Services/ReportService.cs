using AutoMapper;
using EduToyRent.DataAccess.Entities;
using EduToyRent.Repository.Interfaces;
using EduToyRent.Repository.Repositories;
using EduToyRent.Service.Common;
using EduToyRent.Service.DTOs.AccountDTO;
using EduToyRent.Service.DTOs.ReportDTO;
using EduToyRent.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.Services
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRequestFromService _requestFromService;
        private const string Pending = "Pending";
        private const string Processing = "Processing";
        private const string Processed = "Processed";
        public ReportService(IUnitOfWork unitOfWork, IMapper mapper, IRequestFromService requestFromService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _requestFromService = requestFromService;
        }
        public async Task<Result> CreateReportAsync(CreateReportDTO dto)
        {
            var report = _mapper.Map<Report>(dto);
            await _unitOfWork.ReportRepository.AddReport(report);
            return Result.Success();
        }
        public async Task<Pagination<ReportListDTO>> GetReports(int pageIndex, int pageSize)
        {
            var totalItemsCount = await _unitOfWork.ReportRepository.GetTotalReportsCount();
            var reports = await _unitOfWork.ReportRepository.GetReports(pageIndex * pageSize, pageSize);
            var reportDTOs = _mapper.Map<ICollection<ReportListDTO>>(reports);

            return new Pagination<ReportListDTO>
            {
                Items = reportDTOs,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = totalItemsCount
            };
        }

        public async Task<Result> ChangeReportStatus(int reportId)
        {
            var report = await _unitOfWork.ReportRepository.GetReportById(reportId);
            var toy = await _unitOfWork.ToyRepository.GetByIdAsync(report.ToyId);
            if (report == null)
            {
                return Result.Failure(Result.CreateError("404", "Report not found"));
            }

            if (report.Status == Pending)
            {
                report.Status = Processed;
                toy.IsActive = false;
                await _unitOfWork.ToyRepository.UpdateAsync(toy);
                await _unitOfWork.ReportRepository.UpdateReport(report);
                return Result.Success();
            }
            return Result.Failure(Result.CreateError("400", "Invalid status transition"));
        }
        public async Task<Pagination<ReportListDTO>> GetReportsByAccountId(int pageIndex, int pageSize, CurrentUserObject currentUserObject)
        {
            var totalItemsCount = await _unitOfWork.ReportRepository.GetTotalReportsCount();
            var reports = await _unitOfWork.ReportRepository.GetReportsByAccountId(pageIndex * pageSize, pageSize, currentUserObject.AccountId);
            var reportDTOs = _mapper.Map<ICollection<ReportListDTO>>(reports);

            return new Pagination<ReportListDTO>
            {
                Items = reportDTOs,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = totalItemsCount
            };
        }
    }
}
