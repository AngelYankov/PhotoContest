using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhotoContest.Data;
using PhotoContest.Data.Models;
using PhotoContest.Services.Models;
using PhotoContest.Services.Models.Create;
using PhotoContest.Services.Models.Update;
using PhotoContest.Services.Services;

namespace PhotoContest.Web
{
    public class CController : Controller
    {
        private readonly PhotoContestContext _context;
        private readonly IContestService contestService;
        private readonly ICategoryService categoryService;

        public CController(PhotoContestContext context, IContestService contestService, ICategoryService categoryService)
        {
            _context = context;
            this.contestService = contestService;
            this.categoryService = categoryService;
        }

        // GET: Contests
        public async Task<IActionResult> Index()
        {
            var contests = await this.contestService.GetAllAsync();
            return View(contests);
        }

        // GET: Contests/Details/5
        public async Task<IActionResult> Details(string name)
        {
           var contest = await this.contestService.FindContestByNameAsync(name);

            return View(new ContestDTO(contest));
        }

        // GET: Contests/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(this._context.Categories, "Name", "Name");
            return View();
        }

        // POST: Contests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewContestDTO newContestDTO)
        {
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

            ViewData["CategoryId"] = new SelectList(_context.Categories, "Name", "Name");
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Name", "Name");
            return View(new ContestDTO(contest));
        }

        // POST: Contests/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string name, UpdateContestDTO updateContestDTO)
        {
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
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Name", "Name");
            return View();
        }

        // GET: Contests/Delete/5
        public async Task<IActionResult> Delete(string name)
        {
            var contest = await this.contestService.FindContestByNameAsync(name);

            return View(new ContestDTO(contest));
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
    }
}
