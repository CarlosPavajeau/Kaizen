using System;
using System.Threading;
using System.Threading.Tasks;
using Kaizen.HostedServices.ProcessingServices;
using Microsoft.Extensions.DependencyInjection;

namespace Kaizen.HostedServices
{
    public class EmployeeContractHostedService : BackgroundService
    {
        public EmployeeContractHostedService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await DoWork(stoppingToken);
        }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            using IServiceScope scope = ServiceProvider.CreateScope();
            EmployeeContract employeeContractProcessingService =
                scope.ServiceProvider.GetRequiredService<EmployeeContract>();

            await employeeContractProcessingService.DoWork(stoppingToken);
        }
    }
}
