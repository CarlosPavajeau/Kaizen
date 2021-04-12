using System.Threading;
using System.Threading.Tasks;
using Kaizen.Core.Services;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Events;
using MediatR;

namespace Kaizen.DomainEvents.Handlers
{
    public class SendEmailWhenSavedActivity : INotificationHandler<DomainEventNotification<SavedActivity>>
    {
        private readonly IMailService _mailService;
        private readonly IMailTemplate _mailTemplate;

        public SendEmailWhenSavedActivity(IMailService mailService, IMailTemplate mailTemplate)
        {
            _mailService = mailService;
            _mailTemplate = mailTemplate;
        }

        public async Task Handle(DomainEventNotification<SavedActivity> notification,
            CancellationToken cancellationToken)
        {
            Activity activity = notification.DomainEvent.Activity;
            Client client = activity.Client;

            string emailMessage = _mailTemplate.LoadTemplate("NewActivity.html",
                $"{client.LastName} {client.FirstName}", activity.Date.ToString("yyyy/MM/dd hh:mm tt"));

            await _mailService.SendEmailAsync(client.User.Email, "Solicitud de servicios", emailMessage, true);
        }
    }
}
