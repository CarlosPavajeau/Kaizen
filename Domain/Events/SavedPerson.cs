using Kaizen.Core.Domain;
using Kaizen.Domain.Entities;

namespace Kaizen.Domain.Events
{
    public class SavedPerson : IDomainEvent
    {
        public SavedPerson(Client client, string emailConfirmationLink)
        {
            Client = client;
            EmailConfirmationLink = emailConfirmationLink;
        }

        public Client Client { get; }
        public string EmailConfirmationLink { get; }
    }
}
