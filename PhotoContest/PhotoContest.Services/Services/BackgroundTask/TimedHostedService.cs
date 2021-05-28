using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoContest.Services.Services.BackgroundTask
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private readonly IContestBackgroundTask contestBackgroundTask;
        private Timer timer;

        public TimedHostedService(IServiceProvider serviceProvider)
        {
            this.contestBackgroundTask = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IContestBackgroundTask>();
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        public void DoWork(object state)
        {
            this.contestBackgroundTask.ChangeStatus();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}
