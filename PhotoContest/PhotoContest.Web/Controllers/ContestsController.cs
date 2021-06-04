using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        public ContestsController(PhotoContestContext context,
                                  IContestService contestService,
                                  ICategoryService categoryService,
                                  SignInManager<User> signInManager,
                                  IUserContestService userContestService,
                                  IPhotoService photoService,
                                  IUserService userService)
        {
            _context = context;
            this.contestService = contestService;
            this.categoryService = categoryService;
            this.signInManager = signInManager;
            this.userContestService = userContestService;
            this.photoService = photoService;
            this.userService = userService;
        }

        // GET: Contests
        [Authorize]
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("User"))
            {
                return View("AllOpen");
            }
            var contests = await this.contestService.GetAllAsync();
            return View(contests.Select(c => new ContetsViewModel(c)));
        }

        public async Task<IActionResult> AllOpen()
        {
            var contests = await this.contestService.AllOpenView();
            var userContests = await this.userContestService.GetAllUserContestsAsync();
            var photos = await this.photoService.GetAllAsync();
            return View(contests.Select(c => new ContetsViewModel(c) { AllUserContests = userContests, AllPhotos = photos.ToList() }));
        }

        /*public async Task<IActionResult> GetOpen()
        {
            return View();
        }*/

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
                    return View(contests.Select(c => new ContetsViewModel(c) { AllUserContests = userContests, AllPhotos = photos.ToList() }));
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }

            }
            return View();
        }


        // GET: Contests/Details/5
        [Authorize(Roles = "Admin, Organizer")]
        public async Task<IActionResult> Details(string name)
        {
            var contest = await this.contestService.FindContestByNameAsync(name);
            var contestDTO = new ContestDTO(contest);
            return View(new ContetsViewModel(contestDTO));
        }

        // GET: Contests/Create
        [Authorize(Roles = "Admin, Organizer")]
        public async Task<IActionResult> Create()
        {
            var categories = await this.categoryService.GetAllBaseAsync();
            ViewData["Categories"] = new SelectList(categories, "Name", "Name");
            return View();
        }

        // POST: Contests/Create
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
                    return BadRequest(e.Message);
                }

            }
            var categories = await this.categoryService.GetAllBaseAsync();
            ViewData["Categories"] = new SelectList(categories, "Name", "Name");
            return View();
        }

        // GET: Contests/Edit/5
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

        // POST: Contests/Edit/5
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

        // GET: Contests/Delete/5
        [Authorize(Roles = "Admin, Organizer")]
        public async Task<IActionResult> Delete(string name)
        {
            var contest = await this.contestService.FindContestByNameAsync(name);
            var contestDTO = new ContestDTO(contest);
            return View(new ContetsViewModel(contestDTO));
        }

        // POST: Contests/Delete/5
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
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            return RedirectToAction("Index");
        }

        // Get contest to enroll
        [Authorize(Roles = "Admin, Organizer")]
        public async Task<IActionResult> Invite(string name)
        {
            var users = await userService.GetAllAsync();
            ViewData["Users"] = new SelectList(users.Where(u => u.Rank != "Admin" && u.Rank != "Organizer"), "Username", "Username");
            var inviteViewModel = new InviteViewModel();
            inviteViewModel.Name = name;
            return View(inviteViewModel);
        }

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
                    return BadRequest(e.Message);
                }
            }
            return RedirectToAction("Index");
        }

        // Get contest to choose jury
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

        /*[Authorize(Roles = "User")]
        public async Task<IActionResult> GetByUser()
        {
            return View();
        }*/

        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetUserContests()
        {
            var contests = await this.contestService.GetUserContestsAsync();
            var userContests = await this.userContestService.GetAllUserContestsAsync();
            var photos = await this.photoService.GetAllAsync();
            return View(contests.Select(c => new ContetsViewModel(c) { AllUserContests = userContests, AllPhotos = photos.ToList() }));
        }

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

        [Authorize(Roles = "Admin, Organizer")]
        public async Task<IActionResult> GetByPhase()
        {
            return View();
        }

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
