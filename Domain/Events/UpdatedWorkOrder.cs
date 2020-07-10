using Kaizen.Core.Domain;
using Kaizen.Domain.Entities;

namespace Kaizen.Domain.Events
{
    public class UpdatedWorkOrder : IDomainEvent
    {
        public UpdatedWorkOrder(WorkOrder workOrder)
        {
            WorkOrder = workOrder;
        }

        public WorkOrder WorkOrder { get; }
    }
}
