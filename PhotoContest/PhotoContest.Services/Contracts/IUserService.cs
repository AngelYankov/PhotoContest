using PhotoContest.Data;
using PhotoContest.Services.Models;
using PhotoContest.Services.Models.Create;
using PhotoContest.Services.Models.SecurityModels;
using PhotoContest.Services.Models.Update;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Services.Contracts
{
    public interface IUserService
    {
        Task<UserDTO> CreateAsync(NewUserDTO newUserDTO);
        Task<IEnumerable<UserDTO>> GetAllAsync();
        Task<UserDTO> GetAsync(Guid id);
        Task<UserDTO> UpdateAsync(UpdateUserDTO updateUserDTO, Guid id);
        Task<bool> DeleteAsync(Guid id);
        Task<User> GetUserAsync(string username);
        Task<string> AddRoleAsync(AddRoleModel model);
    }
}
