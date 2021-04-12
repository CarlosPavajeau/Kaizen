using System;
using System.Threading;
using System.Threading.Tasks;
using Kaizen.Core.Services;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Events;
using Kaizen.Domain.Repositories;
using Kaizen.Extensions;
using Kaizen.Middleware;
using MediatR;

namespace Kaizen.DomainEvents.Handlers
{
    public class SendEmailWhenSavedClient : INotificationHandler<DomainEventNotification<SavedPerson>>
    {
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly IMailTemplate _mailTemplate;
        private readonly IMailService _mailService;

        public SendEmailWhenSavedClient(IApplicationUserRepository applicationUserRepository,
            IMailTemplate mailTemplate, IMailService mailService)
        {
            _applicationUserRepository = applicationUserRepository;
            _mailTemplate = mailTemplate;
            _mailService = mailService;
        }

        public async Task Handle(DomainEventNotification<SavedPerson> notification, CancellationToken cancellationToken)
        {
            Client client = notification.DomainEvent.Client;

            string emailConfirmationToken =
                await _applicationUserRepository.GenerateEmailConfirmationTokenAsync(client.User);
            UriBuilder uriBuilder = new UriBuilder(KaizenHttpContext.BaseUrl)
            {
                Path = "user/ConfirmEmail",
                Query = $"token={emailConfirmationToken.Base64ForUrlEncode()}&email={client.User.Email}"
            };
            string emailConfirmationLink = uriBuilder.ToString();

            string emailMessage = _mailTemplate.LoadTemplate("NewClient.html",
                $"{client.FirstName} {client.LastName}",
                $"{client.TradeName}", $"{client.ClientAddress.City}",
                $"{client.ClientAddress.Neighborhood}", $"{client.ClientAddress.Street}",
                $"{emailConfirmationLink}");

            await _mailService.SendEmailAsync(client.User.Email, "Cliente Registrado", emailMessage, true);
        }
    }
}
