using Microsoft.EntityFrameworkCore;
using PhotoContest.Data;
using PhotoContest.Data.Models;
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

        public async Task<string> CreateAsync(string categoryName)
        {
            var category = new Category();
            category.Name = categoryName;
            category.CreatedOn = DateTime.UtcNow;
            await this.dbContext.SaveChangesAsync();
            return category.Name;
        }

        public async Task<IList<string>> GetAllAsync()
        {
            return await this.dbContext.Categories.Where(c => c.IsDeleted == false).Select(c => c.Name).ToListAsync();
        }

        public async Task<string> UpdateAsync(Guid id, string newName)
        {
            var category = FindCategory(id);
            category.Name = newName;
            category.ModifiedOn = DateTime.UtcNow;
            await this.dbContext.SaveChangesAsync();
            return category.Name;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var category = FindCategory(id);
            category.IsDeleted = true;
            category.DeletedOn = DateTime.UtcNow;
            await this.dbContext.SaveChangesAsync();
            return category.IsDeleted;
        }

        private Category FindCategory(Guid id)
        {
            var category = dbContext.Categories.FirstOrDefault(c => c.Id == id)
                                    ?? throw new ArgumentException();
            if (category.IsDeleted)
            {
                throw new ArgumentException();
            }
            return category;
        }
    }
}
