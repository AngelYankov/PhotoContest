using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoContest.Data;
using PhotoContest.Data.Models;
using PhotoContest.Services.Services;

namespace PhotoContest.Web.Api_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllAsync()
        {
            var categories = await this.categoryService.GetAllAsync();
            return Ok(categories);
        }

        // PUT: api/Categories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, string name)
        {
            try
            {
                var category = await this.categoryService.UpdateAsync(id, name);
                return Ok(category);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        // POST: api/Categories
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

        // DELETE: api/Categories/5
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
