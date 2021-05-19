using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PhotoContest.Data;
using PhotoContest.Services.Contracts;
using PhotoContest.Services.Models;
using PhotoContest.Services.Models.Create;
using PhotoContest.Services.Models.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Services.Services
{
    public class UserService : IUserService
    {
        private readonly PhotoContestContext dbContext;
        private readonly IMapper mapper;

        public UserService(PhotoContestContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        /// <summary>
        /// Create new user.
        /// </summary>
        /// <param name="newUserDTO">Details of new user to be created.</param>
        /// <returns>Returns created user or an appropriate error message.</returns>
        public async Task<UserDTO> CreateAsync(NewUserDTO newUserDTO)
        {
            if (newUserDTO.FirstName == null) throw new ArgumentException();
            if (newUserDTO.LastName == null) throw new ArgumentException();
            if (newUserDTO.RankId == Guid.Empty) throw new ArgumentException();
            var user = mapper.Map<User>(newUserDTO);
            user.CreatedOn = DateTime.UtcNow;
            await this.dbContext.AddAsync(user);
            await this.dbContext.SaveChangesAsync();
            return new UserDTO(user);
        }
        /// <summary>
        /// Delete a user.
        /// </summary>
        /// <param name="id">Id of user to search for.</param>
        /// <returns>Returns true if delete succesfully or an appropriate error message.</returns>
        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await FindUser(id);
            user.IsDeleted = true;
            user.DeletedOn = DateTime.UtcNow;
            await this.dbContext.SaveChangesAsync();
            return user.IsDeleted;
        }
        /// <summary>
        /// Get a user by id.
        /// </summary>
        /// <param name="id">Id to search for.</param>
        /// <returns>Returns user with that id or an appropriate error message.</returns>
        public async Task<UserDTO> GetAsync(Guid id)
        {
            var user = await FindUser(id);
            return new UserDTO(user);
        }
        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns>Returns all users.</returns>
        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            return await this.dbContext.Users
                                       .Include(u => u.Rank)
                                       .Where(u => u.IsDeleted == false)
                                       .Select(u => new UserDTO(u))
                                       .ToListAsync();
        }
        /// <summary>
        /// Update a user.
        /// </summary>
        /// <param name="updateUserDTO">Details of user to be updated.</param>
        /// <param name="id">Id to search for.</param>
        /// <returns>Returns updated user or an appropriate error message.</returns>
        public async Task<UserDTO> UpdateAsync(UpdateUserDTO updateUserDTO, Guid id)
        {
            var user = await FindUser(id);
            user.FirstName = updateUserDTO.FirstName ?? user.FirstName;
            user.LastName = updateUserDTO.LastName ?? user.LastName;
            if (updateUserDTO.RankId == Guid.Empty) throw new ArgumentException();
            user.RankId = updateUserDTO.RankId;
            user.ModifiedOn = DateTime.UtcNow;
            await this.dbContext.SaveChangesAsync();
            return new UserDTO(user);
        }
        /// <summary>
        /// Get user by username.
        /// </summary>
        /// <param name="username">Username to search for.</param>
        /// <returns>Returns user with that username or an appropriate error message.</returns>
        public async Task<User> GetUserAsync(string username)
        {
            return await this.dbContext
                             .Users
                             .Where(c => c.IsDeleted == false)
                             .FirstOrDefaultAsync(c => c.UserName.Equals(username, StringComparison.OrdinalIgnoreCase))
                             ?? throw new ArgumentException();
        }
        /// <summary>
        /// Get user by id.
        /// </summary>
        /// <param name="id">Id to search for.</param>
        /// <returns>Returns user with that id or an appropriate error message.</returns>
        private async Task<User> FindUser(Guid id)
        {
            return await this.dbContext.Users
                                       .Include(u => u.Rank)
                                       .Where(u => u.IsDeleted == false)
                                       .FirstOrDefaultAsync(u => u.Id == id)
                                       ?? throw new ArgumentException();
        }
    }
}
