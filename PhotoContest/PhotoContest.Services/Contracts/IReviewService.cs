using PhotoContest.Data.Models;
using PhotoContest.Services.Models;
using PhotoContest.Services.Models.Create;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoContest.Services.Contracts
{
    public interface IReviewService
    {
        Task<ReviewDTO> CreateAsync(NewReviewDTO newReviewDTO);
        Task<ReviewDTO> CreateApiAsync(NewReviewDTO newReviewDTO);
        Task<List<ReviewDTO>> GetForPhotoAsync(Guid id);
        Task<List<ReviewDTO>> GetForUserAsync(string username);
        Task<bool> DeleteAsync(Guid reviewId);
        Task<Review> FindReviewAsync(Guid reviewId);
        Task<List<Review>> GetAllReviewsAsync();
    }
}
