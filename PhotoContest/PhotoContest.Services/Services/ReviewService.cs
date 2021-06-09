using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
using System.Threading.Tasks;

namespace PhotoContest.Services.Services
{
    public class ReviewService : IReviewService
    {
        private readonly PhotoContestContext dbContext;
        private readonly IPhotoService photoService;
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IHttpContextAccessor contextAccessor;

        public ReviewService(PhotoContestContext dbContext,
            IPhotoService photoService,
            IUserService userService,
            IHttpContextAccessor contextAccessor,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            this.dbContext = dbContext;
            this.photoService = photoService;
            this.userService = userService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.contextAccessor = contextAccessor;
        }

        /// <summary>
        /// Create a review.
        /// </summary>
        /// <param name="newReviewDTO">Details of new review.</param>
        /// <returns>Returns created review.</returns>
        public async Task<ReviewDTO> CreateAsync(NewReviewDTO newReviewDTO)
        {
            var photo = await this.photoService.FindPhotoAsync(newReviewDTO.PhotoId);
            if (photo.Contest.Status.Name != "Phase 2") throw new ArgumentException(Exceptions.InvalidContestPhase);
            if (newReviewDTO.Comment == null) throw new ArgumentException(Exceptions.InvalidComment);

            var username = this.userManager.GetUserName(this.signInManager.Context.User);
            var user = await this.userService.GetUserByUsernameAsync(username);
            if (user.Rank.Name != "Organizer" && user.Rank.Name != "Admin")
            {
                if (!this.dbContext.Juries.Any(j => j.UserId == user.Id && j.ContestId == photo.ContestId))
                {
                    throw new ArgumentException(Exceptions.UserNotJury);
                }
            }
            if (await this.dbContext.Reviews.AnyAsync(r => r.UserId == user.Id && r.PhotoId == photo.Id))
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
                photo.IsInWrongCategory = true;
            }
            await this.dbContext.Reviews.AddAsync(review);
            await this.dbContext.SaveChangesAsync();
            return new ReviewDTO(review);
        }

        /// <summary>
        /// Create a review for API.
        /// </summary>
        /// <param name="newReviewDTO">Details of new review.</param>
        /// <returns>Returns created review.</returns>
        public async Task<ReviewDTO> CreateApiAsync(NewReviewDTO newReviewDTO)
        {
            var photo = await this.photoService.FindPhotoAsync(newReviewDTO.PhotoId);
            if (photo.Contest.Status.Name != "Phase 2") throw new ArgumentException(Exceptions.InvalidContestPhase);
            if (newReviewDTO.Comment == null) throw new ArgumentException(Exceptions.InvalidComment);
            var username = this.contextAccessor.HttpContext.User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;
            var user = await this.userService.GetUserByUsernameAsync(username);
            if (user.Rank.Name != "Organizer" && user.Rank.Name != "Admin")
            {
                if (!this.dbContext.Juries.Any(j => j.UserId == user.Id && j.ContestId == photo.ContestId))
                {
                    throw new ArgumentException(Exceptions.UserNotJury);
                }
            }
            if (await this.dbContext.Reviews.AnyAsync(r => r.UserId == user.Id && r.PhotoId == photo.Id))
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
                photo.IsInWrongCategory = true;
            }
            await this.dbContext.Reviews.AddAsync(review);
            await this.dbContext.SaveChangesAsync();
            return new ReviewDTO(review);
        }

        /// <summary>
        /// Delete a review by ID.
        /// </summary>
        /// <param name="reviewId">Review ID to search for.</param>
        /// <returns>Returns if is delete succesfully.</returns>
        public async Task<bool> DeleteAsync(Guid reviewId)
        {
            var review = await FindReviewAsync(reviewId);
            review.IsDeleted = true;
            review.DeletedOn = DateTime.UtcNow;
            await this.dbContext.SaveChangesAsync();
            return review.IsDeleted;
        }

        /// <summary>
        /// Get all reviews for certain photo.
        /// </summary>
        /// <param name="id">Id of photo to search for.</param>
        /// <returns>Returns all reviews.</returns>
        public async Task<List<ReviewDTO>> GetForPhotoAsync(Guid photoId)
        {
            await this.photoService.FindPhotoAsync(photoId);
            return await this.dbContext.Reviews
                                       .Include(r => r.Photo)
                                       .Include(r => r.Evaluator)
                                       .Where(r => r.PhotoId == photoId && r.IsDeleted == false)
                                       .Select(r => new ReviewDTO(r)).ToListAsync();
        }

        /// <summary>
        /// Get all reviews for a user.
        /// </summary>
        /// <param name="id">Id to search for.</param>
        /// <returns>Returns all reviews for a user.</returns>
        public async Task<List<ReviewDTO>> GetForUserAsync(string username)
        {
            var user = await this.userService.GetUserByUsernameAsync(username);
            return await this.dbContext.Reviews
                                       .Include(r => r.Photo)
                                       .Include(r => r.Evaluator)
                                       .Where(r => r.Photo.UserId == user.Id && r.IsDeleted == false)
                                       .OrderByDescending(r => r.CreatedOn)
                                       .Select(r => new ReviewDTO(r))
                                       .ToListAsync();
        }

        /// <summary>
        /// Find review by Id.
        /// </summary>
        /// <param name="reviewId">Id of review to search for.</param>
        /// <returns>Return a review with that id.</returns>
        public async Task<Review> FindReviewAsync(Guid reviewId)
        {
            return await this.dbContext.Reviews
                                        .Include(r => r.Photo)
                                        .Include(r => r.Evaluator)
                                        .Where(r => r.IsDeleted == false)
                                        .FirstOrDefaultAsync(r => r.Id == reviewId && r.IsDeleted == false)
                                        ?? throw new ArgumentException(Exceptions.InvalidReviewId);
        }

        /// <summary>
        /// Get all reviews.
        /// </summary>
        /// <returns>Return all reviews.</returns>
        public async Task<List<Review>> GetAllReviewsAsync()
        {
            return await this.dbContext.Reviews
                                       .Include(r => r.Photo)
                                       .Include(r => r.Evaluator)
                                       .Where(r => r.IsDeleted == false)
                                       .ToListAsync();
        }
    }
}
