using System.Threading.Tasks;

namespace Kaizen.Core.Domain
{
    public interface IDomainEventDispatcher
    {
        Task Dispatch(IDomainEvent @event);
    }
}
