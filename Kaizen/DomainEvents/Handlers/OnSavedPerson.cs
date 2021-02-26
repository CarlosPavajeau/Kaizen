using System;
using System.Threading;
using System.Threading.Tasks;
using Kaizen.Core.Services;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Events;
using Kaizen.Domain.Repositories;
using Kaizen.Extensions;
using Kaizen.Hubs;
using Kaizen.Middleware;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Kaizen.DomainEvents.Handlers
{
    public class OnSavedPerson
    {
        public class Handler : INotificationHandler<DomainEventNotification<SavedPerson>>
        {
            private readonly IApplicationUserRepository _applicationUserRepository;
            private readonly IMailService _mailService;
            private readonly IHubContext<ClientHub> _clientHub;
            private readonly IMailTemplate _mailTemplate;
            private readonly IStatisticsRepository _statisticsRepository;

            public Handler(IMailService mailService, IHubContext<ClientHub> clientHub, IMailTemplate mailTemplate,
                IApplicationUserRepository applicationUserRepository,
                IStatisticsRepository statisticsRepository)
            {
                _mailService = mailService;
                _clientHub = clientHub;
                _mailTemplate = mailTemplate;
                _statisticsRepository = statisticsRepository;
                _applicationUserRepository = applicationUserRepository;
            }

            public async Task Handle(DomainEventNotification<SavedPerson> notification,
                CancellationToken cancellationToken)
            {
                Client savedClient = notification.DomainEvent.Client;

                await SendConfirmationEmail(savedClient);
                await NotifyNewClientRegister(cancellationToken);
                await RegisterNewClient();
            }

            private async Task SendConfirmationEmail(Client client)
            {
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

            private async Task NotifyNewClientRegister(CancellationToken cancellationToken)
            {
                await _clientHub.Clients.Groups("Administrator", "OfficeEmployee")
                    .SendAsync("NewPersonRequest", cancellationToken: cancellationToken);
            }

            private async Task RegisterNewClient()
            {
                await _statisticsRepository.RegisterNewClientRegister();
            }
        }
    }
}
