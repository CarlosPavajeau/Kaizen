using System.Threading;
using System.Threading.Tasks;
using Kaizen.Core.Services;
using Kaizen.Domain.Entities;
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
            private readonly IMailTemplate _mailTemplate;

            public Handler(IMailService mailService, IHubContext<ClientHub> clientHub, IMailTemplate mailTemplate)
            {
                _mailService = mailService;
                _clientHub = clientHub;
                _mailTemplate = mailTemplate;
            }

            public async Task Handle(DomainEventNotification<SavedPerson> notification, CancellationToken cancellationToken)
            {
                Client savedPerson = notification.DomainEvent.Client;
                string email = _mailTemplate.LoadTemplate("NewClient.html", $"{savedPerson.FirstName} {savedPerson.LastName}",
                                                          $"{savedPerson.TradeName}", $"{savedPerson.ClientAddress.City}",
                                                          $"{savedPerson.ClientAddress.Neighborhood}", $"{savedPerson.ClientAddress.Street}",
                                                          $"{notification.DomainEvent.EmailConfirmationLink}");

                await _mailService.SendEmailAsync(savedPerson.User.Email, "Cliente Registrado", email, true);
                await _clientHub.Clients.Groups("Administrator", "OfficeEmployee").SendAsync("NewPersonRequest");
            }
        }
    }
}
