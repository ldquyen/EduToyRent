using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.DTOs.ReportDTO
{
    public class CreateReportDTO
    {
        public int ToyId { get; set; }
        public int ReportById { get; set; }
        public string ReportDetail { get; set; }
    }
}
