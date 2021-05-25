using Microsoft.AspNetCore.Http;
using PhotoContest.Data;
using PhotoContest.Data.Models;
using PhotoContest.Services.Contracts;
using PhotoContest.Services.ExceptionMessages;
using PhotoContest.Services.Models;
using PhotoContest.Services.Models.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Services.Services
{
    public class ReviewService : IReviewService
    {
        private readonly PhotoContestContext dbContext;
        private readonly IPhotoService photoService;
        private readonly IUserService userService;
        private readonly IHttpContextAccessor contextAccessor;

        public ReviewService(PhotoContestContext dbContext, IPhotoService photoService, IUserService userService, IHttpContextAccessor contextAccessor)
        {
            this.dbContext = dbContext;
            this.photoService = photoService;
            this.userService = userService;
            this.contextAccessor = contextAccessor;
        }
        /// <summary>
        /// Create a review.
        /// </summary>
        /// <param name="newReviewDTO">Details of new review.</param>
        /// <returns>Returns created review.</returns>
        public async Task<ReviewDTO> CreateAsync(NewReviewDTO newReviewDTO)
        {
            var photo = await this.photoService.FindPhoto(newReviewDTO.PhotoId);
            if (photo.Contest.Status.Name != "Phase2") throw new ArgumentException(Exceptions.InvalidContestPhase);
            if (newReviewDTO.Comment == null) throw new ArgumentException(Exceptions.InvalidComment);
            var userName = this.contextAccessor.HttpContext.User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;
            var user = await this.userService.GetUserByUsernameAsync(userName);
            if (!this.dbContext.Juries.Any(j => j.UserId == user.Id && j.ContestId == photo.ContestId && user.Rank.Name == "User"))
            {
                throw new ArgumentException("User is not in jury for this contest.");
            }

            if (this.dbContext.Reviews.Any(r => r.UserId == user.Id && r.PhotoId == photo.Id))
                throw new ArgumentException(Exceptions.ReviewedPhoto);

            var review = new Review()
            {
                PhotoId = photo.Id,
                Comment = newReviewDTO.Comment,
                Score = newReviewDTO.Score,
                UserId = user.Id,
                CreatedOn = DateTime.UtcNow
            };
            if (newReviewDTO.WrongCategory)
            {
                review.Comment = Exceptions.WrongCategoryComment;
                review.Score = 0;
                review.WrongCategory = true;
            }
            await this.dbContext.Reviews.AddAsync(review);
            await this.dbContext.SaveChangesAsync();
            return new ReviewDTO(review);
        }
        /// <summary>
        /// Get all reviews for certain photo.
        /// </summary>
        /// <param name="id">Id of photo to search for.</param>
        /// <returns>Returns all reviews.</returns>
        public async Task<List<ReviewDTO>> GetForPhotoAsync(Guid photoId)
        {
            var photo = await this.photoService.FindPhoto(photoId);
            return photo.Reviews.Select(r => new ReviewDTO(r)).ToList();
        }
        /// <summary>
        /// Get all reviews for a user.
        /// </summary>
        /// <param name="username">Username to search for.</param>
        /// <returns>Returns all reviews for a user.</returns>
        public async Task<List<ReviewDTO>> GetForUserAsync(Guid id)
        {
            //var user = await this.userService.FindUser(id);
            return this.dbContext.Reviews.Where(r=>r.UserId==id).OrderByDescending(r => r.CreatedOn).Select(r => new ReviewDTO(r)).ToList();
        }
    }
}
