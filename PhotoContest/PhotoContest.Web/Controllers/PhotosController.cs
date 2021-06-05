using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PhotoContest.Services.Models.Update;
using Microsoft.AspNetCore.Authorization;
using PhotoContest.Services.Services;
using System.Web.Helpers;
using Simple.ImageResizer;
using System.Drawing;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace PhotoContest.Web.Controllers
{
    public class PhotosController : Controller
    {
        private readonly IPhotoService photoService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IContestService contestService;
        private readonly IReviewService reviewService;

        public PhotosController(IPhotoService photoService, 
            IWebHostEnvironment webHostEnvironment, 
            IContestService contestService,
            IReviewService reviewService)
        {
            this.photoService = photoService;
            this.webHostEnvironment = webHostEnvironment;
            this.contestService = contestService;
            this.reviewService = reviewService;
        }
        [Authorize(Roles ="Admin,Organizer")]
        public async Task<IActionResult> Index()
        {
            var photos = await this.photoService.GetAllAsync();
            return View(photos.Select(p=>new PhotoViewModel(p)));
        }
        [Authorize(Roles = "Admin,User")]
        public IActionResult Create(string contestName)
        {
            var createPhotoVM = new CreatePhotoViewModel()
            {
                ContestName = contestName
            };
            return View(createPhotoVM);
        }
        
        [Authorize(Roles = "Admin,User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePhotoViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newFileName = $"{Guid.NewGuid()}_{model.File.FileName}";
                    var fileDbPath = $"/Images/{newFileName}";
                    var saveFile = Path.Combine(webHostEnvironment.WebRootPath, "Images", newFileName);
                    using (var fileSelected = new FileStream(saveFile, FileMode.Create)) 
                    {
                        await model.File.CopyToAsync(fileSelected);
                    }
                    using (var img = Image.Load(model.File.OpenReadStream()))
                    {
                        img.Mutate(x => x.Resize(400, 400));
                        //img.Save(fileDbPath);
                    }

                    var newPhotoDTO = new NewPhotoDTO()
                    {
                        Title = model.Title,
                        Description = model.Description,
                        PhotoUrl = fileDbPath,
                        ContestName = model.ContestName
                    };
                    
                    await this.photoService.CreateAsync(newPhotoDTO);
                    return RedirectToAction("GetUserContests","Contests");
                }
                catch (Exception e)
                {
                    return NotFound(e.Message);
                }
            }
            return View(model);
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetPhotosForUser()
        {
            var photos = await this.photoService.GetPhotosForUserAsync();
            return View(photos.Select(p=> new PhotoViewModel(p) { ContestStatus = p.ContestStatus }));
        }

        [Authorize(Roles = "Admin,Organizer,User")]
        public async Task<IActionResult> GetContestsPhotos(string contestName)
        {
            var photos = await this.photoService.GetPhotosForContestAsync(contestName);
            var contest = await this.contestService.FindContestByNameAsync(contestName);
            var juries = await this.contestService.AllJuriesAsync();
            return View(photos.Select(p=>new PhotoViewModel(p) { ContestStatus = contest.Status.Name, Juries = juries.ToList() }));
        }
        [Authorize(Roles = "Admin,Organizer")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var photo = await this.photoService.GetAsync(id);
            return View(new EditPhotoViewModel(photo));
        }
        [Authorize(Roles = "Admin,Organizer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, EditPhotoViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var updateDTO = new UpdatePhotoDTO()
                    {
                        Title = model.Title,
                        Description = model.Description
                    };
                    var photo = await this.photoService.UpdateAsync(updateDTO, id);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            return View(model);
        }
        [Authorize(Roles = "Admin,Organizer")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var photo = await this.photoService.GetAsync(id);
            return View(new PhotoViewModel(photo));
        }
        [Authorize(Roles = "Admin,Organizer")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                /*var photo = await this.photoService.GetAsync(id);
                var imagePath = Path.Combine(webHostEnvironment.WebRootPath, photo.PhotoUrl);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }*/
                await this.photoService.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
