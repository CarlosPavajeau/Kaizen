using System.Threading;
using System.Threading.Tasks;
using Kaizen.Core.Services;
using Kaizen.Domain.Events;
using Kaizen.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Kaizen.DomainEvents.Handlers
{
    public class OnSavedPerson
    {
        public class Handler : INotificationHandler<DomainEventNotification<SavedPerson>>
        {
            public readonly IMailService _mailService;
            private readonly IHubContext<ClientHub> _clientHub;

            public Handler(IMailService mailService, IHubContext<ClientHub> clientHub)
            {
                _mailService = mailService;
                _clientHub = clientHub;
            }

            public async Task Handle(DomainEventNotification<SavedPerson> notification, CancellationToken cancellationToken)
            {
                SavedPerson domainEvent = notification.DomainEvent;

                await _mailService.SendEmailAsync(domainEvent.Email, "Cliente Registrado", $"Hola, {domainEvent.FullName} acabas de registrarte en nuestra empresa.");
                await _clientHub.Clients.Groups("Administrator", "OfficeEmployee").SendAsync("NewPersonRequest");
            }
        }
    }
}
