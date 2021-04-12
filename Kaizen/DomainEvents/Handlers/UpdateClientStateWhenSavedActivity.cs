using System.Threading;
using System.Threading.Tasks;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Events;
using Kaizen.Domain.Repositories;
using MediatR;

namespace Kaizen.DomainEvents.Handlers
{
    public class UpdateClientStateWhenSavedActivity : INotificationHandler<DomainEventNotification<SavedActivity>>
    {
        private readonly IClientsRepository _clientsRepository;
        private readonly IUnitWork _unitWork;

        public UpdateClientStateWhenSavedActivity(IClientsRepository clientsRepository, IUnitWork unitWork)
        {
            _clientsRepository = clientsRepository;
            _unitWork = unitWork;
        }

        public async Task Handle(DomainEventNotification<SavedActivity> notification,
            CancellationToken cancellationToken)
        {
            Activity activity = notification.DomainEvent.Activity;
            Client client = activity.Client;

            client.State = (activity.Periodicity == PeriodicityType.Casual)
                ? ClientState.Casual
                : ClientState.Active;

            _clientsRepository.Update(client);

            await _unitWork.SaveAsync();
        }
    }
}
