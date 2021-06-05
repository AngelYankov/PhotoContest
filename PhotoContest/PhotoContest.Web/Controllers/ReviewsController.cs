using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhotoContest.Data;
using PhotoContest.Data.Models;
using PhotoContest.Services.Contracts;
using PhotoContest.Services.Models;
using PhotoContest.Services.Models.Create;
using PhotoContest.Web.Models;
using PhotoContest.Web.Models.ReviewViewModels;

namespace PhotoContest.Web.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IReviewService reviewService;
        private readonly IPhotoService photoService;

        public ReviewsController(IReviewService reviewService, IPhotoService photoService)
        {
            this.reviewService = reviewService;
            this.photoService = photoService;
        }

        /// <summary>
        /// Get photo to review
        /// </summary>
        /// <param name="photoId">Id of photo</param>
        [Authorize(Roles ="Admin,Organizer")]
        public async Task<IActionResult> Create(Guid photoId)
        {
            var photo = await this.photoService.GetAsync(photoId);
            var model = new CreateReviewViewModel() { PhotoId = photoId, PhotoUrl = photo.PhotoUrl };
            return View(model);
        }

        /// <summary>
        /// Review photo
        /// </summary>
        /// <param name="model">Details of the review</param>
        /// <returns>List of all contests or error page of bad request</returns>
        [Authorize(Roles = "Admin,Organizer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateReviewViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var createDTO = new NewReviewDTO()
                    {
                        Comment = model.Comment,
                        Score = model.Score,
                        PhotoId=model.PhotoId,
                        WrongCategory=model.WrongCategory
                    };
                    await this.reviewService.CreateAsync(createDTO);
                    return RedirectToAction("Index", "Contests");
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            return View(model);
        }

        /// <summary>
        /// Get all reviews for a photo
        /// </summary>
        /// <param name="id">Id of the photo</param>
        /// <returns>List of all photo reviews or error page if bad request</returns>
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetPhotoReviews(Guid id)
        {
            try
            {
                var reviews = await this.reviewService.GetForPhotoAsync(id);
                return View(reviews.Select(r => new ReviewViewModel(r)));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Get review to delete
        /// </summary>
        /// <param name="id">Id of reviews</param>
        /// <returns>Show for to confirm deletion</returns>
        [Authorize(Roles = "Admin,Organizer")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var review = await this.reviewService.FindReviewAsync(id);
                var reviewDTO = new ReviewDTO(review);
                var viewModel = new ReviewViewModel(reviewDTO);
                return View(viewModel);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Delete review
        /// </summary>
        /// <param name="model">Id of the review</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin,Organizer")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(ReviewViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await this.reviewService.DeleteAsync(model.Id);
                    return RedirectToAction("Index", "Contests");
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            return View(model);
        }
    }
}
