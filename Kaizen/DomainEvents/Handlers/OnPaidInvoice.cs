using System.Threading;
using System.Threading.Tasks;
using Kaizen.Domain.Events;
using Kaizen.Domain.Repositories;
using MediatR;

namespace Kaizen.DomainEvents.Handlers
{
    public class OnPaidInvoice
    {
        public class Handler : INotificationHandler<DomainEventNotification<PaidInvoice>>
        {
            private readonly IStatisticsRepository _statisticsRepository;

            public Handler(IStatisticsRepository statisticsRepository)
            {
                _statisticsRepository = statisticsRepository;
            }

            public async Task Handle(DomainEventNotification<PaidInvoice> notification, CancellationToken cancellationToken)
            {
                await _statisticsRepository.RegisterProfits(notification.DomainEvent.Invoice.Total);
            }
        }
    }
}
