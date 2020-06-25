using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Core.Services;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Events;
using Kaizen.Domain.Repositories;
using Kaizen.Hubs;
using Kaizen.Models.Activity;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Kaizen.DomainEvents.Handlers
{
    public class OnSavedActivity
    {
        public class Handler : INotificationHandler<DomainEventNotification<SavedActivity>>
        {
            private readonly IHubContext<ActivityHub> _hubContext;
            private readonly IMapper _mapper;
            private readonly IMailService _mailService;
            private readonly IActivitiesRepository _activitiesRepository;
            public Handler(IHubContext<ActivityHub> hubContext, IMapper mapper, IMailService mailService, IActivitiesRepository activitiesRepository)
            {
                _hubContext = hubContext;
                _mapper = mapper;
                _mailService = mailService;
                _activitiesRepository = activitiesRepository;
            }

            public async Task Handle(DomainEventNotification<SavedActivity> notification, CancellationToken cancellationToken)
            {
                ActivityViewModel activityModel = _mapper.Map<ActivityViewModel>(notification.DomainEvent.Activity);
                await _hubContext.Clients.Groups("Clients").SendAsync("NewActivity", activityModel);
                await _activitiesRepository.ScheduleActivities(notification.DomainEvent.Activity);
                Client client = notification.DomainEvent.Activity.Client;

                await _mailService.SendEmailAsync(client.User.Email, "Actividad pendiente", $"Estimado {client.LastName} {client.FirstName} hemos agendado " +
                    $"la actividad N° {activityModel.Code} para el día {activityModel.Date} para aplicar los servicios solicitados por usted");
            }
        }
    }
}
