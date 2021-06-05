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
        private readonly SignInManager<User> signInManager;

        public UserService(PhotoContestContext dbContext, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.signInManager = signInManager;
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
                Rank = await this.dbContext.Ranks.FirstOrDefaultAsync(r => r.Name.ToLower() == "junkie"),
                CreatedOn = DateTime.UtcNow
            };
            var oldUser = await this.userManager.FindByEmailAsync(newUserDTO.Email);
            if (oldUser != null && oldUser.IsDeleted == true)
            {
                oldUser.IsDeleted = false;
                throw new ArgumentException(Exceptions.RestoreUserAccount);
            }
            if (oldUser == null)
            {
                var result = await this.userManager.CreateAsync(user, newUserDTO.Password);
                if (result.Succeeded)
                {
                    await this.userManager.AddToRoleAsync(user, "User");
                }
                else { throw new ArgumentException(Exceptions.IncorrectPassword); }
            }
            else { throw new ArgumentException(Exceptions.ExistingEmail); }
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
                Rank = await this.dbContext.Ranks.FirstOrDefaultAsync(r => r.Name.ToLower() == "organizer"),
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
        /// Get all users.
        /// </summary>
        /// <returns>Returns all users.</returns>
        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            return await this.dbContext.Users
                                       .Include(u => u.Rank)
                                       //.Include(u => u.Reviews)
                                       .Where(u => u.IsDeleted == false)
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
                var user = await this.dbContext.Users.Include(u => u.Rank).FirstOrDefaultAsync(u => u.Id == userRole.UserId);
                users.Add(user);
            }
            return users.Select(u => new UserDTO(u)).OrderByDescending(u => u.Points);
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
        /// Show info of the logged user
        /// </summary>
        /// <returns>Returns all user's info</returns>
        public async Task<UserDTO> ShowMyAccountInfo()
        {
            var username = this.userManager.GetUserName(this.signInManager.Context.User);
            var user = await GetUserByUsernameAsync(username);
            return new UserDTO(user);
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
            //DO WE NEED TO CHANGE THE RANK?
            return Exceptions.SuccesfullyAddedRole;   //THIS ONLY ADDS ANOTHER ROLE, DO NOT CHANGE IT
        }

        /// <summary>
        /// Changing the rank of a user in the background.
        /// </summary>
        /// <returns></returns>
        public async Task ChangeRank()
        {
            var contestants = await this.dbContext.Users.Where(u => u.Rank.Name != "Admin" && u.Rank.Name != "Organizer").ToListAsync();
            foreach (var contestant in contestants)
            {
                if (contestant.OverallPoints > 50 && contestant.OverallPoints <= 150)
                {
                    var rankEnthusiast = await this.dbContext.Ranks.FirstOrDefaultAsync(r => r.Id == Guid.Parse("41c8e397-f768-48ed-b8f1-f8a238c739b1"));
                    contestant.RankId = Guid.Parse("41c8e397-f768-48ed-b8f1-f8a238c739b1");
                    contestant.Rank = rankEnthusiast;
                    //CHANGE ONLY RANKID NOT RANK
                }
                if (contestant.OverallPoints > 150 && contestant.OverallPoints <= 1000)
                {
                    var rankMaster = await this.dbContext.Ranks.FirstOrDefaultAsync(r => r.Id == Guid.Parse("a9576301-3157-454f-86ce-85bb5eb2dfc9"));
                    contestant.RankId = Guid.Parse("a9576301-3157-454f-86ce-85bb5eb2dfc9");
                    contestant.Rank = rankMaster;
                }
                if (contestant.OverallPoints > 1000)
                {
                    var rankDictator = await this.dbContext.Ranks.FirstOrDefaultAsync(r => r.Id == Guid.Parse("0b1728c7-5582-4958-9e97-52c9b1d44cdb"));
                    contestant.RankId = Guid.Parse("0b1728c7-5582-4958-9e97-52c9b1d44cdb");
                    contestant.Rank = rankDictator;
                }
            }
            await this.dbContext.SaveChangesAsync();
        }

    }
}
