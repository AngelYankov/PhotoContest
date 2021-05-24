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
using PhotoContest.Services.Models.SecurityModels;
using PhotoContest.Services.Models.Update;

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
        [Authorize(Roles = "Organizer,Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var allUsers = await this.userService.GetAllAsync();
            if (allUsers.Count() == 0)
            {
                return NoContent();
            }
            return Ok(allUsers);
        }
        //TODO AUTHORIZE
        [HttpGet("participants")]
        public async Task<IActionResult> GetAllParticipantsAsync()
        {
            try
            {
                var result = await this.userService.GetAllParticipantsAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Get a user by id.
        /// </summary>
        /// <param name="id">Id to search for.</param>
        /// <returns>Returns a user with that id or an appropriate error message.</returns>
        // GET: api/Users/5
        [Authorize(Roles = "Organizer,Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            try
            {
                var user = await this.userService.GetAsync(id);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Update a user.
        /// </summary>
        /// <param name="username">Username to search for.</param>
        /// <param name="updateUserDTO">Details of user to be updated.</param>
        /// <returns>Returns updated user or an appropriate error message.</returns>
        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize(Roles = "Organizer,Admin")]
        [HttpPut("{username}")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateUserDTO updateUserDTO, string username)
        {
            try
            {
                var user = await this.userService.UpdateAsync(updateUserDTO, username);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Create a user.
        /// </summary>
        /// <param name="newUserDTO">Details of user to be created.</param>
        /// <returns>Returns created user or an appropriate error message.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] NewUserDTO newUserDTO)
        {
            try
            {
                var user = await this.userService.CreateAsync(newUserDTO);
                return Created("post", user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Create user which is organizer.
        /// </summary>
        /// <param name="newUserDTO">Details of new user.</param>
        /// <returns>Returns created user.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("organizer")]
        public async Task<IActionResult> CreateOrganizerAsync([FromBody] NewUserDTO newUserDTO)
        {
            try
            {
                var user = await this.userService.CreateOrganizerAsync(newUserDTO);
                return Created("post", user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Delete a user.
        /// </summary>
        /// <param name="username">Username to search for.</param>
        /// <returns>Returns true if deleted successfully or an appropriate error message.</returns>
        // DELETE: api/Users/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteAsync(string username)
        {
            try
            {
                await this.userService.DeleteAsync(username);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Add role to user.
        /// </summary>
        /// <param name="model">Details of user and role to be added.</param>
        /// <returns>Returns appropriate message if created.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("addrole")]
        public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleModel model)
        {
            try
            {
                var result = await this.userService.AddRoleAsync(model);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
