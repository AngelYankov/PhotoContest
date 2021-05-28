using Microsoft.EntityFrameworkCore;
using PhotoContest.Data;
using PhotoContest.Data.Models;
using PhotoContest.Services.ExceptionMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly PhotoContestContext dbContext;
        public CategoryService(PhotoContestContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Create a category.
        /// </summary>
        /// <param name="categoryName">The name of the category to be created.</param>
        /// <returns>Returns the name of the created category or an appropriate error message.</returns>
        public async Task<string> CreateAsync(string categoryName)
        {
            var category = new Category();
            category.Name = categoryName;
            category.CreatedOn = DateTime.UtcNow;
            await this.dbContext.Categories.AddAsync(category);
            await this.dbContext.SaveChangesAsync();
            return category.Name;
        }

        /// <summary>
        /// Get all categories.
        /// </summary>
        /// <returns>Returns all names of the categories.</returns>
        public async Task<IEnumerable<string>> GetAllAsync()
        {
            return await this.dbContext.Categories.Where(c => c.IsDeleted == false).Select(c=>c.Name).ToListAsync();
        }

        /// <summary>
        /// Update name of a category.
        /// </summary>
        /// <param name="id">ID of the category to be updated.</param>
        /// <param name="newName">Name it should be updated to.</param>
        /// <returns>Returns the name of the updated category.</returns>
        public async Task<string> UpdateAsync(string categoryName, string newName)
        {
            var category = await FindCategoryByNameAsync(categoryName);
            category.Name = newName;
            category.ModifiedOn = DateTime.UtcNow;
            await this.dbContext.SaveChangesAsync();
            return category.Name;
        }

        /// <summary>
        /// Delete a category.
        /// </summary>
        /// <param name="id">ID of the category.</param>
        /// <returns>Returns true or false if the category is deleted succesfully or an appropriate error message.</returns>
        public async Task<bool> DeleteAsync(string categoryName)
        {
            var category = await FindCategoryByNameAsync(categoryName);
            category.IsDeleted = true;
            category.DeletedOn = DateTime.UtcNow;
            await this.dbContext.SaveChangesAsync();
            return category.IsDeleted;
        }

        /// <summary>
        /// Finds a category.
        /// </summary>
        /// <param name="id">ID of category to search for.</param>
        /// <returns>Returns category with that ID or an appropriate error message.</returns>
        public async Task<Category> FindCategoryByNameAsync(string categoryName)
        {
            var category = await dbContext.Categories.FirstOrDefaultAsync(c => c.Name == categoryName)
                                    ?? throw new ArgumentException(Exceptions.InvalidCategory);
            if (category.IsDeleted)
            {
                throw new ArgumentException(Exceptions.DeletedCategory);
            }
            return category;
        }
    }
}
