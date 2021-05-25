using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PhotoContest.Data;
using PhotoContest.Data.Models;
using PhotoContest.Services.Contracts;
using PhotoContest.Services.ExceptionMessages;
using PhotoContest.Services.Models;
using PhotoContest.Services.Models.Create;
using PhotoContest.Services.Models.SecurityModels;
using PhotoContest.Services.Models.Update;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Services.Services
{
    public class UserService : IUserService
    {
        private readonly PhotoContestContext dbContext;
        private readonly UserManager<User> userManager;
        public UserService(PhotoContestContext dbContext, UserManager<User> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        /// <summary>
        /// Create new user.
        /// </summary>
        /// <param name="newUserDTO">Details of new user to be created.</param>
        /// <returns>Returns created user or an appropriate error message.</returns>
        public async Task<UserDTO> CreateAsync(NewUserDTO newUserDTO)
        {
            var user = new User()
            {
                FirstName = newUserDTO.FirstName ?? throw new ArgumentException(Exceptions.RequiredFirstName),
                LastName = newUserDTO.LastName ?? throw new ArgumentException(Exceptions.RequiredLastName),
                Email = newUserDTO.Email ?? throw new ArgumentException(Exceptions.RequiredEmail),
                UserName = newUserDTO.Email,
                Rank = await this.dbContext.Ranks.FirstOrDefaultAsync(r=>r.Name.ToLower() == "junkie"),
                CreatedOn = DateTime.UtcNow
            };
            /*var rank = await this.dbContext.Ranks.FirstAsync();
            user.RankId = rank.Id;*/
            if (await this.userManager.FindByEmailAsync(newUserDTO.Email) == null)
            {
                var result = await this.userManager.CreateAsync(user, newUserDTO.Password);
                if (result.Succeeded)
                {
                    await this.userManager.AddToRoleAsync(user, "User");
                }
                else { throw new ArgumentException(Exceptions.IncorrectPassword); }
            }
            else { throw new ArgumentException(Exceptions.ExistingEmail); }
            // await this.dbContext.Users.AddAsync(user);
            await this.dbContext.SaveChangesAsync();
            return new UserDTO(user);
        }
        /// <summary>
        /// Create user with role organizer.
        /// </summary>
        /// <param name="newUserDTO">Details of new user to be created.</param>
        /// <returns>Returns created user or an appropriate error message.</returns>
        public async Task<UserDTO> CreateOrganizerAsync(NewUserDTO newUserDTO)
        {
            var user = new User()
            {
                FirstName = newUserDTO.FirstName ?? throw new ArgumentException(Exceptions.RequiredFirstName),
                LastName = newUserDTO.LastName ?? throw new ArgumentException(Exceptions.RequiredLastName),
                Email = newUserDTO.Email ?? throw new ArgumentException(Exceptions.RequiredEmail),
                UserName = newUserDTO.Email,
                Rank = await this.dbContext.Ranks.FirstOrDefaultAsync(r=>r.Name.ToLower() == "organizer"),
                CreatedOn = DateTime.UtcNow
            };
            if (await this.userManager.FindByEmailAsync(newUserDTO.Email) == null)
            {
                var result = await this.userManager.CreateAsync(user, newUserDTO.Password);
                if (result.Succeeded)
                {
                    await this.userManager.AddToRoleAsync(user, "Organizer");
                }
                else { throw new ArgumentException(Exceptions.IncorrectPassword); }
            }
            else { throw new ArgumentException(Exceptions.ExistingEmail); }
            await this.dbContext.SaveChangesAsync();
            return new UserDTO(user);
        }
        /// <summary>
        /// Delete a user.
        /// </summary>
        /// <param name="id">Id of user to search for.</param>
        /// <returns>Returns true if delete succesfully or an appropriate error message.</returns>
        public async Task<bool> DeleteAsync(string username)
        {
            var user = await GetUserByUsernameAsync(username);
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
                                       .Where(u => u.IsDeleted == false /*&& u.Rank.Name != "Organizer" && u.Rank.Name != "Admin"*/)
                                       .Select(u => new UserDTO(u))
                                       .ToListAsync();
        }
        /// <summary>
        /// Get all participants which are not organizers.
        /// </summary>
        /// <returns>Returns all participants.</returns>
        public async Task<IEnumerable<UserDTO>> GetAllParticipantsAsync()
        {
            var role = await this.dbContext.Roles.FirstOrDefaultAsync(r => r.Name.ToLower() == "user");
            var userRoles = await this.dbContext.UserRoles.Where(ur => ur.RoleId == role.Id).ToListAsync();
            var users = new List<User>();
            foreach (var userRole in userRoles)
            {
                var user = await this.dbContext.Users.Include(u=>u.Rank).FirstOrDefaultAsync(u => u.Id == userRole.UserId);
                users.Add(user);
            }
            if (users.Count == 0)
            {
                throw new ArgumentException(Exceptions.NoParticipants);
            }
            return users.Select(u => new UserDTO(u)).OrderByDescending(u => u.Points); // TODO ASYNC
        }

        /// <summary>
        /// Update a user.
        /// </summary>
        /// <param name="updateUserDTO">Details of user to be updated.</param>
        /// <param name="id">Id to search for.</param>
        /// <returns>Returns updated user or an appropriate error message.</returns>
        public async Task<UserDTO> UpdateAsync(UpdateUserDTO updateUserDTO, string username)
        {
            var user = await GetUserByUsernameAsync(username);
            user.FirstName = updateUserDTO.FirstName ?? user.FirstName;
            user.LastName = updateUserDTO.LastName ?? user.LastName;
            /*if (!string.IsNullOrEmpty(updateUserDTO.RankId))
            {
                
                user.RankId = Guid.Parse(updateUserDTO.RankId);
            }*/
            user.ModifiedOn = DateTime.UtcNow;
            await this.dbContext.SaveChangesAsync();
            return new UserDTO(user);
        }
        /// <summary>
        /// Get user by username.
        /// </summary>
        /// <param name="username">Username to search for.</param>
        /// <returns>Returns user with that username or an appropriate error message.</returns>
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await this.dbContext
                             .Users
                             .Include(u => u.Rank)
                             .Where(u => u.IsDeleted == false)
                             .FirstOrDefaultAsync(c => c.UserName.ToLower() == username.ToLower())
                             ?? throw new ArgumentException(Exceptions.InvalidUser);
        }
        /// <summary>
        /// Add role to user.
        /// </summary>
        /// <param name="model">Email to search for and user role to be added.</param>
        /// <returns>Returns string </returns>
        public async Task<string> AddRoleAsync(AddRoleModel model)
        {
            var user = await this.userManager.FindByEmailAsync(model.Email)
                             ?? throw new ArgumentException(Exceptions.NotFoundEmail);
            if (!await this.dbContext.Roles.AnyAsync(r => r.Name.ToLower() == model.Role.ToLower()))
            {
                throw new ArgumentException(Exceptions.NotFoundRole);
            }
            await this.userManager.AddToRoleAsync(user, model.Role);
            return Exceptions.SuccesfullyAddedRole;   //THIS ONLY ADDS ANOTHER ROLE, DO NOT CHANGE IT
        }

        /// <summary>
        /// Get user by id.
        /// </summary>
        /// <param name="id">Id to search for.</param>
        /// <returns>Returns user with that id or an appropriate error message.</returns>
        public async Task<User> FindUser(Guid id)
        {
            return await this.dbContext.Users
                                       .Include(u => u.Rank)
                                       .Where(u => u.IsDeleted == false)
                                       .FirstOrDefaultAsync(u => u.Id == id)
                                       ?? throw new ArgumentException(Exceptions.InvalidUserID);
        }
    }
}
