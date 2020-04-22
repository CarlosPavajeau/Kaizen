using Kaizen.Domain.Entities;
using Kaizen.InputModels;

namespace Kaizen.ViewModels
{
    public class ContactPersonViewModel : ContactPersonInputModel
    {
        public ContactPersonViewModel(ContactPerson contactPerson)
        {
            Name = contactPerson.Name;
            Phonenumber = contactPerson.PhoneNumber;
        }
    }
}
