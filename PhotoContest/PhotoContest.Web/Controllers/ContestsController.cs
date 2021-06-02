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

        public ContestsController(PhotoContestContext context, IContestService contestService, ICategoryService categoryService, SignInManager<User> signInManager)
        {
            _context = context;
            this.contestService = contestService;
            this.categoryService = categoryService;
            this.signInManager = signInManager;
        }

        // GET: Contests
        //[Authorize]
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("User"))
            {
                ViewData["StatusId"] = new SelectList(_context.Statuses, "Name", "Name");
                return View("GetOpen");
            }
            var contests = await this.contestService.GetAllAsync();
            return View(contests.Select(c => new ViewModel(c)));
        }

        // GET: Open Contests
        public async Task<IActionResult> GetOpen()
        {
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Name", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetOpenFiltered(string status)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var contests = await this.contestService.GetAllOpenAsync(status);
                    return View(contests.Select(c => new ViewModel(c)));
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }

            }
            return View();
        }


        // GET: Contests/Details/5
        public async Task<IActionResult> Details(string name)
        {
            var contest = await this.contestService.FindContestByNameAsync(name);
            var contestDTO = new ContestDTO(contest);
            return View(new ViewModel(contestDTO));
        }

        // GET: Contests/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Name", "Name");
            return View();
        }

        // POST: Contests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateViewModel createViewModel)
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Name", "Name");
            return View();
        }

        // GET: Contests/Edit/5
        public async Task<IActionResult> Edit(string name)
        {

            var contest = await this.contestService.FindContestByNameAsync(name);
            var updateContestDTO = new EditViewModel();

            updateContestDTO.Name = contest.Name;
            updateContestDTO.Category = contest.Category.Name;
            updateContestDTO.IsContestOpen = contest.IsOpen;
            updateContestDTO.Phase1 = contest.Phase1.ToString("dd.MM.yy HH:mm");
            updateContestDTO.Phase2 = contest.Phase2.ToString("dd.MM.yy HH:mm");
            updateContestDTO.Finished = contest.Finished.ToString("dd.MM.yy HH:mm");

            ViewData["CategoryId"] = new SelectList(_context.Categories, "Name", "Name");
            return View(updateContestDTO);
        }

        // POST: Contests/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string name, EditViewModel editViewModel)
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Name", "Name");
            return View();
        }

        // GET: Contests/Delete/5
        public async Task<IActionResult> Delete(string name)
        {
            var contest = await this.contestService.FindContestByNameAsync(name);
            var contestDTO = new ContestDTO(contest);
            return View(new ViewModel(contestDTO));
        }

        // POST: Contests/Delete/5
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
        public async Task<IActionResult> Enroll()
        {
            ViewData["Contests"] = new SelectList(_context.Contests.Where(c => c.IsOpen == true), "Name", "Name");

            return View();
        }

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
        public async Task<IActionResult> Invite(string name)
        {
            ViewData["Contests"] = new SelectList(_context.Contests.Where(c => c.IsOpen == false), "Name", "Name");
            ViewData["Users"] = new SelectList(_context.Users.Where(u => u.Rank.Name != "Admin" && u.Rank.Name != "Organizer"), "UserName", "UserName");
            var inviteViewModel = new InviteViewModel();
            inviteViewModel.Name = name;
            return View(inviteViewModel);
        }

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
        public async Task<IActionResult> ChooseJury(string name)
        {
            ViewData["Users"] = new SelectList(_context.Users
                                                       .Where(u => u.Rank.Name == "Master" || u.Rank.Name == "Wise and Benevolent Photo Dictator"),
                                                                                                                           "UserName", "UserName");
            var inviteViewModel = new InviteViewModel();
            inviteViewModel.Name = name;
            return View(inviteViewModel);
        }

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

        public async Task<IActionResult> GetByUser()
        {
            return View();
        }

        [HttpPost, ActionName("GetByUserFiltered")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetByUserFiltered(string filter)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var contests = await this.contestService.GetByUserAsync(filter);
                    return View(contests.Select(c => new ViewModel(c)));
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> GetByPhase()
        {
            return View();
        }

        [HttpPost, ActionName("GetByPhaseFiltered")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetByPhaseFiltered(string phase, string sortBy, string orderBy)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var contests = await this.contestService.GetByPhaseAsync(phase, sortBy, orderBy);
                    return View(contests.Select(c => new ViewModel(c)));
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
