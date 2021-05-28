using PhotoContest.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoContest.Services.Services
{
    public interface ICategoryService
    {
        Task<string> CreateAsync(string categoryName);
        Task<IEnumerable<string>> GetAllAsync();
        Task<string> UpdateAsync(string categoryName, string newName);
        Task<bool> DeleteAsync(string categoryName);
        Task<Category> FindCategoryByNameAsync(string categoryName);
    }
}