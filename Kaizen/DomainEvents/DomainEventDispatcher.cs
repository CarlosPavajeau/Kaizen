using System;
using System.Threading.Tasks;
using Kaizen.Core.Domain;
using MediatR;

namespace Kaizen.DomainEvents
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IMediator _mediator;

        public DomainEventDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Dispatch(IDomainEvent @event)
        {
            INotification domainEventNotification = CreateDomainEventNotification(@event);

            await _mediator.Publish(domainEventNotification);
        }

        private static INotification CreateDomainEventNotification(IDomainEvent domainEvent)
        {
            Type genericDispatcherType = typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType());
            return (INotification)Activator.CreateInstance(genericDispatcherType, domainEvent);
        }
    }
}
