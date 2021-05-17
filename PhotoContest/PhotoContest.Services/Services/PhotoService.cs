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
        public async Task<bool> DeleteAsync(Guid id)
        {
            var photo = await FindPhoto(id);
            photo.IsDeleted = true;
            photo.DeletedOn = DateTime.UtcNow;
            await this.dbContext.SaveChangesAsync();
            return photo.IsDeleted;
        }

        public async Task<IEnumerable<PhotoDTO>> GetAllAsync()
        {
            return await this.dbContext.Photos
                                       .Include(p => p.User)
                                       .Include(p => p.Contest)
                                       .Where(p => p.IsDeleted == false)
                                       .Select(p => new PhotoDTO(p))
                                       .ToListAsync();
        }

        public async Task<PhotoDTO> GetAsync(Guid id)
        {
            var photo = await FindPhoto(id);
            return new PhotoDTO(photo);
        }

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
