using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoContest.Data;
using PhotoContest.Data.Models;
using PhotoContest.Services.Contracts;
using PhotoContest.Services.Models.Create;
using PhotoContest.Services.Models.Update;

namespace PhotoContest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IPhotoService photoService;

        public PhotosController(IPhotoService photoService)
        {
            this.photoService = photoService;
        }
        /// <summary>
        /// Get all photos.
        /// </summary>
        /// <returns>Returns all photos.</returns>
        [Authorize(Roles = "Admin,Organizer")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var allPhotos = await this.photoService.GetAllAsync();
            if (allPhotos.Count() == 0)
            {
                return NoContent();
            }
            return Ok(allPhotos);
        }
        /// <summary>
        /// Get photo by id.
        /// </summary>
        /// <param name="id">Id to search for.</param>
        /// <returns>Returns photo with that id or an appropriate error message.</returns>
        [Authorize(Roles = "Admin,Organizer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            try
            {
                var photo = await this.photoService.GetAsync(id);
                return Ok(photo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Update photo details.
        /// </summary>
        /// <param name="id">Id search for.</param>
        /// <param name="updateModel">Details of photo to be updated.</param>
        /// <returns>Returns updated photo or an appropriate error message.</returns>
        [Authorize(Roles = "Admin,Organizer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody]UpdatePhotoDTO updateModel, Guid id)
        {
            try
            {
                var photo = await this.photoService.UpdateAsync(updateModel, id);
                return Ok(photo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Create a photo.
        /// </summary>
        /// <param name="newPhotoDTO">New photo to be created.</param>
        /// <returns>Returns created photo or an appropriate error message.</returns>
        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] NewPhotoDTO newPhotoDTO)
        {
            try
            {
                var photo = await this.photoService.CreateApiAsync(newPhotoDTO);
                return Created("post",photo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Delete a photo.
        /// </summary>
        /// <param name="id">Id to search for.</param>
        /// <returns>Returns true if deleted succesfully or an appropriate error message.</returns>
        // DELETE: api/Photos/5
        [Authorize(Roles = "Admin,Organizer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await this.photoService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Get all photos for contest.
        /// </summary>
        /// <param name="contestName">Contest name.</param>
        /// <returns>Returns all photos for that contest.</returns>
        [Authorize(Roles = "Admin,Organizer")]
        [HttpGet("contest")]
        public async Task<IActionResult> GetPhotosForContestAsync(string contestName)
        {
            try
            {
                var result = await this.photoService.GetPhotosForContestAsync(contestName);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Get all photos with detailed info.
        /// </summary>
        /// <param name="contestName">Contest name.</param>
        /// <returns>Return all photos with score and comments.</returns>
        [Authorize]
        [HttpGet("allInfo")]
        public async Task<IActionResult> GetAllWithCommentsAndScoreAsync(string contestName)
        {
            try
            {
                var result = await this.photoService.GetAllWithCommentsAndScoreApiAsync(contestName);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
