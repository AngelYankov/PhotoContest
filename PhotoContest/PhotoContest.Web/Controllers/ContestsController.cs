using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using PhotoContest.Data;
using PhotoContest.Data.Models;
using PhotoContest.Services.Contracts;
using PhotoContest.Services.Models;
using PhotoContest.Services.Models.Create;
using PhotoContest.Services.Models.Update;
using PhotoContest.Services.Services;
using PhotoContest.Web.Models;
using PhotoContest.Web.Models.ContestViewModels;

namespace PhotoContest.Web.Controllers
{
    public class ContestsController : Controller
    {
        private readonly PhotoContestContext _context;
        private readonly IContestService contestService;
        private readonly ICategoryService categoryService;
        private readonly SignInManager<User> signInManager;
        private readonly IUserContestService userContestService;
        private readonly IPhotoService photoService;
        private readonly IUserService userService;
        private readonly IToastNotification toastNotification;

        public ContestsController(PhotoContestContext context,
                                  IContestService contestService,
                                  ICategoryService categoryService,
                                  SignInManager<User> signInManager,
                                  IUserContestService userContestService,
                                  IPhotoService photoService,
                                  IUserService userService,
                                  IToastNotification toastNotification)
        {
            _context = context;
            this.contestService = contestService;
            this.categoryService = categoryService;
            this.signInManager = signInManager;
            this.userContestService = userContestService;
            this.photoService = photoService;
            this.userService = userService;
            this.toastNotification = toastNotification;
        }

        /// <summary>
        /// Get all contests
        /// </summary>
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var contests = await this.contestService.GetAllAsync();
            if (User.IsInRole("User"))
            {
                return RedirectToAction("AllOpen");
            }
            else
                return View(contests.Select(c => new ContetsViewModel(c)));
        }

        /// <summary>
        /// Get all open contests
        /// </summary>
        /// <returns>Return a list with all open contests</returns>
        [Authorize]
        public async Task<IActionResult> AllOpen()
        {
            var contests = await this.contestService.AllOpenViewAsync();
            var userContests = await this.userContestService.GetAllUserContestsAsync();
            var photos = await this.photoService.GetAllAsync();
            var juries = await this.contestService.GetAllJuriesAsync();
            return View(contests.Select(c => new ContetsViewModel(c) { AllUserContests = userContests, AllPhotos = photos.ToList(), Juries = juries.ToList() }));
        }

        /// <summary>
        /// Filter all open contests
        /// </summary>
        /// <param name="status">Phase of the contests to filter by</param>
        /// <returns>A list of all open contests after filtering</returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> GetOpenFiltered(string status)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var contests = await this.contestService.GetAllOpenAsync(status);
                    var userContests = await this.userContestService.GetAllUserContestsAsync();
                    var photos = await this.photoService.GetAllAsync();
                    var juries = await this.contestService.GetAllJuriesAsync();
                    return View(contests.Select(c => new ContetsViewModel(c) { AllUserContests = userContests, AllPhotos = photos.ToList(), Juries = juries.ToList() }));
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            return View();
        }


        /// <summary>
        /// Get details of a contest
        /// </summary>
        /// <param name="name">Name of the contest</param>
        [Authorize]
        public async Task<IActionResult> Details(string name)
        {
            var contest = await this.contestService.FindContestByNameAsync(name);
            var userContests = await this.userContestService.GetAllUserContestsAsync();
            var photos = await this.photoService.GetAllAsync();
            var juries = await this.contestService.GetAllJuriesAsync();
            var contestDTO = new ContestDTO(contest);
            return View(new ContetsViewModel(contestDTO) { AllUserContests = userContests, AllPhotos = photos.ToList(), Juries = juries.ToList() });
        }

        /// <summary>
        /// Show a for to create a contest
        /// </summary>
        [Authorize(Roles = "Admin, Organizer")]
        public async Task<IActionResult> Create()
        {
            var categories = await this.categoryService.GetAllBaseAsync();
            ViewData["Categories"] = new SelectList(categories, "Name", "Name");
            return View();
        }

        /// <summary>
        /// Create a contest
        /// </summary>
        /// <param name="createViewModel">Details of the contest to create</param>
        /// <returns>To list of all contests if successful or go to error page for bad request</returns>
        [Authorize(Roles = "Admin, Organizer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateContestViewModel createViewModel)
        {
            var newContestDTO = new NewContestDTO()
            {
                Name = createViewModel.Name,
                CategoryName = createViewModel.CategoryName,
                IsOpen = createViewModel.IsOpen,
                Phase1 = createViewModel.Phase1,
                Phase2 = createViewModel.Phase2,
                Finished = createViewModel.Finished
            };

            if (ModelState.IsValid)
            {
                try
                {
                    await this.contestService.CreateAsync(newContestDTO);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    toastNotification.AddErrorToastMessage(e.Message, new NotyOptions());
                    return Redirect(Request.Path.Value.ToString());
                }
            }
            var categories = await this.categoryService.GetAllBaseAsync();
            ViewData["Categories"] = new SelectList(categories, "Name", "Name");
            return View();
        }

        /// <summary>
        /// Show for to edit a contest
        /// </summary>
        /// <param name="name">Name of the contest to edit</param>
        [Authorize(Roles = "Admin, Organizer")]
        public async Task<IActionResult> Edit(string name)
        {
            var contest = await this.contestService.FindContestByNameAsync(name);
            var updateContestDTO = new EditContestViewModel();

            updateContestDTO.Name = contest.Name;
            updateContestDTO.Category = contest.Category.Name;
            updateContestDTO.IsContestOpen = contest.IsOpen;
            updateContestDTO.Phase1 = contest.Phase1.ToString("dd.MM.yy HH:mm");
            updateContestDTO.Phase2 = contest.Phase2.ToString("dd.MM.yy HH:mm");
            updateContestDTO.Finished = contest.Finished.ToString("dd.MM.yy HH:mm");

            var categories = await this.categoryService.GetAllBaseAsync();
            ViewData["Categories"] = new SelectList(categories, "Name", "Name");
            return View(updateContestDTO);
        }

        /// <summary>
        /// Edit a contest
        /// </summary>
        /// <param name="name">Name of the contest to edit</param>
        /// <param name="editViewModel">Details to edit</param>
        /// <returns>To list of all contests if successful or go to error page for bad request</returns>
        [Authorize(Roles = "Admin, Organizer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string name, EditContestViewModel editViewModel)
        {
            var updateContestDTO = new UpdateContestDTO();
            updateContestDTO.CategoryName = editViewModel.Category;
            updateContestDTO.IsOpen = editViewModel.IsContestOpen;
            updateContestDTO.Phase1 = editViewModel.Phase1;
            updateContestDTO.Phase2 = editViewModel.Phase2;
            updateContestDTO.Finished = editViewModel.Finished;
            if (ModelState.IsValid)
            {
                try
                {
                    await this.contestService.UpdateAsync(name, updateContestDTO);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            var categories = await this.categoryService.GetAllBaseAsync();
            ViewData["Categories"] = new SelectList(categories, "Name", "Name");
            return View();
        }

        /// <summary>
        /// Get a contest to delete
        /// </summary>
        /// <param name="name">Name of the contest to delete</param>
        [Authorize(Roles = "Admin, Organizer")]
        public async Task<IActionResult> Delete(string name)
        {
            var contest = await this.contestService.FindContestByNameAsync(name);
            var contestDTO = new ContestDTO(contest);
            return View(new ContetsViewModel(contestDTO));
        }

        /// <summary>
        /// Delete a contest
        /// </summary>
        /// <param name="name">Name of the contest to delete</param>
        /// <returns>To list of all contests if successful or go to error page for bad request</returns>
        [Authorize(Roles = "Admin, Organizer")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string name)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await this.contestService.DeleteAsync(name);
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return RedirectToAction("Error");
                }
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Get contest for user to enroll
        /// </summary>
        /// <param name="name">Name of the contest to enroll in</param>
        /// <returns>A view asking for confirmation</returns>
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Enroll(string name)
        {
            var contests = await this.contestService.GetAllAsync();
            ViewData["Contests"] = new SelectList(contests.Where(c => c.OpenOrInvitational == "Open"), "Name", "Name");
            var contest = await this.contestService.FindContestByNameAsync(name);
            var enrolView = new ContetsViewModel(new ContestDTO(contest));
            enrolView.Name = name;
            return View(enrolView);
        }

        /// <summary>
        /// Submit the enrollment
        /// </summary>
        /// <param name="name">Name of the contest to enroll in</param>
        /// <returns>To a list of user's contests or to an error page if bad request</returns>
        [Authorize(Roles = "User")]
        [HttpPost, ActionName("EnrollSubmit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnrollSubmit(string name)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await this.contestService.EnrollAsync(name);
                    return RedirectToAction("GetUserContests");
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Get contest to invite user to
        /// </summary>
        /// <param name="name">Name of the contest to ivite to</param>
        /// <returns>Shows a dropdown of all available users</returns>
        [Authorize(Roles = "Admin, Organizer")]
        public async Task<IActionResult> Invite(string name)
        {
            var users = await userService.GetAllAsync();
            ViewData["Users"] = new SelectList(users.Where(u => u.Rank != "Admin" && u.Rank != "Organizer"), "Username", "Username");
            var inviteViewModel = new InviteViewModel();
            inviteViewModel.Name = name;
            return View(inviteViewModel);
        }

        /// <summary>
        /// Submit Invite request
        /// </summary>
        /// <param name="inviteViewModel">Details of the user invited and the contest name</param>
        /// <returns>List of all contests or error page for bad request</returns>
        [Authorize(Roles = "Admin, Organizer")]
        [HttpPost, ActionName("Invite")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Invite(InviteViewModel inviteViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await this.contestService.InviteAsync(inviteViewModel.Name, inviteViewModel.Username);
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    toastNotification.AddErrorToastMessage(e.Message, new NotyOptions());
                    var path = Request.Path.Value.ToString()+"?name="+ inviteViewModel.Name; 
                    return Redirect(path);
                }
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Get contest to choose jury for
        /// </summary>
        /// <param name="name">Name of the contest to choose for</param>
        /// <returns>Shows a list of available users to choose from</returns>
        [Authorize(Roles = "Admin, Organizer")]
        public async Task<IActionResult> ChooseJury(string name)
        {
            var users = await this.userService.GetAllAsync();
            ViewData["Users"] = new SelectList(users.Where(u => u.Rank == "Master" || u.Rank == "Wise and Benevolent Photo Dictator"),
                                                                                                                            "Username", "Username");
            var inviteViewModel = new InviteViewModel();
            inviteViewModel.Name = name;
            return View(inviteViewModel);
        }

        /// <summary>
        /// Submit chosen jury for contest
        /// </summary>
        /// <param name="inviteViewModel">Name of user chosen and name of contest</param>
        /// <returns>List of all contets or error page for bad request</returns>
        [Authorize(Roles = "Admin, Organizer")]
        [HttpPost, ActionName("ChooseJury")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChooseJury(InviteViewModel inviteViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await this.contestService.ChooseJuryAsync(inviteViewModel.Name, inviteViewModel.Username);
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Get contests for signed in user
        /// </summary>
        /// <returns>List of signed in user's contests</returns>
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetUserContests()
        {
            var contests = await this.contestService.GetUserContestsAsync();
            var userContests = await this.userContestService.GetAllUserContestsAsync();
            var photos = await this.photoService.GetAllAsync();
            return View(contests.Select(c => new ContetsViewModel(c) { AllUserContests = userContests, AllPhotos = photos.ToList() }));
        }

        /// <summary>
        /// Filter signed in user's contests
        /// </summary>
        /// <param name="filter">Phase to filter by</param>
        /// <returns>List of filtered signed in user's contests</returns>
        [Authorize(Roles = "User")]
        [HttpPost, ActionName("GetByUserFiltered")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetByUserFiltered(string filter)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var contests = await this.contestService.GetByUserAsync(filter);
                    var userContests = await this.userContestService.GetAllUserContestsAsync();
                    var photos = await this.photoService.GetAllAsync();
                    return View(contests.Select(c => new ContetsViewModel(c) { AllUserContests = userContests, AllPhotos = photos.ToList(), Filter = filter }));

                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Get filtered contests
        /// </summary>
        /// <param name="contetsViewModel">Filters chosen</param>
        /// <returns>List of filtered contests or error page for bad request</returns>
        [Authorize(Roles = "Admin, Organizer")]
        [HttpPost, ActionName("GetByPhaseFiltered")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetByPhaseFiltered(ContetsViewModel contetsViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var phase = contetsViewModel.Filter;
                    var sortBy = contetsViewModel.SortBy;
                    var orderBy = contetsViewModel.OrderBy;
                    var contests = await this.contestService.GetByPhaseAsync(phase, sortBy, orderBy);
                    return View(contests.Select(c => new ContetsViewModel(c)));
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
