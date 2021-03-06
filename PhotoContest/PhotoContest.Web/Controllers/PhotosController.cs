using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using PhotoContest.Services.Contracts;
using PhotoContest.Services.Models.Create;
using PhotoContest.Services.Models.Update;
using PhotoContest.Services.Services;
using PhotoContest.Web.Models.PhotoViewModels;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoContest.Web.Controllers
{
    public class PhotosController : Controller
    {
        private readonly IPhotoService photoService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IContestService contestService;
        private readonly IReviewService reviewService;
        private readonly IToastNotification toastNotification;

        public PhotosController(IPhotoService photoService,
            IWebHostEnvironment webHostEnvironment,
            IContestService contestService,
            IReviewService reviewService,
            IToastNotification toastNotification)
        {
            this.photoService = photoService;
            this.webHostEnvironment = webHostEnvironment;
            this.contestService = contestService;
            this.reviewService = reviewService;
            this.toastNotification = toastNotification;
        }

        /// <summary>
        /// Get all photos
        /// </summary>
        /// <returns>List of all photos</returns>
        [Authorize(Roles = "Admin,Organizer")]
        public async Task<IActionResult> Index()
        {
            var photos = await this.photoService.GetAllAsync();
            return View(photos.Select(p => new PhotoViewModel(p)));
        }

        /// <summary>
        /// Get contest to upload a photo to
        /// </summary>
        /// <param name="contestName">Contest in which the photo will be uploaded</param>
        [Authorize(Roles = "Admin,User")]
        public IActionResult Create(string contestName)
        {
            var createPhotoVM = new CreatePhotoViewModel()
            {
                ContestName = contestName
            };
            return View(createPhotoVM);
        }

        /// <summary>
        /// Upload a photo in a contest
        /// </summary>
        /// <param name="model">Details of the photo to upload</param>
        /// <returns>List of all user's contests or error page if bad request</returns>
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
                    return RedirectToAction("GetUserContests", "Contests");
                }
                catch (Exception e)
                {
                    toastNotification.AddErrorToastMessage(e.Message, new NotyOptions());
                    var path = Request.Path.Value.ToString() + "?contestName=" + model.ContestName;
                    return Redirect(path); 
                }
            }
            return View(model);
        }

        /// <summary>
        /// Show photos for signed in user
        /// </summary>
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetPhotosForUser()
        {
            var photos = await this.photoService.GetPhotosForUserAsync();
            return View(photos.Select(p => new PhotoViewModel(p) { ContestStatus = p.ContestStatus }));
        }

        /// <summary>
        /// Get photos for a contets
        /// </summary>
        /// <param name="contestName">name of the contest</param>
        /// <returns>List of all photos for a contest</returns>
        [Authorize]
        public async Task<IActionResult> GetContestsPhotos(string contestName)
        {
            try
            {
                var reviews = await this.reviewService.GetAllReviewsAsync();
                var photos = await this.photoService.GetPhotosForContestAsync(contestName);
                var contest = await this.contestService.FindContestByNameAsync(contestName);
                var juries = await this.contestService.GetAllJuriesAsync();
                return View(photos.Select(p => new PhotoViewModel(p) { ContestStatus = contest.Status.Name, Juries = juries, Reviews = reviews }));
            }
            catch (Exception)
            {
                return RedirectToAction("PageNotFound", "Home");
            }
        }

        /// <summary>
        /// Get a photo to edit
        /// </summary>
        /// <param name="id">Id of the photo</param>
        [Authorize(Roles = "Admin,Organizer")]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var photo = await this.photoService.GetAsync(id);
                return View(new EditPhotoViewModel(photo));
            }
            catch (Exception)
            {
                return RedirectToAction("PageNotFound", "Home");
            }
        }

        /// <summary>
        /// Edit a photo
        /// </summary>
        /// <param name="id">Id of the photo to edit</param>
        /// <param name="model">Details of the photo to edit</param>
        /// <returns>Returns list of all photos or error page if bad request</returns>
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
                    toastNotification.AddErrorToastMessage(e.Message, new NotyOptions());
                    var path = Request.Path.Value.ToString()+model.Id;
                    return Redirect(path);
                }
            }
            return View(model);
        }

        /// <summary>
        /// Get photo to delete
        /// </summary>
        /// <param name="id">Id of the photo</param>
        [Authorize(Roles = "Admin,Organizer")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var photo = await this.photoService.GetAsync(id);
                return View(new PhotoViewModel(photo));
            }
            catch (Exception)
            {
                return RedirectToAction("PageNotFound", "Home");
            }
        }

        /// <summary>
        /// Delete a photo
        /// </summary>
        /// <param name="id">Id of the photo</param>
        /// <returns>List of all photos or error page if bad request</returns>
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
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                return View(id);
            }
        }
    }
}
