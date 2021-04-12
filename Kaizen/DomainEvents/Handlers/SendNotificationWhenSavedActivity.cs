using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Events;
using Kaizen.Hubs;
using Kaizen.Models.Activity;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Kaizen.DomainEvents.Handlers
{
    public class SendNotificationWhenSavedActivity : INotificationHandler<DomainEventNotification<SavedActivity>>
    {
        private readonly IHubContext<ActivityHub> _hubContext;
        private readonly IMapper _mapper;

        public SendNotificationWhenSavedActivity(IHubContext<ActivityHub> hubContext, IMapper mapper)
        {
            _hubContext = hubContext;
            _mapper = mapper;
        }

        public async Task Handle(DomainEventNotification<SavedActivity> notification,
            CancellationToken cancellationToken)
        {
            Activity activity = notification.DomainEvent.Activity;
            ActivityViewModel activityModel = _mapper.Map<ActivityViewModel>(activity);

            await _hubContext.Clients.Groups("Clients").SendAsync("NewActivity", activityModel, cancellationToken);
        }
    }
}
