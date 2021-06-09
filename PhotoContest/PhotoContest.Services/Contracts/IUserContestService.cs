using PhotoContest.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoContest.Services.Contracts
{
    public interface IUserContestService
    {
        Task CalculatePointsAsync();
        Task<List<UserContest>> GetAllUserContestsAsync();
    }
}
