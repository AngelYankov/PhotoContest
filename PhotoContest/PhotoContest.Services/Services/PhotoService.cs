using Microsoft.EntityFrameworkCore;
using PhotoContest.Data;
using PhotoContest.Data.Models;
using PhotoContest.Services.Contracts;
using PhotoContest.Services.Models;
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

        public PhotoService(PhotoContestContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Task<PhotoDTO> CreateAsync(PhotoDTO photoDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var photo = FindPhoto(id);
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
            var photo = FindPhoto(id);
            //return await new PhotoDTO(photo);
            throw new ArgumentException();
        }

        public Task<PhotoDTO> UpdateAsync(PhotoDTO photoDTO)
        {
            throw new NotImplementedException();
        }

        private Photo FindPhoto(Guid id)
        {
            return this.dbContext.Photos
                                 .Include(p => p.User)
                                 .Include(p => p.Contest)
                                 .Where(p => p.IsDeleted == false)
                                 .FirstOrDefault(p => p.Id == id)
                                 ?? throw new ArgumentException();
        }
    }
}
