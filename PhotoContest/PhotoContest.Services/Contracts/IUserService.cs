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
        Task<UserDTO> CreateOrganizerAsync(NewUserDTO newUserDTO);
        Task<IEnumerable<UserDTO>> GetAllAsync();
        Task<UserDTO> UpdateAsync(UpdateUserDTO updateUserDTO, string username);
        Task<bool> DeleteAsync(string username);
        Task<User> GetUserByUsernameAsync(string username);
        Task<UserDTO> ShowMyAccountInfo();
       /*Task<string> AddRoleAsync(AddRoleModel model);*/      
        Task<IEnumerable<UserDTO>> GetAllParticipantsAsync();
        Task ChangeRank();
        
    }
}
