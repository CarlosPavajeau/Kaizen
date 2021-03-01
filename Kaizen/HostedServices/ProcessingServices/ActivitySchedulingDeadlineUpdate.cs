using System;
using System.Threading;
using System.Threading.Tasks;
using Kaizen.Domain.Repositories;

namespace Kaizen.HostedServices.ProcessingServices
{
    public class ActivitySchedulingDeadlineUpdate : IScopedProcessingService
    {
        private static readonly int DelayTime = (int)TimeSpan.FromDays(1.0).TotalMilliseconds;

        private readonly IActivitiesRepository _activitiesRepository;

        public ActivitySchedulingDeadlineUpdate(IActivitiesRepository activitiesRepository)
        {
            _activitiesRepository = activitiesRepository;
        }

        public async Task DoWork(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _activitiesRepository.UpdateLimitDate();

                await Task.Delay(DelayTime, cancellationToken);
            }
        }
    }
}
