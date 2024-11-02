using AutoMapper;
using EduToyRent.DAL.Entities;
using EduToyRent.Repository.Interfaces;
using EduToyRent.Service.Common;
using EduToyRent.Service.DTOs.AccountDTO;
using EduToyRent.Service.DTOs.CategoryDTO;
using EduToyRent.Service.DTOs.RequestFormDTO;
using EduToyRent.Service.DTOs.ReviewDTO;
using EduToyRent.Service.Exceptions;
using EduToyRent.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ReviewService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<dynamic> CreateReview(CreateReviewDTO createReviewDTO, CurrentUserObject currentUserObject)
        {
            var toy = _unitOfWork.ToyRepository.GetToyById(createReviewDTO.ToyId);
            if (toy != null)
            {
                var review = _mapper.Map<Review>(createReviewDTO);
                review.Date = DateTime.Now;
                review.AccountId = currentUserObject.AccountId;
                var save = await _unitOfWork.ReviewRepository.AddAsync(review);
                await _unitOfWork.SaveAsync();
                return Result.Success();
            }
            else
            {
                return Result.Failure(ReviewErrors.InvalidToyId);
            }
        }
        public async Task<dynamic> GetListReview()
        {
            var list = await _unitOfWork.ReviewRepository.GetAllAsync();
            List<ViewReviewDTO> listReviewDTO = new List<ViewReviewDTO>();
            foreach (var review in list)
            {
                var dto = new ViewReviewDTO
                {
                   AccountId = review.AccountId,
                   ReviewId = review.ReviewId,
                   Comment = review.Comment,
                   Date = review.Date,
                   Rating = review.Rating,
                   ToyId = review.ToyId
                };

                listReviewDTO.Add(dto);
            }
            return Result.SuccessWithObject(listReviewDTO);
        }
    }
}
