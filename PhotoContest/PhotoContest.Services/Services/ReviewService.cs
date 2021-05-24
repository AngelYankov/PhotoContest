﻿using Microsoft.AspNetCore.Http;
using PhotoContest.Data;
using PhotoContest.Data.Models;
using PhotoContest.Services.Contracts;
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
            if (photo.Contest.Status.Name != "Phase2") throw new ArgumentException("Contest is not in phase2.");
            if (newReviewDTO.Comment == null) throw new ArgumentException("the comment is not valid.");
            var userName = this.contextAccessor.HttpContext.User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;
            var user = await this.userService.GetUserByUsernameAsync(userName);

            if (this.dbContext.Reviews.Any(r => r.UserId == user.Id && r.PhotoId == photo.Id))
                throw new ArgumentException("Photo is already reviewed.");

            var review = new Review()
            {
                PhotoId = photo.Id,
                Comment = newReviewDTO.Comment,
                Score = newReviewDTO.Score,
                UserId = user.Id,
                CreatedOn = DateTime.UtcNow
            };
            await this.dbContext.Reviews.AddAsync(review);
            await this.dbContext.SaveChangesAsync();
            return new ReviewDTO(review);
        }
        /// <summary>
        /// Get all reviews for certain photo.
        /// </summary>
        /// <param name="id">Id of photo to search for.</param>
        /// <returns>Returns all reviews.</returns>
        public async Task<List<ReviewDTO>> GetForPhotoAsync(Guid id)
        {
            var photo = await this.photoService.FindPhoto(id);
            return photo.Reviews.Select(r => new ReviewDTO(r)).ToList();
        }
    }
}
