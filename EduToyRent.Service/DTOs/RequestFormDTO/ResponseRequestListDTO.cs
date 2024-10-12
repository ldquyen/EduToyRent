﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.DTOs.RequestFormDTO
{
    public class ResponseRequestListDTO
    {
        public int RequestId { get; set; }
        public DateTime RequestDate { get; set; }
        public int RequestStatus { get; set; }
        public bool ForRent { get; set; }
        public string? DenyReason { get; set; }
    }
}
