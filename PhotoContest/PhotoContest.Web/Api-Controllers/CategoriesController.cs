using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoContest.Data;
using PhotoContest.Data.Models;
using PhotoContest.Services.Contracts;
using PhotoContest.Services.Services;

namespace PhotoContest.Web.Api_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        //private readonly IUserService userService;
        //private readonly SignInManager<User> signInManager;

        public CategoriesController(ICategoryService categoryService/*, SignInManager<User> signInManager*/)
        {
            this.categoryService = categoryService;
            //this.userService = userService;
            //this.signInManager = signInManager;
        }

        /// <summary>
        /// Get all categories.
        /// </summary>
        /// <returns>returns all categories.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllAsync()
        {
            var categories = await this.categoryService.GetAllAsync();
            return Ok(categories);
        }

        /// <summary>
        /// Update a category.
        /// </summary>
        /// <param name="id">ID of the category to update.</param>
        /// <param name="name">Name of the category to be updated.</param>
        /// <returns>Returns the updated category or an appropriate error message.</returns>
        /*[HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync( Guid id, string name)
        {
            try
            {
                var result = await this.signInManager.PasswordSignInAsync(username, password, false, false);
                if (result.Succeeded)
                {
                    var category = await this.categoryService.UpdateAsync(id, name);
                    return Ok(category);
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }*/

        /// <summary>
        /// Create a category.
        /// </summary>
        /// <param name="name">Name of the category to be created.</param>
        /// <returns>Returns the created category or an appropriate error message.</returns>
        [HttpPost]
        public async Task<ActionResult<Category>> CreateAsync(string name)
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
        /// <param name="id">ID of the category to delete.</param>
        /// <returns>Returns NoContent or an appropriate error message.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> DeleteAsync(Guid id)
        {
            try
            {
                await this.categoryService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
