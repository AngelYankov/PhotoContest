using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoContest.Data;
using PhotoContest.Services.Contracts;
using PhotoContest.Services.Models.Create;
using PhotoContest.Services.Models.Update;
using PhotoContest.Services.Services.SecuritySettings;

namespace PhotoContest.Web.Api_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }
        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns>Returns all users.</returns>
        // GET: api/Users
        [Authorize(Roles ="Organizer")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllAsync()
        {
            var allUsers = await this.userService.GetAllAsync();
            if (allUsers.Count() == 0)
            {
                return NoContent();
            }
            return Ok(allUsers);
        }
        /// <summary>
        /// Get a user by id.
        /// </summary>
        /// <param name="id">Id to search for.</param>
        /// <returns>Returns a user with that id or an appropriate error message.</returns>
        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetAsync(Guid id)
        {
            try
            {
                var user = await this.userService.GetAsync(id);
                return Ok(user);
            }
            catch (Exception E)
            {
                return BadRequest(E.Message);
            }
        }
        /// <summary>
        /// Update a user.
        /// </summary>
        /// <param name="id">Id to search for.</param>
        /// <param name="updateUserDTO">Details of user to be updated.</param>
        /// <returns>Returns updated user or an appropriate error message.</returns>
        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateUserDTO updateUserDTO)
        {
            try
            {
                var user = await this.userService.UpdateAsync(updateUserDTO, id);
                return Ok(user);
            }
            catch (Exception E)
            {
                return BadRequest(E.Message);
            }
        }
        /// <summary>
        /// Create a user.
        /// </summary>
        /// <param name="newUserDTO">Details of user to be created.</param>
        /// <returns>Returns created user or an appropriate error message.</returns>
        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<User>> CreateAsync(NewUserDTO newUserDTO)
        {
            try
            {
                var user = await this.userService.CreateAsync(newUserDTO);
                return Created("post",user);
            }
            catch (Exception E)
            {
                return BadRequest(E.Message);
            }
        }
        /// <summary>
        /// Delete a user.
        /// </summary>
        /// <param name="id">Id to search for.</param>
        /// <returns>Returns true if deleted successfully or an appropriate error message.</returns>
        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteAsync(Guid id)
        {
            try
            {
                await this.userService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception E)
            {
                return BadRequest(E.Message);
            }
        }
        /// <summary>
        /// Create token.
        /// </summary>
        /// <param name="model">Validation details.</param>
        /// <returns>Returns created token.</returns>
        [HttpPost("token")]
        public async Task<IActionResult> GetTokenAsync(TokenRequestModel model)
        {
            var result = await this.userService.GetTokenAsync(model);
            return Ok(result);
        }
        /// <summary>
        /// Add role to user.
        /// </summary>
        /// <param name="model">Details of user and role to be added.</param>
        /// <returns>Returns appropriate message if created.</returns>
        [HttpPost("addrole")]
        public async Task<IActionResult> AddRoleAsync(AddRoleModel model)
        {
            var result = await this.userService.AddRoleAsync(model);
            return Ok(result);
        }
    }
}
