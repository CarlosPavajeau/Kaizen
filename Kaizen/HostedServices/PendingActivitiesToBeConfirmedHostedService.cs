using System;
using System.Threading;
using System.Threading.Tasks;
using Kaizen.HostedServices.ProcessingServices;
using Microsoft.Extensions.DependencyInjection;

namespace Kaizen.HostedServices
{
    public class PendingActivitiesToBeConfirmedHostedService : BackgroundService
    {
        public PendingActivitiesToBeConfirmedHostedService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await DoWork(stoppingToken);
        }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            using IServiceScope scope = ServiceProvider.CreateScope();
            PendingActivitiesToConfirmed pendingActivitiesToConfirmed =
                scope.ServiceProvider.GetRequiredService<PendingActivitiesToConfirmed>();

            await pendingActivitiesToConfirmed.DoWork(stoppingToken);
        }
    }
}
