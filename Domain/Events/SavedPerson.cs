using Kaizen.Core.Domain;
using Kaizen.Domain.Entities;

namespace Kaizen.Domain.Events
{
    public class SavedPerson : IDomainEvent
    {
        public SavedPerson(Client client)
        {
            Client = client;
        }

        public Client Client { get; }
    }
}
