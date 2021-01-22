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
            private readonly IClientsRepository _clientsRepository;
            private readonly IUnitWork _unitWork;

            public Handler(IHubContext<ActivityHub> hubContext, IMapper mapper, IMailService mailService, IActivitiesRepository activitiesRepository, IClientsRepository clientsRepository, IUnitWork unitWork)
            {
                _hubContext = hubContext;
                _mapper = mapper;
                _mailService = mailService;
                _activitiesRepository = activitiesRepository;
                _clientsRepository = clientsRepository;
                _unitWork = unitWork;
            }

            public async Task Handle(DomainEventNotification<SavedActivity> notification, CancellationToken cancellationToken)
            {
                Activity activity = notification.DomainEvent.Activity;

                await _activitiesRepository.ScheduleActivities(activity);
                await NotifyNewActivityRegister(activity, cancellationToken);
                await SendNotificationEmail(activity);
                await UpdateClientState(activity);

            }

            private async Task NotifyNewActivityRegister(Activity activity, CancellationToken cancellationToken)
            {
                ActivityViewModel activityModel = _mapper.Map<ActivityViewModel>(activity);
                await _hubContext.Clients.Groups("Clients").SendAsync("NewActivity", activityModel, cancellationToken);
            }

            private async Task SendNotificationEmail(Activity activity)
            {
                Client client = activity.Client;
                await _mailService.SendEmailAsync(client.User.Email, "Actividad pendiente", $"Estimado {client.LastName} {client.FirstName} hemos agendado " +
                    $"la actividad N° {activity.Code} para el día {activity.Date} para aplicar los servicios solicitados por usted");
            }

            private async Task UpdateClientState(Activity activity)
            {
                Client client = activity.Client;
                client.State = (activity.Periodicity == PeriodicityType.Casual) ? ClientState.Casual : ClientState.Active;

                _clientsRepository.Update(client);

                await _unitWork.SaveAsync();
            }
        }
    }
}
