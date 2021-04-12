using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Core.Services;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Events;
using Kaizen.Domain.Repositories;
using Kaizen.Hubs;
using Kaizen.Middleware;
using Kaizen.Models.Notification;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Kaizen.DomainEvents.Handlers
{
    public class OnUpdatedServiceRequest
    {
        public class Handler : INotificationHandler<DomainEventNotification<UpdatedServiceRequest>>
        {
            private readonly IHubContext<NotificationHub> _notificationsHub;
            private readonly IMapper _mapper;
            private readonly IClientsRepository _clientsRepository;
            private readonly INotificationsRepository _notificationsRepository;
            private readonly IUnitWork _unitWork;
            private readonly IMailTemplate _mailTemplate;
            private readonly IMailService _mailService;

            public Handler(IHubContext<NotificationHub> notificationsHub, IClientsRepository clientsRepository,
                INotificationsRepository notificationsRepository, IUnitWork unitWork, IMapper mapper,
                IMailTemplate mailTemplate, IMailService mailService)
            {
                _notificationsHub = notificationsHub;
                _clientsRepository = clientsRepository;
                _notificationsRepository = notificationsRepository;
                _unitWork = unitWork;
                _mapper = mapper;
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

                var clientNotification = await SaveServiceRequestNotification(serviceRequest, client.User);
                if (clientNotification is null)
                {
                    return;
                }

                await SendServiceRequestNotification(clientNotification, client.User.Id, cancellationToken);
                await SendServiceRequestResponseEmail(serviceRequest, client);
            }

            private async Task SendServiceRequestResponseEmail(ServiceRequest serviceRequest, Client client)
            {
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

            private async Task<Notification> SaveServiceRequestNotification(ServiceRequest serviceRequest,
                ApplicationUser user)
            {
                var message = serviceRequest.State switch
                {
                    ServiceRequestState.Accepted =>
                        "Hemos aceptado tu solicitud de servicio y agendamos las actividades a aplicar. " +
                        "Consulta tu calendario.",
                    ServiceRequestState.Rejected => "Desafortunadamente hemos rechazado tu solicitud de servicio.",
                    ServiceRequestState.PendingSuggestedDate =>
                        "Te hemos sugerido una nueva fecha de aplicación de nuestros servicios." +
                        "Puedes aceptarla o sugerirnos otra.",
                    _ => string.Empty
                };

                if (string.IsNullOrEmpty(message))
                {
                    return null;
                }

                var serviceRequestUrl = serviceRequest.State == ServiceRequestState.PendingSuggestedDate
                    ? "/service_requests/new_date"
                    : $"/service_requests/{serviceRequest.Code}";

                var notification = new Notification
                {
                    Title = "Respuesta de solicitud de servicio",
                    Message = message,
                    Icon = "question_answer",
                    State = NotificationState.Pending,
                    Url = serviceRequestUrl,
                    UserId = user.Id
                };

                _notificationsRepository.Insert(notification);
                await _unitWork.SaveAsync();

                return notification;
            }

            private async Task SendServiceRequestNotification(Notification clientNotification, string userid,
                CancellationToken cancellationToken)
            {
                var notificationViewModel = _mapper.Map<NotificationViewModel>(clientNotification);
                await _notificationsHub.Clients.User(userid).SendAsync("OnNewNotification",
                    notificationViewModel, cancellationToken);
            }
        }
    }
}
