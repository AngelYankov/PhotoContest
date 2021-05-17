using PhotoContest.Services.Models;
using PhotoContest.Services.Models.Create;
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
        Task<PhotoDTO> UpdateAsync(PhotoDTO photoDTO);
        Task<bool> DeleteAsync(Guid id);
    }
}
