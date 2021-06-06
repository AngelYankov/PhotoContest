using PhotoContest.Data.Models;
using PhotoContest.Services.Models;
using PhotoContest.Services.Models.Create;
using PhotoContest.Services.Models.Update;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoContest.Services.Services
{
    public interface IContestService
    {
        Task<ContestDTO> CreateAsync(NewContestDTO dto);
        Task<IEnumerable<ContestDTO>> GetAllAsync();
        Task<IEnumerable<ContestDTO>> GetAllOpenAsync(string phase);
        Task<ContestDTO> UpdateAsync(string contestName, UpdateContestDTO contest);
        Task<bool> DeleteAsync(string contestName);
        Task<IEnumerable<ContestDTO>> GetByPhaseAsync(string phaseName, string sortBy, string order);
        Task<IEnumerable<ContestDTO>> GetByUserAsync(string filter);
        Task<bool> EnrollAsync(string contestName);
        Task<bool> InviteAsync(string contestName, string username);
        Task<bool> ChooseJuryAsync(string contestName, string username);
        Task<Contest> FindContestByNameAsync(string contestName);
        Task<List<Contest>> GetAllFinishedContestsAsync();
        Task ChangeStatus();
        Task<IEnumerable<ContestDTO>> AllOpenViewAsync();
        Task<IEnumerable<ContestDTO>> GetUserContestsAsync();
        Task<List<JuryMember>> GetAllJuriesAsync();
    }
}