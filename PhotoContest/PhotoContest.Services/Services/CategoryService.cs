using PhotoContest.Data;
using PhotoContest.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotoContest.Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly PhotoContestContext dbContext;
        public CategoryService(PhotoContestContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public string Create(string categoryName)
        {
            var category = new Category();
            category.Name = categoryName;
            category.CreatedOn = DateTime.UtcNow;
            this.dbContext.SaveChanges();
            return category.Name;
        }

        public IList<string> GetAll()
        {
            return this.dbContext.Categories.Where(c => c.IsDeleted == false).Select(c => c.Name).ToList();
        }

        public string Update(Guid id, string newName)
        {
            var category = FindCategory(id);
            category.Name = newName;
            category.ModifiedOn = DateTime.UtcNow;
            this.dbContext.SaveChanges();
            return category.Name;
        }

        public bool Delete(Guid id)
        {
            var category = FindCategory(id);
            category.IsDeleted = true;
            category.DeletedOn = DateTime.UtcNow;
            this.dbContext.SaveChanges();
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
