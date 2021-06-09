using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoContest.Services.Contracts;
using PhotoContest.Services.Models;
using PhotoContest.Services.Services;
using PhotoContest.Web.Models;
using PhotoContest.Web.Models.HomeViewModels;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoContest.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IContestService contestService;
        private readonly IPhotoService photoService;

        public HomeController(ILogger<HomeController> logger, IContestService contestService, IPhotoService photoService)
        {
            _logger = logger;
            this.contestService = contestService;
            this.photoService = photoService;
        }

        /// <summary>
        /// Home page of the website
        /// </summary>
        /// <returns>List of latest winning photos and list of newest contests</returns>
        public async Task<IActionResult> Index()
        {
            var allContests = await this.contestService.GetAllAsync();
            var phase1Contests = allContests.Where(c => c.Status == "Phase 1" && c.OpenOrInvitational == "Open");
            var photos = await this.photoService.GetAllBaseAsync();
            var finishedContests = await this.contestService.GetAllFinishedContestsAsync();
            var photosFirstPlaces = new List<PhotoDTO>();

            foreach (var contest in finishedContests)
            {
                foreach (var photo in photos)
                {
                    if (photo.Contest.Name == contest.Name)
                    {
                        photosFirstPlaces.Add(new PhotoDTO(photo));
                        break;
                    }
                }
            }
            return View(new HomeContestsViewModel()
            {
                PhotosFirstPlace = photosFirstPlaces,
                Phase1Contests = phase1Contests.ToList()
            });
        }

        /// <summary>
        /// Shows the privacy page of the website
        /// </summary>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Error page of the website 404 Not found
        /// </summary>
        public IActionResult PageNotFound()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
