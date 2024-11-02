using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.DTOs.ReviewDTO
{
    public class ViewReviewDTO
    {
        public int ReviewId { get; set; }
        public int ToyId { get; set; }
        public int AccountId { get; set; }
        public float Rating { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
    }
}
