using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PhotoContest.Data;
using PhotoContest.Data.Models;
using PhotoContest.Services.Contracts;
using PhotoContest.Services.Models;
using PhotoContest.Services.Models.Create;
using PhotoContest.Services.Models.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Services.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly PhotoContestContext dbContext;
        private readonly IMapper mapper;

        public PhotoService(PhotoContestContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        /// <summary>
        /// Create a photo.
        /// </summary>
        /// <param name="newphotoDTO">Details of photo to be created.</param>
        /// <returns>Returns created photo or an appropriate error message.</returns>
        public async Task<PhotoDTO> CreateAsync(NewPhotoDTO newphotoDTO)
        {
            /*var photo = new Photo();
            photo.Title = newphotoDTO.Title;
            photo.Description = newphotoDTO.Description;
            photo.PhotoUrl = newphotoDTO.PhotoUrl;
            photo.ContestId = newphotoDTO.ContestId;*/

            if (newphotoDTO.Title == null) throw new ArgumentException(); 
            if (newphotoDTO.Description == null) throw new ArgumentException(); 
            if (newphotoDTO.PhotoUrl == null) throw new ArgumentException(); 
            if (newphotoDTO.UserId == Guid.Empty) throw new ArgumentException(); 
            if (newphotoDTO.ContestId == Guid.Empty) throw new ArgumentException(); 

            var photoMapped = mapper.Map<Photo>(newphotoDTO);
            photoMapped.CreatedOn = DateTime.UtcNow;
            await this.dbContext.Photos.AddAsync(photoMapped);
            await this.dbContext.SaveChangesAsync();
            return new PhotoDTO(photoMapped);
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
                                       .Include(p => p.User)
                                       .Include(p => p.Contest)
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
        /// Find a photo.
        /// </summary>
        /// <param name="id">Id to search for.</param>
        /// <returns>Returns photo with that id or an appropriate error message.</returns>
        private async Task<Photo> FindPhoto(Guid id)
        {
            return await this.dbContext.Photos
                                 .Include(p => p.User)
                                 .Include(p => p.Contest)
                                 .ThenInclude(c=>c.Category)
                                 .Where(p => p.IsDeleted == false)
                                 .FirstOrDefaultAsync(p => p.Id == id)
                                 ?? throw new ArgumentException();
        }
    }
}
