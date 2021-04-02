using Kaizen.Core.Domain;
using Kaizen.Domain.Entities;

namespace Kaizen.Domain.Events
{
    public class UpdatedClient : IDomainEvent
    {
        public UpdatedClient(Client client)
        {
            Client = client;
        }

        public Client Client { get; }
    }
}
