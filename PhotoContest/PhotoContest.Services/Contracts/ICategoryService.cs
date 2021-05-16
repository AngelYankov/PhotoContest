using System;
using System.Collections.Generic;

namespace PhotoContest.Services.Services
{
    public interface ICategoryService
    {
        string Create(string categoryName);
        IList<string> GetAll();
        string Update(Guid id, string newName);
        bool Delete(Guid id);
    }
}