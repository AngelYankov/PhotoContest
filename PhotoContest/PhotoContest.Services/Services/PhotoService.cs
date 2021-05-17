using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PhotoContest.Data;
using PhotoContest.Data.Models;
using PhotoContest.Services.Contracts;
using PhotoContest.Services.Models;
using PhotoContest.Services.Models.Create;
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
            var photoMapped = mapper.Map<Photo>(newphotoDTO);
            await this.dbContext.Photos.AddAsync(photoMapped);
            photoMapped.CreatedOn = DateTime.UtcNow;
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

        public Task<PhotoDTO> UpdateAsync(PhotoDTO photoDTO)
        {
            throw new NotImplementedException();
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
