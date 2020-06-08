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

        public async Task Dispatch(IDomainEvent devent)
        {
            INotification domainEventNotification = CreateDomainEventNotification(devent);

            await _mediator.Publish(domainEventNotification);
        }

        private INotification CreateDomainEventNotification(IDomainEvent domainEvent)
        {
            Type genericDispatcherType = typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType());
            return (INotification)Activator.CreateInstance(genericDispatcherType, domainEvent);
        }
    }
}
