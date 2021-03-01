using System;
using System.Threading;
using System.Threading.Tasks;
using Kaizen.HostedServices.ProcessingServices;
using Microsoft.Extensions.DependencyInjection;

namespace Kaizen.HostedServices
{
    public class ActivitySchedulingDeadlineUpdateHostedService : BackgroundService
    {
        public ActivitySchedulingDeadlineUpdateHostedService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await DoWork(stoppingToken);
        }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            using IServiceScope scope = ServiceProvider.CreateScope();
            ActivitySchedulingDeadlineUpdate activitySchedulingDeadlineUpdate =
                scope.ServiceProvider.GetRequiredService<ActivitySchedulingDeadlineUpdate>();

            await activitySchedulingDeadlineUpdate.DoWork(stoppingToken);
        }
    }
}
