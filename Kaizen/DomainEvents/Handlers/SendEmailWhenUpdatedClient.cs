using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Events;
using Kaizen.Domain.Repositories;
using Kaizen.Hubs;
using Kaizen.Models.Notification;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Kaizen.DomainEvents.Handlers
{
    public class SendEmailWhenUpdatedClient : INotificationHandler<DomainEventNotification<UpdatedClient>>
    {
        private readonly INotificationsRepository _notificationsRepository;
        private readonly IUnitWork _unitWork;
        private readonly IHubContext<NotificationHub> _notificationsHub;
        private readonly IMapper _mapper;

        public SendEmailWhenUpdatedClient(INotificationsRepository notificationsRepository, IUnitWork unitWork,
            IHubContext<NotificationHub> hubContext, IMapper mapper)
        {
            _notificationsRepository = notificationsRepository;
            _unitWork = unitWork;
            _notificationsHub = hubContext;
            _mapper = mapper;
        }

        public async Task Handle(DomainEventNotification<UpdatedClient> notification,
            CancellationToken cancellationToken)
        {
            var client = notification.DomainEvent.Client;
            if (client.State != ClientState.Acceptep && client.State != ClientState.Rejected)
            {
                return;
            }

            var message = client.State switch
            {
                ClientState.Acceptep => "Hemos aceptado tu solicitud de ser nuestro cliente.",
                ClientState.Rejected => "Desafortunadamente hemos rechazado tu solicitud de ser nuestro cliente.",
                _ => string.Empty
            };
            var clientNotification = new Notification
            {
                Title = "Respuesta de solicitud de cliente",
                Message = message,
                Icon = "question_answer",
                State = NotificationState.Pending,
                UserId = client.UserId
            };

            _notificationsRepository.Insert(clientNotification);
            await _unitWork.SaveAsync();

            var notificationViewModel = _mapper.Map<NotificationViewModel>(clientNotification);
            await _notificationsHub.Clients.User(client.UserId).SendAsync("OnNewNotification",
                notificationViewModel, cancellationToken);
        }
    }
}
