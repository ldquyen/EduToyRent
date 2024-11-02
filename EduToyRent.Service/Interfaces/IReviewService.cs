using EduToyRent.Service.DTOs.AccountDTO;
using EduToyRent.Service.DTOs.ReviewDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.Interfaces
{
    public interface IReviewService
    {
        Task<dynamic> CreateReview(CreateReviewDTO createReviewDTO, CurrentUserObject currentUserObject);\
        Task<dynamic> GetListReview();

    }
}
