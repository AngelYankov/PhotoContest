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
        Task<IEnumerable<ContestDTO>> GetAllOpenAsync();
        Task<ContestDTO> UpdateAsync(Guid id, UpdateContestDTO contest);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<ContestDTO>> GetByPhaseAsync(string phaseName, string sortBy, string order);
        Task<IEnumerable<ContestDTO>> GetByUserAsync(string username, string filter);
        Task<bool> Enroll(string username, string contestName);
    }
}