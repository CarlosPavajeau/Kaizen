using System.Threading;
using System.Threading.Tasks;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Events;
using Kaizen.Domain.Repositories;
using MediatR;

namespace Kaizen.DomainEvents.Handlers
{
    public class OnUpdatedWorkOrder
    {
        public class Handler : INotificationHandler<DomainEventNotification<UpdatedWorkOrder>>
        {
            private readonly IUnitWork _unitWork;
            public Handler(IUnitWork unitWork)
            {
                _unitWork = unitWork;
            }

            public async Task Handle(DomainEventNotification<UpdatedWorkOrder> notification, CancellationToken cancellationToken)
            {
                WorkOrder workOrder = notification.DomainEvent.WorkOrder;

                if (workOrder.WorkOrderState == WorkOrderState.Valid)
                {
                    Activity activity = await _unitWork.Activities.FindByIdAsync(workOrder.ActivityCode);
                    if (activity is null)
                        return;

                    activity.State = ActivityState.Applied;
                    _unitWork.Activities.Update(activity);
                    await _unitWork.SaveAsync();
                }
            }
        }
    }
}
