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
using PhotoContest.Services.Models;
using PhotoContest.Services.Models.Create;
using PhotoContest.Web.Models.PhotoViewModels;

namespace PhotoContest.Web.Controllers
{
    public class PhotosController : Controller
    {
        private readonly PhotoContestContext _context;
        private readonly IPhotoService photoService;

        public PhotosController(PhotoContestContext context, IPhotoService photoService)
        {
            _context = context;
            this.photoService = photoService;
        }

        public async Task<IActionResult> Index()
        {
            var photos = await this.photoService.GetAllAsync();
            return View(photos.Select(p=>new PhotoViewModel(p)));
        }

        // GET: Photos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photo = await _context.Photos
                .Include(p => p.Contest)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (photo == null)
            {
                return NotFound();
            }

            return View(photo);
        }

        public IActionResult Create(string contestName)
        {
            var createPhotoVM = new CreatePhotoViewModel()
            {
                ContestName = contestName
            };
            return View(createPhotoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePhotoViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newPhotoDTO = new NewPhotoDTO()
                    {
                        Title = model.Title,
                        Description = model.Description,
                        PhotoUrl = model.PhotoUrl,
                        ContestName = model.ContestName
                    };
                    await this.photoService.CreateAsync(newPhotoDTO);
                    return View(nameof(Index));
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            return View(model);
        }
        public async Task<IActionResult> GetContestsPhotos(string contestName)
        {
            var photos = await this.photoService.GetPhotosForContestAsync(contestName);
            return View(photos.Select(p=>new PhotoViewModel(p)));
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photo = await _context.Photos.FindAsync(id);
            if (photo == null)
            {
                return NotFound();
            }
            ViewData["ContestId"] = new SelectList(_context.Contests, "Id", "Name", photo.ContestId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", photo.UserId);
            return View(photo);
        }

        // POST: Photos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Description,PhotoUrl,UserId,ContestId,AllPoints,CreatedOn,ModifiedOn,DeletedOn,IsDeleted")] Photo photo)
        {
            if (id != photo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(photo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhotoExists(photo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContestId"] = new SelectList(_context.Contests, "Id", "Name", photo.ContestId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", photo.UserId);
            return View(photo);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var photo = await this.photoService.GetAsync(id);
            return View(new PhotoViewModel(photo));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await this.photoService.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private bool PhotoExists(Guid id)
        {
            return _context.Photos.Any(e => e.Id == id);
        }
    }
}
