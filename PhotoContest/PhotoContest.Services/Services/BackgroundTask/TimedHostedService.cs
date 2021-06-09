using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhotoContest.Services.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoContest.Services.Services.BackgroundTask
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private readonly IContestService contestService;
        private readonly IUserService userService;
        private readonly IUserContestService userContestService;
        private Timer timer;

        public TimedHostedService(IServiceProvider serviceProvider)
        {
            this.contestService = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IContestService>();
            this.userService = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IUserService>();
            this.userContestService = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IUserContestService>();
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
            return Task.CompletedTask;
        }

        public void DoWork(object state)
        {
            this.contestService.ChangeStatus();
            this.userContestService.CalculatePointsAsync();
            this.userService.ChangeRank();
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
