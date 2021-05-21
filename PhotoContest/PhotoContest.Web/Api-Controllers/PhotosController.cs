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

namespace PhotoContest.Web.Api_Controllers
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
        // GET: api/Photos
        [Authorize(Roles = "Organizer")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Photo>>> GetAllAsync()
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
        // GET: api/Photos/5
        [Authorize(Roles = "Organizer")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Photo>> GetAsync(Guid id)
        {
            try
            {
                var photo = await this.photoService.GetAsync(id);
                return Ok(photo);
            }
            catch (Exception E)
            {
                return BadRequest(E.Message);
            }
        }
        /// <summary>
        /// Update photo details.
        /// </summary>
        /// <param name="id">Id search for.</param>
        /// <param name="updateModel">Details of photo to be updated.</param>
        /// <returns>Returns updated photo or an appropriate error message.</returns>
        // PUT: api/Photos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize(Roles = "Organizer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdatePhotoDTO updateModel)
        {
            try
            {
                var photo = await this.photoService.UpdateAsync(updateModel, id);
                return Ok(photo);
            }
            catch (Exception E)
            {
                return BadRequest(E.Message);
            }
        }
        /// <summary>
        /// Create a photo.
        /// </summary>
        /// <param name="newPhotoDTO">New photo to be created.</param>
        /// <returns>Returns created photo or an appropriate error message.</returns>
        // POST: api/Photos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<ActionResult<Photo>> CreateAsync(NewPhotoDTO newPhotoDTO)
        {
            try
            {
                var photo = await this.photoService.CreateAsync(newPhotoDTO);
                return Created("post",photo);
            }
            catch (Exception E)
            {
                return BadRequest(E.Message);
            }
        }
        /// <summary>
        /// Delete a photo.
        /// </summary>
        /// <param name="id">Id to search for.</param>
        /// <returns>Returns true if deleted succesfully or an appropriate error message.</returns>
        // DELETE: api/Photos/5
        [Authorize(Roles = "Organizer")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Photo>> DeleteAsync(Guid id)
        {
            try
            {
                await this.photoService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception E)
            {
                return BadRequest(E.Message);
            }
        }
    }
}
