using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Events;
using Kaizen.Domain.Repositories;
using Kaizen.Hubs;
using Kaizen.Models.ProductInvoice;
using Kaizen.Models.ServiceInvoice;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Kaizen.DomainEvents.Handlers
{
    public class OnPaidInvoice
    {
        public class Handler : INotificationHandler<DomainEventNotification<PaidInvoice>>
        {
            private readonly IStatisticsRepository _statisticsRepository;
            private readonly IHubContext<InvoiceHub> _invoiceHub;
            private readonly IMapper _mapper;

            public Handler(IStatisticsRepository statisticsRepository, IHubContext<InvoiceHub> invoiceHub,
                IMapper mapper)
            {
                _statisticsRepository = statisticsRepository;
                _invoiceHub = invoiceHub;
                _mapper = mapper;
            }

            public async Task Handle(DomainEventNotification<PaidInvoice> notification,
                CancellationToken cancellationToken)
            {
                await _statisticsRepository.RegisterProfits(notification.DomainEvent.Invoice.Total);

                switch (notification.DomainEvent.Invoice)
                {
                    case ProductInvoice productInvoice:
                    {
                        await _invoiceHub.Clients.Group("Administrator")
                            .SendAsync("OnPaidProductInvoice", _mapper.Map<ProductInvoiceViewModel>(productInvoice),
                                cancellationToken);
                        break;
                    }
                    case ServiceInvoice serviceInvoice:
                    {
                        await _invoiceHub.Clients.Group("Administrator").SendAsync("OnPaidServiceInvoice",
                            _mapper.Map<ServiceInvoiceViewModel>(serviceInvoice), cancellationToken);
                        break;
                    }
                }
            }
        }
    }
}
