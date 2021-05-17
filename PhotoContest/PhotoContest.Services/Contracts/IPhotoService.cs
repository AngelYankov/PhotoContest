using PhotoContest.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Services.Contracts
{
    public interface IPhotoService
    {
        Task<PhotoDTO> CreateAsync(PhotoDTO photoDTO);
        Task<PhotoDTO> GetAsync(Guid id);
        Task<IEnumerable<PhotoDTO>> GetAllAsync();
        Task<PhotoDTO> UpdateAsync(PhotoDTO photoDTO);
        Task<bool> DeleteAsync(Guid id);
    }
}
