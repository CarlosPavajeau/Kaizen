using System.Threading;
using System.Threading.Tasks;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Events;
using Kaizen.Domain.Repositories;
using MediatR;

namespace Kaizen.DomainEvents.Handlers
{
    public class OnPaidServiceInvoice
    {
        public class Handler : INotificationHandler<DomainEventNotification<PaidServiceInvoice>>
        {
            private readonly IStatisticsRepository _statisticsRepository;

            public Handler(IStatisticsRepository statisticsRepository)
            {
                _statisticsRepository = statisticsRepository;
            }

            public async Task Handle(DomainEventNotification<PaidServiceInvoice> notification, CancellationToken cancellationToken)
            {
                ServiceInvoice serviceInvoice = notification.DomainEvent.ServiceInvoice;

                await _statisticsRepository.RegisterProfits(serviceInvoice.Total);
            }
        }
    }
}
