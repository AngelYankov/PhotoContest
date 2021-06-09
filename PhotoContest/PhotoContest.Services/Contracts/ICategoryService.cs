using PhotoContest.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoContest.Services.Services
{
    public interface ICategoryService
    {
        Task<string> CreateAsync(string categoryName);
        Task<IEnumerable<string>> GetAllAsync();
        Task<IEnumerable<Category>> GetAllBaseAsync();
        Task<string> UpdateAsync(string categoryName, string newName);
        Task<bool> DeleteAsync(string categoryName);
        Task<Category> FindCategoryByNameAsync(string categoryName);
    }
}