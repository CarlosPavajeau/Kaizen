using System.Threading;
using System.Threading.Tasks;
using Kaizen.Domain.Data;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Events;
using Kaizen.Domain.Repositories;
using MediatR;

namespace Kaizen.DomainEvents.Handlers
{
    public class
        GenerateCertificateWhenUpdatedWorkOrder : INotificationHandler<DomainEventNotification<UpdatedWorkOrder>>
    {
        private readonly ICertificatesRepository _certificatesRepository;
        private readonly ApplicationDbContext _dbContext;

        public GenerateCertificateWhenUpdatedWorkOrder(ICertificatesRepository certificatesRepository,
            ApplicationDbContext dbContext)
        {
            _certificatesRepository = certificatesRepository;
            _dbContext = dbContext;
        }

        public async Task Handle(DomainEventNotification<UpdatedWorkOrder> notification,
            CancellationToken cancellationToken)
        {
            var workOrder = notification.DomainEvent.WorkOrder;
            if (workOrder.WorkOrderState == WorkOrderState.Valid)
            {
                var certificate = new Certificate
                {
                    WorkOrderCode = workOrder.Code,
                    Validity = workOrder.ExecutionDate.AddDays(180)
                };

                _certificatesRepository.Insert(certificate);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
