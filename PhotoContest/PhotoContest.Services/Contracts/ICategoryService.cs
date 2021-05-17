using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoContest.Services.Services
{
    public interface ICategoryService
    {
        Task<string> CreateAsync(string categoryName);
        Task<IList<string>> GetAllAsync();
        Task<string> UpdateAsync(Guid id, string newName);
        Task<bool> DeleteAsync(Guid id);
    }
}