using System.Threading;
using System.Threading.Tasks;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Events;
using Kaizen.Domain.Repositories;
using MediatR;

namespace Kaizen.DomainEvents.Handlers
{
    public class
        RegisterNewAppliedActivityWhenUpdatedWorkOrder : INotificationHandler<DomainEventNotification<UpdatedWorkOrder>>
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public RegisterNewAppliedActivityWhenUpdatedWorkOrder(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public async Task Handle(DomainEventNotification<UpdatedWorkOrder> notification,
            CancellationToken cancellationToken)
        {
            WorkOrder workOrder = notification.DomainEvent.WorkOrder;

            if (workOrder.WorkOrderState == WorkOrderState.Valid)
            {
                await _statisticsRepository.RegisterNewAppliedActivity();
            }
        }
    }
}
