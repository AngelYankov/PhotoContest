using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhotoContest.Data;
using PhotoContest.Services.Services;
using PhotoContest.Web.Models.CategoryViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoContest.Web.Controllers
{
    [Authorize(Roles = "Admin, Organizer")]
    public class CategoriesController : Controller
    {
        private readonly PhotoContestContext _context;
        private readonly ICategoryService categoryService;

        public CategoriesController(PhotoContestContext context, ICategoryService categoryService)
        {
            this._context = context;
            this.categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await this.categoryService.GetAllAsync();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: Contests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string name)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await this.categoryService.CreateAsync(name);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }

            }
            return View();
        }

        public async Task<IActionResult> Edit(string name)
        {
            var category = await this.categoryService.FindCategoryByNameAsync(name);
            var categoryView = new CategoriesViewModel();
            categoryView.Name = category.Name;
            return View(categoryView);
        }

        // POST: Contests/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string name, CategoriesViewModel update)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await this.categoryService.UpdateAsync(name, update.NewName);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }

            }
            return View();
        }

        // GET: Contests/Delete/5
        public async Task<IActionResult> Delete(string name)
        {
            var category = await this.categoryService.FindCategoryByNameAsync(name);
            var categoryView = new CategoriesViewModel();
            categoryView.Name = name;
            return View(categoryView);
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
                    await this.categoryService.DeleteAsync(name);
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
