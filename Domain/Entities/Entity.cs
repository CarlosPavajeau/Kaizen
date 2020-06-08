using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations.Schema;
using Kaizen.Core.Domain;

namespace Kaizen.Domain.Entities
{
    public abstract class Entity : IEntity
    {
        [NotMapped]
        private readonly ConcurrentQueue<IDomainEvent> _domainEvents = new ConcurrentQueue<IDomainEvent>();
        [NotMapped]
        public IProducerConsumerCollection<IDomainEvent> DomainEvents => _domainEvents;

        public void PublishEvent(IDomainEvent @event)
        {
            _domainEvents.Enqueue(@event);
        }
    }
}
