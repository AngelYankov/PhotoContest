using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PhotoContest.Data;
using PhotoContest.Data.Models;
using PhotoContest.Services.Contracts;
using PhotoContest.Services.Models;
using PhotoContest.Services.Models.Create;
using PhotoContest.Services.Models.Update;
using PhotoContest.Services.Services.SecuritySettings;
using PhotoContest.Web.SecuritySettings;
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
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly JWT jwt;

        public UserService(PhotoContestContext dbContext, IMapper mapper, UserManager<User> userManager, RoleManager<Role> roleManager, IOptions<JWT> jwt)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.jwt = jwt.Value;
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
            var user = mapper.Map<User>(newUserDTO);
            var rank = await this.dbContext.Ranks.FirstAsync();
            user.RankId = rank.Id;
            user.CreatedOn = DateTime.UtcNow;
            if (await this.userManager.FindByEmailAsync(newUserDTO.Email) == null)
            {
                var result = await this.userManager.CreateAsync(user, newUserDTO.Password);
                if (result.Succeeded)
                {
                    await this.userManager.AddToRoleAsync(user, "User");
                }
                else { throw new ArgumentException("Incorrect password."); }
            }
            else { throw new ArgumentException("Email already exists."); }
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
        /// Get token and user details.
        /// </summary>
        /// <param name="model">Validation model.</param>
        /// <returns>Returns details of user token.</returns>
        public async Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model)
        {
            var authenticationModel = new AuthenticationModel();
            var user = await this.userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                authenticationModel.IsAuthenticated = false;
                throw new ArgumentException("No account registered with that email.");
                //authenticationModel.Message = $"No Accounts Registered with {model.Email}.";
                //return authenticationModel;
            }
            if (await this.userManager.CheckPasswordAsync(user, model.Password))
            {
                authenticationModel.IsAuthenticated = true;
                JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
                authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                authenticationModel.Email = user.Email;
                authenticationModel.UserName = user.UserName;
                var rolesList = await this.userManager.GetRolesAsync(user).ConfigureAwait(false);
                authenticationModel.Roles = rolesList.ToList();
                return authenticationModel;
            }
            authenticationModel.IsAuthenticated = false;
            throw new ArgumentException("Incorrect credentials for user.");
            //authenticationModel.Message = $"Incorrect Credentials for user {user.Email}.";
            //return authenticationModel;
        }
        /// <summary>
        /// Create JWT.
        /// </summary>
        /// <param name="user">User to create the token for.</param>
        /// <returns>Returns token.</returns>
        private async Task<JwtSecurityToken> CreateJwtToken(User user)
        {
            var userClaims = await this.userManager.GetClaimsAsync(user);
            var roles = await this.userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("UserId", user.Id.ToString()),
            }
            .Union(userClaims)
            .Union(roleClaims);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: this.jwt.Issuer,
                audience: this.jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(this.jwt.DurationInMinutes)),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
        /// <summary>
        /// Add role to user.
        /// </summary>
        /// <param name="model">Email to search for and user role to be added.</param>
        /// <returns>Returns string </returns>
        public async Task<string> AddRoleAsync(AddRoleModel model)
        {
            var user = await this.userManager.FindByEmailAsync(model.Email)
            ?? throw new ArgumentException("Email not found.");
            var roleExists = this.dbContext.Roles.Any(r => r.Name.Equals(model.Role, StringComparison.OrdinalIgnoreCase));
            if (roleExists)
            {
                await this.userManager.AddToRoleAsync(user, model.Role);
                return $"Added {model.Role} to user {model.Email}.";
            }
            throw new ArgumentException("Role not found.");
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
