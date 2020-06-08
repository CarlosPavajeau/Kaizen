using System.Threading;
using System.Threading.Tasks;
using Kaizen.Core.Services;
using Kaizen.Domain.Events;
using MediatR;

namespace Kaizen.DomainEvents.Handlers
{
    public class OnSavedPerson
    {
        public class Handler : INotificationHandler<DomainEventNotification<SavedPerson>>
        {
            public readonly IMailService _mailService;

            public Handler(IMailService mailService)
            {
                _mailService = mailService;
            }

            public async Task Handle(DomainEventNotification<SavedPerson> notification, CancellationToken cancellationToken)
            {
                SavedPerson domainEvent = notification.DomainEvent;

                await _mailService.SendEmailAsync(domainEvent.Email, "Cliente Registrado", $"Hola, {domainEvent.FullName}");
            }
        }
    }
}
