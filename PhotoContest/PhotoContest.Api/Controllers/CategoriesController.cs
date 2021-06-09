using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotoContest.Services.Services;
using System;
using System.Threading.Tasks;

namespace PhotoContest.Api.Controllers
{
    [Authorize(Roles = "Admin,Organizer")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        /// <summary>
        /// Get all categories.
        /// </summary>
        /// <returns>returns all categories.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var categories = await this.categoryService.GetAllAsync();
            return Ok(categories);
        }

        /// <summary>
        /// Update a category.
        /// </summary>
        /// <param name="categoryName">Name of the category to update.</param>
        /// <param name="name">New name of the category.</param>
        /// <returns>Returns the updated category or an appropriate error message.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromQuery]string categoryName, string name)
        {
            try
            {
                var category = await this.categoryService.UpdateAsync(categoryName, name);
                return Ok(category);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Create a category.
        /// </summary>
        /// <param name="name">Name of the category to be created.</param>
        /// <returns>Returns the created category or an appropriate error message.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync(string name)
        {
            try
            {
                var category = await this.categoryService.CreateAsync(name);
                return Created("post", category);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Delete a category.
        /// </summary>
        /// <param name="categoryName">Name of the category to delete.</param>
        /// <returns>Returns NoContent or an appropriate error message.</returns>
        [HttpDelete("{categoryName}")]
        public async Task<IActionResult> DeleteAsync(string categoryName)
        {
            try
            {
                await this.categoryService.DeleteAsync(categoryName);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
