using System.Threading;
using System.Threading.Tasks;

namespace PhotoContest.Services.Services.BackgroundTask
{
    public interface IContestBackgroundTask
    {
        Task ChangeStatus();
    }
}