using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhotoContest.Data;
using PhotoContest.Data.Models;
using PhotoContest.Services.Contracts;
using PhotoContest.Services.Models.Create;
using PhotoContest.Web.Models.ReviewViewModels;

namespace PhotoContest.Web.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly PhotoContestContext _context;
        private readonly IReviewService reviewService;
        private readonly IPhotoService photoService;

        public ReviewsController(PhotoContestContext context, IReviewService reviewService, IPhotoService photoService)
        {
            _context = context;
            this.reviewService = reviewService;
            this.photoService = photoService;
        }
        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews
                .Include(r => r.Evaluator)
                .Include(r => r.Photo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        public async Task<IActionResult> Create(Guid photoId)
        {
            var photo = await this.photoService.GetAsync(photoId);
            var model = new CreateReviewViewModel() { PhotoId = photoId, PhotoUrl = photo.PhotoUrl };
            return View(model);
        }

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
                    return RedirectToAction("Index", "Contest");
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            return View(model);
        }

        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews
                .Include(r => r.Evaluator)
                .Include(r => r.Photo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var review = await _context.Reviews.FindAsync(id);
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewExists(Guid id)
        {
            return _context.Reviews.Any(e => e.Id == id);
        }
    }
}
