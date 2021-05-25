using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PhotoContest.Data;
using PhotoContest.Data.Models;
using PhotoContest.Services.Contracts;
using PhotoContest.Services.ExceptionMessages;
using PhotoContest.Services.Models;
using PhotoContest.Services.Models.Create;
using PhotoContest.Services.Models.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Services.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly PhotoContestContext dbContext;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IContestService contestService;
        private readonly IUserService userService;

        public PhotoService(PhotoContestContext dbContext, IHttpContextAccessor contextAccessor, IContestService contestService, IUserService userService)
        {
            this.dbContext = dbContext;
            this.contextAccessor = contextAccessor;
            this.contestService = contestService;
            this.userService = userService;
        }
        /// <summary>
        /// Create a photo.
        /// </summary>
        /// <param name="newphotoDTO">Details of photo to be created.</param>
        /// <returns>Returns created photo or an appropriate error message.</returns>
        public async Task<PhotoDTO> CreateAsync(NewPhotoDTO newphotoDTO)
        {
            if (newphotoDTO.Title == null) throw new ArgumentException(Exceptions.RequiredPhotoName);
            if (newphotoDTO.Description == null) throw new ArgumentException(Exceptions.RequiredPhotoDescription);
            if (newphotoDTO.PhotoUrl == null) throw new ArgumentException(Exceptions.RequiredPhotoURL);
            if (newphotoDTO.ContestName == null) throw new ArgumentException(Exceptions.InvalidContestName);
            var contest = await this.contestService.FindContestByNameAsync(newphotoDTO.ContestName);
            if (!contest.IsOpen)
            {
                throw new ArgumentException(Exceptions.ClosedContest);
            }
            var userName = this.contextAccessor.HttpContext.User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;
            var user = await this.userService.GetUserByUsernameAsync(userName);
            if (await this.dbContext.Juries.FirstOrDefaultAsync(j => j.UserId == user.Id && j.ContestId == contest.Id) != null)
            {
                throw new ArgumentException(Exceptions.ExistingJury);
            }
            var photo = new Photo()
            {
                Title = newphotoDTO.Title,
                Description = newphotoDTO.Description,
                PhotoUrl = newphotoDTO.PhotoUrl,
                ContestId = contest.Id,
                UserId = user.Id,
                CreatedOn = DateTime.UtcNow,
            };
            await this.dbContext.Photos.AddAsync(photo);
            await this.dbContext.SaveChangesAsync();
            return new PhotoDTO(photo);
        }
        /// <summary>
        /// Delete a photo.
        /// </summary>
        /// <param name="id">Id to search for.</param>
        /// <returns>Returns true if created succesfully or an appropriate error message.</returns>
        public async Task<bool> DeleteAsync(Guid id)
        {
            var photo = await FindPhoto(id);
            photo.IsDeleted = true;
            photo.DeletedOn = DateTime.UtcNow;
            await this.dbContext.SaveChangesAsync();
            return photo.IsDeleted;
        }
        /// <summary>
        /// Get all photos.
        /// </summary>
        /// <returns>Returns all photos.</returns>
        public async Task<IEnumerable<PhotoDTO>> GetAllAsync()
        {
            return await this.dbContext.Photos
                                       .Include(p=>p.User)
                                       .Include(p => p.Contest)
                                            .ThenInclude(c=>c.Category)
                                       .Where(p => p.IsDeleted == false)
                                       .Select(p => new PhotoDTO(p))
                                       .ToListAsync();
        }
        /// <summary>
        /// Get a photo by Id.
        /// </summary>
        /// <param name="id">Id to search for.</param>
        /// <returns>Returns photo with that id or an appropriate error message.</returns>
        public async Task<PhotoDTO> GetAsync(Guid id)
        {
            var photo = await FindPhoto(id);
            return new PhotoDTO(photo);
        }
        /// <summary>
        /// Update a photo.
        /// </summary>
        /// <param name="updatePhotoDTO">Details to be updated.</param>
        /// <param name="id">Id of the photo to be updated.</param>
        /// <returns>Returns updated photo or an appropriate error message.</returns>
        public async Task<PhotoDTO> UpdateAsync(UpdatePhotoDTO updatePhotoDTO, Guid id)
        {
            var photo = await FindPhoto(id);
            photo.Title = updatePhotoDTO.Title ?? photo.Title;
            photo.Description = updatePhotoDTO.Description ?? photo.Description;
            photo.PhotoUrl = updatePhotoDTO.PhotoUrl ?? photo.PhotoUrl;
            photo.ModifiedOn = DateTime.UtcNow;
            await this.dbContext.SaveChangesAsync();
            return new PhotoDTO(photo);
        }
        /// <summary>
        /// Get all photos for certain contest.
        /// </summary>
        /// <param name="contestName">Contest name to search for.</param>
        /// <returns>Return all photos.</returns>
        public async Task<List<PhotoDTO>> GetPhotosForContestAsync(string contestName)
        {
            await this.contestService.FindContestByNameAsync(contestName);
            return await this.dbContext.Photos
                                       .Include(p=>p.User)
                                       .Include(p => p.Contest)
                                          .ThenInclude(c => c.Category)
                                       .Where(p => p.IsDeleted == false && p.Contest.Name.ToLower() == contestName.ToLower())
                                       .Select(p => new PhotoDTO(p))
                                       .ToListAsync();
        }
        /// <summary>
        /// Get all photos with detailed info.
        /// </summary>
        /// <returns>Return all photos with score and comments.</returns>
        public async Task<List<PhotoReviewDTO>> GetAllWithCommentsAndScoreAsync()
        {
            return await this.dbContext.Photos
                                       .Include(p => p.User)
                                       .Include(p => p.Contest)
                                            .ThenInclude(c => c.Category)
                                       .Include(p => p.Reviews)
                                       .Select(p=>new PhotoReviewDTO(p)).ToListAsync();
        }

        /// <summary>
        /// Find a photo.
        /// </summary>
        /// <param name="id">Id to search for.</param>
        /// <returns>Returns photo with that id or an appropriate error message.</returns>
        public async Task<Photo> FindPhoto(Guid id)
        {
            return await this.dbContext.Photos
                                 .Include(p=>p.User)
                                 .Include(p => p.Contest)
                                        .ThenInclude(c => c.Category)
                                 .Where(p => p.IsDeleted == false)
                                 .FirstOrDefaultAsync(p => p.Id == id)
                                 ?? throw new ArgumentException(Exceptions.InvalidPhotoID);
        }
    }
}
