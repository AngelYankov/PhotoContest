using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
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
        private readonly IToastNotification toastNotification;

        public CategoriesController(PhotoContestContext context, ICategoryService categoryService, IToastNotification toastNotification)
        {
            this._context = context;
            this.categoryService = categoryService;
            this.toastNotification = toastNotification;
        }

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns>Returns a list with all categories</returns>
        public async Task<IActionResult> Index()
        {
            var categories = await this.categoryService.GetAllAsync();
            var categoryViewModel = categories.Select(c=>new CategoriesViewModel() { Name = c });
           
            return View(categoryViewModel);
        }

        /// <summary>
        /// Shows a form to create new category
        /// </summary>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Create category
        /// </summary>
        /// <param name="categoriesViewModel">Created category details</param>
        /// <returns>Goes to the list of categories if successful or a client side validation message</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoriesViewModel categoriesViewModel)
        {
            if (ModelState.IsValid)
            {
                await this.categoryService.CreateAsync(categoriesViewModel.Name);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        /// <summary>
        /// Get a category and show a form to edit it
        /// </summary>
        /// <param name="name">Name of the category to edit</param>
        public async Task<IActionResult> Edit(string name)
        {
            var category = await this.categoryService.FindCategoryByNameAsync(name);
            var categoryView = new CategoriesViewModel();
            categoryView.Name = category.Name;
            return View(categoryView);
        }

        /// <summary>
        /// Edit a category
        /// </summary>
        /// <param name="name">Name of the category to update</param>
        /// <param name="update">New name of the category</param>
        /// <returns>Goes to the list of categories if successful or an error page if BadRequest</returns>
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
                    toastNotification.AddErrorToastMessage("Name of category should be atleast 3 characters long", new NotyOptions());
                    var path = Request.Path.Value.ToString() + "?name=" + update.Name;
                    return Redirect(path);
                }

            }
            return View();
        }

        /// <summary>
        /// Get a category to delete
        /// </summary>
        /// <param name="name">Name of the category to delete</param>
        public async Task<IActionResult> Delete(string name)
        {
            var category = await this.categoryService.FindCategoryByNameAsync(name);
            var categoryView = new CategoriesViewModel();
            categoryView.Name = name;
            return View(categoryView);
        }

        /// <summary>
        /// Delete a category
        /// </summary>
        /// <param name="name">Name of the category to delete</param>
        /// <returns>Goes to the list of categories if successful or an error page if BadRequest</returns>
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
                catch (Exception e)
                {
                    toastNotification.AddErrorToastMessage(e.Message, new NotyOptions());
                    var path = Request.Path.Value.ToString() + name;
                    return Redirect(path);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
