using Kaizen.Core.Domain;
using Kaizen.Domain.Entities;

namespace Kaizen.Domain.Events
{
    public class SavedPerson : IDomainEvent
    {
        public string FullName { get; }
        public string Email { get; }

        public SavedPerson(Person person)
        {
            FullName = $"{person.FirstName} {person.SecondName} {person.LastName} {person.SecondLastName}";
            Email = person.User.Email;
        }
    }
}
