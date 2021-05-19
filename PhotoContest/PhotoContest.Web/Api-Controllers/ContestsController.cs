using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        /// <returns>Returns all parcels</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contest>>> GetContests()
        {
            var contests = await this.contestService.GetAllAsync();
            return Ok(contests);
        }

        /// <summary>
        /// Update a contest.
        /// </summary>
        /// <param name="id">ID for contest to update.</param>
        /// <param name="model">Details of the contest to be updated.</param>
        /// <returns>Returns the updated contest or an appropriate error message.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContest(Guid id, [FromBody] UpdateContestDTO model)
        {
            try
            {
                var contest = await this.contestService.UpdateAsync(id, model);
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
        [HttpPost]
        public async Task<ActionResult<Contest>> PostContest([FromBody] NewContestDTO dto)
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
        [HttpDelete("{id}")]
        public async Task<ActionResult<Contest>> DeleteContest(Guid id)
        {
            try
            {
                var parcel = await this.contestService.DeleteAsync(id);
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
        /// <returns>Returns filtered and/or sorted parcels or an appropriate error message.</returns>
        [HttpGet("filter by phase")]
        public async Task<ActionResult<IEnumerable<ContestDTO>>> GetByPhase([FromQuery] string phaseName, string sortBy, string order)
        {
            try
            {
                var parcelsDTO = await this.contestService.GetByPhaseAsync(phaseName, sortBy, order);
                return Ok(parcelsDTO);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Filter and/or sort contests by user.
        /// </summary>
        /// <param name="username">Username for which we are filtering the contests.</param>
        /// <param name="filter">open/closed</param>
        /// <returns></returns>
        [HttpGet("filter by user")]
        public async Task<ActionResult<IEnumerable<ContestDTO>>> GetByUser([FromQuery] string username, string filter)
        {
            try
            {
                var parcelsDTO = await this.contestService.GetByUserAsync(username, filter);
                return Ok(parcelsDTO);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
