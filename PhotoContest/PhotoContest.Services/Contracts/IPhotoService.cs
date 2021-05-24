using PhotoContest.Services.Models;
using PhotoContest.Services.Models.Create;
using PhotoContest.Services.Models.Update;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Services.Contracts
{
    public interface IPhotoService
    {
        Task<PhotoDTO> CreateAsync(NewPhotoDTO photoDTO);
        Task<PhotoDTO> GetAsync(Guid id);
        Task<IEnumerable<PhotoDTO>> GetAllAsync();
        Task<PhotoDTO> UpdateAsync(UpdatePhotoDTO photoDTO, Guid id);
        Task<bool> DeleteAsync(Guid id);
        Task<PhotoDTO> RatePhoto(Guid id, int points);
        Task<List<PhotoDTO>> GetPhotosForContestAsync(Guid contestId);
    }
}
