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
    public class SendNotificationWhenPaidInvoice : INotificationHandler<DomainEventNotification<PaidInvoice>>
    {
        private readonly IStatisticsRepository _statisticsRepository;
        private readonly IHubContext<InvoiceHub> _invoiceHub;
        private readonly IMapper _mapper;

        public SendNotificationWhenPaidInvoice(IStatisticsRepository statisticsRepository,
            IHubContext<InvoiceHub> invoiceHub,
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

            if (notification.DomainEvent.Invoice is ProductInvoice productInvoice)
            {
                await _invoiceHub.Clients.Group("Administrator")
                    .SendAsync("OnPaidProductInvoice", _mapper.Map<ProductInvoiceViewModel>(productInvoice),
                        cancellationToken);
            }
            else if (notification.DomainEvent.Invoice is ServiceInvoice serviceInvoice)
            {
                await _invoiceHub.Clients.Group("Administrator").SendAsync("OnPaidServiceInvoice",
                    _mapper.Map<ServiceInvoiceViewModel>(serviceInvoice), cancellationToken);
            }
        }
    }
}
