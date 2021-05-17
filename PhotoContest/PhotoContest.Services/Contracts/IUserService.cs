using PhotoContest.Services.Models;
using PhotoContest.Services.Models.Create;
using PhotoContest.Services.Models.Update;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Services.Contracts
{
    public interface IUserService
    {
        Task<UserDTO> Create(NewUserDTO newUserDTO);
        Task<IEnumerable<UserDTO>> GetAll();
        Task<UserDTO> Get(Guid id);
        Task<UserDTO> Update(UpdateUserDTO updateUserDTO, Guid id);
        Task<bool> Delete(Guid id);
    }
}
