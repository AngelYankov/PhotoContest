using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoContest.Data;
using PhotoContest.Data.Models;
using PhotoContest.Services.Models;
using PhotoContest.Services.Models.Create;
using PhotoContest.Services.Models.Update;
using PhotoContest.Services.Services;

namespace PhotoContest.Web.Api_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ContestsController : ControllerBase
    {
        private readonly IContestService contestService;

        public ContestsController(IContestService contestService)
        {
            this.contestService = contestService;
        }

        /// <summary>
        /// Get all contests.
        /// </summary>
        /// <returns>Returns all contests.</returns>
        [Authorize(Roles = "Organizer")]
        [HttpGet]
        public async Task<IActionResult> GetContests()
        {
            var contests = await this.contestService.GetAllAsync();
            return Ok(contests);
        }

        /// <summary>
        /// Get all open contests.
        /// </summary>
        /// <returns>Returns all open contests.</returns>
        [Authorize]
        [HttpGet("оpen")]
        public async Task<IActionResult> GetOpenContests()
        {
            var contests = await this.contestService.GetAllOpenAsync();
            return Ok(contests);
        }

        /// <summary>
        /// Enroll user into a contest.
        /// </summary>
        /// <param name="contestName">Name of the contest to enroll in.</param>
        /// <returns>Returns true if successful or an appropriate error message.</returns>
        [Authorize(Roles = "User")]
        [HttpPost("enroll")]
        public async Task<IActionResult> Enroll(string contestName)
        {
            try
            {
                var isEnrolled = await this.contestService.Enroll(contestName);
                return Ok(isEnrolled);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Update a contest.
        /// </summary>
        /// <param name="contestName">Name for contest to update.</param>
        /// <param name="model">Details of the contest to be updated.</param>
        /// <returns>Returns the updated contest or an appropriate error message.</returns>
        [Authorize(Roles = "Organizer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContest(string contestName, [FromBody] UpdateContestDTO model)
        {
            try
            {
                var contest = await this.contestService.UpdateAsync(contestName, model);
                return Ok(contest);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Create a contest.
        /// </summary>
        /// <param name="dto">Details of the contest to be created.</param>
        /// <returns>Returns the created contest or an appropriate error message.</returns>
        [Authorize(Roles = "Organizer")]
        [HttpPost]
        public async Task<IActionResult> CreateContest([FromQuery] NewContestDTO dto)
        {
            try
            {
                var contest = await this.contestService.CreateAsync(dto);
                return Created("post", contest);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Delete a contest.
        /// </summary>
        /// <param name="id">ID of the contest to delete.</param>
        /// <returns>Returns NoContent or an appropriate error message.</returns>
        [Authorize(Roles = "Organizer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContest(Guid id)
        {
            try
            {
                var contest = await this.contestService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Filter and/or sort contests by phase.
        /// </summary>
        /// <param name="phaseName">phase1/phase2/finished</param>
        /// <param name="sortBy">name/category/newest/oldest</param>
        /// <param name="order">asc/desc</param>
        /// <returns>Returns filtered and/or sorted contests or an appropriate error message.</returns>
        [Authorize(Roles = "Organizer")]
        [HttpGet("filterPhase")]
        public async Task<IActionResult> GetByPhase([FromQuery] string phaseName, string sortBy, string order)
        {
            try
            {
                var contestsDTO = await this.contestService.GetByPhaseAsync(phaseName, sortBy, order);
                return Ok(contestsDTO);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Filter and/or sort contests by user.
        /// </summary>
        /// <param name="filter">open/closed</param>
        /// <returns></returns>
        [Authorize(Roles = "User")]
        [HttpGet("filterUser")]
        public async Task<IActionResult> GetByUser(string filter)
        {
            try
            {
                var contestsDTO = await this.contestService.GetByUserAsync(filter);
                return Ok(contestsDTO);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
