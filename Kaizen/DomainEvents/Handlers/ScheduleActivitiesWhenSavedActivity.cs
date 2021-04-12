using System.Threading;
using System.Threading.Tasks;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Events;
using Kaizen.Domain.Repositories;
using MediatR;

namespace Kaizen.DomainEvents.Handlers
{
    public class ScheduleActivitiesWhenSavedActivity : INotificationHandler<DomainEventNotification<SavedActivity>>
    {
        private readonly IActivitiesRepository _activitiesRepository;

        public ScheduleActivitiesWhenSavedActivity(IActivitiesRepository activitiesRepository)
        {
            _activitiesRepository = activitiesRepository;
        }

        public async Task Handle(DomainEventNotification<SavedActivity> notification,
            CancellationToken cancellationToken)
        {
            Activity activity = notification.DomainEvent.Activity;
            await _activitiesRepository.ScheduleActivities(activity);
        }
    }
}
