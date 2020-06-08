using System.Collections.Concurrent;

namespace Kaizen.Core.Domain
{
    public interface IEntity
    {
        IProducerConsumerCollection<IDomainEvent> DomainEvents { get; }

        void PublishEvent(IDomainEvent @event);
    }
}
