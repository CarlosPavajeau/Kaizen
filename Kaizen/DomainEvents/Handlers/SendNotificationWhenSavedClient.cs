using System.Threading;
using System.Threading.Tasks;
using Kaizen.Domain.Events;
using Kaizen.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Kaizen.DomainEvents.Handlers
{
    public class SendNotificationWhenSavedClient : INotificationHandler<DomainEventNotification<SavedPerson>>
    {
        private readonly IHubContext<ClientHub> _clientHub;


        public SendNotificationWhenSavedClient(IHubContext<ClientHub> clientHub)
        {
            _clientHub = clientHub;
        }

        public async Task Handle(DomainEventNotification<SavedPerson> notification,
            CancellationToken cancellationToken)
        {
            await _clientHub.Clients.Groups("Administrator", "OfficeEmployee")
                .SendAsync("NewPersonRequest", cancellationToken);
        }
    }
}
