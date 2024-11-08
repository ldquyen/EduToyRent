using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.DTOs.ReportDTO
{
    public class ReportListDTO
    {
        public int ReportId { get; set; }
        public int ToyId { get; set; }
        public string ToyName { get; set; }
        public string ReporterName { get; set; }
        public string ReportDetail { get; set; }
        public DateTime ReportDate { get; set; }
        public string Status { get; set; }
        public string? Response {  get; set; }
    }

    public class ChangeReportStatusDTO
    {
        public int ReportId { get; set; }
        public string NewStatus { get; set; }
        public string? Response { get; set; }
    }
}

