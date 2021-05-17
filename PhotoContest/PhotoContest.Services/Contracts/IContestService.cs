using PhotoContest.Data.Models;
using PhotoContest.Services.Models;
using PhotoContest.Services.Models.Create;
using PhotoContest.Services.Models.Update;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoContest.Services.Services
{
    public interface IContestService
    {
        Task<ContestDTO> CreateAsync(NewContestDTO dto);
        Task<IEnumerable<ContestDTO>> GetAllAsync();
        Task<ContestDTO> UpdateAsync(Guid id, UpdateContestDTO contest);
        Task<bool> DeleteAsync(Guid id);
    }
}