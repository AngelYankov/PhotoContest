using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoContest.Services.Contracts;
using PhotoContest.Services.Models;
using PhotoContest.Services.Services;
using PhotoContest.Web.Models;
using PhotoContest.Web.Models.HomeViewModels;
using System;
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

        public async Task<IActionResult> Index()
        {
            var allContests = await this.contestService.GetAllAsync();
            var phase1Contests = allContests.Where(c => c.Status == "Phase 1");
            var photos = await this.photoService.GetAllBaseAsync();
            var finishedContests = await this.contestService.GetAllFinishedContestsAsync();
            
            foreach (var contest in finishedContests)
            {
                foreach (var photo in photos)
                {
                    if(photo.Contest.Name == contest.Name)
                    {
                        contest.Photos.Add(photo);
                    }
                }
                contest.Photos = contest.Photos.OrderByDescending(p => p.AllPoints).ToList();
            }
            return View(new HomeContestsViewModel() { FinishedContests = finishedContests.ToList(), 
                                                      Phase1Contests = phase1Contests.ToList() });
        }

        public IActionResult Privacy()
        {
            return View();
        }
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
