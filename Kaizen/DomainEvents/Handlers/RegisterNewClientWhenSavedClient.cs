using System.Threading;
using System.Threading.Tasks;
using Kaizen.Domain.Events;
using Kaizen.Domain.Repositories;
using MediatR;

namespace Kaizen.DomainEvents.Handlers
{
    public class RegisterNewClientWhenSavedClient : INotificationHandler<DomainEventNotification<SavedPerson>>
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public RegisterNewClientWhenSavedClient(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public async Task Handle(DomainEventNotification<SavedPerson> notification, CancellationToken cancellationToken)
        {
            await _statisticsRepository.RegisterNewClientRegister();
        }
    }
}
