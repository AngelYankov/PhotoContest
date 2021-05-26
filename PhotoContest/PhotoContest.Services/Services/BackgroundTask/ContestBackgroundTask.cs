using Microsoft.EntityFrameworkCore;
using PhotoContest.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoContest.Services.Services.BackgroundTask
{
    public class ContestBackgroundTask : IContestBackgroundTask
    {
        private readonly PhotoContestContext dbContext;

        public ContestBackgroundTask(PhotoContestContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Changing the status of a contest in the background.
        /// </summary>
        /// <returns></returns>
        public async Task ChangeStatus(CancellationToken cancellationToken)
        {
            var contests = await this.dbContext.Contests.ToListAsync();

            while (!cancellationToken.IsCancellationRequested)
            {
                foreach (var contest in contests)
                {
                    if (DateTime.UtcNow < contest.Phase2)
                    {
                        contest.StatusId = Guid.Parse("9dd48e5a-f5f5-4b90-ad93-e0a5ad62e186");
                    }
                    if (DateTime.UtcNow >= contest.Phase2 && DateTime.UtcNow < contest.Finished)
                    {
                        contest.StatusId = Guid.Parse("27c7d81e-eb1c-469b-8919-a532322273cc");
                    }
                    if (DateTime.UtcNow >= contest.Finished)
                    {
                        contest.StatusId = Guid.Parse("cf6bf4fb-655e-47cc-8dac-4a39cbff74b6");
                    }
                    await this.dbContext.SaveChangesAsync();
                }
                await Task.Delay(1000 * 5);
            }
        }
    }
}
