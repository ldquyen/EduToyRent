using AutoMapper;
using EduToyRent.DataAccess.Entities;
using EduToyRent.Repository.Interfaces;
using EduToyRent.Service.Common;
using EduToyRent.Service.DTOs.ReportDTO;
using EduToyRent.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.Services
{
    public class ReportReplyService : IReportReplyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRequestFromService _requestFromService;
        public ReportReplyService(IUnitOfWork unitOfWork, IMapper mapper, IRequestFromService requestFromService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _requestFromService = requestFromService;
        }
        public async Task<Result> AddReplyAsync(CreateReportReplyDTO dto)
        {
            var reply = _mapper.Map<ReportReply>(dto);
            await _unitOfWork.ReportReplyRepository.AddReplyAsync(reply);
            return Result.Success();
        }

        public async Task<ICollection<ReportReplyDTO>> GetRepliesByReportIdAsync(int reportId)
        {
            var replies = await _unitOfWork.ReportReplyRepository.GetRepliesByReportIdAsync(reportId);
            return _mapper.Map<ICollection<ReportReplyDTO>>(replies);
        }
    }
}
