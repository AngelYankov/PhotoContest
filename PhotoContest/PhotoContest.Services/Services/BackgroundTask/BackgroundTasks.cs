using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoContest.Services.Services.BackgroundTask
{
    public class BackgroundTasks : BackgroundService
    {
        private readonly IContestBackgroundTask backgroundTask;

        public BackgroundTasks(IContestBackgroundTask backgroundTask)
        {
            this.backgroundTask = backgroundTask;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await this.backgroundTask.ChangeStatus(stoppingToken);
        }
    }
}
