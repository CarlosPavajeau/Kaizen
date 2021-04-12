using System;
using System.Threading;
using System.Threading.Tasks;
using Kaizen.Core.Services;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Events;
using Kaizen.Domain.Repositories;
using Kaizen.Middleware;
using MediatR;

namespace Kaizen.DomainEvents.Handlers
{
    public class
        SendEmailWhenUpdatedServiceRequest : INotificationHandler<DomainEventNotification<UpdatedServiceRequest>>
    {
        private readonly IClientsRepository _clientsRepository;
        private readonly IMailTemplate _mailTemplate;
        private readonly IMailService _mailService;

        public SendEmailWhenUpdatedServiceRequest(IClientsRepository clientsRepository, IMailTemplate mailTemplate,
            IMailService mailService)
        {
            _clientsRepository = clientsRepository;
            _mailTemplate = mailTemplate;
            _mailService = mailService;
        }

        public async Task Handle(DomainEventNotification<UpdatedServiceRequest> notification,
            CancellationToken cancellationToken)
        {
            var serviceRequest = notification.DomainEvent.ServiceRequest;
            if (serviceRequest.State == ServiceRequestState.Pending)
            {
                return;
            }

            var client =
                await _clientsRepository.GetClientWithUser(serviceRequest.ClientId);

            string responseMessage = serviceRequest.State switch
            {
                ServiceRequestState.Accepted =>
                    "Hemos aceptado tu solicitud de servicio y agendamos las actividades a aplicar.",
                ServiceRequestState.Rejected => "Desafortunadamente hemos rechazado tu solicitud de servicio.",
                ServiceRequestState.PendingSuggestedDate =>
                    "Te hemos sugerido una nueva fecha de aplicación de nuestros servicios.",
                _ => "Tu solicitud aún está pendiente"
            };

            var responseUrl = new UriBuilder(KaizenHttpContext.BaseUrl)
            {
                Path = serviceRequest.State switch
                {
                    ServiceRequestState.Accepted => "/activity_schedule/client_schedule",
                    ServiceRequestState.Rejected => "/service_requests/register",
                    ServiceRequestState.PendingSuggestedDate => "/service_requests/new_date",
                    _ => $"/service_requests/{serviceRequest.Code}"
                }
            };

            var responseButtonMessage = serviceRequest.State switch
            {
                ServiceRequestState.Accepted => "Ver mi calendario de actividades",
                ServiceRequestState.Rejected => "Intentar hacer otra solicitud",
                ServiceRequestState.PendingSuggestedDate => "Ver fecha sugerida",
                _ => "Ver solicitud"
            };

            string emailMessage = _mailTemplate.LoadTemplate("ServiceRequestResponse.html",
                $"{client.FirstName} {client.LastName}",
                responseMessage,
                responseUrl.ToString(),
                responseButtonMessage);

            await _mailService.SendEmailAsync(
                client.User.Email,
                "Respuesta de solicitud de servicio",
                emailMessage,
                true);
        }
    }
}
